using System.Collections.Generic;

namespace XSample.Common
{
    public class CommonApiResponse
    {
        public CommonApiResponse()
        {
        }

        public CommonApiResponse(object data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public CommonApiResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }
}