using System;

namespace System.ComponentModel
{
	/// <summary>Defines the interface for extending properties to other components in a container.</summary>
	// Token: 0x02000280 RID: 640
	public interface IExtenderProvider
	{
		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="extendee">The <see cref="T:System.Object" /> to receive the extender properties. </param>
		// Token: 0x06001467 RID: 5223
		bool CanExtend(object extendee);
	}
}
