using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the information used to render a list control that repeats a list of items. This class cannot be inherited.</summary>
	// Token: 0x020003FD RID: 1021
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RepeatInfo
	{
		/// <summary>Renders a list control that repeats a list of items, using the specified information.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to render HTML content on the client.</param>
		/// <param name="user">An <see cref="T:System.Web.UI.WebControls.IRepeatInfoUser" /> implemented object that represents the control to render.</param>
		/// <param name="controlStyle">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style in which to display the items.</param>
		/// <param name="baseControl">The control from which to copy base attributes.</param>
		// Token: 0x06002D1A RID: 11546 RVA: 0x00077938 File Offset: 0x00075B38
		public void RenderRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			RepeatLayout repeatLayout = this.RepeatLayout;
			bool flag = repeatLayout == RepeatLayout.OrderedList || repeatLayout == RepeatLayout.UnorderedList;
			if (flag)
			{
				if (user != null && (user.HasHeader || user.HasFooter || user.HasSeparators))
				{
					throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support headers, footers or separators.");
				}
				if (this.OuterTableImplied)
				{
					throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support implied outer tables.");
				}
				if (this.RepeatColumns > 1)
				{
					throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support multi-column layouts.");
				}
			}
			if (this.RepeatDirection == RepeatDirection.Vertical)
			{
				if (flag)
				{
					this.RenderList(writer, user, controlStyle, baseControl);
					return;
				}
				this.RenderVert(writer, user, controlStyle, baseControl);
				return;
			}
			else
			{
				if (flag)
				{
					throw new InvalidOperationException("The UnorderedList and OrderedList layouts only support vertical layout.");
				}
				this.RenderHoriz(writer, user, controlStyle, baseControl);
				return;
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000779E5 File Offset: 0x00075BE5
		private void RenderBr(HtmlTextWriter w)
		{
			w.Write("<br />");
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000779F4 File Offset: 0x00075BF4
		private void RenderList(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			int repeatedItemCount = user.RepeatedItemCount;
			this.RenderBeginTag(w, controlStyle, baseControl);
			for (int i = 0; i < repeatedItemCount; i++)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Li);
				user.RenderItem(ListItemType.Item, i, this, w);
				w.RenderEndTag();
				w.WriteLine();
			}
			w.RenderEndTag();
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x00077A44 File Offset: 0x00075C44
		private void RenderVert(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			int repeatedItemCount = user.RepeatedItemCount;
			int num = ((this.RepeatColumns == 0) ? 1 : this.RepeatColumns);
			int num2 = (repeatedItemCount + num - 1) / num;
			bool hasSeparators = user.HasSeparators;
			bool outerTableImplied = this.OuterTableImplied;
			int num3 = num * ((hasSeparators && num != 1) ? 2 : 1);
			bool flag = this.RepeatLayout == RepeatLayout.Table && !outerTableImplied;
			bool flag2 = true;
			bool flag3 = true;
			if (!outerTableImplied)
			{
				this.RenderBeginTag(w, controlStyle, baseControl);
			}
			if (this.Caption.Length > 0)
			{
				if (this.CaptionAlign != TableCaptionAlign.NotSet)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Align, this.CaptionAlign.ToString());
				}
				w.RenderBeginTag(HtmlTextWriterTag.Caption);
				w.Write(this.Caption);
				w.RenderEndTag();
			}
			if (user.HasHeader)
			{
				if (outerTableImplied)
				{
					user.RenderItem(ListItemType.Header, -1, this, w);
				}
				else if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num3 != 1)
					{
						w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), false);
					}
					if (this.UseAccessibleHeader)
					{
						w.AddAttribute("scope", "col", false);
					}
					Style itemStyle = user.GetItemStyle(ListItemType.Header, -1);
					if (itemStyle != null)
					{
						itemStyle.AddAttributesToRender(w);
					}
					if (this.UseAccessibleHeader)
					{
						w.RenderBeginTag(HtmlTextWriterTag.Th);
					}
					else
					{
						w.RenderBeginTag(HtmlTextWriterTag.Td);
					}
					user.RenderItem(ListItemType.Header, -1, this, w);
					w.RenderEndTag();
					w.RenderEndTag();
				}
				else
				{
					user.RenderItem(ListItemType.Header, -1, this, w);
					this.RenderBr(w);
				}
			}
			for (int i = 0; i < num2; i++)
			{
				if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				for (int j = 0; j < num; j++)
				{
					int num4 = this.index_vert(num2, num, i, j, repeatedItemCount);
					if (flag2 || num4 < repeatedItemCount)
					{
						if (flag)
						{
							Style style = null;
							if (num4 < repeatedItemCount)
							{
								style = user.GetItemStyle(ListItemType.Item, num4);
							}
							if (style != null)
							{
								style.AddAttributesToRender(w);
							}
							w.RenderBeginTag(HtmlTextWriterTag.Td);
						}
						if (num4 < repeatedItemCount)
						{
							user.RenderItem(ListItemType.Item, num4, this, w);
						}
						if (flag)
						{
							w.RenderEndTag();
						}
						if (hasSeparators && num != 1)
						{
							if (flag)
							{
								if (num4 < repeatedItemCount - 1)
								{
									Style itemStyle2 = user.GetItemStyle(ListItemType.Separator, num4);
									if (itemStyle2 != null)
									{
										itemStyle2.AddAttributesToRender(w);
									}
								}
								if (num4 < repeatedItemCount - 1 || flag3)
								{
									w.RenderBeginTag(HtmlTextWriterTag.Td);
								}
							}
							if (num4 < repeatedItemCount - 1)
							{
								user.RenderItem(ListItemType.Separator, num4, this, w);
							}
							if (flag && (num4 < repeatedItemCount - 1 || flag3))
							{
								w.RenderEndTag();
							}
						}
					}
				}
				if (!outerTableImplied)
				{
					if (flag)
					{
						w.RenderEndTag();
					}
					else if (i != num2 - 1)
					{
						this.RenderBr(w);
					}
				}
				if (hasSeparators && i != num2 - 1 && num == 1)
				{
					if (flag)
					{
						w.RenderBeginTag(HtmlTextWriterTag.Tr);
						Style itemStyle3 = user.GetItemStyle(ListItemType.Separator, i);
						if (itemStyle3 != null)
						{
							itemStyle3.AddAttributesToRender(w);
						}
						w.RenderBeginTag(HtmlTextWriterTag.Td);
					}
					user.RenderItem(ListItemType.Separator, i, this, w);
					if (flag)
					{
						w.RenderEndTag();
						w.RenderEndTag();
					}
					else if (!outerTableImplied)
					{
						this.RenderBr(w);
					}
				}
			}
			if (user.HasFooter)
			{
				if (outerTableImplied)
				{
					user.RenderItem(ListItemType.Footer, -1, this, w);
				}
				else if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num3 != 1)
					{
						w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), false);
					}
					Style itemStyle4 = user.GetItemStyle(ListItemType.Footer, -1);
					if (itemStyle4 != null)
					{
						itemStyle4.AddAttributesToRender(w);
					}
					w.RenderBeginTag(HtmlTextWriterTag.Td);
					user.RenderItem(ListItemType.Footer, -1, this, w);
					w.RenderEndTag();
					w.RenderEndTag();
				}
				else
				{
					if (repeatedItemCount != 0)
					{
						this.RenderBr(w);
					}
					user.RenderItem(ListItemType.Footer, -1, this, w);
				}
			}
			if (!outerTableImplied)
			{
				w.RenderEndTag();
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x00077DBC File Offset: 0x00075FBC
		private void RenderHoriz(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			int repeatedItemCount = user.RepeatedItemCount;
			int num = ((this.RepeatColumns == 0) ? repeatedItemCount : this.RepeatColumns);
			int num2 = ((num == 0) ? 0 : ((repeatedItemCount + num - 1) / num));
			bool hasSeparators = user.HasSeparators;
			int num3 = num * (hasSeparators ? 2 : 1);
			bool flag = this.RepeatLayout == RepeatLayout.Table;
			bool flag2 = true;
			bool flag3 = true;
			this.RenderBeginTag(w, controlStyle, baseControl);
			if (this.Caption.Length > 0)
			{
				if (this.CaptionAlign != TableCaptionAlign.NotSet)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Align, this.CaptionAlign.ToString());
				}
				w.RenderBeginTag(HtmlTextWriterTag.Caption);
				w.Write(this.Caption);
				w.RenderEndTag();
			}
			if (user.HasHeader)
			{
				if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num3 != 1)
					{
						w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), false);
					}
					if (this.UseAccessibleHeader)
					{
						w.AddAttribute("scope", "col", false);
					}
					Style itemStyle = user.GetItemStyle(ListItemType.Header, -1);
					if (itemStyle != null)
					{
						itemStyle.AddAttributesToRender(w);
					}
					if (this.UseAccessibleHeader)
					{
						w.RenderBeginTag(HtmlTextWriterTag.Th);
					}
					else
					{
						w.RenderBeginTag(HtmlTextWriterTag.Td);
					}
					user.RenderItem(ListItemType.Header, -1, this, w);
					w.RenderEndTag();
					w.RenderEndTag();
				}
				else
				{
					user.RenderItem(ListItemType.Header, -1, this, w);
					if (!flag && this.RepeatColumns != 0 && repeatedItemCount != 0)
					{
						this.RenderBr(w);
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				for (int j = 0; j < num; j++)
				{
					int num4 = i * num + j;
					if (flag2 || num4 < repeatedItemCount)
					{
						if (flag)
						{
							Style style = null;
							if (num4 < repeatedItemCount)
							{
								style = user.GetItemStyle(ListItemType.Item, num4);
							}
							if (style != null)
							{
								style.AddAttributesToRender(w);
							}
							w.RenderBeginTag(HtmlTextWriterTag.Td);
						}
						if (num4 < repeatedItemCount)
						{
							user.RenderItem(ListItemType.Item, num4, this, w);
						}
						if (flag)
						{
							w.RenderEndTag();
						}
						if (hasSeparators)
						{
							if (flag)
							{
								if (num4 < repeatedItemCount - 1)
								{
									Style itemStyle2 = user.GetItemStyle(ListItemType.Separator, num4);
									if (itemStyle2 != null)
									{
										itemStyle2.AddAttributesToRender(w);
									}
								}
								if (num4 < repeatedItemCount - 1 || flag3)
								{
									w.RenderBeginTag(HtmlTextWriterTag.Td);
								}
							}
							if (num4 < repeatedItemCount - 1)
							{
								user.RenderItem(ListItemType.Separator, num4, this, w);
							}
							if (flag && (num4 < repeatedItemCount - 1 || flag3))
							{
								w.RenderEndTag();
							}
						}
					}
				}
				if (flag)
				{
					w.RenderEndTag();
				}
				else if (i != num2 - 1 || this.RepeatColumns != 0)
				{
					this.RenderBr(w);
				}
			}
			if (user.HasFooter)
			{
				if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num3 != 1)
					{
						w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), false);
					}
					Style itemStyle3 = user.GetItemStyle(ListItemType.Footer, -1);
					if (itemStyle3 != null)
					{
						itemStyle3.AddAttributesToRender(w);
					}
					w.RenderBeginTag(HtmlTextWriterTag.Td);
					user.RenderItem(ListItemType.Footer, -1, this, w);
					w.RenderEndTag();
					w.RenderEndTag();
				}
				else
				{
					user.RenderItem(ListItemType.Footer, -1, this, w);
				}
			}
			w.RenderEndTag();
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0007809C File Offset: 0x0007629C
		private int index_vert(int rows, int cols, int r, int c, int items)
		{
			int num = items % cols;
			if (num == 0)
			{
				num = cols;
			}
			if (r == rows - 1 && c >= num)
			{
				return items;
			}
			int num2;
			if (c > num)
			{
				num2 = num * rows + (c - num) * (rows - 1) + r;
			}
			else
			{
				num2 = rows * c + r;
			}
			return num2;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000780E0 File Offset: 0x000762E0
		private void RenderBeginTag(HtmlTextWriter w, Style s, WebControl wc)
		{
			WebControl webControl;
			switch (this.RepeatLayout)
			{
			case RepeatLayout.Table:
				webControl = new Table();
				break;
			case RepeatLayout.Flow:
				webControl = new Label();
				break;
			case RepeatLayout.UnorderedList:
				webControl = new WebControl(HtmlTextWriterTag.Ul);
				break;
			case RepeatLayout.OrderedList:
				webControl = new WebControl(HtmlTextWriterTag.Ol);
				break;
			default:
				throw new InvalidOperationException(string.Format("Unsupported RepeatLayout value '{0}'.", this.RepeatLayout));
			}
			webControl.ID = wc.ClientID;
			webControl.CopyBaseAttributes(wc);
			webControl.ApplyStyle(s);
			webControl.Enabled = wc.IsEnabled;
			webControl.RenderBeginTag(w);
		}

		/// <summary>Gets or sets a value indicating whether items should be rendered as if they are contained in a table.</summary>
		/// <returns>true if the items should be rendered as if they are contained in a table; otherwise, false.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x00078178 File Offset: 0x00076378
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x00078180 File Offset: 0x00076380
		public bool OuterTableImplied
		{
			get
			{
				return this.outer_table_implied;
			}
			set
			{
				this.outer_table_implied = value;
			}
		}

		/// <summary>Gets or sets the number of columns to render.</summary>
		/// <returns>The number of columns to render.</returns>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x00078189 File Offset: 0x00076389
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x00078191 File Offset: 0x00076391
		public int RepeatColumns
		{
			get
			{
				return this.repeat_cols;
			}
			set
			{
				this.repeat_cols = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the items are displayed vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> enumeration values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified direction is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> enumeration values. </exception>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0007819A File Offset: 0x0007639A
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000781A2 File Offset: 0x000763A2
		public RepeatDirection RepeatDirection
		{
			get
			{
				return this.dir;
			}
			set
			{
				if (value != RepeatDirection.Horizontal && value != RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.dir = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether items are displayed in a table.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> enumeration values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> enumeration values. </exception>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000781B8 File Offset: 0x000763B8
		// (set) Token: 0x06002D28 RID: 11560 RVA: 0x000781C0 File Offset: 0x000763C0
		public RepeatLayout RepeatLayout
		{
			get
			{
				return this.layout;
			}
			set
			{
				if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.layout = value;
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000781DC File Offset: 0x000763DC
		[Conditional("DEBUG_REPEAT_INFO")]
		internal void PrintValues(IRepeatInfoUser riu)
		{
			string text = string.Format("Layout {0}; Direction {1}; Cols {2}; OuterTableImplied {3}\nUser: itms {4}, hdr {5}; ftr {6}; sep {7}", new object[] { this.RepeatLayout, this.RepeatDirection, this.RepeatColumns, this.OuterTableImplied, riu.RepeatedItemCount, riu.HasSeparators, riu.HasHeader, riu.HasFooter, riu.HasSeparators });
			Console.WriteLine(text);
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Trace.Write(text);
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.Table.Caption" /> property if the control is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
		/// <returns>A string that specifies the <see cref="T:System.Web.UI.WebControls.Table" /> caption.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x00078296 File Offset: 0x00076496
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x0007829E File Offset: 0x0007649E
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		/// <summary>Gets or sets the alignment of the caption, if the <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> value for the rendered table. The default value is <see cref="F:System.Web.UI.WebControls.TableCaptionAlign.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values.</exception>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000782A7 File Offset: 0x000764A7
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x000782AF File Offset: 0x000764AF
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		public TableCaptionAlign CaptionAlign
		{
			get
			{
				return this.captionAlign;
			}
			set
			{
				this.captionAlign = value;
			}
		}

		/// <summary>Gets or sets a value to indicate whether to add a <see cref="P:System.Web.UI.WebControls.TableHeaderCell.Scope" /> attribute when the control is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
		/// <returns>true if a "scope" attribute is to be added, otherwise, false.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000782B8 File Offset: 0x000764B8
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000782C0 File Offset: 0x000764C0
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		public bool UseAccessibleHeader
		{
			get
			{
				return this.useAccessibleHeader;
			}
			set
			{
				this.useAccessibleHeader = value;
			}
		}

		// Token: 0x04001B63 RID: 7011
		private bool outer_table_implied;

		// Token: 0x04001B64 RID: 7012
		private int repeat_cols;

		// Token: 0x04001B65 RID: 7013
		private RepeatDirection dir = RepeatDirection.Vertical;

		// Token: 0x04001B66 RID: 7014
		private RepeatLayout layout;

		// Token: 0x04001B67 RID: 7015
		private string caption = string.Empty;

		// Token: 0x04001B68 RID: 7016
		private TableCaptionAlign captionAlign;

		// Token: 0x04001B69 RID: 7017
		private bool useAccessibleHeader;
	}
}
