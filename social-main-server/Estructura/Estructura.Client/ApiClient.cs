using Estructura.Client.Interfaces;
using Estructura.Client.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Estructura.Client
{
    public class ApiClient
    {
        public ICommand RefreshingTokenCommand { get; set; }
        public ICommand RefreshedTokenCommand { get; set; }
        public ICommand FailedToRefreshTokenCommand { get; set; }
        private int TokenExpirationMins = 60;
        public static DateTime NextExpirationDate { get; private set; }
        private static HttpClient _clientInternal;
        private string RefreshToken { get; set; }
        private static bool IsRefreshing = false;

        private HttpClient _client
        {
            get
            {
                Task.Run(async () =>
                {
                    if (NextExpirationDate.AddHours(1) < DateTime.UtcNow.AddMinutes(5)) // Must logout
                    {
                        if (FailedToRefreshTokenCommand != null)
                            FailedToRefreshTokenCommand.Execute(_clientInternal);
                    }
                    else if (NextExpirationDate < DateTime.UtcNow.AddMinutes(1) && _clientInternal.DefaultRequestHeaders.Authorization != null)
                    {
                        if (IsRefreshing) return;
                        IsRefreshing = true;
                        if (RefreshingTokenCommand != null)
                            RefreshingTokenCommand.Execute(_clientInternal);
                        var refresh = await Refresh();
                        if (refresh)
                        {
                            if (RefreshedTokenCommand != null)
                                RefreshedTokenCommand.Execute(_clientInternal);
                        }
                        else //Must logout
                        {
                            if (FailedToRefreshTokenCommand != null)
                                FailedToRefreshTokenCommand.Execute(_clientInternal);
                        }
                        IsRefreshing = false;
                    }
                });
                return _clientInternal;
            }
        }

        public ApiClient(string authToken, string baseUrl, string refreshToken)
        {
            _clientInternal = _clientInternal ?? new HttpClient();
            this.RefreshToken = refreshToken;
            _client.CancelPendingRequests();
            if (string.IsNullOrWhiteSpace(_client.BaseAddress?.AbsoluteUri))
                _client.BaseAddress = new System.Uri(baseUrl);
            SetClientAuthorization(authToken);
        }

        public ApiClient(string baseUrl)
        {
            NextExpirationDate = DateTime.UtcNow.AddMinutes(-TokenExpirationMins);
            _clientInternal = _clientInternal ?? new HttpClient();
            _client.CancelPendingRequests();
            if (string.IsNullOrWhiteSpace(_client.BaseAddress?.AbsoluteUri))
                _client.BaseAddress = new System.Uri(baseUrl);
        }

        private void SetClientAuthorization(string authToken)
        {
            // Update refresh time here and json config files (3)
            NextExpirationDate = DateTime.UtcNow.AddMinutes(TokenExpirationMins);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
        }

        private async Task<bool> Refresh()
        {
            try
            {
                var ident = await AuthService.RefreshTokenAsync(
                    new Common.Request.RefreshTokenRequest()
                    {
                        AppBuildVersion = 9999,
                        Token = this.RefreshToken
                    });
                if (ident == null || !ident.Sucess || string.IsNullOrWhiteSpace(ident.Token))
                    return false;
                SetClientAuthorization(ident.Token);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


        private IAuthService _loginService;
        private IUtilitiesService _utilitiesService;
        private IAccountService _accountService;
        private ICandidateService _candidateService;
        private IStudyService _studyService;
        private IVisitService _visitService;
        private IMediaService _mediaService;
        private IFileService _fileService;
        private IStudyFinalResultService _studyFinalResultService;
        private IStudyGeneralInformationService _studyGeneralInformationService;
        private IStudySchoolarityService _studySchoolarityService;
        private IStudyFamilyService _studyFamilyService;
        private IStudyEconomicSituationService _studyEconomicSituationService;
        private IStudySocialService _studySocialService;
        private IStudyLaboralTrayectoryService _studyLaboralTrayectoryService;
        private IStudyIMSSValidationService _studyIMSSValidationService;
        private IStudyPersonalReferenceService _studyPersonalReferenceService;
        private IStudyPicturesService _studyPicturesService;


        public IAuthService AuthService => _loginService ?? (_loginService = new AuthService(_client));
        public IUtilitiesService UtilitiesService => _utilitiesService ?? (_utilitiesService = new UtilitiesService(_client));
        public IAccountService AccountService => _accountService ?? (_accountService = new AccountService(_client));
        public ICandidateService CandidateService => _candidateService ?? (_candidateService = new CandidateService(_client));
        public IStudyService StudyService => _studyService ?? (_studyService = new StudyService(_client));
        public IVisitService VisitService => _visitService ?? (_visitService = new VisitService(_client));
        public IMediaService MediaService => _mediaService ?? (_mediaService = new MediaService(_client));
        public IFileService FileService => _fileService ??(_fileService = new FileService(_client));
        public IStudyFinalResultService StudyFinalResultService => _studyFinalResultService ?? (_studyFinalResultService = new StudyFinalResultService(_client));
        public IStudyGeneralInformationService StudyGeneralInformationService => _studyGeneralInformationService ??(_studyGeneralInformationService = new StudyGeneralInformationService(_client));
        public IStudySchoolarityService StudySchoolarityService => _studySchoolarityService ??(_studySchoolarityService = new StudySchoolarityService(_client));
        public IStudyFamilyService StudyFamilyService => _studyFamilyService ??(_studyFamilyService = new StudyFamilyService(_client));
        public IStudyEconomicSituationService StudyEconomicSituationService => _studyEconomicSituationService ??(_studyEconomicSituationService = new StudyEconomicSituationService(_client));
        public IStudySocialService StudySocialService => _studySocialService ??(_studySocialService = new StudySocialService(_client));
        public IStudyLaboralTrayectoryService StudyLaboralTrayectoryService => _studyLaboralTrayectoryService ??(_studyLaboralTrayectoryService = new StudyLaboralTrayectoryService(_client));
        public IStudyIMSSValidationService StudyIMSSValidationService => _studyIMSSValidationService ??(_studyIMSSValidationService = new StudyIMSSValidationService(_client));
        public IStudyPersonalReferenceService StudyPersonalReferenceService => _studyPersonalReferenceService ??(_studyPersonalReferenceService = new StudyPersonalReferenceService(_client));
        public IStudyPicturesService StudyPicturesService => _studyPicturesService ??(_studyPicturesService = new StudyPicturesService(_client));
    }
}
