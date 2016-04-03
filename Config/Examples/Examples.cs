using System.Collections.Generic;
using Config;
using Config.Attribute;
using Config.Format;
using Config.Values;

namespace Examples
{
    public class Examples
    {
        public static readonly FormatSpecifier FormatSpecifier = new FormatSpecifier
        {
            RequiredSections = new List<FormatSectionSpecifier>
            {
                new FormatSectionSpecifier("Section1")
                {
                    RequiredOptions = new List<IFormatOption>
                    {
                        new FormatOption<string>("Option1"),
                        new FormatOption<IntConfigValue>("Option2")
                    }
                },
                new FormatSectionSpecifier("Section2")
                {
                    RequiredOptions = new List<IFormatOption>
                    {
                        new FormatListOption<int>("Opt1")
                    },
                    OptionalOptions = new List<IFormatOption>
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
                        new FormatOption<double>("ReqInOpt1")
                    }
                }
            }
        };

        private void ExampleUsage(IConfigBuilder configBuilder)
		{
			var config = configBuilder.Build(FormatSpecifier, BuildMode.Strict);
			config["foo"]["bar"].Get<int>();
		}
	}

    [Config("c:/testCongig.ini", typeof(ExampleFormater))]
    public class AttributeExample
    {
        [ConfigOption("First", "testString")]
        private string _testString;

        [ConfigOption("First", "testBool")]
        private bool _testBool;
    }

    public class ExampleFormater : IFormatSpecifier
    {
        private List<FormatSectionSpecifier> _requiredSections = new List
            <FormatSectionSpecifier>
        {
            new FormatSectionSpecifier("Section1")
            {
                RequiredOptions = new List<IFormatOption>
                {
                    new FormatOption<string>("Option1"),
                    new FormatOption<IntConfigValue>("Option2")
                }
            },
            new FormatSectionSpecifier("Section2")
            {
                RequiredOptions = new List<IFormatOption>
                {
                    new FormatListOption<int>("Opt1")
                },
                OptionalOptions = new List<IFormatOption>
                {
                    new FormatOption<bool>("Optional1")
                }
            }
        };

        private List<FormatSectionSpecifier> _optionalSections = new List
            <FormatSectionSpecifier>
        {
            new FormatSectionSpecifier("OptSection")
            {
                RequiredOptions = new List<IFormatOption>
                {
                    new FormatOption<double>("ReqInOpt1")
                }
            }
        };

        #region Implementation of IFormatSpecifier

        public List<FormatSectionSpecifier> RequiredSections
        {
            get { return _requiredSections; }
            private set { _requiredSections = value; }
        }

        public List<FormatSectionSpecifier> OptionalSections
        {
            get { return _optionalSections; }
            private set { _optionalSections = value; }
        }

        #endregion
    }
}
