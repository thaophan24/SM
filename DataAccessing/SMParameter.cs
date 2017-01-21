using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing
{
    public class SMParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType DbType { get; set; } 
    }
    public class SMParameterCollection : List<SMParameter>
    {
        public SMParameterCollection AddParam(SMParameter item)
        {
            if (item != null)
            {
                this.Add(item);
            }
            return this;
        }
    }
}
