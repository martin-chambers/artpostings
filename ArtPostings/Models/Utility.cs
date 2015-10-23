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
        public static string PrepForSql(string value)
        {
            // tags - get rid
            var step1 = Regex.Replace(value, @"<[^>]+>", "").Trim();
            // double spaces, %20s and nspbs, replace with 1 space
            var step2 = Regex.Replace(step1, @"\s{2,}|%20|&nbsp;|&nbsp", " ");
            // single quotes - replace with double
            var step3 = Regex.Replace(step2, @"'", "''");
            return step3;
        }
    }
}