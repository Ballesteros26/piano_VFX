using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model validation information.</summary>
	// Token: 0x020006F7 RID: 1783
	public sealed class ModelValidationNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class, using the model metadata and state key.</summary>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <param name="modelStateKey">The model state key.</param>
		// Token: 0x06004B6C RID: 19308 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class, using the model metadata, the model state key, and child model-validation nodes.</summary>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <param name="modelStateKey">The model state key.</param>
		/// <param name="childNodes">The model child nodes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelMetadata" /> or <paramref name="modelStateKey" /> parameter is null.</exception>
		// Token: 0x06004B6D RID: 19309 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey, IEnumerable<ModelValidationNode> childNodes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the child nodes.</summary>
		/// <returns>The child nodes.</returns>
		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public ICollection<ModelValidationNode> ChildNodes
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the model metadata.</summary>
		/// <returns>The model metadata.</returns>
		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelMetadata ModelMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model state key.</summary>
		/// <returns>The model state key.</returns>
		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ModelStateKey
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether validation should be suppressed.</summary>
		/// <returns>true if validation should be suppressed; otherwise, false.</returns>
		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x000CAB44 File Offset: 0x000C8D44
		// (set) Token: 0x06004B72 RID: 19314 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool SuppressValidation
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether all properties of the model should be validated.</summary>
		/// <returns>true if all properties of the model should be validated, or false if validation should be skipped.</returns>
		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06004B73 RID: 19315 RVA: 0x000CAB60 File Offset: 0x000C8D60
		// (set) Token: 0x06004B74 RID: 19316 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ValidateAllProperties
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the model has been validated.</summary>
		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06004B75 RID: 19317 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004B76 RID: 19318 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler<ModelValidatedEventArgs> Validated
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the model is being validated.</summary>
		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06004B77 RID: 19319 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004B78 RID: 19320 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler<ModelValidatingEventArgs> Validating
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Combines the current <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> instance with a specified <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> instance.</summary>
		/// <param name="otherNode">The model validation node to combine with the current instance.</param>
		// Token: 0x06004B79 RID: 19321 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CombineWith(ModelValidationNode otherNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the model using the specified execution context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B7A RID: 19322 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the model using the specified execution context and parent node.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent node.</param>
		// Token: 0x06004B7B RID: 19323 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
