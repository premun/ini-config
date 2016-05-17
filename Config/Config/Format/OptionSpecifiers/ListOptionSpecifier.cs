using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for a list of option.
    /// </summary>
    /// <typeparam name="T">The type of the list options.</typeparam>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{T}" />
    public sealed class ListOptionSpecifier<T> : OptionSpecifier<T>
    {
        private readonly char[] _possibleDelimiters = {':', ','};

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListOptionSpecifier{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        /// <exception cref="System.InvalidOperationException">Lists of lists are not allowed.</exception>
        public ListOptionSpecifier(string name, bool required = false,
            IEnumerable<T> defaultValue = null)
            : base(name, required, default(T))
        {
            if (typeof (T) == typeof (ListOption<>))
            {
                throw new InvalidOperationException(
                    "Lists of lists are not allowed.");
            }

            DefaultValue = defaultValue == null
                ? null
                : string.Join(",", defaultValue);
        }

        /// <summary>
        ///     Parses the specified value to the boolean option (creates new instance of
        ///     <seealso cref="Config.Options.ListOption{T}" />). Generic type of the ListOption and each option are resolved at
        ///     the run-time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            var optionType = GetSpecificOption();
            var listOption = CreateListOption(optionType, value);

            return listOption;
        }

        private dynamic CreateListOption(Type innerGenericType,
            string inputValue)
        {
            // Prepares ListOption<T> object
            var genericListType = typeof (ListOption<>);
            dynamic values = CreateGenericOptions(innerGenericType, inputValue);
            Type specific = genericListType.MakeGenericType(innerGenericType);
            ConstructorInfo ctor =
                specific.GetConstructor(new[]
                {typeof (IEnumerable<>).MakeGenericType(innerGenericType)});
            dynamic parsedListOption = ctor.Invoke(new object[] {values});

            return parsedListOption;
        }

        private dynamic CreateGenericOptions(Type innerGenericType,
            string inputValue)
        {
            if (inputValue == null)
            {
                return null;
            }

            // Prepares each Option from parsed input value
            Type genericList = typeof (List<>);
            Type specificList = genericList.MakeGenericType(innerGenericType);
            ConstructorInfo ctor = specificList.GetConstructor(Type.EmptyTypes);
            dynamic options = ctor.Invoke(new object[] {});
            var add = options.GetType().GetMethod("Add");

            var splitListValues = SplitListValues(inputValue);

            // Creates instance of each parsed option
            foreach (var simpleValue in splitListValues)
            {
                dynamic option = Activator.CreateInstance(innerGenericType,
                    simpleValue);
                add.Invoke(options, new object[] {option});
            }

            return options;
        }

        /// <summary>
        ///     Gets the specific option based on generic T type.
        /// </summary>
        /// <returns>The relevant type.</returns>
        /// <exception cref="System.ArgumentException">Thrown when type is not supported.</exception>
        private Type GetSpecificOption()
        {
            var type = typeof (T);

            if (type == typeof (int))
            {
                return typeof (IntOption);
            }
            if (type == typeof (bool))
            {
                return typeof (BoolOption);
            }
            if (type == typeof (Enum))
            {
                return typeof (EnumOption<>);
            }
            if (type == typeof (float))
            {
                return typeof (FloatOption);
            }
            if (type == typeof (long))
            {
                return typeof (SignedOption);
            }
            if (type == typeof (string))
            {
                return typeof (StringOption);
            }
            if (type == typeof (ulong))
            {
                return typeof (UnsignedOption);
            }
            if (type.IsEnum)
            {
                return typeof (EnumOption<>).MakeGenericType(type);
            }

            throw new ArgumentException(string.Format("Unknown type {0}.", type));
        }

        /// <summary>
        ///     Splits the list values. Looks for the first delimiter (':' or ',') and split the given value with it.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Spitted input value.</returns>
        private IEnumerable<string> SplitListValues(string value)
        {
            // Selects default when string does not contains any delimiter
            var delimiter =
                value.FirstOrDefault(x => _possibleDelimiters.Contains(x));

            value = value.Replace("\\\\", "&quot;");
            bool quoted = false;
            int currStartIndex = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char currChar = value[i];
                if (currChar == '\\')
                {
                    quoted = true;
                }
                else if (delimiter == currChar)
                {
                    if (!quoted)
                    {
                        yield return
                            value.Substring(currStartIndex, i - currStartIndex)
                                .Trim()
                                .Replace("&quot;", "\\");
                        currStartIndex = i + 1;
                    }
                }
                else
                {
                    // char is escaped, now looks for other delimiter
                    quoted = false;
                }
            }
            yield return
                value.Substring(currStartIndex, value.Length - currStartIndex)
                    .Trim()
                    .Replace("&quot;", "\\");
        }
    }
}