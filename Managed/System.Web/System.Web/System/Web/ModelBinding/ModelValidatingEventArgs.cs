using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides data for the <see cref="E:System.Web.ModelBinding.ModelValidationNode.Validating" /> event.</summary>
	// Token: 0x020006F9 RID: 1785
	public sealed class ModelValidatingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatingEventArgs" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent model validation node.</param>
		// Token: 0x06004B7F RID: 19327 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelValidatingEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the parent model validation node.</summary>
		/// <returns>The parent model validation node.</returns>
		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06004B81 RID: 19329 RVA: 0x0000E80B File Offset: 0x0000CA0B
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
