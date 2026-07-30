using System;

namespace System.Web.UI.WebControls
{
	/// <summary>In accessibility scenarios, represents a header cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x02000374 RID: 884
	public class DataControlFieldHeaderCell : DataControlFieldCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldHeaderCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002171 RID: 8561 RVA: 0x00055726 File Offset: 0x00053926
		public DataControlFieldHeaderCell(DataControlField containingField)
			: base(HtmlTextWriterTag.Th, containingField)
		{
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00055731 File Offset: 0x00053931
		internal DataControlFieldHeaderCell(DataControlField containerField, TableHeaderScope scope)
			: this(containerField)
		{
			this.scope = scope;
		}

		/// <summary>Gets or sets the header cell's scope within an HTML table.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableHeaderScope" /> values. The default is <see cref="F:System.Web.UI.WebControls.TableHeaderScope.NotSet" />.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x00055744 File Offset: 0x00053944
		// (set) Token: 0x06002174 RID: 8564 RVA: 0x0005576D File Offset: 0x0005396D
		public virtual TableHeaderScope Scope
		{
			get
			{
				object obj = this.ViewState["Scope"];
				if (obj != null)
				{
					return (TableHeaderScope)obj;
				}
				return TableHeaderScope.NotSet;
			}
			set
			{
				this.ViewState["Scope"] = value;
			}
		}

		/// <summary>Gets or sets abbreviated text, which is rendered in an HTML abbr attribute and is used by screen readers.</summary>
		/// <returns>A shortened version of the table header text, which is read by screen readers. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x00055788 File Offset: 0x00053988
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x000557B5 File Offset: 0x000539B5
		public virtual string AbbreviatedText
		{
			get
			{
				object obj = this.ViewState["AbbreviatedText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AbbreviatedText"] = value;
			}
		}

		/// <summary>Adds information about the table cell to the list of attributes to render.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x06002177 RID: 8567 RVA: 0x000557C8 File Offset: 0x000539C8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			TableHeaderScope tableHeaderScope = this.scope;
			if (tableHeaderScope != TableHeaderScope.Row)
			{
				if (tableHeaderScope == TableHeaderScope.Column)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", false);
				}
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row", false);
			}
			if (this.AbbreviatedText.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Abbr, this.AbbreviatedText);
			}
		}

		// Token: 0x040018CF RID: 6351
		private TableHeaderScope scope;
	}
}
