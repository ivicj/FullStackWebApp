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
    public class Makelaar
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
