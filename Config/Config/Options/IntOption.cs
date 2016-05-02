namespace Config.Options
{
	public class IntOption : Option<int>
	{
		public IntOption(int data)
		{
			Data = data;
		}

		public IntOption(string value)
		{
			Data = int.Parse(value);
		}

		public static implicit operator IntOption(int i)
		{
			return new IntOption(i);
		}
	}
}
