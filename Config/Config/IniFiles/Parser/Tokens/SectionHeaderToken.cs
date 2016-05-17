using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles.Parser.Tokens
{
    /// <summary>
    ///     Represents a config file section [Header] that the parser read.
    /// </summary>
    internal sealed class SectionHeaderToken : Token
    {
        internal string Name { get; set; }

        internal static SectionHeaderToken FromStream(StreamReader stream)
        {
            var sb = new StringBuilder();

            // Consume opening square bracket
            if (stream.Peek() == '[')
            {
                stream.Read();
            }

            int c = stream.Read();
            while (c != ']')
            {
                if (c == '\n')
                {
                    throw new FormatException(
                        "Section header without closing bracket found!");
                }

                sb.Append((char) c);
                c = stream.Read();
            }

            if (sb.Length == 0)
            {
                throw new FormatException(
                    "Section header with empty name encountered!");
            }

            return new SectionHeaderToken
            {
                Name = sb.ToString()
            };
        }
    }
}