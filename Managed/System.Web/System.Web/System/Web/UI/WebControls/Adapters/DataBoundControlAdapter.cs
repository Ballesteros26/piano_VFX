using System;
using System.Collections;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object with which the adapter is associated for specific browser requests.</summary>
	// Token: 0x0200045B RID: 1115
	public class DataBoundControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> class.</summary>
		// Token: 0x060033BC RID: 13244 RVA: 0x0008A673 File Offset: 0x00088873
		public DataBoundControlAdapter()
		{
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0008A67B File Offset: 0x0008887B
		internal DataBoundControlAdapter(DataBoundControl c)
			: base(c)
		{
		}

		/// <summary>Binds the data in the data source of the associated <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to the control adapter.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> of <see cref="T:System.Object" /> to be bound to the derived <see cref="T:System.Web.UI.WebControls.DataBoundControl" />.</param>
		// Token: 0x060033BE RID: 13246 RVA: 0x0008A684 File Offset: 0x00088884
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
			this.Control.PerformDataBinding(data);
		}

		/// <summary>Retrieves a strongly-typed reference to the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object associated with this control adapter.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> is attached.</returns>
		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x0008A692 File Offset: 0x00088892
		protected new DataBoundControl Control
		{
			get
			{
				return (DataBoundControl)this.control;
			}
		}
	}
}
