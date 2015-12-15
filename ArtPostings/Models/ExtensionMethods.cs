using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ArtPostings.Models
{
    /// <summary>
    /// Provides string extension methods to help deal with formatting issues
    /// There are three formats, apart from vanilla text, in use in the application:
    /// 1. Html strings where it is necessary to replace soft spaces with &nbsp (non-breaking spaces). This is used for the title field
    ///    in the ItemPosting class. Failure to use it caused the painting title to be broken at the first space
    /// 
    /// 2. Url-encoded strings where the space character is replaced with %20. The HttpUtility.UrlPathEcode is used in this application
    ///    in the ItemPosting class.   
    /// 
    ///    The official advice is not to use UrlPathEncode because it does not escape all alphanumerics. Trouble
    ///    is - the src attribute of the img tag requires a vanilla string with single quotes and space chararcters where
    ///    they appear in the filename. Even if you use UrlDecode in the view, UrlEncode replaces space chararcters with 
    ///    '+' symbols, which seem to be converted back to breaking space characters by UrlDecode
    /// 
    /// 3. JavaScript-friendly strings where the single quote is replaced with \'. This is used in the PictureFileRecord class and the     /// 
    ///    value is made available to the startdrag and filedelete functions in Admin/_PictureList.cshtml    /// 
    /// </summary>
    public static class ExtensionMethods
    {
        
        public static string WithNBSP(this string value)
        {
            return value.Replace(" ", "&nbsp");
        }

        public static string JSEscSingleQuote(this string value)
        {
            string s = value.Replace("'", "\\'");
            return s;
        }
        public static string WithSoftSpace(this string value)
        {
            // takes out double spaces, %20s and nspbs, replaces with 1 space
            return Regex.Replace(value, @"\s{2,}|%20|&nbsp;|&nbsp", " ");
        }
        /// <summary>
        /// Converts a string to plain readable format. Removes relevant escape characters and url encoding
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Normalise(this string value)
        {
            // takes out html tag markers
            // takes out double spaces, %20s and nspbs, replaces with 1 space
            // replaces JS escaped single quote with normal single quote
            return Utility.NormaliseString(value);
        }

        public static string CustomUrlEncode(this string value)
        {
            return value.Replace(" ", "%20");
        }

    }
}