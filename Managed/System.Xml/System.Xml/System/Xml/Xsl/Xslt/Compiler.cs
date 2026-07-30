using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000569 RID: 1385
	internal class Compiler
	{
		// Token: 0x0600373F RID: 14143 RVA: 0x001342D4 File Offset: 0x001324D4
		public Compiler(XsltSettings settings, bool debug, string scriptAssemblyPath)
		{
			TempFileCollection tempFileCollection = settings.TempFiles ?? new TempFileCollection();
			this.Settings = settings;
			this.IsDebug = settings.IncludeDebugInformation || debug;
			this.ScriptAssemblyPath = scriptAssemblyPath;
			this.CompilerResults = new CompilerResults(tempFileCollection);
			this.Scripts = new Scripts(this);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x001343C1 File Offset: 0x001325C1
		public CompilerResults Compile(object stylesheet, XmlResolver xmlResolver, out QilExpression qil)
		{
			new XsltLoader().Load(this, stylesheet, xmlResolver);
			qil = QilGenerator.CompileStylesheet(this);
			this.SortErrors();
			return this.CompilerResults;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x001343E4 File Offset: 0x001325E4
		public Stylesheet CreateStylesheet()
		{
			Stylesheet stylesheet = new Stylesheet(this, this.CurrentPrecedence);
			int currentPrecedence = this.CurrentPrecedence;
			this.CurrentPrecedence = currentPrecedence - 1;
			if (currentPrecedence == 0)
			{
				this.Root = new RootLevel(stylesheet);
			}
			return stylesheet;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0013441E File Offset: 0x0013261E
		public void AddModule(string baseUri)
		{
			if (!this.moduleOrder.ContainsKey(baseUri))
			{
				this.moduleOrder[baseUri] = this.moduleOrder.Count;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x00134448 File Offset: 0x00132648
		public void ApplyNsAliases(ref string prefix, ref string nsUri)
		{
			NsAlias nsAlias;
			if (this.NsAliases.TryGetValue(nsUri, out nsAlias))
			{
				nsUri = nsAlias.ResultNsUri;
				prefix = nsAlias.ResultPrefix;
			}
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x00134478 File Offset: 0x00132678
		public bool SetNsAlias(string ssheetNsUri, string resultNsUri, string resultPrefix, int importPrecedence)
		{
			NsAlias nsAlias;
			if (this.NsAliases.TryGetValue(ssheetNsUri, out nsAlias) && (importPrecedence < nsAlias.ImportPrecedence || resultNsUri == nsAlias.ResultNsUri))
			{
				return false;
			}
			this.NsAliases[ssheetNsUri] = new NsAlias(resultNsUri, resultPrefix, importPrecedence);
			return nsAlias != null;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x001344C8 File Offset: 0x001326C8
		private void MergeWhitespaceRules(Stylesheet sheet)
		{
			for (int i = 0; i <= 2; i++)
			{
				sheet.WhitespaceRules[i].Reverse();
				this.WhitespaceRules.AddRange(sheet.WhitespaceRules[i]);
			}
			sheet.WhitespaceRules = null;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x00134508 File Offset: 0x00132708
		private void MergeAttributeSets(Stylesheet sheet)
		{
			foreach (QilName qilName in sheet.AttributeSets.Keys)
			{
				AttributeSet attributeSet;
				if (!this.AttributeSets.TryGetValue(qilName, out attributeSet))
				{
					this.AttributeSets[qilName] = sheet.AttributeSets[qilName];
				}
				else
				{
					attributeSet.MergeContent(sheet.AttributeSets[qilName]);
				}
			}
			sheet.AttributeSets = null;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0013459C File Offset: 0x0013279C
		private void MergeGlobalVarPars(Stylesheet sheet)
		{
			foreach (XslNode xslNode in sheet.GlobalVarPars)
			{
				VarPar varPar = (VarPar)xslNode;
				if (!this.AllGlobalVarPars.ContainsKey(varPar.Name))
				{
					if (varPar.NodeType == XslNodeType.Variable)
					{
						this.GlobalVars.Add(varPar);
					}
					else
					{
						this.ExternalPars.Add(varPar);
					}
					this.AllGlobalVarPars[varPar.Name] = varPar;
				}
			}
			sheet.GlobalVarPars = null;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x00134640 File Offset: 0x00132840
		public void MergeWithStylesheet(Stylesheet sheet)
		{
			this.MergeWhitespaceRules(sheet);
			this.MergeAttributeSets(sheet);
			this.MergeGlobalVarPars(sheet);
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0008BAF1 File Offset: 0x00089CF1
		public static string ConstructQName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x00134658 File Offset: 0x00132858
		public bool ParseQName(string qname, out string prefix, out string localName, IErrorHelper errorHelper)
		{
			bool flag;
			try
			{
				ValidateNames.ParseQNameThrow(qname, out prefix, out localName);
				flag = true;
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
				prefix = this.PhantomNCName;
				localName = this.PhantomNCName;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x001346A8 File Offset: 0x001328A8
		public bool ParseNameTest(string nameTest, out string prefix, out string localName, IErrorHelper errorHelper)
		{
			bool flag;
			try
			{
				ValidateNames.ParseNameTestThrow(nameTest, out prefix, out localName);
				flag = true;
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
				prefix = this.PhantomNCName;
				localName = this.PhantomNCName;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x001346F8 File Offset: 0x001328F8
		public void ValidatePiName(string name, IErrorHelper errorHelper)
		{
			try
			{
				ValidateNames.ValidateNameThrow(string.Empty, name, string.Empty, XPathNodeType.ProcessingInstruction, ValidateNames.Flags.AllExceptPrefixMapping);
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0013473C File Offset: 0x0013293C
		public string CreatePhantomNamespace()
		{
			object obj = "\0namespace";
			int num = this.phantomNsCounter;
			this.phantomNsCounter = num + 1;
			return obj + num;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x00134769 File Offset: 0x00132969
		public bool IsPhantomNamespace(string namespaceName)
		{
			return namespaceName.Length > 0 && namespaceName[0] == '\0';
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x00134780 File Offset: 0x00132980
		public bool IsPhantomName(QilName qname)
		{
			string namespaceUri = qname.NamespaceUri;
			return namespaceUri.Length > 0 && namespaceUri[0] == '\0';
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x001347A9 File Offset: 0x001329A9
		// (set) Token: 0x06003751 RID: 14161 RVA: 0x001347BC File Offset: 0x001329BC
		private int ErrorCount
		{
			get
			{
				return this.CompilerResults.Errors.Count;
			}
			set
			{
				for (int i = this.ErrorCount - 1; i >= value; i--)
				{
					this.CompilerResults.Errors.RemoveAt(i);
				}
			}
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x001347ED File Offset: 0x001329ED
		public void EnterForwardsCompatible()
		{
			this.savedErrorCount = this.ErrorCount;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x001347FB File Offset: 0x001329FB
		public bool ExitForwardsCompatible(bool fwdCompat)
		{
			if (fwdCompat && this.ErrorCount > this.savedErrorCount)
			{
				this.ErrorCount = this.savedErrorCount;
				return false;
			}
			return true;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x00134820 File Offset: 0x00132A20
		public CompilerError CreateError(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			this.AddModule(lineInfo.Uri);
			return new CompilerError(lineInfo.Uri, lineInfo.Start.Line, lineInfo.Start.Pos, string.Empty, XslTransformException.CreateMessage(res, args));
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0013486C File Offset: 0x00132A6C
		public void ReportError(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			CompilerError compilerError = this.CreateError(lineInfo, res, args);
			this.CompilerResults.Errors.Add(compilerError);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x00134898 File Offset: 0x00132A98
		public void ReportWarning(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			int num = 1;
			if (0 <= this.Settings.WarningLevel && this.Settings.WarningLevel < num)
			{
				return;
			}
			CompilerError compilerError = this.CreateError(lineInfo, res, args);
			if (this.Settings.TreatWarningsAsErrors)
			{
				compilerError.ErrorText = XslTransformException.CreateMessage("Warning as Error: {0}", new string[] { compilerError.ErrorText });
				this.CompilerResults.Errors.Add(compilerError);
				return;
			}
			compilerError.IsWarning = true;
			this.CompilerResults.Errors.Add(compilerError);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x00134928 File Offset: 0x00132B28
		private void SortErrors()
		{
			CompilerErrorCollection errors = this.CompilerResults.Errors;
			if (errors.Count > 1)
			{
				CompilerError[] array = new CompilerError[errors.Count];
				errors.CopyTo(array, 0);
				Array.Sort<CompilerError>(array, new Compiler.CompilerErrorComparer(this.moduleOrder));
				errors.Clear();
				errors.AddRange(array);
			}
		}

		// Token: 0x04002359 RID: 9049
		public XsltSettings Settings;

		// Token: 0x0400235A RID: 9050
		public bool IsDebug;

		// Token: 0x0400235B RID: 9051
		public string ScriptAssemblyPath;

		// Token: 0x0400235C RID: 9052
		public int Version;

		// Token: 0x0400235D RID: 9053
		public string inputTypeAnnotations;

		// Token: 0x0400235E RID: 9054
		public CompilerResults CompilerResults;

		// Token: 0x0400235F RID: 9055
		public int CurrentPrecedence;

		// Token: 0x04002360 RID: 9056
		public XslNode StartApplyTemplates;

		// Token: 0x04002361 RID: 9057
		public RootLevel Root;

		// Token: 0x04002362 RID: 9058
		public Scripts Scripts;

		// Token: 0x04002363 RID: 9059
		public Output Output = new Output();

		// Token: 0x04002364 RID: 9060
		public List<VarPar> ExternalPars = new List<VarPar>();

		// Token: 0x04002365 RID: 9061
		public List<VarPar> GlobalVars = new List<VarPar>();

		// Token: 0x04002366 RID: 9062
		public List<WhitespaceRule> WhitespaceRules = new List<WhitespaceRule>();

		// Token: 0x04002367 RID: 9063
		public DecimalFormats DecimalFormats = new DecimalFormats();

		// Token: 0x04002368 RID: 9064
		public Keys Keys = new Keys();

		// Token: 0x04002369 RID: 9065
		public List<ProtoTemplate> AllTemplates = new List<ProtoTemplate>();

		// Token: 0x0400236A RID: 9066
		public Dictionary<QilName, VarPar> AllGlobalVarPars = new Dictionary<QilName, VarPar>();

		// Token: 0x0400236B RID: 9067
		public Dictionary<QilName, Template> NamedTemplates = new Dictionary<QilName, Template>();

		// Token: 0x0400236C RID: 9068
		public Dictionary<QilName, AttributeSet> AttributeSets = new Dictionary<QilName, AttributeSet>();

		// Token: 0x0400236D RID: 9069
		public Dictionary<string, NsAlias> NsAliases = new Dictionary<string, NsAlias>();

		// Token: 0x0400236E RID: 9070
		private Dictionary<string, int> moduleOrder = new Dictionary<string, int>();

		// Token: 0x0400236F RID: 9071
		public readonly string PhantomNCName = "error";

		// Token: 0x04002370 RID: 9072
		private int phantomNsCounter;

		// Token: 0x04002371 RID: 9073
		private int savedErrorCount = -1;

		// Token: 0x0200056A RID: 1386
		private class CompilerErrorComparer : IComparer<CompilerError>
		{
			// Token: 0x06003758 RID: 14168 RVA: 0x0013497C File Offset: 0x00132B7C
			public CompilerErrorComparer(Dictionary<string, int> moduleOrder)
			{
				this.moduleOrder = moduleOrder;
			}

			// Token: 0x06003759 RID: 14169 RVA: 0x0013498C File Offset: 0x00132B8C
			public int Compare(CompilerError x, CompilerError y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				int num = this.moduleOrder[x.FileName].CompareTo(this.moduleOrder[y.FileName]);
				if (num != 0)
				{
					return num;
				}
				num = x.Line.CompareTo(y.Line);
				if (num != 0)
				{
					return num;
				}
				num = x.Column.CompareTo(y.Column);
				if (num != 0)
				{
					return num;
				}
				num = x.IsWarning.CompareTo(y.IsWarning);
				if (num != 0)
				{
					return num;
				}
				num = string.CompareOrdinal(x.ErrorNumber, y.ErrorNumber);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.ErrorText, y.ErrorText);
			}

			// Token: 0x04002372 RID: 9074
			private Dictionary<string, int> moduleOrder;
		}
	}
}
