using System;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Represents a read-only collection of <see cref="T:System.Security.Cryptography.ManifestSignatureInformation" /> objects.  </summary>
	// Token: 0x02000364 RID: 868
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManifestSignatureInformationCollection : ReadOnlyCollection<ManifestSignatureInformation>
	{
		// Token: 0x06001A49 RID: 6729 RVA: 0x0000220F File Offset: 0x0000040F
		internal ManifestSignatureInformationCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
