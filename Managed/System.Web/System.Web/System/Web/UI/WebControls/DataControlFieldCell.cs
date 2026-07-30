using System;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x02000372 RID: 882
	public class DataControlFieldCell : TableCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002156 RID: 8534 RVA: 0x00055473 File Offset: 0x00053673
		public DataControlFieldCell(DataControlField containingField)
			: this(HtmlTextWriterTag.Td, containingField)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="tagKey">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the HTML tag to render for the cell. The default tag used by the <see cref="T:System.Web.UI.WebControls.TableCell" /> class is <see cref="F:System.Web.UI.HtmlTextWriterTag.Td" />.</param>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002157 RID: 8535 RVA: 0x0005547E File Offset: 0x0005367E
		protected DataControlFieldCell(HtmlTextWriterTag tagKey, DataControlField containingField)
			: base(tagKey)
		{
			this.containerField = containingField;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object that contains the current cell.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell, or null, if no <see cref="T:System.Web.UI.WebControls.DataControlField" /> is passed to the class constructor.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0005548E File Offset: 0x0005368E
		public DataControlField ContainingField
		{
			get
			{
				return this.containerField;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x00055498 File Offset: 0x00053698
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040018CB RID: 6347
		private DataControlField containerField;
	}
}
