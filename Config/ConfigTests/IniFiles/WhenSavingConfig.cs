using System;
using System.IO;
using System.Text;
using Config;
using Config.Format;
using Config.Format.OptionSpecifiers;
using Config.IniFiles;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.IniFiles
{
	[TestClass]
	public class WhenSavingConfig
	{
		[TestMethod]
		public void BasicCaseShouldWork()
		{
			var config = new Config.Config();
			var section = config.AddSection("Section 1");

			section["int"] = 123;
			section["bool"] = true;

			section = config.AddSection("Section 2");

			section["float"] = 0.4f;
			section["signed"] = 100L;

			var lines = SaveConfigToString(config, Verbosity.None);

			lines[0].ShouldBeEquivalentTo("[Section 1]");
			lines[1].ShouldBeEquivalentTo("int = 123");
			lines[2].ShouldBeEquivalentTo("bool = false");

			lines[4].ShouldBeEquivalentTo("[Section 2]");
			lines[5].ShouldBeEquivalentTo("float = 0.4");
			lines[6].ShouldBeEquivalentTo("signed = 100");

			// TODO: test all types
		}

		[TestMethod]
		public void CommentsShouldBeSaved()
		{
			var config = new Config.Config();
			var section = config.AddSection("Section 1");

			section.Comment = "section comment";

			section["int"] = 123;
			section["int"].Comment = "foobar";

			var lines = SaveConfigToString(config, Verbosity.Comments);

			lines[0].ShouldBeEquivalentTo("[Section 1]");
			lines[1].ShouldBeEquivalentTo("; " + section.Comment);
			lines[2].ShouldBeEquivalentTo("int = 123\t; foobar");
		}

		[TestMethod]
		public void DefaultsShouldBeSaved()
		{
			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Section 1")
					.AddOption(new FloatOptionSpecifier("float", defaultValue: 0.4f))
					.AddOption(new IntOptionSpecifier("int", defaultValue: 4))
				.FinishDefinition();

			var config = new Config.Config(formatSpecifier);
			config.AddSection("Section 1");

			var lines = SaveConfigToString(config, Verbosity.Defaults);

			lines[0].ShouldBeEquivalentTo("[Section 1]");
			lines[1].ShouldBeEquivalentTo("float = 0.4");
			lines[2].ShouldBeEquivalentTo("int = 4");
		}

		private static string[] SaveConfigToString(IConfig config, Verbosity verbosity)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);

			new IniFileConfigSaver().SaveConfig(writer, config, verbosity);

			writer.Flush();

			return Encoding.UTF8
				.GetString(stream.GetBuffer(), 0, (int) stream.Length)
				.Split(new [] { Environment.NewLine }, StringSplitOptions.None);
		}
	}
}
