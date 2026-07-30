using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	/// <summary>Starts a URL editor that allows a user to select or create a URL. This class cannot be inherited.</summary>
	// Token: 0x020000AE RID: 174
	public sealed class UrlBuilder
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x00002352 File Offset: 0x00000552
		private UrlBuilder()
		{
		}

		/// <summary>Creates a UI to create or pick a URL.</summary>
		/// <returns>The URL returned from the UI.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services. </param>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window. </param>
		/// <param name="initialUrl">The initial URL to be shown in the picker window. </param>
		/// <param name="caption">The caption of the picker window. </param>
		/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window. </param>
		// Token: 0x06000532 RID: 1330 RVA: 0x000094A0 File Offset: 0x000076A0
		[MonoTODO]
		public static string BuildUrl(IComponent component, Control owner, string initialUrl, string caption, string filter)
		{
			return UrlBuilder.BuildUrl(component, owner, initialUrl, caption, filter, UrlBuilderOptions.None);
		}

		/// <summary>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</summary>
		/// <returns>The URL returned from the UI.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services. </param>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window. </param>
		/// <param name="initialUrl">The initial URL to be shown in the picker window. </param>
		/// <param name="caption">The caption of the picker window. </param>
		/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window. </param>
		/// <param name="options">A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> indicating the options for URL selection. </param>
		// Token: 0x06000533 RID: 1331 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string BuildUrl(IComponent component, Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</summary>
		/// <returns>The URL returned from the UI.</returns>
		/// <param name="serviceProvider">The <see cref="T:System.IServiceProvider" /> to be used to access design-time services.</param>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window.</param>
		/// <param name="initialUrl">The initial URL to be shown in the picker window.</param>
		/// <param name="caption">The caption of the picker window.</param>
		/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window.</param>
		/// <param name="options">A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> indicating the options for URL selection.</param>
		// Token: 0x06000534 RID: 1332 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string BuildUrl(IServiceProvider serviceProvider, Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
