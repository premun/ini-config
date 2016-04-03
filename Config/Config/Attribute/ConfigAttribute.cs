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
        private IFormatSpecifier _formatSpecifier;
        private BuildMode _mode;

        public ConfigAttribute(string file, Type formatSpecifier, BuildMode mode)
        {
            _file = file;
            _mode = mode;

            if (!typeof(IFormatSpecifier).IsAssignableFrom(formatSpecifier) )
            {
                throw new InvalidDataException();
            }
            _formatSpecifier = (IFormatSpecifier)Activator.CreateInstance(formatSpecifier);
        }
    }
}