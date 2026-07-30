using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides an interface to allow a composite-control designer to recreate the child controls of its associated control at design time.</summary>
	// Token: 0x020002D1 RID: 721
	public interface ICompositeControlDesignerAccessor
	{
		/// <summary>In a control derived from <see cref="T:System.Web.UI.WebControls.CompositeControl" />, recreates the child controls at design time. Called by the control's associated designer.</summary>
		// Token: 0x06001B61 RID: 7009
		void RecreateChildControls();
	}
}
