using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Services
{
    public class StudyEconomicSituationService : IStudyEconomicSituationService
    {
        private readonly HttpClient _client;
        public StudyEconomicSituationService(HttpClient _client)
        {
            this._client=_client;
        }



        //Study Economic Situation
        public async Task<GenericResponse<StudyEconomicSituation>> CreateStudyEconomicSituation(StudyEconomicSituation request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyEconomicSituation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyEconomicSituation>(response);
        }

        public async Task<GenericResponse<StudyEconomicSituation>> DeleteStudyEconomicSituation(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyEconomicSituation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyEconomicSituation>(response);
        }

        public async Task<GenericResponse<List<StudyEconomicSituation>>> GetStudyEconomicSituation(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyEconomicSituation>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyEconomicSituation>>(response);
        }

        public async Task<GenericResponse<StudyEconomicSituation>> UpdateStudyEconomicSituation(StudyEconomicSituation request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyEconomicSituation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyEconomicSituation>(response);
        }



        // Incoming
        public async Task<GenericResponse<List<Incoming>>> CreateIncoming(List<Incoming> request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation/Incoming", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Incoming>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Incoming>>(response);
        }

        public async Task<GenericResponse<Incoming>> DeleteIncoming(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation/Incoming?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Incoming>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Incoming>(response);
        }

        public async Task<GenericResponse<List<Incoming>>> GetIncoming(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation/Incoming?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Incoming>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Incoming>>(response);
        }

        public async Task<GenericResponse<Incoming>> UpdateIncoming(Incoming request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation/Incoming", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Incoming>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Incoming>(response);
        }



        // Additional Incoming
        public async Task<GenericResponse<List<AdditionalIncoming>>> CreateAdditionalIncoming(List<AdditionalIncoming> request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation/AdditionalIncoming", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<AdditionalIncoming>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<AdditionalIncoming>>(response);
        }

        public async Task<GenericResponse<AdditionalIncoming>> DeleteAdditionalIncoming(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation/AdditionalIncoming?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<AdditionalIncoming>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<AdditionalIncoming>(response);
        }

        public async Task<GenericResponse<List<AdditionalIncoming>>> GetAdditionalIncoming(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation/AdditionalIncoming?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<AdditionalIncoming>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<AdditionalIncoming>>(response);
        }

        public async Task<GenericResponse<AdditionalIncoming>> UpdateAdditionalIncoming(AdditionalIncoming request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation/AdditionalIncoming", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<AdditionalIncoming>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<AdditionalIncoming>(response);
        }



        // Credit
        public async Task<GenericResponse<List<Credit>>> CreateCredit(List<Credit> request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation/Credit", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Credit>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Credit>>(response);
        }

        public async Task<GenericResponse<Credit>> DeleteCredit(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation/Credit?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Credit>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Credit>(response);
        }

        public async Task<GenericResponse<List<Credit>>> GetCredit(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation/Credit?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Credit>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Credit>>(response);
        }

        public async Task<GenericResponse<Credit>> UpdateCredit(Credit request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation/Credit", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Credit>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Credit>(response);
        }



        // Estate
        public async Task<GenericResponse<List<Estate>>> CreateEstate(List<Estate> request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation/Estate", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Estate>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Estate>>(response);
        }

        public async Task<GenericResponse<Estate>> DeleteEstate(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation/Estate?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Estate>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Estate>(response);
        }

        public async Task<GenericResponse<List<Estate>>> GetEstate(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation/Estate?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Estate>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Estate>>(response);
        }

        public async Task<GenericResponse<Estate>> UpdateEstate(Estate request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation/Estate", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Estate>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Estate>(response);
        }



        // Vehicle
        public async Task<GenericResponse<List<Vehicle>>> CreateVehicle(List<Vehicle> request)
        {
            var response = await _client.PostAsync("StudyEconomicSituation/Vehicle", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Vehicle>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Vehicle>>(response);
        }

        public async Task<GenericResponse<Vehicle>> DeleteVehicle(long id)
        {
            var response = await _client.DeleteAsync($@"StudyEconomicSituation/Vehicle?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Vehicle>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Vehicle>(response);
        }

        public async Task<GenericResponse<List<Vehicle>>> GetVehicle(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"StudyEconomicSituation/Vehicle?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Vehicle>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Vehicle>>(response);
        }

        public async Task<GenericResponse<Vehicle>> UpdateVehicle(Vehicle request)
        {
            var response = await _client.PutAsync("StudyEconomicSituation/Vehicle", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Vehicle>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Vehicle>(response);
        }

    }
}
