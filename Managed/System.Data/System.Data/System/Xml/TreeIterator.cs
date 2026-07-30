using System;

namespace System.Xml
{
	// Token: 0x02000031 RID: 49
	internal sealed class TreeIterator : BaseTreeIterator
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00007621 File Offset: 0x00005821
		internal TreeIterator(XmlNode nodeTop)
			: base(((XmlDataDocument)nodeTop.OwnerDocument).Mapper)
		{
			this._nodeTop = nodeTop;
			this._currentNode = nodeTop;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007647 File Offset: 0x00005847
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007650 File Offset: 0x00005850
		internal override bool Next()
		{
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000767C File Offset: 0x0000587C
		internal override bool NextRight()
		{
			if (this._currentNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._nodeTop && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			return true;
		}

		// Token: 0x0400041B RID: 1051
		private readonly XmlNode _nodeTop;

		// Token: 0x0400041C RID: 1052
		private XmlNode _currentNode;
	}
}
