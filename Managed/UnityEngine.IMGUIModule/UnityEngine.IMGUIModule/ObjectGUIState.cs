using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal class ObjectGUIState : IDisposable
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EB87 File Offset: 0x0000CD87
		public ObjectGUIState()
		{
			this.m_Ptr = ObjectGUIState.Internal_Create();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		~ObjectGUIState()
		{
			this.Destroy();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectGUIState.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060003F8 RID: 1016
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create();

		// Token: 0x060003F9 RID: 1017
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0400011E RID: 286
		internal IntPtr m_Ptr;
	}
}
