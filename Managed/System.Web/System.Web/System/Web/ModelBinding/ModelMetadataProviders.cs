using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for the current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance.</summary>
	// Token: 0x02000726 RID: 1830
	public static class ModelMetadataProviders
	{
		/// <summary>Gets or sets the current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance.</summary>
		/// <returns>The current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance. The default value is a new instance of the <see cref="T:System.Web.ModelBinding.EmptyModelMetadataProvider" /> class.</returns>
		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004C2C RID: 19500 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static ModelMetadataProvider Current
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
