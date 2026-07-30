using System;

namespace System.ComponentModel
{
	/// <summary>Provides the functionality to offer custom error information that a user interface can bind to.</summary>
	// Token: 0x0200027E RID: 638
	public interface IDataErrorInfo
	{
		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		/// <param name="columnName">The name of the property whose error message to get. </param>
		// Token: 0x1700043B RID: 1083
		string this[string columnName] { get; }

		/// <summary>Gets an error message indicating what is wrong with this object.</summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001463 RID: 5219
		string Error { get; }
	}
}
