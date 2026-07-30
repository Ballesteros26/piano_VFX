using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Defines the methods that convert permission object state to and from XML element representation.</summary>
	// Token: 0x02000541 RID: 1345
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06003C82 RID: 15490
		void FromXml(SecurityElement e);

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003C83 RID: 15491
		SecurityElement ToXml();
	}
}
