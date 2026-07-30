using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x0200028C RID: 652
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600147F RID: 5247
		bool IsInitialized { get; }

		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001480 RID: 5248
		// (remove) Token: 0x06001481 RID: 5249
		event EventHandler Initialized;
	}
}
