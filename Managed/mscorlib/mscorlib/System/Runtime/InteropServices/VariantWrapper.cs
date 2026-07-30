using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type VT_VARIANT | VT_BYREF from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200090B RID: 2315
	[Serializable]
	public sealed class VariantWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> class for the specified <see cref="T:System.Object" /> parameter.</summary>
		/// <param name="obj">The object to marshal. </param>
		// Token: 0x060055CD RID: 21965 RVA: 0x001292AE File Offset: 0x001274AE
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x001292BD File Offset: 0x001274BD
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002D84 RID: 11652
		private object m_WrappedObject;
	}
}
