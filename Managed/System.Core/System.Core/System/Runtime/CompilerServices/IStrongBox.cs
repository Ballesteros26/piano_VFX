using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a property for accessing the value that an object references.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000302 RID: 770
	public interface IStrongBox
	{
		/// <summary>Gets or sets the value that an object references.</summary>
		/// <returns>The value that the object references.</returns>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600176E RID: 5998
		// (set) Token: 0x0600176F RID: 5999
		object Value { get; set; }
	}
}
