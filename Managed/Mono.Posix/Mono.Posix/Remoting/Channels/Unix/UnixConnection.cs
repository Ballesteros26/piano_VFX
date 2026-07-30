using System;
using System.IO;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008B RID: 139
	internal class UnixConnection
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		public UnixConnection(HostConnectionPool pool, ReusableUnixClient client)
		{
			this._pool = pool;
			this._client = client;
			this._stream = new BufferedStream(client.GetStream());
			this._controlTime = DateTime.UtcNow;
			this._buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000F109 File Offset: 0x0000D309
		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000F111 File Offset: 0x0000D311
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0000F119 File Offset: 0x0000D319
		public DateTime ControlTime
		{
			get
			{
				return this._controlTime;
			}
			set
			{
				this._controlTime = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000F122 File Offset: 0x0000D322
		public bool IsAlive
		{
			get
			{
				return this._client.IsAlive;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000F12F File Offset: 0x0000D32F
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000F137 File Offset: 0x0000D337
		public void Release()
		{
			this._pool.ReleaseConnection(this);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000F145 File Offset: 0x0000D345
		public void Close()
		{
			this._client.Close();
		}

		// Token: 0x040004B8 RID: 1208
		private DateTime _controlTime;

		// Token: 0x040004B9 RID: 1209
		private Stream _stream;

		// Token: 0x040004BA RID: 1210
		private ReusableUnixClient _client;

		// Token: 0x040004BB RID: 1211
		private HostConnectionPool _pool;

		// Token: 0x040004BC RID: 1212
		private byte[] _buffer;
	}
}
