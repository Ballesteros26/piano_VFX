using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200079C RID: 1948
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x06004FAA RID: 20394 RVA: 0x0011EAB0 File Offset: 0x0011CCB0
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				ChannelServices.RegisterChannel(new CrossAppDomainChannel());
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x0011EAF4 File Offset: 0x0011CCF4
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x0011EAFB File Offset: 0x0011CCFB
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0011EAFF File Offset: 0x0011CCFF
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x0011EB05 File Offset: 0x0011CD05
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0011EB11 File Offset: 0x0011CD11
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException("CrossAppdomain channel dont support UrlsForUri");
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0011EB20 File Offset: 0x0011CD20
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x04002A56 RID: 10838
		private const string _strName = "MONOCAD";

		// Token: 0x04002A57 RID: 10839
		private static object s_lock = new object();
	}
}
