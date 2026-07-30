using System;
using System.ComponentModel;
using System.Data;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a client-side HTTP cookie to a parameter object. The parameter can be used in a parameterized query or command to select, filter, or update data.</summary>
	// Token: 0x02000360 RID: 864
	[DefaultProperty("CookieName")]
	public class CookieParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class.</summary>
		// Token: 0x06001FF1 RID: 8177 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public CookieParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> from which the current instance is initialized. </param>
		// Token: 0x06001FF2 RID: 8178 RVA: 0x00050873 File Offset: 0x0004EA73
		protected CookieParameter(CookieParameter original)
			: base(original)
		{
			this.CookieName = original.CookieName;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FF3 RID: 8179 RVA: 0x00050888 File Offset: 0x0004EA88
		public CookieParameter(string name, string cookieName)
			: base(name)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FF4 RID: 8180 RVA: 0x00050898 File Offset: 0x0004EA98
		public CookieParameter(string name, TypeCode type, string cookieName)
			: base(name, type)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class that has the specified name and database type and that is bound to the specified HTTP cookie.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="dbType">The database type that the parameter represents. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000508A9 File Offset: 0x0004EAA9
		public CookieParameter(string name, DbType dbType, string cookieName)
			: base(name, dbType)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.CookieParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x000508BA File Offset: 0x0004EABA
		protected override Parameter Clone()
		{
			return new CookieParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is null, the <see cref="M:System.Web.UI.WebControls.CookieParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Forms page where the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> is used. </param>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x000508C4 File Offset: 0x0004EAC4
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			HttpCookie httpCookie = context.Request.Cookies[this.CookieName];
			if (httpCookie == null)
			{
				return null;
			}
			return httpCookie.Value;
		}

		/// <summary>Gets or sets the name of the HTTP cookie that the parameter binds to.</summary>
		/// <returns>A string that identifies the client-side HTTP cookie that the parameter binds to.</returns>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x00050900 File Offset: 0x0004EB00
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x00050917 File Offset: 0x0004EB17
		[DefaultValue("")]
		public string CookieName
		{
			get
			{
				return base.ViewState.GetString("CookieName", string.Empty);
			}
			set
			{
				if (this.CookieName != value)
				{
					base.ViewState["CookieName"] = value;
					base.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets a value that specifies whether the parameter's value is validated.</summary>
		/// <returns>true if the parameter's value is validated; otherwise, false.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00050940 File Offset: 0x0004EB40
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ValidateInput
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
