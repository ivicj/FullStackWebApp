using FullStackWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.Models
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            Objects = new List<AanbodDTO>();
            Paging = new PagingDTO();
        }

        public List<AanbodDTO> Objects { get; set; }
        public PagingDTO Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }


    }
}
