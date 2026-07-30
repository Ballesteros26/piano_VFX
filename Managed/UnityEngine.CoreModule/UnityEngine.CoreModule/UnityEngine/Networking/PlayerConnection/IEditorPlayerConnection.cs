using System;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002FC RID: 764
	public interface IEditorPlayerConnection
	{
		// Token: 0x06001A6C RID: 6764
		void Register(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x06001A6D RID: 6765
		void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x06001A6E RID: 6766
		void DisconnectAll();

		// Token: 0x06001A6F RID: 6767
		void RegisterConnection(UnityAction<int> callback);

		// Token: 0x06001A70 RID: 6768
		void RegisterDisconnection(UnityAction<int> callback);

		// Token: 0x06001A71 RID: 6769
		void UnregisterConnection(UnityAction<int> callback);

		// Token: 0x06001A72 RID: 6770
		void UnregisterDisconnection(UnityAction<int> callback);

		// Token: 0x06001A73 RID: 6771
		void Send(Guid messageId, byte[] data);

		// Token: 0x06001A74 RID: 6772
		bool TrySend(Guid messageId, byte[] data);
	}
}
