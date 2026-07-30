using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.TypeValidationCompleted" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039B RID: 923
	public class TypeValidationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TypeValidationEventArgs" /> class.</summary>
		/// <param name="validatingType">The <see cref="T:System.Type" /> that the formatted input string was being validated against. </param>
		/// <param name="isValidInput">A <see cref="T:System.Boolean" /> value indicating whether the formatted string was successfully converted to the validating type. </param>
		/// <param name="returnValue">An <see cref="T:System.Object" /> that is the result of the formatted string being converted to the target type. </param>
		/// <param name="message">A <see cref="T:System.String" /> containing a description of the conversion process. </param>
		// Token: 0x06004380 RID: 17280 RVA: 0x0010ACDC File Offset: 0x00108EDC
		public TypeValidationEventArgs(Type validatingType, bool isValidInput, object returnValue, string message)
		{
			this.is_valid_input = isValidInput;
			this.message = message;
			this.return_value = returnValue;
			this.validating_type = validatingType;
			this.cancel = false;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled and focus retained by the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control; otherwise, false to continue validation processing.</returns>
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x0010AD14 File Offset: 0x00108F14
		// (set) Token: 0x06004382 RID: 17282 RVA: 0x0010AD1C File Offset: 0x00108F1C
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets a value indicating whether the formatted input string was successfully converted to the validating type.</summary>
		/// <returns>true if the formatted input string can be converted into the type specified by the <see cref="P:System.Windows.Forms.TypeValidationEventArgs.ValidatingType" /> property; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x0010AD28 File Offset: 0x00108F28
		public bool IsValidInput
		{
			get
			{
				return this.is_valid_input;
			}
		}

		/// <summary>Gets a text message describing the conversion process.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a description of the conversion process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x0010AD30 File Offset: 0x00108F30
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the object that results from the conversion of the formatted input string.</summary>
		/// <returns>If the validation is successful, an <see cref="T:System.Object" /> that represents the converted type; otherwise, null. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x0010AD38 File Offset: 0x00108F38
		public object ReturnValue
		{
			get
			{
				return this.return_value;
			}
		}

		/// <summary>Gets the type that the formatted input string is being validated against.</summary>
		/// <returns>The target <see cref="T:System.Type" /> of the conversion process. This should never be null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x0010AD40 File Offset: 0x00108F40
		public Type ValidatingType
		{
			get
			{
				return this.validating_type;
			}
		}

		// Token: 0x04001C5F RID: 7263
		private bool cancel;

		// Token: 0x04001C60 RID: 7264
		private bool is_valid_input;

		// Token: 0x04001C61 RID: 7265
		private string message;

		// Token: 0x04001C62 RID: 7266
		private object return_value;

		// Token: 0x04001C63 RID: 7267
		private Type validating_type;
	}
}
