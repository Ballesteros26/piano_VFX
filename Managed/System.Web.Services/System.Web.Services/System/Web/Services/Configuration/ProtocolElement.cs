using System;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents a protocol element in the Web Services configuration file. The class cannot be inherited.</summary>
	// Token: 0x0200013F RID: 319
	public sealed class ProtocolElement : ConfigurationElement
	{
		/// <summary>Creates an instance of this class.</summary>
		// Token: 0x060009B8 RID: 2488 RVA: 0x000435F0 File Offset: 0x000417F0
		public ProtocolElement()
		{
			this.properties.Add(this.name);
		}

		/// <summary>Creates an instance of this class, and initializes the <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" /> property.</summary>
		/// <param name="protocol">The value to initialize <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" />.</param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x00043640 File Offset: 0x00041840
		public ProtocolElement(WebServiceProtocols protocol)
			: this()
		{
			this.Name = protocol;
		}

		/// <summary>Gets or sets the protocol name.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> object that represents the protocol name.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0004364F File Offset: 0x0004184F
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00043662 File Offset: 0x00041862
		[ConfigurationProperty("name", IsKey = true, DefaultValue = WebServiceProtocols.Unknown)]
		public WebServiceProtocols Name
		{
			get
			{
				return (WebServiceProtocols)base[this.name];
			}
			set
			{
				if (!this.IsValidProtocolsValue(value))
				{
					value = WebServiceProtocols.Unknown;
				}
				base[this.name] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00043682 File Offset: 0x00041882
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0004368A File Offset: 0x0004188A
		private bool IsValidProtocolsValue(WebServiceProtocols value)
		{
			return Enum.IsDefined(typeof(WebServiceProtocols), value);
		}

		// Token: 0x0400059D RID: 1437
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400059E RID: 1438
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(WebServiceProtocols), WebServiceProtocols.Unknown, ConfigurationPropertyOptions.IsKey);
	}
}
