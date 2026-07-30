using System;
using System.Threading;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A9 RID: 169
	public abstract class LdapEventSource
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001405D File Offset: 0x0001225D
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00014065 File Offset: 0x00012265
		public int SleepInterval
		{
			get
			{
				return this.sleep_interval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("SleepInterval", "cannot take the negative or zero values ");
				}
				this.sleep_interval = value;
			}
		}

		// Token: 0x0600043E RID: 1086
		protected abstract int GetListeners();

		// Token: 0x0600043F RID: 1087 RVA: 0x00014084 File Offset: 0x00012284
		protected LdapEventSource.LISTENERS_COUNT GetCurrentListenersState()
		{
			int num = 0;
			num += this.GetListeners();
			if (this.directory_event != null)
			{
				num += this.directory_event.GetInvocationList().Length;
			}
			if (this.directory_exception_event != null)
			{
				num += this.directory_exception_event.GetInvocationList().Length;
			}
			if (num == 0)
			{
				return LdapEventSource.LISTENERS_COUNT.ZERO;
			}
			if (1 == num)
			{
				return LdapEventSource.LISTENERS_COUNT.ONE;
			}
			return LdapEventSource.LISTENERS_COUNT.MORE_THAN_ONE;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000140D8 File Offset: 0x000122D8
		protected void ListenerAdded()
		{
			switch (this.GetCurrentListenersState())
			{
			case LdapEventSource.LISTENERS_COUNT.ZERO:
			case LdapEventSource.LISTENERS_COUNT.MORE_THAN_ONE:
				break;
			case LdapEventSource.LISTENERS_COUNT.ONE:
				this.StartSearchAndPolling();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014108 File Offset: 0x00012308
		protected void ListenerRemoved()
		{
			LdapEventSource.LISTENERS_COUNT currentListenersState = this.GetCurrentListenersState();
			if (currentListenersState != LdapEventSource.LISTENERS_COUNT.ZERO)
			{
				int num = currentListenersState - LdapEventSource.LISTENERS_COUNT.ONE;
				return;
			}
			this.StopSearchAndPolling();
		}

		// Token: 0x06000442 RID: 1090
		protected abstract void StartSearchAndPolling();

		// Token: 0x06000443 RID: 1091
		protected abstract void StopSearchAndPolling();

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000444 RID: 1092 RVA: 0x0001412C File Offset: 0x0001232C
		// (remove) Token: 0x06000445 RID: 1093 RVA: 0x0001414B File Offset: 0x0001234B
		public event LdapEventSource.DirectoryEventHandler DirectoryEvent
		{
			add
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Combine(this.directory_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Remove(this.directory_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000446 RID: 1094 RVA: 0x0001416A File Offset: 0x0001236A
		// (remove) Token: 0x06000447 RID: 1095 RVA: 0x00014189 File Offset: 0x00012389
		public event LdapEventSource.DirectoryExceptionEventHandler DirectoryExceptionEvent
		{
			add
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Combine(this.directory_exception_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Remove(this.directory_exception_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000141A8 File Offset: 0x000123A8
		protected void StartEventPolling(LdapMessageQueue queue, LdapConnection conn, int msgid)
		{
			if (queue == null || conn == null)
			{
				throw new ArgumentException("No parameter can be Null.");
			}
			if (this.m_objEventsGenerator == null)
			{
				this.m_objEventsGenerator = new LdapEventSource.EventsGenerator(this, queue, conn, msgid);
				this.m_objEventsGenerator.SleepTime = this.sleep_interval;
				this.m_objEventsGenerator.StartEventPolling();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000141F9 File Offset: 0x000123F9
		protected void StopEventPolling()
		{
			if (this.m_objEventsGenerator != null)
			{
				this.m_objEventsGenerator.StopEventPolling();
				this.m_objEventsGenerator = null;
			}
		}

		// Token: 0x0600044A RID: 1098
		protected abstract bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType);

		// Token: 0x0600044B RID: 1099 RVA: 0x00014215 File Offset: 0x00012415
		protected void NotifyListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			if (!this.NotifyEventListeners(sourceMessage, aClassification, nType))
			{
				this.NotifyDirectoryListeners(sourceMessage, aClassification);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001422A File Offset: 0x0001242A
		protected void NotifyDirectoryListeners(LdapMessage sourceMessage, EventClassifiers aClassification)
		{
			this.NotifyDirectoryListeners(new DirectoryEventArgs(sourceMessage, aClassification));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014239 File Offset: 0x00012439
		protected void NotifyDirectoryListeners(DirectoryEventArgs objDirectoryEventArgs)
		{
			if (this.directory_event != null)
			{
				this.directory_event(this, objDirectoryEventArgs);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014250 File Offset: 0x00012450
		protected void NotifyExceptionListeners(LdapMessage sourceMessage, LdapException ldapException)
		{
			if (this.directory_exception_event != null)
			{
				this.directory_exception_event(this, new DirectoryExceptionEventArgs(sourceMessage, ldapException));
			}
		}

		// Token: 0x04000311 RID: 785
		protected internal const int EVENT_TYPE_UNKNOWN = -1;

		// Token: 0x04000312 RID: 786
		protected const int DEFAULT_SLEEP_TIME = 1000;

		// Token: 0x04000313 RID: 787
		protected int sleep_interval = 1000;

		// Token: 0x04000314 RID: 788
		protected LdapEventSource.DirectoryEventHandler directory_event;

		// Token: 0x04000315 RID: 789
		protected LdapEventSource.DirectoryExceptionEventHandler directory_exception_event;

		// Token: 0x04000316 RID: 790
		protected LdapEventSource.EventsGenerator m_objEventsGenerator;

		// Token: 0x020000F7 RID: 247
		protected enum LISTENERS_COUNT
		{
			// Token: 0x040004F6 RID: 1270
			ZERO,
			// Token: 0x040004F7 RID: 1271
			ONE,
			// Token: 0x040004F8 RID: 1272
			MORE_THAN_ONE
		}

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x0600063D RID: 1597
		public delegate void DirectoryEventHandler(object source, DirectoryEventArgs objDirectoryEventArgs);

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x06000641 RID: 1601
		public delegate void DirectoryExceptionEventHandler(object source, DirectoryExceptionEventArgs objDirectoryExceptionEventArgs);

		// Token: 0x020000FA RID: 250
		protected class EventsGenerator
		{
			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x00019CAC File Offset: 0x00017EAC
			// (set) Token: 0x06000645 RID: 1605 RVA: 0x00019CB4 File Offset: 0x00017EB4
			public int SleepTime
			{
				get
				{
					return this.sleep_time;
				}
				set
				{
					this.sleep_time = value;
				}
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x00019CBD File Offset: 0x00017EBD
			public EventsGenerator(LdapEventSource objEventSource, LdapMessageQueue queue, LdapConnection conn, int msgid)
			{
				this.m_objLdapEventSource = objEventSource;
				this.searchqueue = queue;
				this.ldapconnection = conn;
				this.messageid = msgid;
				this.sleep_time = 1000;
			}

			// Token: 0x06000647 RID: 1607 RVA: 0x00019CF8 File Offset: 0x00017EF8
			protected void Run()
			{
				while (this.isrunning)
				{
					LdapMessage ldapMessage = null;
					try
					{
						while (this.isrunning && !this.searchqueue.isResponseReceived(this.messageid))
						{
							try
							{
								Thread.Sleep(this.sleep_time);
							}
							catch (ThreadInterruptedException ex)
							{
								Console.WriteLine("EventsGenerator::Run Got ThreadInterruptedException e = {0}", ex);
							}
						}
						if (this.isrunning)
						{
							ldapMessage = this.searchqueue.getResponse(this.messageid);
						}
						if (ldapMessage != null)
						{
							this.processmessage(ldapMessage);
						}
					}
					catch (LdapException ex2)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(ldapMessage, ex2);
					}
				}
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00019DA4 File Offset: 0x00017FA4
			protected void processmessage(LdapMessage response)
			{
				if (response is LdapResponse)
				{
					try
					{
						((LdapResponse)response).chkResultCode();
						this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
						return;
					}
					catch (LdapException ex)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(response, ex);
						return;
					}
				}
				this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x00019E04 File Offset: 0x00018004
			public void StartEventPolling()
			{
				this.isrunning = true;
				new Thread(new ThreadStart(this.Run)).Start();
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x00019E25 File Offset: 0x00018025
			public void StopEventPolling()
			{
				this.isrunning = false;
			}

			// Token: 0x040004F9 RID: 1273
			private LdapEventSource m_objLdapEventSource;

			// Token: 0x040004FA RID: 1274
			private LdapMessageQueue searchqueue;

			// Token: 0x040004FB RID: 1275
			private int messageid;

			// Token: 0x040004FC RID: 1276
			private LdapConnection ldapconnection;

			// Token: 0x040004FD RID: 1277
			private volatile bool isrunning = true;

			// Token: 0x040004FE RID: 1278
			private int sleep_time;
		}
	}
}
