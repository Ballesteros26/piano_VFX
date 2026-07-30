using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the choice element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The choice allows only one of its children to appear in an instance. </summary>
	// Token: 0x0200043E RID: 1086
	public class XmlSchemaChoice : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of the elements contained with the compositor (choice): XmlSchemaElement, XmlSchemaGroupRef, XmlSchemaChoice, XmlSchemaSequence, or XmlSchemaAny.</summary>
		/// <returns>The collection of elements contained within XmlSchemaChoice.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x00105075 File Offset: 0x00103275
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0010507D File Offset: 0x0010327D
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x00105085 File Offset: 0x00103285
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001D41 RID: 7489
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
