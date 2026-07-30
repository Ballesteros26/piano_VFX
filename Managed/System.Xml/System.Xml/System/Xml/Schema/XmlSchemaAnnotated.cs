using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for any element that can contain annotation elements.</summary>
	// Token: 0x02000436 RID: 1078
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x001049B2 File Offset: 0x00102BB2
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x001049BA File Offset: 0x00102BBA
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

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> representing the annotation property.</returns>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x001049C3 File Offset: 0x00102BC3
		// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x001049CB File Offset: 0x00102BCB
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the current schema's target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x001049D4 File Offset: 0x00102BD4
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x001049DC File Offset: 0x00102BDC
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

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x001049E5 File Offset: 0x00102BE5
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x001049ED File Offset: 0x00102BED
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

		// Token: 0x06002AC5 RID: 10949 RVA: 0x001049DC File Offset: 0x00102BDC
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x001049CB File Offset: 0x00102BCB
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001D1F RID: 7455
		private string id;

		// Token: 0x04001D20 RID: 7456
		private XmlSchemaAnnotation annotation;

		// Token: 0x04001D21 RID: 7457
		private XmlAttribute[] moreAttributes;
	}
}
