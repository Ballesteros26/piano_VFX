using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Gets an object's <see cref="T:System.Security.Policy.Evidence" />.</summary>
	// Token: 0x0200053F RID: 1343
	[ComVisible(true)]
	public interface IEvidenceFactory
	{
		/// <summary>Gets <see cref="T:System.Security.Policy.Evidence" /> that verifies the current object's identity.</summary>
		/// <returns>
		///   <see cref="T:System.Security.Policy.Evidence" /> of the current object's identity.</returns>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003C7C RID: 15484
		Evidence Evidence { get; }
	}
}
