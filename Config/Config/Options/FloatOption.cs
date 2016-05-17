using System.Globalization;

namespace Config.Options
{
    /// <summary>
    ///     Represents float option parsed with <c>CultureInfo.InvariantCulture</c>.
    /// </summary>
    /// <seealso cref="Config.Options.NumericOption{System.Single}" />
    public sealed class FloatOption : NumericOption<float>
    {
        public FloatOption(float data)
        {
            Data = data;
        }

        public FloatOption(string value)
        {
            // Float does not support binary/hex/... format
            Data = float.Parse(value, CultureInfo.InvariantCulture);
        }

        public override string Serialize()
        {
            return ((float) Data).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.Single" /> to <see cref="FloatOption" />.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator FloatOption(float f)
        {
            return new FloatOption(f);
        }
    }
}