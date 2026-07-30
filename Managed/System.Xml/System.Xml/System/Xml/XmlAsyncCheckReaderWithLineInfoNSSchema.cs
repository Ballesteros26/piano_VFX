using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C6 RID: 198
	internal class XmlAsyncCheckReaderWithLineInfoNSSchema : XmlAsyncCheckReaderWithLineInfoNS, IXmlSchemaInfo
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001BF05 File Offset: 0x0001A105
		public XmlAsyncCheckReaderWithLineInfoNSSchema(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlSchemaInfo = (IXmlSchemaInfo)reader;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001BF1A File Offset: 0x0001A11A
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001BF27 File Offset: 0x0001A127
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001BF34 File Offset: 0x0001A134
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001BF41 File Offset: 0x0001A141
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.MemberType;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001BF4E File Offset: 0x0001A14E
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001BF5B File Offset: 0x0001A15B
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaElement;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001BF68 File Offset: 0x0001A168
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaAttribute;
			}
		}

		// Token: 0x040003E0 RID: 992
		private readonly IXmlSchemaInfo readerAsIXmlSchemaInfo;
	}
}
