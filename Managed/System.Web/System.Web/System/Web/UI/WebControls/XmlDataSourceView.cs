using System;
using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a tabular data source view on XML data for an <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	// Token: 0x02000456 RID: 1110
	public sealed class XmlDataSourceView : DataSourceView
	{
		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> class, and associates the specified <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> with it.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> that the <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> is associated with. </param>
		/// <param name="name">The name of the view. </param>
		// Token: 0x06003395 RID: 13205 RVA: 0x0008A105 File Offset: 0x00088305
		public XmlDataSourceView(XmlDataSource owner, string name)
			: base(owner, name)
		{
			this.owner = owner;
		}

		/// <summary>Retrieves a list of data rows from the underlying XML.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of data items.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that is used to request operations on the data beyond basic data retrieval.</param>
		// Token: 0x06003396 RID: 13206 RVA: 0x000720BE File Offset: 0x000702BE
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0008A118 File Offset: 0x00088318
		private void DoXPathSelect()
		{
			XmlNodeList xmlNodeList = this.owner.GetXmlDocument().SelectNodes((this.owner.XPath != "") ? this.owner.XPath : "/*/*");
			this.nodes = new ArrayList(xmlNodeList.Count);
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					this.nodes.Add(xmlNode);
				}
			}
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x0008A1C8 File Offset: 0x000883C8
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			if (this.nodes == null)
			{
				this.DoXPathSelect();
			}
			ArrayList arrayList = new ArrayList();
			int num = arguments.StartRowIndex + ((arguments.MaximumRows > 0) ? arguments.MaximumRows : this.nodes.Count);
			if (num > this.nodes.Count)
			{
				num = this.nodes.Count;
			}
			for (int i = arguments.StartRowIndex; i < num; i++)
			{
				arrayList.Add(new XmlDataSourceNodeDescriptor((XmlElement)this.nodes[i]));
			}
			if (arguments.RetrieveTotalRowCount)
			{
				arguments.TotalRowCount = this.nodes.Count;
			}
			return arrayList;
		}

		// Token: 0x04001CD9 RID: 7385
		private ArrayList nodes;

		// Token: 0x04001CDA RID: 7386
		private XmlDataSource owner;
	}
}
