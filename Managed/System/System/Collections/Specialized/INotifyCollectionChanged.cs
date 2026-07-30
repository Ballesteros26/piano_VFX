using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Notifies listeners of dynamic changes, such as when items get added and removed or the whole list is refreshed.</summary>
	// Token: 0x020006F7 RID: 1783
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface INotifyCollectionChanged
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060037ED RID: 14317
		// (remove) Token: 0x060037EE RID: 14318
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
