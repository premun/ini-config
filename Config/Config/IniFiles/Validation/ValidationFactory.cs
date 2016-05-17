using System;

namespace Config.IniFiles.Validation
{
    /// <summary>
    ///     Creates validation strategy based on a required build mode.
    /// </summary>
    internal static class ValidationFactory
    {
        /// <summary>
        ///     Gets the validation strategy based on the build mode.
        /// </summary>
        /// <param name="buildMode">The build mode.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if build mode is unsupported.</exception>
        public static IValidation GetValidationStrategy(BuildMode buildMode)
        {
            switch (buildMode)
            {
                case BuildMode.Strict:
                    return new StrictStrategy();
                case BuildMode.Relaxed:
                    return new RelaxedStrategy();
                default:
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            "BuildMode is out of range. Founded value `{0}`",
                            buildMode));
            }
        }
    }
}