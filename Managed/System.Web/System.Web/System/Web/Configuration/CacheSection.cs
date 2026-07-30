using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the global cache settings for an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x0200058B RID: 1419
	public sealed class CacheSection : ConfigurationSection
	{
		// Token: 0x06003BFF RID: 15359 RVA: 0x000A04C0 File Offset: 0x0009E6C0
		static CacheSection()
		{
			CacheSection.properties.Add(CacheSection.disableExpirationProp);
			CacheSection.properties.Add(CacheSection.disableMemoryCollectionProp);
			CacheSection.properties.Add(CacheSection.percentagePhysicalMemoryUsedLimitProp);
			CacheSection.properties.Add(CacheSection.privateBytesLimitProp);
			CacheSection.properties.Add(CacheSection.privateBytesPollTimeProp);
		}

		/// <summary>Gets or sets a value indicating whether the cache expiration is disabled.</summary>
		/// <returns>true if the cache expiration is disabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000A060B File Offset: 0x0009E80B
		// (set) Token: 0x06003C01 RID: 15361 RVA: 0x000A061D File Offset: 0x0009E81D
		[ConfigurationProperty("disableExpiration", DefaultValue = "False")]
		public bool DisableExpiration
		{
			get
			{
				return (bool)base[CacheSection.disableExpirationProp];
			}
			set
			{
				base[CacheSection.disableExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache memory collection is disabled.</summary>
		/// <returns>true if the cache memory collection is disabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000A0630 File Offset: 0x0009E830
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x000A0642 File Offset: 0x0009E842
		[ConfigurationProperty("disableMemoryCollection", DefaultValue = "False")]
		public bool DisableMemoryCollection
		{
			get
			{
				return (bool)base[CacheSection.disableMemoryCollectionProp];
			}
			set
			{
				base[CacheSection.disableMemoryCollectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum percentage of virtual memory usage.</summary>
		/// <returns>The maximum percentage of virtual memory usage. The default value is 90%.</returns>
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000A0655 File Offset: 0x0009E855
		// (set) Token: 0x06003C05 RID: 15365 RVA: 0x000A0667 File Offset: 0x0009E867
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		[ConfigurationProperty("percentagePhysicalMemoryUsedLimit", DefaultValue = "0")]
		public int PercentagePhysicalMemoryUsedLimit
		{
			get
			{
				return (int)base[CacheSection.percentagePhysicalMemoryUsedLimitProp];
			}
			set
			{
				base[CacheSection.percentagePhysicalMemoryUsedLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum size of the working-process private space.</summary>
		/// <returns>The maximum number, in bytes, of the private space allocated to the working process. The default value is 0.</returns>
		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000A067A File Offset: 0x0009E87A
		// (set) Token: 0x06003C07 RID: 15367 RVA: 0x000A068C File Offset: 0x0009E88C
		[LongValidator(MinValue = 0L, MaxValue = 9223372036854775807L)]
		[ConfigurationProperty("privateBytesLimit", DefaultValue = "0")]
		public long PrivateBytesLimit
		{
			get
			{
				return (long)base[CacheSection.privateBytesLimitProp];
			}
			set
			{
				base[CacheSection.privateBytesLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval between polling for the worker-process memory usage.</summary>
		/// <returns>The time interval between polling for the worker process memory usage. The default value is 2 minutes.</returns>
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000A069F File Offset: 0x0009E89F
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x000A06B1 File Offset: 0x0009E8B1
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("privateBytesPollTime", DefaultValue = "00:02:00")]
		public TimeSpan PrivateBytesPollTime
		{
			get
			{
				return (TimeSpan)base[CacheSection.privateBytesPollTimeProp];
			}
			set
			{
				base[CacheSection.privateBytesPollTimeProp] = value;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000A06C4 File Offset: 0x0009E8C4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CacheSection.properties;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003C0D RID: 15373 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string DefaultProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProviderSettingsCollection Providers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040020A6 RID: 8358
		private static ConfigurationProperty disableExpirationProp = new ConfigurationProperty("disableExpiration", typeof(bool), false);

		// Token: 0x040020A7 RID: 8359
		private static ConfigurationProperty disableMemoryCollectionProp = new ConfigurationProperty("disableMemoryCollection", typeof(bool), false);

		// Token: 0x040020A8 RID: 8360
		private static ConfigurationProperty percentagePhysicalMemoryUsedLimitProp = new ConfigurationProperty("percentagePhysicalMemoryUsedLimit", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020A9 RID: 8361
		private static ConfigurationProperty privateBytesLimitProp = new ConfigurationProperty("privateBytesLimit", typeof(long), 0L, TypeDescriptor.GetConverter(typeof(long)), new LongValidator(0L, long.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040020AA RID: 8362
		private static ConfigurationProperty privateBytesPollTimeProp = new ConfigurationProperty("privateBytesPollTime", typeof(TimeSpan), TimeSpan.FromMinutes(2.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020AB RID: 8363
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
