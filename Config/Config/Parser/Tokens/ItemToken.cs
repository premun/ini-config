using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]

namespace Config.Parser.Tokens
{
    /// <summary>
    /// Represents a config key=value item that the parser read.
    /// </summary>
    internal class ItemToken : Token
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ItemToken(StreamReader reader)
        {
            string line = reader.ReadLine();

            if (!line.Contains("="))
            {
                throw new FormatException("Missing '=' in item declaration!");
            }

            string[] parts = line.Split(new []{ '=' }, 2);
            string value = TrimEndEscaped(parts[1].TrimStart());

            // Comment after value
            // foo = bar ; comment
            if (value.Contains(";"))
            {
                value = value.Substring(0, value.IndexOf(';'));
                value = TrimEndEscaped(value);
            }

            Name = parts[0].Trim();
            Value = value.Replace("\\ ", " ");
        }

        private string TrimEndEscaped(string s)
        {
            if (char.IsWhiteSpace(s[s.Length - 1]))
            {
                s = s.TrimEnd();
                return s.EndsWith("\\") ? s + ' ' : s;
            }

            return s;
        }
    }
}
