using Config.Parser.Tokens;

namespace Config.Parser
{
    internal interface ITokenParser
    {
        Token GetNextToken();
    }
}