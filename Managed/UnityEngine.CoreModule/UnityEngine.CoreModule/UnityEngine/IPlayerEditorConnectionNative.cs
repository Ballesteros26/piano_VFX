using System;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	internal interface IPlayerEditorConnectionNative
	{
		// Token: 0x06001256 RID: 4694
		void Initialize();

		// Token: 0x06001257 RID: 4695
		void DisconnectAll();

		// Token: 0x06001258 RID: 4696
		void SendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06001259 RID: 4697
		bool TrySendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x0600125A RID: 4698
		void Poll();

		// Token: 0x0600125B RID: 4699
		void RegisterInternal(Guid messageId);

		// Token: 0x0600125C RID: 4700
		void UnregisterInternal(Guid messageId);

		// Token: 0x0600125D RID: 4701
		bool IsConnected();
	}
}
