namespace Config.Options
{
	public class UnsignedOption : Option<ulong>
	{
		public UnsignedOption(ulong data)
		{
			Data = data;
		}

		public UnsignedOption(string value)
		{
			Data = ulong.Parse(value);
		}

		public static implicit operator UnsignedOption(ulong l)
		{
			return new UnsignedOption(l);
		}
	}
}