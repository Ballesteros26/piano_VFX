using System;
using System.EnterpriseServices;

namespace System.Web.Services
{
	/// <summary>Adding this attribute to a method within an XML Web service created using ASP.NET makes the method callable from remote Web clients. This class cannot be inherited.</summary>
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class WebMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
		// Token: 0x06000010 RID: 16 RVA: 0x00002117 File Offset: 0x00000317
		public WebMethodAttribute()
		{
			this.enableSession = false;
			this.transactionOption = 0;
			this.cacheDuration = 0;
			this.bufferResponse = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
		/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
		// Token: 0x06000011 RID: 17 RVA: 0x0000213B File Offset: 0x0000033B
		public WebMethodAttribute(bool enableSession)
			: this()
		{
			this.EnableSession = enableSession;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
		/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
		/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
		// Token: 0x06000012 RID: 18 RVA: 0x0000214A File Offset: 0x0000034A
		public WebMethodAttribute(bool enableSession, TransactionOption transactionOption)
			: this()
		{
			this.EnableSession = enableSession;
			this.transactionOption = (int)transactionOption;
			this.transactionOptionSpecified = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
		/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
		/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
		/// <param name="cacheDuration">Initializes the number of seconds the response is cached. </param>
		// Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		public WebMethodAttribute(bool enableSession, TransactionOption transactionOption, int cacheDuration)
		{
			this.EnableSession = enableSession;
			this.transactionOption = (int)transactionOption;
			this.transactionOptionSpecified = true;
			this.CacheDuration = cacheDuration;
			this.BufferResponse = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
		/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
		/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
		/// <param name="cacheDuration">Initializes the number of seconds the response is cached. </param>
		/// <param name="bufferResponse">Initializes whether the response for this request is buffered. </param>
		// Token: 0x06000014 RID: 20 RVA: 0x00002192 File Offset: 0x00000392
		public WebMethodAttribute(bool enableSession, TransactionOption transactionOption, int cacheDuration, bool bufferResponse)
		{
			this.EnableSession = enableSession;
			this.transactionOption = (int)transactionOption;
			this.transactionOptionSpecified = true;
			this.CacheDuration = cacheDuration;
			this.BufferResponse = bufferResponse;
		}

		/// <summary>A descriptive message describing the XML Web service method.</summary>
		/// <returns>A descriptive message describing the XML Web service method. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021BE File Offset: 0x000003BE
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021D4 File Offset: 0x000003D4
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
			set
			{
				this.description = value;
				this.descriptionSpecified = true;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021E4 File Offset: 0x000003E4
		internal bool DescriptionSpecified
		{
			get
			{
				return this.descriptionSpecified;
			}
		}

		/// <summary>Indicates whether session state is enabled for an XML Web service method.</summary>
		/// <returns>true if session state is enabled for an XML Web service method. The default is false.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021F4 File Offset: 0x000003F4
		public bool EnableSession
		{
			get
			{
				return this.enableSession;
			}
			set
			{
				this.enableSession = value;
				this.enableSessionSpecified = true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002204 File Offset: 0x00000404
		internal bool EnableSessionSpecified
		{
			get
			{
				return this.enableSessionSpecified;
			}
		}

		/// <summary>Gets or sets the number of seconds the response should be held in the cache.</summary>
		/// <returns>The number of seconds the response should be held in the cache. The default is 0, which means the response is not cached.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000220C File Offset: 0x0000040C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002214 File Offset: 0x00000414
		public int CacheDuration
		{
			get
			{
				return this.cacheDuration;
			}
			set
			{
				this.cacheDuration = value;
				this.cacheDurationSpecified = true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002224 File Offset: 0x00000424
		internal bool CacheDurationSpecified
		{
			get
			{
				return this.cacheDurationSpecified;
			}
		}

		/// <summary>Gets or sets whether the response for this request is buffered.</summary>
		/// <returns>true if the response for this request is buffered; otherwise, false. The default is true.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000222C File Offset: 0x0000042C
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002234 File Offset: 0x00000434
		public bool BufferResponse
		{
			get
			{
				return this.bufferResponse;
			}
			set
			{
				this.bufferResponse = value;
				this.bufferResponseSpecified = true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002244 File Offset: 0x00000444
		internal bool BufferResponseSpecified
		{
			get
			{
				return this.bufferResponseSpecified;
			}
		}

		/// <summary>Indicates the transaction support of an XML Web service method.</summary>
		/// <returns>The transaction support of an XML Web service method. The default is <see cref="F:System.EnterpriseServices.TransactionOption.Disabled" />.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000224C File Offset: 0x0000044C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002254 File Offset: 0x00000454
		public TransactionOption TransactionOption
		{
			get
			{
				return (TransactionOption)this.transactionOption;
			}
			set
			{
				this.transactionOption = (int)value;
				this.transactionOptionSpecified = true;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002264 File Offset: 0x00000464
		internal bool TransactionOptionSpecified
		{
			get
			{
				return this.transactionOptionSpecified;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000226C File Offset: 0x0000046C
		internal bool TransactionEnabled
		{
			get
			{
				return this.transactionOption != 0;
			}
		}

		/// <summary>The name used for the XML Web service method in the data passed to and returned from an XML Web service method.</summary>
		/// <returns>The name used for the XML Web service method in the data passed to and from an XML Web service method. The default is the name of the XML Web service method.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002277 File Offset: 0x00000477
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000228D File Offset: 0x0000048D
		public string MessageName
		{
			get
			{
				if (this.messageName != null)
				{
					return this.messageName;
				}
				return string.Empty;
			}
			set
			{
				this.messageName = value;
				this.messageNameSpecified = true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000229D File Offset: 0x0000049D
		internal bool MessageNameSpecified
		{
			get
			{
				return this.messageNameSpecified;
			}
		}

		// Token: 0x04000067 RID: 103
		private int transactionOption;

		// Token: 0x04000068 RID: 104
		private bool enableSession;

		// Token: 0x04000069 RID: 105
		private int cacheDuration;

		// Token: 0x0400006A RID: 106
		private bool bufferResponse;

		// Token: 0x0400006B RID: 107
		private string description;

		// Token: 0x0400006C RID: 108
		private string messageName;

		// Token: 0x0400006D RID: 109
		private bool transactionOptionSpecified;

		// Token: 0x0400006E RID: 110
		private bool enableSessionSpecified;

		// Token: 0x0400006F RID: 111
		private bool cacheDurationSpecified;

		// Token: 0x04000070 RID: 112
		private bool bufferResponseSpecified;

		// Token: 0x04000071 RID: 113
		private bool descriptionSpecified;

		// Token: 0x04000072 RID: 114
		private bool messageNameSpecified;
	}
}
