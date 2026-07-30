using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	/// <summary>Provides design-time support for localization features to a root designer.</summary>
	// Token: 0x0200012C RID: 300
	[Obsolete("use CodeDomLocalizationProvider")]
	[ProvideProperty("Localizable", typeof(object))]
	[ProvideProperty("Language", typeof(object))]
	[ProvideProperty("LoadLanguage", typeof(object))]
	public class LocalizationExtenderProvider : IExtenderProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" /> class using the specified service provider and base component.</summary>
		/// <param name="serviceProvider">A service provider for the specified component. </param>
		/// <param name="baseComponent">The base component to localize. </param>
		// Token: 0x060008DC RID: 2268 RVA: 0x00002352 File Offset: 0x00000552
		[MonoTODO]
		public LocalizationExtenderProvider(ISite serviceProvider, IComponent baseComponent)
		{
		}

		/// <summary>Indicates whether this object can provide its extender properties to the specified object.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="o">The object to receive the extender properties. </param>
		// Token: 0x060008DD RID: 2269 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool CanExtend(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" />.</summary>
		// Token: 0x060008DE RID: 2270 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060008DF RID: 2271 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current resource culture for the specified object.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> indicating the resource variety.</returns>
		/// <param name="o">The object to get the current resource culture for. </param>
		// Token: 0x060008E0 RID: 2272 RVA: 0x0000234B File Offset: 0x0000054B
		[DesignOnly(true)]
		[Localizable(true)]
		[MonoTODO]
		public CultureInfo GetLanguage(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the default resource culture to use when initializing the values of a localized object at design time.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> indicating the resource culture to use to initialize the values of the specified object.</returns>
		/// <param name="o">The object to get the resource culture for. </param>
		// Token: 0x060008E1 RID: 2273 RVA: 0x0000234B File Offset: 0x0000054B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DesignOnly(true)]
		[MonoTODO]
		public CultureInfo GetLoadLanguage(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object supports resource localization.</summary>
		/// <returns>true if the specified object supports resource localization; otherwise, false.</returns>
		/// <param name="o">The object to check for localization support. </param>
		// Token: 0x060008E2 RID: 2274 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Localizable(true)]
		[DesignOnly(true)]
		public bool GetLocalizable(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Resets the resource culture for the specified object.</summary>
		/// <param name="o">The object to reset the resource culture for. </param>
		// Token: 0x060008E3 RID: 2275 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void ResetLanguage(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current resource culture for the specified object to the specified resource culture.</summary>
		/// <param name="o">The base component to set the resource culture for. </param>
		/// <param name="language">A <see cref="T:System.Globalization.CultureInfo" /> that indicates the resource culture to use. </param>
		// Token: 0x060008E4 RID: 2276 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SetLanguage(object o, CultureInfo language)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a value indicating whether the specified object supports localized resources.</summary>
		/// <param name="o">The base component to set as localizable or not localizable. </param>
		/// <param name="localizable">true if the object supports resource localization; otherwise, false. </param>
		// Token: 0x060008E5 RID: 2277 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SetLocalizable(object o, bool localizable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object must have its localizable values persisted in a resource.</summary>
		/// <returns>true if the localizable values should be persisted in resources; otherwise, false.</returns>
		/// <param name="o">The object to get the language support persistence flag for. </param>
		// Token: 0x060008E6 RID: 2278 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool ShouldSerializeLanguage(object o)
		{
			throw new NotImplementedException();
		}
	}
}
