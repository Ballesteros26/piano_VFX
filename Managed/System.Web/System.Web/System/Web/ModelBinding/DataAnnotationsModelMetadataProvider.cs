using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Implements the default model metadata provider.</summary>
	// Token: 0x0200070D RID: 1805
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelMetadataProvider" /> class.</summary>
		// Token: 0x06004BC5 RID: 19397 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsModelMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates metadata for a specified model.</summary>
		/// <returns>Metadata for a model.</returns>
		/// <param name="attributes">The attributes.</param>
		/// <param name="containerType">The type of the container, or null if there is no container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null if the model is not a property.</param>
		// Token: 0x06004BC6 RID: 19398 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
