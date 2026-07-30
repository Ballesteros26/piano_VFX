using System;

namespace System.Web.UI
{
	/// <summary>Allows the control serializer to get to the builder for a control.</summary>
	// Token: 0x0200016B RID: 363
	public interface IControlBuilderAccessor
	{
		/// <summary>Gets the control builder for this control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, null if no builder was used.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000F57 RID: 3927
		ControlBuilder ControlBuilder { get; }
	}
}
