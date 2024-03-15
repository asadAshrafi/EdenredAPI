using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Edenred.Model
{
    public class Response
    {
        public string Result { get; set; }
        public string ResultText { get; set; }
        public string ResultCode { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int Id { get; set; }
        public List<Error> Errors { get; set; }
    }
}
