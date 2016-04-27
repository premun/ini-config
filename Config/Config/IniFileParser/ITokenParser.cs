using Config.IniFileParser.Tokens;

namespace Config.IniFileParser
{
	internal interface ITokenParser
	{
		Token GetNextToken();
	}
}