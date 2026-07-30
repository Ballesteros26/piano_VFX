using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Provides the base functionality for reading data from resource files.</summary>
	// Token: 0x0200029E RID: 670
	[ComVisible(true)]
	public interface IResourceReader : IEnumerable, IDisposable
	{
		/// <summary>Closes the resource reader after releasing any resources associated with it.</summary>
		// Token: 0x06001EE7 RID: 7911
		void Close();

		/// <summary>Returns a dictionary enumerator of the resources for this reader.</summary>
		/// <returns>A dictionary enumerator for the resources for this reader.</returns>
		// Token: 0x06001EE8 RID: 7912
		IDictionaryEnumerator GetEnumerator();
	}
}
