using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000523 RID: 1315
	internal class InputScopeManager
	{
		// Token: 0x060034EB RID: 13547 RVA: 0x0012AF5A File Offset: 0x0012915A
		public InputScopeManager(XPathNavigator navigator, InputScope rootScope)
		{
			this.navigator = navigator;
			this.scopeStack = rootScope;
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x0012AF7B File Offset: 0x0012917B
		internal InputScope CurrentScope
		{
			get
			{
				return this.scopeStack;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x0012AF83 File Offset: 0x00129183
		internal InputScope VariableScope
		{
			get
			{
				return this.scopeStack.Parent;
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x0012AF90 File Offset: 0x00129190
		internal InputScopeManager Clone()
		{
			return new InputScopeManager(this.navigator, null)
			{
				scopeStack = this.scopeStack,
				defaultNS = this.defaultNS
			};
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x0012AFB6 File Offset: 0x001291B6
		public XPathNavigator Navigator
		{
			get
			{
				return this.navigator;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x0012AFBE File Offset: 0x001291BE
		internal InputScope PushScope()
		{
			this.scopeStack = new InputScope(this.scopeStack);
			return this.scopeStack;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x0012AFD8 File Offset: 0x001291D8
		internal void PopScope()
		{
			if (this.scopeStack == null)
			{
				return;
			}
			for (NamespaceDecl namespaceDecl = this.scopeStack.Scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				this.defaultNS = namespaceDecl.PrevDefaultNsUri;
			}
			this.scopeStack = this.scopeStack.Parent;
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x0012B023 File Offset: 0x00129223
		internal void PushNamespace(string prefix, string nspace)
		{
			this.scopeStack.AddNamespace(prefix, nspace, this.defaultNS);
			if (prefix == null || prefix.Length == 0)
			{
				this.defaultNS = nspace;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x0012B04B File Offset: 0x0012924B
		public string DefaultNamespace
		{
			get
			{
				return this.defaultNS;
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x0012B054 File Offset: 0x00129254
		private string ResolveNonEmptyPrefix(string prefix)
		{
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				string text = parent.ResolveNonAtom(prefix);
				if (text != null)
				{
					return text;
				}
			}
			throw XsltException.Create("Prefix '{0}' is not defined.", new string[] { prefix });
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x0012B0BB File Offset: 0x001292BB
		public string ResolveXmlNamespace(string prefix)
		{
			if (prefix.Length == 0)
			{
				return this.defaultNS;
			}
			return this.ResolveNonEmptyPrefix(prefix);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x0012B0D3 File Offset: 0x001292D3
		public string ResolveXPathNamespace(string prefix)
		{
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return this.ResolveNonEmptyPrefix(prefix);
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x0012B0EC File Offset: 0x001292EC
		internal void InsertExtensionNamespaces(string[] nsList)
		{
			for (int i = 0; i < nsList.Length; i++)
			{
				this.scopeStack.InsertExtensionNamespace(nsList[i]);
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0012B118 File Offset: 0x00129318
		internal bool IsExtensionNamespace(string nspace)
		{
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				if (parent.IsExtensionNamespace(nspace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x0012B144 File Offset: 0x00129344
		internal void InsertExcludedNamespaces(string[] nsList)
		{
			for (int i = 0; i < nsList.Length; i++)
			{
				this.scopeStack.InsertExcludedNamespace(nsList[i]);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x0012B170 File Offset: 0x00129370
		internal bool IsExcludedNamespace(string nspace)
		{
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				if (parent.IsExcludedNamespace(nspace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040021CE RID: 8654
		private InputScope scopeStack;

		// Token: 0x040021CF RID: 8655
		private string defaultNS = string.Empty;

		// Token: 0x040021D0 RID: 8656
		private XPathNavigator navigator;
	}
}
