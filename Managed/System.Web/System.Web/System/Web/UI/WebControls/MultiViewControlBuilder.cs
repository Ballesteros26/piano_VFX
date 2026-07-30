using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	// Token: 0x020003DC RID: 988
	public class MultiViewControlBuilder : ControlBuilder
	{
		/// <summary>Adds builders to the <see cref="T:System.Web.UI.ControlBuilder" /> object for any child controls that belong to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="subBuilder">The ControlBuilder object assigned to the child control. </param>
		// Token: 0x06002A96 RID: 10902 RVA: 0x000710E1 File Offset: 0x0006F2E1
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			base.AppendSubBuilder(subBuilder);
		}
	}
}
