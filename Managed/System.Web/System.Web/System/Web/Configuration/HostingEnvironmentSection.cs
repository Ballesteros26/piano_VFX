using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that control the behavior of the application hosting environment. This class cannot be inherited.</summary>
	// Token: 0x020005A5 RID: 1445
	public sealed class HostingEnvironmentSection : ConfigurationSection
	{
		// Token: 0x06003D63 RID: 15715 RVA: 0x000A2E8C File Offset: 0x000A108C
		static HostingEnvironmentSection()
		{
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.idleTimeoutProp);
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.shadowCopyBinAssembliesProp);
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.shutdownTimeoutProp);
		}

		/// <summary>Gets or sets the amount of time, in minutes, before unloading an inactive application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of minutes before unloading an inactive application. </returns>
		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000A2F54 File Offset: 0x000A1154
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000A2F66 File Offset: 0x000A1166
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection.idleTimeoutProp];
			}
			set
			{
				base[HostingEnvironmentSection.idleTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory. </summary>
		/// <returns>true if the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory; otherwise, false. The default is true.</returns>
		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000A2F79 File Offset: 0x000A1179
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000A2F8B File Offset: 0x000A118B
		[ConfigurationProperty("shadowCopyBinAssemblies", DefaultValue = "True")]
		public bool ShadowCopyBinAssemblies
		{
			get
			{
				return (bool)base[HostingEnvironmentSection.shadowCopyBinAssembliesProp];
			}
			set
			{
				base[HostingEnvironmentSection.shadowCopyBinAssembliesProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time, in seconds, to gracefully shut down the application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of seconds to gracefully shut down the application. The default is 30 seconds.</returns>
		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000A2F9E File Offset: 0x000A119E
		// (set) Token: 0x06003D69 RID: 15721 RVA: 0x000A2FB0 File Offset: 0x000A11B0
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection.shutdownTimeoutProp];
			}
			set
			{
				base[HostingEnvironmentSection.shutdownTimeoutProp] = value;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000A2FC3 File Offset: 0x000A11C3
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HostingEnvironmentSection.properties;
			}
		}

		/// <summary>Gets or sets a value that determines how ASP.NET caches URL metadata.</summary>
		/// <returns>A value that determines how ASP.NET caches URL metadata. The default value is 1 minute.</returns>
		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000A2FCC File Offset: 0x000A11CC
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TimeSpan UrlMetadataSlidingExpiration
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002119 RID: 8473
		private static ConfigurationProperty idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400211A RID: 8474
		private static ConfigurationProperty shadowCopyBinAssembliesProp = new ConfigurationProperty("shadowCopyBinAssemblies", typeof(bool), true);

		// Token: 0x0400211B RID: 8475
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400211C RID: 8476
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
