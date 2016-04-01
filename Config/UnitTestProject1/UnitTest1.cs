using System;
using System.Collections.Generic;
using Config.Format;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        enum DummyEnum
        {
            One,
            Two
        }

        [TestMethod]
        public void TestMethod1()
        {
            var formater = new FormatSpecifier
            {
                RequiredSections = new List<FormatSectionSpecifier>
                {
                    new FormatSectionSpecifier("Section1")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatOption<string>("Option1"),
                            new FormatOption<int>("Option2")
                        }
                    },
                    new FormatSectionSpecifier("Section2")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatListOption<int>("Opt1")
                        },
                        OptionalOption = new List<IFormatOption>
                        {
                            new FormatOption<bool>("Optional1")
                        }
                    }
                },
                OptionalSections = new List<FormatSectionSpecifier>
                {
                    new FormatSectionSpecifier("OptSection")
                    {
                        RequiredOptions = new List<IFormatOption>
                        {
                            new FormatOption<double>("ReqInOpt1"),
                            new FormatEnumOption<DummyEnum>("EnumOpt")
                        }
                    }
                }
            };

            Console.WriteLine(formater);
        }
    }
}
