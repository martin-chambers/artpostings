using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtPostings.Models
{
    public class ChangeResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ChangeResult(bool _success, string _message, int _statuscode)
        {
            Success = _success;
            Message = _message;
            StatusCode = _statuscode;
            
        }
        /// <summary>
        /// Default pessimistic constructor
        /// </summary>
        public ChangeResult()
        {
            Success = false;
            Message = "Unknown error";
            StatusCode = 500;
        }
    }
}