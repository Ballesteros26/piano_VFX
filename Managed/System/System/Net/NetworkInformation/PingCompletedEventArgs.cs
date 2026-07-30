using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
	// Token: 0x0200066D RID: 1645
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600344D RID: 13389 RVA: 0x000C32E1 File Offset: 0x000C14E1
		internal PingCompletedEventArgs(Exception ex, bool cancelled, object userState, PingReply reply)
			: base(ex, cancelled, userState)
		{
			this.reply = reply;
		}

		/// <summary>Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol (ICMP) echo request message and receive a corresponding ICMP echo reply message.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that describes the results of the ICMP echo request.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000C32F4 File Offset: 0x000C14F4
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal PingCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400296F RID: 10607
		private PingReply reply;
	}
}
