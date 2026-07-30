using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000096 RID: 150
	public class PartitionEntryCountResponse : LdapExtendedResponse
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000138E4 File Offset: 0x00011AE4
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000138EC File Offset: 0x00011AEC
		public PartitionEntryCountResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.count = -1;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(value);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.count = asn1Integer.intValue();
		}

		// Token: 0x040002B0 RID: 688
		private int count;
	}
}
