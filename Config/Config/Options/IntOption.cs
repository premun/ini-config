using System;
using Config.Format.OptionSpecifiers;

namespace Config.Options
{
	public class IntOption : Option<int>
	{
		public IntOption(int rawValue)
		{
			RawValue = rawValue;
		}

		public IntOption(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator IntOption(int i)
		{
			return new IntOption(i);
		}
	}
}
