using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000A RID: 10
	internal class Axis : AstNode
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002217 File Offset: 0x00000417
		public Axis(Axis.AxisType axisType, AstNode input, string prefix, string name, XPathNodeType nodetype)
		{
			this.axisType = axisType;
			this.input = input;
			this.prefix = prefix;
			this.name = name;
			this.nodeType = nodetype;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000224F File Offset: 0x0000044F
		public Axis(Axis.AxisType axisType, AstNode input)
			: this(axisType, input, string.Empty, string.Empty, XPathNodeType.All)
		{
			this.abbrAxis = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000226C File Offset: 0x0000046C
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Axis;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002272 File Offset: 0x00000472
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000227A File Offset: 0x0000047A
		public AstNode Input
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002283 File Offset: 0x00000483
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000228B File Offset: 0x0000048B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002293 File Offset: 0x00000493
		public XPathNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000229B File Offset: 0x0000049B
		public Axis.AxisType TypeOfAxis
		{
			get
			{
				return this.axisType;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022A3 File Offset: 0x000004A3
		public bool AbbrAxis
		{
			get
			{
				return this.abbrAxis;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022AB File Offset: 0x000004AB
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000022B3 File Offset: 0x000004B3
		public string Urn
		{
			get
			{
				return this.urn;
			}
			set
			{
				this.urn = value;
			}
		}

		// Token: 0x04000044 RID: 68
		private Axis.AxisType axisType;

		// Token: 0x04000045 RID: 69
		private AstNode input;

		// Token: 0x04000046 RID: 70
		private string prefix;

		// Token: 0x04000047 RID: 71
		private string name;

		// Token: 0x04000048 RID: 72
		private XPathNodeType nodeType;

		// Token: 0x04000049 RID: 73
		protected bool abbrAxis;

		// Token: 0x0400004A RID: 74
		private string urn = string.Empty;

		// Token: 0x0200000B RID: 11
		public enum AxisType
		{
			// Token: 0x0400004C RID: 76
			Ancestor,
			// Token: 0x0400004D RID: 77
			AncestorOrSelf,
			// Token: 0x0400004E RID: 78
			Attribute,
			// Token: 0x0400004F RID: 79
			Child,
			// Token: 0x04000050 RID: 80
			Descendant,
			// Token: 0x04000051 RID: 81
			DescendantOrSelf,
			// Token: 0x04000052 RID: 82
			Following,
			// Token: 0x04000053 RID: 83
			FollowingSibling,
			// Token: 0x04000054 RID: 84
			Namespace,
			// Token: 0x04000055 RID: 85
			Parent,
			// Token: 0x04000056 RID: 86
			Preceding,
			// Token: 0x04000057 RID: 87
			PrecedingSibling,
			// Token: 0x04000058 RID: 88
			Self,
			// Token: 0x04000059 RID: 89
			None
		}
	}
}
