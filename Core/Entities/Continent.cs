using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Continent : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
