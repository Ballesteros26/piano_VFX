using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000CC RID: 204
	public class LdapVirtualListControl : LdapControl
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000169E6 File Offset: 0x00014BE6
		public virtual int AfterCount
		{
			get
			{
				return this.m_afterCount;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000169EE File Offset: 0x00014BEE
		public virtual int BeforeCount
		{
			get
			{
				return this.m_beforeCount;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000169F6 File Offset: 0x00014BF6
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000169FE File Offset: 0x00014BFE
		public virtual int ListSize
		{
			get
			{
				return this.m_contentCount;
			}
			set
			{
				this.m_contentCount = value;
				this.BuildIndexedVLVRequest();
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00016A23 File Offset: 0x00014C23
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00016A2C File Offset: 0x00014C2C
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				int num = 3;
				this.m_context = value;
				if (this.m_vlvRequest.size() == 4)
				{
					this.m_vlvRequest.set_Renamed(num, new Asn1OctetString(this.m_context));
				}
				else if (this.m_vlvRequest.size() == 3)
				{
					this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
				}
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00016AA3 File Offset: 0x00014CA3
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount)
			: this(jumpTo, beforeCount, afterCount, null)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount, string context)
		{
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.m_context = context;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00016B0C File Offset: 0x00014D0C
		private void BuildTypedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapVirtualListControl.GREATERTHANOREQUAL), new Asn1OctetString(this.m_jumpTo), false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00016B97 File Offset: 0x00014D97
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount)
			: this(startIndex, beforeCount, afterCount, contentCount, null)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016BA8 File Offset: 0x00014DA8
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount, string context)
		{
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = startIndex;
			this.m_contentCount = contentCount;
			this.m_context = context;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016C0C File Offset: 0x00014E0C
		private void BuildIndexedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			Asn1Sequence asn1Sequence = new Asn1Sequence(2);
			asn1Sequence.add(new Asn1Integer(this.m_startIndex));
			asn1Sequence.add(new Asn1Integer(this.m_contentCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, true, LdapVirtualListControl.BYOFFSET), asn1Sequence, false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016CB6 File Offset: 0x00014EB6
		public virtual void setRange(int listIndex, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = listIndex;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016CE9 File Offset: 0x00014EE9
		public virtual void setRange(string jumpTo, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016D1C File Offset: 0x00014F1C
		static LdapVirtualListControl()
		{
			try
			{
				LdapControl.register(LdapVirtualListControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapVirtualListResponse"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0400048A RID: 1162
		private static int BYOFFSET = 0;

		// Token: 0x0400048B RID: 1163
		private static int GREATERTHANOREQUAL = 1;

		// Token: 0x0400048C RID: 1164
		private static string requestOID = "2.16.840.1.113730.3.4.9";

		// Token: 0x0400048D RID: 1165
		private static string responseOID = "2.16.840.1.113730.3.4.10";

		// Token: 0x0400048E RID: 1166
		private Asn1Sequence m_vlvRequest;

		// Token: 0x0400048F RID: 1167
		private int m_beforeCount;

		// Token: 0x04000490 RID: 1168
		private int m_afterCount;

		// Token: 0x04000491 RID: 1169
		private string m_jumpTo;

		// Token: 0x04000492 RID: 1170
		private string m_context;

		// Token: 0x04000493 RID: 1171
		private int m_startIndex;

		// Token: 0x04000494 RID: 1172
		private int m_contentCount;
	}
}
