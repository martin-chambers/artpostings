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
        public int StatusCode
        {
            get
            {
                if (Success)
                {
                    return 200;
                }
                else
                {
                    return 500;
                }
            }            
        }
        public ChangeResult(bool _success, string _message)
        {
            Success = _success;
            Message = _message;
            
        }
        public ChangeResult()
        {
            Success = false;
            Message = "Unknown error";
        }
    }
}