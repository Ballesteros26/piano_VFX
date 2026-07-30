using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskInputRejected" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023E RID: 574
	public class MaskInputRejectedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskInputRejectedEventArgs" /> class.</summary>
		/// <param name="position">An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</param>
		/// <param name="rejectionHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</param>
		// Token: 0x06002555 RID: 9557 RVA: 0x0008CF7C File Offset: 0x0008B17C
		public MaskInputRejectedEventArgs(int position, MaskedTextResultHint rejectionHint)
		{
			this.position = position;
			this.rejection_hint = rejectionHint;
		}

		/// <summary>Gets the position in the mask corresponding to the invalid input character.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x0008CF94 File Offset: 0x0008B194
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>Gets an enumerated value that describes why the input character was rejected.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0008CF9C File Offset: 0x0008B19C
		public MaskedTextResultHint RejectionHint
		{
			get
			{
				return this.rejection_hint;
			}
		}

		// Token: 0x040012EE RID: 4846
		private int position;

		// Token: 0x040012EF RID: 4847
		private MaskedTextResultHint rejection_hint;
	}
}
