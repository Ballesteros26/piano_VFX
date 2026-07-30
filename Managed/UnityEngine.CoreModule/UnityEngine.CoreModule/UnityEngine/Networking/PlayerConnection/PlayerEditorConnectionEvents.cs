using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000301 RID: 769
	[Serializable]
	internal class PlayerEditorConnectionEvents
	{
		// Token: 0x06001A8E RID: 6798 RVA: 0x0002B70C File Offset: 0x0002990C
		public void InvokeMessageIdSubscribers(Guid messageId, byte[] data, int playerId)
		{
			IEnumerable<PlayerEditorConnectionEvents.MessageTypeSubscribers> enumerable = Enumerable.Where<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(enumerable);
			if (flag)
			{
				Debug.LogError("No actions found for messageId: " + messageId);
			}
			else
			{
				MessageEventArgs messageEventArgs = new MessageEventArgs
				{
					playerId = playerId,
					data = data
				};
				foreach (PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers in enumerable)
				{
					messageTypeSubscribers.messageCallback.Invoke(messageEventArgs);
				}
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0002B7CC File Offset: 0x000299CC
		public UnityEvent<MessageEventArgs> AddAndCreate(Guid messageId)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = Enumerable.SingleOrDefault<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (flag)
			{
				messageTypeSubscribers = new PlayerEditorConnectionEvents.MessageTypeSubscribers
				{
					MessageTypeId = messageId,
					messageCallback = new PlayerEditorConnectionEvents.MessageEvent()
				};
				this.messageTypeSubscribers.Add(messageTypeSubscribers);
			}
			messageTypeSubscribers.subscriberCount++;
			return messageTypeSubscribers.messageCallback;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0002B84C File Offset: 0x00029A4C
		public void UnregisterManagedCallback(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = Enumerable.SingleOrDefault<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (!flag)
			{
				messageTypeSubscribers.subscriberCount--;
				messageTypeSubscribers.messageCallback.RemoveListener(callback);
				bool flag2 = messageTypeSubscribers.subscriberCount <= 0;
				if (flag2)
				{
					this.messageTypeSubscribers.Remove(messageTypeSubscribers);
				}
			}
		}

		// Token: 0x0400082A RID: 2090
		[SerializeField]
		public List<PlayerEditorConnectionEvents.MessageTypeSubscribers> messageTypeSubscribers = new List<PlayerEditorConnectionEvents.MessageTypeSubscribers>();

		// Token: 0x0400082B RID: 2091
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent connectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x0400082C RID: 2092
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent disconnectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x02000302 RID: 770
		[Serializable]
		public class MessageEvent : UnityEvent<MessageEventArgs>
		{
		}

		// Token: 0x02000303 RID: 771
		[Serializable]
		public class ConnectionChangeEvent : UnityEvent<int>
		{
		}

		// Token: 0x02000304 RID: 772
		[Serializable]
		public class MessageTypeSubscribers
		{
			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0002B900 File Offset: 0x00029B00
			// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0002B91D File Offset: 0x00029B1D
			public Guid MessageTypeId
			{
				get
				{
					return new Guid(this.m_messageTypeId);
				}
				set
				{
					this.m_messageTypeId = value.ToString();
				}
			}

			// Token: 0x0400082D RID: 2093
			[SerializeField]
			private string m_messageTypeId;

			// Token: 0x0400082E RID: 2094
			public int subscriberCount = 0;

			// Token: 0x0400082F RID: 2095
			public PlayerEditorConnectionEvents.MessageEvent messageCallback = new PlayerEditorConnectionEvents.MessageEvent();
		}
	}
}
