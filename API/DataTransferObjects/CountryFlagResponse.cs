using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CountryFlagResponse
    {

        [Required]
        public string fileName { get; set; }
        [Required]
        public string fileBase64 { get; set; }
        [Required]
        public string sha256 { get; set; }
    }
}
