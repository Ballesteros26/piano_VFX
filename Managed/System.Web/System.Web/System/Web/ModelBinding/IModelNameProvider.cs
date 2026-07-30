using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a way to specify an alternate name to use for model binding instead of using the parameter name.</summary>
	// Token: 0x0200051C RID: 1308
	public interface IModelNameProvider
	{
		/// <summary>When implemented in a class, gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x060039E5 RID: 14821
		string GetModelName();
	}
}
