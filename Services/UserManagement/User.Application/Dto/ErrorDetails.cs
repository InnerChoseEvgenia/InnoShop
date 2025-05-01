using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace User.Application.Dto
{
    public class ErrorDetails
    {
        public ErrorDetails(int statusCode, string message = null, string details = null)
        {
            StatusCode=statusCode;
            Message=message;
            Details=details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; } 
        public string Details { get; set; }
        //public int StatusCode { get; set; }
        //public string? Message { get; set; }
        //public override string ToString() => JsonSerializer.Serialize(this);
    }

}
