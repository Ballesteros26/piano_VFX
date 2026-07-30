using System;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000615 RID: 1557
	internal class XmlQueryDataWriter : BinaryWriter
	{
		// Token: 0x06003D22 RID: 15650 RVA: 0x00153173 File Offset: 0x00151373
		public XmlQueryDataWriter(Stream output)
			: base(output)
		{
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0015317C File Offset: 0x0015137C
		public void WriteInt32Encoded(int value)
		{
			base.Write7BitEncodedInt(value);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x00153185 File Offset: 0x00151385
		public void WriteStringQ(string value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value);
			}
		}
	}
}
