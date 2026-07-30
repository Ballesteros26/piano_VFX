using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Posix
{
	// Token: 0x0200009E RID: 158
	[Obsolete("Use Mono.Unix.UnixEndPoint")]
	[Serializable]
	public class UnixEndPoint : EndPoint
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x000103DE File Offset: 0x0000E5DE
		public UnixEndPoint(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (filename == "")
			{
				throw new ArgumentException("Cannot be empty.", "filename");
			}
			this.filename = filename;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00010418 File Offset: 0x0000E618
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00010420 File Offset: 0x0000E620
		public string Filename
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00010429 File Offset: 0x0000E629
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001042C File Offset: 0x0000E62C
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[i + 2];
			}
			return new UnixEndPoint(Encoding.Default.GetString(array));
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00010474 File Offset: 0x0000E674
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.filename);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, bytes.Length + 2);
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[i + 2] = bytes[i];
			}
			return socketAddress;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000104BE File Offset: 0x0000E6BE
		public override string ToString()
		{
			return this.filename;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000104C6 File Offset: 0x0000E6C6
		public override int GetHashCode()
		{
			return this.filename.GetHashCode();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public override bool Equals(object o)
		{
			UnixEndPoint unixEndPoint = o as UnixEndPoint;
			return unixEndPoint != null && unixEndPoint.filename == this.filename;
		}

		// Token: 0x04000544 RID: 1348
		private string filename;
	}
}
