# Ini Config

School assignment project - configuration library written in C#

Supports following features:
- Config sections, each section has options (key/value tuples)
- Scalar types (string, int, float, long, bool, enumeration, references)
- Lists of scalar values
- Value constraints and constraint checking, conf error reporting
- Expected options specification and format checking (required sections/options, default values...)

# Ini file format

The library can load/save .ini text files with configuration. The format of the ini file is following:
```
[Section name]
# section comment
[key] = [value] # option comment
```
An example:
```
[MySQL]
# Connection string
host = mydb.example.com
port = 3306
username = root
password = ${Passwords#mysql} # A reference
use_https = true
allowedIPs = 10.0.0.138, 192.168.0.1

[Passwords]
mysql = anfvoqerO3208fuasnd
```

# Examples

## Specify required fields

```c#
var formatSpecifier = new ConfigFormatSpecifier()
	.AddSection("Server", true)
		.AddOption(new StringOptionSpecifier("hostname", true))
		.AddOption(new ConstraintOptionSpecifier<int>("port", x => x > 0 && x < 65536, defaultValue: 3306))
		.AddOption(new EnumOptionSpecifier<Domains>("domain", defaultValue: Domains.Eu))
	.AddSection("HTTP", true)
		.AddOption(new IntOptionSpecifier("timeout", defaultValue: 5000))
		.AddOption(new BoolOptionSpecifier("use_https"))
	.FinishDefinition();
```
## Read config from file

```c#
IConfig config;

try {
	// Notice the IniConfig class that adds the ini file representation
	// Representation is not bound to basic implementation, more sources can be added easily
	config = IniConfig.FromFile("/www/mywebsite/config.ini", FormatSpecifier);
}
catch (IniConfigException e)
{
	Console.WriteLine("Cannot build config. Encountered following errors:");

	e.Errors
		.ToList()
		.ForEach(error => Console.WriteLine("  Line " + error.Line + ": " + error.Message));

	Console.WriteLine();

	throw;
}
```

## Use the config object
```c#
// Get config values
// Either directly
string hostname = config["MySQL"]["hostname"].String;

// Or through section
var mysqlSection = config["MySQL"];
int port = mysqlSection["port"].Int;

// Set new values (notice method chaining)
config["MySQL"]
	.Set("foo", 3.14f)
	.Set("bar", 0x45);

// Set new values using indexer 
// (notice auto-boxing to BoolOption or ListOption<string>)
config["MySQL"]["persistent"] = true;
config["MySQL"]["allowedIPs"] = new[] { "147.54.32.148", "10.12.45.188" };

// Get specific section that was not required
var httpSection = config["HTTP"];
if (httpSection != null)
{
	bool secure = httpSection["use_https"].Bool;
	int timeout = httpSection["timeout"].Int;
}

// Add new section
var newSection = config.AddSection("New section");

// Add new values
newSection
	.Set("foo", 123)
	.Set("bar", false);

config.SaveToFile(Verbosity.Defaults & Verbosity.Comments);
```
