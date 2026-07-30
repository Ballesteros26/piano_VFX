using System;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Table" /> Web server control.</summary>
	// Token: 0x020000DC RID: 220
	public class TableDesigner : ControlDesigner
	{
		/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
		/// <returns>The HTML used to represent the control at design time.</returns>
		// Token: 0x0600066D RID: 1645 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetPersistInnerHtml()
		{
			throw new NotImplementedException();
		}
	}
}
