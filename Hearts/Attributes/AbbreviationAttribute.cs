using System;

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
