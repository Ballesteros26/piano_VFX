using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Reserves a location on the Web page to display static text.</summary>
	// Token: 0x020003C4 RID: 964
	[DefaultProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Web.UI.Design.WebControls.LiteralDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlBuilder(typeof(LiteralControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Literal : Control, ITextControl
	{
		/// <summary>Gets or sets an enumeration value that specifies how the content in the <see cref="T:System.Web.UI.WebControls.Literal" /> control is rendered.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. The default is Transform.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. </exception>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00068338 File Offset: 0x00066538
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x00068363 File Offset: 0x00066563
		[WebSysDescription("")]
		[DefaultValue(LiteralMode.Transform)]
		[WebCategory("Behavior")]
		public LiteralMode Mode
		{
			get
			{
				if (this.ViewState["Mode"] != null)
				{
					return (LiteralMode)this.ViewState["Mode"];
				}
				return LiteralMode.Transform;
			}
			set
			{
				if (value < LiteralMode.Transform || value > LiteralMode.Encode)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["Mode"] = value;
			}
		}

		/// <summary>Gets or sets the caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <returns>The caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x0004A02A File Offset: 0x0004822A
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Set input focus to a control; the <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> base control method is not supported on a <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> was called on a <see cref="T:System.Web.UI.WebControls.Literal" />.</exception>
		// Token: 0x0600281F RID: 10271 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x06002820 RID: 10272 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.Literal" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> of the control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
		// Token: 0x06002821 RID: 10273 RVA: 0x0006838C File Offset: 0x0006658C
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.Text = literalControl.Text;
				return;
			}
			throw new HttpException(global::Locale.GetText("'Literal' cannot have children of type '{0}'", new object[] { obj.GetType() }));
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
		// Token: 0x06002822 RID: 10274 RVA: 0x000683CE File Offset: 0x000665CE
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Mode == LiteralMode.Encode)
			{
				writer.Write(HttpUtility.HtmlEncode(this.Text));
				return;
			}
			writer.Write(this.Text);
		}
	}
}
