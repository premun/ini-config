using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    public class ListOptionSpecifier<T> : OptionSpecifier<T>
    {
        public ListOptionSpecifier(string name, bool required = false, IEnumerable<T> defaultValue = null)
            : base(name, required, default(T))
        {
	        if (typeof(T) == typeof(ListOption<>))
	        {
		        throw new InvalidOperationException("List of list is not allowed.");
	        }

            DefaultValue = defaultValue;
        }

        internal override Option Parse(string value)
        {
            

            var subOptionType = GetSpecificOption();

            // Prepares ListOption<T> object
            var genericListType = typeof(ListOption<>);
            Type specific = genericListType.MakeGenericType(subOptionType);
            ConstructorInfo ci = specific.GetConstructor(Type.EmptyTypes);
            dynamic parsedListOption = ci.Invoke(new object[] { });

            parsedListOption.Values = CreateListOption(subOptionType, value);

            return parsedListOption;
        }

        private dynamic CreateListOption(Type innerGenericType, string inputValue)
        {
            // Prepares each Option from parsed input value
            Type genericList = typeof(List<>);
            Type specificList = genericList.MakeGenericType(innerGenericType);
            ConstructorInfo ctor = specificList.GetConstructor(Type.EmptyTypes);
            dynamic options = ctor.Invoke(new object[] { });

            var values = SplitListValues(inputValue);

            // Creates instance of each parsed option
            foreach (var simpleValue in values)
            {
                dynamic option = Activator.CreateInstance(innerGenericType, simpleValue);
                options.Add(option);
            }
        }

        private Type GetSpecificOption()
        {
            // TODO udelat nejak lepe?
            var type = typeof(T);
            if (type == typeof(int))
            {
                return typeof(IntOption);
            }
            if (type == typeof(bool))
            {
                return typeof(BoolOption);
            }
            // TODO test!
            if (type == typeof(Enum))
            {
                return typeof(EnumOption<>);
            }
            if (type == typeof(float))
            {
                return typeof(FloatOption);
            }
            if (type == typeof(List<>))
            {
                throw new ArgumentException("Value cannot contain List of List");
            }
            if (type == typeof(long))
            {
                return typeof(SignedOption);
            }
            if (type == typeof(string))
            {
                return typeof(StringOption);
            }
            if (type == typeof(ulong))
            {
                return typeof(UnsignedOption);
            }

            throw new ArgumentException(string.Format("Unknown type {0}.", type));
        }

        private IEnumerable<string> SplitListValues(string value)
        {
            var delimiters = new[] { ':', ',' };

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
                else if (delimiters.Contains(currChar))
                {
                    if (!quoted)
                    {
                        yield return
                            value.Substring(currStartIndex, i - currStartIndex)
                                .Trim()
                                .Replace("\\,", ",")
                                .Replace("\\:", ":")
                                .Replace("&quot;", "\\");
                        currStartIndex = i + 1;
                    }
                    else
                    {
                        // char is escaped, now looks for other delimiter
                        quoted = false;
                    }
                }
            }
            yield return value.Substring(currStartIndex, value.Length - currStartIndex)
                .Trim()
                .Replace("\\,", ",")
                .Replace("\\:", ":")
                .Replace("&quot;", "\\");
        }
    }
}
