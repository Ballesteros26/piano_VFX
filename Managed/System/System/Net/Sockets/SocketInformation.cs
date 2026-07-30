using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020005CF RID: 1487
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000B9CE0 File Offset: 0x000B7EE0
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000B9CE8 File Offset: 0x000B7EE8
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000B9CF1 File Offset: 0x000B7EF1
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000B9CF9 File Offset: 0x000B7EF9
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000B9D02 File Offset: 0x000B7F02
		// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x000B9D0F File Offset: 0x000B7F0F
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000B9D32 File Offset: 0x000B7F32
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000B9D3F File Offset: 0x000B7F3F
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000B9D62 File Offset: 0x000B7F62
		// (set) Token: 0x06002EFC RID: 12028 RVA: 0x000B9D6F File Offset: 0x000B7F6F
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000B9D92 File Offset: 0x000B7F92
		// (set) Token: 0x06002EFE RID: 12030 RVA: 0x000B9D9F File Offset: 0x000B7F9F
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000B9DC2 File Offset: 0x000B7FC2
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x000B9DCA File Offset: 0x000B7FCA
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x040026AD RID: 9901
		private byte[] protocolInformation;

		// Token: 0x040026AE RID: 9902
		private SocketInformationOptions options;

		// Token: 0x040026AF RID: 9903
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
