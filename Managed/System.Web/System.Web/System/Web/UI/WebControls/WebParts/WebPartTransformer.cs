using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides basic implementation for transformer classes to convert data between two incompatible connection points.</summary>
	// Token: 0x020006BB RID: 1723
	public abstract class WebPartTransformer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class. </summary>
		// Token: 0x06004921 RID: 18721 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected WebPartTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Displays an ASP.NET control that configures a transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that configures a transformer.</returns>
		// Token: 0x06004922 RID: 18722 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Loads the configuration state saved with the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartTransformer.SaveConfigurationState" /> method.</summary>
		/// <param name="savedState">An object containing configuration state saved by using <see cref="M:System.Web.UI.WebControls.WebParts.WebPartTransformer.SaveConfigurationState" />.</param>
		// Token: 0x06004923 RID: 18723 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal virtual void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the configuration state set by the user in the ASP.NET configuration control. </summary>
		/// <returns>An object representing the configuration state.</returns>
		// Token: 0x06004924 RID: 18724 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal virtual object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When implemented, provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x06004925 RID: 18725
		public abstract object Transform(object providerData);
	}
}
