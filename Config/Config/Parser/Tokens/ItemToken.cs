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
            string line = reader.ReadLine().Trim();

            if (!line.Contains("="))
            {
                throw new FormatException("Missing '=' in item declaration!");
            }

            string[] parts = line.Split(new []{ '=' }, 2);
            string value = parts[1].Trim();

            // Comment after value
            // foo = bar ; comment
            if (value.Contains(";"))
            {
                value = value
                    .Substring(0, value.IndexOf(';'))
                    .Trim();
            }

            // Escaped spaces at the end
            if (value.EndsWith("\\"))
            {
                value += ' ';
            }

            Name = parts[0].Trim();
            Value = value;
        }
    }
}
