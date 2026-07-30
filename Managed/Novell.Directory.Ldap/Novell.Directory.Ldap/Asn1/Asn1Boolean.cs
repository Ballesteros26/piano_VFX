using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000CE RID: 206
	public class Asn1Boolean : Asn1Object
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00016EB7 File Offset: 0x000150B7
		public Asn1Boolean(bool content)
			: base(Asn1Boolean.ID)
		{
			this.content = content;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00016ECB File Offset: 0x000150CB
		[CLSCompliant(false)]
		public Asn1Boolean(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Boolean.ID)
		{
			this.content = (bool)dec.decodeBoolean(in_Renamed, len);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00016EEB File Offset: 0x000150EB
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00016EF5 File Offset: 0x000150F5
		public bool booleanValue()
		{
			return this.content;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00016EFD File Offset: 0x000150FD
		public override string ToString()
		{
			return base.ToString() + "BOOLEAN: " + this.content.ToString();
		}

		// Token: 0x04000499 RID: 1177
		private bool content;

		// Token: 0x0400049A RID: 1178
		public const int TAG = 1;

		// Token: 0x0400049B RID: 1179
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 1);
	}
}
