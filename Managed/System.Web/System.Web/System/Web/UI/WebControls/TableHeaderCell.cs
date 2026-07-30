using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a heading cell within a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x0200041D RID: 1053
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableHeaderCell : TableCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> class.</summary>
		// Token: 0x06002F91 RID: 12177 RVA: 0x0007D718 File Offset: 0x0007B918
		public TableHeaderCell()
			: base(HtmlTextWriterTag.Th)
		{
		}

		/// <summary>Gets or sets the abbr attribute of the HTML th element for the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
		/// <returns>A string representing the abbreviated text. The default is an empty string ("").</returns>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x0007D724 File Offset: 0x0007B924
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x0007D751 File Offset: 0x0007B951
		[DefaultValue("")]
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
				if (value == null)
				{
					this.ViewState.Remove("AbbreviatedText");
					return;
				}
				this.ViewState["AbbreviatedText"] = value;
			}
		}

		/// <summary>Gets or sets the axis attribute of the HTML th element for the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
		/// <returns>An array of string values representing the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> categories. </returns>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x0007D778 File Offset: 0x0007B978
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x0007D7A6 File Offset: 0x0007B9A6
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		public virtual string[] CategoryText
		{
			get
			{
				object obj = this.ViewState["CategoryText"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				this.ViewState["CategoryText"] = value;
			}
		}

		/// <summary>Gets or sets the scope of the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control when it is rendered.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableHeaderScope" /> value. The default is <see cref="F:System.Web.UI.WebControls.TableHeaderScope.NotSet" />. </returns>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x0007D7BC File Offset: 0x0007B9BC
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x0007D7E5 File Offset: 0x0007B9E5
		[DefaultValue(TableHeaderScope.NotSet)]
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
				this.ViewState["Scope"] = (int)value;
			}
		}

		/// <summary>Applies attributes to render to the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x06002F98 RID: 12184 RVA: 0x0007D800 File Offset: 0x0007BA00
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (writer != null)
			{
				object obj = this.ViewState["AbbreviatedText"];
				if (obj != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Abbr, (string)obj);
				}
				TableHeaderScope scope = this.Scope;
				if (scope != TableHeaderScope.Row)
				{
					if (scope == TableHeaderScope.Column)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Scope, "column", false);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row", false);
				}
				string[] categoryText = this.CategoryText;
				if (categoryText.Length == 1)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Axis, categoryText[0]);
					return;
				}
				if (categoryText.Length > 1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < categoryText.Length - 1; i++)
					{
						stringBuilder.Append(categoryText[i]);
						stringBuilder.Append(",");
					}
					stringBuilder.Append(categoryText[categoryText.Length - 1]);
					writer.AddAttribute(HtmlTextWriterAttribute.Axis, stringBuilder.ToString());
				}
			}
		}
	}
}
