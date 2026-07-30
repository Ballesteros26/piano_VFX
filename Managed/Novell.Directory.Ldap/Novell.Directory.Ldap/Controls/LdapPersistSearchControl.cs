using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000C8 RID: 200
	public class LdapPersistSearchControl : LdapControl
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00016535 File Offset: 0x00014735
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0001653D File Offset: 0x0001473D
		public virtual int ChangeTypes
		{
			get
			{
				return this.m_changeTypes;
			}
			set
			{
				this.m_changeTypes = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGETYPES_INDEX, new Asn1Integer(this.m_changeTypes));
				this.setValue();
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00016567 File Offset: 0x00014767
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0001656F File Offset: 0x0001476F
		public virtual bool ReturnControls
		{
			get
			{
				return this.m_returnControls;
			}
			set
			{
				this.m_returnControls = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.RETURNCONTROLS_INDEX, new Asn1Boolean(this.m_returnControls));
				this.setValue();
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00016599 File Offset: 0x00014799
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000165A1 File Offset: 0x000147A1
		public virtual bool ChangesOnly
		{
			get
			{
				return this.m_changesOnly;
			}
			set
			{
				this.m_changesOnly = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGESONLY_INDEX, new Asn1Boolean(this.m_changesOnly));
				this.setValue();
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000165CB File Offset: 0x000147CB
		public LdapPersistSearchControl()
			: this(LdapPersistSearchControl.ANY, true, true, true)
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000165DC File Offset: 0x000147DC
		public LdapPersistSearchControl(int changeTypes, bool changesOnly, bool returnControls, bool isCritical)
			: base(LdapPersistSearchControl.requestOID, isCritical, null)
		{
			this.m_changeTypes = changeTypes;
			this.m_changesOnly = changesOnly;
			this.m_returnControls = returnControls;
			this.m_sequence = new Asn1Sequence(LdapPersistSearchControl.SEQUENCE_SIZE);
			this.m_sequence.add(new Asn1Integer(this.m_changeTypes));
			this.m_sequence.add(new Asn1Boolean(this.m_changesOnly));
			this.m_sequence.add(new Asn1Boolean(this.m_returnControls));
			this.setValue();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016664 File Offset: 0x00014864
		public override string ToString()
		{
			sbyte[] encoding = this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder);
			StringBuilder stringBuilder = new StringBuilder(encoding.Length);
			for (int i = 0; i < encoding.Length; i++)
			{
				stringBuilder.Append(encoding[i].ToString());
				if (i < encoding.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000166C6 File Offset: 0x000148C6
		private void setValue()
		{
			base.setValue(this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000166E0 File Offset: 0x000148E0
		static LdapPersistSearchControl()
		{
			try
			{
				LdapControl.register(LdapPersistSearchControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapEntryChangeControl"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000471 RID: 1137
		private static int SEQUENCE_SIZE = 3;

		// Token: 0x04000472 RID: 1138
		private static int CHANGETYPES_INDEX = 0;

		// Token: 0x04000473 RID: 1139
		private static int CHANGESONLY_INDEX = 1;

		// Token: 0x04000474 RID: 1140
		private static int RETURNCONTROLS_INDEX = 2;

		// Token: 0x04000475 RID: 1141
		private static LBEREncoder s_encoder = new LBEREncoder();

		// Token: 0x04000476 RID: 1142
		private int m_changeTypes;

		// Token: 0x04000477 RID: 1143
		private bool m_changesOnly;

		// Token: 0x04000478 RID: 1144
		private bool m_returnControls;

		// Token: 0x04000479 RID: 1145
		private Asn1Sequence m_sequence;

		// Token: 0x0400047A RID: 1146
		private static string requestOID = "2.16.840.1.113730.3.4.3";

		// Token: 0x0400047B RID: 1147
		private static string responseOID = "2.16.840.1.113730.3.4.7";

		// Token: 0x0400047C RID: 1148
		public const int ADD = 1;

		// Token: 0x0400047D RID: 1149
		public const int DELETE = 2;

		// Token: 0x0400047E RID: 1150
		public const int MODIFY = 4;

		// Token: 0x0400047F RID: 1151
		public const int MODDN = 8;

		// Token: 0x04000480 RID: 1152
		public static readonly int ANY = 15;
	}
}
