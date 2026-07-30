using System;
using System.ComponentModel;
using System.Data;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of an HTTP request query-string field to a parameter object. </summary>
	// Token: 0x020003F7 RID: 1015
	[DefaultProperty("QueryStringField")]
	public class QueryStringParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class.</summary>
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public QueryStringParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the values of the instance that is specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> instance from which the current instance is initialized. </param>
		// Token: 0x06002CC9 RID: 11465 RVA: 0x00076EDC File Offset: 0x000750DC
		protected QueryStringParameter(QueryStringParameter original)
			: base(original)
		{
			this.QueryStringField = original.QueryStringField;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified string to identify which query-string field to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string (""). </param>
		// Token: 0x06002CCA RID: 11466 RVA: 0x00076EF1 File Offset: 0x000750F1
		public QueryStringParameter(string name, string queryStringField)
			: base(name)
		{
			this.QueryStringField = queryStringField;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified string to identify which query-string field to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string (""). </param>
		// Token: 0x06002CCB RID: 11467 RVA: 0x00076F01 File Offset: 0x00075101
		public QueryStringParameter(string name, TypeCode type, string queryStringField)
			: base(name, type)
		{
			this.QueryStringField = queryStringField;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified query-string field and the data type of the parameter.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dbType">The data type of the parameter.</param>
		/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string ("").</param>
		// Token: 0x06002CCC RID: 11468 RVA: 0x00076F12 File Offset: 0x00075112
		public QueryStringParameter(string name, DbType dbType, string queryStringField)
			: base(name, dbType)
		{
			this.QueryStringField = queryStringField;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> instance.</summary>
		/// <returns>A duplicate of the current instance.</returns>
		// Token: 0x06002CCD RID: 11469 RVA: 0x00076F23 File Offset: 0x00075123
		protected override Parameter Clone()
		{
			return new QueryStringParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> object.</summary>
		/// <returns>An object that represents the current value of the parameter. If the context or the request is null, the <see cref="M:System.Web.UI.WebControls.QueryStringParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null. </returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> instance of the request.</param>
		/// <param name="control">A Web server control that is associated with the ASP.NET Web page where the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> object is used.Note   This parameter is not used.</param>
		// Token: 0x06002CCE RID: 11470 RVA: 0x00076F2B File Offset: 0x0007512B
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			return context.Request.QueryString[this.QueryStringField];
		}

		/// <summary>Gets or sets the name of the query-string field that the parameter binds to.</summary>
		/// <returns>The name of the query-string field that the parameter binds to.</returns>
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x00076F50 File Offset: 0x00075150
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x00076F7D File Offset: 0x0007517D
		[DefaultValue("")]
		public string QueryStringField
		{
			get
			{
				string text = base.ViewState["QueryStringField"] as string;
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				if (this.QueryStringField != value)
				{
					base.ViewState["QueryStringField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets whether the value of the query string parameter is being validated or not.</summary>
		/// <returns>true if the value of the query parameter is being validated; otherwise, false.</returns>
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00076FA4 File Offset: 0x000751A4
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x0000B3E4 File Offset: 0x000095E4
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
