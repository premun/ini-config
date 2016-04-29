using Config.IniFiles.Parser.Tokens;

namespace Config.Options
{
	public class StringOption : Option<string>
	{
		public StringOption(string rawValue)
		{
			RawValue = rawValue;
		}

		internal StringOption(OptionToken token)
		{
			RawValue = token.Value;
			Comment = token.Comment;
		}

		public static implicit operator StringOption(string s)
		{
			return new StringOption(s);
		}
	}
}
