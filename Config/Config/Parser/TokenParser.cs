using System.IO;
using System.Runtime.CompilerServices;
using Config.Parser.Tokens;

[assembly: InternalsVisibleTo("Test")]

namespace Config.Parser
{
    /// <summary>
    /// Parses stream into tokens and returns them one by one.
    /// </summary>
    internal class TokenParser : ITokenParser
    {
        private readonly StreamReader _reader;

        public TokenParser(StreamReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Reads new token from stream.
        /// </summary>
        /// <returns>Next token or null if file at the end.</returns>
        public Token GetNextToken()
        {
            if (_reader.EndOfStream)
            {
                return null;
            }

            // Let's skip empty beginnings of lines
            int c = _reader.Peek();
            while (isWhiteSpace(c))
            {
                _reader.Read();
                c = _reader.Peek();
            }

            // File ended with whitespaces / new lines
            if (_reader.EndOfStream)
            {
                return null;
            }

            switch (c)
            {
                case '[':
                    return new SectionHeaderToken(_reader);

                case ';':
                    return new CommentToken(_reader);

                case '\n':
                    return new NewLineToken();

                default:
                    return new ItemToken(_reader);
            }
        }

        private bool isWhiteSpace(int c)
        {
            return char.IsWhiteSpace((char) c);
        }
    }
}
