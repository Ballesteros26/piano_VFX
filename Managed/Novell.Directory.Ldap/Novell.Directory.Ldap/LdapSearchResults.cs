using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000038 RID: 56
	public class LdapSearchResults
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A850 File Offset: 0x00008A50
		public virtual int Count
		{
			get
			{
				int count = this.queue.MessageAgent.Count;
				return this.entryCount - this.entryIndex + this.referenceCount - this.referenceIndex + count;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A88B File Offset: 0x00008A8B
		public virtual LdapControl[] ResponseControls
		{
			get
			{
				return this.controls;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A894 File Offset: 0x00008A94
		private bool BatchOfResults
		{
			get
			{
				int i = 0;
				while (i < this.batchSize)
				{
					try
					{
						LdapMessage response;
						if ((response = this.queue.getResponse()) == null)
						{
							LdapException ex = new LdapException(null, 85, null);
							this.entries.Add(ex);
							break;
						}
						LdapControl[] array = response.Controls;
						if (array != null)
						{
							this.controls = array;
						}
						if (response is LdapSearchResult)
						{
							object entry = ((LdapSearchResult)response).Entry;
							this.entries.Add(entry);
							i++;
							this.entryCount++;
						}
						else if (response is LdapSearchResultReference)
						{
							string[] referrals = ((LdapSearchResultReference)response).Referrals;
							if (!this.cons.ReferralFollowing)
							{
								this.references.Add(referrals);
								this.referenceCount++;
							}
						}
						else
						{
							LdapResponse ldapResponse = (LdapResponse)response;
							int num = ldapResponse.ResultCode;
							if (ldapResponse.hasException())
							{
								num = 91;
							}
							if ((num != 10 || !this.cons.ReferralFollowing) && num != 0)
							{
								this.entries.Add(ldapResponse);
								this.entryCount++;
							}
							if (this.queue.MessageIDs.Length == 0)
							{
								return true;
							}
						}
					}
					catch (LdapException ex2)
					{
						this.entries.Add(ex2);
					}
				}
				return false;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000AA0C File Offset: 0x00008C0C
		internal LdapSearchResults(LdapConnection conn, LdapSearchQueue queue, LdapSearchConstraints cons)
		{
			this.conn = conn;
			this.cons = cons;
			int num = cons.BatchSize;
			this.entries = new ArrayList((num == 0) ? 64 : num);
			this.entryCount = 0;
			this.entryIndex = 0;
			this.references = new ArrayList(5);
			this.referenceCount = 0;
			this.referenceIndex = 0;
			this.queue = queue;
			this.batchSize = ((num == 0) ? int.MaxValue : num);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public virtual bool hasMore()
		{
			bool flag = false;
			if (this.entryIndex < this.entryCount || this.referenceIndex < this.referenceCount)
			{
				flag = true;
			}
			else if (!this.completed)
			{
				this.resetVectors();
				flag = this.entryIndex < this.entryCount || this.referenceIndex < this.referenceCount;
			}
			return flag;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AAEC File Offset: 0x00008CEC
		private void resetVectors()
		{
			if (this.completed)
			{
				return;
			}
			if (this.referenceIndex != 0 && this.referenceIndex >= this.referenceCount)
			{
				SupportClass.SetSize(this.references, 0);
				this.referenceCount = 0;
				this.referenceIndex = 0;
			}
			if (this.entryIndex != 0 && this.entryIndex >= this.entryCount)
			{
				SupportClass.SetSize(this.entries, 0);
				this.entryCount = 0;
				this.entryIndex = 0;
			}
			if (this.referenceIndex == 0 && this.referenceCount == 0 && this.entryIndex == 0 && this.entryCount == 0)
			{
				this.completed = this.BatchOfResults;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AB90 File Offset: 0x00008D90
		public virtual LdapEntry next()
		{
			if (this.completed && this.entryIndex >= this.entryCount && this.referenceIndex >= this.referenceCount)
			{
				throw new ArgumentOutOfRangeException("LdapSearchResults.next() no more results");
			}
			this.resetVectors();
			if (this.referenceIndex < this.referenceCount)
			{
				ArrayList arrayList = this.references;
				int num = this.referenceIndex;
				this.referenceIndex = num + 1;
				string[] array = (string[])arrayList[num];
				LdapReferralException ex = new LdapReferralException("REFERENCE_NOFOLLOW");
				ex.setReferrals(array);
				throw ex;
			}
			if (this.entryIndex < this.entryCount)
			{
				ArrayList arrayList2 = this.entries;
				int num = this.entryIndex;
				this.entryIndex = num + 1;
				object obj = arrayList2[num];
				if (obj is LdapResponse)
				{
					if (((LdapResponse)obj).hasException())
					{
						LdapResponse ldapResponse = (LdapResponse)obj;
						ReferralInfo activeReferral = ldapResponse.ActiveReferral;
						if (activeReferral != null)
						{
							LdapReferralException ex2 = new LdapReferralException("REFERENCE_ERROR", ldapResponse.Exception);
							ex2.setReferrals(activeReferral.ReferralList);
							ex2.FailedReferral = activeReferral.ReferralUrl.ToString();
							throw ex2;
						}
					}
					((LdapResponse)obj).chkResultCode();
				}
				else if (obj is LdapException)
				{
					throw (LdapException)obj;
				}
				return (LdapEntry)obj;
			}
			throw new LdapException("REFERRAL_LOCAL", new object[] { "next" }, 82, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000ACDA File Offset: 0x00008EDA
		internal virtual void Abandon()
		{
			this.queue.MessageAgent.AbandonAll();
			this.resetVectors();
			this.completed = true;
		}

		// Token: 0x04000160 RID: 352
		private ArrayList entries;

		// Token: 0x04000161 RID: 353
		private int entryCount;

		// Token: 0x04000162 RID: 354
		private int entryIndex;

		// Token: 0x04000163 RID: 355
		private ArrayList references;

		// Token: 0x04000164 RID: 356
		private int referenceCount;

		// Token: 0x04000165 RID: 357
		private int referenceIndex;

		// Token: 0x04000166 RID: 358
		private int batchSize;

		// Token: 0x04000167 RID: 359
		private bool completed;

		// Token: 0x04000168 RID: 360
		private LdapControl[] controls;

		// Token: 0x04000169 RID: 361
		private LdapSearchQueue queue;

		// Token: 0x0400016A RID: 362
		private static object nameLock = new object();

		// Token: 0x0400016B RID: 363
		private static int resultsNum;

		// Token: 0x0400016C RID: 364
		private string name;

		// Token: 0x0400016D RID: 365
		private LdapConnection conn;

		// Token: 0x0400016E RID: 366
		private LdapSearchConstraints cons;

		// Token: 0x0400016F RID: 367
		private ArrayList referralConn;
	}
}
