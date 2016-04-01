using System;
using System.Collections.Generic;
using Config.Format;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
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

            Console.WriteLine(formater);
        }
    }
}
