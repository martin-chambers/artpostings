using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;



namespace ArtPostings.Models
{
    public static class Utility
    {
        /// <summary>
        /// removes html tags, double spaces and nbsps
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NormaliseString(string value)
        {
            string rv = "";
            // tags - get rid
            if (value != null)
            {
                // take out html tag markers
                string step1 = Regex.Replace(value, @"<[^>]+>", "").Trim();
                // take out double spaces, %20s and nspbs, replace with 1 space
                string step2 = step1.WithSoftSpace();
                // replaces JS escaped single quote with normal single quote
                string step3 = Regex.Replace(step2, @"\'", "'");
                // single quotes - replace with double
                //var step3 = Regex.Replace(step2, @"'", "''");
                rv = step3;
            }
            return rv;
        }
        public static string GetFilenameFromFilepath(string filepath)
        {
            int posLastSlash = Math.Max(filepath.LastIndexOf("\\"), filepath.LastIndexOf("/"));
            string fileName = filepath.Substring(posLastSlash + 1, filepath.Length - (posLastSlash + 1));
            return fileName;
        }
    }
}