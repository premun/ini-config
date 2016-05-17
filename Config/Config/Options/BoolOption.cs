using System;

namespace Config.Options
{
    /// <summary>
    ///     Holds value of boolean option.
    /// </summary>
    /// <seealso cref="Config.Options.Option{System.Boolean}" />
    public sealed class BoolOption : Option<bool>
    {
        private readonly string _textValue;

        public BoolOption(bool data)
        {
            Data = data;
            _textValue = "false";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BoolOption" /> class.
        ///     Input value for <c>false</c>:
        ///     <list type="number">
        ///         <item>0</item>
        ///         <item>f</item>
        ///         <item>n</item>
        ///         <item>off</item>
        ///         <item>no</item>
        ///         <item>disabled</item>
        ///         <item>false</item>
        ///         <item>False</item>
        ///     </list>
        ///     Input value for <c>true</c>:
        ///     <list type="number">
        ///         <item>1</item>
        ///         <item>t</item>
        ///         <item>y</item>
        ///         <item>on</item>
        ///         <item>yes</item>
        ///         <item>enabled</item>
        ///         <item>true</item>
        ///         <item>True</item>
        ///     </list>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public BoolOption(string value)
        {
            _textValue = value;

            switch (value)
            {
                case "0":
                case "f":
                case "n":
                case "off":
                case "no":
                case "disabled":
                case "false":
                case "False":
                    Data = false;
                    break;

                case "1":
                case "t":
                case "y":
                case "on":
                case "yes":
                case "enabled":
                case "true":
                case "True":
                    Data = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        string.Format("Unknown boolean value '{0}'", value));
            }
        }

        public override string Serialize()
        {
            return _textValue;
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.Boolean" /> to <see cref="BoolOption" />.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BoolOption(bool b)
        {
            return new BoolOption(b);
        }
    }
}