using System;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002D RID: 45
	public class LdapReferralException : LdapException
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000952F File Offset: 0x0000772F
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009537 File Offset: 0x00007737
		public virtual string FailedReferral
		{
			get
			{
				return this.failedReferral;
			}
			set
			{
				this.failedReferral = value;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009540 File Offset: 0x00007740
		public LdapReferralException()
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009548 File Offset: 0x00007748
		public LdapReferralException(string message)
			: base(message, 10, null)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009554 File Offset: 0x00007754
		public LdapReferralException(string message, object[] arguments)
			: base(message, arguments, 10, null)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009561 File Offset: 0x00007761
		public LdapReferralException(string message, Exception rootException)
			: base(message, 10, null, rootException)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000956E File Offset: 0x0000776E
		public LdapReferralException(string message, object[] arguments, Exception rootException)
			: base(message, arguments, 10, null, rootException)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000957C File Offset: 0x0000777C
		public LdapReferralException(string message, int resultCode, string serverMessage)
			: base(message, resultCode, serverMessage)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009587 File Offset: 0x00007787
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage)
			: base(message, arguments, resultCode, serverMessage)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009594 File Offset: 0x00007794
		public LdapReferralException(string message, int resultCode, string serverMessage, Exception rootException)
			: base(message, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000095A1 File Offset: 0x000077A1
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage, Exception rootException)
			: base(message, arguments, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000095B0 File Offset: 0x000077B0
		public virtual string[] getReferrals()
		{
			return this.referrals;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000095B8 File Offset: 0x000077B8
		internal virtual void setReferrals(string[] urls)
		{
			this.referrals = urls;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000095C4 File Offset: 0x000077C4
		public override string ToString()
		{
			string text = this.getExceptionString("LdapReferralException");
			if (this.failedReferral != null)
			{
				string text2 = ResourcesHandler.getMessage("FAILED_REFERRAL", new object[] { "LdapReferralException", this.failedReferral });
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = "LdapReferralException: Failed Referral: " + this.failedReferral;
				}
				text = text + "\n" + text2;
			}
			if (this.referrals != null)
			{
				for (int i = 0; i < this.referrals.Length; i++)
				{
					string text2 = ResourcesHandler.getMessage("REFERRAL_ITEM", new object[]
					{
						"LdapReferralException",
						this.referrals[i]
					});
					if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
					{
						text2 = "LdapReferralException: Referral: " + this.referrals[i];
					}
					text = text + "\n" + text2;
				}
			}
			return text;
		}

		// Token: 0x0400012E RID: 302
		private string failedReferral;

		// Token: 0x0400012F RID: 303
		private string[] referrals;
	}
}
