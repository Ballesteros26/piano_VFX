using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ModelDataSource.CallingDataMethods" /> event.</summary>
	// Token: 0x02000286 RID: 646
	public class CallingDataMethodsEventArgs : EventArgs
	{
		/// <summary>The type that contains the data methods to call, when the data methods are static methods.</summary>
		/// <returns>The type that contains the static data methods to call, or null if the data methods are not static methods.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00045D6F File Offset: 0x00043F6F
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00045D77 File Offset: 0x00043F77
		public Type DataMethodsType { get; set; }

		/// <summary>An object that contains the data methods to call, when the data methods are not static methods on a type.</summary>
		/// <returns>The instance that contains the data methods to call, or null if the data methods are static methods.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00045D80 File Offset: 0x00043F80
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00045D88 File Offset: 0x00043F88
		public object DataMethodsObject { get; set; }
	}
}
