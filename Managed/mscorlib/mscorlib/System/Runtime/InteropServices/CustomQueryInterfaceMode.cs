using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> method's IUnknown::QueryInterface calls can use the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface.</summary>
	// Token: 0x02000910 RID: 2320
	public enum CustomQueryInterfaceMode
	{
		/// <summary>IUnknown::QueryInterface method calls can use the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface. When you use this value, the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> method overload functions like the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type)" /> overload.</summary>
		// Token: 0x04002D8D RID: 11661
		Allow = 1,
		/// <summary>IUnknown::QueryInterface method calls should ignore the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface.</summary>
		// Token: 0x04002D8E RID: 11662
		Ignore = 0
	}
}
