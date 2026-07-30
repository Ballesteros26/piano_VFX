using System;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000543 RID: 1347
	internal class TemplateAction : TemplateBaseAction
	{
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x001318D5 File Offset: 0x0012FAD5
		internal int MatchKey
		{
			get
			{
				return this.matchKey;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x001318DD File Offset: 0x0012FADD
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x001318E5 File Offset: 0x0012FAE5
		internal double Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x001318ED File Offset: 0x0012FAED
		internal XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x001318F5 File Offset: 0x0012FAF5
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x001318FD File Offset: 0x0012FAFD
		internal int TemplateId
		{
			get
			{
				return this.templateId;
			}
			set
			{
				this.templateId = value;
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x00131908 File Offset: 0x0012FB08
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (this.matchKey == -1)
			{
				if (this.name == null)
				{
					throw XsltException.Create("The 'xsl:template' instruction must have the 'match' and/or 'name' attribute present.", Array.Empty<string>());
				}
				if (this.mode != null)
				{
					throw XsltException.Create("An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.", Array.Empty<string>());
				}
			}
			compiler.BeginTemplate(this);
			if (compiler.Recurse())
			{
				this.CompileParameters(compiler);
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
			compiler.EndTemplate();
			this.AnalyzePriority(compiler);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x00131992 File Offset: 0x0012FB92
		internal virtual void CompileSingle(Compiler compiler)
		{
			this.matchKey = compiler.AddQuery("/", false, true, true);
			this.priority = 0.5;
			base.CompileOnceTemplate(compiler);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x001319C0 File Offset: 0x0012FBC0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Match))
			{
				this.matchKey = compiler.AddQuery(value, false, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.name = compiler.CreateXPathQName(value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Priority))
			{
				this.priority = XmlConvert.ToXPathDouble(value);
				if (double.IsNaN(this.priority) && !compiler.ForwardCompatibility)
				{
					throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "priority", value });
				}
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Mode))
				{
					return false;
				}
				if (compiler.AllowBuiltInMode && value == "*")
				{
					this.mode = Compiler.BuiltInMode;
				}
				else
				{
					this.mode = compiler.CreateXPathQName(value);
				}
			}
			return true;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x00131ACC File Offset: 0x0012FCCC
		private void AnalyzePriority(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (!double.IsNaN(this.priority) || this.matchKey == -1)
			{
				return;
			}
			TheQuery theQuery = compiler.QueryStore[this.MatchKey];
			CompiledXpathExpr compiledQuery = theQuery.CompiledQuery;
			Query query = compiledQuery.QueryTree;
			UnionExpr unionExpr;
			while ((unionExpr = query as UnionExpr) != null)
			{
				TemplateAction templateAction = this.CloneWithoutName();
				compiler.QueryStore.Add(new TheQuery(new CompiledXpathExpr(unionExpr.qy2, compiledQuery.Expression, false), theQuery._ScopeManager));
				templateAction.matchKey = compiler.QueryStore.Count - 1;
				templateAction.priority = unionExpr.qy2.XsltDefaultPriority;
				compiler.AddTemplate(templateAction);
				query = unionExpr.qy1;
			}
			if (compiledQuery.QueryTree != query)
			{
				compiler.QueryStore[this.MatchKey] = new TheQuery(new CompiledXpathExpr(query, compiledQuery.Expression, false), theQuery._ScopeManager);
			}
			this.priority = query.XsltDefaultPriority;
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x00131BC8 File Offset: 0x0012FDC8
		protected void CompileParameters(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			for (;;)
			{
				switch (input.NodeType)
				{
				case XPathNodeType.Element:
					if (!Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) || !Ref.Equal(input.LocalName, input.Atoms.Param))
					{
						return;
					}
					compiler.PushNamespaceScope();
					base.AddAction(compiler.CreateVariableAction(VariableType.LocalParameter));
					compiler.PopScope();
					break;
				case XPathNodeType.Text:
					return;
				case XPathNodeType.SignificantWhitespace:
					base.AddEvent(compiler.CreateTextEvent());
					break;
				}
				if (!input.Advance())
				{
					return;
				}
			}
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x00131C65 File Offset: 0x0012FE65
		private TemplateAction CloneWithoutName()
		{
			return new TemplateAction
			{
				containedActions = this.containedActions,
				mode = this.mode,
				variableCount = this.variableCount,
				replaceNSAliasesDone = true
			};
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x00131C97 File Offset: 0x0012FE97
		internal override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (!this.replaceNSAliasesDone)
			{
				base.ReplaceNamespaceAlias(compiler);
				this.replaceNSAliasesDone = true;
			}
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x00131CB0 File Offset: 0x0012FEB0
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				frame.Finished();
				return;
			}
			else
			{
				if (this.variableCount > 0)
				{
					frame.AllocateVariables(this.variableCount);
				}
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			}
		}

		// Token: 0x040022FF RID: 8959
		private int matchKey = -1;

		// Token: 0x04002300 RID: 8960
		private XmlQualifiedName name;

		// Token: 0x04002301 RID: 8961
		private double priority = double.NaN;

		// Token: 0x04002302 RID: 8962
		private XmlQualifiedName mode;

		// Token: 0x04002303 RID: 8963
		private int templateId;

		// Token: 0x04002304 RID: 8964
		private bool replaceNSAliasesDone;
	}
}
