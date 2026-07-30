using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Unix
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class AbstractUnixEndPoint : EndPoint
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		public AbstractUnixEndPoint(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == "")
			{
				throw new ArgumentException("Cannot be empty.", "path");
			}
			this.path = path;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020CE File Offset: 0x000002CE
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D6 File Offset: 0x000002D6
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020DF File Offset: 0x000002DF
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E4 File Offset: 0x000002E4
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2 - 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[3 + i];
			}
			return new AbstractUnixEndPoint(Encoding.Default.GetString(array));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000212C File Offset: 0x0000032C
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.path);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, 3 + bytes.Length);
			socketAddress[2] = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[i + 2 + 1] = bytes[i];
			}
			return socketAddress;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002180 File Offset: 0x00000380
		public override string ToString()
		{
			return this.path;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
		public override int GetHashCode()
		{
			return this.path.GetHashCode();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002198 File Offset: 0x00000398
		public override bool Equals(object o)
		{
			AbstractUnixEndPoint abstractUnixEndPoint = o as AbstractUnixEndPoint;
			return abstractUnixEndPoint != null && abstractUnixEndPoint.path == this.path;
		}

		// Token: 0x0400002C RID: 44
		private string path;
	}
}
