using Config.Format;
using Config.Format.OptionSpecifiers;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests
{
    [TestFixture]
    public class WhenSpecifyingDefaults
    {
        private enum Colors
        {
            Black,
            White,
            Green
        }

        [Test]
        public void BoolDefaults()
        {
            const bool defaultValue = true;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new BoolOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new BoolOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Bool.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        /// <summary>
        ///     Currently it is not possible to initialize a constraint option without a default value.
        ///     When default value not specified, a default value of that type is used.
        /// </summary>
        [Test]
        [Ignore]
        public void ConstraintDefaults()
        {
            const int defaultValue = 6;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new ConstraintOptionSpecifier<int>("default",
                    x => x < 10, defaultValue: defaultValue))
                .AddOption(new ConstraintOptionSpecifier<int>("null",
                    x => x < 10))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Int.ShouldBeEquivalentTo(defaultValue);

            // This will fail, because we cannot force generic type to be nullable in ConstraintOptionSpecifier
            // We set this to default(T), not null and then we are unable to tell
            section["null"].Should().BeNull();
        }


        /// <summary>
        ///     Same as above
        /// </summary>
        [Test]
        [Ignore]
        public void EnumDefaults()
        {
            const Colors defaultValue = Colors.Green;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new EnumOptionSpecifier<Colors>("default",
                    defaultValue: defaultValue))
                .AddOption(new EnumOptionSpecifier<Colors>("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            ((Colors) section["default"].Data).ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        [Test]
        public void FloatDefaults()
        {
            const float defaultValue = 0.4f;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new FloatOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new FloatOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Float.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        [Test]
        public void IntDefaults()
        {
            const int defaultValue = 4;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new IntOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new IntOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Int.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        [Test]
        public void ListDefaults()
        {
            var defaultValue = new[] {6, 9, 42};

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new ListOptionSpecifier<int>("default",
                    defaultValue: defaultValue))
                .AddOption(new ListOptionSpecifier<int>("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            var defs = section["default"].IntList;
            defs[0].ShouldBeEquivalentTo(defaultValue[0]);
            defs[1].ShouldBeEquivalentTo(defaultValue[1]);
            defs[2].ShouldBeEquivalentTo(defaultValue[2]);
            section["null"].Should().BeNull();
        }


        [Test]
        public void SignedDefaults()
        {
            const long defaultValue = 100L;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new SignedOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new SignedOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Signed.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        [Test]
        public void StringDefaults()
        {
            const string defaultValue = "bar";

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new StringOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new StringOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].String.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }


        [Test]
        public void UnsignedDefaults()
        {
            const ulong defaultValue = 100L;

            var formatSpecifier = new ConfigFormatSpecifier()
                .AddSection("Foo")
                .AddOption(new UnsignedOptionSpecifier("default",
                    defaultValue: defaultValue))
                .AddOption(new UnsignedOptionSpecifier("null"))
                .FinishDefinition();

            var config = new Config.Config(formatSpecifier);

            var section = config.AddSection("Foo");
            section["default"].Unsigned.ShouldBeEquivalentTo(defaultValue);
            section["null"].Should().BeNull();
        }
    }
}