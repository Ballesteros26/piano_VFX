using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020001A1 RID: 417
	internal sealed class ApplicationFileParser : TemplateParser
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x0002BADF File Offset: 0x00029CDF
		public ApplicationFileParser(string fname, HttpContext context)
		{
			base.InputFile = fname;
			base.Context = context;
			base.VirtualPath = new VirtualPath("/" + Path.GetFileName(fname));
			this.LoadConfigDefaults();
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002BB16 File Offset: 0x00029D16
		internal ApplicationFileParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002BB24 File Offset: 0x00029D24
		internal ApplicationFileParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.Reader = reader;
			if (string.IsNullOrEmpty(inputFile))
			{
				base.InputFile = virtualPath.PhysicalPath;
			}
			else
			{
				base.InputFile = inputFile;
			}
			base.SetBaseType(null);
			this.LoadConfigDefaults();
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002BB77 File Offset: 0x00029D77
		internal override Type CompileIntoType()
		{
			return GlobalAsaxCompiler.CompileApplicationType(this);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002BB80 File Offset: 0x00029D80
		internal static Type GetCompiledApplicationType(string inputFile, HttpContext context)
		{
			ApplicationFileParser applicationFileParser = new ApplicationFileParser(inputFile, context);
			Type compiledType = new AspGenerator(applicationFileParser).GetCompiledType();
			ApplicationFileParser.dependencies = applicationFileParser.Dependencies;
			return compiledType;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002BBAC File Offset: 0x00029DAC
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare(directive, "application", true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Import", true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Assembly", true, Helpers.InvariantCulture) != 0)
			{
				base.ThrowParseException("Invalid directive: " + directive, Array.Empty<object>());
			}
			base.AddDirective(directive, atts);
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0002BC10 File Offset: 0x00029E10
		internal static List<string> FileDependencies
		{
			get
			{
				return ApplicationFileParser.dependencies;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0002BC18 File Offset: 0x00029E18
		internal override Type DefaultBaseType
		{
			get
			{
				Type defaultApplicationBaseType = PageParser.DefaultApplicationBaseType;
				if (defaultApplicationBaseType == null)
				{
					return base.DefaultBaseType;
				}
				return defaultApplicationBaseType;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0002BC3C File Offset: 0x00029E3C
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.HttpApplication";
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0002BC43 File Offset: 0x00029E43
		internal override string DefaultDirectiveName
		{
			get
			{
				return "application";
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0002BC4A File Offset: 0x00029E4A
		internal override string BaseVirtualDir
		{
			get
			{
				return base.Context.Request.ApplicationPath;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0002BC5C File Offset: 0x00029E5C
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0002BC64 File Offset: 0x00029E64
		internal override TextReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		// Token: 0x04001347 RID: 4935
		private static List<string> dependencies;

		// Token: 0x04001348 RID: 4936
		private TextReader reader;
	}
}
