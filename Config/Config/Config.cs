﻿using System.Collections.Generic;

namespace Config
{
	public class Config : IConfig
	{
		public IConfigSection this[string sectionName]
		{
			get { throw new System.NotImplementedException(); }
		}

		public IConfigSection GetSection(string sectionName)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<IConfigSection> Sections { get; }

		public bool AddSection(IConfigSection section)
		{
			throw new System.NotImplementedException();
		}

		public IConfigSection AddSection(string name)
		{
			throw new System.NotImplementedException();
		}

		public bool RemoveSection(IConfigSection section)
		{
			throw new System.NotImplementedException();
		}

		public bool RemoveSection(string sectionName)
		{
			throw new System.NotImplementedException();
		}
	}
}