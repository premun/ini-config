using System;

namespace Config.Options
{
	public class SignedOption : Option<long>
	{
		public SignedOption(long rawValue)
		{
			RawValue = rawValue;
		}

		public SignedOption(string value)
		{
			RawValue = long.Parse(value);
		}

		public static implicit operator SignedOption(long l)
		{
			return new SignedOption(l);
		}
	}
}