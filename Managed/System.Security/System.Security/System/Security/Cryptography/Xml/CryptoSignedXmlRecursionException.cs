using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	internal class CryptoSignedXmlRecursionException : XmlException
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006AE6 File Offset: 0x00004CE6
		public CryptoSignedXmlRecursionException()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006AEE File Offset: 0x00004CEE
		public CryptoSignedXmlRecursionException(string message)
			: base(message)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006AF7 File Offset: 0x00004CF7
		public CryptoSignedXmlRecursionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006B01 File Offset: 0x00004D01
		protected CryptoSignedXmlRecursionException(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
