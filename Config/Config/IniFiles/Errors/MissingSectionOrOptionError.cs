using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.Format;

namespace Config.IniFiles.Errors
{
    public class MissingSectionOrOptionError : FormatError
    {
        public IEnumerable<ConfigException> ConfigExceptions { get; } 

        public MissingSectionOrOptionError(ConfigFormatException configException) 
            : base(
                  string.Format(
                      "Config does not contain all requred members. See exception errirs `{1}` and message: `{0}`", 
                      configException,
                      string.Join(",", configException.ErrorList)), 
                  -1)
        {
            ConfigExceptions = configException.ErrorList;
        }
    }
}