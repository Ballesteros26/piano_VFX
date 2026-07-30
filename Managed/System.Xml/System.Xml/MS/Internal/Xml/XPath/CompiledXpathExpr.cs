using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000014 RID: 20
	internal class CompiledXpathExpr : XPathExpression
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002F10 File Offset: 0x00001110
		internal CompiledXpathExpr(Query query, string expression, bool needContext)
		{
			this.query = query;
			this.expr = expression;
			this.needContext = needContext;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F2D File Offset: 0x0000112D
		internal Query QueryTree
		{
			get
			{
				if (this.needContext)
				{
					throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
				}
				return this.query;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002F48 File Offset: 0x00001148
		public override string Expression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void CheckErrors()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F54 File Offset: 0x00001154
		public override void AddSort(object expr, IComparer comparer)
		{
			Query query;
			if (expr is string)
			{
				query = new QueryBuilder().Build((string)expr, out this.needContext);
			}
			else
			{
				if (!(expr is CompiledXpathExpr))
				{
					throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
				}
				query = ((CompiledXpathExpr)expr).QueryTree;
			}
			SortQuery sortQuery = this.query as SortQuery;
			if (sortQuery == null)
			{
				sortQuery = (this.query = new SortQuery(this.query));
			}
			sortQuery.AddSort(query, comparer);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FCD File Offset: 0x000011CD
		public override void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
		{
			this.AddSort(expr, new XPathComparerHelper(order, caseOrder, lang, dataType));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FE1 File Offset: 0x000011E1
		public override XPathExpression Clone()
		{
			return new CompiledXpathExpr(Query.Clone(this.query), this.expr, this.needContext);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FFF File Offset: 0x000011FF
		public override void SetContext(XmlNamespaceManager nsManager)
		{
			this.SetContext(nsManager);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003008 File Offset: 0x00001208
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			XsltContext xsltContext = nsResolver as XsltContext;
			if (xsltContext == null)
			{
				if (nsResolver == null)
				{
					nsResolver = new XmlNamespaceManager(new NameTable());
				}
				xsltContext = new CompiledXpathExpr.UndefinedXsltContext(nsResolver);
			}
			this.query.SetXsltContext(xsltContext);
			this.needContext = false;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003048 File Offset: 0x00001248
		public override XPathResultType ReturnType
		{
			get
			{
				return this.query.StaticType;
			}
		}

		// Token: 0x0400006F RID: 111
		private Query query;

		// Token: 0x04000070 RID: 112
		private string expr;

		// Token: 0x04000071 RID: 113
		private bool needContext;

		// Token: 0x02000015 RID: 21
		private class UndefinedXsltContext : XsltContext
		{
			// Token: 0x0600007B RID: 123 RVA: 0x00003055 File Offset: 0x00001255
			public UndefinedXsltContext(IXmlNamespaceResolver nsResolver)
				: base(false)
			{
				this.nsResolver = nsResolver;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600007C RID: 124 RVA: 0x00003065 File Offset: 0x00001265
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x0600007D RID: 125 RVA: 0x0000306C File Offset: 0x0000126C
			public override string LookupNamespace(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				string text = this.nsResolver.LookupNamespace(prefix);
				if (text == null)
				{
					throw XPathException.Create("Namespace prefix '{0}' is not defined.", prefix);
				}
				return text;
			}

			// Token: 0x0600007E RID: 126 RVA: 0x00003097 File Offset: 0x00001297
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x0600007F RID: 127 RVA: 0x00003097 File Offset: 0x00001297
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000080 RID: 128 RVA: 0x0000226C File Offset: 0x0000046C
			public override bool Whitespace
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000081 RID: 129 RVA: 0x0000226C File Offset: 0x0000046C
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return false;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000030A3 File Offset: 0x000012A3
			public override int CompareDocument(string baseUri, string nextbaseUri)
			{
				return string.CompareOrdinal(baseUri, nextbaseUri);
			}

			// Token: 0x04000072 RID: 114
			private IXmlNamespaceResolver nsResolver;
		}
	}
}
