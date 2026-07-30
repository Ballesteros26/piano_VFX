using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Defines a method to release allocated resources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000188 RID: 392
	[ComVisible(true)]
	public interface IDisposable
	{
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A4 RID: 4260
		void Dispose();
	}
}
