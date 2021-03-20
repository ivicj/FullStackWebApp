using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.DTOs
{
    public class PagingDTO
    {
        public int AantalPaginas { get; set; } // total pages
        public int HuidigePagina { get; set; } // current page
    }
}
