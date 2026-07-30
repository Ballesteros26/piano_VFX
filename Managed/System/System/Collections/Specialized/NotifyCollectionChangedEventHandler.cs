using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Represents the method that handles the <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event. </summary>
	/// <param name="sender">The object that raised the event.</param>
	/// <param name="e">Information about the event.</param>
	// Token: 0x02000706 RID: 1798
	// (Invoke) Token: 0x06003886 RID: 14470
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);
}
