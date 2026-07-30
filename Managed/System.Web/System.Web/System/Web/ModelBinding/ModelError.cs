using System;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an error that occurs during model binding.</summary>
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class ModelError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception.</summary>
		/// <param name="exception">The exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is null.</exception>
		// Token: 0x060039F4 RID: 14836 RVA: 0x0009D048 File Offset: 0x0009B248
		public ModelError(Exception exception)
			: this(exception, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception and error message.</summary>
		/// <param name="exception">The exception.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is null.</exception>
		// Token: 0x060039F5 RID: 14837 RVA: 0x0009D052 File Offset: 0x0009B252
		public ModelError(Exception exception, string errorMessage)
			: this(errorMessage)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified error message.</summary>
		/// <param name="errorMessage">The error message.</param>
		// Token: 0x060039F6 RID: 14838 RVA: 0x0009D070 File Offset: 0x0009B270
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = errorMessage ?? string.Empty;
		}

		/// <summary>Gets the exception object.</summary>
		/// <returns>The exception object.</returns>
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x0009D088 File Offset: 0x0009B288
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x0009D090 File Offset: 0x0009B290
		public Exception Exception { get; private set; }

		/// <summary>Gets the error message.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x0009D099 File Offset: 0x0009B299
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x0009D0A1 File Offset: 0x0009B2A1
		public string ErrorMessage { get; private set; }
	}
}
