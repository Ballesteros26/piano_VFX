using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value is changing.</summary>
	// Token: 0x02000287 RID: 647
	public interface INotifyPropertyChanging
	{
		/// <summary>Occurs when a property value is changing.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001474 RID: 5236
		// (remove) Token: 0x06001475 RID: 5237
		event PropertyChangingEventHandler PropertyChanging;
	}
}
