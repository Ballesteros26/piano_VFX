using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties and methods any class must implement to support view state management for a server control.</summary>
	// Token: 0x02000182 RID: 386
	public interface IStateManager
	{
		/// <summary>When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.</summary>
		/// <returns>true if a server control is tracking its view state changes; otherwise, false.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000F91 RID: 3985
		bool IsTrackingViewState { get; }

		/// <summary>When implemented by a class, loads the server control's previously saved view state to the control.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
		// Token: 0x06000F92 RID: 3986
		void LoadViewState(object state);

		/// <summary>When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06000F93 RID: 3987
		object SaveViewState();

		/// <summary>When implemented by a class, instructs the server control to track changes to its view state.</summary>
		// Token: 0x06000F94 RID: 3988
		void TrackViewState();
	}
}
