using System.Collections.Generic;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class ListOptionSpecifier<T> : OptionSpecifier<T>
	{
		public ListOptionSpecifier(string name, bool required = false, IEnumerable<T> defaultValue = null) 
			: base(name, required, default(T))
		{
			DefaultValue = defaultValue;
		}

		internal override Option Parse(string value)
		{
			throw new System.NotImplementedException();
		}
	}
}
