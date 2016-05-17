using System;
using System.Collections.Generic;
using System.Linq;
using Config.ConfigExceptions;
using Config.Format;
using Config.Format.OptionSpecifiers;
using Config.IniFiles;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.IniFiles
{
    [TestFixture]
    public class WhenParsingValueTypes
    {
        private enum Colors
        {
            Black,
            White,
            Green
        }

        private static ITokenParser GetTokenParser(string configData)
        {
            var parser = MockFactory.TokenParser(configData);
            IList<Token> tokens = new List<Token>();
            Token token;
            while ((token = parser.GetNextToken()) != null)
            {
                tokens.Add(token);
            }

            return MockFactory.TokenParser(tokens);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void AssignmentInsideReferenceShouldRaiseException()
        {
            const string configData = @"
[Scalars]
bool = f ; bool commentary
float = 1.2
enum = Black
int=100
signed=60
string=  bar ;xyz
unsigned = 70
[Refs]
reference=    ${Scalars#int}";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new BoolOptionSpecifier("bool", defaultValue: true))
                .AddOption(new FloatOptionSpecifier("float", defaultValue: 0.75f))
                .AddOption(new EnumOptionSpecifier<Colors>("enum",
                    defaultValue: Colors.Green))
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new StringOptionSpecifier("string",
                    defaultValue: "foo"))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50L))
                .AddSection("Refs")
                .AddOption(new BoolOptionSpecifier("reference"))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(2);

            config["Refs"]["reference"] = true;
        }

        [Test]
        public void FormatBinaryShoulWorks()
        {
            const string configData = @"
[Scalars]
int= 0b00001100
signed=0b00001100
unsigned = 0b00001100";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);


            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50UL))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config["Scalars"]["signed"].Signed.ShouldBeEquivalentTo(12);
            config["Scalars"]["unsigned"].Unsigned.ShouldBeEquivalentTo(12);
            config["Scalars"]["int"].Int.ShouldBeEquivalentTo(12);
        }

        [Test]
        public void FormatHexadecimalShoulWorks()
        {
            const string configData = @"
[Scalars]
int=0x80C1
signed=0x80C1
unsigned = 0x80C1";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50L))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config["Scalars"]["signed"].Signed.ShouldBeEquivalentTo(32961);
            config["Scalars"]["unsigned"].Unsigned.ShouldBeEquivalentTo(32961);
            config["Scalars"]["int"].Int.ShouldBeEquivalentTo(32961);
        }

        [Test]
        public void FormatOctalShoulWorks()
        {
            const string configData = @"
[Scalars]
int=014
signed=014
unsigned = 014";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);


            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50UL))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config["Scalars"]["signed"].Signed.ShouldBeEquivalentTo(12);
            config["Scalars"]["unsigned"].Unsigned.ShouldBeEquivalentTo(12);
            config["Scalars"]["int"].Int.ShouldBeEquivalentTo(12);
        }

        [Test]
        public void FormatZeroShoulWorks()
        {
            const string configData = @"
[Scalars]
int = 0
signed=0
unsigned = 0";

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50UL))
                .FinishDefinition();

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);
            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config["Scalars"]["signed"].Signed.ShouldBeEquivalentTo(0);
            config["Scalars"]["unsigned"].Unsigned.ShouldBeEquivalentTo(0);
            config["Scalars"]["int"].Int.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void InvalidStringFormatShoulNotWorks()
        {
            const string configData = @"
[Scalars]
invalidString = this,is:invalidString
";

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new StringOptionSpecifier("invalidString"))
                .FinishDefinition();

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            Action build = () => { builder.Build(formatSpecifier); };

            build.ShouldThrow<ConfigException>();

            builder.Ok.Should().BeFalse();
        }

        [Test]
        [ExpectedException(typeof (MissingReferencedException))]
        public void MissingReferenceShouldRaiseException()
        {
            const string configData = @"
[Scalars]
bool = f ; bool commentary
[Refs]
reference=    ${Scalars#int}";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new BoolOptionSpecifier("bool"))
                .AddSection("Refs")
                .AddOption(new BoolOptionSpecifier("reference"))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            var x = config["Refs"]["reference"].Int;
        }

        [Test]
        public void ParsingConstraintsShouldWork()
        {
            const string configData = @"
[Constraints]
int = 40
enum = Green 
";

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Constraints")
                .AddOption(new ConstraintOptionSpecifier<int>("int",
                    x => 0 < x && x < 50, defaultValue: 1))
                .AddOption(new ConstraintOptionSpecifier<Colors>("enum",
                    x => x != Colors.Black, defaultValue: Colors.White))
                .FinishDefinition();

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);
            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(1);
            var section = config["Constraints"];

            section["int"].Int.ShouldBeEquivalentTo(40);
            ((Colors) section["enum"].Data).ShouldBeEquivalentTo(Colors.Green);
        }

        [Test]
        public void ParsingListsShouldWork()
        {
            const string configData = @"
[Lists]
ints = 40, 50
floats = 1.2,3.5
longs = 1:2:3
colors = Green, Black, Black
";
            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Lists")
                .AddOption(new ListOptionSpecifier<int>("ints",
                    defaultValue: new[] {6, 9, 42}))
                .AddOption(new ListOptionSpecifier<float>("floats",
                    defaultValue: new[] {6f, 9f, 42f}))
                .AddOption(new ListOptionSpecifier<long>("longs",
                    defaultValue: new[] {6L, 9L, 42L}))
                .AddOption(new ListOptionSpecifier<Colors>("colors",
                    defaultValue: new[] {Colors.Black}))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(1);
            var section = config["Lists"];

            var ints = section["ints"].IntList;
            ints.ElementAt(0).ShouldBeEquivalentTo(40);
            ints.ElementAt(1).ShouldBeEquivalentTo(50);
        }

        [Test]
        public void ParsingListsShouldWork2()
        {
            const string configData = @"
[Lists]
strings = foo, bar\;, xy\,z
";
            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Lists")
                .AddOption(new ListOptionSpecifier<string>("strings"))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(1);
            var section = config["Lists"];

            var strings = section["strings"].StringList;
            strings.ElementAt(0).ShouldBeEquivalentTo("foo");
            strings.ElementAt(1).ShouldBeEquivalentTo("bar;");
            strings.ElementAt(2).ShouldBeEquivalentTo("xy,z");
        }

        [Test]
        public void ParsingListsShouldWorkDelimiters()
        {
            const string configData = @"
[Lists]
strings = foo, bar\;\: xy\,z
";
            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Lists")
                .AddOption(new ListOptionSpecifier<string>("strings"))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(1);
            var section = config["Lists"];

            var strings = section["strings"].StringList;
            strings.ElementAt(0).ShouldBeEquivalentTo("foo");
            strings.ElementAt(1).ShouldBeEquivalentTo("bar;: xy,z");
        }

        [Test]
        public void ParsingReferenceShouldWork()
        {
            const string configData = @"
[Scalars]
bool = f ; bool commentary
float = 1.2
enum = Black
int=100
signed=60
string=  bar ;xyz
unsigned = 70
[Refs]
reference=    ${Scalars#int}";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new BoolOptionSpecifier("bool", defaultValue: true))
                .AddOption(new FloatOptionSpecifier("float", defaultValue: 0.75f))
                .AddOption(new EnumOptionSpecifier<Colors>("enum",
                    defaultValue: Colors.Green))
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new StringOptionSpecifier("string",
                    defaultValue: "foo"))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50L))
                .AddSection("Refs")
                .AddOption(new BoolOptionSpecifier("reference"))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(2);

            var result = config["Refs"]["reference"].Int;
            result.ShouldBeEquivalentTo(100);
        }

        [Test]
        public void ParsingScalarsShouldWork()
        {
            const string configData = @"
[Scalars]
bool = f ; bool commentary
float = 1.2
enum = Black
int=100
signed=60
string=  bar\  ;xyz
unsigned = 70 
";
            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Scalars")
                .AddOption(new BoolOptionSpecifier("bool", defaultValue: true))
                .AddOption(new FloatOptionSpecifier("float", defaultValue: 0.75f))
                .AddOption(new EnumOptionSpecifier<Colors>("enum",
                    defaultValue: Colors.Green))
                .AddOption(new IntOptionSpecifier("int", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
                .AddOption(new StringOptionSpecifier("string",
                    defaultValue: "foo"))
                .AddOption(new UnsignedOptionSpecifier("unsigned",
                    defaultValue: 50L))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            config.Sections.Count().ShouldBeEquivalentTo(1);
            var section = config["Scalars"];

            section["bool"].Bool.ShouldBeEquivalentTo(false);
            section["float"].Float.ShouldBeEquivalentTo(1.2f);
            ((Colors) section["enum"].Data).ShouldBeEquivalentTo(Colors.Black);
            section["int"].Int.ShouldBeEquivalentTo(100);
            section["signed"].Signed.ShouldBeEquivalentTo(60L);
            section["string"].String.ShouldBeEquivalentTo("bar ");
            section["unsigned"].Unsigned.ShouldBeEquivalentTo(70L);
        }

        [Test]
        [ExpectedException(typeof (ReferenceCycleException))]
        public void ReferenceCycleShouldRaiseException()
        {
            const string configData = @"
[Refs]
ref1 = ${Refs#ref2};
ref2 = ${Refs#ref3};
ref3 = ${Refs#ref1}";

            var parser = GetTokenParser(configData);
            var builder = new IniFileConfigBuilder(parser);

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Refs")
                .AddOption(new IntOptionSpecifier("ref1", defaultValue: 4))
                .AddOption(new SignedOptionSpecifier("ref2", defaultValue: 40L))
                .AddOption(new UnsignedOptionSpecifier("ref3", defaultValue: 50L))
                .FinishDefinition();

            var config = builder.Build(formatSpecifier);

            builder.Ok.Should().BeTrue();

            var x = config["Refs"]["ref3"].String;
            Console.WriteLine(x);
        }
    }
}