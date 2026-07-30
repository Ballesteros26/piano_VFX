using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B6 RID: 182
	public class EdirEventSource : LdapEventSource
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600046E RID: 1134 RVA: 0x000149A0 File Offset: 0x00012BA0
		// (remove) Token: 0x0600046F RID: 1135 RVA: 0x000149BF File Offset: 0x00012BBF
		public event EdirEventSource.EdirEventHandler EdirEvent
		{
			add
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Combine(this.edir_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Remove(this.edir_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000149E0 File Offset: 0x00012BE0
		protected override int GetListeners()
		{
			int num = 0;
			if (this.edir_event != null)
			{
				num = this.edir_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014A06 File Offset: 0x00012C06
		public EdirEventSource(EdirEventSpecifier[] specifier, LdapConnection conn)
		{
			if (specifier == null || conn == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mRequestOperation = new MonitorEventRequest(specifier);
			this.mConnection = conn;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014A34 File Offset: 0x00012C34
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.ExtendedOperation(this.mRequestOperation, null, null);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014A8F File Offset: 0x00012C8F
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00014AA8 File Offset: 0x00012CA8
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			if (this.edir_event != null && sourceMessage != null && sourceMessage.Type == 25 && sourceMessage is EdirEventIntermediateResponse)
			{
				this.edir_event(this, new EdirEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_EDIR_EVENT));
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000425 RID: 1061
		protected EdirEventSource.EdirEventHandler edir_event;

		// Token: 0x04000426 RID: 1062
		protected LdapConnection mConnection;

		// Token: 0x04000427 RID: 1063
		protected MonitorEventRequest mRequestOperation;

		// Token: 0x04000428 RID: 1064
		protected LdapResponseQueue mQueue;

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000654 RID: 1620
		public delegate void EdirEventHandler(object source, EdirEventArgs objEdirEventArgs);
	}
}
