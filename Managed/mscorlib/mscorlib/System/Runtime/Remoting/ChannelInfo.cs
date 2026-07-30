using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000746 RID: 1862
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x06004D23 RID: 19747 RVA: 0x00116AC8 File Offset: 0x00114CC8
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x00116ADB File Offset: 0x00114CDB
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[] { remoteChannelData };
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x00116AF3 File Offset: 0x00114CF3
		// (set) Token: 0x06004D26 RID: 19750 RVA: 0x00116AFB File Offset: 0x00114CFB
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x04002988 RID: 10632
		private object[] channelData;
	}
}
