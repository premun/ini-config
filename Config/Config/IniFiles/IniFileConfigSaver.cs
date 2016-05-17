using System.IO;
using System.Linq;
using Config.Options;

namespace Config.IniFiles
{
	/// <summary>
	/// Example stub of an object that will store config into an ini file.
	/// </summary>
    internal class IniFileConfigSaver : IConfigSaver
	{
		private readonly string _path;

		private Verbosity _verbosity;

		private TextWriter _writer;

		public IniFileConfigSaver(string path = null)
		{
			_path = path;
		}

        /// <summary>
        /// Saves given IConfig data. Target is dependent on implementation (ini file, database...).
        /// </summary>
        /// <param name="config">Config data object</param>
        /// <param name="verbosity">Verbosity of saver</param>
		public void SaveConfig(IConfig config, Verbosity verbosity)
		{
			using (var writer = new StreamWriter(_path))
			{
				SaveConfig(writer, config, verbosity);
			}
		}

        /// <summary>
        /// Saves all section of the configuration to the given writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="verbosity">The verbosity.</param>
		public void SaveConfig(TextWriter writer, IConfig config, Verbosity verbosity)
		{
			_verbosity = verbosity;
			_writer = writer;

			config.Sections.ToList().ForEach(WriteSection);
		}

		private void WriteSection(IConfigSection section)
		{
			_writer.WriteLine("[" + section.Name + "]");

			if (CommentsFlag && !string.IsNullOrEmpty(section.Comment))
			{
				_writer.WriteLine("; " + section.Comment);
			}

			foreach (var key in section.Keys(DefaultsFlag))
			{
				WriteSection(key, section[key]);
			}

			_writer.WriteLine();
		}

		private void WriteSection(string key, Option option)
		{
			_writer.Write("{0} = {1}", key, option.Serialize());

			if (CommentsFlag && !string.IsNullOrEmpty(option.Comment))
			{
				_writer.WriteLine("\t; " + option.Comment);
			}

			_writer.WriteLine();
		}

		private bool CommentsFlag
		{
			get { return (_verbosity & Verbosity.Comments) == Verbosity.Comments; }
		}

		private bool DefaultsFlag
		{
			get { return (_verbosity & Verbosity.Defaults) == Verbosity.Defaults; }
		}
	}
}