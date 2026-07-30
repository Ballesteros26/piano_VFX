using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Monitors Web Parts connections for circular connections.</summary>
	// Token: 0x020007C0 RID: 1984
	public sealed class WebPartTracker : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTracker" /> class. </summary>
		/// <param name="webPart">The control to track for circular connections. </param>
		/// <param name="providerConnectionPoint">The connection point used with <paramref name="webPart" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> or <paramref name="providerConnectionPoint" /> is not provided.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerConnectionPoint" /> is not a valid type.</exception>
		// Token: 0x06004FED RID: 20461 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartTracker(WebPart webPart, ProviderConnectionPoint providerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether a provider connection point is involved in more than one connection with a Web Parts control.</summary>
		/// <returns>true if the provider connection point is used in more than one connection with the Web Parts control; otherwise, false.</returns>
		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x000CBA24 File Offset: 0x000C9C24
		public bool IsCircularConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		// Token: 0x06004FEF RID: 20463 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDisposable.Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
