﻿using System.Collections.Generic;
using Config.Format;
using Config.Values;

namespace Examples
{
	/// <summary>
	/// Example of how a config file format can be specified.
	/// There are two required sections (Server, HTTP) and one optional (Paths).
	/// </summary>
	public class FormatSpecification
    {
	    public readonly IFormatSpecifier FormatSpecifier = new FormatSpecifier
		{
			RequiredSections = new List<IFormatSectionSpecifier>
				{
					new FormatSectionSpecifier("Server")
					{
						RequiredOptions = new List<IFormatOption>
						{
							new FormatOption<StringConfigValue>("hostname"),
							new FormatOption<IntConfigValue>("port")
						}
					},
					new FormatSectionSpecifier("HTTP")
					{
						RequiredOptions = new List<IFormatOption>
						{
							new FormatListOption<IntConfigValue>("timeout")
						},
						OptionalOptions = new List<IFormatOption>
						{
							new FormatOption<BoolConfigValue>("use_https")
						}
					}
				},
			OptionalSections = new List<IFormatSectionSpecifier>
				{
					new FormatSectionSpecifier("Paths")
					{
						RequiredOptions = new List<IFormatOption>
						{
							new FormatOption<StringConfigValue>("jquery_js")
						},
						OptionalOptions = new List<IFormatOption>
						{
							new FormatOption<StringConfigValue>("main_css"),
							new FormatOption<StringConfigValue>("main_js")
						}
					}
				}
		};
    }
}
