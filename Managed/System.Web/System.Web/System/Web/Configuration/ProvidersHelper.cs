using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Common;
using System.Data.SqlClient;

namespace System.Web.Configuration
{
	/// <summary>Provides methods for creating provider instances, either singly or in a batch.</summary>
	// Token: 0x020005D0 RID: 1488
	public static class ProvidersHelper
	{
		/// <summary>Initializes and returns a single provider of the given type using the supplied settings.</summary>
		/// <returns>A new provider of the given type using the supplied settings.</returns>
		/// <param name="providerSettings">The settings to be passed to the provider upon initialization.</param>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the provider to be initialized.</param>
		/// <exception cref="T:System.ArgumentException">The provider type defined in configuration was null or an empty string ("").- or -The provider type defined in configuration is not compatible with the type used by the feature that is attempting to create a new instance of the provider.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The provider threw an exception while it was being initialized.- or -An error occurred while attempting to resolve a <see cref="T:System.Type" /> instance for the provider specified in <paramref name="providerSettings" />.</exception>
		// Token: 0x06004038 RID: 16440 RVA: 0x000A948C File Offset: 0x000A768C
		public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
		{
			Type type = HttpApplication.LoadType(providerSettings.Type);
			if (type == null)
			{
				throw new ConfigurationErrorsException(string.Format("Could not find type: {0}", providerSettings.Type));
			}
			if (!providerType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(string.Format("Provider '{0}' must subclass from '{1}'", providerSettings.Name, providerType));
			}
			ProviderBase providerBase = Activator.CreateInstance(type) as ProviderBase;
			NameValueCollection nameValueCollection = new NameValueCollection(providerSettings.Parameters);
			providerBase.Initialize(providerSettings.Name, nameValueCollection);
			return providerBase;
		}

		/// <summary>Initializes a collection of providers of the given type using the supplied settings.</summary>
		/// <param name="configProviders">A collection of settings to be passed to the provider upon initialization.</param>
		/// <param name="providers">The collection used to contain the initialized providers after the method returns.</param>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the providers to be initialized.</param>
		// Token: 0x06004039 RID: 16441 RVA: 0x000A9508 File Offset: 0x000A7708
		public static void InstantiateProviders(ProviderSettingsCollection configProviders, ProviderCollection providers, Type providerType)
		{
			if (!typeof(ProviderBase).IsAssignableFrom(providerType))
			{
				throw new ConfigurationErrorsException(string.Format("type '{0}' must subclass from ProviderBase", providerType));
			}
			foreach (object obj in configProviders)
			{
				ProviderSettings providerSettings = (ProviderSettings)obj;
				providers.Add(ProvidersHelper.InstantiateProvider(providerSettings, providerType));
			}
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000A9588 File Offset: 0x000A7788
		internal static DbProviderFactory GetDbProviderFactory(string providerName)
		{
			DbProviderFactory dbProviderFactory = null;
			if (providerName != null && providerName != "")
			{
				try
				{
					dbProviderFactory = DbProviderFactories.GetFactory(providerName);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				if (dbProviderFactory != null)
				{
					return dbProviderFactory;
				}
			}
			return SqlClientFactory.Instance;
		}
	}
}
