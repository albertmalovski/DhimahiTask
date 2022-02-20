using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CountryLanguage : BaseEntity
    {

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }

    }
}
