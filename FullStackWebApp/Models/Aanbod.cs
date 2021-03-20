using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FullStackWebApp.Models
{
    [DataContract]
    [Serializable]
    public class Aanbod
    {
        public Aanbod()
        {
            Makelaar = new Makelaar();
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string Type { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string City { get; set; }

        [DataMember]
        public bool Tuin { get; set; }

        [DataMember]
        public Makelaar Makelaar { get; set; }

    }
}
