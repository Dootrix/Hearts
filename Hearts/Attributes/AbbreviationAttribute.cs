using System;

namespace Hearts.Attributes
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
