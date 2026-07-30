using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	[NativeHeader("Runtime/Export/Networking/Ping.bindings.h")]
	public sealed class Ping
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0001E4F5 File Offset: 0x0001C6F5
		public Ping(string address)
		{
			this.m_Ptr = Ping.Internal_Create(address);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0001E50C File Offset: 0x0001C70C
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0001E53C File Offset: 0x0001C73C
		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (!flag)
			{
				Ping.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06001250 RID: 4688
		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06001251 RID: 4689
		[FreeFunction("CreatePing")]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(string address);

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0001E578 File Offset: 0x0001C778
		public bool isDone
		{
			get
			{
				bool flag = this.m_Ptr == IntPtr.Zero;
				return !flag && this.Internal_IsDone();
			}
		}

		// Token: 0x06001253 RID: 4691
		[NativeName("GetIsDone")]
		[MethodImpl(4096)]
		private extern bool Internal_IsDone();

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001254 RID: 4692
		public extern int time
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001255 RID: 4693
		public extern string ip
		{
			[NativeName("GetIP")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x04000615 RID: 1557
		internal IntPtr m_Ptr;
	}
}
