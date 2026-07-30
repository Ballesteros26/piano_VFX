using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020005BA RID: 1466
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">true to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, false. </param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called. </param>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000B4324 File Offset: 0x000B2524
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, false.</returns>
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000B433A File Offset: 0x000B253A
		// (set) Token: 0x06002D90 RID: 11664 RVA: 0x000B4342 File Offset: 0x000B2542
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000B434B File Offset: 0x000B254B
		// (set) Token: 0x06002D92 RID: 11666 RVA: 0x000B4353 File Offset: 0x000B2553
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x040025D8 RID: 9688
		private bool enabled;

		// Token: 0x040025D9 RID: 9689
		private int lingerTime;
	}
}
