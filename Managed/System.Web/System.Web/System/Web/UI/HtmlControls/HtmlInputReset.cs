using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type=reset&gt; element on the server.</summary>
	// Token: 0x02000269 RID: 617
	[SupportsEventValidation]
	[DefaultEvent("")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputReset : HtmlInputButton
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using default values.</summary>
		// Token: 0x06001946 RID: 6470 RVA: 0x00043DAF File Offset: 0x00041FAF
		public HtmlInputReset()
			: base("reset")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using the specified input type.</summary>
		/// <param name="type">The input type.</param>
		// Token: 0x06001947 RID: 6471 RVA: 0x00043DBC File Offset: 0x00041FBC
		public HtmlInputReset(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets a value that indicates whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked. </summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked; otherwise, false. The default value is true. </returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00041BB3 File Offset: 0x0003FDB3
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked on the Web page. </summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600194C RID: 6476 RVA: 0x00043DC5 File Offset: 0x00041FC5
		// (remove) Token: 0x0600194D RID: 6477 RVA: 0x00043DD8 File Offset: 0x00041FD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputReset.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputReset.ServerClickEvent, value);
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00043DEB File Offset: 0x00041FEB
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputReset()
		{
			HtmlInputReset.ServerClickEvent = new object();
		}
	}
}
