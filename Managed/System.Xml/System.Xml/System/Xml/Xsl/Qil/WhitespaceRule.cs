using System;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000655 RID: 1621
	internal class WhitespaceRule
	{
		// Token: 0x0600412D RID: 16685 RVA: 0x000020FD File Offset: 0x000002FD
		protected WhitespaceRule()
		{
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0015BD51 File Offset: 0x00159F51
		public WhitespaceRule(string localName, string namespaceName, bool preserveSpace)
		{
			this.Init(localName, namespaceName, preserveSpace);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x0015BD62 File Offset: 0x00159F62
		protected void Init(string localName, string namespaceName, bool preserveSpace)
		{
			this.localName = localName;
			this.namespaceName = namespaceName;
			this.preserveSpace = preserveSpace;
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x0015BD79 File Offset: 0x00159F79
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x0015BD81 File Offset: 0x00159F81
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x0015BD8A File Offset: 0x00159F8A
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x0015BD92 File Offset: 0x00159F92
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
			set
			{
				this.namespaceName = value;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x0015BD9B File Offset: 0x00159F9B
		public bool PreserveSpace
		{
			get
			{
				return this.preserveSpace;
			}
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0015BDA3 File Offset: 0x00159FA3
		public void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.WriteStringQ(this.localName);
			writer.WriteStringQ(this.namespaceName);
			writer.Write(this.preserveSpace);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0015BDC9 File Offset: 0x00159FC9
		public WhitespaceRule(XmlQueryDataReader reader)
		{
			this.localName = reader.ReadStringQ();
			this.namespaceName = reader.ReadStringQ();
			this.preserveSpace = reader.ReadBoolean();
		}

		// Token: 0x040028F6 RID: 10486
		private string localName;

		// Token: 0x040028F7 RID: 10487
		private string namespaceName;

		// Token: 0x040028F8 RID: 10488
		private bool preserveSpace;
	}
}
