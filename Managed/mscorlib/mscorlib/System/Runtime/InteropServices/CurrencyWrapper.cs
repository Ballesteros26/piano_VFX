using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_CY.</summary>
	// Token: 0x020008DB RID: 2267
	[ComVisible(true)]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the Decimal to be wrapped and marshaled as type VT_CY.</summary>
		/// <param name="obj">The Decimal to be wrapped and marshaled as VT_CY. </param>
		// Token: 0x06005559 RID: 21849 RVA: 0x00128C75 File Offset: 0x00126E75
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the object containing the Decimal to be wrapped and marshaled as type VT_CY.</summary>
		/// <param name="obj">The object containing the Decimal to be wrapped and marshaled as VT_CY. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.Decimal" /> type.</exception>
		// Token: 0x0600555A RID: 21850 RVA: 0x00128C84 File Offset: 0x00126E84
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Decimal."), "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		/// <summary>Gets the wrapped object to be marshaled as type VT_CY.</summary>
		/// <returns>The wrapped object to be marshaled as type VT_CY.</returns>
		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x0600555B RID: 21851 RVA: 0x00128CB5 File Offset: 0x00126EB5
		public decimal WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002CCC RID: 11468
		private decimal m_WrappedObject;
	}
}
