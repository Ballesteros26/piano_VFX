using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Identifies a context attribute.</summary>
	// Token: 0x02000783 RID: 1923
	[ComVisible(true)]
	public interface IContextAttribute
	{
		/// <summary>Returns context properties to the caller in the given message.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context properties. </param>
		// Token: 0x06004F26 RID: 20262
		void GetPropertiesForNewContext(IConstructionCallMessage msg);

		/// <summary>Returns a Boolean value indicating whether the specified context meets the context attribute's requirements.</summary>
		/// <returns>true if the passed in context is okay; otherwise, false.</returns>
		/// <param name="ctx">The context to check against the current context attribute. </param>
		/// <param name="msg">The construction call, parameters of which need to be checked against the current context. </param>
		// Token: 0x06004F27 RID: 20263
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);
	}
}
