using System.IO;
using Config.Parser.Tokens;

namespace Config.Parser
{
    class Parser
    {
        private readonly StreamReader _reader;

        public Parser(StreamReader reader)
        {
            _reader = reader;
        }

        public Token GetNextToken()
        {
            if (_reader.EndOfStream)
            {
                return null;
            }

            int c = _reader.Peek();
            while (isWhiteSpace(c))
            {
                _reader.Read();
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
