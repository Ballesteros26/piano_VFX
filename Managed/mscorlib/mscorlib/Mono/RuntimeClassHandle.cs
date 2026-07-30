using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000013 RID: 19
	internal struct RuntimeClassHandle
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003B2C File Offset: 0x00001D2C
		internal unsafe RuntimeClassHandle(RuntimeStructs.MonoClass* value)
		{
			this.value = value;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003B35 File Offset: 0x00001D35
		internal unsafe RuntimeClassHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.MonoClass*)(void*)ptr;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003B43 File Offset: 0x00001D43
		internal unsafe RuntimeStructs.MonoClass* Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003B4C File Offset: 0x00001D4C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeClassHandle)obj).Value;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B94 File Offset: 0x00001D94
		public unsafe override int GetHashCode()
		{
			return ((IntPtr)((void*)this.value)).GetHashCode();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public bool Equals(RuntimeClassHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public static bool operator ==(RuntimeClassHandle left, object right)
		{
			return right != null && right is RuntimeClassHandle && left.Equals((RuntimeClassHandle)right);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003BE1 File Offset: 0x00001DE1
		public static bool operator !=(RuntimeClassHandle left, object right)
		{
			return right == null || !(right is RuntimeClassHandle) || !left.Equals((RuntimeClassHandle)right);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003C00 File Offset: 0x00001E00
		public static bool operator ==(object left, RuntimeClassHandle right)
		{
			return left != null && left is RuntimeClassHandle && ((RuntimeClassHandle)left).Equals(right);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003C2C File Offset: 0x00001E2C
		public static bool operator !=(object left, RuntimeClassHandle right)
		{
			return left == null || !(left is RuntimeClassHandle) || !((RuntimeClassHandle)left).Equals(right);
		}

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr GetTypeFromClass(RuntimeStructs.MonoClass* klass);

		// Token: 0x0600007E RID: 126 RVA: 0x00003C58 File Offset: 0x00001E58
		internal RuntimeTypeHandle GetTypeHandle()
		{
			return new RuntimeTypeHandle(RuntimeClassHandle.GetTypeFromClass(this.value));
		}

		// Token: 0x0400037E RID: 894
		private unsafe RuntimeStructs.MonoClass* value;
	}
}
