using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Calendar" /> Web server control.</summary>
	// Token: 0x020000CA RID: 202
	public class CalendarDesigner : ControlDesigner
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000234B File Offset: 0x0000054B
		public override DesignerVerbCollection Verbs
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> object for this designer. </param>
		// Token: 0x060005EC RID: 1516 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when an auto-format scheme has been applied to the control.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060005ED RID: 1517 RVA: 0x0000234B File Offset: 0x0000054B
		protected void OnAutoFormat(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
