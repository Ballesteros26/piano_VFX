using System;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007D7 RID: 2007
	internal class SoapHelper
	{
		// Token: 0x060050BA RID: 20666 RVA: 0x00120157 File Offset: 0x0011E357
		public static Exception GetException(ISoapXsd type, string msg)
		{
			return new RemotingException("Soap Parse error, xsd:type xsd:" + type.GetXsdType() + " " + msg);
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x00002119 File Offset: 0x00000319
		public static string Normalize(string s)
		{
			return s;
		}
	}
}
