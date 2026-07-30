using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000286 RID: 646
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001472 RID: 5234
		// (remove) Token: 0x06001473 RID: 5235
		event PropertyChangedEventHandler PropertyChanged;
	}
}
