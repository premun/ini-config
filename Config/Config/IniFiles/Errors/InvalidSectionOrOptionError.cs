using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.Format;

namespace Config.IniFiles.Errors
{
    /// <summary>
    ///     Raised when a config does not contain required section or option.
    /// </summary>
    /// <seealso cref="Config.IniFiles.Errors.FormatError" />
    public sealed class InvalidSectionOrOptionError : FormatError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidSectionOrOptionError" /> class.
        /// </summary>
        /// <param name="configException">The configuration exception.</param>
        public InvalidSectionOrOptionError(ConfigFormatException configException)
            : base(
                string.Format(
                    "Config does not contain all requred members. See exception errirs `{1}` and message: `{0}`",
                    configException,
                    string.Join(",", configException.ErrorList)),
                -1)
        {
            ConfigExceptions = configException.ErrorList;
        }

        /// <summary>
        ///     Gets the detected configuration exceptions.
        /// </summary>
        /// <value>
        ///     The configuration exceptions.
        /// </value>
        public IEnumerable<ConfigException> ConfigExceptions { get; private set;
        }
    }
}