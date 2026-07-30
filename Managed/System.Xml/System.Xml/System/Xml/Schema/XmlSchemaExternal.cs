using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class. Provides information about the included schema.</summary>
	// Token: 0x02000450 RID: 1104
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) location for the schema, which tells the schema processor where the schema physically resides.</summary>
		/// <returns>The URI location for the schema.Optional for imported schemas.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x00106D7D File Offset: 0x00104F7D
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x00106D85 File Offset: 0x00104F85
		[XmlAttribute("schemaLocation", DataType = "anyURI")]
		public string SchemaLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>Gets or sets the XmlSchema for the referenced schema.</summary>
		/// <returns>The XmlSchema for the referenced schema.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x00106D8E File Offset: 0x00104F8E
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x00106D96 File Offset: 0x00104F96
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x00106D9F File Offset: 0x00104F9F
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x00106DA7 File Offset: 0x00104FA7
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

		/// <summary>Gets and sets the qualified attributes, which do not belong to the schema target namespace.</summary>
		/// <returns>Qualified attributes that belong to another target namespace.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x00106DB0 File Offset: 0x00104FB0
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x00106DB8 File Offset: 0x00104FB8
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

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x00106DC1 File Offset: 0x00104FC1
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x00106DC9 File Offset: 0x00104FC9
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x00106DD2 File Offset: 0x00104FD2
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x00106DDA File Offset: 0x00104FDA
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

		// Token: 0x06002C21 RID: 11297 RVA: 0x00106DB8 File Offset: 0x00104FB8
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x00106DE3 File Offset: 0x00104FE3
		// (set) Token: 0x06002C23 RID: 11299 RVA: 0x00106DEB File Offset: 0x00104FEB
		internal Compositor Compositor
		{
			get
			{
				return this.compositor;
			}
			set
			{
				this.compositor = value;
			}
		}

		// Token: 0x04001D9E RID: 7582
		private string location;

		// Token: 0x04001D9F RID: 7583
		private Uri baseUri;

		// Token: 0x04001DA0 RID: 7584
		private XmlSchema schema;

		// Token: 0x04001DA1 RID: 7585
		private string id;

		// Token: 0x04001DA2 RID: 7586
		private XmlAttribute[] moreAttributes;

		// Token: 0x04001DA3 RID: 7587
		private Compositor compositor;
	}
}
