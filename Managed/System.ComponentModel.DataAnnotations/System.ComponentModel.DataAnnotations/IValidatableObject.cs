using System;
using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Provides a way for an object to be invalidated.</summary>
	// Token: 0x0200001A RID: 26
	public interface IValidatableObject
	{
		/// <summary>Determines whether the specified object is valid.</summary>
		/// <returns>A collection that holds failed-validation information.</returns>
		/// <param name="validationContext">The validation context.</param>
		// Token: 0x0600009F RID: 159
		IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
	}
}
