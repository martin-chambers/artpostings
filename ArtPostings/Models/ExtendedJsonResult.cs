using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtPostings.Models
{
    /// <summary>
    /// Bundles HttpResponse-useable status code with JsonResult
    /// (credit to NaveenBhat: http://stackoverflow.com/questions/15208699/setting-up-net-mvc-http-error-codes-for-ajax-calls )
    /// </summary>
    public class ExtendedJsonResult : JsonResult
    {
        public ExtendedJsonResult(ChangeResult data)
        {
            base.Data = data;
            Success = data.Success;
            Message = data.Message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ExtendedJsonResult(ChangeResult data, int statuscode) : this(data)
        {
            StatusCode = statuscode;
        }

        public int StatusCode { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = this.StatusCode;
            base.ExecuteResult(context);
        }
    }
}