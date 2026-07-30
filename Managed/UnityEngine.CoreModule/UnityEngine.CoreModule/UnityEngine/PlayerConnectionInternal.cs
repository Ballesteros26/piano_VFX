using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000179 RID: 377
	[NativeHeader("Runtime/Export/PlayerConnection/PlayerConnectionInternal.bindings.h")]
	internal class PlayerConnectionInternal : IPlayerEditorConnectionNative
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		void IPlayerEditorConnectionNative.SendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			PlayerConnectionInternal.SendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		bool IPlayerEditorConnectionNative.TrySendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			return PlayerConnectionInternal.TrySendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0001E628 File Offset: 0x0001C828
		void IPlayerEditorConnectionNative.Poll()
		{
			PlayerConnectionInternal.PollInternal();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0001E631 File Offset: 0x0001C831
		void IPlayerEditorConnectionNative.RegisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.RegisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0001E646 File Offset: 0x0001C846
		void IPlayerEditorConnectionNative.UnregisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.UnregisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0001E65B File Offset: 0x0001C85B
		void IPlayerEditorConnectionNative.Initialize()
		{
			PlayerConnectionInternal.Initialize();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0001E664 File Offset: 0x0001C864
		bool IPlayerEditorConnectionNative.IsConnected()
		{
			return PlayerConnectionInternal.IsConnected();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0001E67B File Offset: 0x0001C87B
		void IPlayerEditorConnectionNative.DisconnectAll()
		{
			PlayerConnectionInternal.DisconnectAll();
		}

		// Token: 0x06001266 RID: 4710
		[FreeFunction("PlayerConnection_Bindings::IsConnected")]
		[MethodImpl(4096)]
		private static extern bool IsConnected();

		// Token: 0x06001267 RID: 4711
		[FreeFunction("PlayerConnection_Bindings::Initialize")]
		[MethodImpl(4096)]
		private static extern void Initialize();

		// Token: 0x06001268 RID: 4712
		[FreeFunction("PlayerConnection_Bindings::RegisterInternal")]
		[MethodImpl(4096)]
		private static extern void RegisterInternal(string messageId);

		// Token: 0x06001269 RID: 4713
		[FreeFunction("PlayerConnection_Bindings::UnregisterInternal")]
		[MethodImpl(4096)]
		private static extern void UnregisterInternal(string messageId);

		// Token: 0x0600126A RID: 4714
		[FreeFunction("PlayerConnection_Bindings::SendMessage")]
		[MethodImpl(4096)]
		private static extern void SendMessage(string messageId, byte[] data, int playerId);

		// Token: 0x0600126B RID: 4715
		[FreeFunction("PlayerConnection_Bindings::TrySendMessage")]
		[MethodImpl(4096)]
		private static extern bool TrySendMessage(string messageId, byte[] data, int playerId);

		// Token: 0x0600126C RID: 4716
		[FreeFunction("PlayerConnection_Bindings::PollInternal")]
		[MethodImpl(4096)]
		private static extern void PollInternal();

		// Token: 0x0600126D RID: 4717
		[FreeFunction("PlayerConnection_Bindings::DisconnectAll")]
		[MethodImpl(4096)]
		private static extern void DisconnectAll();

		// Token: 0x0200017A RID: 378
		[Flags]
		public enum MulticastFlags
		{
			// Token: 0x04000617 RID: 1559
			kRequestImmediateConnect = 1,
			// Token: 0x04000618 RID: 1560
			kSupportsProfile = 2,
			// Token: 0x04000619 RID: 1561
			kCustomMessage = 4,
			// Token: 0x0400061A RID: 1562
			kUseAlternateIP = 8
		}
	}
}
