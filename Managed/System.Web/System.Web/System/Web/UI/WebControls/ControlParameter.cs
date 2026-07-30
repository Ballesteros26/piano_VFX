using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a property of a <see cref="T:System.Web.UI.Control" /> to a parameter object. </summary>
	// Token: 0x0200035E RID: 862
	[DefaultProperty("ControlID")]
	public class ControlParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class.</summary>
		// Token: 0x06001FE1 RID: 8161 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public ControlParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class with values from the specified instance.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.ControlParameter" /> instance from which the current instance is initialized. </param>
		// Token: 0x06001FE2 RID: 8162 RVA: 0x000506AC File Offset: 0x0004E8AC
		protected ControlParameter(ControlParameter original)
			: base(original)
		{
			this.ControlID = original.ControlID;
			this.PropertyName = original.PropertyName;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified control name to identify which control to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FE3 RID: 8163 RVA: 0x000506CD File Offset: 0x0004E8CD
		public ControlParameter(string name, string controlID)
			: base(name)
		{
			this.ControlID = controlID;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified property name and control name to identify which control to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		/// <param name="propertyName">The name of the property on the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FE4 RID: 8164 RVA: 0x000506DD File Offset: 0x0004E8DD
		public ControlParameter(string name, string controlID, string propertyName)
			: base(name)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified property name and control name to identify which control to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		/// <param name="propertyName">The name of the property of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FE5 RID: 8165 RVA: 0x000506F4 File Offset: 0x0004E8F4
		public ControlParameter(string name, TypeCode type, string controlID, string propertyName)
			: base(name, type)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class by using the specified parameter name, database type, control ID, and property name. </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dbType">The data type of the parameter.</param>
		/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		/// <param name="propertyName">The name of the property of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06001FE6 RID: 8166 RVA: 0x0005070D File Offset: 0x0004E90D
		public ControlParameter(string name, DbType dbType, string controlID, string propertyName)
			: base(name, dbType)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.ControlParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ControlParameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00050726 File Offset: 0x0004E926
		protected override Parameter Clone()
		{
			return new ControlParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the updated and current value of the parameter.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that the parameter is bound to. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property is not set.- or -The <see cref="P:System.Web.UI.WebControls.ControlParameter.PropertyName" /> property is not set and the <see cref="T:System.Web.UI.Control" /> identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property is not decorated with a <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> attribute. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Web.UI.Control.FindControl(System.String)" /> does not return the specified control.- or -The control identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property does not support the property named by <see cref="P:System.Web.UI.WebControls.ControlParameter.PropertyName" />. </exception>
		// Token: 0x06001FE8 RID: 8168 RVA: 0x00050730 File Offset: 0x0004E930
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (control == null)
			{
				return null;
			}
			if (control.Page == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.ControlID))
			{
				throw new ArgumentException("The ControlID property is not set.");
			}
			Control control2 = null;
			for (Control control3 = control.NamingContainer; control3 != null; control3 = control3.NamingContainer)
			{
				control2 = control3.FindControl(this.ControlID);
				if (control2 != null)
				{
					break;
				}
			}
			if (control2 == null)
			{
				throw new InvalidOperationException("Control '" + this.ControlID + "' not found.");
			}
			string text = this.PropertyName;
			if (string.IsNullOrEmpty(text))
			{
				object[] customAttributes = control2.GetType().GetCustomAttributes(typeof(ControlValuePropertyAttribute), true);
				if (customAttributes.Length == 0)
				{
					throw new ArgumentException("The PropertyName property is not set and the Control identified by the ControlID property is not decorated with a ControlValuePropertyAttribute attribute.");
				}
				text = ((ControlValuePropertyAttribute)customAttributes[0]).Name;
			}
			return DataBinder.Eval(control2, text);
		}

		/// <summary>Specifies the name of the control that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to.</summary>
		/// <returns>A string that represents the name of a Web server control.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000507EF File Offset: 0x0004E9EF
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x00050806 File Offset: 0x0004EA06
		[IDReferenceProperty(typeof(Control))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ControlIDConverter))]
		[WebCategory("Control")]
		public string ControlID
		{
			get
			{
				return base.ViewState.GetString("ControlID", string.Empty);
			}
			set
			{
				if (this.ControlID != value)
				{
					base.ViewState["ControlID"] = value;
					base.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets the property name of the control identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to.</summary>
		/// <returns>A string that represents the name of a control's property that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> binds to.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0005082D File Offset: 0x0004EA2D
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x00050844 File Offset: 0x0004EA44
		[DefaultValue("")]
		[TypeConverter(typeof(ControlPropertyNameConverter))]
		[WebCategory("Control")]
		public string PropertyName
		{
			get
			{
				return base.ViewState.GetString("PropertyName", string.Empty);
			}
			set
			{
				if (this.PropertyName != value)
				{
					base.ViewState["PropertyName"] = value;
					base.OnParameterChanged();
				}
			}
		}
	}
}
