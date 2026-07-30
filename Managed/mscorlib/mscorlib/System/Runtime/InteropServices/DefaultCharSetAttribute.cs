using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the value of the <see cref="T:System.Runtime.InteropServices.CharSet" /> enumeration. This class cannot be inherited.</summary>
	// Token: 0x020008D2 RID: 2258
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.CharSet" /> value.</summary>
		/// <param name="charSet">One of the <see cref="T:System.Runtime.InteropServices.CharSet" /> values.</param>
		// Token: 0x0600553C RID: 21820 RVA: 0x00128A3B File Offset: 0x00126C3B
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		/// <summary>Gets the default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</summary>
		/// <returns>The default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x00128A4A File Offset: 0x00126C4A
		public CharSet CharSet
		{
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002CB7 RID: 11447
		internal CharSet _CharSet;
	}
}
