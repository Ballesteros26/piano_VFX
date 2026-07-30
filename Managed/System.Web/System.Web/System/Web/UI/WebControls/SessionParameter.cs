using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a session variable to a parameter object. </summary>
	// Token: 0x02000407 RID: 1031
	[DefaultProperty("SessionField")]
	public class SessionParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class.</summary>
		// Token: 0x06002DB4 RID: 11700 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public SessionParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.SessionParameter" /> from which the current instance is initialized. </param>
		// Token: 0x06002DB5 RID: 11701 RVA: 0x0007900D File Offset: 0x0007720D
		protected SessionParameter(SessionParameter original)
			: base(original)
		{
			this.SessionField = original.SessionField;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, using the specified string to identify which session state name/value pair to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06002DB6 RID: 11702 RVA: 0x00079022 File Offset: 0x00077222
		public SessionParameter(string name, string sessionField)
			: base(name)
		{
			this.SessionField = sessionField;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, using the specified string to identify which session state name/value pair to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06002DB7 RID: 11703 RVA: 0x00079032 File Offset: 0x00077232
		public SessionParameter(string name, TypeCode type, string sessionField)
			: base(name, type)
		{
			this.SessionField = sessionField;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, by using the specified name and type, and binding the parameter to the specified session state name/value pair. This constructor is for database types.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="dbType">The database type that the parameter represents.</param>
		/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06002DB8 RID: 11704 RVA: 0x00079043 File Offset: 0x00077243
		public SessionParameter(string name, DbType dbType, string sessionField)
			: base(name, dbType)
		{
			this.SessionField = sessionField;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.SessionParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SessionParameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06002DB9 RID: 11705 RVA: 0x00079054 File Offset: 0x00077254
		protected override Parameter Clone()
		{
			return new SessionParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is null, the <see cref="M:System.Web.UI.WebControls.SessionParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Forms page where the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> is used. </param>
		// Token: 0x06002DBA RID: 11706 RVA: 0x0007905C File Offset: 0x0007725C
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Session == null)
			{
				return null;
			}
			return context.Session[this.SessionField];
		}

		/// <summary>Gets or sets the name of the session variable that the parameter binds to.</summary>
		/// <returns>A string that identifies the <see cref="T:System.Web.SessionState.HttpSessionState" /> that the parameter binds to.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x0007907C File Offset: 0x0007727C
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x000790A9 File Offset: 0x000772A9
		[DefaultValue("")]
		[WebCategory("Parameter")]
		public string SessionField
		{
			get
			{
				string text = base.ViewState["SessionField"] as string;
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				if (this.SessionField != value)
				{
					base.ViewState["SessionField"] = value;
					base.OnParameterChanged();
				}
			}
		}
	}
}
