using System;

namespace System.Data.SqlTypes
{
	/// <summary>All the <see cref="N:System.Data.SqlTypes" /> objects and structures implement the INullable interface. </summary>
	// Token: 0x020002BA RID: 698
	public interface INullable
	{
		/// <summary>Indicates whether a structure is null. This property is read-only.</summary>
		/// <returns>
		///   <see cref="T:System.Data.SqlTypes.SqlBoolean" />true if the value of this object is null. Otherwise, false.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001DC6 RID: 7622
		bool IsNull { get; }
	}
}
