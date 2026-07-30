using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Holds text, markup, and server controls to render to a <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control in a master page.</summary>
	// Token: 0x02000359 RID: 857
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.WebControls.ContentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlBuilder(typeof(ContentBuilderInternal))]
	public class Content : Control, INamingContainer, INonBindingContainer
	{
		/// <summary>Gets or sets the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control that is associated with the current content.</summary>
		/// <returns>A string containing the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> associated with the current content. The default is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the property at run time.</exception>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x00003A01 File Offset: 0x00001C01
		[IDReferenceProperty(typeof(ContentPlaceHolder))]
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public string ContentPlaceHolderID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Occurs when the control binds to a data source.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06001FC9 RID: 8137 RVA: 0x000504A4 File Offset: 0x0004E6A4
		// (remove) Token: 0x06001FCA RID: 8138 RVA: 0x000504AD File Offset: 0x0004E6AD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler DataBinding
		{
			add
			{
				base.DataBinding += value;
			}
			remove
			{
				base.DataBinding -= value;
			}
		}

		/// <summary>Occurs when the control is released from memory.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06001FCB RID: 8139 RVA: 0x000504B6 File Offset: 0x0004E6B6
		// (remove) Token: 0x06001FCC RID: 8140 RVA: 0x000504BF File Offset: 0x0004E6BF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Disposed
		{
			add
			{
				base.Disposed += value;
			}
			remove
			{
				base.Disposed -= value;
			}
		}

		/// <summary>Occurs when the control is initialized.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06001FCD RID: 8141 RVA: 0x000504C8 File Offset: 0x0004E6C8
		// (remove) Token: 0x06001FCE RID: 8142 RVA: 0x000504D1 File Offset: 0x0004E6D1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Init
		{
			add
			{
				base.Init += value;
			}
			remove
			{
				base.Init -= value;
			}
		}

		/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> control. </summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001FCF RID: 8143 RVA: 0x000504DA File Offset: 0x0004E6DA
		// (remove) Token: 0x06001FD0 RID: 8144 RVA: 0x000504E3 File Offset: 0x0004E6E3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Load
		{
			add
			{
				base.Load += value;
			}
			remove
			{
				base.Load -= value;
			}
		}

		/// <summary>Occurs when the server control is about to render to its containing <see cref="T:System.Web.UI.Page" /> control.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06001FD1 RID: 8145 RVA: 0x000504EC File Offset: 0x0004E6EC
		// (remove) Token: 0x06001FD2 RID: 8146 RVA: 0x000504F5 File Offset: 0x0004E6F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler PreRender
		{
			add
			{
				base.PreRender += value;
			}
			remove
			{
				base.PreRender -= value;
			}
		}

		/// <summary>Occurs when the server control is unloaded from memory.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001FD3 RID: 8147 RVA: 0x000504FE File Offset: 0x0004E6FE
		// (remove) Token: 0x06001FD4 RID: 8148 RVA: 0x00050507 File Offset: 0x0004E707
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Unload
		{
			add
			{
				base.Unload += value;
			}
			remove
			{
				base.Unload -= value;
			}
		}
	}
}
