using System;

namespace System.Net.Sockets
{
	// Token: 0x020005DD RID: 1501
	internal class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x000BBDF5 File Offset: 0x000B9FF5
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this.socket = socket;
			this.userSocket = userSocket;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000BBE0C File Offset: 0x000BA00C
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			attemptSocket = this.socket;
			while (this.nextAddress < this.addressList.Length)
			{
				IPAddress ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (this.socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					return ipaddress;
				}
			}
			return null;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000BBE65 File Offset: 0x000BA065
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this.userSocket)
			{
				this.socket.Close();
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnSucceed()
		{
		}

		// Token: 0x04002720 RID: 10016
		private Socket socket;

		// Token: 0x04002721 RID: 10017
		private bool userSocket;
	}
}
