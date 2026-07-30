using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000010 RID: 16
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class VFXSpawnerState : IDisposable
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002719 File Offset: 0x00000919
		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
		}

		// Token: 0x06000076 RID: 118
		[MethodImpl(4096)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000077 RID: 119 RVA: 0x00002734 File Offset: 0x00000934
		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			return new VFXSpawnerState(IntPtr.Zero, false);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002754 File Offset: 0x00000954
		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptr)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			this.m_Ptr = ptr;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002780 File Offset: 0x00000980
		private void Release()
		{
			bool flag = this.m_Ptr != IntPtr.Zero && this.m_Owner;
			if (flag)
			{
				VFXSpawnerState.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000027C8 File Offset: 0x000009C8
		~VFXSpawnerState()
		{
			this.Release();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000027F8 File Offset: 0x000009F8
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600007C RID: 124
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000280C File Offset: 0x00000A0C
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002827 File Offset: 0x00000A27
		public bool playing
		{
			get
			{
				return this.loopState == VFXSpawnerLoopState.Looping;
			}
			set
			{
				this.loopState = (value ? VFXSpawnerLoopState.Looping : VFXSpawnerLoopState.Finished);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007F RID: 127
		public extern bool newLoop
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000080 RID: 128
		// (set) Token: 0x06000081 RID: 129
		public extern VFXSpawnerLoopState loopState
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		public extern float spawnCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public extern float deltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000086 RID: 134
		// (set) Token: 0x06000087 RID: 135
		public extern float totalTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000088 RID: 136
		// (set) Token: 0x06000089 RID: 137
		public extern float delayBeforeLoop
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008A RID: 138
		// (set) Token: 0x0600008B RID: 139
		public extern float loopDuration
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008C RID: 140
		// (set) Token: 0x0600008D RID: 141
		public extern float delayAfterLoop
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008E RID: 142
		// (set) Token: 0x0600008F RID: 143
		public extern int loopIndex
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144
		// (set) Token: 0x06000091 RID: 145
		public extern int loopCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000092 RID: 146
		public extern VFXEventAttribute vfxEventAttribute
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x040000D4 RID: 212
		private IntPtr m_Ptr;

		// Token: 0x040000D5 RID: 213
		private bool m_Owner;
	}
}
