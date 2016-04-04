using System;

namespace Config.Values
{
	public class ReferenceConfigValue : ConfigValue
	{
		public string Section { get; set; }

		public string Option { get; set; }

		public ConfigValue ReferencedValue { get; set; }

		public ReferenceConfigValue(string value)
		{
			throw new NotImplementedException();
		}
	}
}