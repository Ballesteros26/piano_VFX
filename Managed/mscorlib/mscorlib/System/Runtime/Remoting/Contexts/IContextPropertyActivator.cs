using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property is interested in participating in activation and might not have provided a message sink.</summary>
	// Token: 0x02000785 RID: 1925
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		/// <summary>Called on each client context property that has this interface, before the construction request leaves the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06004F2B RID: 20267
		void CollectFromClientContext(IConstructionCallMessage msg);

		/// <summary>Called on each server context property that has this interface, before the construction response leaves the server for the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />. </param>
		// Token: 0x06004F2C RID: 20268
		void CollectFromServerContext(IConstructionReturnMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <returns>true if successful; otherwise, false.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06004F2D RID: 20269
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <returns>true if successful; otherwise, false.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />. </param>
		// Token: 0x06004F2E RID: 20270
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);

		/// <summary>Indicates whether it is all right to activate the object type indicated in the <paramref name="msg" /> parameter.</summary>
		/// <returns>A Boolean value indicating whether the requested type can be activated.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06004F2F RID: 20271
		bool IsOKToActivate(IConstructionCallMessage msg);
	}
}
