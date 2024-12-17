using Dapper;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{
    public class StudyGeneralInformationRepository:BaseRepository, IStudyGeneralInformationRepository
    {
        public StudyGeneralInformationRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }


        public async Task<GenericResponse<StudyGeneralInformation>> CreateStudyGeneralInformation(StudyGeneralInformation request)
        {
			try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyGeneralInformation>($@"
                        INSERT INTO StudyGeneralInformation (
							StudyId,
							Name,
							Email,
							TimeOnComany,
							EmployeeNumber,
							BornCityId,
							BornStateId,
							CountryName,
							BornDate,
							Age,
							MaritalStatusId,
							TaxRegime,
							Address,
							PostalCode,
							Suburb,
							HomePhone,
							CityId,
							StateId,
							MobilPhone,

							IDCardOriginal,
							IDCardCopy,
							IDCardRecord,
							IDCardExpeditionPlace,
							IDCardObservations,

							AddressProofOriginal,
							AddressProofCopy,
							AddressProofRecord,
							AddressProofExpeditionPlace,
							AddressProofObservations,

							BirthCertificateOriginal,
							BirthCertificateCopy,
							BirthCertificateRecord,
							BirthCertificateExpeditionPlace,
							BirthCertificateObservations,

							CURPOriginal,
							CURPCopy,
							CURPRecord,
							CURPExpeditionPlace,
							CURPObservations,

							StudyProofOriginal,
							StudyProofCopy,
							StudyProofRecord,
							StudyProofExpeditionPlace,
							StudyProofObservations,

							SocialSecurityProofOriginal,
							SocialSecurityProofCopy,
							SocialSecurityProofRecord,
							SocialSecurityProofExpeditionPlace,
							SocialSecurityProofObservations,

							MilitaryLetterOriginal,
							MilitaryLetterCopy,
							MilitaryLetterRecord,
							MilitaryLetterExpeditionPlace,
							MilitaryLetterObservations,

							RFCOriginal,
							RFCCopy,
							RFCRecord,
							RFCExpeditionPlace,
							RFCObservations,

							CriminalRecordLetterOriginal,
							CriminalRecordLetterCopy,
							CriminalRecordLetterRecord,
							CriminalRecordLetterExpeditionPlace,
							CriminalRecordLetterObservations,

							INEFrontMediaId,
							INEBackMediaId,
							AddressProofMediaId,
							BornCertificateMediaId,
							CURPMediaId,
							StudiesProofMediaId,
							SocialSecurityProofMediaId,
							MilitaryLetterMediaId,
							RFCMediaId,
							CriminalRecordMediaId,

							PresentedIdentificationMediaId,
							VerificationMediaId)
						OUTPUT INSERTED.*
						VALUES (
							{request.StudyId},
							'{request.Name}',
							'{request.Email}',
							'{request.TimeOnComany}',
							'{request.EmployeeNumber}',
							{request.BornCity.Id},	
							{request.BornState.Id},
							'{request.CountryName}',
							'{request.BornDate}',
							'{request.Age}',
							{(int)request.MaritalStatus},
							'{request.TaxRegime}',
							'{request.Address}',
							'{request.PostalCode}',
							'{request.Suburb}',
							'{request.HomePhone}',
							{request.City.Id},
							{request.State.Id},
							'{request.MobilPhone}',

							{(request.IDCardOriginal ? 1 : 0)},
							{(request.IDCardCopy ? 1 : 0)},
							'{request.IDCardRecord}',
							'{request.IDCardExpeditionPlace}',
							'{request.IDCardObservations}',

							{(request.AddressProofOriginal ? 1 : 0)},
							{(request.AddressProofCopy ? 1 : 0)},
							'{request.AddressProofRecord}',
							'{request.AddressProofExpeditionPlace}',
							'{request.AddressProofObservations}',

							{(request.BirthCertificateOriginal ? 1 : 0)},
							{(request.BirthCertificateCopy ? 1 : 0)},
							'{request.BirthCertificateRecord}',
							'{request.BirthCertificateExpeditionPlace}',
							'{request.BirthCertificateObservations}',

							{(request.CURPOriginal ? 1 : 0)},
							{(request.CURPCopy ? 1 : 0)},
							'{request.CURPRecord}',
							'{request.CURPExpeditionPlace}',
							'{request.CURPObservations}',

							{(request.StudyProofOriginal ? 1 : 0)},
							{(request.StudyProofCopy ? 1 : 0)},
							'{request.StudyProofRecord}',
							'{request.StudyProofExpeditionPlace}',
							'{request.StudyProofObservations}',

							{(request.SocialSecurityProofOriginal ? 1 : 0)},
							{(request.SocialSecurityProofCopy ? 1 : 0)},
							'{request.SocialSecurityProofRecord}',
							'{request.SocialSecurityProofExpeditionPlace}',
							'{request.SocialSecurityProofObservations}',

							{(request.MilitaryLetterOriginal ? 1 : 0)},
							{(request.MilitaryLetterCopy ? 1 : 0)},
							'{request.MilitaryLetterRecord}',
							'{request.MilitaryLetterExpeditionPlace}',
							'{request.MilitaryLetterObservations}',

							{(request.RFCOriginal ? 1 : 0)},
							{(request.RFCCopy ? 1 : 0)},
							'{request.RFCRecord}',
							'{request.RFCExpeditionPlace}',
							'{request.RFCObservations}',

							{(request.CriminalRecordLetterOriginal ? 1 : 0)},
							{(request.CriminalRecordLetterCopy ? 1 : 0)},
							'{request.CriminalRecordLetterRecord}',
							'{request.CriminalRecordLetterExpeditionPlace}',
							'{request.CriminalRecordLetterObservations}',

							{(request.INEFrontMedia == null || request.INEFrontMedia.Id == 0 ? "null" : request.INEFrontMedia.Id.ToString())},
							{(request.INEBackMedia == null || request.INEBackMedia.Id == 0 ? "null" : request.INEBackMedia.Id.ToString())},
							{(request.AddressProofMedia == null || request.AddressProofMedia.Id == 0 ? "null" : request.AddressProofMedia.Id.ToString())},
							{(request.BornCertificateMedia == null || request.BornCertificateMedia.Id == 0 ? "null" : request.BornCertificateMedia.Id.ToString())},
							{(request.CURPMedia == null || request.CURPMedia.Id == 0 ? "null" : request.CURPMedia.Id.ToString())},
							{(request.StudiesProofMedia == null || request.StudiesProofMedia.Id == 0 ? "null" : request.StudiesProofMedia.Id.ToString())},
							{(request.SocialSecurityProofMedia == null || request.SocialSecurityProofMedia.Id == 0 ? "null" : request.SocialSecurityProofMedia.Id.ToString())},
							{(request.MilitaryLetterMedia == null || request.MilitaryLetterMedia.Id == 0 ? "null" : request.MilitaryLetterMedia.Id.ToString())},
							{(request.RFCMedia == null || request.RFCMedia.Id == 0 ? "null" : request.RFCMedia.Id.ToString())},
							{(request.CriminalRecordMedia == null || request.CriminalRecordMedia.Id == 0 ? "null" : request.CriminalRecordMedia.Id.ToString())},
							
							{(request.PresentedIdentificationMedia == null || request.PresentedIdentificationMedia.Id == 0 ? "null" : request.PresentedIdentificationMedia.Id.ToString())},
							{(request.VerificationMedia == null || request.VerificationMedia.Id == 0 ? "null" : request.VerificationMedia.Id.ToString())}
                    )");

                    return new GenericResponse<StudyGeneralInformation>()
                    {
                        Sucess = true,
                        Response = apiResponse,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<StudyGeneralInformation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

		public async Task<GenericResponse<List<StudyGeneralInformation>>> GetStudyGeneralInformation(List<long> id, bool byStudy)
		{
			try
			{
				string ids = string.Empty;
				id.ForEach(e =>
				{
					if (!string.IsNullOrWhiteSpace(ids)) ids+=", ";
					ids+=e;
				});
				using (var conn = Connection)
				{
					string query = $@"
                        SELECT
							sgi.*,
							sgi.INEFrontMediaId as ifmi,
							sgi.INEBackMediaId as ibmi,
							sgi.AddressProofMediaId as apmi,
							sgi.BornCertificateMediaId as bcmi,
							sgi.CURPMediaId as cmi,
							sgi.StudiesProofMediaId as spmi,
							sgi.SocialSecurityProofMediaId as sspmi,
							sgi.PresentedIdentificationMediaId as pimi,
							sgi.VerificationMediaId as vmi,
							sgi.MilitaryLetterMediaId as mlmi,
							sgi.RFCMediaId as rfcmi,
							sgi.CriminalRecordMediaId as crmi,
							sgi.MaritalStatusId AS MaritalStatusId,
							bc1.*,
							bs1.*,
							c1.*,
							s1.*,
							(
								SELECT
									*
								FROM 
									RecommendationCard rc
								WHERE 
									rc.DeletedAt IS NULL
								AND 
									rc.StudyGeneralInformationId = sgi.Id  FOR JSON PATH
							) AS recommendationCards
						FROM StudyGeneralInformation sgi
						LEFT JOIN Cities bc1
						ON 
							bc1.Id = sgi.BornCityId
						LEFT JOIN States bs1
						ON 
							bs1.Id = sgi.BornStateId
						LEFT JOIN Cities c1
						ON 
							c1.Id = sgi.CityId
						LEFT JOIN States s1
						ON 
							s1.Id = sgi.StateId
						WHERE 
							sgi.DeletedAt IS NULL
						AND
							{(byStudy ? $@"sgi.StudyId IN ({ids})" : $@"sgi.Id IN ({ids})")}
                    ";

					conn.Open();
					var apiResponse = await conn.QueryAsync<StudyGeneralInformation>(query,
                        new[]
                        {
							typeof(StudyGeneralInformation), // 0
							typeof(long?),
							typeof(long?),//2
							typeof(long?),
							typeof(long?),//4
							typeof(long?),
							typeof(long?),//6
							typeof(long?),
							typeof(long?),//8
							typeof(long?),
							typeof(long?),// 10
							typeof(long?),
							typeof(long?),//12
							typeof(int?),
							typeof(City),//14
							typeof(State),
							typeof(City),//16
							typeof(State),
							typeof(string), //18

                        }, (objects) =>
                        {
							var current = objects[0] as StudyGeneralInformation;


							current.INEFrontMedia = objects[1]==null ? new Media() : new Media() { Id = long.Parse(objects[1].ToString()) };
							current.INEBackMedia = objects[2]==null ? new Media() : new Media() { Id = long.Parse(objects[2].ToString()) };
							current.AddressProofMedia = objects[3]==null ? new Media() : new Media() { Id = long.Parse(objects[3].ToString()) };
							current.BornCertificateMedia = objects[4]==null ? new Media() : new Media() { Id = long.Parse(objects[4].ToString()) };
							current.CURPMedia = objects[5]==null ? new Media() : new Media() { Id = long.Parse(objects[5].ToString()) };
							current.StudiesProofMedia = objects[6]==null ? new Media() : new Media() { Id = long.Parse(objects[6].ToString()) };
							current.SocialSecurityProofMedia = objects[7]==null ? new Media() : new Media() { Id = long.Parse(objects[7].ToString()) };
							current.PresentedIdentificationMedia = objects[8]==null ? new Media() : new Media() { Id = long.Parse(objects[8].ToString()) };
							current.VerificationMedia = objects[9]==null ? new Media() : new Media() { Id = long.Parse(objects[9].ToString()) };
							current.MilitaryLetterMedia = objects[10]==null ? new Media() : new Media() { Id = long.Parse(objects[10].ToString()) };
							current.RFCMedia = objects[11]==null ? new Media() : new Media() { Id = long.Parse(objects[11].ToString()) };
							current.CriminalRecordMedia = objects[12]==null ? new Media() : new Media() { Id = long.Parse(objects[12].ToString()) };


							current.MaritalStatus = (Common.Enums.MaritalStatus)objects[13];

							current.BornCity = objects[14] as City;
							current.BornState = objects[15] as State;

							current.City = objects[16] as City;
							current.State = objects[17] as State;

							if (!string.IsNullOrWhiteSpace(objects[18] as string))
								current.RecommendationCards = JsonConvert.DeserializeObject<List<RecommendationCard>>(objects[18] as string);

							return current;
                        }, splitOn: "Id, ifmi, ibmi, apmi, bcmi, cmi, spmi, sspmi, pimi, vmi, mlmi, rfcmi, crmi, MaritalStatusId, Id, Id, Id, Id, recommendationCards");

					return new GenericResponse<List<StudyGeneralInformation>>()
					{
						Sucess = true,
						Response = apiResponse.ToList(),
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<List<StudyGeneralInformation>>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}

		public async Task<GenericResponse<StudyGeneralInformation>> UpdateStudyGeneralInformation(StudyGeneralInformation request)
        {
			try
			{
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QuerySingleAsync<StudyGeneralInformation>($@"
                        UPDATE StudyGeneralInformation
						SET
							Name = '{request.Name}',
							Email = '{request.Email}',
							TimeOnComany = '{request.TimeOnComany}',
							EmployeeNumber = '{request.EmployeeNumber}',
							BornCityId = {request.BornCity.Id},	
							BornStateId = {request.BornState.Id},
							CountryName = '{request.CountryName}',
							BornDate = '{request.BornDate}',
							Age = '{request.Age}',
							MaritalStatusId = {(int)request.MaritalStatus},
							TaxRegime = '{request.TaxRegime}',
							Address = '{request.Address}',
							PostalCode = '{request.PostalCode}',
							Suburb = '{request.Suburb}',
							HomePhone = '{request.HomePhone}',
							CityId = {request.City.Id},
							StateId = {request.State.Id},
							MobilPhone = '{request.MobilPhone}',

							IDCardOriginal = {(request.IDCardOriginal ? 1 : 0)},
							IDCardCopy = {(request.IDCardCopy ? 1 : 0)},
							IDCardRecord = '{request.IDCardRecord}',
							IDCardExpeditionPlace = '{request.IDCardExpeditionPlace}',
							IDCardObservations = '{request.IDCardObservations}',

							AddressProofOriginal = {(request.AddressProofOriginal ? 1 : 0)},
							AddressProofCopy = {(request.AddressProofCopy ? 1 : 0)},
							AddressProofRecord = '{request.AddressProofRecord}',
							AddressProofExpeditionPlace = '{request.AddressProofExpeditionPlace}',
							AddressProofObservations = '{request.AddressProofObservations}',

							BirthCertificateOriginal = {(request.BirthCertificateOriginal ? 1 : 0)},
							BirthCertificateCopy = {(request.BirthCertificateCopy ? 1 : 0)},
							BirthCertificateRecord = '{request.BirthCertificateRecord}',
							BirthCertificateExpeditionPlace = '{request.BirthCertificateExpeditionPlace}',
							BirthCertificateObservations = '{request.BirthCertificateObservations}',

							CURPOriginal = {(request.CURPOriginal ? 1 : 0)},
							CURPCopy = {(request.CURPCopy ? 1 : 0)},
							CURPRecord = '{request.CURPRecord}',
							CURPExpeditionPlace = '{request.CURPExpeditionPlace}',
							CURPObservations = '{request.CURPObservations}',

							StudyProofOriginal = {(request.StudyProofOriginal ? 1 : 0)},
							StudyProofCopy = {(request.StudyProofCopy ? 1 : 0)},
							StudyProofRecord = '{request.StudyProofRecord}',
							StudyProofExpeditionPlace = '{request.StudyProofExpeditionPlace}',
							StudyProofObservations = '{request.StudyProofObservations}',

							SocialSecurityProofOriginal = {(request.SocialSecurityProofOriginal ? 1 : 0)},
							SocialSecurityProofCopy = {(request.SocialSecurityProofCopy ? 1 : 0)},
							SocialSecurityProofRecord = '{request.SocialSecurityProofRecord}',
							SocialSecurityProofExpeditionPlace = '{request.SocialSecurityProofExpeditionPlace}',
							SocialSecurityProofObservations = '{request.SocialSecurityProofObservations}',

							MilitaryLetterOriginal = {(request.MilitaryLetterOriginal ? 1 : 0)},
							MilitaryLetterCopy = {(request.MilitaryLetterCopy ? 1 : 0)},
							MilitaryLetterRecord = '{request.MilitaryLetterRecord}',
							MilitaryLetterExpeditionPlace = '{request.MilitaryLetterExpeditionPlace}',
							MilitaryLetterObservations = '{request.MilitaryLetterObservations}',

							RFCOriginal = {(request.RFCOriginal ? 1 : 0)},
							RFCCopy = {(request.RFCCopy ? 1 : 0)},
							RFCRecord = '{request.RFCRecord}',
							RFCExpeditionPlace = '{request.RFCExpeditionPlace}',
							RFCObservations = '{request.RFCObservations}',

							CriminalRecordLetterOriginal = {(request.CriminalRecordLetterOriginal ? 1 : 0)},
							CriminalRecordLetterCopy = {(request.CriminalRecordLetterCopy ? 1 : 0)},
							CriminalRecordLetterRecord = '{request.CriminalRecordLetterRecord}',
							CriminalRecordLetterExpeditionPlace = '{request.CriminalRecordLetterExpeditionPlace}',
							CriminalRecordLetterObservations = '{request.CriminalRecordLetterObservations}',

							INEFrontMediaId = {(request.INEFrontMedia == null || request.INEFrontMedia.Id == 0 ? "null" : request.INEFrontMedia.Id.ToString())},
							INEBackMediaId = {(request.INEBackMedia == null || request.INEBackMedia.Id == 0 ? "null" : request.INEBackMedia.Id.ToString())},
							AddressProofMediaId = {(request.AddressProofMedia == null || request.AddressProofMedia.Id == 0 ? "null" : request.AddressProofMedia.Id.ToString())},
							BornCertificateMediaId = {(request.BornCertificateMedia == null || request.BornCertificateMedia.Id == 0 ? "null" : request.BornCertificateMedia.Id.ToString())},
							CURPMediaId = {(request.CURPMedia == null || request.CURPMedia.Id == 0 ? "null" : request.CURPMedia.Id.ToString())},
							StudiesProofMediaId = {(request.StudiesProofMedia == null || request.StudiesProofMedia.Id == 0 ? "null" : request.StudiesProofMedia.Id.ToString())},
							SocialSecurityProofMediaId = {(request.SocialSecurityProofMedia == null || request.SocialSecurityProofMedia.Id == 0 ? "null" : request.SocialSecurityProofMedia.Id.ToString())},
							MilitaryLetterMediaId = {(request.MilitaryLetterMedia == null || request.MilitaryLetterMedia.Id == 0 ? "null" : request.MilitaryLetterMedia.Id.ToString())},
							RFCMediaId = {(request.RFCMedia == null || request.RFCMedia.Id == 0 ? "null" : request.RFCMedia.Id.ToString())},
							CriminalRecordMediaId = {(request.CriminalRecordMedia == null || request.CriminalRecordMedia.Id == 0 ? "null" : request.CriminalRecordMedia.Id.ToString())},

							PresentedIdentificationMediaId = {(request.PresentedIdentificationMedia == null || request.PresentedIdentificationMedia.Id == 0 ? "null" : request.PresentedIdentificationMedia.Id.ToString())},
							VerificationMediaId = {(request.VerificationMedia == null || request.VerificationMedia.Id == 0 ? "null" : request.VerificationMedia.Id.ToString())},
							UpdatedAt = '{DateTime.UtcNow}'
						OUTPUT INSERTED.*
						WHERE Id = {request.Id}
                    ");

					return new GenericResponse<StudyGeneralInformation>()
					{
						Sucess = true,
						Response = apiResponse,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<StudyGeneralInformation>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}

		public async Task<GenericResponse<StudyGeneralInformation>> DeleteStudyGeneralInformation(long id)
		{
			try
			{
				string query = $@"
                        UPDATE StudyGeneralInformation
						SET
							DeletedAt = '{DateTime.UtcNow}'
						OUTPUT INSERTED.*
						WHERE
							Id = {id}";
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QuerySingleAsync<StudyGeneralInformation>(query);

					return new GenericResponse<StudyGeneralInformation>()
					{
						Sucess = true,
						Response = apiResponse,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<StudyGeneralInformation>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}




        //Recommendation Card
        public async Task<GenericResponse<List<RecommendationCard>>> CreateRecommendationCard(List<RecommendationCard> request)
		{
			string items = string.Empty;
			request.ForEach(e =>
			{
				if (!string.IsNullOrWhiteSpace(items)) items+=", ";
				items+=$@"(
							{e.StudyGeneralInformationId},
							'{e.IssueCompany}',
							'{e.WorkingFrom}',
							'{e.WorkingTo}',
							'{e.TimeWorking}'
						)";
			});

			try
			{
				string query = $@"
                        INSERT INTO RecommendationCard (
							StudyGeneralInformationId,
							IssueCompany,
							WorkingFrom,
							WorkingTo,
							TimeWorking)
						OUTPUT INSERTED.*
						VALUES {items}
                    ";
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QueryAsync<RecommendationCard>(query);

					return new GenericResponse<List<RecommendationCard>>()
					{
						Sucess = true,
						Response = apiResponse.ToList(),
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<List<RecommendationCard>>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}

        public async Task<GenericResponse<RecommendationCard>> UpdateRecommendationCard(RecommendationCard request)
		{
			try
			{
				string query = $@"
                        UPDATE RecommendationCard
						SET
							IssueCompany = '{request.IssueCompany}',
							WorkingFrom = '{request.WorkingFrom}',
							WorkingTo = '{request.WorkingTo}',
							TimeWorking = '{request.TimeWorking}',
							UpdatedAt = '{DateTime.UtcNow}'
						OUTPUT INSERTED.*
						WHERE
							Id = {request.Id}";
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QuerySingleAsync<RecommendationCard>(query);

					return new GenericResponse<RecommendationCard>()
					{
						Sucess = true,
						Response = apiResponse,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<RecommendationCard>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}

		public async Task<GenericResponse<List<RecommendationCard>>> GetRecommendationCard(List<long> id)
		{
			try
			{
				string ids = string.Empty;
				id.ForEach(e =>
				{
					if (!string.IsNullOrWhiteSpace(ids)) ids += ", ";
					ids+=e;
				});

				string query = $@"
                        SELECT* FROM
							RecommendationCard
						WHERE
							DeletedAt IS NULL
							AND Id IN ({ids})";
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QueryAsync<RecommendationCard>(query);

					return new GenericResponse<List<RecommendationCard>>()
					{
						Sucess = true,
						Response = apiResponse.ToList(),
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<List<RecommendationCard>>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}

		public async Task<GenericResponse<RecommendationCard>> DeleteRecommendationCard(long id)
		{
			try
			{
				string query = $@"
                        UPDATE RecommendationCard
						SET
							DeletedAt = '{DateTime.UtcNow}'
						OUTPUT INSERTED.*
						WHERE
							Id = {id}";
				using (var conn = Connection)
				{
					conn.Open();
					var apiResponse = await conn.QuerySingleAsync<RecommendationCard>(query);

					return new GenericResponse<RecommendationCard>()
					{
						Sucess = true,
						Response = apiResponse,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.WriteLine(exc.Message);
				return new GenericResponse<RecommendationCard>()
				{
					StatusCode = System.Net.HttpStatusCode.InternalServerError,
					ErrorMessage = exc.Message
				};
			}
		}
    }
}
