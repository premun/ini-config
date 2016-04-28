using System;

namespace Config.Options
{
	public class ReferenceOption : Option
	{
		public string Section { get; set; }

		public string Option { get; set; }

		public Option ReferencedValue { get; set; }

		public ReferenceOption(string value)
		{
			throw new NotImplementedException();
		}
	}
}