using System;

namespace System.ComponentModel
{
	/// <summary>Defines the mechanism for querying the object for changes and resetting of the changed status.</summary>
	// Token: 0x02000279 RID: 633
	public interface IChangeTracking
	{
		/// <summary>Gets the object's changed status.</summary>
		/// <returns>true if the object’s content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges" />; otherwise, false.</returns>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001440 RID: 5184
		bool IsChanged { get; }

		/// <summary>Resets the object’s state to unchanged by accepting the modifications.</summary>
		// Token: 0x06001441 RID: 5185
		void AcceptChanges();
	}
}
