using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000541 RID: 1345
	internal class Stylesheet
	{
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x00131230 File Offset: 0x0012F430
		internal bool Whitespace
		{
			get
			{
				return this.whitespace;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x00131238 File Offset: 0x0012F438
		internal ArrayList Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x00131240 File Offset: 0x0012F440
		internal Hashtable AttributeSetTable
		{
			get
			{
				return this.attributeSetTable;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x00131248 File Offset: 0x0012F448
		internal void AddSpace(Compiler compiler, string query, double Priority, bool PreserveSpace)
		{
			Stylesheet.WhitespaceElement whitespaceElement;
			if (this.queryKeyTable != null)
			{
				if (this.queryKeyTable.Contains(query))
				{
					whitespaceElement = (Stylesheet.WhitespaceElement)this.queryKeyTable[query];
					whitespaceElement.ReplaceValue(PreserveSpace);
					return;
				}
			}
			else
			{
				this.queryKeyTable = new Hashtable();
				this.whitespaceList = new ArrayList();
			}
			whitespaceElement = new Stylesheet.WhitespaceElement(compiler.AddQuery(query), Priority, PreserveSpace);
			this.queryKeyTable[query] = whitespaceElement;
			this.whitespaceList.Add(whitespaceElement);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x001312C8 File Offset: 0x0012F4C8
		internal void SortWhiteSpace()
		{
			if (this.queryKeyTable != null)
			{
				for (int i = 0; i < this.whitespaceList.Count; i++)
				{
					for (int j = this.whitespaceList.Count - 1; j > i; j--)
					{
						Stylesheet.WhitespaceElement whitespaceElement = (Stylesheet.WhitespaceElement)this.whitespaceList[j - 1];
						Stylesheet.WhitespaceElement whitespaceElement2 = (Stylesheet.WhitespaceElement)this.whitespaceList[j];
						if (whitespaceElement2.Priority < whitespaceElement.Priority)
						{
							this.whitespaceList[j - 1] = whitespaceElement2;
							this.whitespaceList[j] = whitespaceElement;
						}
					}
				}
				this.whitespace = true;
			}
			if (this.imports != null)
			{
				for (int k = this.imports.Count - 1; k >= 0; k--)
				{
					Stylesheet stylesheet = (Stylesheet)this.imports[k];
					if (stylesheet.Whitespace)
					{
						stylesheet.SortWhiteSpace();
						this.whitespace = true;
					}
				}
			}
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x001313B4 File Offset: 0x0012F5B4
		internal bool PreserveWhiteSpace(Processor proc, XPathNavigator node)
		{
			if (this.whitespaceList != null)
			{
				int num = this.whitespaceList.Count - 1;
				while (0 <= num)
				{
					Stylesheet.WhitespaceElement whitespaceElement = (Stylesheet.WhitespaceElement)this.whitespaceList[num];
					if (proc.Matches(node, whitespaceElement.Key))
					{
						return whitespaceElement.PreserveSpace;
					}
					num--;
				}
			}
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					if (!((Stylesheet)this.imports[i]).PreserveWhiteSpace(proc, node))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00131448 File Offset: 0x0012F648
		internal void AddAttributeSet(AttributeSetAction attributeSet)
		{
			if (this.attributeSetTable == null)
			{
				this.attributeSetTable = new Hashtable();
			}
			if (!this.attributeSetTable.ContainsKey(attributeSet.Name))
			{
				this.attributeSetTable[attributeSet.Name] = attributeSet;
				return;
			}
			((AttributeSetAction)this.attributeSetTable[attributeSet.Name]).Merge(attributeSet);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x001314AC File Offset: 0x0012F6AC
		internal void AddTemplate(TemplateAction template)
		{
			XmlQualifiedName xmlQualifiedName = template.Mode;
			if (template.Name != null)
			{
				if (this.templateNameTable.ContainsKey(template.Name))
				{
					throw XsltException.Create("'{0}' is a duplicate template name.", new string[] { template.Name.ToString() });
				}
				this.templateNameTable[template.Name] = template;
			}
			if (template.MatchKey != -1)
			{
				if (this.modeManagers == null)
				{
					this.modeManagers = new Hashtable();
				}
				if (xmlQualifiedName == null)
				{
					xmlQualifiedName = XmlQualifiedName.Empty;
				}
				TemplateManager templateManager = (TemplateManager)this.modeManagers[xmlQualifiedName];
				if (templateManager == null)
				{
					templateManager = new TemplateManager(this, xmlQualifiedName);
					this.modeManagers[xmlQualifiedName] = templateManager;
					if (xmlQualifiedName.IsEmpty)
					{
						this.templates = templateManager;
					}
				}
				int num = this.templateCount + 1;
				this.templateCount = num;
				template.TemplateId = num;
				templateManager.AddTemplate(template);
			}
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x00131598 File Offset: 0x0012F798
		internal void ProcessTemplates()
		{
			if (this.modeManagers != null)
			{
				IDictionaryEnumerator enumerator = this.modeManagers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((TemplateManager)enumerator.Value).ProcessTemplates();
				}
			}
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					((Stylesheet)this.imports[i]).ProcessTemplates();
				}
			}
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x0013160C File Offset: 0x0012F80C
		internal void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.modeManagers != null)
			{
				IDictionaryEnumerator enumerator = this.modeManagers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					TemplateManager templateManager = (TemplateManager)enumerator.Value;
					if (templateManager.templates != null)
					{
						for (int i = 0; i < templateManager.templates.Count; i++)
						{
							((TemplateAction)templateManager.templates[i]).ReplaceNamespaceAlias(compiler);
						}
					}
				}
			}
			if (this.templateNameTable != null)
			{
				IDictionaryEnumerator enumerator2 = this.templateNameTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					((TemplateAction)enumerator2.Value).ReplaceNamespaceAlias(compiler);
				}
			}
			if (this.imports != null)
			{
				for (int j = this.imports.Count - 1; j >= 0; j--)
				{
					((Stylesheet)this.imports[j]).ReplaceNamespaceAlias(compiler);
				}
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x001316E4 File Offset: 0x0012F8E4
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator, XmlQualifiedName mode)
		{
			TemplateAction templateAction = null;
			if (this.modeManagers != null)
			{
				TemplateManager templateManager = (TemplateManager)this.modeManagers[mode];
				if (templateManager != null)
				{
					templateAction = templateManager.FindTemplate(processor, navigator);
				}
			}
			if (templateAction == null)
			{
				templateAction = this.FindTemplateImports(processor, navigator, mode);
			}
			return templateAction;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x00131728 File Offset: 0x0012F928
		internal TemplateAction FindTemplateImports(Processor processor, XPathNavigator navigator, XmlQualifiedName mode)
		{
			TemplateAction templateAction = null;
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(processor, navigator, mode);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x00131778 File Offset: 0x0012F978
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator)
		{
			TemplateAction templateAction = null;
			if (this.templates != null)
			{
				templateAction = this.templates.FindTemplate(processor, navigator);
			}
			if (templateAction == null)
			{
				templateAction = this.FindTemplateImports(processor, navigator);
			}
			return templateAction;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x001317AC File Offset: 0x0012F9AC
		internal TemplateAction FindTemplate(XmlQualifiedName name)
		{
			TemplateAction templateAction = null;
			if (this.templateNameTable != null)
			{
				templateAction = (TemplateAction)this.templateNameTable[name];
			}
			if (templateAction == null && this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(name);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00131818 File Offset: 0x0012FA18
		internal TemplateAction FindTemplateImports(Processor processor, XPathNavigator navigator)
		{
			TemplateAction templateAction = null;
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(processor, navigator);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x00131866 File Offset: 0x0012FA66
		internal Hashtable ScriptObjectTypes
		{
			get
			{
				return this.scriptObjectTypes;
			}
		}

		// Token: 0x040022F2 RID: 8946
		private ArrayList imports = new ArrayList();

		// Token: 0x040022F3 RID: 8947
		private Hashtable modeManagers;

		// Token: 0x040022F4 RID: 8948
		private Hashtable templateNameTable = new Hashtable();

		// Token: 0x040022F5 RID: 8949
		private Hashtable attributeSetTable;

		// Token: 0x040022F6 RID: 8950
		private int templateCount;

		// Token: 0x040022F7 RID: 8951
		private Hashtable queryKeyTable;

		// Token: 0x040022F8 RID: 8952
		private ArrayList whitespaceList;

		// Token: 0x040022F9 RID: 8953
		private bool whitespace;

		// Token: 0x040022FA RID: 8954
		private Hashtable scriptObjectTypes = new Hashtable();

		// Token: 0x040022FB RID: 8955
		private TemplateManager templates;

		// Token: 0x02000542 RID: 1346
		private class WhitespaceElement
		{
			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x06003684 RID: 13956 RVA: 0x00131897 File Offset: 0x0012FA97
			internal double Priority
			{
				get
				{
					return this.priority;
				}
			}

			// Token: 0x17000B85 RID: 2949
			// (get) Token: 0x06003685 RID: 13957 RVA: 0x0013189F File Offset: 0x0012FA9F
			internal int Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x06003686 RID: 13958 RVA: 0x001318A7 File Offset: 0x0012FAA7
			internal bool PreserveSpace
			{
				get
				{
					return this.preserveSpace;
				}
			}

			// Token: 0x06003687 RID: 13959 RVA: 0x001318AF File Offset: 0x0012FAAF
			internal WhitespaceElement(int Key, double priority, bool PreserveSpace)
			{
				this.key = Key;
				this.priority = priority;
				this.preserveSpace = PreserveSpace;
			}

			// Token: 0x06003688 RID: 13960 RVA: 0x001318CC File Offset: 0x0012FACC
			internal void ReplaceValue(bool PreserveSpace)
			{
				this.preserveSpace = PreserveSpace;
			}

			// Token: 0x040022FC RID: 8956
			private int key;

			// Token: 0x040022FD RID: 8957
			private double priority;

			// Token: 0x040022FE RID: 8958
			private bool preserveSpace;
		}
	}
}
