using System;
using System.Threading;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003E RID: 62
	internal class MessageAgent
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000BF94 File Offset: 0x0000A194
		private void InitBlock()
		{
			this.messages = new MessageVector(5, 5);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000BFA3 File Offset: 0x0000A1A3
		internal virtual object[] MessageArray
		{
			get
			{
				return this.messages.ObjectArray;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		internal virtual int[] MessageIDs
		{
			get
			{
				int count = this.messages.Count;
				int[] array = new int[count];
				for (int i = 0; i < count; i++)
				{
					Message message = (Message)this.messages[i];
					array[i] = message.MessageID;
				}
				return array;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		internal virtual string AgentName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000C000 File Offset: 0x0000A200
		internal virtual int Count
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.messages.Count; i++)
				{
					Message message = (Message)this.messages[i];
					num += message.Count;
				}
				return num;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C041 File Offset: 0x0000A241
		internal MessageAgent()
		{
			this.InitBlock();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C050 File Offset: 0x0000A250
		internal void merge(MessageAgent fromAgent)
		{
			object[] messageArray = fromAgent.MessageArray;
			for (int i = 0; i < messageArray.Length; i++)
			{
				this.messages.Add(messageArray[i]);
				((Message)messageArray[i]).Agent = this;
			}
			object syncRoot = this.messages.SyncRoot;
			lock (syncRoot)
			{
				if (messageArray.Length > 1)
				{
					Monitor.PulseAll(this.messages.SyncRoot);
				}
				else if (messageArray.Length == 1)
				{
					Monitor.Pulse(this.messages.SyncRoot);
				}
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		internal void sleepersAwake(bool all)
		{
			object syncRoot = this.messages.SyncRoot;
			lock (syncRoot)
			{
				if (all)
				{
					Monitor.PulseAll(this.messages.SyncRoot);
				}
				else
				{
					Monitor.Pulse(this.messages.SyncRoot);
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C154 File Offset: 0x0000A354
		internal bool isResponseReceived()
		{
			int count = this.messages.Count;
			int num = this.indexLastRead + 1;
			for (int i = 0; i < count; i++)
			{
				if (num == count)
				{
					num = 0;
				}
				if (((Message)this.messages[num]).hasReplies())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		internal bool isResponseReceived(int msgId)
		{
			bool flag;
			try
			{
				flag = this.messages.findMessageById(msgId).hasReplies();
			}
			catch (FieldAccessException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		internal void Abandon(int msgId, LdapConstraints cons)
		{
			try
			{
				Message message = this.messages.findMessageById(msgId);
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(cons, null);
				return;
			}
			catch (FieldAccessException)
			{
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C224 File Offset: 0x0000A424
		internal void AbandonAll()
		{
			int count = this.messages.Count;
			for (int i = 0; i < count; i++)
			{
				Message message = (Message)this.messages[i];
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(null, null);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C270 File Offset: 0x0000A470
		internal bool isComplete(int msgid)
		{
			try
			{
				if (!this.messages.findMessageById(msgid).Complete)
				{
					return false;
				}
			}
			catch (FieldAccessException)
			{
			}
			return true;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		internal Message getMessage(int msgid)
		{
			return this.messages.findMessageById(msgid);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		internal void sendMessage(Connection conn, LdapMessage msg, int timeOut, LdapMessageQueue queue, BindProperties bindProps)
		{
			Message message = new Message(msg, timeOut, conn, this, queue, bindProps);
			this.messages.Add(message);
			message.sendMessage();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C2EA File Offset: 0x0000A4EA
		internal object getLdapMessage(int msgId)
		{
			return this.getLdapMessage(new Integer32(msgId));
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		internal object getLdapMessage(Integer32 msgId)
		{
			if (this.messages.Count == 0)
			{
				return null;
			}
			if (msgId != null)
			{
				try
				{
					Message message = this.messages.findMessageById(msgId.intValue);
					object obj = message.waitForReply();
					if (!message.acceptsReplies() && !message.hasReplies())
					{
						SupportClass.VectorRemoveElement(this.messages, message);
						message.Abandon(null, null);
					}
					return obj;
				}
				catch (FieldAccessException)
				{
					return null;
				}
			}
			object syncRoot = this.messages.SyncRoot;
			object obj2;
			lock (syncRoot)
			{
				object obj;
				for (;;)
				{
					int num = this.indexLastRead + 1;
					for (int i = 0; i < this.messages.Count; i++)
					{
						if (num >= this.messages.Count)
						{
							num = 0;
						}
						Message message2 = (Message)this.messages[num];
						this.indexLastRead = num++;
						obj = message2.Reply;
						if (!message2.acceptsReplies() && !message2.hasReplies())
						{
							SupportClass.VectorRemoveElement(this.messages, message2);
							message2.Abandon(null, null);
							i--;
						}
						if (obj != null)
						{
							goto Block_12;
						}
					}
					if (this.messages.Count == 0)
					{
						goto Block_14;
					}
					try
					{
						Monitor.Wait(this.messages.SyncRoot);
					}
					catch (ThreadInterruptedException)
					{
					}
				}
				Block_12:
				return obj;
				Block_14:
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C478 File Offset: 0x0000A678
		private void debugDisplayMessages()
		{
		}

		// Token: 0x04000187 RID: 391
		private MessageVector messages;

		// Token: 0x04000188 RID: 392
		private int indexLastRead;

		// Token: 0x04000189 RID: 393
		private static object nameLock = new object();

		// Token: 0x0400018A RID: 394
		private static int agentNum;

		// Token: 0x0400018B RID: 395
		private string name;
	}
}
