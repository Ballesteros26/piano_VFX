using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item in a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	// Token: 0x02000383 RID: 899
	[ToolboxItem("")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataListItem : WebControl, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060022B3 RID: 8883 RVA: 0x000599E7 File Offset: 0x00057BE7
		public DataListItem(int itemIndex, ListItemType itemType)
		{
			this.index = itemIndex;
			this.type = itemType;
		}

		/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000599FD File Offset: 0x00057BFD
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x00059A05 File Offset: 0x00057C05
		public virtual object DataItem
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection of the control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x00059A0E File Offset: 0x00057C0E
		public virtual int ItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00059A16 File Offset: 0x00057C16
		public virtual ListItemType ItemType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control, which contains the style properties for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the control.</returns>
		// Token: 0x060022B9 RID: 8889 RVA: 0x00059A1E File Offset: 0x00057C1E
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Determines whether the event for the control is passed up the server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060022BA RID: 8890 RVA: 0x00059A2C File Offset: 0x00057C2C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				base.RaiseBubbleEvent(this, new DataListCommandEventArgs(this, source, commandEventArgs));
				return true;
			}
			return false;
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that contains the output stream for rendering on the client. </param>
		/// <param name="extractRows">true to extract rows; otherwise false. </param>
		/// <param name="tableLayout">true to display as a table; otherwise false. </param>
		// Token: 0x060022BB RID: 8891 RVA: 0x00059A58 File Offset: 0x00057C58
		public virtual void RenderItem(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			bool flag = !extractRows && !tableLayout;
			if (flag)
			{
				writer.RenderBeginTag(this.TagKey);
			}
			if (this.HasControls())
			{
				if (extractRows)
				{
					bool flag2 = false;
					foreach (object obj in this.Controls)
					{
						Table table = ((Control)obj) as Table;
						if (table != null)
						{
							flag2 = true;
							using (IEnumerator enumerator2 = table.Rows.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									TableRow tableRow = (TableRow)obj2;
									if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
									{
										tableRow.ControlStyle.MergeWith(base.ControlStyle);
									}
									tableRow.RenderControl(writer);
								}
								break;
							}
						}
					}
					if (!flag2)
					{
						throw new HttpException("No Table found in DataList template.");
					}
				}
				else
				{
					this.RenderContents(writer);
				}
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		/// <summary>Sets the current <see cref="P:System.Web.UI.WebControls.DataListItem.ItemType" /> property for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control.</summary>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" />  value.</param>
		// Token: 0x060022BC RID: 8892 RVA: 0x00059B80 File Offset: 0x00057D80
		protected virtual void SetItemType(ListItemType itemType)
		{
			this.type = itemType;
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value to use when data-binding operations are performed.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000599FD File Offset: 0x00057BFD
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>An integer representing the index of the data item bound to a control.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x00059A0E File Offset: 0x00057C0E
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>An integer representing the position of the data item as displayed in a control.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00059A0E File Offset: 0x00057C0E
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001933 RID: 6451
		private int index;

		// Token: 0x04001934 RID: 6452
		private ListItemType type;

		// Token: 0x04001935 RID: 6453
		private object item;
	}
}
