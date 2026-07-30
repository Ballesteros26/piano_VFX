using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000041 RID: 65
	internal sealed class SortQuery : Query
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00006B18 File Offset: 0x00004D18
		public SortQuery(Query qyInput)
		{
			this.results = new List<SortKey>();
			this.comparer = new XPathSortComparer();
			this.qyInput = qyInput;
			this.count = 0;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006B44 File Offset: 0x00004D44
		private SortQuery(SortQuery other)
			: base(other)
		{
			this.results = new List<SortKey>(other.results);
			this.comparer = other.comparer.Clone();
			this.qyInput = Query.Clone(other.qyInput);
			this.count = 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00002979 File Offset: 0x00000B79
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006B92 File Offset: 0x00004D92
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qyInput.SetXsltContext(xsltContext);
			if (this.qyInput.StaticType != XPathResultType.NodeSet && this.qyInput.StaticType != XPathResultType.Any)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006BC8 File Offset: 0x00004DC8
		private void BuildResultsList()
		{
			int numSorts = this.comparer.NumSorts;
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				SortKey sortKey = new SortKey(numSorts, this.results.Count, xpathNavigator.Clone());
				for (int i = 0; i < numSorts; i++)
				{
					sortKey[i] = this.comparer.Expression(i).Evaluate(this.qyInput);
				}
				this.results.Add(sortKey);
			}
			this.results.Sort(this.comparer);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006C51 File Offset: 0x00004E51
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qyInput.Evaluate(context);
			this.results.Clear();
			this.BuildResultsList();
			this.count = 0;
			return this;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006C7C File Offset: 0x00004E7C
		public override XPathNavigator Advance()
		{
			if (this.count < this.results.Count)
			{
				List<SortKey> list = this.results;
				int count = this.count;
				this.count = count + 1;
				return list[count].Node;
			}
			return null;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006CBF File Offset: 0x00004EBF
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.results[this.count - 1].Node;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006CE3 File Offset: 0x00004EE3
		internal void AddSort(Query evalQuery, IComparer comparer)
		{
			this.comparer.AddSort(evalQuery, comparer);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006CF2 File Offset: 0x00004EF2
		public override XPathNodeIterator Clone()
		{
			return new SortQuery(this);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000029F5 File Offset: 0x00000BF5
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006CFA File Offset: 0x00004EFA
		public override int Count
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006D07 File Offset: 0x00004F07
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)7;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006D0A File Offset: 0x00004F0A
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.qyInput.PrintQuery(w);
			w.WriteElementString("XPathSortComparer", "... PrintTree() not implemented ...");
			w.WriteEndElement();
		}

		// Token: 0x040000F6 RID: 246
		private List<SortKey> results;

		// Token: 0x040000F7 RID: 247
		private XPathSortComparer comparer;

		// Token: 0x040000F8 RID: 248
		private Query qyInput;
	}
}
