using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007B8 RID: 1976
	internal sealed class CodeDomConfigurationHandler : ConfigurationSection
	{
		// Token: 0x06003FBB RID: 16315 RVA: 0x000E0130 File Offset: 0x000DE330
		static CodeDomConfigurationHandler()
		{
			CodeDomConfigurationHandler.properties.Add(CodeDomConfigurationHandler.compilersProp);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000E017E File Offset: 0x000DE37E
		protected override void InitializeDefault()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000C4A98 File Offset: 0x000C2C98
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00002068 File Offset: 0x00000268
		protected override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000E019E File Offset: 0x000DE39E
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x000E01B0 File Offset: 0x000DE3B0
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				CompilerCollection compilerCollection = (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
				if (compilerCollection == null)
				{
					return null;
				}
				return compilerCollection.CompilerInfos;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000E01CD File Offset: 0x000DE3CD
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeDomConfigurationHandler.properties;
			}
		}

		// Token: 0x04002E7E RID: 11902
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002E7F RID: 11903
		private static ConfigurationProperty compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);

		// Token: 0x04002E80 RID: 11904
		private static CompilerCollection default_compilers = new CompilerCollection();
	}
}
