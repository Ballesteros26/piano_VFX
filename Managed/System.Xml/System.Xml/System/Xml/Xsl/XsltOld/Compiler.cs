using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.Xslt;
using System.Xml.Xsl.XsltOld.Debugger;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F4 RID: 1268
	internal class Compiler
	{
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x0012620A File Offset: 0x0012440A
		internal KeywordsTable Atoms
		{
			get
			{
				return this.atoms;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x00126212 File Offset: 0x00124412
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x0012621A File Offset: 0x0012441A
		internal int Stylesheetid
		{
			get
			{
				return this.stylesheetid;
			}
			set
			{
				this.stylesheetid = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x00126223 File Offset: 0x00124423
		internal NavigatorInput Document
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x00126223 File Offset: 0x00124423
		internal NavigatorInput Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x0012622B File Offset: 0x0012442B
		internal bool Advance()
		{
			return this.Document.Advance();
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x00126238 File Offset: 0x00124438
		internal bool Recurse()
		{
			return this.Document.Recurse();
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x00126245 File Offset: 0x00124445
		internal bool ToParent()
		{
			return this.Document.ToParent();
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x00126252 File Offset: 0x00124452
		internal Stylesheet CompiledStylesheet
		{
			get
			{
				return this.stylesheet;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x0012625A File Offset: 0x0012445A
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x00126262 File Offset: 0x00124462
		internal RootAction RootAction
		{
			get
			{
				return this.rootAction;
			}
			set
			{
				this.rootAction = value;
				this.currentTemplate = this.rootAction;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x00126277 File Offset: 0x00124477
		internal List<TheQuery> QueryStore
		{
			get
			{
				return this.queryStore;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual IXsltDebugger Debugger
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x0012627F File Offset: 0x0012447F
		internal string GetUnicRtfId()
		{
			this.rtfCount++;
			return this.rtfCount.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x001262A0 File Offset: 0x001244A0
		internal void Compile(NavigatorInput input, XmlResolver xmlResolver, Evidence evidence)
		{
			evidence = null;
			this.xmlResolver = xmlResolver;
			this.PushInputDocument(input);
			this.rootScope = this.scopeManager.PushScope();
			this.queryStore = new List<TheQuery>();
			try
			{
				this.rootStylesheet = new Stylesheet();
				this.PushStylesheet(this.rootStylesheet);
				try
				{
					this.CreateRootAction();
				}
				catch (XsltCompileException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new XsltCompileException(ex, this.Input.BaseURI, this.Input.LineNumber, this.Input.LinePosition);
				}
				this.stylesheet.ProcessTemplates();
				this.rootAction.PorcessAttributeSets(this.rootStylesheet);
				this.stylesheet.SortWhiteSpace();
				this.CompileScript(evidence);
				if (evidence != null)
				{
					this.rootAction.permissions = SecurityManager.GetStandardSandbox(evidence);
				}
				if (this.globalNamespaceAliasTable != null)
				{
					this.stylesheet.ReplaceNamespaceAlias(this);
					this.rootAction.ReplaceNamespaceAlias(this);
				}
			}
			finally
			{
				this.PopInputDocument();
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x001263B8 File Offset: 0x001245B8
		// (set) Token: 0x06003386 RID: 13190 RVA: 0x001263CA File Offset: 0x001245CA
		internal bool ForwardCompatibility
		{
			get
			{
				return this.scopeManager.CurrentScope.ForwardCompatibility;
			}
			set
			{
				this.scopeManager.CurrentScope.ForwardCompatibility = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x001263DD File Offset: 0x001245DD
		// (set) Token: 0x06003388 RID: 13192 RVA: 0x001263EF File Offset: 0x001245EF
		internal bool CanHaveApplyImports
		{
			get
			{
				return this.scopeManager.CurrentScope.CanHaveApplyImports;
			}
			set
			{
				this.scopeManager.CurrentScope.CanHaveApplyImports = value;
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x00126404 File Offset: 0x00124604
		internal void InsertExtensionNamespace(string value)
		{
			string[] array = this.ResolvePrefixes(value);
			if (array != null)
			{
				this.scopeManager.InsertExtensionNamespaces(array);
			}
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00126428 File Offset: 0x00124628
		internal void InsertExcludedNamespace(string value)
		{
			string[] array = this.ResolvePrefixes(value);
			if (array != null)
			{
				this.scopeManager.InsertExcludedNamespaces(array);
			}
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0012644C File Offset: 0x0012464C
		internal void InsertExtensionNamespace()
		{
			this.InsertExtensionNamespace(this.Input.Navigator.GetAttribute(this.Input.Atoms.ExtensionElementPrefixes, this.Input.Atoms.UriXsl));
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x00126484 File Offset: 0x00124684
		internal void InsertExcludedNamespace()
		{
			this.InsertExcludedNamespace(this.Input.Navigator.GetAttribute(this.Input.Atoms.ExcludeResultPrefixes, this.Input.Atoms.UriXsl));
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x001264BC File Offset: 0x001246BC
		internal bool IsExtensionNamespace(string nspace)
		{
			return this.scopeManager.IsExtensionNamespace(nspace);
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x001264CA File Offset: 0x001246CA
		internal bool IsExcludedNamespace(string nspace)
		{
			return this.scopeManager.IsExcludedNamespace(nspace);
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x001264D8 File Offset: 0x001246D8
		internal void PushLiteralScope()
		{
			this.PushNamespaceScope();
			string attribute = this.Input.Navigator.GetAttribute(this.Atoms.Version, this.Atoms.UriXsl);
			if (attribute.Length != 0)
			{
				this.ForwardCompatibility = attribute != "1.0";
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0012652C File Offset: 0x0012472C
		internal void PushNamespaceScope()
		{
			this.scopeManager.PushScope();
			NavigatorInput navigatorInput = this.Input;
			if (navigatorInput.MoveToFirstNamespace())
			{
				do
				{
					this.scopeManager.PushNamespace(navigatorInput.LocalName, navigatorInput.Value);
				}
				while (navigatorInput.MoveToNextNamespace());
				navigatorInput.ToParent();
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x0012657A File Offset: 0x0012477A
		protected InputScopeManager ScopeManager
		{
			get
			{
				return this.scopeManager;
			}
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x00126582 File Offset: 0x00124782
		internal virtual void PopScope()
		{
			this.currentTemplate.ReleaseVariableSlots(this.scopeManager.CurrentScope.GetVeriablesCount());
			this.scopeManager.PopScope();
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x001265AA File Offset: 0x001247AA
		internal InputScopeManager CloneScopeManager()
		{
			return this.scopeManager.Clone();
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x001265B8 File Offset: 0x001247B8
		internal int InsertVariable(VariableAction variable)
		{
			InputScope variableScope;
			if (variable.IsGlobal)
			{
				variableScope = this.rootScope;
			}
			else
			{
				variableScope = this.scopeManager.VariableScope;
			}
			VariableAction variableAction = variableScope.ResolveVariable(variable.Name);
			if (variableAction != null)
			{
				if (!variableAction.IsGlobal)
				{
					throw XsltException.Create("Variable or parameter '{0}' was duplicated within the same scope.", new string[] { variable.NameStr });
				}
				if (variable.IsGlobal)
				{
					if (variable.Stylesheetid == variableAction.Stylesheetid)
					{
						throw XsltException.Create("Variable or parameter '{0}' was duplicated within the same scope.", new string[] { variable.NameStr });
					}
					if (variable.Stylesheetid < variableAction.Stylesheetid)
					{
						variableScope.InsertVariable(variable);
						return variableAction.VarKey;
					}
					return -1;
				}
			}
			variableScope.InsertVariable(variable);
			return this.currentTemplate.AllocateVariableSlot();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x00126674 File Offset: 0x00124874
		internal void AddNamespaceAlias(string StylesheetURI, NamespaceInfo AliasInfo)
		{
			if (this.globalNamespaceAliasTable == null)
			{
				this.globalNamespaceAliasTable = new Hashtable();
			}
			NamespaceInfo namespaceInfo = this.globalNamespaceAliasTable[StylesheetURI] as NamespaceInfo;
			if (namespaceInfo == null || AliasInfo.stylesheetId <= namespaceInfo.stylesheetId)
			{
				this.globalNamespaceAliasTable[StylesheetURI] = AliasInfo;
			}
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x001266C4 File Offset: 0x001248C4
		internal bool IsNamespaceAlias(string StylesheetURI)
		{
			return this.globalNamespaceAliasTable != null && this.globalNamespaceAliasTable.Contains(StylesheetURI);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x001266DC File Offset: 0x001248DC
		internal NamespaceInfo FindNamespaceAlias(string StylesheetURI)
		{
			if (this.globalNamespaceAliasTable != null)
			{
				return (NamespaceInfo)this.globalNamespaceAliasTable[StylesheetURI];
			}
			return null;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x001266F9 File Offset: 0x001248F9
		internal string ResolveXmlNamespace(string prefix)
		{
			return this.scopeManager.ResolveXmlNamespace(prefix);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x00126707 File Offset: 0x00124907
		internal string ResolveXPathNamespace(string prefix)
		{
			return this.scopeManager.ResolveXPathNamespace(prefix);
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x00126715 File Offset: 0x00124915
		internal string DefaultNamespace
		{
			get
			{
				return this.scopeManager.DefaultNamespace;
			}
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x00126722 File Offset: 0x00124922
		internal void InsertKey(XmlQualifiedName name, int MatchKey, int UseKey)
		{
			this.rootAction.InsertKey(name, MatchKey, UseKey);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x00126732 File Offset: 0x00124932
		internal void AddDecimalFormat(XmlQualifiedName name, DecimalFormat formatinfo)
		{
			this.rootAction.AddDecimalFormat(name, formatinfo);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x00126744 File Offset: 0x00124944
		private string[] ResolvePrefixes(string tokens)
		{
			if (tokens == null || tokens.Length == 0)
			{
				return null;
			}
			string[] array = XmlConvert.SplitString(tokens);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array[i] = this.scopeManager.ResolveXmlNamespace((text == "#default") ? string.Empty : text);
				}
			}
			catch (XsltException)
			{
				if (!this.ForwardCompatibility)
				{
					throw;
				}
				return null;
			}
			return array;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x001267C0 File Offset: 0x001249C0
		internal bool GetYesNo(string value)
		{
			if (value == "yes")
			{
				return true;
			}
			if (value == "no")
			{
				return false;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				this.Input.LocalName,
				value
			});
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x00126810 File Offset: 0x00124A10
		internal string GetSingleAttribute(string attributeAtom)
		{
			NavigatorInput navigatorInput = this.Input;
			string localName = navigatorInput.LocalName;
			string text = null;
			if (navigatorInput.MoveToFirstAttribute())
			{
				string localName2;
				for (;;)
				{
					string namespaceURI = navigatorInput.NamespaceURI;
					localName2 = navigatorInput.LocalName;
					if (namespaceURI.Length == 0)
					{
						if (Ref.Equal(localName2, attributeAtom))
						{
							text = navigatorInput.Value;
						}
						else if (!this.ForwardCompatibility)
						{
							break;
						}
					}
					if (!navigatorInput.MoveToNextAttribute())
					{
						goto Block_4;
					}
				}
				throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[] { localName2, localName });
				Block_4:
				navigatorInput.ToParent();
			}
			if (text == null)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[] { attributeAtom });
			}
			return text;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x001268AC File Offset: 0x00124AAC
		internal XmlQualifiedName CreateXPathQName(string qname)
		{
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(qname, out text, out text2);
			return new XmlQualifiedName(text2, this.scopeManager.ResolveXPathNamespace(text));
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x001268D8 File Offset: 0x00124AD8
		internal XmlQualifiedName CreateXmlQName(string qname)
		{
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(qname, out text, out text2);
			return new XmlQualifiedName(text2, this.scopeManager.ResolveXmlNamespace(text));
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00126904 File Offset: 0x00124B04
		internal static XPathDocument LoadDocument(XmlTextReaderImpl reader)
		{
			reader.EntityHandling = EntityHandling.ExpandEntities;
			reader.XmlValidatingReaderCompatibilityMode = true;
			XPathDocument xpathDocument;
			try
			{
				xpathDocument = new XPathDocument(reader, XmlSpace.Preserve);
			}
			finally
			{
				reader.Close();
			}
			return xpathDocument;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00126944 File Offset: 0x00124B44
		private void AddDocumentURI(string href)
		{
			this.documentURIs.Add(href, null);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00126953 File Offset: 0x00124B53
		private void RemoveDocumentURI(string href)
		{
			this.documentURIs.Remove(href);
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x00126961 File Offset: 0x00124B61
		internal bool IsCircularReference(string href)
		{
			return this.documentURIs.Contains(href);
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x00126970 File Offset: 0x00124B70
		internal Uri ResolveUri(string relativeUri)
		{
			string baseURI = this.Input.BaseURI;
			Uri uri = this.xmlResolver.ResolveUri((baseURI.Length != 0) ? this.xmlResolver.ResolveUri(null, baseURI) : null, relativeUri);
			if (uri == null)
			{
				throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[] { relativeUri });
			}
			return uri;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x001269D0 File Offset: 0x00124BD0
		internal NavigatorInput ResolveDocument(Uri absoluteUri)
		{
			object entity = this.xmlResolver.GetEntity(absoluteUri, null, null);
			string text = absoluteUri.ToString();
			if (entity is Stream)
			{
				return new NavigatorInput(Compiler.LoadDocument(new XmlTextReaderImpl(text, (Stream)entity)
				{
					XmlResolver = this.xmlResolver
				}).CreateNavigator(), text, this.rootScope);
			}
			if (entity is XPathNavigator)
			{
				return new NavigatorInput((XPathNavigator)entity, text, this.rootScope);
			}
			throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[] { text });
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x00126A5C File Offset: 0x00124C5C
		internal void PushInputDocument(NavigatorInput newInput)
		{
			string href = newInput.Href;
			this.AddDocumentURI(href);
			newInput.Next = this.input;
			this.input = newInput;
			this.atoms = this.input.Atoms;
			this.scopeManager = this.input.InputScopeManager;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x00126AAC File Offset: 0x00124CAC
		internal void PopInputDocument()
		{
			NavigatorInput navigatorInput = this.input;
			this.input = navigatorInput.Next;
			navigatorInput.Next = null;
			if (this.input != null)
			{
				this.atoms = this.input.Atoms;
				this.scopeManager = this.input.InputScopeManager;
			}
			else
			{
				this.atoms = null;
				this.scopeManager = null;
			}
			this.RemoveDocumentURI(navigatorInput.Href);
			navigatorInput.Close();
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x00126B1F File Offset: 0x00124D1F
		internal void PushStylesheet(Stylesheet stylesheet)
		{
			if (this.stylesheets == null)
			{
				this.stylesheets = new Stack();
			}
			this.stylesheets.Push(stylesheet);
			this.stylesheet = stylesheet;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x00126B47 File Offset: 0x00124D47
		internal Stylesheet PopStylesheet()
		{
			Stylesheet stylesheet = (Stylesheet)this.stylesheets.Pop();
			this.stylesheet = (Stylesheet)this.stylesheets.Peek();
			return stylesheet;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x00126B6F File Offset: 0x00124D6F
		internal void AddAttributeSet(AttributeSetAction attributeSet)
		{
			this.stylesheet.AddAttributeSet(attributeSet);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x00126B7D File Offset: 0x00124D7D
		internal void AddTemplate(TemplateAction template)
		{
			this.stylesheet.AddTemplate(template);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x00126B8B File Offset: 0x00124D8B
		internal void BeginTemplate(TemplateAction template)
		{
			this.currentTemplate = template;
			this.currentMode = template.Mode;
			this.CanHaveApplyImports = template.MatchKey != -1;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x00126BB2 File Offset: 0x00124DB2
		internal void EndTemplate()
		{
			this.currentTemplate = this.rootAction;
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x00126BC0 File Offset: 0x00124DC0
		internal XmlQualifiedName CurrentMode
		{
			get
			{
				return this.currentMode;
			}
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x00126BC8 File Offset: 0x00124DC8
		internal int AddQuery(string xpathQuery)
		{
			return this.AddQuery(xpathQuery, true, true, false);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x00126BD4 File Offset: 0x00124DD4
		internal int AddQuery(string xpathQuery, bool allowVar, bool allowKey, bool isPattern)
		{
			CompiledXpathExpr compiledXpathExpr;
			try
			{
				compiledXpathExpr = new CompiledXpathExpr(isPattern ? this.queryBuilder.BuildPatternQuery(xpathQuery, allowVar, allowKey) : this.queryBuilder.Build(xpathQuery, allowVar, allowKey), xpathQuery, false);
			}
			catch (XPathException ex)
			{
				if (!this.ForwardCompatibility)
				{
					throw XsltException.Create("'{0}' is an invalid XPath expression.", new string[] { xpathQuery }, ex);
				}
				compiledXpathExpr = new Compiler.ErrorXPathExpression(xpathQuery, this.Input.BaseURI, this.Input.LineNumber, this.Input.LinePosition);
			}
			this.queryStore.Add(new TheQuery(compiledXpathExpr, this.scopeManager));
			return this.queryStore.Count - 1;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x00126C8C File Offset: 0x00124E8C
		internal int AddStringQuery(string xpathQuery)
		{
			string text = (XmlCharType.Instance.IsOnlyWhitespace(xpathQuery) ? xpathQuery : ("string(" + xpathQuery + ")"));
			return this.AddQuery(text);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x00126CC4 File Offset: 0x00124EC4
		internal int AddBooleanQuery(string xpathQuery)
		{
			string text = (XmlCharType.Instance.IsOnlyWhitespace(xpathQuery) ? xpathQuery : ("boolean(" + xpathQuery + ")"));
			return this.AddQuery(text);
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x00126CFC File Offset: 0x00124EFC
		private static string GenerateUniqueClassName()
		{
			return "ScriptClass_" + Interlocked.Increment(ref Compiler.scriptClassCounter);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x00126D18 File Offset: 0x00124F18
		internal void AddScript(string source, ScriptingLanguage lang, string ns, string fileName, int lineNumber)
		{
			Compiler.ValidateExtensionNamespace(ns);
			for (ScriptingLanguage scriptingLanguage = ScriptingLanguage.JScript; scriptingLanguage <= ScriptingLanguage.CSharp; scriptingLanguage++)
			{
				Hashtable hashtable = this._typeDeclsByLang[(int)scriptingLanguage];
				if (lang == scriptingLanguage)
				{
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)hashtable[ns];
					if (codeTypeDeclaration == null)
					{
						codeTypeDeclaration = new CodeTypeDeclaration(Compiler.GenerateUniqueClassName());
						codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
						hashtable.Add(ns, codeTypeDeclaration);
					}
					CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(source);
					if (lineNumber > 0)
					{
						codeSnippetTypeMember.LinePragma = new CodeLinePragma(fileName, lineNumber);
						this.scriptFiles.Add(fileName);
					}
					codeTypeDeclaration.Members.Add(codeSnippetTypeMember);
				}
				else if (hashtable.Contains(ns))
				{
					throw XsltException.Create("All script blocks implementing the namespace '{0}' must use the same language.", new string[] { ns });
				}
			}
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x00126DCA File Offset: 0x00124FCA
		private static void ValidateExtensionNamespace(string nsUri)
		{
			if (nsUri.Length == 0 || nsUri == "http://www.w3.org/1999/XSL/Transform")
			{
				throw XsltException.Create("Extension namespace cannot be 'null' or an XSLT namespace URI.", Array.Empty<string>());
			}
			XmlConvert.ToUri(nsUri);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x00126DF8 File Offset: 0x00124FF8
		private void FixCompilerError(CompilerError e)
		{
			foreach (object obj in this.scriptFiles)
			{
				string text = (string)obj;
				if (e.FileName == text)
				{
					return;
				}
			}
			e.FileName = string.Empty;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x00126E68 File Offset: 0x00125068
		private CodeDomProvider ChooseCodeDomProvider(ScriptingLanguage lang)
		{
			if (lang == ScriptingLanguage.JScript)
			{
				return (CodeDomProvider)Activator.CreateInstance(Type.GetType("Microsoft.JScript.JScriptCodeProvider, Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
			}
			if (lang != ScriptingLanguage.VisualBasic)
			{
				return new CSharpCodeProvider();
			}
			return new VBCodeProvider();
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x00126E9C File Offset: 0x0012509C
		private void CompileScript(Evidence evidence)
		{
			for (ScriptingLanguage scriptingLanguage = ScriptingLanguage.JScript; scriptingLanguage <= ScriptingLanguage.CSharp; scriptingLanguage++)
			{
				int num = (int)scriptingLanguage;
				if (this._typeDeclsByLang[num].Count > 0)
				{
					this.CompileAssembly(scriptingLanguage, this._typeDeclsByLang[num], scriptingLanguage.ToString(), evidence);
				}
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x00126EE4 File Offset: 0x001250E4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void CompileAssembly(ScriptingLanguage lang, Hashtable typeDecls, string nsName, Evidence evidence)
		{
			nsName = "Microsoft.Xslt.CompiledScripts." + nsName;
			CodeNamespace codeNamespace = new CodeNamespace(nsName);
			foreach (string text in Compiler._defaultNamespaces)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport(text));
			}
			if (lang == ScriptingLanguage.VisualBasic)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
			}
			foreach (object obj in typeDecls.Values)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				codeNamespace.Types.Add(codeTypeDeclaration);
			}
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			codeCompileUnit.UserData["AllowLateBound"] = true;
			codeCompileUnit.UserData["RequireVariableDeclaration"] = false;
			codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SecurityRulesAttribute)), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityRuleSet)), "Level1"))
			}));
			CompilerParameters compilerParameters = new CompilerParameters();
			try
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
				try
				{
					compilerParameters.GenerateInMemory = true;
					compilerParameters.Evidence = evidence;
					compilerParameters.ReferencedAssemblies.Add(typeof(XPathNavigator).Module.FullyQualifiedName);
					compilerParameters.ReferencedAssemblies.Add("System.dll");
					if (lang == ScriptingLanguage.VisualBasic)
					{
						compilerParameters.ReferencedAssemblies.Add("microsoft.visualbasic.dll");
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			catch
			{
				throw;
			}
			CompilerResults compilerResults = this.ChooseCodeDomProvider(lang).CompileAssemblyFromDom(compilerParameters, new CodeCompileUnit[] { codeCompileUnit });
			if (compilerResults.Errors.HasErrors)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				foreach (object obj2 in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj2;
					this.FixCompilerError(compilerError);
					stringWriter.WriteLine(compilerError.ToString());
				}
				throw XsltException.Create("Script compile errors:\n{0}", new string[] { stringWriter.ToString() });
			}
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			foreach (object obj3 in typeDecls)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				string text2 = (string)dictionaryEntry.Key;
				CodeTypeDeclaration codeTypeDeclaration2 = (CodeTypeDeclaration)dictionaryEntry.Value;
				this.stylesheet.ScriptObjectTypes.Add(text2, compiledAssembly.GetType(nsName + "." + codeTypeDeclaration2.Name));
			}
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x001271FC File Offset: 0x001253FC
		public string GetNsAlias(ref string prefix)
		{
			if (prefix == "#default")
			{
				prefix = string.Empty;
				return this.DefaultNamespace;
			}
			if (!PrefixQName.ValidatePrefix(prefix))
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					this.input.LocalName,
					prefix
				});
			}
			return this.ResolveXPathNamespace(prefix);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0012725C File Offset: 0x0012545C
		private static void getTextLex(string avt, ref int start, StringBuilder lex)
		{
			int length = avt.Length;
			int i;
			for (i = start; i < length; i++)
			{
				char c = avt[i];
				if (c == '{')
				{
					if (i + 1 >= length || avt[i + 1] != '{')
					{
						break;
					}
					i++;
				}
				else if (c == '}')
				{
					if (i + 1 >= length || avt[i + 1] != '}')
					{
						throw XsltException.Create("Right curly brace in the attribute value template '{0}' must be doubled.", new string[] { avt });
					}
					i++;
				}
				lex.Append(c);
			}
			start = i;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x001272E4 File Offset: 0x001254E4
		private static void getXPathLex(string avt, ref int start, StringBuilder lex)
		{
			int length = avt.Length;
			int num = 0;
			for (int i = start + 1; i < length; i++)
			{
				char c = avt[i];
				switch (num)
				{
				case 0:
					if (c <= '\'')
					{
						if (c != '"')
						{
							if (c == '\'')
							{
								num = 1;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						if (c == '{')
						{
							throw XsltException.Create("AVT cannot be nested in AVT '{0}'.", new string[] { avt });
						}
						if (c == '}')
						{
							i++;
							if (i == start + 2)
							{
								throw XsltException.Create("XPath Expression in AVT cannot be empty: '{0}'.", new string[] { avt });
							}
							lex.Append(avt, start + 1, i - start - 2);
							start = i;
							return;
						}
					}
					break;
				case 1:
					if (c == '\'')
					{
						num = 0;
					}
					break;
				case 2:
					if (c == '"')
					{
						num = 0;
					}
					break;
				}
			}
			throw XsltException.Create((num == 0) ? "The braces are not closed in AVT expression '{0}'." : "The literal in AVT expression is not correctly closed '{0}'.", new string[] { avt });
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x001273CC File Offset: 0x001255CC
		private static bool GetNextAvtLex(string avt, ref int start, StringBuilder lex, out bool isAvt)
		{
			isAvt = false;
			if (start == avt.Length)
			{
				return false;
			}
			lex.Length = 0;
			Compiler.getTextLex(avt, ref start, lex);
			if (lex.Length == 0)
			{
				isAvt = true;
				Compiler.getXPathLex(avt, ref start, lex);
			}
			return true;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00127400 File Offset: 0x00125600
		internal ArrayList CompileAvt(string avtText, out bool constant)
		{
			ArrayList arrayList = new ArrayList();
			constant = true;
			int num = 0;
			bool flag;
			while (Compiler.GetNextAvtLex(avtText, ref num, this.AvtStringBuilder, out flag))
			{
				string text = this.AvtStringBuilder.ToString();
				if (flag)
				{
					arrayList.Add(new AvtEvent(this.AddStringQuery(text)));
					constant = false;
				}
				else
				{
					arrayList.Add(new TextEvent(text));
				}
			}
			return arrayList;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x00127464 File Offset: 0x00125664
		internal ArrayList CompileAvt(string avtText)
		{
			bool flag;
			return this.CompileAvt(avtText, out flag);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x0012747A File Offset: 0x0012567A
		public virtual ApplyImportsAction CreateApplyImportsAction()
		{
			ApplyImportsAction applyImportsAction = new ApplyImportsAction();
			applyImportsAction.Compile(this);
			return applyImportsAction;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x00127488 File Offset: 0x00125688
		public virtual ApplyTemplatesAction CreateApplyTemplatesAction()
		{
			ApplyTemplatesAction applyTemplatesAction = new ApplyTemplatesAction();
			applyTemplatesAction.Compile(this);
			return applyTemplatesAction;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x00127496 File Offset: 0x00125696
		public virtual AttributeAction CreateAttributeAction()
		{
			AttributeAction attributeAction = new AttributeAction();
			attributeAction.Compile(this);
			return attributeAction;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x001274A4 File Offset: 0x001256A4
		public virtual AttributeSetAction CreateAttributeSetAction()
		{
			AttributeSetAction attributeSetAction = new AttributeSetAction();
			attributeSetAction.Compile(this);
			return attributeSetAction;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x001274B2 File Offset: 0x001256B2
		public virtual CallTemplateAction CreateCallTemplateAction()
		{
			CallTemplateAction callTemplateAction = new CallTemplateAction();
			callTemplateAction.Compile(this);
			return callTemplateAction;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x001274C0 File Offset: 0x001256C0
		public virtual ChooseAction CreateChooseAction()
		{
			ChooseAction chooseAction = new ChooseAction();
			chooseAction.Compile(this);
			return chooseAction;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x001274CE File Offset: 0x001256CE
		public virtual CommentAction CreateCommentAction()
		{
			CommentAction commentAction = new CommentAction();
			commentAction.Compile(this);
			return commentAction;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x001274DC File Offset: 0x001256DC
		public virtual CopyAction CreateCopyAction()
		{
			CopyAction copyAction = new CopyAction();
			copyAction.Compile(this);
			return copyAction;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x001274EA File Offset: 0x001256EA
		public virtual CopyOfAction CreateCopyOfAction()
		{
			CopyOfAction copyOfAction = new CopyOfAction();
			copyOfAction.Compile(this);
			return copyOfAction;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x001274F8 File Offset: 0x001256F8
		public virtual ElementAction CreateElementAction()
		{
			ElementAction elementAction = new ElementAction();
			elementAction.Compile(this);
			return elementAction;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x00127506 File Offset: 0x00125706
		public virtual ForEachAction CreateForEachAction()
		{
			ForEachAction forEachAction = new ForEachAction();
			forEachAction.Compile(this);
			return forEachAction;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x00127514 File Offset: 0x00125714
		public virtual IfAction CreateIfAction(IfAction.ConditionType type)
		{
			IfAction ifAction = new IfAction(type);
			ifAction.Compile(this);
			return ifAction;
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x00127523 File Offset: 0x00125723
		public virtual MessageAction CreateMessageAction()
		{
			MessageAction messageAction = new MessageAction();
			messageAction.Compile(this);
			return messageAction;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x00127531 File Offset: 0x00125731
		public virtual NewInstructionAction CreateNewInstructionAction()
		{
			NewInstructionAction newInstructionAction = new NewInstructionAction();
			newInstructionAction.Compile(this);
			return newInstructionAction;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x0012753F File Offset: 0x0012573F
		public virtual NumberAction CreateNumberAction()
		{
			NumberAction numberAction = new NumberAction();
			numberAction.Compile(this);
			return numberAction;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x0012754D File Offset: 0x0012574D
		public virtual ProcessingInstructionAction CreateProcessingInstructionAction()
		{
			ProcessingInstructionAction processingInstructionAction = new ProcessingInstructionAction();
			processingInstructionAction.Compile(this);
			return processingInstructionAction;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x0012755B File Offset: 0x0012575B
		public virtual void CreateRootAction()
		{
			this.RootAction = new RootAction();
			this.RootAction.Compile(this);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x00127574 File Offset: 0x00125774
		public virtual SortAction CreateSortAction()
		{
			SortAction sortAction = new SortAction();
			sortAction.Compile(this);
			return sortAction;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x00127582 File Offset: 0x00125782
		public virtual TemplateAction CreateTemplateAction()
		{
			TemplateAction templateAction = new TemplateAction();
			templateAction.Compile(this);
			return templateAction;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00127590 File Offset: 0x00125790
		public virtual TemplateAction CreateSingleTemplateAction()
		{
			TemplateAction templateAction = new TemplateAction();
			templateAction.CompileSingle(this);
			return templateAction;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x0012759E File Offset: 0x0012579E
		public virtual TextAction CreateTextAction()
		{
			TextAction textAction = new TextAction();
			textAction.Compile(this);
			return textAction;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x001275AC File Offset: 0x001257AC
		public virtual UseAttributeSetsAction CreateUseAttributeSetsAction()
		{
			UseAttributeSetsAction useAttributeSetsAction = new UseAttributeSetsAction();
			useAttributeSetsAction.Compile(this);
			return useAttributeSetsAction;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x001275BA File Offset: 0x001257BA
		public virtual ValueOfAction CreateValueOfAction()
		{
			ValueOfAction valueOfAction = new ValueOfAction();
			valueOfAction.Compile(this);
			return valueOfAction;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x001275C8 File Offset: 0x001257C8
		public virtual VariableAction CreateVariableAction(VariableType type)
		{
			VariableAction variableAction = new VariableAction(type);
			variableAction.Compile(this);
			if (variableAction.VarKey != -1)
			{
				return variableAction;
			}
			return null;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x001275EF File Offset: 0x001257EF
		public virtual WithParamAction CreateWithParamAction()
		{
			WithParamAction withParamAction = new WithParamAction();
			withParamAction.Compile(this);
			return withParamAction;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x001275FD File Offset: 0x001257FD
		public virtual BeginEvent CreateBeginEvent()
		{
			return new BeginEvent(this);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x00127605 File Offset: 0x00125805
		public virtual TextEvent CreateTextEvent()
		{
			return new TextEvent(this);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x00127610 File Offset: 0x00125810
		public XsltException UnexpectedKeyword()
		{
			XPathNavigator xpathNavigator = this.Input.Navigator.Clone();
			string name = xpathNavigator.Name;
			xpathNavigator.MoveToParent();
			string name2 = xpathNavigator.Name;
			return XsltException.Create("'{0}' cannot be a child of the '{1}' element.", new string[] { name, name2 });
		}

		// Token: 0x04002144 RID: 8516
		internal const int InvalidQueryKey = -1;

		// Token: 0x04002145 RID: 8517
		internal const double RootPriority = 0.5;

		// Token: 0x04002146 RID: 8518
		internal StringBuilder AvtStringBuilder = new StringBuilder();

		// Token: 0x04002147 RID: 8519
		private int stylesheetid;

		// Token: 0x04002148 RID: 8520
		private InputScope rootScope;

		// Token: 0x04002149 RID: 8521
		private XmlResolver xmlResolver;

		// Token: 0x0400214A RID: 8522
		private TemplateBaseAction currentTemplate;

		// Token: 0x0400214B RID: 8523
		private XmlQualifiedName currentMode;

		// Token: 0x0400214C RID: 8524
		private Hashtable globalNamespaceAliasTable;

		// Token: 0x0400214D RID: 8525
		private Stack stylesheets;

		// Token: 0x0400214E RID: 8526
		private HybridDictionary documentURIs = new HybridDictionary();

		// Token: 0x0400214F RID: 8527
		private NavigatorInput input;

		// Token: 0x04002150 RID: 8528
		private KeywordsTable atoms;

		// Token: 0x04002151 RID: 8529
		private InputScopeManager scopeManager;

		// Token: 0x04002152 RID: 8530
		internal Stylesheet stylesheet;

		// Token: 0x04002153 RID: 8531
		internal Stylesheet rootStylesheet;

		// Token: 0x04002154 RID: 8532
		private RootAction rootAction;

		// Token: 0x04002155 RID: 8533
		private List<TheQuery> queryStore;

		// Token: 0x04002156 RID: 8534
		private QueryBuilder queryBuilder = new QueryBuilder();

		// Token: 0x04002157 RID: 8535
		private int rtfCount;

		// Token: 0x04002158 RID: 8536
		public bool AllowBuiltInMode;

		// Token: 0x04002159 RID: 8537
		public static XmlQualifiedName BuiltInMode = new XmlQualifiedName("*", string.Empty);

		// Token: 0x0400215A RID: 8538
		private Hashtable[] _typeDeclsByLang = new Hashtable[]
		{
			new Hashtable(),
			new Hashtable(),
			new Hashtable()
		};

		// Token: 0x0400215B RID: 8539
		private ArrayList scriptFiles = new ArrayList();

		// Token: 0x0400215C RID: 8540
		private static string[] _defaultNamespaces = new string[] { "System", "System.Collections", "System.Text", "System.Text.RegularExpressions", "System.Xml", "System.Xml.Xsl", "System.Xml.XPath" };

		// Token: 0x0400215D RID: 8541
		private static int scriptClassCounter = 0;

		// Token: 0x020004F5 RID: 1269
		internal class ErrorXPathExpression : CompiledXpathExpr
		{
			// Token: 0x060033E0 RID: 13280 RVA: 0x0012772A File Offset: 0x0012592A
			public ErrorXPathExpression(string expression, string baseUri, int lineNumber, int linePosition)
				: base(null, expression, false)
			{
				this.baseUri = baseUri;
				this.lineNumber = lineNumber;
				this.linePosition = linePosition;
			}

			// Token: 0x060033E1 RID: 13281 RVA: 0x00002068 File Offset: 0x00000268
			public override XPathExpression Clone()
			{
				return this;
			}

			// Token: 0x060033E2 RID: 13282 RVA: 0x0012774B File Offset: 0x0012594B
			public override void CheckErrors()
			{
				throw new XsltException("'{0}' is an invalid XPath expression.", new string[] { this.Expression }, this.baseUri, this.linePosition, this.lineNumber, null);
			}

			// Token: 0x0400215E RID: 8542
			private string baseUri;

			// Token: 0x0400215F RID: 8543
			private int lineNumber;

			// Token: 0x04002160 RID: 8544
			private int linePosition;
		}
	}
}
