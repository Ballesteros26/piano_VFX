using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurface.Loaded" /> event. This class cannot be inherited.</summary>
	// Token: 0x0200012A RID: 298
	public sealed class LoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.LoadedEventArgs" /> class.</summary>
		/// <param name="succeeded">true to indicate that the designer load was successful; otherwise, false.</param>
		/// <param name="errors">A collection of errors that occurred while the designer was loading.</param>
		// Token: 0x060008D5 RID: 2261 RVA: 0x0000F192 File Offset: 0x0000D392
		public LoadedEventArgs(bool succeeded, ICollection errors)
		{
			this._succeeded = succeeded;
			this._errors = errors;
		}

		/// <summary>Gets a collection of errors that occurred while the designer was loading.</summary>
		/// <returns>A collection of errors that occurred while the designer was loading.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		public ICollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets a value that indicates whether the designer load was successful.</summary>
		/// <returns>true if the designer load was successful; otherwise, false.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public bool HasSucceeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x040001F6 RID: 502
		private ICollection _errors;

		// Token: 0x040001F7 RID: 503
		private bool _succeeded;
	}
}
