using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event. </summary>
	// Token: 0x02000704 RID: 1796
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public enum NotifyCollectionChangedAction
	{
		/// <summary>One or more items were added to the collection.</summary>
		// Token: 0x04002C5A RID: 11354
		Add,
		/// <summary>One or more items were removed from the collection.</summary>
		// Token: 0x04002C5B RID: 11355
		Remove,
		/// <summary>One or more items were replaced in the collection.</summary>
		// Token: 0x04002C5C RID: 11356
		Replace,
		/// <summary>One or more items were moved within the collection.</summary>
		// Token: 0x04002C5D RID: 11357
		Move,
		/// <summary>The content of the collection changed dramatically.</summary>
		// Token: 0x04002C5E RID: 11358
		Reset
	}
}
