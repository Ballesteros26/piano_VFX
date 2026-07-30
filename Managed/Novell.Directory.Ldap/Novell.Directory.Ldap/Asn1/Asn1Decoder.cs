using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D0 RID: 208
	[CLSCompliant(false)]
	public interface Asn1Decoder : ISerializable
	{
		// Token: 0x06000519 RID: 1305
		Asn1Object decode(sbyte[] value_Renamed);

		// Token: 0x0600051A RID: 1306
		Asn1Object decode(Stream in_Renamed);

		// Token: 0x0600051B RID: 1307
		Asn1Object decode(Stream in_Renamed, int[] length);

		// Token: 0x0600051C RID: 1308
		object decodeBoolean(Stream in_Renamed, int len);

		// Token: 0x0600051D RID: 1309
		object decodeNumeric(Stream in_Renamed, int len);

		// Token: 0x0600051E RID: 1310
		object decodeOctetString(Stream in_Renamed, int len);

		// Token: 0x0600051F RID: 1311
		object decodeCharacterString(Stream in_Renamed, int len);
	}
}
