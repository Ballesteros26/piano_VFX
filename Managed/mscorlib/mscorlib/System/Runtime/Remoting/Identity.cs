using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200074F RID: 1871
	internal abstract class Identity
	{
		// Token: 0x06004D3F RID: 19775 RVA: 0x00116C85 File Offset: 0x00114E85
		public Identity(string objectUri)
		{
			this._objectUri = objectUri;
		}

		// Token: 0x06004D40 RID: 19776
		public abstract ObjRef CreateObjRef(Type requestedType);

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x00116C94 File Offset: 0x00114E94
		public bool IsFromThisAppDomain
		{
			get
			{
				return this._channelSink == null;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x00116C9F File Offset: 0x00114E9F
		// (set) Token: 0x06004D43 RID: 19779 RVA: 0x00116CA7 File Offset: 0x00114EA7
		public IMessageSink ChannelSink
		{
			get
			{
				return this._channelSink;
			}
			set
			{
				this._channelSink = value;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x00116CB0 File Offset: 0x00114EB0
		public IMessageSink EnvoySink
		{
			get
			{
				return this._envoySink;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x00116CB8 File Offset: 0x00114EB8
		// (set) Token: 0x06004D46 RID: 19782 RVA: 0x00116CC0 File Offset: 0x00114EC0
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
			set
			{
				this._objectUri = value;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00116CC9 File Offset: 0x00114EC9
		public bool IsConnected
		{
			get
			{
				return this._objectUri != null;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x00116CD4 File Offset: 0x00114ED4
		// (set) Token: 0x06004D49 RID: 19785 RVA: 0x00116CDC File Offset: 0x00114EDC
		public bool Disposed
		{
			get
			{
				return this._disposed;
			}
			set
			{
				this._disposed = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004D4A RID: 19786 RVA: 0x00116CE5 File Offset: 0x00114EE5
		public DynamicPropertyCollection ClientDynamicProperties
		{
			get
			{
				if (this._clientDynamicProperties == null)
				{
					this._clientDynamicProperties = new DynamicPropertyCollection();
				}
				return this._clientDynamicProperties;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x00116D00 File Offset: 0x00114F00
		public DynamicPropertyCollection ServerDynamicProperties
		{
			get
			{
				if (this._serverDynamicProperties == null)
				{
					this._serverDynamicProperties = new DynamicPropertyCollection();
				}
				return this._serverDynamicProperties;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004D4C RID: 19788 RVA: 0x00116D1B File Offset: 0x00114F1B
		public bool HasClientDynamicSinks
		{
			get
			{
				return this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004D4D RID: 19789 RVA: 0x00116D32 File Offset: 0x00114F32
		public bool HasServerDynamicSinks
		{
			get
			{
				return this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties;
			}
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00116D49 File Offset: 0x00114F49
		public void NotifyClientDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties)
			{
				this._clientDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00116D70 File Offset: 0x00114F70
		public void NotifyServerDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties)
			{
				this._serverDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x04002991 RID: 10641
		protected string _objectUri;

		// Token: 0x04002992 RID: 10642
		protected IMessageSink _channelSink;

		// Token: 0x04002993 RID: 10643
		protected IMessageSink _envoySink;

		// Token: 0x04002994 RID: 10644
		private DynamicPropertyCollection _clientDynamicProperties;

		// Token: 0x04002995 RID: 10645
		private DynamicPropertyCollection _serverDynamicProperties;

		// Token: 0x04002996 RID: 10646
		protected ObjRef _objRef;

		// Token: 0x04002997 RID: 10647
		private bool _disposed;
	}
}
