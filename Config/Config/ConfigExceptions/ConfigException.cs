using System;

namespace Config.ConfigExceptions
{
    /// <summary>
    ///     General exception for Config library
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ConfigException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigException" /> class.
        /// </summary>
        public ConfigException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConfigException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner.</param>
        public ConfigException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}