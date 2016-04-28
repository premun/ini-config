using System;

namespace Config.Options
{
	public class UnsignedOption : Option<ulong>
	{
		public UnsignedOption(ulong rawValue)
		{
			RawValue = rawValue;
		}

		public UnsignedOption(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator UnsignedOption(ulong l)
		{
			return new UnsignedOption(l);
		}
	}
}