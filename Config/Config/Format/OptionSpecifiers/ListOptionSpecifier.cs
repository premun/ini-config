using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class ListOptionSpecifier<T> : OptionSpecifier<T> where T : Option
	{
		public ListOptionSpecifier(string name, bool required = false, T defaultValue = null) : base(name, required, defaultValue)
		{
		}

		internal override Option Parse(string value)
		{
			throw new System.NotImplementedException();
		}
	}
}
