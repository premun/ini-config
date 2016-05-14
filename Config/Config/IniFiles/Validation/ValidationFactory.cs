using System;

namespace Config.IniFiles.Validation
{
    public static class ValidationFactory
    {
        public static IValidation GetValidationStrategy(BuildMode buildMode)
        {
            switch (buildMode)
            {
                case BuildMode.Strict:
                    return new StrictStrategy();
                case BuildMode.Relaxed:
                    return new RelaxedStrategy();
                default:
                    throw new ArgumentOutOfRangeException(string.Format("BuildMode is out of range. Founded value `{0}`", buildMode));
            }
        }
    }
}