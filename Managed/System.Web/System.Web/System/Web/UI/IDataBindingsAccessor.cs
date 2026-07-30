using System;

namespace System.Web.UI
{
	/// <summary>Allows access to the collection of data-binding expressions on a control at design time.</summary>
	// Token: 0x0200016E RID: 366
	public interface IDataBindingsAccessor
	{
		/// <summary>Gets a collection of all data bindings on the control. This property is read-only.</summary>
		/// <returns>The collection of data bindings.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000F61 RID: 3937
		DataBindingCollection DataBindings { get; }

		/// <summary>Gets a value indicating whether the control contains any data-binding logic.</summary>
		/// <returns>true if the control contains data binding logic.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000F62 RID: 3938
		bool HasDataBindings { get; }
	}
}
