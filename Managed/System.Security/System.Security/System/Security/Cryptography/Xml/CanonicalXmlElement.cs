using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000044 RID: 68
	internal class CanonicalXmlElement : XmlElement, ICanonicalizableNode
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000575A File Offset: 0x0000395A
		public CanonicalXmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(prefix, localName, namespaceURI, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000576F File Offset: 0x0000396F
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005777 File Offset: 0x00003977
		public bool IsInNodeSet
		{
			get
			{
				return this._isInNodeSet;
			}
			set
			{
				this._isInNodeSet = value;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005780 File Offset: 0x00003980
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable hashtable = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, hashtable);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, hashtable);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string text = ((this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns");
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(text);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, hashtable);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, hashtable);
				strBuilder.Append("<" + this.Name);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				strBuilder.Append(">");
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(hashtable);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (object obj4 in this.ChildNodes)
			{
				CanonicalizationDispatcher.Write((XmlNode)obj4, strBuilder, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				strBuilder.Append("</" + this.Name + ">");
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005A20 File Offset: 0x00003C20
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable hashtable = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, hashtable);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, hashtable);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string text = ((this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns");
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(text);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, hashtable);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, hashtable);
				byte[] array = utf8Encoding.GetBytes("<" + this.Name);
				hash.TransformBlock(array, 0, array.Length, array, 0);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				array = utf8Encoding.GetBytes(">");
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(hashtable);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (object obj4 in this.ChildNodes)
			{
				CanonicalizationDispatcher.WriteHash((XmlNode)obj4, hash, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				byte[] array = utf8Encoding.GetBytes("</" + this.Name + ">");
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
		}

		// Token: 0x04000113 RID: 275
		private bool _isInNodeSet;
	}
}
