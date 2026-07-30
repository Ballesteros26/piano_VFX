using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a compiler that is used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x02000594 RID: 1428
	public sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06003C77 RID: 15479 RVA: 0x000A11D8 File Offset: 0x0009F3D8
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0009F629 File Offset: 0x0009D829
		internal Compiler()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.Compiler" /> initialized to the provided values.</summary>
		/// <param name="compilerOptions">Lists additional compiler-specific options to pass during compilation.</param>
		/// <param name="extension">Provides a semicolon-separated list of file-name extensions used for dynamic code-behind files. For example, ".cs".</param>
		/// <param name="language">Provides a semicolon-separated list of languages used in dynamic compilation files. For example, "c#;cs;csharp".</param>
		/// <param name="type">Specifies a comma-separated class/assembly combination that indicates the .NET Framework class.</param>
		/// <param name="warningLevel">Specifies compiler warning levels.</param>
		// Token: 0x06003C79 RID: 15481 RVA: 0x000A12EA File Offset: 0x0009F4EA
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		/// <summary>Gets a list of compiler-specific options to use during compilation.</summary>
		/// <returns>A value specifying the compiler-specific options to use during compilation. This is not a merged set but rather overrides any previously defined values in earlier configuration entries.</returns>
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x000A1317 File Offset: 0x0009F517
		// (set) Token: 0x06003C7B RID: 15483 RVA: 0x000A1329 File Offset: 0x0009F529
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

		/// <summary>Gets a list of file-name extensions used for dynamic code-behind files. </summary>
		/// <returns>A value specifying the file-name extensions used for dynamic code-behind files, files in the code directory, and other referenced files.</returns>
		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x000A1337 File Offset: 0x0009F537
		// (set) Token: 0x06003C7D RID: 15485 RVA: 0x000A1349 File Offset: 0x0009F549
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

		/// <summary>Gets a list of programming languages to use in dynamic compilation files.</summary>
		/// <returns>A value specifying the programming languages to use in dynamic compilation files.</returns>
		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000A1357 File Offset: 0x0009F557
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x000A1369 File Offset: 0x0009F569
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

		/// <summary>Gets the compiler type name of the language provider for dynamic compilation files. </summary>
		/// <returns>A value specifying the type name of the language compiler to use in dynamic compilation files.</returns>
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000A1377 File Offset: 0x0009F577
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000A1389 File Offset: 0x0009F589
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

		/// <summary>Gets the compiler warning level.</summary>
		/// <returns>A value specifying the compiler warning level.</returns>
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000A1397 File Offset: 0x0009F597
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000A13A9 File Offset: 0x0009F5A9
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
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

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000A13BC File Offset: 0x0009F5BC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x040020C9 RID: 8393
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");

		// Token: 0x040020CA RID: 8394
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "");

		// Token: 0x040020CB RID: 8395
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020CC RID: 8396
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020CD RID: 8397
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x040020CE RID: 8398
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
