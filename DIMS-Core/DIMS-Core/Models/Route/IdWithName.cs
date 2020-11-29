using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Route
{
    public class IdWithName
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public IdWithName(int id, string name)
        {
            Value = id;
            Name = name;
        }
    }
}
