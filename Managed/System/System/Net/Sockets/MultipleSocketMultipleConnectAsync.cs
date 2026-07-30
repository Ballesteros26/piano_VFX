using System;

namespace System.Net.Sockets
{
	// Token: 0x020005DE RID: 1502
	internal class MultipleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x000BBE7D File Offset: 0x000BA07D
		public MultipleSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this.socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this.socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000BBEB0 File Offset: 0x000BA0B0
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this.nextAddress >= this.addressList.Length)
				{
					return null;
				}
				ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this.socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this.socket4;
				}
			}
			return ipaddress;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000BBF1C File Offset: 0x000BA11C
		protected override void OnSucceed()
		{
			if (this.socket4 != null && !this.socket4.Connected)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null && !this.socket6.Connected)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000BBF69 File Offset: 0x000BA169
		protected override void OnFail(bool abortive)
		{
			if (this.socket4 != null)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x04002722 RID: 10018
		private Socket socket4;

		// Token: 0x04002723 RID: 10019
		private Socket socket6;
	}
}
