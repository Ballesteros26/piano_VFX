using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides a mechanism for retrieving an object to control formatting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018A RID: 394
	[ComVisible(true)]
	public interface IFormatProvider
	{
		/// <summary>Returns an object that provides formatting services for the specified type.</summary>
		/// <returns>An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.</returns>
		/// <param name="formatType">An object that specifies the type of format object to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010A6 RID: 4262
		object GetFormat(Type formatType);
	}
}
