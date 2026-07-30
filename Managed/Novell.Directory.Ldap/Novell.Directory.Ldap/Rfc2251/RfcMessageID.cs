using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000072 RID: 114
	internal class RfcMessageID : Asn1Integer
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000122E8 File Offset: 0x000104E8
		private static int MessageID
		{
			get
			{
				object obj = RfcMessageID.lock_Renamed;
				int num;
				lock (obj)
				{
					num = ((RfcMessageID.messageID < int.MaxValue) ? (++RfcMessageID.messageID) : (RfcMessageID.messageID = 1));
				}
				return num;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012348 File Offset: 0x00010548
		protected internal RfcMessageID()
			: base(RfcMessageID.MessageID)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012355 File Offset: 0x00010555
		protected internal RfcMessageID(int i)
			: base(i)
		{
		}

		// Token: 0x0400024F RID: 591
		private static int messageID;

		// Token: 0x04000250 RID: 592
		private static object lock_Renamed = new object();
	}
}
