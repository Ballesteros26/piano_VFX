using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007E RID: 126
	public class RfcSearchResultEntry : Asn1Sequence
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000125DB File Offset: 0x000107DB
		public virtual Asn1OctetString ObjectName
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000125E9 File Offset: 0x000107E9
		public virtual Asn1Sequence Attributes
		{
			get
			{
				return (Asn1Sequence)base.get_Renamed(1);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000125F7 File Offset: 0x000107F7
		[CLSCompliant(false)]
		public RfcSearchResultEntry(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012602 File Offset: 0x00010802
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 4);
		}
	}
}
