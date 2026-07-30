using System;
using System.Collections;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	// Token: 0x020002BC RID: 700
	internal class XPathNavigatorKeyComparer : IEqualityComparer
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x000929D0 File Offset: 0x00090BD0
		bool IEqualityComparer.Equals(object obj1, object obj2)
		{
			XPathNavigator xpathNavigator = obj1 as XPathNavigator;
			XPathNavigator xpathNavigator2 = obj2 as XPathNavigator;
			return xpathNavigator != null && xpathNavigator2 != null && xpathNavigator.IsSamePosition(xpathNavigator2);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00092A00 File Offset: 0x00090C00
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			XPathDocumentNavigator xpathDocumentNavigator;
			int num;
			XPathNavigator xpathNavigator;
			if ((xpathDocumentNavigator = obj as XPathDocumentNavigator) != null)
			{
				num = xpathDocumentNavigator.GetPositionHashCode();
			}
			else if ((xpathNavigator = obj as XPathNavigator) != null)
			{
				object underlyingObject = xpathNavigator.UnderlyingObject;
				if (underlyingObject != null)
				{
					num = underlyingObject.GetHashCode();
				}
				else
				{
					num = (int)xpathNavigator.NodeType;
					num ^= xpathNavigator.LocalName.GetHashCode();
					num ^= xpathNavigator.Prefix.GetHashCode();
					num ^= xpathNavigator.NamespaceURI.GetHashCode();
				}
			}
			else
			{
				num = obj.GetHashCode();
			}
			return num;
		}
	}
}
