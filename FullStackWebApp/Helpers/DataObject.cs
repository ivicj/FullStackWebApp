using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.Models
{
    public class DataObject
    {
        public int GlobalId { get; set; }
        public string Id { get; set; }
        public string Woonplaats { get; set; }
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
    }
}
