using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Defines members that data entity classes can implement to provide custom synchronous and asynchronous validation support.</summary>
	// Token: 0x02000285 RID: 645
	public interface INotifyDataErrorInfo
	{
		/// <summary>Gets a value that indicates whether the entity has validation errors. </summary>
		/// <returns>true if the entity currently has validation errors; otherwise, false.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600146E RID: 5230
		bool HasErrors { get; }

		/// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
		/// <returns>The validation errors for the property or entity.</returns>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		// Token: 0x0600146F RID: 5231
		IEnumerable GetErrors(string propertyName);

		/// <summary>Occurs when the validation errors have changed for a property or for the entire entity. </summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001470 RID: 5232
		// (remove) Token: 0x06001471 RID: 5233
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}
