using Config.Format;
using Config.Format.OptionSpecifiers;
using FluentAssertions;
using NUnit.Framework;
using Config.IniFiles.Parser.Tokens;
using Config.IniFiles;
using Config;
using System;
using System.Linq;
using Config.IniFiles.Errors;
using Config.ConfigExceptions;

namespace ConfigTests.Format
{
	[TestFixture]
	public class WhenSpecifyingFormat
	{
		private enum Domains
		{
			Com,
			Eu,
			Fr
		}

		[Test]
		public void SpecificationShouldBeSaved()
		{
			var formatSpecifier = new ConfigFormatSpecifier()
			.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.AddOption(new ConstraintOptionSpecifier<int>("port", x => x > 0 && x < 65536, defaultValue: 3306))
				.AddOption(new EnumOptionSpecifier<Domains>("domain", defaultValue: Domains.Eu))
			.AddSection("HTTP", true)
				.AddOption(new IntOptionSpecifier("timeout", defaultValue: 5000))
				.AddOption(new BoolOptionSpecifier("use_https"))
			.FinishDefinition();
			
			formatSpecifier["Server"].Should().NotBeNull();
			formatSpecifier["HTTP"].Should().NotBeNull();

			var serverSection = formatSpecifier["Server"];
			serverSection.Name.ShouldBeEquivalentTo("Server");
			serverSection.Required.ShouldBeEquivalentTo(true);

			serverSection["hostname"].Should().NotBeNull();
			serverSection["hostname"].Required.Should().BeTrue();
			serverSection["port"].Required.Should().BeFalse();
			((ConstraintOptionSpecifier<int>) serverSection["port"]).DefaultValue.ShouldBeEquivalentTo(3306);
			serverSection["domain"].Should().BeOfType<EnumOptionSpecifier<Domains>>();
			((EnumOptionSpecifier<Domains>) serverSection["domain"]).DefaultValue.ShouldBeEquivalentTo(Domains.Eu);

			var httpSection = formatSpecifier["HTTP"];
			httpSection.Name.ShouldBeEquivalentTo("HTTP");
			httpSection.Required.ShouldBeEquivalentTo(true);

			httpSection["use_https"].Should().NotBeNull();
			httpSection["use_https"].Required.Should().BeFalse();
			((BoolOptionSpecifier) httpSection["use_https"]).DefaultValue.ShouldBeEquivalentTo(null);
		}

		[Test]
		public void MissingSectionShouldRaise()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken { Name = "Foo" },
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.FinishDefinition();

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(formatSpecifier, BuildMode.Strict);
			};

			build.ShouldThrow<ConfigFormatException>();
			builder.Ok.Should().BeFalse();
			builder.Errors.First().Should().BeOfType<MissingSectionOrOptionError>();
		    var error = (MissingSectionOrOptionError) builder.Errors.First();
		    error.ConfigExceptions.First().Should().BeOfType<MissingSectionException>();
		}

		[Test]
		public void MissingOptionShouldRaise()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken { Name = "Server" },
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.FinishDefinition();

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(formatSpecifier, BuildMode.Strict);
			};

			build.ShouldThrow<ConfigFormatException>();
			builder.Ok.Should().BeFalse();
            builder.Errors.First().Should().BeOfType<MissingSectionOrOptionError>();
            var error = (MissingSectionOrOptionError)builder.Errors.First();
            error.ConfigExceptions.First().Should().BeOfType<MissingOptionException>();
		}

		[Test]
		public void MultipleMissingItemsShouldRaise()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken { Name = "Foo" },
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.AddOption(new IntOptionSpecifier("port", true))
				.AddSection("Server 2", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.FinishDefinition();

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(formatSpecifier, BuildMode.Strict);
			};

			build.ShouldThrow<ConfigFormatException>();
			builder.Ok.Should().BeFalse();

            builder.Errors.First().Should().BeOfType<MissingSectionOrOptionError>();
            var error = (MissingSectionOrOptionError)builder.Errors.First();

            var errors = error.ConfigExceptions.ToArray();

			errors.Count().ShouldBeEquivalentTo(2);
			errors[0].Should().BeOfType<MissingSectionException>();
			errors[1].Should().BeOfType<MissingSectionException>();
		}

		[Test]
		public void MissingItemsInRelaxedModeShouldBeReported()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken { Name = "Foo" },
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.AddOption(new IntOptionSpecifier("port", true))
				.AddSection("Server 2", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.FinishDefinition();

			var builder = new IniFileConfigBuilder(parser);

			builder.Build(formatSpecifier, BuildMode.Relaxed);
			builder.Ok.Should().BeFalse();

			var errors = builder.Errors.ToArray();

			errors.Count().ShouldBeEquivalentTo(3);
			errors[0].Should().BeOfType<MissingSectionException>();
			errors[1].Should().BeOfType<MissingOptionException>();
			errors[2].Should().BeOfType<MissingSectionException>();
		}
	}
}
