using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007BA RID: 1978
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal sealed class CompilerCollection : ConfigurationElementCollection
	{
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000E0418 File Offset: 0x000DE618
		static CompilerCollection()
		{
			CompilerInfo compilerInfo = new CompilerInfo(null, "Microsoft.CSharp.CSharpCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[] { ".cs" }, new string[] { "c#", "cs", "csharp" });
			compilerInfo.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo);
			CompilerInfo compilerInfo2 = new CompilerInfo(null, "Microsoft.VisualBasic.VBCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[] { ".vb" }, new string[] { "vb", "vbs", "visualbasic", "vbscript" });
			compilerInfo2.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo2);
			CompilerInfo compilerInfo3 = new CompilerInfo(null, "Microsoft.JScript.JScriptCodeProvider, Microsoft.JScript, Version=8.0.1100.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", new string[] { ".js" }, new string[] { "js", "jscript", "javascript" });
			compilerInfo3.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo3);
			CompilerInfo compilerInfo4 = new CompilerInfo(null, "Microsoft.VJSharp.VJSharpCodeProvider, VJSharpCodeProvider, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", new string[] { ".jsl", ".java" }, new string[] { "vj#", "vjs", "vjsharp" });
			compilerInfo4.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo4);
			CompilerInfo compilerInfo5 = new CompilerInfo(null, "Microsoft.VisualC.CppCodeProvider, CppCodeProvider, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", new string[] { ".h" }, new string[] { "c++", "mc", "cpp" });
			compilerInfo5.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo5);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000E060C File Offset: 0x000DE80C
		private static void AddCompilerInfo(CompilerInfo ci)
		{
			ci.CreateProvider();
			CompilerCollection.compiler_infos.Add(ci);
			string[] languages = ci.GetLanguages();
			if (languages != null)
			{
				foreach (string text in languages)
				{
					CompilerCollection.compiler_languages[text] = ci;
				}
			}
			string[] extensions = ci.GetExtensions();
			if (extensions != null)
			{
				foreach (string text2 in extensions)
				{
					CompilerCollection.compiler_extensions[text2] = ci;
				}
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000E0684 File Offset: 0x000DE884
		private static void AddCompilerInfo(Compiler compiler)
		{
			CompilerCollection.AddCompilerInfo(new CompilerInfo(null, compiler.Type, new string[] { compiler.Extension }, new string[] { compiler.Language })
			{
				CompilerParams = 
				{
					CompilerOptions = compiler.CompilerOptions,
					WarningLevel = compiler.WarningLevel
				}
			});
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000E06E4 File Offset: 0x000DE8E4
		protected override void BaseAdd(ConfigurationElement element)
		{
			Compiler compiler = element as Compiler;
			if (compiler != null)
			{
				CompilerCollection.AddCompilerInfo(compiler);
			}
			base.BaseAdd(element);
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x00004240 File Offset: 0x00002440
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000E0708 File Offset: 0x000DE908
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000E0710 File Offset: 0x000DE910
		public CompilerInfo GetCompilerInfoForLanguage(string language)
		{
			if (CompilerCollection.compiler_languages.Count == 0)
			{
				return null;
			}
			CompilerInfo compilerInfo;
			if (CompilerCollection.compiler_languages.TryGetValue(language, out compilerInfo))
			{
				return compilerInfo;
			}
			return null;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000E0740 File Offset: 0x000DE940
		public CompilerInfo GetCompilerInfoForExtension(string extension)
		{
			if (CompilerCollection.compiler_extensions.Count == 0)
			{
				return null;
			}
			CompilerInfo compilerInfo;
			if (CompilerCollection.compiler_extensions.TryGetValue(extension, out compilerInfo))
			{
				return compilerInfo;
			}
			return null;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000E0770 File Offset: 0x000DE970
		public string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtension = this.GetCompilerInfoForExtension(extension);
			if (compilerInfoForExtension == null)
			{
				return null;
			}
			string[] languages = compilerInfoForExtension.GetLanguages();
			if (languages != null && languages.Length != 0)
			{
				return languages[0];
			}
			return null;
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000E079D File Offset: 0x000DE99D
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000E07AB File Offset: 0x000DE9AB
		public Compiler Get(string language)
		{
			return (Compiler)base.BaseGet(language);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000E07B9 File Offset: 0x000DE9B9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000E07C6 File Offset: 0x000DE9C6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000E07D4 File Offset: 0x000DE9D4
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[CompilerCollection.compiler_infos.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = string.Join(";", CompilerCollection.compiler_infos[i].GetLanguages());
				}
				return array;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x00004240 File Offset: 0x00002440
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000E0820 File Offset: 0x000DEA20
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x000E0827 File Offset: 0x000DEA27
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection.properties;
			}
		}

		// Token: 0x17000F66 RID: 3942
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		// Token: 0x17000F67 RID: 3943
		public CompilerInfo this[string language]
		{
			get
			{
				return this.GetCompilerInfoForLanguage(language);
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x000E0837 File Offset: 0x000DEA37
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				return CompilerCollection.compiler_infos.ToArray();
			}
		}

		// Token: 0x04002E88 RID: 11912
		private static readonly string defaultCompilerVersion = "3.5";

		// Token: 0x04002E89 RID: 11913
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002E8A RID: 11914
		private static List<CompilerInfo> compiler_infos = new List<CompilerInfo>();

		// Token: 0x04002E8B RID: 11915
		private static Dictionary<string, CompilerInfo> compiler_languages = new Dictionary<string, CompilerInfo>(16, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002E8C RID: 11916
		private static Dictionary<string, CompilerInfo> compiler_extensions = new Dictionary<string, CompilerInfo>(6, StringComparer.OrdinalIgnoreCase);
	}
}
