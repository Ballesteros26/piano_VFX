using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000656 RID: 1622
	internal abstract class GenericBuildProvider<TParser> : BuildProvider
	{
		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x000BE9F7 File Offset: 0x000BCBF7
		protected bool Parsed
		{
			get
			{
				return this._parsed;
			}
		}

		// Token: 0x06004587 RID: 17799
		protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context);

		// Token: 0x06004588 RID: 17800
		protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context);

		// Token: 0x06004589 RID: 17801
		protected abstract BaseCompiler CreateCompiler(TParser parser);

		// Token: 0x0600458A RID: 17802
		protected abstract string GetParserLanguage(TParser parser);

		// Token: 0x0600458B RID: 17803
		protected abstract ICollection GetParserDependencies(TParser parser);

		// Token: 0x0600458C RID: 17804
		protected abstract string GetCodeBehindSource(TParser parser);

		// Token: 0x0600458D RID: 17805
		protected abstract string GetClassType(BaseCompiler compiler, TParser parser);

		// Token: 0x0600458E RID: 17806
		protected abstract AspGenerator CreateAspGenerator(TParser parser);

		// Token: 0x0600458F RID: 17807
		protected abstract List<string> GetReferencedAssemblies(TParser parser);

		// Token: 0x06004590 RID: 17808 RVA: 0x000BEA00 File Offset: 0x000BCC00
		protected virtual string MapPath(VirtualPath virtualPath)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest != null)
			{
				return httpRequest.MapPath(base.VirtualPath);
			}
			return null;
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000BEA34 File Offset: 0x000BCC34
		protected virtual TParser Parse()
		{
			TParser parser = this.Parser;
			if (this._parsed)
			{
				return parser;
			}
			if (!this.IsDirectoryBuilder)
			{
				AspGenerator aspGenerator = this.CreateAspGenerator(parser);
				if (this._reader != null)
				{
					aspGenerator.Parse(this._reader, this.MapPath(base.VirtualPathInternal), true);
				}
				else
				{
					aspGenerator.Parse();
				}
			}
			this._parsed = true;
			return parser;
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OverrideAssemblyPrefix(TParser parser, AssemblyBuilder assemblyBuilder)
		{
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x000BEA94 File Offset: 0x000BCC94
		internal override void GenerateCode()
		{
			TParser tparser = this.Parse();
			this._compiler = this.CreateCompiler(tparser);
			if (this.NeedsConstructType)
			{
				this._compiler.ConstructType();
			}
			this._codeGenerated = true;
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000BEAD0 File Offset: 0x000BCCD0
		protected virtual void GenerateCode(AssemblyBuilder assemblyBuilder, TParser parser, BaseCompiler compiler)
		{
			CodeCompileUnit compileUnit = this._compiler.CompileUnit;
			if (compileUnit == null)
			{
				throw new HttpException("Unable to generate source code.");
			}
			assemblyBuilder.AddCodeCompileUnit(this, compileUnit);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x000BEB00 File Offset: 0x000BCD00
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			if (!this._codeGenerated)
			{
				this.GenerateCode();
			}
			TParser tparser = this.Parse();
			this.OverrideAssemblyPrefix(tparser, assemblyBuilder);
			string codeBehindSource = this.GetCodeBehindSource(tparser);
			if (codeBehindSource != null)
			{
				assemblyBuilder.AddCodeFile(codeBehindSource, this, true);
			}
			List<string> referencedAssemblies = this.GetReferencedAssemblies(tparser);
			if (referencedAssemblies != null && referencedAssemblies.Count > 0)
			{
				foreach (string text in referencedAssemblies)
				{
					assemblyBuilder.AddAssemblyReference(text);
				}
			}
			this.GenerateCode(assemblyBuilder, tparser, this._compiler);
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual Type LoadTypeFromBin(BaseCompiler compiler, TParser parser)
		{
			return null;
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x000BEBA4 File Offset: 0x000BCDA4
		public override Type GetGeneratedType(CompilerResults results)
		{
			if (this.NeedsLoadFromBin && this._compiler != null)
			{
				return this.LoadTypeFromBin(this._compiler, this.Parser);
			}
			Type type = null;
			Assembly assembly = ((results != null) ? results.CompiledAssembly : null);
			if (assembly != null)
			{
				type = assembly.GetType(this.GetClassType(this._compiler, this.Parser));
			}
			if (type == null)
			{
				throw new HttpException(500, string.Format("Type {0} could not be loaded", this.GetClassType(this._compiler, this.Parser)));
			}
			return type;
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x000BEC36 File Offset: 0x000BCE36
		protected virtual TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
		{
			physicalPath = null;
			return base.OpenReader(virtualPath.Original);
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x000BEC48 File Offset: 0x000BCE48
		public override ICollection VirtualPathDependencies
		{
			get
			{
				TParser parser = this.Parser;
				return this.GetParserDependencies(parser);
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x000BEC64 File Offset: 0x000BCE64
		internal override string LanguageName
		{
			get
			{
				TParser tparser = this.Parse();
				if (tparser != null)
				{
					return this.GetParserLanguage(tparser);
				}
				return base.LanguageName;
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000BEC8E File Offset: 0x000BCE8E
		public override CompilerType CodeCompilerType
		{
			get
			{
				if (this._compilerType == null)
				{
					this._compilerType = base.GetDefaultCompilerTypeForLanguage(this.LanguageName);
				}
				return this._compilerType;
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x000BECB0 File Offset: 0x000BCEB0
		public TParser Parser
		{
			get
			{
				if (this._parser == null)
				{
					VirtualPath virtualPathInternal = base.VirtualPathInternal;
					if (virtualPathInternal == null)
					{
						throw new HttpException("VirtualPath not set, cannot instantiate parser.");
					}
					if (!this.IsDirectoryBuilder)
					{
						string text;
						this._reader = this.SpecialOpenReader(virtualPathInternal, out text);
						this._parser = this.CreateParser(virtualPathInternal, text, this._reader, HttpContext.Current);
					}
					else
					{
						this._parser = this.CreateParser(virtualPathInternal, null, HttpContext.Current);
					}
					if (this._parser == null)
					{
						throw new HttpException("Unable to create type parser.");
					}
				}
				return this._parser;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool IsDirectoryBuilder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool NeedsConstructType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool NeedsLoadFromBin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x000BED42 File Offset: 0x000BCF42
		internal override CodeCompileUnit CodeUnit
		{
			get
			{
				if (!this._codeGenerated)
				{
					this.GenerateCode();
				}
				return this._compiler.CompileUnit;
			}
		}

		// Token: 0x040024FA RID: 9466
		private TParser _parser;

		// Token: 0x040024FB RID: 9467
		private CompilerType _compilerType;

		// Token: 0x040024FC RID: 9468
		private BaseCompiler _compiler;

		// Token: 0x040024FD RID: 9469
		private TextReader _reader;

		// Token: 0x040024FE RID: 9470
		private bool _parsed;

		// Token: 0x040024FF RID: 9471
		private bool _codeGenerated;
	}
}
