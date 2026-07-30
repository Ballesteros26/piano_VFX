using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model-binder error message providers.</summary>
	// Token: 0x02000723 RID: 1827
	public static class ModelBinderErrorMessageProviders
	{
		/// <summary>Returns the current provider for type-conversion error messages.</summary>
		/// <returns>The error message provider.</returns>
		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static ModelBinderErrorMessageProvider TypeConversionErrorMessageProvider
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

		/// <summary>Returns the current provider for value-required error messages.</summary>
		/// <returns>The error message provider.</returns>
		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004C28 RID: 19496 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static ModelBinderErrorMessageProvider ValueRequiredErrorMessageProvider
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
