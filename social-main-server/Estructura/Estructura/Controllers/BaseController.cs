using Microsoft.AspNetCore.Mvc;
using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Models;
using Estructura.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public ITokenUtil tokenUtil;
        private AppUser _appUser;
        private static bool overrideErrorStatusCode = false; // If production, it need to be always true
        public AppUser AppUser
        {
            get { return _appUser ?? (_appUser = new AppUser(User)); }
            set { _appUser = value; }
        }

        public BaseController(ITokenUtil tokenUtil)
        {
            this.tokenUtil = tokenUtil;
        }

        public async Task<ActionResult<T>> GetActionResult<T>(Task<T> request, string url = "")
        {
            var apiResult = await request;
            return GetFormattedActionResult(apiResult, url);
        }

        public ActionResult<T> GetActionResult<T>(T request, string url = "")
        {
            return GetFormattedActionResult(request);
        }

        private ActionResult<T> GetFormattedActionResult<T>(T apiResult, string url = "")
        {
            var response = apiResult as ResponseBaseModel;
            if (response == null)
            {
                var errorItem = (T)Activator.CreateInstance(typeof(T));
                (errorItem as ResponseBaseModel).ErrorMessage = "Internal server error";
                (errorItem as ResponseBaseModel).StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(500, errorItem);
            }

            if (overrideErrorStatusCode && !string.IsNullOrEmpty(response.ErrorMessage))
            {
                response.ErrorMessage = "Unhandled";
            }

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    {
                        response.ErrorMessage = string.Empty;
                        return Ok(response);
                    }
                case System.Net.HttpStatusCode.NotFound:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Item not found" : response.ErrorMessage;
                        return NotFound(response);
                    }
                case System.Net.HttpStatusCode.Created:
                    {
                        response.ErrorMessage = string.Empty;
                        return Created(url, response);
                    }
                case System.Net.HttpStatusCode.BadRequest:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Invalid request" : response.ErrorMessage;
                        return BadRequest(response);
                    }
                case System.Net.HttpStatusCode.Unauthorized:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Unauthorized" : response.ErrorMessage;
                        return Unauthorized(response);
                    }
                case System.Net.HttpStatusCode.NotAcceptable:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Not acceptable" : response.ErrorMessage;
                        return Unauthorized(response);
                    }
                case System.Net.HttpStatusCode.Forbidden:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Forbidden" : response.ErrorMessage;
                        return StatusCode(403, response);
                    }
                case System.Net.HttpStatusCode.ServiceUnavailable:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Service not available" : response.ErrorMessage;
                        return StatusCode(503, response);
                    }
                default:
                    {
                        response.ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? "Internal server error" : response.ErrorMessage;
                        return StatusCode(500, response);
                    }
            }
        }
    }
}
