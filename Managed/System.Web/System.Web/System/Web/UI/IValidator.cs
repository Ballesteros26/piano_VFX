using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties and methods that objects that participate in Web Forms validation must implement.</summary>
	// Token: 0x0200018A RID: 394
	public interface IValidator
	{
		/// <summary>When implemented by a class, gets or sets a value indicating whether the user-entered content in the specified control passes validation.</summary>
		/// <returns>true if the content is valid; otherwise, false.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000FA1 RID: 4001
		// (set) Token: 0x06000FA2 RID: 4002
		bool IsValid { get; set; }

		/// <summary>When implemented by a class, gets or sets the error message text generated when the condition being validated fails.</summary>
		/// <returns>The error message to generate.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000FA3 RID: 4003
		// (set) Token: 0x06000FA4 RID: 4004
		string ErrorMessage { get; set; }

		/// <summary>When implemented by a class, evaluates the condition it checks and updates the <see cref="P:System.Web.UI.IValidator.IsValid" /> property.</summary>
		// Token: 0x06000FA5 RID: 4005
		void Validate();
	}
}
