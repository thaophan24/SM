using SM.Business.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Model
{
    public class Model1
    {
        [DynamicRequiredIf(Order = 2)]
        [Attribute1(Order = 1)]
        [Required]
        [Attribute2(Order =4)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
