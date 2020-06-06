using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Helpers
{
    public class PropertyMapping
    {
        public bool IsRevert { get; private set; }
        public string TargetProperty { get; private set; }
        public PropertyMapping(string targetProperty, bool revert = false)
        {
            TargetProperty = targetProperty;
            IsRevert = revert;
        }
    }
}
