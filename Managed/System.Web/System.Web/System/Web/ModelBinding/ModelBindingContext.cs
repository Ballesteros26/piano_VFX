using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides the context in which a model binder functions.</summary>
	// Token: 0x020006F0 RID: 1776
	public class ModelBindingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingContext" /> class.</summary>
		// Token: 0x06004B0B RID: 19211 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBindingContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingContext" /> class by using an existing instance of the class.</summary>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B0C RID: 19212 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the model.</summary>
		/// <returns>The model.</returns>
		/// <exception cref="T:System.InvalidOperationException">Model metadata must be set before accessing this property.</exception>
		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B0E RID: 19214 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public object Model
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a collection of model-binder providers.</summary>
		/// <returns>The collection of model-binder providers. If the collection does not exist yet, a new collection of model-binder providers is created and returned.</returns>
		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B10 RID: 19216 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBinderProviderCollection ModelBinderProviders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets metadata for the model.</summary>
		/// <returns>Metadata for the model.</returns>
		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B12 RID: 19218 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelMetadata ModelMetadata
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the model.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B14 RID: 19220 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ModelName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the model state.</summary>
		/// <returns>The model state, or a new instance of <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> if the model state does not exist yet.</returns>
		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B16 RID: 19222 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelStateDictionary ModelState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		/// <exception cref="T:System.InvalidOperationException">Model metadata must be set before accessing this property.</exception>
		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets metadata for properties of the model.</summary>
		/// <returns>Metadata for properties of the model.</returns>
		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets a value that indicates whether client input is validated.</summary>
		/// <returns>true if client input is validated; otherwise, false.</returns>
		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x000CA9F4 File Offset: 0x000C8BF4
		// (set) Token: 0x06004B1A RID: 19226 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ValidateRequest
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

		/// <summary>Gets or sets the validation node.</summary>
		/// <returns>The validation node, or a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class if the validation node does not exist yet.</returns>
		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06004B1B RID: 19227 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B1C RID: 19228 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelValidationNode ValidationNode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the value provider.</summary>
		/// <returns>The value provider.</returns>
		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06004B1D RID: 19229 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B1E RID: 19230 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public IValueProvider ValueProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
