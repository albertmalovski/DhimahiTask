using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Country : BaseEntity
    {
        public string ISOCode { get; set; }
        public string Name { get; set; }
        public string CapitalCity { get; set; }
        public string PhoneCode { get; set; }
        public string CountryFlag { get; set; }
        public int ContinentId { get; set; }
        [ForeignKey(nameof(ContinentId))]
        public Continent Continent { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currency Currency { get; set; }
        public ICollection<CountryLanguage> CountryLanguage { get; set; }
    }
}
