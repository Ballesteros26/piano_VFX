using System;
using Novell.Directory.Ldap.Controls;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000AA RID: 170
	public class PSearchEventSource : LdapEventSource
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000450 RID: 1104 RVA: 0x00014280 File Offset: 0x00012480
		// (remove) Token: 0x06000451 RID: 1105 RVA: 0x0001429F File Offset: 0x0001249F
		public event PSearchEventSource.SearchResultEventHandler SearchResultEvent
		{
			add
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Combine(this.search_result_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Remove(this.search_result_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000452 RID: 1106 RVA: 0x000142BE File Offset: 0x000124BE
		// (remove) Token: 0x06000453 RID: 1107 RVA: 0x000142DD File Offset: 0x000124DD
		public event PSearchEventSource.SearchReferralEventHandler SearchReferralEvent
		{
			add
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Combine(this.search_referral_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Remove(this.search_referral_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000142FC File Offset: 0x000124FC
		protected override int GetListeners()
		{
			int num = 0;
			if (this.search_result_event != null)
			{
				num = this.search_result_event.GetInvocationList().Length;
			}
			if (this.search_referral_event != null)
			{
				num += this.search_referral_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001433C File Offset: 0x0001253C
		public PSearchEventSource(LdapConnection conn, string searchBase, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints constraints, LdapEventType eventchangetype, bool changeonly)
		{
			if (conn == null || searchBase == null || filter == null || attrs == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mConnection = conn;
			this.mSearchBase = searchBase;
			this.mScope = scope;
			this.mFilter = filter;
			this.mAttrs = attrs;
			this.mTypesOnly = typesOnly;
			this.mEventChangeType = eventchangetype;
			if (constraints == null)
			{
				this.mSearchConstraints = new LdapSearchConstraints();
			}
			else
			{
				this.mSearchConstraints = constraints;
			}
			LdapPersistSearchControl ldapPersistSearchControl = new LdapPersistSearchControl((int)eventchangetype, changeonly, true, true);
			this.mSearchConstraints.setControls(ldapPersistSearchControl);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000143D0 File Offset: 0x000125D0
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.Search(this.mSearchBase, this.mScope, this.mFilter, this.mAttrs, this.mTypesOnly, null, this.mSearchConstraints);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014448 File Offset: 0x00012648
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014464 File Offset: 0x00012664
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			if (sourceMessage == null)
			{
				return flag;
			}
			int i = sourceMessage.Type;
			if (i != 4)
			{
				if (i != 5)
				{
					if (i == 19 && this.search_referral_event != null)
					{
						this.search_referral_event(this, new SearchReferralEventArgs(sourceMessage, aClassification, (LdapEventType)nType));
						flag = true;
					}
				}
				else
				{
					base.NotifyDirectoryListeners(new LdapEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY));
					flag = true;
				}
			}
			else if (this.search_result_event != null)
			{
				LdapEventType ldapEventType = LdapEventType.TYPE_UNKNOWN;
				foreach (LdapControl ldapControl in sourceMessage.Controls)
				{
					if (ldapControl is LdapEntryChangeControl)
					{
						ldapEventType = (LdapEventType)((LdapEntryChangeControl)ldapControl).ChangeType;
					}
				}
				this.search_result_event(this, new SearchResultEventArgs(sourceMessage, aClassification, ldapEventType));
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000317 RID: 791
		protected PSearchEventSource.SearchResultEventHandler search_result_event;

		// Token: 0x04000318 RID: 792
		protected PSearchEventSource.SearchReferralEventHandler search_referral_event;

		// Token: 0x04000319 RID: 793
		protected LdapConnection mConnection;

		// Token: 0x0400031A RID: 794
		protected string mSearchBase;

		// Token: 0x0400031B RID: 795
		protected int mScope;

		// Token: 0x0400031C RID: 796
		protected string[] mAttrs;

		// Token: 0x0400031D RID: 797
		protected string mFilter;

		// Token: 0x0400031E RID: 798
		protected bool mTypesOnly;

		// Token: 0x0400031F RID: 799
		protected LdapSearchConstraints mSearchConstraints;

		// Token: 0x04000320 RID: 800
		protected LdapEventType mEventChangeType;

		// Token: 0x04000321 RID: 801
		protected LdapSearchQueue mQueue;

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x0600064C RID: 1612
		public delegate void SearchResultEventHandler(object source, SearchResultEventArgs objArgs);

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x06000650 RID: 1616
		public delegate void SearchReferralEventHandler(object source, SearchReferralEventArgs objArgs);
	}
}
