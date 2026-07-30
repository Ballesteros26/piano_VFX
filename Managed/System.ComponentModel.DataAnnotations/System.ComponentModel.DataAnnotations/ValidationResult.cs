using System;
using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Represents a container for the results of a validation request.</summary>
	// Token: 0x02000042 RID: 66
	public class ValidationResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class by using an error message.</summary>
		/// <param name="errorMessage">The error message.</param>
		// Token: 0x06000186 RID: 390 RVA: 0x0000550E File Offset: 0x0000370E
		public ValidationResult(string errorMessage)
			: this(errorMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class by using an error message and a list of members that have validation errors.</summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="memberNames">The list of member names that have validation errors.</param>
		// Token: 0x06000187 RID: 391 RVA: 0x00005518 File Offset: 0x00003718
		public ValidationResult(string errorMessage, IEnumerable<string> memberNames)
		{
			this._errorMessage = errorMessage;
			this._memberNames = memberNames ?? new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class by using a <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> object.</summary>
		/// <param name="validationResult">The validation result object.</param>
		// Token: 0x06000188 RID: 392 RVA: 0x00005538 File Offset: 0x00003738
		protected ValidationResult(ValidationResult validationResult)
		{
			if (validationResult == null)
			{
				throw new ArgumentNullException("validationResult");
			}
			this._errorMessage = validationResult._errorMessage;
			this._memberNames = validationResult._memberNames;
		}

		/// <summary>Gets the collection of member names that indicate which fields have validation errors.</summary>
		/// <returns>The collection of member names that indicate which fields have validation errors.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005566 File Offset: 0x00003766
		public IEnumerable<string> MemberNames
		{
			get
			{
				return this._memberNames;
			}
		}

		/// <summary>Gets the error message for the validation.</summary>
		/// <returns>The error message for the validation.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000556E File Offset: 0x0000376E
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005576 File Offset: 0x00003776
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
			set
			{
				this._errorMessage = value;
			}
		}

		/// <summary>Returns a string representation of the current validation result.</summary>
		/// <returns>The current validation result.</returns>
		// Token: 0x0600018C RID: 396 RVA: 0x0000557F File Offset: 0x0000377F
		public override string ToString()
		{
			return this.ErrorMessage ?? base.ToString();
		}

		// Token: 0x040000CC RID: 204
		private IEnumerable<string> _memberNames;

		// Token: 0x040000CD RID: 205
		private string _errorMessage;

		/// <summary>Represents the success of the validation (true if validation was successful; otherwise, false).</summary>
		// Token: 0x040000CE RID: 206
		public static readonly ValidationResult Success;
	}
}
