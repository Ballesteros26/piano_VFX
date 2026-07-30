using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004B RID: 75
	internal class CanonicalizationDispatcher
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00002050 File Offset: 0x00000250
		private CanonicalizationDispatcher()
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006196 File Offset: 0x00004396
		public static void Write(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).Write(strBuilder, docPos, anc);
				return;
			}
			CanonicalizationDispatcher.WriteGenericNode(node, strBuilder, docPos, anc);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000061B8 File Offset: 0x000043B8
		public static void WriteGenericNode(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (object obj in node.ChildNodes)
			{
				CanonicalizationDispatcher.Write((XmlNode)obj, strBuilder, docPos, anc);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006220 File Offset: 0x00004420
		public static void WriteHash(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).WriteHash(hash, docPos, anc);
				return;
			}
			CanonicalizationDispatcher.WriteHashGenericNode(node, hash, docPos, anc);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006244 File Offset: 0x00004444
		public static void WriteHashGenericNode(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (object obj in node.ChildNodes)
			{
				CanonicalizationDispatcher.WriteHash((XmlNode)obj, hash, docPos, anc);
			}
		}
	}
}
