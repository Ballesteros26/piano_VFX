using System;

namespace System.Web.ModelBinding
{
	/// <summary>Encapsulates the state of model binding.</summary>
	// Token: 0x02000524 RID: 1316
	[Serializable]
	public class ModelState
	{
		/// <summary>Gets or sets an object that encapsulates the value that was being bound during model binding.</summary>
		/// <returns>The value of the model.</returns>
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x0009D0CE File Offset: 0x0009B2CE
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x0009D0D6 File Offset: 0x0009B2D6
		public ValueProviderResult Value { get; set; }

		/// <summary>Gets a collection of errors that occurred during model binding.</summary>
		/// <returns>The collection of errors.</returns>
		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x0009D0DF File Offset: 0x0009B2DF
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x04001F57 RID: 8023
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
