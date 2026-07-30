using System;

namespace System.Data.Common
{
	// Token: 0x0200038D RID: 909
	internal class DbProviderFactoryConfigSection
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x000BE39C File Offset: 0x000BC59C
		public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
		{
			try
			{
				this.factType = FactoryType;
				this.name = FactoryName;
				this.invariantName = this.factType.Namespace.ToString();
				this.description = FactoryDescription;
				this.assemblyQualifiedName = this.factType.AssemblyQualifiedName.ToString();
			}
			catch
			{
				this.factType = null;
				this.name = string.Empty;
				this.invariantName = string.Empty;
				this.description = string.Empty;
				this.assemblyQualifiedName = string.Empty;
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000BE438 File Offset: 0x000BC638
		public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
		{
			this.factType = null;
			this.name = FactoryName;
			this.invariantName = FactoryInvariantName;
			this.description = FactoryDescription;
			this.assemblyQualifiedName = FactoryAssemblyQualifiedName;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000BE464 File Offset: 0x000BC664
		public bool IsNull()
		{
			return this.factType == null && this.invariantName == string.Empty;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000BE489 File Offset: 0x000BC689
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000BE491 File Offset: 0x000BC691
		public string InvariantName
		{
			get
			{
				return this.invariantName;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000BE499 File Offset: 0x000BC699
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000BE4A1 File Offset: 0x000BC6A1
		public string AssemblyQualifiedName
		{
			get
			{
				return this.assemblyQualifiedName;
			}
		}

		// Token: 0x04001A07 RID: 6663
		private Type factType;

		// Token: 0x04001A08 RID: 6664
		private string name;

		// Token: 0x04001A09 RID: 6665
		private string invariantName;

		// Token: 0x04001A0A RID: 6666
		private string description;

		// Token: 0x04001A0B RID: 6667
		private string assemblyQualifiedName;
	}
}
