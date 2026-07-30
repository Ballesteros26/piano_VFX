using System;
using System.Threading;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003D RID: 61
	internal class Message
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000B962 File Offset: 0x00009B62
		private void InitBlock()
		{
			this.replies = new MessageVector(5, 5);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B974 File Offset: 0x00009B74
		internal virtual int Count
		{
			get
			{
				int count = this.replies.Count;
				if (!this.complete)
				{
					return count;
				}
				if (count <= 0)
				{
					return count;
				}
				return count - 1;
			}
		}

		// Token: 0x170000AF RID: 175
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		internal virtual MessageAgent Agent
		{
			set
			{
				this.agent = value;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		internal virtual bool hasReplies()
		{
			return this.replies != null && this.replies.Count > 0;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000B9C3 File Offset: 0x00009BC3
		internal virtual int MessageType
		{
			get
			{
				if (this.msg == null)
				{
					return -1;
				}
				return this.msg.Type;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B9DA File Offset: 0x00009BDA
		internal virtual int MessageID
		{
			get
			{
				return this.msgId;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		internal virtual bool Complete
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B9EC File Offset: 0x00009BEC
		internal virtual object waitForReply()
		{
			if (this.replies == null)
			{
				return null;
			}
			object syncRoot = this.replies.SyncRoot;
			object obj3;
			lock (syncRoot)
			{
				while (this.waitForReply_Renamed_Field)
				{
					if (this.replies.Count != 0)
					{
						object obj = this.replies[0];
						this.replies.RemoveAt(0);
						object obj2 = obj;
						if ((this.complete || !this.acceptReplies) && this.replies.Count == 0)
						{
							this.conn.removeMessage(this);
						}
						return obj2;
					}
					try
					{
						Monitor.Wait(this.replies.SyncRoot);
					}
					catch (ThreadInterruptedException)
					{
					}
					if (!this.waitForReply_Renamed_Field)
					{
						break;
					}
				}
				obj3 = null;
			}
			return obj3;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		internal virtual object Reply
		{
			get
			{
				if (this.replies == null)
				{
					return null;
				}
				object syncRoot = this.replies.SyncRoot;
				object obj2;
				lock (syncRoot)
				{
					if (this.replies.Count == 0)
					{
						return null;
					}
					object obj = this.replies[0];
					this.replies.RemoveAt(0);
					obj2 = obj;
				}
				if (this.conn != null && (this.complete || !this.acceptReplies) && this.replies.Count == 0)
				{
					this.conn.removeMessage(this);
				}
				return obj2;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BB6C File Offset: 0x00009D6C
		internal virtual bool acceptsReplies()
		{
			return this.acceptReplies;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000BB74 File Offset: 0x00009D74
		internal virtual LdapMessage Request
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000BB7C File Offset: 0x00009D7C
		internal virtual bool BindRequest
		{
			get
			{
				return this.bindprops != null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000BB87 File Offset: 0x00009D87
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BB90 File Offset: 0x00009D90
		internal Message(LdapMessage msg, int mslimit, Connection conn, MessageAgent agent, LdapMessageQueue queue, BindProperties bindprops)
		{
			this.InitBlock();
			this.msg = msg;
			this.conn = conn;
			this.agent = agent;
			this.queue = queue;
			this.mslimit = mslimit;
			this.msgId = msg.MessageID;
			this.bindprops = bindprops;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		internal void sendMessage()
		{
			this.conn.writeMessage(this);
			if (this.mslimit != 0)
			{
				int type = this.msg.Type;
				if (type == 2 || type == 16)
				{
					this.mslimit = 0;
					return;
				}
				this.timer = new Message.Timeout(this, this.mslimit, this);
				this.timer.IsBackground = true;
				this.timer.Start();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BC58 File Offset: 0x00009E58
		internal virtual void Abandon(LdapConstraints cons, InterThreadException informUserEx)
		{
			if (!this.waitForReply_Renamed_Field)
			{
				return;
			}
			this.acceptReplies = false;
			this.waitForReply_Renamed_Field = false;
			if (!this.complete)
			{
				try
				{
					if (this.bindprops != null)
					{
						int bindSemId;
						if (this.conn.BindSemIdClear)
						{
							bindSemId = this.msgId;
						}
						else
						{
							bindSemId = this.conn.BindSemId;
							this.conn.clearBindSemId();
						}
						this.conn.freeWriteSemaphore(bindSemId);
					}
					LdapControl[] array = null;
					if (cons != null)
					{
						array = cons.getControls();
					}
					LdapMessage ldapMessage = new LdapAbandonRequest(this.msgId, array);
					this.conn.writeMessage(ldapMessage);
				}
				catch (LdapException)
				{
				}
				if (informUserEx == null)
				{
					this.agent.Abandon(this.msgId, null);
				}
				this.conn.removeMessage(this);
			}
			if (informUserEx != null)
			{
				this.replies.Add(new LdapResponse(informUserEx, this.conn.ActiveReferral));
				this.stopTimer();
				this.sleepersAwake();
				return;
			}
			this.sleepersAwake();
			this.cleanup();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BD5C File Offset: 0x00009F5C
		private void cleanup()
		{
			this.stopTimer();
			try
			{
				this.acceptReplies = false;
				if (this.conn != null)
				{
					this.conn.removeMessage(this);
				}
				if (this.replies != null)
				{
					while (this.replies.Count != 0)
					{
						object obj = this.replies[0];
						this.replies.RemoveAt(0);
					}
				}
			}
			catch (Exception)
			{
			}
			this.conn = null;
			this.msg = null;
			this.queue = null;
			this.bindprops = null;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BDEC File Offset: 0x00009FEC
		~Message()
		{
			this.cleanup();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BE18 File Offset: 0x0000A018
		internal virtual void putReply(RfcLdapMessage message)
		{
			if (!this.acceptReplies)
			{
				return;
			}
			MessageVector messageVector = this.replies;
			lock (messageVector)
			{
				this.replies.Add(message);
			}
			message.RequestingMessage = this.msg;
			int type = message.Type;
			if (type != 4 && type != 19 && type != 25)
			{
				this.stopTimer();
				this.acceptReplies = false;
				this.complete = true;
				if (this.bindprops != null)
				{
					int num = ((RfcResponse)message.Response).getResultCode().intValue();
					if (num != 14)
					{
						if (num == 0)
						{
							this.conn.BindProperties = this.bindprops;
						}
						int bindSemId;
						if (this.conn.BindSemIdClear)
						{
							bindSemId = this.msgId;
						}
						else
						{
							bindSemId = this.conn.BindSemId;
							this.conn.clearBindSemId();
						}
						this.conn.freeWriteSemaphore(bindSemId);
					}
				}
			}
			this.sleepersAwake();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BF20 File Offset: 0x0000A120
		internal virtual void stopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Interrupt();
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BF38 File Offset: 0x0000A138
		private void sleepersAwake()
		{
			object syncRoot = this.replies.SyncRoot;
			lock (syncRoot)
			{
				Monitor.Pulse(this.replies.SyncRoot);
			}
			this.agent.sleepersAwake(false);
		}

		// Token: 0x0400017A RID: 378
		private LdapMessage msg;

		// Token: 0x0400017B RID: 379
		private Connection conn;

		// Token: 0x0400017C RID: 380
		private MessageAgent agent;

		// Token: 0x0400017D RID: 381
		private LdapMessageQueue queue;

		// Token: 0x0400017E RID: 382
		private int mslimit;

		// Token: 0x0400017F RID: 383
		private SupportClass.ThreadClass timer;

		// Token: 0x04000180 RID: 384
		private MessageVector replies;

		// Token: 0x04000181 RID: 385
		private int msgId;

		// Token: 0x04000182 RID: 386
		private bool acceptReplies = true;

		// Token: 0x04000183 RID: 387
		private bool waitForReply_Renamed_Field = true;

		// Token: 0x04000184 RID: 388
		private bool complete;

		// Token: 0x04000185 RID: 389
		private string name;

		// Token: 0x04000186 RID: 390
		private BindProperties bindprops;

		// Token: 0x020000F3 RID: 243
		private sealed class Timeout : SupportClass.ThreadClass
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x000193D7 File Offset: 0x000175D7
			private void InitBlock(Message enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000625 RID: 1573 RVA: 0x000193E0 File Offset: 0x000175E0
			public Message Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x000193E8 File Offset: 0x000175E8
			internal Timeout(Message enclosingInstance, int interval, Message msg)
			{
				this.InitBlock(enclosingInstance);
				this.timeToWait = interval;
				this.message = msg;
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x00019408 File Offset: 0x00017608
			public override void Run()
			{
				try
				{
					Thread.Sleep(new TimeSpan((long)(10000 * this.timeToWait)));
					this.message.acceptReplies = false;
					this.message.Abandon(null, new InterThreadException("Client request timed out", null, 85, null, this.message));
				}
				catch (ThreadInterruptedException)
				{
				}
			}

			// Token: 0x040004E5 RID: 1253
			private Message enclosingInstance;

			// Token: 0x040004E6 RID: 1254
			private int timeToWait;

			// Token: 0x040004E7 RID: 1255
			private Message message;
		}
	}
}
