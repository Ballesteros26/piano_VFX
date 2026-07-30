using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x02000243 RID: 579
	internal class UserControlParser : TemplateControlParser
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x00040B0B File Offset: 0x0003ED0B
		internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: this(virtualPath, inputFile, context, null)
		{
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00040B17 File Offset: 0x0003ED17
		internal UserControlParser(VirtualPath virtualPath, string inputFile, List<string> deps, HttpContext context)
			: this(virtualPath, inputFile, context, null)
		{
			base.Dependencies = deps;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00040B2B File Offset: 0x0003ED2B
		internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context, string type)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.BaseVirtualDir = virtualPath.DirectoryNoNormalize;
			base.InputFile = inputFile;
			base.SetBaseType(type);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00040B68 File Offset: 0x0003ED68
		internal UserControlParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00040B74 File Offset: 0x0003ED74
		internal UserControlParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.BaseVirtualDir = virtualPath.DirectoryNoNormalize;
			if (string.IsNullOrEmpty(inputFile))
			{
				base.InputFile = virtualPath.PhysicalPath;
			}
			else
			{
				base.InputFile = inputFile;
			}
			this.Reader = reader;
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00040BDC File Offset: 0x0003EDDC
		internal UserControlParser(TextReader reader, int? uniqueSuffix, HttpContext context)
		{
			base.Context = context;
			string filePath = context.Request.FilePath;
			base.VirtualPath = new VirtualPath(filePath);
			this.BaseVirtualDir = VirtualPathUtility.GetDirectory(filePath, false);
			base.InputFile = VirtualPathUtility.GetFileName(filePath) + "#" + ((uniqueSuffix != null) ? uniqueSuffix.Value.ToString("x") : "0");
			this.Reader = reader;
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00040C6F File Offset: 0x0003EE6F
		internal static Type GetCompiledType(TextReader reader, int? inputHashCode, HttpContext context)
		{
			return new UserControlParser(reader, inputHashCode, context).CompileIntoType();
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00040C7E File Offset: 0x0003EE7E
		internal static Type GetCompiledType(string virtualPath, string inputFile, List<string> deps, HttpContext context)
		{
			return new UserControlParser(new VirtualPath(virtualPath), inputFile, deps, context).CompileIntoType();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00040C93 File Offset: 0x0003EE93
		public static Type GetCompiledType(string virtualPath, string inputFile, HttpContext context)
		{
			return new UserControlParser(new VirtualPath(virtualPath), inputFile, context).CompileIntoType();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0003B1E5 File Offset: 0x000393E5
		internal override Type CompileIntoType()
		{
			return new AspGenerator(this).GetCompiledType();
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00040CA7 File Offset: 0x0003EEA7
		internal override void ProcessMainAttributes(IDictionary atts)
		{
			this.masterPage = BaseParser.GetString(atts, "MasterPageFile", null);
			if (this.masterPage != null)
			{
				this.AddDependency(this.masterPage);
			}
			base.ProcessMainAttributes(atts);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00040CD6 File Offset: 0x0003EED6
		internal override void ProcessOutputCacheAttributes(IDictionary atts)
		{
			this.providerName = BaseParser.GetString(atts, "ProviderName", null);
			base.ProcessOutputCacheAttributes(atts);
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		internal override Type DefaultBaseType
		{
			get
			{
				Type defaultUserControlBaseType = PageParser.DefaultUserControlBaseType;
				if (defaultUserControlBaseType == null)
				{
					return base.DefaultBaseType;
				}
				return defaultUserControlBaseType;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00040D18 File Offset: 0x0003EF18
		internal override string DefaultBaseTypeName
		{
			get
			{
				return base.PagesConfig.UserControlBaseType;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00040D25 File Offset: 0x0003EF25
		internal override string DefaultDirectiveName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00040D2C File Offset: 0x0003EF2C
		internal string MasterPageFile
		{
			get
			{
				return this.masterPage;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00040D34 File Offset: 0x0003EF34
		internal string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x040015FE RID: 5630
		private string masterPage;

		// Token: 0x040015FF RID: 5631
		private string providerName;
	}
}
