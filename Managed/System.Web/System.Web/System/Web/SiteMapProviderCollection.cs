using System;
using System.Configuration.Provider;

namespace System.Web
{
	/// <summary>Used by the <see cref="T:System.Web.SiteMap" /> class to track the set of <see cref="T:System.Web.SiteMapProvider" /> objects that are available to the <see cref="T:System.Web.SiteMap" /> during site map initialization. This class cannot be inherited. </summary>
	// Token: 0x020000D6 RID: 214
	public sealed class SiteMapProviderCollection : ProviderCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.SiteMapProvider" /> to the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property as the key.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of the <see cref="T:System.Web.SiteMapProvider" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not an instance of the <see cref="T:System.Web.SiteMapProvider" /> class.- or -A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001F454 File Offset: 0x0001D654
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SiteMapProvider))
			{
				throw new InvalidOperationException(string.Format("{0} must implement {1} to act as a site map provider", provider.GetType(), typeof(SiteMapProvider)));
			}
			if (this[provider.Name] != null)
			{
				throw new ArgumentException("Duplicate site map providers");
			}
			base.Add(provider);
		}

		/// <summary>Adds a <see cref="T:System.Web.SiteMapProvider" /> object to the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property as the key.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />.</exception>
		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001F4B7 File Offset: 0x0001D6B7
		public void Add(SiteMapProvider provider)
		{
			this.Add(provider);
		}

		/// <summary>Adds an array of <see cref="T:System.Web.SiteMapProvider" /> objects into the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> properties as keys.</summary>
		/// <param name="providerArray">The array of <see cref="T:System.Web.SiteMapProvider" /> objects to add.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerArray" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public void AddArray(SiteMapProvider[] providerArray)
		{
			foreach (SiteMapProvider siteMapProvider in providerArray)
			{
				this.Add(siteMapProvider);
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.SiteMapProvider" /> object with a specific name from the provider collection.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> that represents a <see cref="T:System.Web.SiteMapProviderCollection" /> element.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.SiteMapProvider" /> to find. </param>
		// Token: 0x1700041E RID: 1054
		public SiteMapProvider this[string name]
		{
			get
			{
				return (SiteMapProvider)base[name];
			}
		}
	}
}
