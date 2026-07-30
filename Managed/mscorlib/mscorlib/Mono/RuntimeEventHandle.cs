using System;

namespace Mono
{
	// Token: 0x02000016 RID: 22
	internal struct RuntimeEventHandle
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003D3E File Offset: 0x00001F3E
		internal RuntimeEventHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003D47 File Offset: 0x00001F47
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D50 File Offset: 0x00001F50
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeEventHandle)obj).Value;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003D98 File Offset: 0x00001F98
		public bool Equals(RuntimeEventHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003DAC File Offset: 0x00001FAC
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public static bool operator ==(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public static bool operator !=(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000381 RID: 897
		private IntPtr value;
	}
}
