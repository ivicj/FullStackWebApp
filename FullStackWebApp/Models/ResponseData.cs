using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.Models
{
    public class ResponseData
    {
        public ResponseData()
        {
            Objects = new List<DataObject>();
        }

        public int AccountStatus { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public bool ValidationFailed { get; set; }
        public string ValidationReport { get; set; }
        public int Website { get; set; }
        //public string Metadata { get; set; }
        public List<DataObject> Objects { get; set; }
        //public string Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }


    }
}
