using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000522 RID: 1314
	internal class InputScope : DocumentScope
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x0012ADC8 File Offset: 0x00128FC8
		internal InputScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x0012ADD0 File Offset: 0x00128FD0
		internal Hashtable Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x0012ADD8 File Offset: 0x00128FD8
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x0012ADE0 File Offset: 0x00128FE0
		internal bool ForwardCompatibility
		{
			get
			{
				return this.forwardCompatibility;
			}
			set
			{
				this.forwardCompatibility = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x0012ADE9 File Offset: 0x00128FE9
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x0012ADF1 File Offset: 0x00128FF1
		internal bool CanHaveApplyImports
		{
			get
			{
				return this.canHaveApplyImports;
			}
			set
			{
				this.canHaveApplyImports = value;
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x0012ADFA File Offset: 0x00128FFA
		internal InputScope(InputScope parent)
		{
			this.Init(parent);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0012AE09 File Offset: 0x00129009
		internal void Init(InputScope parent)
		{
			this.scopes = null;
			this.parent = parent;
			if (this.parent != null)
			{
				this.forwardCompatibility = this.parent.forwardCompatibility;
				this.canHaveApplyImports = this.parent.canHaveApplyImports;
			}
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0012AE43 File Offset: 0x00129043
		internal void InsertExtensionNamespace(string nspace)
		{
			if (this.extensionNamespaces == null)
			{
				this.extensionNamespaces = new Hashtable();
			}
			this.extensionNamespaces[nspace] = null;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x0012AE65 File Offset: 0x00129065
		internal bool IsExtensionNamespace(string nspace)
		{
			return this.extensionNamespaces != null && this.extensionNamespaces.Contains(nspace);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x0012AE7D File Offset: 0x0012907D
		internal void InsertExcludedNamespace(string nspace)
		{
			if (this.excludedNamespaces == null)
			{
				this.excludedNamespaces = new Hashtable();
			}
			this.excludedNamespaces[nspace] = null;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0012AE9F File Offset: 0x0012909F
		internal bool IsExcludedNamespace(string nspace)
		{
			return this.excludedNamespaces != null && this.excludedNamespaces.Contains(nspace);
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x0012AEB7 File Offset: 0x001290B7
		internal void InsertVariable(VariableAction variable)
		{
			if (this.variables == null)
			{
				this.variables = new Hashtable();
			}
			this.variables[variable.Name] = variable;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x0012AEDE File Offset: 0x001290DE
		internal int GetVeriablesCount()
		{
			if (this.variables == null)
			{
				return 0;
			}
			return this.variables.Count;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x0012AEF8 File Offset: 0x001290F8
		public VariableAction ResolveVariable(XmlQualifiedName qname)
		{
			for (InputScope inputScope = this; inputScope != null; inputScope = inputScope.Parent)
			{
				if (inputScope.Variables != null)
				{
					VariableAction variableAction = (VariableAction)inputScope.Variables[qname];
					if (variableAction != null)
					{
						return variableAction;
					}
				}
			}
			return null;
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x0012AF34 File Offset: 0x00129134
		public VariableAction ResolveGlobalVariable(XmlQualifiedName qname)
		{
			InputScope inputScope = null;
			for (InputScope inputScope2 = this; inputScope2 != null; inputScope2 = inputScope2.Parent)
			{
				inputScope = inputScope2;
			}
			return inputScope.ResolveVariable(qname);
		}

		// Token: 0x040021C8 RID: 8648
		private InputScope parent;

		// Token: 0x040021C9 RID: 8649
		private bool forwardCompatibility;

		// Token: 0x040021CA RID: 8650
		private bool canHaveApplyImports;

		// Token: 0x040021CB RID: 8651
		private Hashtable variables;

		// Token: 0x040021CC RID: 8652
		private Hashtable extensionNamespaces;

		// Token: 0x040021CD RID: 8653
		private Hashtable excludedNamespaces;
	}
}
