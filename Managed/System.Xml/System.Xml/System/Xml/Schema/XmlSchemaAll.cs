using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) all element (compositor).</summary>
	// Token: 0x02000435 RID: 1077
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of XmlSchemaElement elements contained within the all compositor.</summary>
		/// <returns>The collection of elements contained in XmlSchemaAll.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x00104974 File Offset: 0x00102B74
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0010497C File Offset: 0x00102B7C
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x00104996 File Offset: 0x00102B96
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001D1E RID: 7454
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
