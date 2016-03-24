using System.Collections.Generic;
using Config.Format.Errors;

namespace Config.Format
{
    /// <summary>
    /// Represents a functionality for validating IConfig in correspondence to format specification.
    /// </summary>
    public interface IFormatValidator
    {
        /// <summary>
        /// Validates IConfig in correspondence to format specification.
        /// </summary>
        /// <returns>Returns a list of errors.</returns>
        IEnumerable<Error> ValidateConfig();
    }
}