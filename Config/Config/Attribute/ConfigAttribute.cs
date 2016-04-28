using System;
using System.IO;
using Config.Format;

namespace Config.Attribute
{
	/// <summary>
	/// Class represents an attribute that is used for linking a class and a config file.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ConfigAttribute : System.Attribute
    {
        private string _file;
        private ConfigFormatSpecifier _formatSpecifier;
        private BuildMode _mode;

        public ConfigAttribute(string file, Type formatSpecifier, BuildMode mode)
        {
            _file = file;
            _mode = mode;

            if (!typeof(ConfigFormatSpecifier).IsAssignableFrom(formatSpecifier) )
            {
                throw new InvalidDataException();
            }

            _formatSpecifier = ((IConfigFormatSpecifier) Activator.CreateInstance(formatSpecifier)).GetFormatSpecifier();
        }
    }
}