using System;

namespace System.Xml.Schema
{
	// Token: 0x02000389 RID: 905
	internal class AutoValidator : BaseValidator
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x000DE521 File Offset: 0x000DC721
		public AutoValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
			: base(reader, schemaCollection, eventHandling)
		{
			this.schemaInfo = new SchemaInfo();
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000DE538 File Offset: 0x000DC738
		public override void Validate()
		{
			switch (this.DetectValidationType())
			{
			case ValidationType.Auto:
			case ValidationType.DTD:
				break;
			case ValidationType.XDR:
				this.reader.Validator = new XdrValidator(this);
				this.reader.Validator.Validate();
				return;
			case ValidationType.Schema:
				this.reader.Validator = new XsdValidator(this);
				this.reader.Validator.Validate();
				break;
			default:
				return;
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00002F50 File Offset: 0x00001150
		public override void CompleteValidation()
		{
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0000365F File Offset: 0x0000185F
		public override object FindId(string name)
		{
			return null;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000DE5A8 File Offset: 0x000DC7A8
		private ValidationType DetectValidationType()
		{
			if (this.reader.Schemas != null && this.reader.Schemas.Count > 0)
			{
				XmlSchemaCollectionEnumerator enumerator = this.reader.Schemas.GetEnumerator();
				while (enumerator.MoveNext())
				{
					SchemaInfo schemaInfo = enumerator.CurrentNode.SchemaInfo;
					if (schemaInfo.SchemaType == SchemaType.XSD)
					{
						return ValidationType.Schema;
					}
					if (schemaInfo.SchemaType == SchemaType.XDR)
					{
						return ValidationType.XDR;
					}
				}
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				SchemaType schemaType = base.SchemaNames.SchemaTypeFromRoot(this.reader.LocalName, this.reader.NamespaceURI);
				if (schemaType == SchemaType.XSD)
				{
					return ValidationType.Schema;
				}
				if (schemaType == SchemaType.XDR)
				{
					return ValidationType.XDR;
				}
				int attributeCount = this.reader.AttributeCount;
				for (int i = 0; i < attributeCount; i++)
				{
					this.reader.MoveToAttribute(i);
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						if (XdrBuilder.IsXdrSchema(this.reader.Value))
						{
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
					else
					{
						if (Ref.Equal(namespaceURI, base.SchemaNames.NsXsi))
						{
							this.reader.MoveToElement();
							return ValidationType.Schema;
						}
						if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
						{
							this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
				}
				if (attributeCount > 0)
				{
					this.reader.MoveToElement();
				}
			}
			return ValidationType.Auto;
		}

		// Token: 0x040018DC RID: 6364
		private const string x_schema = "x-schema";
	}
}
