using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000079 RID: 121
	internal class XmlTextWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000E501 File Offset: 0x0000C701
		internal XmlTextWriterBase64Encoder(XmlTextEncoder xmlTextEncoder)
		{
			this.xmlTextEncoder = xmlTextEncoder;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000E510 File Offset: 0x0000C710
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.xmlTextEncoder.WriteRaw(chars, index, count);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A533 File Offset: 0x00008733
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400022B RID: 555
		private XmlTextEncoder xmlTextEncoder;
	}
}
