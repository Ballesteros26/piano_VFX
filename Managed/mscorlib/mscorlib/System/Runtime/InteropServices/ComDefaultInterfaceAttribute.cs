using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies a default interface to expose to COM. This class cannot be inherited.</summary>
	// Token: 0x020008A9 RID: 2217
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> class with the specified <see cref="T:System.Type" /> object as the default interface exposed to COM.</summary>
		/// <param name="defaultInterface">A <see cref="T:System.Type" /> value indicating the default interface to expose to COM. </param>
		// Token: 0x060054E0 RID: 21728 RVA: 0x00128381 File Offset: 0x00126581
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</returns>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x00128390 File Offset: 0x00126590
		public Type Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002BFC RID: 11260
		internal Type _val;
	}
}
