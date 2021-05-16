using System.Collections.Generic;

namespace Application.Orchestrator
{
    public class Response
    {
        public Response()
        {
        }

        public Response(object data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }
}