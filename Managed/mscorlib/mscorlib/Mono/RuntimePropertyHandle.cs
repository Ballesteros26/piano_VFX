using System;

namespace Mono
{
	// Token: 0x02000017 RID: 23
	internal struct RuntimePropertyHandle
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal RuntimePropertyHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003DD9 File Offset: 0x00001FD9
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimePropertyHandle)obj).Value;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003E2C File Offset: 0x0000202C
		public bool Equals(RuntimePropertyHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E40 File Offset: 0x00002040
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E4D File Offset: 0x0000204D
		public static bool operator ==(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003E57 File Offset: 0x00002057
		public static bool operator !=(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000382 RID: 898
		private IntPtr value;
	}
}
