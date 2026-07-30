using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200060C RID: 1548
	internal class XmlILQueryEventArgs : XsltMessageEncounteredEventArgs
	{
		// Token: 0x06003C37 RID: 15415 RVA: 0x00150587 File Offset: 0x0014E787
		public XmlILQueryEventArgs(string message)
		{
			this.message = message;
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x00150596 File Offset: 0x0014E796
		public override string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x04002787 RID: 10119
		private string message;
	}
}
