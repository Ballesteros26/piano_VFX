using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides a base class from which all objects to be used as evidence must derive. </summary>
	// Token: 0x02000564 RID: 1380
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class EvidenceBase
	{
		/// <summary>Creates a new object that is a complete copy of the current instance.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06003E0A RID: 15882 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		public virtual EvidenceBase Clone()
		{
			throw new NotImplementedException();
		}
	}
}
