using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class ListOptionSpecifier<T> : OptionSpecifier<T>
	{
		private readonly char[] _possibleDelimiters = { ':', ',' };

		public ListOptionSpecifier(string name, bool required = false, IEnumerable<T> defaultValue = null)
			: base(name, required, default(T))
		{
			if (typeof(T) == typeof(ListOption<>))
			{
				throw new InvalidOperationException("Lists of lists are not allowed.");
			}

			DefaultValue = defaultValue == null ? null : string.Join(",", defaultValue);
		}

		internal override Option Parse(string value)
		{
			var optionType = GetSpecificOption();
			var listOption = CreateListOption(optionType, value);

			return listOption;
		}

		private dynamic CreateListOption(Type innerGenericType, string inputValue)
		{
			// Prepares ListOption<T> object
			var genericListType = typeof(ListOption<>);
			Type specific = genericListType.MakeGenericType(innerGenericType);
			ConstructorInfo ctor = specific.GetConstructor(Type.EmptyTypes);
			dynamic parsedListOption = ctor.Invoke(new object[] { });

			parsedListOption.Values = CreateGenericOptions(innerGenericType, inputValue);

			return parsedListOption;
		}

		private dynamic CreateGenericOptions(Type innerGenericType, string inputValue)
		{
			if (inputValue == null)
			{
				return null;
			}

			// Prepares each Option from parsed input value
			Type genericList = typeof(List<>);
			Type specificList = genericList.MakeGenericType(innerGenericType);
			ConstructorInfo ctor = specificList.GetConstructor(Type.EmptyTypes);
			dynamic options = ctor.Invoke(new object[] { });

			var splitListValues = SplitListValues(inputValue);

			// Creates instance of each parsed option
			foreach (var simpleValue in splitListValues)
			{
				dynamic option = Activator.CreateInstance(innerGenericType, simpleValue);
				options.Add(option);
			}

			return options;
		}

		private Type GetSpecificOption()
		{
			var type = typeof(T);

			if (type == typeof(int))
			{
				return typeof(IntOption);
			}
			if (type == typeof(bool))
			{
				return typeof(BoolOption);
			}
			if (type == typeof(Enum))
			{
				return typeof(EnumOption<>);
			}
			if (type == typeof(float))
			{
				return typeof(FloatOption);
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
			// Selects default when string does not contains any delimiter
			var delimiter = value.FirstOrDefault(x => _possibleDelimiters.Contains(x));

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
