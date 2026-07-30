using System;
using System.Net;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x020000A2 RID: 162
	internal class SimpleResolverEventArgs : EventArgs
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003A0 RID: 928 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		// (remove) Token: 0x060003A1 RID: 929 RVA: 0x0000BE2C File Offset: 0x0000A02C
		public event EventHandler<SimpleResolverEventArgs> Completed;

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000BE69 File Offset: 0x0000A069
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000BE71 File Offset: 0x0000A071
		public ResolverError ResolverError { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000BE7A File Offset: 0x0000A07A
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000BE82 File Offset: 0x0000A082
		public string ErrorMessage { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000BE8B File Offset: 0x0000A08B
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000BE93 File Offset: 0x0000A093
		public string HostName { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000BE9C File Offset: 0x0000A09C
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public IPHostEntry HostEntry { get; internal set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		public object UserToken { get; set; }

		// Token: 0x060003AD RID: 941 RVA: 0x0000BEBE File Offset: 0x0000A0BE
		internal void Reset(ResolverAsyncOperation op)
		{
			this.ResolverError = ResolverError.NoError;
			this.ErrorMessage = null;
			this.HostEntry = null;
			this.LastOperation = op;
			this.QueryID = 0;
			this.Retries = 0;
			this.PTRAddress = null;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		protected internal void OnCompleted(object sender)
		{
			EventHandler<SimpleResolverEventArgs> completed = this.Completed;
			if (completed != null)
			{
				completed(sender, this);
			}
		}

		// Token: 0x0400090C RID: 2316
		public ResolverAsyncOperation LastOperation;

		// Token: 0x04000910 RID: 2320
		internal ushort QueryID;

		// Token: 0x04000911 RID: 2321
		internal ushort Retries;

		// Token: 0x04000912 RID: 2322
		internal Timer Timer;

		// Token: 0x04000913 RID: 2323
		internal IPAddress PTRAddress;
	}
}
