using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D1 RID: 209
	public interface Asn1Encoder : ISerializable
	{
		// Token: 0x06000520 RID: 1312
		void encode(Asn1Boolean b, Stream out_Renamed);

		// Token: 0x06000521 RID: 1313
		void encode(Asn1Numeric n, Stream out_Renamed);

		// Token: 0x06000522 RID: 1314
		void encode(Asn1Null n, Stream out_Renamed);

		// Token: 0x06000523 RID: 1315
		void encode(Asn1OctetString os, Stream out_Renamed);

		// Token: 0x06000524 RID: 1316
		void encode(Asn1Structured c, Stream out_Renamed);

		// Token: 0x06000525 RID: 1317
		void encode(Asn1Tagged t, Stream out_Renamed);

		// Token: 0x06000526 RID: 1318
		void encode(Asn1Identifier id, Stream out_Renamed);
	}
}
