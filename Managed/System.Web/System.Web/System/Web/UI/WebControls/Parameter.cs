using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a mechanism that data source controls use to bind to application variables, user identities and choices, and other data. Serves as the base class for all ASP.NET parameter types. </summary>
	// Token: 0x020003E7 RID: 999
	[DefaultProperty("DefaultValue")]
	public class Parameter : ICloneable, IStateManager
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class.</summary>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x00002050 File Offset: 0x00000250
		public Parameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class with the values of the original, specified instance.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.Parameter" /> instance from which the current instance is initialized. </param>
		// Token: 0x06002BE1 RID: 11233 RVA: 0x0007490C File Offset: 0x00072B0C
		protected Parameter(Parameter original)
		{
			if (original == null)
			{
				throw new NullReferenceException(".NET emulation");
			}
			this.DefaultValue = original.DefaultValue;
			this.Direction = original.Direction;
			this.ConvertEmptyStringToNull = original.ConvertEmptyStringToNull;
			this.Type = original.Type;
			this.Name = original.Name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name.</summary>
		/// <param name="name">The name of the parameter. </param>
		// Token: 0x06002BE2 RID: 11234 RVA: 0x00074969 File Offset: 0x00072B69
		public Parameter(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name and type.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">A <see cref="T:System.TypeCode" /> that describes the type of the parameter. </param>
		// Token: 0x06002BE3 RID: 11235 RVA: 0x00074978 File Offset: 0x00072B78
		public Parameter(string name, TypeCode type)
			: this(name)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name, the specified type, and the specified string for its <see cref="P:System.Web.UI.WebControls.Parameter.DefaultValue" /> property.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">A <see cref="T:System.TypeCode" /> that describes the type of the parameter. </param>
		/// <param name="defaultValue">A string that serves as a default value for the parameter, if the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to a value that is not yet initialized when <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> is called. </param>
		// Token: 0x06002BE4 RID: 11236 RVA: 0x00074988 File Offset: 0x00072B88
		public Parameter(string name, TypeCode type, string defaultValue)
			: this(name, type)
		{
			this.DefaultValue = defaultValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name and database type.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="dbType">The database type of the parameter. </param>
		// Token: 0x06002BE5 RID: 11237 RVA: 0x00074999 File Offset: 0x00072B99
		public Parameter(string name, DbType dbType)
			: this(name)
		{
			this.DbType = dbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name, the specified database type, and the specified value for its <see cref="P:System.Web.UI.WebControls.Parameter.DefaultValue" /> property.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. </param>
		/// <param name="dbType">The database type of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. </param>
		/// <param name="defaultValue">The default value for the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance, if the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to a value that is not yet initialized when <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> is called. </param>
		// Token: 0x06002BE6 RID: 11238 RVA: 0x000749A9 File Offset: 0x00072BA9
		public Parameter(string name, DbType dbType, string defaultValue)
			: this(name, dbType)
		{
			this.DefaultValue = defaultValue;
		}

		/// <summary>Converts a <see cref="T:System.Data.DbType" /> value to an equivalent <see cref="T:System.TypeCode" /> value.</summary>
		/// <returns>A <see cref="T:System.TypeCode" /> value that is equivalent to the specified <see cref="T:System.Data.DbType" /> value.</returns>
		/// <param name="dbType">A <see cref="T:System.Data.DbType" /> value to convert to an equivalent <see cref="T:System.TypeCode" /> value.</param>
		// Token: 0x06002BE7 RID: 11239 RVA: 0x000749BC File Offset: 0x00072BBC
		public static TypeCode ConvertDbTypeToTypeCode(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
			case DbType.String:
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:
				return TypeCode.String;
			case DbType.Binary:
			case DbType.Guid:
			case DbType.Object:
			case DbType.Xml:
			case DbType.DateTimeOffset:
				return TypeCode.Object;
			case DbType.Byte:
				return TypeCode.Byte;
			case DbType.Boolean:
				return TypeCode.Boolean;
			case DbType.Currency:
			case DbType.Decimal:
			case DbType.VarNumeric:
				return TypeCode.Decimal;
			case DbType.Date:
			case DbType.DateTime:
			case DbType.Time:
			case DbType.DateTime2:
				return TypeCode.DateTime;
			case DbType.Double:
				return TypeCode.Double;
			case DbType.Int16:
				return TypeCode.Int16;
			case DbType.Int32:
				return TypeCode.Int32;
			case DbType.Int64:
				return TypeCode.Int64;
			case DbType.SByte:
				return TypeCode.SByte;
			case DbType.Single:
				return TypeCode.Single;
			case DbType.UInt16:
				return TypeCode.UInt16;
			case DbType.UInt32:
				return TypeCode.UInt32;
			case DbType.UInt64:
				return TypeCode.UInt64;
			}
			return TypeCode.Object;
		}

		/// <summary>Converts a <see cref="T:System.TypeCode" /> value to an equivalent <see cref="T:System.Data.DbType" /> value.</summary>
		/// <returns>A <see cref="T:System.Data.DbType" /> value that is equivalent to the specified <see cref="T:System.TypeCode" /> value.</returns>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> value to convert to an equivalent <see cref="T:System.Data.DbType" /> value.</param>
		// Token: 0x06002BE8 RID: 11240 RVA: 0x00074A6C File Offset: 0x00072C6C
		public static DbType ConvertTypeCodeToDbType(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Object:
			case TypeCode.DBNull:
				return DbType.Object;
			case TypeCode.Boolean:
				return DbType.Boolean;
			case TypeCode.Char:
				return DbType.StringFixedLength;
			case TypeCode.SByte:
				return DbType.SByte;
			case TypeCode.Byte:
				return DbType.Byte;
			case TypeCode.Int16:
				return DbType.Int16;
			case TypeCode.UInt16:
				return DbType.UInt16;
			case TypeCode.Int32:
				return DbType.Int32;
			case TypeCode.UInt32:
				return DbType.UInt32;
			case TypeCode.Int64:
				return DbType.Int64;
			case TypeCode.UInt64:
				return DbType.UInt64;
			case TypeCode.Single:
				return DbType.Single;
			case TypeCode.Double:
				return DbType.Double;
			case TypeCode.Decimal:
				return DbType.Decimal;
			case TypeCode.DateTime:
				return DbType.DateTime;
			case TypeCode.String:
				return DbType.String;
			}
			return DbType.Object;
		}

		/// <summary>Gets the <see cref="T:System.Data.DbType" /> value that is equivalent to the CLR type of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
		/// <returns>The <see cref="T:System.Data.DbType" /> value that is equivalent to the CLR type of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.Parameter.DbType" /> property is already set to a value other than <see cref="F:System.Data.DbType.Object" />.</exception>
		// Token: 0x06002BE9 RID: 11241 RVA: 0x00074AFA File Offset: 0x00072CFA
		public DbType GetDatabaseType()
		{
			if (this.DbType != DbType.Object)
			{
				throw new InvalidOperationException("The DbType property is already set to a value other than DbType.Object.");
			}
			return Parameter.ConvertTypeCodeToDbType(this.Type);
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Parameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06002BEA RID: 11242 RVA: 0x00074B1C File Offset: 0x00072D1C
		protected virtual Parameter Clone()
		{
			return new Parameter(this);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.ParameterCollection.OnParametersChanged(System.EventArgs)" /> method of the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection that contains the <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
		// Token: 0x06002BEB RID: 11243 RVA: 0x00074B24 File Offset: 0x00072D24
		protected void OnParameterChanged()
		{
			if (this._owner != null)
			{
				this._owner.CallOnParameterChanged();
			}
		}

		/// <summary>Restores the data source view's previously saved view state.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.Parameter" /> state to restore. </param>
		// Token: 0x06002BEC RID: 11244 RVA: 0x00074B39 File Offset: 0x00072D39
		protected virtual void LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state since the time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> view state. If there is no view state associated with the object, this method returns null.</returns>
		// Token: 0x06002BED RID: 11245 RVA: 0x00074B47 File Offset: 0x00072D47
		protected virtual object SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> object and persisted across requests for the same page.</summary>
		// Token: 0x06002BEE RID: 11246 RVA: 0x00074B54 File Offset: 0x00072D54
		protected virtual void TrackViewState()
		{
			this.isTrackingViewState = true;
			if (this.viewState != null)
			{
				this.viewState.TrackViewState();
			}
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Parameter" /> that is a copy of the current object.</returns>
		// Token: 0x06002BEF RID: 11247 RVA: 0x00074B70 File Offset: 0x00072D70
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Restores the data source view's previously saved view state.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.Parameter" /> state to restore. </param>
		// Token: 0x06002BF0 RID: 11248 RVA: 0x00074B78 File Offset: 0x00072D78
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state since the time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state. If there is no view state associated with the object, this method returns null.</returns>
		// Token: 0x06002BF1 RID: 11249 RVA: 0x00074B81 File Offset: 0x00072D81
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> object and persisted across requests for the same page.</summary>
		// Token: 0x06002BF2 RID: 11250 RVA: 0x00074B89 File Offset: 0x00072D89
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is saving changes to its view state.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x00074B91 File Offset: 0x00072D91
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>A string representation of the value of this instance.</returns>
		// Token: 0x06002BF4 RID: 11252 RVA: 0x00074B99 File Offset: 0x00072D99
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>Specifies a default value for the parameter, should the value that the parameter is bound to be uninitialized when the <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method is called.</summary>
		/// <returns>A string that serves as a default value for the <see cref="T:System.Web.UI.WebControls.Parameter" /> when the value it is bound to cannot be resolved or is uninitialized.</returns>
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x00074BA1 File Offset: 0x00072DA1
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x00074BB4 File Offset: 0x00072DB4
		[WebCategory("Parameter")]
		[DefaultValue(null)]
		[WebSysDescription("Default value to be used in case value is null.")]
		public string DefaultValue
		{
			get
			{
				return this.ViewState.GetString("DefaultValue", null);
			}
			set
			{
				if (this.DefaultValue != value)
				{
					this.ViewState["DefaultValue"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is used to bind a value to a control, or the control can be used to change the value.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. <see cref="P:System.Web.UI.WebControls.Parameter.Direction" /> is set to <see cref="F:System.Data.ParameterDirection.Input" /> by default.</returns>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x00074BDB File Offset: 0x00072DDB
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x00074BEE File Offset: 0x00072DEE
		[WebCategory("Parameter")]
		[DefaultValue("Input")]
		[WebSysDescription("Parameter's direction.")]
		public ParameterDirection Direction
		{
			get
			{
				return (ParameterDirection)this.ViewState.GetInt("Direction", 1);
			}
			set
			{
				if (this.Direction != value)
				{
					this.ViewState["Direction"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x00074C18 File Offset: 0x00072E18
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x00074C45 File Offset: 0x00072E45
		[WebCategory("Parameter")]
		[DefaultValue("")]
		[WebSysDescription("Parameter's name.")]
		public string Name
		{
			get
			{
				string text = this.ViewState["Name"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (this.Name != value)
				{
					this.ViewState["Name"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the value that the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is bound to should be converted to null if it is <see cref="F:System.String.Empty" />.</summary>
		/// <returns>true if the value that the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to should be converted to null when it is <see cref="F:System.String.Empty" />; otherwise, false. The default value is true.</returns>
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x00074C6C File Offset: 0x00072E6C
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x00074C7F File Offset: 0x00072E7F
		[WebCategory("Parameter")]
		[DefaultValue(true)]
		[WebSysDescription("Checks whether an empty string is treated as a null value.")]
		public bool ConvertEmptyStringToNull
		{
			get
			{
				return this.ViewState.GetBool("ConvertEmptyStringToNull", true);
			}
			set
			{
				if (this.ConvertEmptyStringToNull != value)
				{
					this.ViewState["ConvertEmptyStringToNull"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets the database type of the parameter.</summary>
		/// <returns>The database type of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. The default value is <see cref="F:System.Data.DbType.Object" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is not in the <see cref="T:System.Data.DbType" /> enumeration.</exception>
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x00074CA8 File Offset: 0x00072EA8
		// (set) Token: 0x06002BFE RID: 11262 RVA: 0x00074CD2 File Offset: 0x00072ED2
		[WebCategory("Parameter")]
		[DefaultValue(DbType.Object)]
		[WebSysDescription("Parameter's DbType.")]
		public DbType DbType
		{
			get
			{
				object obj = this.ViewState["DbType"];
				if (obj == null)
				{
					return DbType.Object;
				}
				return (DbType)obj;
			}
			set
			{
				if (this.DbType != value)
				{
					this.ViewState["DbType"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets the size of the parameter.</summary>
		/// <returns>The size of the parameter expressed as an integer.</returns>
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x00074CF9 File Offset: 0x00072EF9
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x00074D0C File Offset: 0x00072F0C
		[DefaultValue(0)]
		public int Size
		{
			get
			{
				return this.ViewState.GetInt("Size", 0);
			}
			set
			{
				if (this.Size != value)
				{
					this.ViewState["Size"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the <see cref="T:System.Web.UI.WebControls.Parameter" />. The default value is <see cref="F:System.TypeCode.Object" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameter type is not one of the <see cref="T:System.TypeCode" /> values.</exception>
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x00074D33 File Offset: 0x00072F33
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x00074D46 File Offset: 0x00072F46
		[WebCategory("Parameter")]
		[DefaultValue(TypeCode.Empty)]
		[WebSysDescription("Represents type of the parameter.")]
		public TypeCode Type
		{
			get
			{
				return (TypeCode)this.ViewState.GetInt("Type", 0);
			}
			set
			{
				if (this.Type != value)
				{
					this.ViewState["Type"] = value;
					this.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.Parameter" /> object across multiple requests for the same page.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view-state information.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x00074D6D File Offset: 0x00072F6D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (this.viewState == null)
				{
					this.viewState = new StateBag();
					if (this.IsTrackingViewState)
					{
						this.viewState.TrackViewState();
					}
				}
				return this.viewState;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is saving changes to its view state.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x00074D9B File Offset: 0x00072F9B
		protected bool IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> the parameter is bound to. If the parameter is not bound to a control, the <paramref name="control" /> parameter is ignored. </param>
		// Token: 0x06002C05 RID: 11269 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected internal virtual object Evaluate(HttpContext context, Control control)
		{
			return null;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x00074DA4 File Offset: 0x00072FA4
		internal void UpdateValue(HttpContext context, Control control)
		{
			object obj = this.ViewState["ParameterValue"];
			object obj2 = this.Evaluate(context, control);
			if (!object.Equals(obj, obj2))
			{
				this.ViewState["ParameterValue"] = obj2;
				this.OnParameterChanged();
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00074DEC File Offset: 0x00072FEC
		internal object GetValue(HttpContext context, Control control)
		{
			this.UpdateValue(context, control);
			object obj = this.ConvertValue(this.ViewState["ParameterValue"]);
			if (obj == null)
			{
				obj = this.ConvertValue(this.DefaultValue);
			}
			return obj;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00074E29 File Offset: 0x00073029
		internal object ConvertValue(object val)
		{
			if (val == null)
			{
				return null;
			}
			if (this.ConvertEmptyStringToNull && val.Equals(string.Empty))
			{
				return null;
			}
			if (this.Type == TypeCode.Empty)
			{
				return val;
			}
			return Convert.ChangeType(val, this.Type);
		}

		/// <summary>Marks the <see cref="T:System.Web.UI.WebControls.Parameter" /> object so its state will be recorded in view state.</summary>
		// Token: 0x06002C09 RID: 11273 RVA: 0x00074E5D File Offset: 0x0007305D
		protected internal virtual void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00074E6B File Offset: 0x0007306B
		internal void SetOwnerCollection(ParameterCollection own)
		{
			this._owner = own;
		}

		// Token: 0x04001B34 RID: 6964
		private StateBag viewState;

		// Token: 0x04001B35 RID: 6965
		private bool isTrackingViewState;

		// Token: 0x04001B36 RID: 6966
		private ParameterCollection _owner;
	}
}
