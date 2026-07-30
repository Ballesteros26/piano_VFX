using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> events. </summary>
	// Token: 0x020006D5 RID: 1749
	public class WebPartConnectionsCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> class without a specified connection. </summary>
		/// <param name="provider">The Web Parts control providing data in the connection. </param>
		/// <param name="providerConnectionPoint">The connection point for providing data. </param>
		/// <param name="consumer">The Web Parts control consuming data in the connection. </param>
		/// <param name="consumerConnectionPoint">The connection point for consuming data. </param>
		// Token: 0x06004A31 RID: 18993 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> class with the specified connection. </summary>
		/// <param name="provider">The Web Parts control providing data in the connection. </param>
		/// <param name="providerConnectionPoint">The connection point for providing data. </param>
		/// <param name="consumer">The Web Parts control consuming data in the connection. </param>
		/// <param name="consumerConnectionPoint">The connection point for consuming data. </param>
		/// <param name="connection">The Web Parts connection involved in the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> event. </param>
		// Token: 0x06004A32 RID: 18994 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the Web Parts connection involved in the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> involved in the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> event.</returns>
		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnection Connection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the Web Parts control acting as the consumer in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the consumer in the connection.</returns>
		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart Consumer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the connection point that consumes data in the Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />.</returns>
		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06004A35 RID: 18997 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the Web Parts control acting as the provider in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the provider in the connection.</returns>
		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the connection point that provides data in the Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" />.</returns>
		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
