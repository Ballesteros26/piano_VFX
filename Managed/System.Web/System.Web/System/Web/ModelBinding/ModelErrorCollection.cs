using System;
using System.Collections.ObjectModel;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model validation errors.</summary>
	// Token: 0x02000523 RID: 1315
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		/// <summary>Adds a validation error to the collection using the specified exception.</summary>
		/// <param name="exception">The exception.</param>
		// Token: 0x060039FB RID: 14843 RVA: 0x0009D0AA File Offset: 0x0009B2AA
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		/// <summary>Adds a validation error to the collection using the specified error message string.</summary>
		/// <param name="errorMessage">The error message string.</param>
		// Token: 0x060039FC RID: 14844 RVA: 0x0009D0B8 File Offset: 0x0009B2B8
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
