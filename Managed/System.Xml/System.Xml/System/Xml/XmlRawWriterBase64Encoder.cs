using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000078 RID: 120
	internal class XmlRawWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000E4D2 File Offset: 0x0000C6D2
		internal XmlRawWriterBase64Encoder(XmlRawWriter rawWriter)
		{
			this.rawWriter = rawWriter;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E4E1 File Offset: 0x0000C6E1
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.rawWriter.WriteRaw(chars, index, count);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			return this.rawWriter.WriteRawAsync(chars, index, count);
		}

		// Token: 0x0400022A RID: 554
		private XmlRawWriter rawWriter;
	}
}
