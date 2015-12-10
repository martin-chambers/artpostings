using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtPostings.Models
{
    public class PictureFileRecordsViewModel
    {
        public List<PictureFileRecord> PictureFileRecords { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public string Status { get; set; }
        public PictureFileRecordsViewModel() { }
        public PictureFileRecordsViewModel(List<PictureFileRecord> _pictureFileRecords,  NameValueCollection _statuscollection, string statusKey)
        {
            Status = statusKey;
            List<SelectListItem> statuslist = new List<SelectListItem>();
            PictureFileRecords = _pictureFileRecords;
            foreach(string key in _statuscollection)
            {
                SelectListItem item = new SelectListItem();
                item.Text = _statuscollection[key];
                item.Value = key;
                if(key == statusKey)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }                
                statuslist.Add(item);
            }
            Statuses = statuslist.AsEnumerable();
        }
    }
}