using System.Collections.Generic;
using Config.Format;

namespace Config
{
    public class Examples
    {
        public void TestFormater()
        {
            var formater = new FormatSpecifier
            {
                RequiredSections = new List<FormatSectionSpecifier>
                {
                    new FormatSectionSpecifier("Section1")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatOption("Option1"),
                            new FormatOption("Option2")
                        }
                    },
                    new FormatSectionSpecifier("Section2")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatOption("Opt1")
                        },
                        OptionalOption = new List<IFormatOption>
                        {
                            new FormatOption("Optional1")
                        }
                    }
                },
                OptionalSections = new List<FormatSectionSpecifier>
                {
                    new FormatSectionSpecifier("OptSection")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatOption("ReqInOpt1")
                        }
                    }
                }
            };

        }
    }
}