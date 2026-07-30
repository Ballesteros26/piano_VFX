using System;
using System.Xml;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data view on an XML node or collection of XML nodes for an <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	// Token: 0x02000457 RID: 1111
	public class XmlHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06003399 RID: 13209 RVA: 0x0008A26F File Offset: 0x0008846F
		internal XmlHierarchicalDataSourceView(XmlNodeList nodeList)
		{
			this.nodeList = nodeList;
		}

		/// <summary>Gets a list of the data items from the underlying data source.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of data items based on the hierarchical level of the current view.</returns>
		// Token: 0x0600339A RID: 13210 RVA: 0x0008A27E File Offset: 0x0008847E
		public override IHierarchicalEnumerable Select()
		{
			return new XmlHierarchicalEnumerable(this.nodeList);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal XmlHierarchicalDataSourceView()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001CDB RID: 7387
		private XmlNodeList nodeList;
	}
}
