using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000C RID: 12
	internal abstract class BaseAxisQuery : Query
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000022BC File Offset: 0x000004BC
		protected BaseAxisQuery(Query qyInput)
		{
			this.name = string.Empty;
			this.prefix = string.Empty;
			this.nsUri = string.Empty;
			this.qyInput = qyInput;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022EC File Offset: 0x000004EC
		protected BaseAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
		{
			this.qyInput = qyInput;
			this.name = name;
			this.prefix = prefix;
			this.typeTest = typeTest;
			this.nameTest = prefix.Length != 0 || name.Length != 0;
			this.nsUri = string.Empty;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002344 File Offset: 0x00000544
		protected BaseAxisQuery(BaseAxisQuery other)
			: base(other)
		{
			this.qyInput = Query.Clone(other.qyInput);
			this.name = other.name;
			this.prefix = other.prefix;
			this.nsUri = other.nsUri;
			this.typeTest = other.typeTest;
			this.nameTest = other.nameTest;
			this.position = other.position;
			this.currentNode = other.currentNode;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023BD File Offset: 0x000005BD
		public override void Reset()
		{
			this.position = 0;
			this.currentNode = null;
			this.qyInput.Reset();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023D8 File Offset: 0x000005D8
		public override void SetXsltContext(XsltContext context)
		{
			this.nsUri = context.LookupNamespace(this.prefix);
			this.qyInput.SetXsltContext(context);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023F8 File Offset: 0x000005F8
		protected string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002400 File Offset: 0x00000600
		protected string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002408 File Offset: 0x00000608
		protected string Namespace
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002410 File Offset: 0x00000610
		protected bool NameTest
		{
			get
			{
				return this.nameTest;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002418 File Offset: 0x00000618
		protected XPathNodeType TypeTest
		{
			get
			{
				return this.typeTest;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002420 File Offset: 0x00000620
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002428 File Offset: 0x00000628
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002430 File Offset: 0x00000630
		public virtual bool matches(XPathNavigator e)
		{
			if (this.TypeTest == e.NodeType || this.TypeTest == XPathNodeType.All || (this.TypeTest == XPathNodeType.Text && (e.NodeType == XPathNodeType.Whitespace || e.NodeType == XPathNodeType.SignificantWhitespace)))
			{
				if (!this.NameTest)
				{
					return true;
				}
				if ((this.name.Equals(e.LocalName) || this.name.Length == 0) && this.nsUri.Equals(e.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024B0 File Offset: 0x000006B0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.ResetCount();
			this.Reset();
			this.qyInput.Evaluate(nodeIterator);
			return this;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000024CC File Offset: 0x000006CC
		public override double XsltDefaultPriority
		{
			get
			{
				if (this.qyInput.GetType() != typeof(ContextQuery))
				{
					return 0.5;
				}
				if (this.name.Length != 0)
				{
					return 0.0;
				}
				if (this.prefix.Length != 0)
				{
					return -0.25;
				}
				return -0.5;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002538 File Offset: 0x00000738
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.NameTest)
			{
				w.WriteAttributeString("name", (this.Prefix.Length != 0) ? (this.Prefix + ":" + this.Name) : this.Name);
			}
			if (this.TypeTest != XPathNodeType.Element)
			{
				w.WriteAttributeString("nodeType", this.TypeTest.ToString());
			}
			this.qyInput.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x0400005A RID: 90
		internal Query qyInput;

		// Token: 0x0400005B RID: 91
		private bool nameTest;

		// Token: 0x0400005C RID: 92
		private string name;

		// Token: 0x0400005D RID: 93
		private string prefix;

		// Token: 0x0400005E RID: 94
		private string nsUri;

		// Token: 0x0400005F RID: 95
		private XPathNodeType typeTest;

		// Token: 0x04000060 RID: 96
		protected XPathNavigator currentNode;

		// Token: 0x04000061 RID: 97
		protected int position;
	}
}
