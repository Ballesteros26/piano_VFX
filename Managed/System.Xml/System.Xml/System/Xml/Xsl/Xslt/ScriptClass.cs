using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000589 RID: 1417
	internal class ScriptClass
	{
		// Token: 0x06003869 RID: 14441 RVA: 0x0013D1FC File Offset: 0x0013B3FC
		public ScriptClass(string ns, CompilerInfo compilerInfo)
		{
			this.ns = ns;
			this.compilerInfo = compilerInfo;
			this.refAssemblies = new StringCollection();
			this.nsImports = new StringCollection();
			this.typeDecl = new CodeTypeDeclaration(ScriptClass.GenerateUniqueClassName());
			this.refAssembliesByHref = false;
			this.scriptUris = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x0013D25A File Offset: 0x0013B45A
		private static string GenerateUniqueClassName()
		{
			return "Script" + Interlocked.Increment(ref ScriptClass.scriptClassCounter);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0013D278 File Offset: 0x0013B478
		public void AddScriptBlock(string source, string uriString, int lineNumber, Location end)
		{
			CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(source);
			string fileName = SourceLineInfo.GetFileName(uriString);
			if (lineNumber > 0)
			{
				codeSnippetTypeMember.LinePragma = new CodeLinePragma(fileName, lineNumber);
				this.scriptUris[fileName] = uriString;
			}
			this.typeDecl.Members.Add(codeSnippetTypeMember);
			this.endUri = uriString;
			this.endLoc = end;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x0013D2D2 File Offset: 0x0013B4D2
		public ISourceLineInfo EndLineInfo
		{
			get
			{
				return new SourceLineInfo(this.endUri, this.endLoc, this.endLoc);
			}
		}

		// Token: 0x04002491 RID: 9361
		public string ns;

		// Token: 0x04002492 RID: 9362
		public CompilerInfo compilerInfo;

		// Token: 0x04002493 RID: 9363
		public StringCollection refAssemblies;

		// Token: 0x04002494 RID: 9364
		public StringCollection nsImports;

		// Token: 0x04002495 RID: 9365
		public CodeTypeDeclaration typeDecl;

		// Token: 0x04002496 RID: 9366
		public bool refAssembliesByHref;

		// Token: 0x04002497 RID: 9367
		public Dictionary<string, string> scriptUris;

		// Token: 0x04002498 RID: 9368
		public string endUri;

		// Token: 0x04002499 RID: 9369
		public Location endLoc;

		// Token: 0x0400249A RID: 9370
		private static long scriptClassCounter;
	}
}
