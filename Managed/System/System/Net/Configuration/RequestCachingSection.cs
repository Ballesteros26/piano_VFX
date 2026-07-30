using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x020006AC RID: 1708
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06003569 RID: 13673 RVA: 0x000C51A0 File Offset: 0x000C33A0
		static RequestCachingSection()
		{
			RequestCachingSection.properties.Add(RequestCachingSection.defaultFtpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultHttpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultPolicyLevelProp);
			RequestCachingSection.properties.Add(RequestCachingSection.disableAllCachingProp);
			RequestCachingSection.properties.Add(RequestCachingSection.isPrivateCacheProp);
			RequestCachingSection.properties.Add(RequestCachingSection.unspecifiedMaximumAgeProp);
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000C52C7 File Offset: 0x000C34C7
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[RequestCachingSection.defaultFtpCachePolicyProp];
			}
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000C52D9 File Offset: 0x000C34D9
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[RequestCachingSection.defaultHttpCachePolicyProp];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000C52EB File Offset: 0x000C34EB
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x000C52FD File Offset: 0x000C34FD
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = "BypassCache")]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[RequestCachingSection.defaultPolicyLevelProp];
			}
			set
			{
				base[RequestCachingSection.defaultPolicyLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>true if caching is disabled on the local computer; otherwise, false.</returns>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000C5310 File Offset: 0x000C3510
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x000C5322 File Offset: 0x000C3522
		[ConfigurationProperty("disableAllCaching", DefaultValue = "False")]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[RequestCachingSection.disableAllCachingProp];
			}
			set
			{
				base[RequestCachingSection.disableAllCachingProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>true if the cache provides user isolation; otherwise, false.</returns>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000C5335 File Offset: 0x000C3535
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000C5347 File Offset: 0x000C3547
		[ConfigurationProperty("isPrivateCache", DefaultValue = "True")]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[RequestCachingSection.isPrivateCacheProp];
			}
			set
			{
				base[RequestCachingSection.isPrivateCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000C535A File Offset: 0x000C355A
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000C536C File Offset: 0x000C356C
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[RequestCachingSection.unspecifiedMaximumAgeProp];
			}
			set
			{
				base[RequestCachingSection.unspecifiedMaximumAgeProp] = value;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000C537F File Offset: 0x000C357F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RequestCachingSection.properties;
			}
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000C4A98 File Offset: 0x000C2C98
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000C5386 File Offset: 0x000C3586
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x04002A89 RID: 10889
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A8A RID: 10890
		private static ConfigurationProperty defaultFtpCachePolicyProp = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement));

		// Token: 0x04002A8B RID: 10891
		private static ConfigurationProperty defaultHttpCachePolicyProp = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement));

		// Token: 0x04002A8C RID: 10892
		private static ConfigurationProperty defaultPolicyLevelProp = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache);

		// Token: 0x04002A8D RID: 10893
		private static ConfigurationProperty disableAllCachingProp = new ConfigurationProperty("disableAllCaching", typeof(bool), false);

		// Token: 0x04002A8E RID: 10894
		private static ConfigurationProperty isPrivateCacheProp = new ConfigurationProperty("isPrivateCache", typeof(bool), true);

		// Token: 0x04002A8F RID: 10895
		private static ConfigurationProperty unspecifiedMaximumAgeProp = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), new TimeSpan(1, 0, 0, 0));
	}
}
