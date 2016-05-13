using System;
using System.Globalization;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public abstract class OptionSpecifier
	{
		public string Name { get; set; }

		public bool Required { get; set; }

		public object DefaultValue { get; set; }

		protected OptionSpecifier(string name, bool required = false, object defaultValue = null)
		{
			Name = name;
			Required = required;
			DefaultValue = defaultValue;
		}

		internal abstract Option Parse(string value);

		internal Option Parse(object value)
		{
			// TODO: Tady by se melo ulozit parsedValue nekam a pak vracet to
			//       Duvod je ten, ze kdyz se budu opakovane ptat na option, ktery v configu neni,
			//		 ale ma defaultValue, tak se vytvari dokola porad novy a novy objekty
			return Parse(string.Format(CultureInfo.InvariantCulture, "{0}", value));
		}
	}

	public abstract class OptionSpecifier<T> : OptionSpecifier
	{
		protected OptionSpecifier(string name, bool required, T defaultValue) 
			: base(name, required, defaultValue)
		{
		}
	}
}