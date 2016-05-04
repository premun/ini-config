using System;
using System.Collections.Generic;
using System.Linq;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class ListOptionSpecifier<T> : OptionSpecifier<T>
	{
		public ListOptionSpecifier(string name, bool required = false, IEnumerable<T> defaultValue = null) 
			: base(name, required, default(T))
		{
			DefaultValue = defaultValue;
		}

		internal override Option Parse(string value)
		{
            var splitedValue = SplitListValues(value);
            

            // TODO jak idealne konverotvat type na option? a pak podle toho tvorit jednotlive option?
            var resultOption = new ListOption<T>();

		    foreach (var simpleValue in splitedValue)
		    {
		        Console.WriteLine(simpleValue);
		    }

			// TODO
			throw new System.NotImplementedException();
		}

        private IEnumerable<string> SplitListValues(string value)
        {
            var delimiters = new[] {':', ';', ','};

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
                                .Replace("\\", "")
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
                .Replace("\\", "")
                .Replace("&quot;", "\\");
        }
	}
}
