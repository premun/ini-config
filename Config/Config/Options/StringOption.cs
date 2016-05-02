using Config.IniFiles.Parser.Tokens;

namespace Config.Options
{
	// TODO: element typu string může obsahovat libovolné znaky s výjimkou ',', ':' a ';', kde je třeba je uvést znakem '\'
	public class StringOption : Option<string>
	{
		public StringOption(string data)
		{
			Data = data;
		}

		internal StringOption(OptionToken token)
		{
			Data = token.Value;
			Comment = token.Comment;
		}

		public static implicit operator StringOption(string s)
		{
			return new StringOption(s);
		}
	}
}
