using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AbbreviationAttribute : Attribute
    {
        public string Value { get; private set; }

        public AbbreviationAttribute(string value)
        {
            this.Value = value;
        }
    }
}
