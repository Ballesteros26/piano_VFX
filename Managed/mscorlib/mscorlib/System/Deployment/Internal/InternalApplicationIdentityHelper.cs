using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal
{
	/// <summary>Provides access to internal properties of an <see cref="T:System.ApplicationIdentity" /> object.</summary>
	// Token: 0x0200025F RID: 607
	[ComVisible(false)]
	public static class InternalApplicationIdentityHelper
	{
		/// <summary>Gets an IDefinitionAppId Interface representing the unique identifier of an <see cref="T:System.ApplicationIdentity" /> object.</summary>
		/// <returns>The unique identifier held by the <see cref="T:System.ApplicationIdentity" /> object.</returns>
		/// <param name="id">The object from which to extract the identifier.</param>
		// Token: 0x06001C14 RID: 7188 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static object GetInternalAppId(ApplicationIdentity id)
		{
			throw new NotImplementedException();
		}
	}
}
