using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of an ASP.NET Profile property to a parameter object. </summary>
	// Token: 0x020003F6 RID: 1014
	[DefaultProperty("PropertyName")]
	public class ProfileParameter : Parameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class.</summary>
		// Token: 0x06002CBF RID: 11455 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public ProfileParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance from which the current instance is initialized.</param>
		// Token: 0x06002CC0 RID: 11456 RVA: 0x00076E1C File Offset: 0x0007501C
		protected ProfileParameter(ProfileParameter original)
			: base(original)
		{
			this.PropertyName = original.PropertyName;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06002CC1 RID: 11457 RVA: 0x00076E31 File Offset: 0x00075031
		public ProfileParameter(string name, string propertyName)
			: base(name)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />.</param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06002CC2 RID: 11458 RVA: 0x00076E41 File Offset: 0x00075041
		public ProfileParameter(string name, TypeCode type, string propertyName)
			: base(name, type)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to. </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dbType">The database type that the parameter represents. </param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to.</param>
		// Token: 0x06002CC3 RID: 11459 RVA: 0x00076E52 File Offset: 0x00075052
		public ProfileParameter(string name, DbType dbType, string propertyName)
			: base(name, dbType)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object that is an exact duplicate of the current one.</returns>
		// Token: 0x06002CC4 RID: 11460 RVA: 0x00076E63 File Offset: 0x00075063
		protected override Parameter Clone()
		{
			return new ProfileParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the ASP.NET Profile is null (Nothing in Visual Basic), the <see cref="M:System.Web.UI.WebControls.ProfileParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Form where the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> is used.</param>
		// Token: 0x06002CC5 RID: 11461 RVA: 0x00076E6B File Offset: 0x0007506B
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Profile == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return null;
			}
			return context.Profile[this.PropertyName];
		}

		/// <summary>Gets or sets the name of the ASP.NET Profile property that the parameter binds to.</summary>
		/// <returns>A string that identifies the ASP.NET Profile property that the parameter binds to.</returns>
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x00076E9C File Offset: 0x0007509C
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x00076EC9 File Offset: 0x000750C9
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				object obj = base.ViewState["PropertyName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PropertyName"] = value;
			}
		}
	}
}
