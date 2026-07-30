using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the sequence element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The sequence requires the elements in the group to appear in the specified sequence within the containing element.</summary>
	// Token: 0x0200047B RID: 1147
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		/// <summary>The elements contained within the compositor. Collection of <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaSequence" />, or <see cref="T:System.Xml.Schema.XmlSchemaAny" />.</summary>
		/// <returns>The elements contained within the compositor.</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00107E31 File Offset: 0x00106031
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x00107E39 File Offset: 0x00106039
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x00107E53 File Offset: 0x00106053
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001DFE RID: 7678
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
