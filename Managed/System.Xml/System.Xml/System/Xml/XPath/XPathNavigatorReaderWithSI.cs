using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x020002BF RID: 703
	internal class XPathNavigatorReaderWithSI : XPathNavigatorReader, IXmlSchemaInfo
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x00093A4D File Offset: 0x00091C4D
		internal XPathNavigatorReaderWithSI(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi)
			: base(navToRead, xli, xsi)
		{
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00093A58 File Offset: 0x00091C58
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				if (!base.IsReading)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.schemaInfo.Validity;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x00093A6F File Offset: 0x00091C6F
		public override bool IsDefault
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsDefault;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00093A86 File Offset: 0x00091C86
		public virtual bool IsNil
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsNil;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00093A9D File Offset: 0x00091C9D
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.MemberType;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00093AB4 File Offset: 0x00091CB4
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaType;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00093ACB File Offset: 0x00091CCB
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaElement;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00093AE2 File Offset: 0x00091CE2
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaAttribute;
			}
		}
	}
}
