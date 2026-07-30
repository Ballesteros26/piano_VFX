using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000064 RID: 100
	internal abstract class XPathNodeHelper
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000C38C File Offset: 0x0000A58C
		public static int GetLocalNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].HasNamespaceDecls)
			{
				return pageElem[idxElem].Document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static int GetInScopeNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].NodeType == XPathNodeType.Element)
			{
				XPathDocument document = pageElem[idxElem].Document;
				while (!pageElem[idxElem].HasNamespaceDecls)
				{
					idxElem = pageElem[idxElem].GetParent(out pageElem);
					if (idxElem == 0)
					{
						return document.GetXmlNamespaceNode(out pageNmsp);
					}
				}
				return document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C41A File Offset: 0x0000A61A
		public static bool GetFirstAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (pageNode[idxNode].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref pageNode, ref idxNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C438 File Offset: 0x0000A638
		public static bool GetNextAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array;
			int sibling = pageNode[idxNode].GetSibling(out array);
			if (sibling != 0 && array[sibling].NodeType == XPathNodeType.Attribute)
			{
				pageNode = array;
				idxNode = sibling;
				return true;
			}
			return false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C474 File Offset: 0x0000A674
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (array[num].NodeType == XPathNodeType.Attribute)
				{
					num = array[num].GetSibling(out array);
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (!array[num].IsAttrNmsp)
			{
				num = array[num].GetSibling(out array);
				if (num != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C504 File Offset: 0x0000A704
		public static bool GetParent(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C532 File Offset: 0x0000A732
		public static int GetLocation(XPathNode[] pageNode, int idxNode)
		{
			return (pageNode[0].PageInfo.PageNumber << 16) | idxNode;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C54C File Offset: 0x0000A74C
		public static bool GetElementChild(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasElementChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].ElementMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		public static bool GetElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (!array[num].ElementMatch(localName, namespaceName));
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(typ);
				XPathNodeHelper.GetChild(ref array, ref num);
				while (((1 << (int)array[num].NodeType) & contentKindMask) == 0)
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				if (typ == XPathNodeType.Attribute)
				{
					return false;
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C65C File Offset: 0x0000A85C
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (((1 << (int)array[num].NodeType) & contentKindMask) == 0);
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public static bool GetPreviousContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				int num2 = idxNode - 1;
				XPathNode[] array2;
				if (num2 == 0)
				{
					array2 = pageNode[0].PageInfo.PreviousPage;
					num2 = array2.Length - 1;
				}
				else
				{
					array2 = pageNode;
				}
				if (num == num2 && array == array2)
				{
					return false;
				}
				XPathNode[] array3 = array2;
				int num3 = num2;
				do
				{
					array2 = array3;
					num2 = num3;
					num3 = array3[num3].GetParent(out array3);
				}
				while (num3 != num || array3 != array);
				if (array2[num2].NodeType != XPathNodeType.Attribute)
				{
					pageNode = array2;
					idxNode = num2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C754 File Offset: 0x0000A954
		public static bool GetPreviousElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				while (XPathNodeHelper.GetPreviousContentSibling(ref array, ref num))
				{
					if (array[num].ElementMatch(localName, namespaceName))
					{
						pageNode = array;
						idxNode = num;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C79C File Offset: 0x0000A99C
		public static bool GetPreviousContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			while (XPathNodeHelper.GetPreviousContentSibling(ref array, ref num))
			{
				if (((1 << (int)array[num].NodeType) & contentKindMask) != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public static bool GetAttribute(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].NameMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0 || array[num].NodeType != XPathNodeType.Attribute)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C844 File Offset: 0x0000AA44
		public static bool GetFollowing(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (++num >= array[0].PageInfo.NodeCount)
			{
				array = array[0].PageInfo.NextPage;
				num = 0;
				if (array == null)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = num;
			return true;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C890 File Offset: 0x0000AA90
		public static bool GetElementFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, string localName, string namespaceName)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			if (array[i].NodeType != XPathNodeType.Element || array[i].LocalName != localName)
			{
				i++;
				while (array != pageEnd || i > idxEnd)
				{
					while (i < array[0].PageInfo.NodeCount)
					{
						if (array[i].ElementMatch(localName, namespaceName))
						{
							goto IL_012C;
						}
						i++;
					}
					array = array[0].PageInfo.NextPage;
					i = 1;
					if (array == null)
					{
						return false;
					}
				}
				while (i != idxEnd)
				{
					if (array[i].ElementMatch(localName, namespaceName))
					{
						goto IL_012C;
					}
					i++;
				}
				return false;
			}
			int num = 0;
			if (pageEnd != null)
			{
				num = pageEnd[0].PageInfo.PageNumber;
				int num2 = array[0].PageInfo.PageNumber;
				if (num2 > num || (num2 == num && i >= idxEnd))
				{
					pageEnd = null;
				}
			}
			do
			{
				i = array[i].GetSimilarElement(out array);
				if (i == 0)
				{
					return false;
				}
				if (pageEnd != null)
				{
					int num2 = array[0].PageInfo.PageNumber;
					if (num2 > num || (num2 == num && i >= idxEnd))
					{
						return false;
					}
				}
			}
			while (array[i].LocalName != localName || !(array[i].NamespaceUri == namespaceName));
			IL_012C:
			pageCurrent = array;
			idxCurrent = i;
			return true;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public static bool GetContentFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, XPathNodeType typ)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (((1 << (int)array[i].NodeType) & contentKindMask) != 0)
					{
						goto IL_0081;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_0081:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (((1 << (int)array[i].NodeType) & contentKindMask) != 0)
				{
					goto IL_0081;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public static bool GetTextFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
					{
						goto IL_00AB;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_00AB:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
				{
					goto IL_00AB;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public static bool GetNonDescendant(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (!array[num].HasSibling)
			{
				num = array[num].GetParent(out array);
				if (num == 0)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = array[num].GetSibling(out pageNode);
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000CB70 File Offset: 0x0000AD70
		private static void GetChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			int num = idxNode + 1;
			idxNode = num;
			if (num >= pageNode.Length)
			{
				pageNode = pageNode[0].PageInfo.NextPage;
				idxNode = 1;
			}
		}
	}
}
