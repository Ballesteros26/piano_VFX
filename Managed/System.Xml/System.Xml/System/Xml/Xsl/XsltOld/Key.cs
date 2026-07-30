using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053A RID: 1338
	internal class Key
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x0012F6DD File Offset: 0x0012D8DD
		public Key(XmlQualifiedName name, int matchkey, int usekey)
		{
			this.name = name;
			this.matchKey = matchkey;
			this.useKey = usekey;
			this.keyNodes = null;
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x0012F701 File Offset: 0x0012D901
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x0012F709 File Offset: 0x0012D909
		public int MatchKey
		{
			get
			{
				return this.matchKey;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x0012F711 File Offset: 0x0012D911
		public int UseKey
		{
			get
			{
				return this.useKey;
			}
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x0012F719 File Offset: 0x0012D919
		public void AddKey(XPathNavigator root, Hashtable table)
		{
			if (this.keyNodes == null)
			{
				this.keyNodes = new ArrayList();
			}
			this.keyNodes.Add(new DocumentKeyList(root, table));
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x0012F748 File Offset: 0x0012D948
		public Hashtable GetKeys(XPathNavigator root)
		{
			if (this.keyNodes != null)
			{
				for (int i = 0; i < this.keyNodes.Count; i++)
				{
					if (((DocumentKeyList)this.keyNodes[i]).RootNav.IsSamePosition(root))
					{
						return ((DocumentKeyList)this.keyNodes[i]).KeyTable;
					}
				}
			}
			return null;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x0012F7AF File Offset: 0x0012D9AF
		public Key Clone()
		{
			return new Key(this.name, this.matchKey, this.useKey);
		}

		// Token: 0x0400227A RID: 8826
		private XmlQualifiedName name;

		// Token: 0x0400227B RID: 8827
		private int matchKey;

		// Token: 0x0400227C RID: 8828
		private int useKey;

		// Token: 0x0400227D RID: 8829
		private ArrayList keyNodes;
	}
}
