using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000069 RID: 105
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class Sockaddr
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B50B File Offset: 0x0000970B
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000B513 File Offset: 0x00009713
		public UnixAddressFamily sa_family
		{
			get
			{
				return this._sa_family;
			}
			set
			{
				this._sa_family = value;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000B51C File Offset: 0x0000971C
		public Sockaddr()
		{
			this.type = SockaddrType.Sockaddr;
			this.sa_family = UnixAddressFamily.AF_UNSPEC;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000B532 File Offset: 0x00009732
		internal Sockaddr(SockaddrType type, UnixAddressFamily sa_family)
		{
			this.type = type;
			this.sa_family = sa_family;
		}

		// Token: 0x0600044F RID: 1103
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Sockaddr_GetNativeSize", SetLastError = true)]
		private unsafe static extern int GetNativeSize(_SockaddrHeader* address, out long size);

		// Token: 0x06000450 RID: 1104 RVA: 0x0000B548 File Offset: 0x00009748
		internal unsafe long GetNativeSize()
		{
			long num;
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(this).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] array;
				byte* ptr3;
				if ((array = Sockaddr.GetDynamicData(this)) == null || array.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &array[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(this, ptr3, false);
				if (Sockaddr.GetNativeSize(Sockaddr.GetNative(&sockaddrDynamic, ptr2), out num) != 0)
				{
					throw new ArgumentException("Failed to get size of native struct", "this");
				}
				array = null;
			}
			return num;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000B5B7 File Offset: 0x000097B7
		internal static Sockaddr GetAddress(Sockaddr address)
		{
			if (address == null)
			{
				return Sockaddr.nullSockaddr;
			}
			return address;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000B5C4 File Offset: 0x000097C4
		internal unsafe static _SockaddrHeader* GetNative(_SockaddrDynamic* dyn, SockaddrType* addr)
		{
			if (dyn->data != null)
			{
				return (_SockaddrHeader*)dyn;
			}
			fixed (SockaddrType* ptr = &Sockaddr.nullSockaddr.type)
			{
				SockaddrType* ptr2 = ptr;
				if (addr == ptr2)
				{
					return null;
				}
			}
			return (_SockaddrHeader*)addr;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000B5F6 File Offset: 0x000097F6
		internal static byte[] GetDynamicData(Sockaddr addr)
		{
			if (addr == null)
			{
				return null;
			}
			return addr.DynamicData();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000B603 File Offset: 0x00009803
		internal virtual byte[] DynamicData()
		{
			return null;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000B606 File Offset: 0x00009806
		internal virtual long GetDynamicLength()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000B60D File Offset: 0x0000980D
		internal virtual void SetDynamicLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000B614 File Offset: 0x00009814
		public SockaddrStorage ToSockaddrStorage()
		{
			SockaddrStorage sockaddrStorage = new SockaddrStorage((int)this.GetNativeSize());
			sockaddrStorage.SetTo(this);
			return sockaddrStorage;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000B62C File Offset: 0x0000982C
		public static Sockaddr FromSockaddrStorage(SockaddrStorage storage)
		{
			Sockaddr sockaddr = new Sockaddr();
			storage.CopyTo(sockaddr);
			return sockaddr;
		}

		// Token: 0x04000471 RID: 1137
		internal SockaddrType type;

		// Token: 0x04000472 RID: 1138
		internal UnixAddressFamily _sa_family;

		// Token: 0x04000473 RID: 1139
		private static Sockaddr nullSockaddr = new Sockaddr();
	}
}
