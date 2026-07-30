using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides data for the <see cref="E:System.Web.ModelBinding.ModelValidationNode.Validated" /> event.</summary>
	// Token: 0x020006F8 RID: 1784
	public sealed class ModelValidatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatedEventArgs" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent model validation node.</param>
		// Token: 0x06004B7C RID: 19324 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelValidatedEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the parent model-validation node.</summary>
		/// <returns>The model-validation node.</returns>
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelValidationNode ParentNode
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
