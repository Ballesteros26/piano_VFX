using System;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> object with which this control adapter is associated, for specific browser requests.</summary>
	// Token: 0x0200045D RID: 1117
	public class HierarchicalDataBoundControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> class.</summary>
		// Token: 0x060033C3 RID: 13251 RVA: 0x0008A673 File Offset: 0x00088873
		public HierarchicalDataBoundControlAdapter()
		{
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x0008A67B File Offset: 0x0008887B
		internal HierarchicalDataBoundControlAdapter(HierarchicalDataBoundControl c)
			: base(c)
		{
		}

		/// <summary>Binds the data in the data source of the associated hierarchical data-bound control to the adapter.</summary>
		// Token: 0x060033C5 RID: 13253 RVA: 0x0008A6B6 File Offset: 0x000888B6
		protected internal virtual void PerformDataBinding()
		{
			this.Control.PerformDataBinding();
		}

		/// <summary>Retrieves a strongly typed reference to the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> control associated with this <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> associated with the current instance of <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" />.</returns>
		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x0008A6C3 File Offset: 0x000888C3
		protected new HierarchicalDataBoundControl Control
		{
			get
			{
				return (HierarchicalDataBoundControl)this.control;
			}
		}
	}
}
