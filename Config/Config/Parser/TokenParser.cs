﻿using System.IO;
using System.Runtime.CompilerServices;
using Config.Parser.Tokens;

[assembly: InternalsVisibleTo("Test")]

namespace Config.Parser
{
    class TokenParser
    {
        private readonly StreamReader _reader;

        public TokenParser(StreamReader reader)
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
                c = _reader.Peek();
            }

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