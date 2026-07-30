using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a mutable data object.</summary>
	// Token: 0x02000729 RID: 1833
	public class MutableObjectModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.MutableObjectModelBinder" /> class.</summary>
		// Token: 0x06004C33 RID: 19507 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public MutableObjectModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.InvalidOperationException">The model could not be bound because one or more required properties are missing. (The message specifies the first missing property.)</exception>
		// Token: 0x06004C34 RID: 19508 RVA: 0x000CAE1C File Offset: 0x000C901C
		public virtual bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns a value that indicates whether a property can be updated.</summary>
		/// <returns>true if the property can be updated; otherwise, false.</returns>
		/// <param name="propertyMetadata">Metadata for the property to be evaluated.</param>
		// Token: 0x06004C35 RID: 19509 RVA: 0x000CAE38 File Offset: 0x000C9038
		protected virtual bool CanUpdateProperty(ModelMetadata propertyMetadata)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Creates an instance of the model.</summary>
		/// <returns>The model object.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C36 RID: 19510 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object CreateModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates a model instance if an instance does not yet exist in the binding context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C37 RID: 19511 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EnsureModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns metadata for properties of the model.</summary>
		/// <returns>Metadata for properties of the model.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C38 RID: 19512 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		protected virtual IEnumerable<ModelMetadata> GetMetadataForProperties(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Sets the value of a specified property.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <param name="propertyMetadata">Metadata for the property to set.</param>
		/// <param name="complexModelResult">Validation information about the property.</param>
		// Token: 0x06004C39 RID: 19513 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void SetProperty(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, ComplexModelResult complexModelResult)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
