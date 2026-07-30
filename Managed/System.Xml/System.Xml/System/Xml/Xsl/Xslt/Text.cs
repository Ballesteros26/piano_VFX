using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A1 RID: 1441
	internal class Text : XslNode
	{
		// Token: 0x060038C7 RID: 14535 RVA: 0x0013F1ED File Offset: 0x0013D3ED
		public Text(string data, SerializationHints hints, XslVersion xslVer)
			: base(XslNodeType.Text, null, data, xslVer)
		{
			this.Hints = hints;
		}

		// Token: 0x04002515 RID: 9493
		public readonly SerializationHints Hints;
	}
}
