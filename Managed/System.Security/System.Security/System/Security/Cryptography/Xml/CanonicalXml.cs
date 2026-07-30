using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003F RID: 63
	internal class CanonicalXml
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000503C File Offset: 0x0000323C
		internal CanonicalXml(Stream inputStream, bool includeComments, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			this._c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005095 File Offset: 0x00003295
		internal CanonicalXml(XmlDocument document, XmlResolver resolver)
			: this(document, resolver, false)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000050A0 File Offset: 0x000032A0
		internal CanonicalXml(XmlDocument document, XmlResolver resolver, bool includeComments)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this._c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(new XmlNodeReader(document));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000050F8 File Offset: 0x000032F8
		internal CanonicalXml(XmlNodeList nodeList, XmlResolver resolver, bool includeComments)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			XmlDocument ownerDocument = Utils.GetOwnerDocument(nodeList);
			if (ownerDocument == null)
			{
				throw new ArgumentException("nodeList");
			}
			this._c14nDoc = new CanonicalXmlDocument(false, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(new XmlNodeReader(ownerDocument));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
			CanonicalXml.MarkInclusionStateForNodes(nodeList, ownerDocument, this._c14nDoc);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005170 File Offset: 0x00003370
		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005188 File Offset: 0x00003388
		private static void MarkInclusionStateForNodes(XmlNodeList nodeList, XmlDocument inputRoot, XmlDocument root)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			canonicalXmlNodeList.Add(inputRoot);
			canonicalXmlNodeList2.Add(root);
			int num = 0;
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList[num];
				XmlNode xmlNode2 = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				XmlNodeList childNodes2 = xmlNode2.ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					canonicalXmlNodeList.Add(childNodes[i]);
					canonicalXmlNodeList2.Add(childNodes2[i]);
					if (Utils.NodeInList(childNodes[i], nodeList))
					{
						CanonicalXml.MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes != null)
					{
						for (int j = 0; j < attributes.Count; j++)
						{
							if (Utils.NodeInList(attributes[j], nodeList))
							{
								CanonicalXml.MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
							}
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005294 File Offset: 0x00003494
		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, this._ancMgr);
			return new UTF8Encoding(false).GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000052CB File Offset: 0x000034CB
		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			this._c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, this._ancMgr);
			hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return array;
		}

		// Token: 0x0400010A RID: 266
		private CanonicalXmlDocument _c14nDoc;

		// Token: 0x0400010B RID: 267
		private C14NAncestralNamespaceContextManager _ancMgr;
	}
}
