using System.Linq;
using Config.ConfigExceptions;
using Config.IniFiles.Parser.Tokens;

namespace Config.Options
{
    /// <summary>
    ///     Represents string option. Given value is validated if do not contain any of ',', ':', ';' characters which are not
    ///     preceded with \
    /// </summary>
    /// <seealso cref="Config.Options.Option{System.String}" />
    public sealed class StringOption : Option<string>
    {
        private readonly char[] _forbiddenChars = {',', ':', ';'};

        public StringOption(string data)
        {
            var value = ValidateStringFormat(data);
            Data = value;
        }

        internal StringOption(OptionToken token)
        {
            var value = ValidateStringFormat(token.Value);
            Data = value;
            Comment = token.Comment;
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.String" /> to <see cref="StringOption" />.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator StringOption(string s)
        {
            return new StringOption(s);
        }

        private string ValidateStringFormat(string value)
        {
            value = value.Replace("\\\\", "&quot;");
            bool quoted = false;
            foreach (char currChar in value)
            {
                if (currChar == '\\')
                {
                    quoted = true;
                }
                else if (_forbiddenChars.Contains(currChar))
                {
                    if (!quoted)
                    {
                        throw new ConfigException(
                            string.Format(
                                "String option `{0}` cannot contains forbidden char `{1}`.",
                                value, currChar));
                    }

                    // char is escaped, now looks for other delimiter
                    quoted = false;
                }
            }

            return value
                .Replace("\\,", ",")
                .Replace("\\:", ":")
                .Replace("\\;", ";");
        }
    }
}