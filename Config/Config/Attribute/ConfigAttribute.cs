using System;
using System.IO;
using Config.Format;

namespace Config.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct)
    ]
    public class ConfigAttribute : System.Attribute
    {
        private string _file;
        private readonly IFormatSpecifier _formatSpecifier;

        public ConfigAttribute(string file, Type formatSpecifier)
        {
            _file = file;

            if (!typeof(IFormatSpecifier).IsAssignableFrom(formatSpecifier) )
            {
                throw new InvalidDataException();
            }
            _formatSpecifier = (IFormatSpecifier)Activator.CreateInstance(formatSpecifier);
        }
    }
}