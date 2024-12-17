using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidate;
        private readonly IAccountService _accountService;
        private readonly IMediaService _mediaService;
        public CandidateService(ICandidateRepository _candidate, IMediaService _mediaService, IAccountService _accountService)
        {
            this._candidate=_candidate;
            this._accountService = _accountService;
            this._mediaService=_mediaService;
        }
       
        public async Task<GenericResponse<Candidate>> CreateCandidate(Candidate request)
        {
            return await _candidate.CreateCandidate(request);
        }


        public async Task<GenericResponse<Candidate>> DeleteCandidate(long Id)
        {
            return await _candidate.DeleteCandidate(Id);
        }

        public async Task<GenericResponse<List<Candidate>>> GetCandidate(List<long> Id, int currentPage, int offset)
        {
            var current = await _candidate.GetCandidate(Id, currentPage, offset);
            if (current==null||!current.Sucess)
            {
                return new GenericResponse<List<Candidate>>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Candidates not found"
                };
            }

            List<long> clientIds = new List<long>();
            //current.Response.ForEach(e => clientIds.Add(e.Client.Id));
            for (int i = 0; i < current.Response.Count; i++)
            {
                var client = current.Response[i].Client;

                if (client?.Id != null) // Verifica si Client y Client.Id no son nulos
                {
                    Console.WriteLine($"Client Id: {client.Id}");
                    clientIds.Add(client.Id);
                }
                else
                {
                    Console.WriteLine("Client or Client.Id is null.");
                }
            }

            var clients = await _accountService.GetActiveUsers(clientIds, 0, 1000);
            if (clients.Sucess)
            {
                current.Response.ForEach(e =>
                {
                    e.Client = clients.Response.FirstOrDefault(f => f.Id == e.Client.Id);
                });
            }

            List<long> media = new List<long>();
            current.Response.ForEach(e =>
            {
                if (e.Media!=null&&e.Media.Id!=0)
                    media.Add(e.Media.Id);
            });

            var mediaItems = await _mediaService.GetMedia(media);
            if (mediaItems!=null)
            {
                current.Response.ForEach(e =>
                {
                    if(e.Media!=null&&e.Media.Id!=0)
                    e.Media = mediaItems.Response.FirstOrDefault(m => m.Id == e.Media.Id);
                });
            }

            return current;
        }

        public async Task<GenericResponse<int>> Pagination(string key = "", long clientId = 0, int splitBy=10)
        {
            return await _candidate.Pagination(key, clientId, splitBy);
        }

        public async Task<GenericResponse<List<Candidate>>> SearchCandidate(string key, long clientId = 0, int currentPage = 0, int offset = 10)
        {
            var candidates = await _candidate.SearchCandidate(key, clientId, currentPage, offset);
            if (candidates == null || !candidates.Sucess)
                return candidates;
            List<long> candidateIds = new List<long>();
            candidates.Response.ForEach(e => candidateIds.Add(e.Id));


            List<long> media = new List<long>();
            candidates.Response.ForEach(e =>
            {
                if (e.Media!=null&&e.Media.Id!=0)
                    media.Add(e.Media.Id);
            });

            var mediaItems = await _mediaService.GetMedia(media);
            if (mediaItems!=null)
            {
                candidates.Response.ForEach(e =>
                {
                    if (e.Media!=null&&e.Media.Id!=0)
                        e.Media = mediaItems.Response.FirstOrDefault(m => m.Id == e.Media.Id);
                });
            }

            return await GetCandidate(candidateIds, 0, int.MaxValue);
        }

        public async Task<GenericResponse<Candidate>> UpdateCandidate(Candidate request)
        {
            var candidates = await GetCandidate(new List<long>() { request.Id }, 0, 10);
            if(candidates==null||!candidates.Sucess || candidates.Response.Count==0)
            {
                return new GenericResponse<Candidate>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ErrorMessage = "Candidate not found" };
            }

            //Create user image
            if (request.Media!=null && request.Media.Id == 0 && !string.IsNullOrWhiteSpace(request.Media.Base64Image))
            {
                request.Media.StoreMediaType = StoreMediaType.PROFILE;
                var mediaResult = await _mediaService.CreateMedia(request.Media);
                request.Media = mediaResult.Response;
            }

            var requestedUser = candidates.Response.First();
            request.Name = string.IsNullOrWhiteSpace(request.Name) ? requestedUser.Name : request.Name;
            request.Lastname = string.IsNullOrWhiteSpace(request.Lastname) ? requestedUser.Lastname : request.Lastname;
            request.Phone = string.IsNullOrWhiteSpace(request.Phone) ? requestedUser.Phone : request.Phone;
            request.Email = string.IsNullOrWhiteSpace(request.Email) ? requestedUser.Email : request.Email;
            request.CURP = string.IsNullOrWhiteSpace(request.CURP) ? requestedUser.CURP : request.CURP;
            request.NSS = string.IsNullOrWhiteSpace(request.NSS) ? requestedUser.NSS : request.NSS;
            request.Address = string.IsNullOrWhiteSpace(request.Address) ? requestedUser.Address : request.Address;
            request.Position = string.IsNullOrWhiteSpace(request.Position) ? requestedUser.Position : request.Position;
            request.City = request.City==null||request.City.Id==0 ? requestedUser.City : request.City;
            request.State = request.State==null||request.State.Id==0 ? requestedUser.State : request.State;
            request.CandidateStatus = request.CandidateStatus== Common.Enums.CandidateStatus.NONE? requestedUser.CandidateStatus : request.CandidateStatus;
            request.Media = request.Media == null? requestedUser.Media : request.Media;
            var response = await _candidate.UpdateCandidate(request);
            var finalCandidate = await GetCandidate(new List<long>() { request.Id }, 0, 10);
            response.Response = finalCandidate.Response.FirstOrDefault();
            return response;
        }


        //Notes
        public async Task<GenericResponse<CandidateNote>> CreateNote(CandidateNote request)
        {
            return await _candidate.CreateNote(request);
        }

        public async Task<GenericResponse<CandidateNote>> UpdateNote(CandidateNote request)
        {
            return await _candidate.UpdateNote(request);
        }
        
        public async Task<GenericResponse<CandidateNote>> DeleteNote(long Id)
        {
            return await _candidate.DeleteNote(Id);
        }

        public async Task<GenericResponse<List<CandidateNote>>> GetNotes(List<long> Id, string key = "", long candidateId = 0, int currentPage = 0, int offset = 10)
        {
            return await _candidate.GetNotes(Id, key, candidateId, currentPage, offset);
        }
    }
}
