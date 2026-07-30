using System;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> class. </summary>
	// Token: 0x020000C7 RID: 199
	public class BaseValidatorDesigner : ControlDesigner
	{
		/// <summary>Gets the markup that is used to render the associated control at design time. </summary>
		/// <returns>A string containing the markup used to render the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> at design time.</returns>
		// Token: 0x060005E0 RID: 1504 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}
	}
}
