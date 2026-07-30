using System;
using System.ComponentModel;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for controls that implement the methods of the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> abstract class.</summary>
	// Token: 0x020000CC RID: 204
	public class CompositeControlDesigner : ControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.CompositeControlDesigner" /> class.</summary>
		// Token: 0x060005F0 RID: 1520 RVA: 0x000092B3 File Offset: 0x000074B3
		public CompositeControlDesigner()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates the child controls of this <see cref="T:System.Web.UI.WebControls.CompositeControl" /> control.</summary>
		// Token: 0x060005F1 RID: 1521 RVA: 0x0000234B File Offset: 0x0000054B
		protected virtual void CreateChildControls()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
		/// <returns>The HTML that is used to represent the control at design time.</returns>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the designer with the specified <see cref="T:System.ComponentModel.IComponent" /> object.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" />, which is the control associated with this designer.</param>
		// Token: 0x060005F4 RID: 1524 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
