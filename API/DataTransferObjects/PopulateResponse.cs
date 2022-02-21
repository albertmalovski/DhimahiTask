using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PopulateResponse
    {

        [Required]
        public string Status { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
