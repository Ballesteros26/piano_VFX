using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a data-binding handler for a calendar.</summary>
	// Token: 0x02000051 RID: 81
	public class CalendarDataBindingHandler : DataBindingHandler
	{
		/// <summary>Sets the calendar's date to the current day if the <see cref="P:System.Web.UI.WebControls.Calendar.SelectedDate" /> property is data-bound.</summary>
		/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to which data binding will be added. </param>
		// Token: 0x060002A1 RID: 673 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			throw new NotImplementedException();
		}
	}
}
