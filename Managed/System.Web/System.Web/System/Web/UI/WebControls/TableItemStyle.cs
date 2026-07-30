using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style properties for an element of a control that renders as a <see cref="T:System.Web.UI.WebControls.TableRow" /> or <see cref="T:System.Web.UI.WebControls.TableCell" />.</summary>
	// Token: 0x0200041E RID: 1054
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableItemStyle : Style
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class using default values.</summary>
		// Token: 0x06002F99 RID: 12185 RVA: 0x0006ED8C File Offset: 0x0006CF8C
		public TableItemStyle()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class with the specified state bag.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
		// Token: 0x06002F9A RID: 12186 RVA: 0x0006ED94 File Offset: 0x0006CF94
		public TableItemStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the horizontal alignment of the contents in a cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. </exception>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x0007D8D5 File Offset: 0x0007BAD5
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x0007D8FB File Offset: 0x0007BAFB
		[DefaultValue(HorizontalAlign.NotSet)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.CheckBit(65536))
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)base.ViewState["HorizontalAlign"];
			}
			set
			{
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid HorizontalAlign value."));
				}
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the vertical alignment of the contents in a cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified vertical alignment was not one of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. </exception>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x0007D936 File Offset: 0x0007BB36
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x0007D95C File Offset: 0x0007BB5C
		[DefaultValue(VerticalAlign.NotSet)]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.CheckBit(131072))
				{
					return VerticalAlign.NotSet;
				}
				return (VerticalAlign)base.ViewState["VerticalAlign"];
			}
			set
			{
				if (value < VerticalAlign.NotSet || value > VerticalAlign.Bottom)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid VerticalAlign value."));
				}
				base.ViewState["VerticalAlign"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents of a cell wrap in the cell.</summary>
		/// <returns>true if the contents of the cell wrap in the cell; otherwise, false. The default is true.</returns>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x0007D997 File Offset: 0x0007BB97
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x0007D9BD File Offset: 0x0007BBBD
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				return !base.CheckBit(262144) || (bool)base.ViewState["Wrap"];
			}
			set
			{
				base.ViewState["Wrap"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Adds information about horizontal alignment, vertical alignment, and wrap to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		/// <param name="owner">The control that the style refers to. </param>
		// Token: 0x06002FA1 RID: 12193 RVA: 0x0007D9E0 File Offset: 0x0007BBE0
		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			if (writer == null)
			{
				return;
			}
			switch (this.HorizontalAlign)
			{
			case HorizontalAlign.Left:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", false);
				break;
			case HorizontalAlign.Center:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center", false);
				break;
			case HorizontalAlign.Right:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", false);
				break;
			case HorizontalAlign.Justify:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "justify", false);
				break;
			}
			switch (this.VerticalAlign)
			{
			case VerticalAlign.Top:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top", false);
				break;
			case VerticalAlign.Middle:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle", false);
				break;
			case VerticalAlign.Bottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom", false);
				break;
			}
			if (!this.Wrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x0007DAB4 File Offset: 0x0007BCB4
		private void Copy(string name, TableItemStyle.TableItemStyles s, Style source)
		{
			if (source.CheckBit((int)s))
			{
				object obj = source.ViewState[name];
				if (obj != null)
				{
					base.ViewState[name] = obj;
					this.SetBit((int)s);
				}
			}
		}

		/// <summary>Duplicates the non-empty style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to copy. </param>
		// Token: 0x06002FA3 RID: 12195 RVA: 0x0007DAF0 File Offset: 0x0007BCF0
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
			if (s != null && !s.IsEmpty)
			{
				this.Copy("HorizontalAlign", TableItemStyle.TableItemStyles.HorizontalAlign, s);
				this.Copy("VerticalAlign", TableItemStyle.TableItemStyles.VerticalAlign, s);
				this.Copy("Wrap", TableItemStyle.TableItemStyles.Wrap, s);
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0007DB44 File Offset: 0x0007BD44
		private void Merge(string name, TableItemStyle.TableItemStyles s, Style source)
		{
			if (!base.CheckBit((int)s) && source.CheckBit((int)s))
			{
				object obj = source.ViewState[name];
				if (obj != null)
				{
					base.ViewState[name] = obj;
					this.SetBit((int)s);
				}
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to combine. </param>
		// Token: 0x06002FA5 RID: 12197 RVA: 0x0007DB88 File Offset: 0x0007BD88
		public override void MergeWith(Style s)
		{
			if (this.IsEmpty)
			{
				this.CopyFrom(s);
				return;
			}
			base.MergeWith(s);
			if (s != null)
			{
				this.Merge("HorizontalAlign", TableItemStyle.TableItemStyles.HorizontalAlign, s);
				this.Merge("VerticalAlign", TableItemStyle.TableItemStyles.VerticalAlign, s);
				this.Merge("Wrap", TableItemStyle.TableItemStyles.Wrap, s);
			}
		}

		/// <summary>Removes any defined style elements from the style.</summary>
		// Token: 0x06002FA6 RID: 12198 RVA: 0x0007DBE4 File Offset: 0x0007BDE4
		public override void Reset()
		{
			if (base.CheckBit(65536))
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			if (base.CheckBit(131072))
			{
				base.ViewState.Remove("VerticalAlign");
			}
			if (base.CheckBit(262144))
			{
				base.ViewState.Remove("Wrap");
			}
			base.Reset();
		}

		// Token: 0x0200041F RID: 1055
		[Flags]
		private enum TableItemStyles
		{
			// Token: 0x04001BEC RID: 7148
			HorizontalAlign = 65536,
			// Token: 0x04001BED RID: 7149
			VerticalAlign = 131072,
			// Token: 0x04001BEE RID: 7150
			Wrap = 262144
		}
	}
}
