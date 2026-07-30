using System;

namespace System.ComponentModel
{
	/// <summary>Provides support for rolling back the changes</summary>
	// Token: 0x02000289 RID: 649
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		/// <summary>Resets the object’s state to unchanged by rejecting the modifications.</summary>
		// Token: 0x06001477 RID: 5239
		void RejectChanges();
	}
}
