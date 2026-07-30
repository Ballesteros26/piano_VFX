using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) annotation element.</summary>
	// Token: 0x02000437 RID: 1079
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x001049FE File Offset: 0x00102BFE
		// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x00104A06 File Offset: 0x00102C06
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets the Items collection that is used to store the appinfo and documentation child elements.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of appinfo and documentation child elements.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x00104A0F File Offset: 0x00102C0F
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the schema's target namespace.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x00104A17 File Offset: 0x00102C17
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x00104A1F File Offset: 0x00102C1F
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x00104A28 File Offset: 0x00102C28
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x00104A30 File Offset: 0x00102C30
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00104A1F File Offset: 0x00102C1F
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x04001D22 RID: 7458
		private string id;

		// Token: 0x04001D23 RID: 7459
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04001D24 RID: 7460
		private XmlAttribute[] moreAttributes;
	}
}
