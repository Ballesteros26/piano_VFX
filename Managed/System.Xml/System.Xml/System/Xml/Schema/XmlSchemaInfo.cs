using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x0200046B RID: 1131
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> class.</summary>
		// Token: 0x06002C72 RID: 11378 RVA: 0x00107144 File Offset: 0x00105344
		public XmlSchemaInfo()
		{
			this.Clear();
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x00107152 File Offset: 0x00105352
		internal XmlSchemaInfo(XmlSchemaValidity validity)
			: this()
		{
			this.validity = validity;
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value.</returns>
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00107161 File Offset: 0x00105361
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x00107169 File Offset: 0x00105369
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.validity;
			}
			set
			{
				this.validity = value;
			}
		}

		/// <summary>Gets or sets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x00107172 File Offset: 0x00105372
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x0010717A File Offset: 0x0010537A
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		/// <summary>Gets or sets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x00107183 File Offset: 0x00105383
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x0010718B File Offset: 0x0010538B
		public bool IsNil
		{
			get
			{
				return this.isNil;
			}
			set
			{
				this.isNil = value;
			}
		}

		/// <summary>Gets or sets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x00107194 File Offset: 0x00105394
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x0010719C File Offset: 0x0010539C
		public XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}

		/// <summary>Gets or sets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x001071A5 File Offset: 0x001053A5
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x001071AD File Offset: 0x001053AD
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
				if (this.schemaType != null)
				{
					this.contentType = this.schemaType.SchemaContentType;
					return;
				}
				this.contentType = XmlSchemaContentType.Empty;
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object.</returns>
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x001071D7 File Offset: 0x001053D7
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x001071DF File Offset: 0x001053DF
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
				if (value != null)
				{
					this.schemaAttribute = null;
				}
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object.</returns>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x001071F2 File Offset: 0x001053F2
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x001071FA File Offset: 0x001053FA
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
				if (value != null)
				{
					this.schemaElement = null;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object that corresponds to the content type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x0010720D File Offset: 0x0010540D
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x00107215 File Offset: 0x00105415
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x0010721E File Offset: 0x0010541E
		internal XmlSchemaType XmlType
		{
			get
			{
				if (this.memberType != null)
				{
					return this.memberType;
				}
				return this.schemaType;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x00107235 File Offset: 0x00105435
		internal bool HasDefaultValue
		{
			get
			{
				return this.schemaElement != null && this.schemaElement.ElementDecl.DefaultValueTyped != null;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x00107254 File Offset: 0x00105454
		internal bool IsUnionType
		{
			get
			{
				return this.schemaType != null && this.schemaType.Datatype != null && this.schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union;
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x00107280 File Offset: 0x00105480
		internal void Clear()
		{
			this.isNil = false;
			this.isDefault = false;
			this.schemaType = null;
			this.schemaElement = null;
			this.schemaAttribute = null;
			this.memberType = null;
			this.validity = XmlSchemaValidity.NotKnown;
			this.contentType = XmlSchemaContentType.Empty;
		}

		// Token: 0x04001DCC RID: 7628
		private bool isDefault;

		// Token: 0x04001DCD RID: 7629
		private bool isNil;

		// Token: 0x04001DCE RID: 7630
		private XmlSchemaElement schemaElement;

		// Token: 0x04001DCF RID: 7631
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04001DD0 RID: 7632
		private XmlSchemaType schemaType;

		// Token: 0x04001DD1 RID: 7633
		private XmlSchemaSimpleType memberType;

		// Token: 0x04001DD2 RID: 7634
		private XmlSchemaValidity validity;

		// Token: 0x04001DD3 RID: 7635
		private XmlSchemaContentType contentType;
	}
}
