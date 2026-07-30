using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007BC RID: 1980
	[ConfigurationCollection(typeof(CompilerProviderOption), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "providerOption")]
	internal sealed class CompilerProviderOptionsCollection : ConfigurationElementCollection
	{
		// Token: 0x06003FF2 RID: 16370 RVA: 0x000E090A File Offset: 0x000DEB0A
		protected override ConfigurationElement CreateNewElement()
		{
			return new CompilerProviderOption();
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000E0911 File Offset: 0x000DEB11
		public CompilerProviderOption Get(int index)
		{
			return (CompilerProviderOption)base.BaseGet(index);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000E091F File Offset: 0x000DEB1F
		public CompilerProviderOption Get(string name)
		{
			return (CompilerProviderOption)base.BaseGet(name);
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000E092D File Offset: 0x000DEB2D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CompilerProviderOption)element).Name;
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000E07C6 File Offset: 0x000DE9C6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000E093C File Offset: 0x000DEB3C
		public string[] AllKeys
		{
			get
			{
				int count = base.Count;
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000E0973 File Offset: 0x000DEB73
		protected override string ElementName
		{
			get
			{
				return "providerOption";
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x000E097A File Offset: 0x000DEB7A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOptionsCollection.properties;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000E0984 File Offset: 0x000DEB84
		public Dictionary<string, string> ProviderOptions
		{
			get
			{
				int count = base.Count;
				if (count == 0)
				{
					return null;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(count);
				for (int i = 0; i < count; i++)
				{
					CompilerProviderOption compilerProviderOption = this[i];
					dictionary.Add(compilerProviderOption.Name, compilerProviderOption.Value);
				}
				return dictionary;
			}
		}

		// Token: 0x17000F70 RID: 3952
		public CompilerProviderOption this[int index]
		{
			get
			{
				return (CompilerProviderOption)base.BaseGet(index);
			}
		}

		// Token: 0x17000F71 RID: 3953
		public CompilerProviderOption this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					CompilerProviderOption compilerProviderOption = (CompilerProviderOption)obj;
					if (compilerProviderOption.Name == name)
					{
						return compilerProviderOption;
					}
				}
				return null;
			}
		}

		// Token: 0x04002E90 RID: 11920
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
