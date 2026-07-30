using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Describes a complex model, using a collection rather than individual properties as the data store.</summary>
	// Token: 0x02000701 RID: 1793
	public class ComplexModel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ComplexModel" /> class.</summary>
		/// <param name="modelMetadata">Metadata for the model.</param>
		/// <param name="propertyMetadata">Metadata for the properties of the model.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelMetadata" /> or <paramref name="propertyMetadata" /> parameter is null.</exception>
		// Token: 0x06004B99 RID: 19353 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ComplexModel(ModelMetadata modelMetadata, IEnumerable<ModelMetadata> propertyMetadata)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the metadata for the model.</summary>
		/// <returns>The metadata for the model.</returns>
		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelMetadata ModelMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the metadata for the properties of the model.</summary>
		/// <returns>The metadata for the properties of the model.</returns>
		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public ReadOnlyCollection<ModelMetadata> PropertyMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a collection that contains entries that correspond to each property for which binding was attempted.</summary>
		/// <returns>Metadata for model properties for which model binding was attempted.</returns>
		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IDictionary<ModelMetadata, ComplexModelResult> Results
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
