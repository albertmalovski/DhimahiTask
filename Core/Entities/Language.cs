using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Language : BaseEntity
    {
        public string ISOCode { get; set; }
        public string Name { get; set; }
        public ICollection<CountryLanguage> CountryLanguage { get; set; }

    }

}
