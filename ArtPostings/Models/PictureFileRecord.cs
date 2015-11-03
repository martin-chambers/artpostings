using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ArtPostings.Models
{
    public class PictureFileRecord
    {
        public static string NullStringFormat = "D5";
        public static int NULL_ORDER = int.MaxValue;
        public enum StatusType { All = 0, ForSale = 1, Archived = 2, NotDisplayed = 3 };
        public StatusType Status { get; set;}
        public static StatusType GetStatusType(string statusString)
        {
            if (PictureFileRecord.StatusList[statusString] == null)
            {
                throw new ArgumentException("Value supplied for picture file record filter does not match a configured source value");
            }
            List<string> keys = StatusList.AllKeys.ToList();
            StatusType type = (StatusType)keys.IndexOf(statusString);
            return type;
        }
        public static string GetStatusString(StatusType type)
        {
            return StatusList.Keys[(int)type];            
        }

        public static NameValueCollection StatusList
        {
            get
            {
                NameValueCollection statuses = (NameValueCollection)ConfigurationManager.GetSection("PictureFileStatuses");
                return statuses;
            }            
        }
        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        public string FileName { get; set; }
        public string FilePath { get; set; }
        private int order;
        public string Order
        {
            get
            {
                if( order == int.MaxValue)
                {
                    return "No order";
                }
                else
                {
                    return order.ToString(NullStringFormat);
                }
            }
            set
            {
                try
                {
                    order = Convert.ToInt32(value);
                }
                catch(ArgumentException argEx)
                {
                    throw new ArgumentException("Invalid value passed to picture file table ordering function", argEx);
                }
            }
        }
        public string Header { get; set; }
        public PictureFileRecord(string _filePath)
        {
            FileName = Utility.GetFilenameFromFilepath(_filePath);
            FilePath = HttpUtility.UrlPathEncode(Path.Combine(webSafePictureFolder, FileName));
            Status = StatusType.NotDisplayed;
        }
        public PictureFileRecord() { }

    }
}