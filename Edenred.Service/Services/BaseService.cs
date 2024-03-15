using Edenred.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Edenred.Service.Services
{
    public class BaseService
    {
        public Response SuccessResponse()
        {
            return new Response
            {
                Result = "Success",
                StatusCode = HttpStatusCode.OK,
                ResultCode = "200",
                ResultText = "Successful",
                AdditionalData = null
            };
        }

        public Response SuccessResponse(int id = 0, string resultText = "Successful", Dictionary<string, string> additionalData = null)
        {
            return new Response
            {
                Id = id,
                Result = "Success",
                StatusCode = HttpStatusCode.OK,
                ResultCode = "200",
                ResultText = resultText,
                AdditionalData = additionalData
            };
        }

        public Response FaultResponse(string resultText = "Failed", List<Error> errorList = null, Dictionary<string, string> additionalData = null, string ResultCode = "192")
        {
            return new Response
            {
                Result = "Failed",
                Errors = errorList,
                AdditionalData = additionalData,
                StatusCode = HttpStatusCode.BadRequest,
                ResultCode = ResultCode,
                ResultText = resultText
            };
        }
    }
}
