using System;
using System.Globalization;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200052F RID: 1327
	internal class OutputScopeManager
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x0012C90A File Offset: 0x0012AB0A
		internal string DefaultNamespace
		{
			get
			{
				return this.defaultNS;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x0012C912 File Offset: 0x0012AB12
		internal OutputScope CurrentElementScope
		{
			get
			{
				return (OutputScope)this.elementScopesStack.Peek();
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x0012C924 File Offset: 0x0012AB24
		internal XmlSpace XmlSpace
		{
			get
			{
				return this.CurrentElementScope.Space;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x0012C931 File Offset: 0x0012AB31
		internal string XmlLang
		{
			get
			{
				return this.CurrentElementScope.Lang;
			}
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x0012C940 File Offset: 0x0012AB40
		internal OutputScopeManager(XmlNameTable nameTable, OutKeywords atoms)
		{
			this.elementScopesStack = new HWStack(10);
			this.nameTable = nameTable;
			this.atoms = atoms;
			this.defaultNS = this.atoms.Empty;
			OutputScope outputScope = (OutputScope)this.elementScopesStack.Push();
			if (outputScope == null)
			{
				outputScope = new OutputScope();
				this.elementScopesStack.AddToTop(outputScope);
			}
			outputScope.Init(string.Empty, string.Empty, string.Empty, XmlSpace.None, string.Empty, false);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x0012C9C1 File Offset: 0x0012ABC1
		internal void PushNamespace(string prefix, string nspace)
		{
			this.CurrentElementScope.AddNamespace(prefix, nspace, this.defaultNS);
			if (prefix == null || prefix.Length == 0)
			{
				this.defaultNS = nspace;
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x0012C9EC File Offset: 0x0012ABEC
		internal void PushScope(string name, string nspace, string prefix)
		{
			OutputScope currentElementScope = this.CurrentElementScope;
			OutputScope outputScope = (OutputScope)this.elementScopesStack.Push();
			if (outputScope == null)
			{
				outputScope = new OutputScope();
				this.elementScopesStack.AddToTop(outputScope);
			}
			outputScope.Init(name, nspace, prefix, currentElementScope.Space, currentElementScope.Lang, currentElementScope.Mixed);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x0012CA44 File Offset: 0x0012AC44
		internal void PopScope()
		{
			for (NamespaceDecl namespaceDecl = ((OutputScope)this.elementScopesStack.Pop()).Scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				this.defaultNS = namespaceDecl.PrevDefaultNsUri;
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x0012CA80 File Offset: 0x0012AC80
		internal string ResolveNamespace(string prefix)
		{
			bool flag;
			return this.ResolveNamespace(prefix, out flag);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x0012CA98 File Offset: 0x0012AC98
		internal string ResolveNamespace(string prefix, out bool thisScope)
		{
			thisScope = true;
			if (prefix == null || prefix.Length == 0)
			{
				return this.defaultNS;
			}
			if (Ref.Equal(prefix, this.atoms.Xml))
			{
				return this.atoms.XmlNamespace;
			}
			if (Ref.Equal(prefix, this.atoms.Xmlns))
			{
				return this.atoms.XmlnsNamespace;
			}
			for (int i = this.elementScopesStack.Length - 1; i >= 0; i--)
			{
				string text = ((OutputScope)this.elementScopesStack[i]).ResolveAtom(prefix);
				if (text != null)
				{
					thisScope = i == this.elementScopesStack.Length - 1;
					return text;
				}
			}
			return null;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x0012CB40 File Offset: 0x0012AD40
		internal bool FindPrefix(string nspace, out string prefix)
		{
			int num = this.elementScopesStack.Length - 1;
			while (0 <= num)
			{
				OutputScope outputScope = (OutputScope)this.elementScopesStack[num];
				string text = null;
				if (outputScope.FindPrefix(nspace, out text))
				{
					string text2 = this.ResolveNamespace(text);
					if (text2 != null && Ref.Equal(text2, nspace))
					{
						prefix = text;
						return true;
					}
					break;
				}
				else
				{
					num--;
				}
			}
			prefix = null;
			return false;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x0012CBA0 File Offset: 0x0012ADA0
		internal string GeneratePrefix(string format)
		{
			string text;
			do
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				int num = this.prefixIndex;
				this.prefixIndex = num + 1;
				text = string.Format(invariantCulture, format, num);
			}
			while (this.nameTable.Get(text) != null);
			return this.nameTable.Add(text);
		}

		// Token: 0x04002215 RID: 8725
		private const int STACK_INCREMENT = 10;

		// Token: 0x04002216 RID: 8726
		private HWStack elementScopesStack;

		// Token: 0x04002217 RID: 8727
		private string defaultNS;

		// Token: 0x04002218 RID: 8728
		private OutKeywords atoms;

		// Token: 0x04002219 RID: 8729
		private XmlNameTable nameTable;

		// Token: 0x0400221A RID: 8730
		private int prefixIndex;
	}
}
