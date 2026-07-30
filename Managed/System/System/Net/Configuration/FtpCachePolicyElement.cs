using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default FTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x0200069E RID: 1694
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x0600351D RID: 13597 RVA: 0x000C474D File Offset: 0x000C294D
		static FtpCachePolicyElement()
		{
			FtpCachePolicyElement.properties.Add(FtpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets FTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000C4787 File Offset: 0x000C2987
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000C4799 File Offset: 0x000C2999
		[ConfigurationProperty("policyLevel", DefaultValue = "Default")]
		public RequestCacheLevel PolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[FtpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[FtpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000C47AC File Offset: 0x000C29AC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FtpCachePolicyElement.properties;
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002A65 RID: 10853
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default);

		// Token: 0x04002A66 RID: 10854
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
