using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract base class for a custom metadata provider.</summary>
	// Token: 0x020006F4 RID: 1780
	public abstract class ModelMetadataProvider
	{
		/// <summary>When overridden in a derived class, initializes a new instance of the object that derives from the <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> class.</summary>
		// Token: 0x06004B5D RID: 19293 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ModelMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Web.ModelBinding.ModelMetadata" /> object for all properties of a model.</summary>
		/// <returns>A collection of <see cref="T:System.Web.ModelBinding.ModelMetadata" /> objects.</returns>
		/// <param name="container">The container object.</param>
		/// <param name="containerType">The type of the container object.</param>
		// Token: 0x06004B5E RID: 19294
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		/// <summary>Returns metadata for the specified property.</summary>
		/// <returns>Metadata for the specified property.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="containerType">The type of the container.</param>
		/// <param name="propertyName">The name of the property.</param>
		// Token: 0x06004B5F RID: 19295
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		/// <summary>Returns metadata for the specified model type.</summary>
		/// <returns>Metadata for the specified model type.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		// Token: 0x06004B60 RID: 19296
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
