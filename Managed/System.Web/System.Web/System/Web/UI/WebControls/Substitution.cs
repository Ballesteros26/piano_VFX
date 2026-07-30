using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies a section on an output-cached Web page that is exempt from caching. At this location, dynamic content is retrieved and substituted for the <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</summary>
	// Token: 0x02000417 RID: 1047
	[DefaultProperty("MethodName")]
	[ParseChildren(true)]
	[Designer("System.Web.UI.Design.WebControls.SubstitutionDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[PersistChildren(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Substitution : Control
	{
		/// <summary>Gets or sets the name of the callback method to invoke when the <see cref="T:System.Web.UI.WebControls.Substitution" /> control executes.</summary>
		/// <returns>A string that represents the name of the method to invoke when the <see cref="T:System.Web.UI.WebControls.Substitution" /> control executes.</returns>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x0007CDE4 File Offset: 0x0007AFE4
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x0007CE16 File Offset: 0x0007B016
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public virtual string MethodName
		{
			get
			{
				string text = this.ViewState["MethodName"] as string;
				if (string.IsNullOrEmpty(text))
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["MethodName"] = value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Web.UI.EmptyControlCollection" /> object, indicating that the <see cref="T:System.Web.UI.WebControls.Substitution" /> control does not support child controls.</summary>
		/// <returns>An <see cref="T:System.Web.UI.EmptyControlCollection" />, indicating that the control does not support child controls.</returns>
		// Token: 0x06002F3F RID: 12095 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The parent control or master page is cached.</exception>
		// Token: 0x06002F40 RID: 12096 RVA: 0x000419F4 File Offset: 0x0003FBF4
		[global::System.MonoTODO("Why override?")]
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
		// Token: 0x06002F41 RID: 12097 RVA: 0x0007CE2C File Offset: 0x0007B02C
		protected internal override void Render(HtmlTextWriter writer)
		{
			string methodName = this.MethodName;
			if (methodName.Length == 0)
			{
				return;
			}
			TemplateControl templateControl = base.TemplateControl;
			if (templateControl == null)
			{
				return;
			}
			HttpContext context = this.Context;
			HttpResponse httpResponse = ((context != null) ? context.Response : null);
			if (httpResponse == null)
			{
				return;
			}
			httpResponse.WriteSubstitution(this.CreateCallback(methodName, templateControl));
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0007CE7C File Offset: 0x0007B07C
		private HttpResponseSubstitutionCallback CreateCallback(string method, TemplateControl tc)
		{
			HttpResponseSubstitutionCallback httpResponseSubstitutionCallback;
			try
			{
				httpResponseSubstitutionCallback = Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), tc.GetType(), method, true, true) as HttpResponseSubstitutionCallback;
			}
			catch (Exception ex)
			{
				throw new HttpException("Cannot find static method '" + method + "' matching HttpResponseSubstitutionCallback", ex);
			}
			return httpResponseSubstitutionCallback;
		}
	}
}
