using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020003AB RID: 939
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Director/PlayableOutputHandle.bindings.h")]
	public struct PlayableOutputHandle : IEquatable<PlayableOutputHandle>
	{
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x00037A18 File Offset: 0x00035C18
		public static PlayableOutputHandle Null
		{
			get
			{
				return PlayableOutputHandle.m_Null;
			}
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00037A30 File Offset: 0x00035C30
		[VisibleToOtherModules]
		internal bool IsPlayableOutputOfType<T>()
		{
			return this.GetPlayableOutputType() == typeof(T);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00037A54 File Offset: 0x00035C54
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00037A80 File Offset: 0x00035C80
		public static bool operator ==(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00037A9C File Offset: 0x00035C9C
		public static bool operator !=(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return !PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00037AB8 File Offset: 0x00035CB8
		public override bool Equals(object p)
		{
			return p is PlayableOutputHandle && this.Equals((PlayableOutputHandle)p);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00037AE4 File Offset: 0x00035CE4
		public bool Equals(PlayableOutputHandle other)
		{
			return PlayableOutputHandle.CompareVersion(this, other);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00037B04 File Offset: 0x00035D04
		internal static bool CompareVersion(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00037B3A File Offset: 0x00035D3A
		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableOutputHandle.IsNull_Injected(ref this);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00037B42 File Offset: 0x00035D42
		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableOutputHandle.IsValid_Injected(ref this);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00037B4A File Offset: 0x00035D4A
		[FreeFunction("PlayableOutputHandleBindings::GetPlayableOutputType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetPlayableOutputType()
		{
			return PlayableOutputHandle.GetPlayableOutputType_Injected(ref this);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00037B52 File Offset: 0x00035D52
		[FreeFunction("PlayableOutputHandleBindings::GetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetReferenceObject()
		{
			return PlayableOutputHandle.GetReferenceObject_Injected(ref this);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00037B5A File Offset: 0x00035D5A
		[FreeFunction("PlayableOutputHandleBindings::SetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal void SetReferenceObject(Object target)
		{
			PlayableOutputHandle.SetReferenceObject_Injected(ref this, target);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00037B63 File Offset: 0x00035D63
		[FreeFunction("PlayableOutputHandleBindings::GetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetUserData()
		{
			return PlayableOutputHandle.GetUserData_Injected(ref this);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00037B6B File Offset: 0x00035D6B
		[FreeFunction("PlayableOutputHandleBindings::SetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal void SetUserData([Writable] Object target)
		{
			PlayableOutputHandle.SetUserData_Injected(ref this, target);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00037B74 File Offset: 0x00035D74
		[FreeFunction("PlayableOutputHandleBindings::GetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetSourcePlayable()
		{
			PlayableHandle playableHandle;
			PlayableOutputHandle.GetSourcePlayable_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00037B8A File Offset: 0x00035D8A
		[FreeFunction("PlayableOutputHandleBindings::SetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSourcePlayable(PlayableHandle target, int port)
		{
			PlayableOutputHandle.SetSourcePlayable_Injected(ref this, ref target, port);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00037B95 File Offset: 0x00035D95
		[FreeFunction("PlayableOutputHandleBindings::GetSourceOutputPort", HasExplicitThis = true, ThrowsException = true)]
		internal int GetSourceOutputPort()
		{
			return PlayableOutputHandle.GetSourceOutputPort_Injected(ref this);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00037B9D File Offset: 0x00035D9D
		[FreeFunction("PlayableOutputHandleBindings::GetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal float GetWeight()
		{
			return PlayableOutputHandle.GetWeight_Injected(ref this);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00037BA5 File Offset: 0x00035DA5
		[FreeFunction("PlayableOutputHandleBindings::SetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetWeight(float weight)
		{
			PlayableOutputHandle.SetWeight_Injected(ref this, weight);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00037BAE File Offset: 0x00035DAE
		[FreeFunction("PlayableOutputHandleBindings::PushNotification", HasExplicitThis = true, ThrowsException = true)]
		internal void PushNotification(PlayableHandle origin, INotification notification, object context)
		{
			PlayableOutputHandle.PushNotification_Injected(ref this, ref origin, notification, context);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00037BBA File Offset: 0x00035DBA
		[FreeFunction("PlayableOutputHandleBindings::GetNotificationReceivers", HasExplicitThis = true, ThrowsException = true)]
		internal INotificationReceiver[] GetNotificationReceivers()
		{
			return PlayableOutputHandle.GetNotificationReceivers_Injected(ref this);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00037BC2 File Offset: 0x00035DC2
		[FreeFunction("PlayableOutputHandleBindings::AddNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void AddNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.AddNotificationReceiver_Injected(ref this, receiver);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00037BCB File Offset: 0x00035DCB
		[FreeFunction("PlayableOutputHandleBindings::RemoveNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void RemoveNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.RemoveNotificationReceiver_Injected(ref this, receiver);
		}

		// Token: 0x06002132 RID: 8498
		[MethodImpl(4096)]
		private static extern bool IsNull_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002133 RID: 8499
		[MethodImpl(4096)]
		private static extern bool IsValid_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002134 RID: 8500
		[MethodImpl(4096)]
		private static extern Type GetPlayableOutputType_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002135 RID: 8501
		[MethodImpl(4096)]
		private static extern Object GetReferenceObject_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002136 RID: 8502
		[MethodImpl(4096)]
		private static extern void SetReferenceObject_Injected(ref PlayableOutputHandle _unity_self, Object target);

		// Token: 0x06002137 RID: 8503
		[MethodImpl(4096)]
		private static extern Object GetUserData_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002138 RID: 8504
		[MethodImpl(4096)]
		private static extern void SetUserData_Injected(ref PlayableOutputHandle _unity_self, [Writable] Object target);

		// Token: 0x06002139 RID: 8505
		[MethodImpl(4096)]
		private static extern void GetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, out PlayableHandle ret);

		// Token: 0x0600213A RID: 8506
		[MethodImpl(4096)]
		private static extern void SetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle target, int port);

		// Token: 0x0600213B RID: 8507
		[MethodImpl(4096)]
		private static extern int GetSourceOutputPort_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x0600213C RID: 8508
		[MethodImpl(4096)]
		private static extern float GetWeight_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x0600213D RID: 8509
		[MethodImpl(4096)]
		private static extern void SetWeight_Injected(ref PlayableOutputHandle _unity_self, float weight);

		// Token: 0x0600213E RID: 8510
		[MethodImpl(4096)]
		private static extern void PushNotification_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle origin, INotification notification, object context);

		// Token: 0x0600213F RID: 8511
		[MethodImpl(4096)]
		private static extern INotificationReceiver[] GetNotificationReceivers_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002140 RID: 8512
		[MethodImpl(4096)]
		private static extern void AddNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		// Token: 0x06002141 RID: 8513
		[MethodImpl(4096)]
		private static extern void RemoveNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		// Token: 0x04000BB0 RID: 2992
		internal IntPtr m_Handle;

		// Token: 0x04000BB1 RID: 2993
		internal uint m_Version;

		// Token: 0x04000BB2 RID: 2994
		private static readonly PlayableOutputHandle m_Null = default(PlayableOutputHandle);
	}
}
