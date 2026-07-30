using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000033 RID: 51
	public class LdapSearchConstraints : LdapConstraints
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000A30B File Offset: 0x0000850B
		private void InitBlock()
		{
			this.dereference = 0;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A314 File Offset: 0x00008514
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000A31C File Offset: 0x0000851C
		public virtual int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				this.batchSize = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000A325 File Offset: 0x00008525
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000A32D File Offset: 0x0000852D
		public virtual int Dereference
		{
			get
			{
				return this.dereference;
			}
			set
			{
				this.dereference = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000A336 File Offset: 0x00008536
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000A33E File Offset: 0x0000853E
		public virtual int MaxResults
		{
			get
			{
				return this.maxResults;
			}
			set
			{
				this.maxResults = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A347 File Offset: 0x00008547
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000A34F File Offset: 0x0000854F
		public virtual int ServerTimeLimit
		{
			get
			{
				return this.serverTimeLimit;
			}
			set
			{
				this.serverTimeLimit = value;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A358 File Offset: 0x00008558
		public LdapSearchConstraints()
		{
			this.InitBlock();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A378 File Offset: 0x00008578
		public LdapSearchConstraints(LdapConstraints cons)
			: base(cons.TimeLimit, cons.ReferralFollowing, cons.getReferralHandler(), cons.HopLimit)
		{
			this.InitBlock();
			LdapControl[] controls = cons.getControls();
			if (controls != null)
			{
				LdapControl[] array = new LdapControl[controls.Length];
				controls.CopyTo(array, 0);
				base.setControls(array);
			}
			Hashtable properties = cons.Properties;
			if (properties != null)
			{
				base.Properties = (Hashtable)properties.Clone();
			}
			if (cons is LdapSearchConstraints)
			{
				LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)cons;
				this.serverTimeLimit = ldapSearchConstraints.ServerTimeLimit;
				this.dereference = ldapSearchConstraints.Dereference;
				this.maxResults = ldapSearchConstraints.MaxResults;
				this.batchSize = ldapSearchConstraints.BatchSize;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A438 File Offset: 0x00008638
		public LdapSearchConstraints(int msLimit, int serverTimeLimit, int dereference, int maxResults, bool doReferrals, int batchSize, LdapReferralHandler handler, int hop_limit)
			: base(msLimit, doReferrals, handler, hop_limit)
		{
			this.InitBlock();
			this.serverTimeLimit = serverTimeLimit;
			this.dereference = dereference;
			this.maxResults = maxResults;
			this.batchSize = batchSize;
		}

		// Token: 0x04000143 RID: 323
		private int dereference;

		// Token: 0x04000144 RID: 324
		private int serverTimeLimit;

		// Token: 0x04000145 RID: 325
		private int maxResults = 1000;

		// Token: 0x04000146 RID: 326
		private int batchSize = 1;

		// Token: 0x04000147 RID: 327
		private static object nameLock = new object();

		// Token: 0x04000148 RID: 328
		private static int lSConsNum;

		// Token: 0x04000149 RID: 329
		private string name;

		// Token: 0x0400014A RID: 330
		public const int DEREF_NEVER = 0;

		// Token: 0x0400014B RID: 331
		public const int DEREF_SEARCHING = 1;

		// Token: 0x0400014C RID: 332
		public const int DEREF_FINDING = 2;

		// Token: 0x0400014D RID: 333
		public const int DEREF_ALWAYS = 3;
	}
}
