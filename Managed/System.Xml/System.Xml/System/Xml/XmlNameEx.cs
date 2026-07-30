using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200022D RID: 557
	internal sealed class XmlNameEx : XmlName
	{
		// Token: 0x0600151E RID: 5406 RVA: 0x00077CDC File Offset: 0x00075EDC
		internal XmlNameEx(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
			: base(prefix, localName, ns, hashCode, ownerDoc, next)
		{
			this.SetValidity(schemaInfo.Validity);
			this.SetIsDefault(schemaInfo.IsDefault);
			this.SetIsNil(schemaInfo.IsNil);
			this.memberType = schemaInfo.MemberType;
			this.schemaType = schemaInfo.SchemaType;
			this.decl = ((schemaInfo.SchemaElement != null) ? schemaInfo.SchemaElement : schemaInfo.SchemaAttribute);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00077D58 File Offset: 0x00075F58
		public override XmlSchemaValidity Validity
		{
			get
			{
				if (!this.ownerDoc.CanReportValidity)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return (XmlSchemaValidity)(this.flags & 3);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00077D71 File Offset: 0x00075F71
		public override bool IsDefault
		{
			get
			{
				return (this.flags & 4) > 0;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00077D7E File Offset: 0x00075F7E
		public override bool IsNil
		{
			get
			{
				return (this.flags & 8) > 0;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00077D8B File Offset: 0x00075F8B
		public override XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00077D93 File Offset: 0x00075F93
		public override XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00077D9B File Offset: 0x00075F9B
		public override XmlSchemaElement SchemaElement
		{
			get
			{
				return this.decl as XmlSchemaElement;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00077DA8 File Offset: 0x00075FA8
		public override XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.decl as XmlSchemaAttribute;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00077DB5 File Offset: 0x00075FB5
		public void SetValidity(XmlSchemaValidity value)
		{
			this.flags = (byte)(((int)this.flags & -4) | (int)((byte)value));
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00077DCA File Offset: 0x00075FCA
		public void SetIsDefault(bool value)
		{
			if (value)
			{
				this.flags |= 4;
				return;
			}
			this.flags = (byte)((int)this.flags & -5);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00077DEF File Offset: 0x00075FEF
		public void SetIsNil(bool value)
		{
			if (value)
			{
				this.flags |= 8;
				return;
			}
			this.flags = (byte)((int)this.flags & -9);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00077E14 File Offset: 0x00076014
		public override bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo != null && schemaInfo.Validity == (XmlSchemaValidity)(this.flags & 3) && schemaInfo.IsDefault == (this.flags & 4) > 0 && schemaInfo.IsNil == (this.flags & 8) > 0 && schemaInfo.MemberType == this.memberType && schemaInfo.SchemaType == this.schemaType && schemaInfo.SchemaElement == this.decl as XmlSchemaElement && schemaInfo.SchemaAttribute == this.decl as XmlSchemaAttribute;
		}

		// Token: 0x04000DF0 RID: 3568
		private byte flags;

		// Token: 0x04000DF1 RID: 3569
		private XmlSchemaSimpleType memberType;

		// Token: 0x04000DF2 RID: 3570
		private XmlSchemaType schemaType;

		// Token: 0x04000DF3 RID: 3571
		private object decl;

		// Token: 0x04000DF4 RID: 3572
		private const byte ValidityMask = 3;

		// Token: 0x04000DF5 RID: 3573
		private const byte IsDefaultBit = 4;

		// Token: 0x04000DF6 RID: 3574
		private const byte IsNilBit = 8;
	}
}
