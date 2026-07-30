using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066B RID: 1643
	internal abstract class SimpleBuildProvider : GenericBuildProvider<SimpleWebHandlerParser>
	{
		// Token: 0x06004639 RID: 17977 RVA: 0x000C15D4 File Offset: 0x000BF7D4
		protected override SimpleWebHandlerParser Parse()
		{
			SimpleWebHandlerParser parser = base.Parser;
			if (this._parsed)
			{
				return parser;
			}
			this._parsed = true;
			return parser;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x000C15FC File Offset: 0x000BF7FC
		protected override void GenerateCode(AssemblyBuilder assemblyBuilder, SimpleWebHandlerParser parser, BaseCompiler compiler)
		{
			if (assemblyBuilder == null || parser == null)
			{
				return;
			}
			string text = parser.Program.Trim();
			if (string.IsNullOrEmpty(text))
			{
				this._needLoadFromBin = true;
				return;
			}
			this._needLoadFromBin = false;
			using (TextWriter textWriter = assemblyBuilder.CreateCodeFile(this))
			{
				textWriter.WriteLine(text);
			}
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x000C1660 File Offset: 0x000BF860
		protected override Type LoadTypeFromBin(BaseCompiler compiler, SimpleWebHandlerParser parser)
		{
			return parser.GetTypeFromBin(parser.ClassName);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x000C166E File Offset: 0x000BF86E
		protected override string GetClassType(BaseCompiler compiler, SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.ClassName;
			}
			return null;
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x000C167B File Offset: 0x000BF87B
		protected override ICollection GetParserDependencies(SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.Dependencies;
			}
			return null;
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x000C1688 File Offset: 0x000BF888
		protected override string GetParserLanguage(SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.Language;
			}
			return null;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected override string GetCodeBehindSource(SimpleWebHandlerParser parser)
		{
			return null;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected override AspGenerator CreateAspGenerator(SimpleWebHandlerParser parser)
		{
			return null;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000C1695 File Offset: 0x000BF895
		protected override BaseCompiler CreateCompiler(SimpleWebHandlerParser parser)
		{
			return new WebServiceCompiler(parser);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x000C16A0 File Offset: 0x000BF8A0
		protected override List<string> GetReferencedAssemblies(SimpleWebHandlerParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			ArrayList assemblies = parser.Assemblies;
			if (assemblies == null || assemblies.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (object obj in assemblies)
			{
				string text = obj as string;
				if (text != null && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool NeedsConstructType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000C1724 File Offset: 0x000BF924
		protected override bool NeedsLoadFromBin
		{
			get
			{
				return this._needLoadFromBin;
			}
		}

		// Token: 0x0400252F RID: 9519
		private bool _parsed;

		// Token: 0x04002530 RID: 9520
		private bool _needLoadFromBin;
	}
}
