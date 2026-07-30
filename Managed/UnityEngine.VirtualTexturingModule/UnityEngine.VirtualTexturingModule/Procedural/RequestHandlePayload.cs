using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[UsedByNativeCode]
	internal struct RequestHandlePayload : IEquatable<RequestHandlePayload>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002388 File Offset: 0x00000588
		public static bool operator !=(RequestHandlePayload lhs, RequestHandlePayload rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000023A4 File Offset: 0x000005A4
		public override bool Equals(object obj)
		{
			return obj is RequestHandlePayload && this == (RequestHandlePayload)obj;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023D4 File Offset: 0x000005D4
		public bool Equals(RequestHandlePayload other)
		{
			return this == other;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000023F4 File Offset: 0x000005F4
		public override int GetHashCode()
		{
			int num = -2128608763;
			num = num * -1521134295 + this.id.GetHashCode();
			num = num * -1521134295 + this.lifetime.GetHashCode();
			return num * -1521134295 + this.callback.GetHashCode();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000244C File Offset: 0x0000064C
		public static bool operator ==(RequestHandlePayload lhs, RequestHandlePayload rhs)
		{
			return lhs.id == rhs.id && lhs.lifetime == rhs.lifetime && lhs.callback == rhs.callback;
		}

		// Token: 0x0400001F RID: 31
		internal int id;

		// Token: 0x04000020 RID: 32
		internal int lifetime;

		// Token: 0x04000021 RID: 33
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr callback;
	}
}
