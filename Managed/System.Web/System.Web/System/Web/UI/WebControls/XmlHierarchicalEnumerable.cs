using System;
using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000458 RID: 1112
	internal class XmlHierarchicalEnumerable : IHierarchicalEnumerable, IEnumerable
	{
		// Token: 0x0600339C RID: 13212 RVA: 0x0008A28B File Offset: 0x0008848B
		internal XmlHierarchicalEnumerable(XmlNodeList nodeList)
		{
			this.nodeList = nodeList;
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x0008A29C File Offset: 0x0008849C
		IEnumerator IEnumerable.GetEnumerator()
		{
			ArrayList arrayList = new ArrayList(this.nodeList.Count);
			foreach (object obj in this.nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					arrayList.Add(new XmlHierarchyData(xmlNode));
				}
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x0008A31C File Offset: 0x0008851C
		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return (IHierarchyData)enumeratedItem;
		}

		// Token: 0x04001CDC RID: 7388
		private XmlNodeList nodeList;
	}
}
