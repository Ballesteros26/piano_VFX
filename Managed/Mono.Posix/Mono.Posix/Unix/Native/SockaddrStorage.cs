using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x0200006B RID: 107
	[CLSCompliant(false)]
	public sealed class SockaddrStorage : Sockaddr, IEquatable<SockaddrStorage>
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000B6FF File Offset: 0x000098FF
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000B707 File Offset: 0x00009907
		public byte[] data { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000B710 File Offset: 0x00009910
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000B718 File Offset: 0x00009918
		public long data_len { get; set; }

		// Token: 0x06000460 RID: 1120 RVA: 0x0000B721 File Offset: 0x00009921
		internal override byte[] DynamicData()
		{
			return this.data;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000B729 File Offset: 0x00009929
		internal override long GetDynamicLength()
		{
			return this.data_len;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000B731 File Offset: 0x00009931
		internal override void SetDynamicLength(long value)
		{
			this.data_len = value;
		}

		// Token: 0x06000463 RID: 1123
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_SockaddrStorage_get_size", SetLastError = true)]
		private static extern int get_size();

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B73A File Offset: 0x0000993A
		public SockaddrStorage()
			: base((SockaddrType)32769, UnixAddressFamily.AF_UNSPEC)
		{
			this.data = new byte[SockaddrStorage.default_size];
			this.data_len = 0L;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000B760 File Offset: 0x00009960
		public SockaddrStorage(int size)
			: base((SockaddrType)32769, UnixAddressFamily.AF_UNSPEC)
		{
			this.data = new byte[size];
			this.data_len = 0L;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000B784 File Offset: 0x00009984
		public unsafe void SetTo(Sockaddr address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			long nativeSize = address.GetNativeSize();
			if (nativeSize > (long)this.data.Length)
			{
				this.data = new byte[nativeSize];
			}
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy(address, (IntPtr)((void*)ptr)))
			{
				throw new ArgumentException("Failed to convert to native struct", "address");
			}
			array = null;
			this.data_len = nativeSize;
			base.sa_family = address.sa_family;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B810 File Offset: 0x00009A10
		public unsafe void CopyTo(Sockaddr address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (this.data_len < 0L || this.data_len > (long)this.data.Length)
			{
				throw new ArgumentException("data_len < 0 || data_len > data.Length", "this");
			}
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy((IntPtr)((void*)ptr), this.data_len, address))
			{
				throw new ArgumentException("Failed to convert from native struct", "this");
			}
			array = null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000B89C File Offset: 0x00009A9C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{{sa_family={0}, data_len={1}, data=(", base.sa_family, this.data_len);
			int num = 0;
			while ((long)num < this.data_len)
			{
				if (num != 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(this.data[num].ToString("x2"));
				num++;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000B924 File Offset: 0x00009B24
		public override int GetHashCode()
		{
			int num = 4660;
			int num2 = 0;
			while ((long)num2 < this.data_len)
			{
				num += num2 ^ (int)this.data[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000B957 File Offset: 0x00009B57
		public override bool Equals(object obj)
		{
			return obj is SockaddrStorage && this.Equals((SockaddrStorage)obj);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000B970 File Offset: 0x00009B70
		public bool Equals(SockaddrStorage value)
		{
			if (value == null)
			{
				return false;
			}
			if (this.data_len != value.data_len)
			{
				return false;
			}
			int num = 0;
			while ((long)num < this.data_len)
			{
				if (this.data[num] != value.data[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0400047A RID: 1146
		private static readonly int default_size = SockaddrStorage.get_size();
	}
}
