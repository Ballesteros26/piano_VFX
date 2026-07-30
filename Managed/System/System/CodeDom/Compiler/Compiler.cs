using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007B9 RID: 1977
	internal sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06003FC3 RID: 16323 RVA: 0x000E01D4 File Offset: 0x000DE3D4
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
			Compiler.properties.Add(Compiler.providerOptionsProp);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		internal Compiler()
		{
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000E0312 File Offset: 0x000DE512
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000E033F File Offset: 0x000DE53F
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x000E0351 File Offset: 0x000DE551
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler.compilerOptionsProp];
			}
			internal set
			{
				base[Compiler.compilerOptionsProp] = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000E035F File Offset: 0x000DE55F
		// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x000E0371 File Offset: 0x000DE571
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler.extensionProp];
			}
			internal set
			{
				base[Compiler.extensionProp] = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000E037F File Offset: 0x000DE57F
		// (set) Token: 0x06003FCB RID: 16331 RVA: 0x000E0391 File Offset: 0x000DE591
		[ConfigurationProperty("language", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Language
		{
			get
			{
				return (string)base[Compiler.languageProp];
			}
			internal set
			{
				base[Compiler.languageProp] = value;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x000E039F File Offset: 0x000DE59F
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x000E03B1 File Offset: 0x000DE5B1
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[Compiler.typeProp];
			}
			internal set
			{
				base[Compiler.typeProp] = value;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000E03BF File Offset: 0x000DE5BF
		// (set) Token: 0x06003FCF RID: 16335 RVA: 0x000E03D1 File Offset: 0x000DE5D1
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler.warningLevelProp];
			}
			internal set
			{
				base[Compiler.warningLevelProp] = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000E03E4 File Offset: 0x000DE5E4
		// (set) Token: 0x06003FD1 RID: 16337 RVA: 0x000E03F6 File Offset: 0x000DE5F6
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CompilerProviderOptionsCollection ProviderOptions
		{
			get
			{
				return (CompilerProviderOptionsCollection)base[Compiler.providerOptionsProp];
			}
			internal set
			{
				base[Compiler.providerOptionsProp] = value;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000E0404 File Offset: 0x000DE604
		public Dictionary<string, string> ProviderOptionsDictionary
		{
			get
			{
				return this.ProviderOptions.ProviderOptions;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000E0411 File Offset: 0x000DE611
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x04002E81 RID: 11905
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");

		// Token: 0x04002E82 RID: 11906
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "");

		// Token: 0x04002E83 RID: 11907
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002E84 RID: 11908
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002E85 RID: 11909
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x04002E86 RID: 11910
		private static ConfigurationProperty providerOptionsProp = new ConfigurationProperty("", typeof(CompilerProviderOptionsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002E87 RID: 11911
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
