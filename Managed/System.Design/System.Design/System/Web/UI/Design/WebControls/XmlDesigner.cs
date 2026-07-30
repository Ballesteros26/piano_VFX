using System;
using System.ComponentModel;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Xml" /> Web server control.</summary>
	// Token: 0x020000DE RID: 222
	public class XmlDesigner : ControlDesigner
	{
		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebControls.XmlDesigner" /> control and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000673 RID: 1651 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup that is used to represent the control at design time.</summary>
		// Token: 0x06000674 RID: 1652 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the HTML that is used to fill an empty control.</summary>
		/// <returns>The HTML used to fill an empty control.</returns>
		// Token: 0x06000675 RID: 1653 RVA: 0x0000234B File Offset: 0x0000054B
		protected override string GetEmptyDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the designer with the control that this instance of the designer is associated with.</summary>
		/// <param name="component">The associated control. </param>
		// Token: 0x06000676 RID: 1654 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
