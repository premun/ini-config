using System;

namespace Config.Attribute
{
	/// <summary>
	/// Class represents an attribute that is used for linking a class field/property and a config option.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConfigOptionAttribute : System.Attribute
    {
        private readonly string _section;
		private readonly string _option;
	    private readonly bool _required;

	    public ConfigOptionAttribute(string section, string option, bool required = false)
        {
            _section = section;
            _option = option;
		    _required = required;
        }
    }
}