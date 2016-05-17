using System;
using System.Linq;
using Config;
using Config.IniFiles;
using Config.IniFiles.Errors;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.IniFiles
{
    [TestFixture]
    public class WhenBuildingInvalidConfig
    {
        [Test]
        public void DuplicateSectionShouldRaiseError()
        {
            var parser = MockFactory.TokenParser(new Token[]
            {
                new SectionHeaderToken {Name = "Foo"},
                new OptionToken
                {
                    Name = "foo",
                    Value = "bar"
                },
                new SectionHeaderToken {Name = "Foo"}
            });

            var builder = new IniFileConfigBuilder(parser);

            Action build = () => { builder.Build(buildMode: BuildMode.Strict); };

            build.ShouldThrow<IniConfigException>()
                .And.Errors.First()
                .Should()
                .BeOfType<DuplicateSectionError>();
            builder.Ok.Should().BeFalse();
        }

        [Test]
        public void InvalidIdentifierShouldRaiseError()
        {
            var parser = MockFactory.TokenParser(new Token[]
            {
                new SectionHeaderToken {Name = "Foo"},
                new OptionToken
                {
                    Name = "foo!",
                    Value = "bar"
                }
            });

            var builder = new IniFileConfigBuilder(parser);

            Action build = () => { builder.Build(buildMode: BuildMode.Strict); };

            build
                .ShouldThrow<IniConfigException>()
                .And.Errors.First().Should().BeOfType<InvalidIdentifierError>();
            builder.Ok.Should().BeFalse();
        }

        [Test]
        public void NoSectionShouldRaiseError()
        {
            var parser = MockFactory.TokenParser(new Token[]
            {
                new OptionToken
                {
                    Name = "foo",
                    Value = "bar"
                },
                new SectionHeaderToken {Name = "Foo"}
            });

            var builder = new IniFileConfigBuilder(parser);

            Action build = () => { builder.Build(buildMode: BuildMode.Strict); };

            build.ShouldThrow<IniConfigException>();
            builder.Ok.Should().BeFalse();
            builder.Errors.First().Should().BeOfType<NoSectionError>();
        }

        [Test]
        public void RelaxedBuildModeShouldNotThrow()
        {
            var parser = MockFactory.TokenParser(new Token[]
            {
                new SectionHeaderToken {Name = "Foo"},
                new SectionHeaderToken {Name = "Foo"}
            });

            var builder = new IniFileConfigBuilder(parser);
            builder.Build();

            builder.Ok.Should().BeFalse();
            builder.Errors.First().Should().BeOfType<DuplicateSectionError>();
        }

        [Test]
        public void StrictBuildModeShouldNotThrow()
        {
            var parser = MockFactory.TokenParser(new Token[]
            {
                new OptionToken
                {
                    Name = "foo",
                    Value = "bar"
                }
            });

            var builder = new IniFileConfigBuilder(parser);
            Action build = () => { builder.Build(buildMode: BuildMode.Strict); };

            build.ShouldThrow<IniConfigException>();

            builder.Ok.Should().BeFalse();
            builder.Errors.First().Should().BeOfType<NoSectionError>();
        }
    }
}