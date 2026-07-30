using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an empty metadata provider for data models that do not require metadata.</summary>
	// Token: 0x02000717 RID: 1815
	public class EmptyModelMetadataProvider : AssociatedMetadataProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.EmptyModelMetadataProvider" /> class.</summary>
		// Token: 0x06004BEE RID: 19438 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public EmptyModelMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.ModelBinding.ModelMetadata" /> class.</summary>
		/// <returns>An empty metadata object.</returns>
		/// <param name="attributes">The attributes.</param>
		/// <param name="containerType">The type of the container, or null if there is no container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property or null if the model is not a property.</param>
		// Token: 0x06004BEF RID: 19439 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
