using System;

namespace Config.Options
{
	public class SignedOption : Option<long>
	{
		public SignedOption(long data)
		{
			Data = data;
		}

		public SignedOption(string value)
		{
			Data = long.Parse(value);
		}

		public static implicit operator SignedOption(long l)
		{
			return new SignedOption(l);
		}
	}
}