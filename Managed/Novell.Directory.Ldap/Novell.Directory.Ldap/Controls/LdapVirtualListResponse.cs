using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000CD RID: 205
	public class LdapVirtualListResponse : LdapControl
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00016D74 File Offset: 0x00014F74
		public virtual int ContentCount
		{
			get
			{
				return this.m_ContentCount;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00016D7C File Offset: 0x00014F7C
		public virtual int FirstPosition
		{
			get
			{
				return this.m_firstPosition;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00016D84 File Offset: 0x00014F84
		public virtual int ResultCode
		{
			get
			{
				return this.m_resultCode;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00016D8C File Offset: 0x00014F8C
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00016D94 File Offset: 0x00014F94
		[CLSCompliant(false)]
		public LdapVirtualListResponse(string oid, bool critical, sbyte[] values)
			: base(oid, critical, values)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object = lberdecoder.decode(values);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object2 = ((Asn1Sequence)asn1Object).get_Renamed(0);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_firstPosition = ((Asn1Integer)asn1Object2).intValue();
			Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
			if (asn1Object3 == null || !(asn1Object3 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_ContentCount = ((Asn1Integer)asn1Object3).intValue();
			Asn1Object asn1Object4 = ((Asn1Sequence)asn1Object).get_Renamed(2);
			if (asn1Object4 != null && asn1Object4 is Asn1Enumerated)
			{
				this.m_resultCode = ((Asn1Enumerated)asn1Object4).intValue();
				if (((Asn1Sequence)asn1Object).size() > 3)
				{
					Asn1Object asn1Object5 = ((Asn1Sequence)asn1Object).get_Renamed(3);
					if (asn1Object5 != null && asn1Object5 is Asn1OctetString)
					{
						this.m_context = ((Asn1OctetString)asn1Object5).stringValue();
					}
				}
				return;
			}
			throw new IOException("Decoding error");
		}

		// Token: 0x04000495 RID: 1173
		private int m_firstPosition;

		// Token: 0x04000496 RID: 1174
		private int m_ContentCount;

		// Token: 0x04000497 RID: 1175
		private int m_resultCode;

		// Token: 0x04000498 RID: 1176
		private string m_context;
	}
}
