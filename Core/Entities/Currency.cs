using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Currency : BaseEntity
    {
        public string ISOCode { get; set; }
        public string Name { get; set; }
    }
}
