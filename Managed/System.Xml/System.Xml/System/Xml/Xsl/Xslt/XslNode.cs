using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000595 RID: 1429
	internal class XslNode
	{
		// Token: 0x060038AD RID: 14509 RVA: 0x0013EE0D File Offset: 0x0013D00D
		public XslNode(XslNodeType nodeType, QilName name, object arg, XslVersion xslVer)
		{
			this.NodeType = nodeType;
			this.Name = name;
			this.Arg = arg;
			this.XslVersion = xslVer;
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x0013EE32 File Offset: 0x0013D032
		public XslNode(XslNodeType nodeType)
		{
			this.NodeType = nodeType;
			this.XslVersion = XslVersion.Version10;
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x0013EE48 File Offset: 0x0013D048
		public string Select
		{
			get
			{
				return (string)this.Arg;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x0013EE55 File Offset: 0x0013D055
		public bool ForwardsCompatible
		{
			get
			{
				return this.XslVersion == XslVersion.ForwardsCompatible;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x0013EE60 File Offset: 0x0013D060
		public IList<XslNode> Content
		{
			get
			{
				IList<XslNode> list = this.content;
				return list ?? XslNode.EmptyList;
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x0013EE7E File Offset: 0x0013D07E
		public void SetContent(List<XslNode> content)
		{
			this.content = content;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x0013EE87 File Offset: 0x0013D087
		public void AddContent(XslNode node)
		{
			if (this.content == null)
			{
				this.content = new List<XslNode>();
			}
			this.content.Add(node);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x0013EEA8 File Offset: 0x0013D0A8
		public void InsertContent(IEnumerable<XslNode> collection)
		{
			if (this.content == null)
			{
				this.content = new List<XslNode>(collection);
				return;
			}
			this.content.InsertRange(0, collection);
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x0000365F File Offset: 0x0000185F
		internal string TraceName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040024E9 RID: 9449
		public readonly XslNodeType NodeType;

		// Token: 0x040024EA RID: 9450
		public ISourceLineInfo SourceLine;

		// Token: 0x040024EB RID: 9451
		public NsDecl Namespaces;

		// Token: 0x040024EC RID: 9452
		public readonly QilName Name;

		// Token: 0x040024ED RID: 9453
		public readonly object Arg;

		// Token: 0x040024EE RID: 9454
		public readonly XslVersion XslVersion;

		// Token: 0x040024EF RID: 9455
		public XslFlags Flags;

		// Token: 0x040024F0 RID: 9456
		private List<XslNode> content;

		// Token: 0x040024F1 RID: 9457
		private static readonly IList<XslNode> EmptyList = new List<XslNode>().AsReadOnly();
	}
}
