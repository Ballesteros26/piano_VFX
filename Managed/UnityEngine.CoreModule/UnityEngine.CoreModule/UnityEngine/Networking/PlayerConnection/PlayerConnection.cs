using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002FD RID: 765
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0002B298 File Offset: 0x00029498
		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection playerConnection;
				if (flag)
				{
					playerConnection = PlayerConnection.CreateInstance();
				}
				else
				{
					playerConnection = PlayerConnection.s_Instance;
				}
				return playerConnection;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0002B2C8 File Offset: 0x000294C8
		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0002B2E8 File Offset: 0x000294E8
		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0002B318 File Offset: 0x00029518
		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0002B348 File Offset: 0x00029548
		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0002B368 File Offset: 0x00029568
		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.m_PlayerEditorConnectionEvents.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0002B3F8 File Offset: 0x000295F8
		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.m_PlayerEditorConnectionEvents.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0002B460 File Offset: 0x00029660
		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int num in this.m_connectedPlayers)
			{
				callback(num);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0002B4CC File Offset: 0x000296CC
		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0002B4E1 File Offset: 0x000296E1
		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0002B4F6 File Offset: 0x000296F6
		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0002B50C File Offset: 0x0002970C
		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0002B54C File Offset: 0x0002974C
		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0002B58C File Offset: 0x0002978C
		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> unityAction = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime now = DateTime.Now;
			this.Register(messageId, unityAction);
			while ((DateTime.Now - now).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, unityAction);
			return msgReceived;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0002B60E File Offset: 0x0002980E
		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0002B620 File Offset: 0x00029820
		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] array = null;
			bool flag = size > 0UL;
			if (flag)
			{
				array = new byte[size];
				Marshal.Copy(data, array, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), array, (int)guid);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0002B667 File Offset: 0x00029867
		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0002B691 File Offset: 0x00029891
		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}

		// Token: 0x04000822 RID: 2082
		internal static IPlayerEditorConnectionNative connectionNative;

		// Token: 0x04000823 RID: 2083
		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		// Token: 0x04000824 RID: 2084
		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		// Token: 0x04000825 RID: 2085
		private bool m_IsInitilized;

		// Token: 0x04000826 RID: 2086
		private static PlayerConnection s_Instance;
	}
}
