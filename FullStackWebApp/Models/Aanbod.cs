using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FullStackWebApp.Models
{
    [DataContract]
    [Serializable]
    public class Aanbod
    {

        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string GUID { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string Type { get; set; } = "koop";

        [DataMember]
        [MaxLength(50)]
        public string City { get; set; }

        [DataMember]
        public bool Tuin { get; set; }

        [DataMember]
        public int MakelaarId { get; set; }

        [DataMember]
        [MaxLength(100)]
        public string MakelaarNaam { get; set; }

    }
}
