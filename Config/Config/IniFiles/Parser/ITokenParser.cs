using Config.IniFiles.Parser.Tokens;

namespace Config.IniFiles.Parser
{
	internal interface ITokenParser
	{
		Token GetNextToken();
	}
}