using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Configures a timeout that helps mitigate denial of service attacks by terminating any request that takes longer than the <see cref="P:System.Web.Services.Configuration.SoapEnvelopeProcessingElement.ReadTimeout" /> property value. </summary>
	// Token: 0x02000142 RID: 322
	public sealed class SoapEnvelopeProcessingElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> class. </summary>
		// Token: 0x060009CE RID: 2510 RVA: 0x000438B8 File Offset: 0x00041AB8
		public SoapEnvelopeProcessingElement()
		{
			this.properties.Add(this.readTimeout);
			this.properties.Add(this.strict);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> class using the provided <see cref="T:System.Int32" /> value. </summary>
		/// <param name="readTimeout">The value of the timeout period.</param>
		// Token: 0x060009CF RID: 2511 RVA: 0x00043943 File Offset: 0x00041B43
		public SoapEnvelopeProcessingElement(int readTimeout)
			: this()
		{
			this.ReadTimeout = readTimeout;
		}

		/// <summary>Gets or sets the timeout period used to determine whether to terminate requests to mitigate against denial of service attacks.</summary>
		/// <param name="readTimeout">The time to wait before terminating requests to <see cref="M:System.Xml.XmlReader.Read" /> and <see cref="M:System.Xml.XmlReader.MoveToContent" />.</param>
		/// <param name="strict">Whether to throw an exception if the serializer encounters elements or attributes that were not in the original schema. For details, see the <see cref="P:System.Web.Services.Configuration.SoapEnvelopeProcessingElement.IsStrict" /> property.</param>
		// Token: 0x060009D0 RID: 2512 RVA: 0x00043952 File Offset: 0x00041B52
		public SoapEnvelopeProcessingElement(int readTimeout, bool strict)
			: this()
		{
			this.ReadTimeout = readTimeout;
			this.IsStrict = strict;
		}

		/// <summary>Gets or sets the timeout period used to determine whether to terminate requests to mitigate against denial of service attacks.</summary>
		/// <returns>The time to wait before terminating requests to <see cref="M:System.Xml.XmlReader.Read" /> and <see cref="M:System.Xml.XmlReader.MoveToContent" />.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00043968 File Offset: 0x00041B68
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0004397B File Offset: 0x00041B7B
		[ConfigurationProperty("readTimeout", DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		public int ReadTimeout
		{
			get
			{
				return (int)base[this.readTimeout];
			}
			set
			{
				base[this.readTimeout] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to throw an exception if the serializer encounters unexpected elements or attributes.</summary>
		/// <returns>true if the Web services serializer tries to detect unexpected elements or attributes; otherwise, false. The default is false.</returns>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0004398F File Offset: 0x00041B8F
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x000439A2 File Offset: 0x00041BA2
		[ConfigurationProperty("strict", DefaultValue = false)]
		public bool IsStrict
		{
			get
			{
				return (bool)base[this.strict];
			}
			set
			{
				base[this.strict] = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000439B6 File Offset: 0x00041BB6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040005A8 RID: 1448
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005A9 RID: 1449
		private readonly ConfigurationProperty readTimeout = new ConfigurationProperty("readTimeout", typeof(int), int.MaxValue, new InfiniteIntConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005AA RID: 1450
		private readonly ConfigurationProperty strict = new ConfigurationProperty("strict", typeof(bool), false);
	}
}
