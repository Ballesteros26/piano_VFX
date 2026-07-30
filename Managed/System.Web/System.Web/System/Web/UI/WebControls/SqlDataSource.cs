using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an SQL database to data-bound controls.</summary>
	// Token: 0x0200040E RID: 1038
	[ParseChildren(true)]
	[DefaultProperty("SelectQuery")]
	[ToolboxBitmap("")]
	[PersistChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.SqlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Selecting")]
	public class SqlDataSource : DataSourceControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
		// Token: 0x06002E1A RID: 11802 RVA: 0x00079F9D File Offset: 0x0007819D
		public SqlDataSource()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
		/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		// Token: 0x06002E1B RID: 11803 RVA: 0x00079FC2 File Offset: 0x000781C2
		public SqlDataSource(string connectionString, string selectCommand)
		{
			this.ConnectionString = connectionString;
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
		/// <param name="providerName">The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses. If no provider is set, the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses the ADO.NET provider for Microsoft SQL Server, by default. </param>
		/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		// Token: 0x06002E1C RID: 11804 RVA: 0x00079FF5 File Offset: 0x000781F5
		public SqlDataSource(string providerName, string connectionString, string selectCommand)
		{
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.SelectCommand = selectCommand;
		}

		/// <summary>Gets the named data source view that is associated with the data source control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> named "Table" that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="viewName" /> is null or something other than Table. </exception>
		// Token: 0x06002E1D RID: 11805 RVA: 0x0007A02F File Offset: 0x0007822F
		protected override DataSourceView GetView(string viewName)
		{
			if (string.IsNullOrEmpty(viewName) || string.Compare(viewName, SqlDataSource.emptyNames[0], StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return this.View;
			}
			throw new ArgumentException("viewName");
		}

		/// <summary>Creates a data source view object that is associated with the data source control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		/// <param name="viewName">The name of the data source view. </param>
		// Token: 0x06002E1E RID: 11806 RVA: 0x0007A05C File Offset: 0x0007825C
		protected virtual SqlDataSourceView CreateDataSourceView(string viewName)
		{
			SqlDataSourceView sqlDataSourceView = new SqlDataSourceView(this, viewName, this.Context);
			if (base.IsTrackingViewState)
			{
				((IStateManager)sqlDataSourceView).TrackViewState();
			}
			return sqlDataSourceView;
		}

		/// <summary>Returns the <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the ADO.NET provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbProviderFactory" /> that represents the identified ADO.NET provider; otherwise, and instance of the <see cref="N:System.Data.SqlClient" />, if no provider is set.</returns>
		// Token: 0x06002E1F RID: 11807 RVA: 0x0007A086 File Offset: 0x00078286
		protected virtual DbProviderFactory GetDbProviderFactory()
		{
			if (!string.IsNullOrEmpty(this.ProviderName))
			{
				return DbProviderFactories.GetFactory(this.ProviderName);
			}
			return SqlClientFactory.Instance;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0007A0A6 File Offset: 0x000782A6
		internal DbProviderFactory GetDbProviderFactoryInternal()
		{
			return this.GetDbProviderFactory();
		}

		/// <summary>Gets a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x06002E21 RID: 11809 RVA: 0x0007A0AE File Offset: 0x000782AE
		protected override ICollection GetViewNames()
		{
			return SqlDataSource.emptyNames;
		}

		/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002E22 RID: 11810 RVA: 0x0007A0B5 File Offset: 0x000782B5
		public int Insert()
		{
			return this.View.Insert(null);
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002E23 RID: 11811 RVA: 0x0007A0C3 File Offset: 0x000782C3
		public int Delete()
		{
			return this.View.Delete(null, null);
		}

		/// <summary>Retrieves data from the underlying database by using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that is used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> object cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002E24 RID: 11812 RVA: 0x0007A0D2 File Offset: 0x000782D2
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.View.Select(arguments);
		}

		/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002E25 RID: 11813 RVA: 0x0007A0E0 File Offset: 0x000782E0
		public int Update()
		{
			return this.View.Update(null, null, null);
		}

		/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the <see cref="T:System.Web.UI.Page" /> control that contains the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002E26 RID: 11814 RVA: 0x0007A0F0 File Offset: 0x000782F0
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x0007A110 File Offset: 0x00078310
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.FilterParameters.UpdateValues(this.Context, this);
			this.SelectParameters.UpdateValues(this.Context, this);
		}

		/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control that need to be persisted.</summary>
		/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</param>
		// Token: 0x06002E28 RID: 11816 RVA: 0x0007A138 File Offset: 0x00078338
		protected override void LoadViewState(object savedState)
		{
			Pair pair = savedState as Pair;
			if (pair != null)
			{
				base.LoadViewState(pair.First);
				((IStateManager)this.View).LoadViewState(pair.Second);
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x06002E29 RID: 11817 RVA: 0x0007A16C File Offset: 0x0007836C
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.View).SaveViewState();
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		/// <summary>Tracks view state changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control so that the changes can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control.</summary>
		// Token: 0x06002E2A RID: 11818 RVA: 0x0007A19B File Offset: 0x0007839B
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.view != null)
			{
				((IStateManager)this.view).TrackViewState();
			}
		}

		/// <summary>Gets or sets a value indicating whether a data retrieval operation is canceled when any parameter that is contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluates to null.</summary>
		/// <returns>true if a data retrieval operation is canceled when a parameter contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluated to null; otherwise, false. The default is true.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x0007A1B6 File Offset: 0x000783B6
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x0007A1C3 File Offset: 0x000783C3
		[DefaultValue(true)]
		public virtual bool CancelSelectOnNullParameter
		{
			get
			{
				return this.View.CancelSelectOnNullParameter;
			}
			set
			{
				this.View.CancelSelectOnNullParameter = value;
			}
		}

		/// <summary>Gets or sets the value indicating how the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control performs updates and deletes when data in a row in the underlying database changes during the time of the operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x0007A1D1 File Offset: 0x000783D1
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x0007A1DE File Offset: 0x000783DE
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.View.ConflictDetection;
			}
			set
			{
				this.View.ConflictDetection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x0007A1EC File Offset: 0x000783EC
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x0007A1F9 File Offset: 0x000783F9
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType DeleteCommandType
		{
			get
			{
				return this.View.DeleteCommandType;
			}
			set
			{
				this.View.DeleteCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property is an SQL statement or the name of a stored procedure. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x0007A207 File Offset: 0x00078407
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x0007A214 File Offset: 0x00078414
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType InsertCommandType
		{
			get
			{
				return this.View.InsertCommandType;
			}
			set
			{
				this.View.InsertCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property is an SQL query or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x0007A222 File Offset: 0x00078422
		// (set) Token: 0x06002E34 RID: 11828 RVA: 0x0007A22F File Offset: 0x0007842F
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType SelectCommandType
		{
			get
			{
				return this.View.SelectCommandType;
			}
			set
			{
				this.View.SelectCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x0007A23D File Offset: 0x0007843D
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x0007A24A File Offset: 0x0007844A
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType UpdateCommandType
		{
			get
			{
				return this.View.UpdateCommandType;
			}
			set
			{
				this.View.UpdateCommandType = value;
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of any parameters that are passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> method.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> parameters passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> methods. The default is "{0}".</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x0007A258 File Offset: 0x00078458
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x0007A265 File Offset: 0x00078465
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.View.OldValuesParameterFormatString;
			}
			set
			{
				this.View.OldValuesParameterFormatString = value;
			}
		}

		/// <summary>Gets or sets the name of a stored procedure parameter that is used to sort retrieved data when data retrieval is performed using a stored procedure.</summary>
		/// <returns>The name of a stored procedure parameter used to sort retrieved data when data retrieval is performed using a stored procedure.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x0007A273 File Offset: 0x00078473
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x0007A280 File Offset: 0x00078480
		[DefaultValue("")]
		public string SortParameterName
		{
			get
			{
				return this.View.SortParameterName;
			}
			set
			{
				this.View.SortParameterName = value;
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method is called.</summary>
		/// <returns>A string that represents a filtering expression applied when data is retrieved using the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property was set and the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> is in <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataReader" /> mode. </exception>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0007A28E File Offset: 0x0007848E
		// (set) Token: 0x06002E3C RID: 11836 RVA: 0x0007A29B File Offset: 0x0007849B
		[DefaultValue("")]
		public string FilterExpression
		{
			get
			{
				return this.View.FilterExpression;
			}
			set
			{
				this.View.FilterExpression = value;
			}
		}

		/// <summary>Gets or sets the name of the .NET Framework data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying data source.</summary>
		/// <returns>The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses; otherwise, the ADO.NET provider for Microsoft SQL Server, if no provider is set. The default is the ADO.NET provider for Microsoft SQL Server.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x0007A2A9 File Offset: 0x000784A9
		// (set) Token: 0x06002E3E RID: 11838 RVA: 0x0007A2B1 File Offset: 0x000784B1
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.WebControls.DataProviderNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ProviderName
		{
			get
			{
				return this.providerName;
			}
			set
			{
				if (this.providerName != value)
				{
					this.providerName = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the ADO.NET provider–specific connection string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying database.</summary>
		/// <returns>A .NET Framework data provider–specific string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to connect to the SQL database that it represents. The default is an empty string ("").</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x0007A2D3 File Offset: 0x000784D3
		// (set) Token: 0x06002E40 RID: 11840 RVA: 0x0007A2DB File Offset: 0x000784DB
		[DefaultValue("")]
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.SqlDataSourceConnectionStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				if (this.connectionString != value)
				{
					this.connectionString = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data retrieval mode that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to fetch data.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" /> values. The default is the  <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataSet" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.DataSourceMode" /> property is not one of the values defined in the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" />. </exception>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x0007A2FD File Offset: 0x000784FD
		// (set) Token: 0x06002E42 RID: 11842 RVA: 0x0007A305 File Offset: 0x00078505
		[DefaultValue(SqlDataSourceMode.DataSet)]
		public SqlDataSourceMode DataSourceMode
		{
			get
			{
				return this.dataSourceMode;
			}
			set
			{
				if (this.dataSourceMode != value)
				{
					this.dataSourceMode = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to delete data from the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to delete data.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0007A322 File Offset: 0x00078522
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x0007A32F File Offset: 0x0007852F
		[DefaultValue("")]
		public string DeleteCommand
		{
			get
			{
				return this.View.DeleteCommand;
			}
			set
			{
				this.View.DeleteCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x0007A33D File Offset: 0x0007853D
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.View.DeleteParameters;
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x0007A34A File Offset: 0x0007854A
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.View.FilterParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to insert data into the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to insert data.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x0007A357 File Offset: 0x00078557
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x0007A364 File Offset: 0x00078564
		[DefaultValue("")]
		public string InsertCommand
		{
			get
			{
				return this.View.InsertCommand;
			}
			set
			{
				this.View.InsertCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0007A372 File Offset: 0x00078572
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.View.InsertParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to retrieve data from the underlying database.</summary>
		/// <returns>An SQL string or the name of a stored procedure that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to retrieve data.</returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x0007A37F File Offset: 0x0007857F
		// (set) Token: 0x06002E4B RID: 11851 RVA: 0x0007A38C File Offset: 0x0007858C
		[DefaultValue("")]
		public string SelectCommand
		{
			get
			{
				return this.View.SelectCommand;
			}
			set
			{
				this.View.SelectCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x0007A39A File Offset: 0x0007859A
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.View.SelectParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to update data in the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to update data.</returns>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x0007A3A7 File Offset: 0x000785A7
		// (set) Token: 0x06002E4E RID: 11854 RVA: 0x0007A3B4 File Offset: 0x000785B4
		[DefaultValue("")]
		public string UpdateCommand
		{
			get
			{
				return this.View.UpdateCommand;
			}
			set
			{
				this.View.UpdateCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> control that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x0007A3C2 File Offset: 0x000785C2
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.View.UpdateParameters;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x0007A3CF File Offset: 0x000785CF
		internal DataSourceCacheManager Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new DataSourceCacheManager(this.CacheDuration, this.CacheKeyDependency, this.CacheExpirationPolicy, this, this.Context);
				}
				return this.cache;
			}
		}

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control. All cache objects are explicitly expired when the key is expired.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x0007A403 File Offset: 0x00078603
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x0007A419 File Offset: 0x00078619
		[DefaultValue("")]
		public virtual string CacheKeyDependency
		{
			get
			{
				if (this.cacheKeyDependency == null)
				{
					return string.Empty;
				}
				return this.cacheKeyDependency;
			}
			set
			{
				this.cacheKeyDependency = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited string that indicates which databases and tables to use for the Microsoft SQL Server cache dependency.</summary>
		/// <returns>A string that indicates which databases and tables to use for the SQL Server cache dependency.</returns>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x0007A422 File Offset: 0x00078622
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x0007A438 File Offset: 0x00078638
		[DefaultValue("")]
		[global::System.MonoTODO("SQLServer specific")]
		public virtual string SqlCacheDependency
		{
			get
			{
				if (this.sqlCacheDependency == null)
				{
					return string.Empty;
				}
				return this.sqlCacheDependency;
			}
			set
			{
				this.sqlCacheDependency = value;
			}
		}

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> caches the results of a <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> operation. The default is 0. The value cannot be negative.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x0007A441 File Offset: 0x00078641
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x0007A449 File Offset: 0x00078649
		[TypeConverter("System.Web.UI.DataSourceCacheDurationConverter")]
		[DefaultValue(0)]
		public virtual int CacheDuration
		{
			get
			{
				return this.cacheDuration;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The duration must be non-negative");
				}
				this.cacheDuration = value;
			}
		}

		/// <summary>Gets or sets the cache expiration behavior that, when combined with the duration, describes the behavior of the cache that the data source control uses.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is the <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" /> value.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x0007A466 File Offset: 0x00078666
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x0007A46E File Offset: 0x0007866E
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this.cacheExpirationPolicy;
			}
			set
			{
				this.cacheExpirationPolicy = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.EnableCaching" /> property is set to true when caching is not supported by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />. </exception>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x0007A477 File Offset: 0x00078677
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x0007A47F File Offset: 0x0007867F
		[DefaultValue(false)]
		public virtual bool EnableCaching
		{
			get
			{
				return this.enableCaching;
			}
			set
			{
				if (this.DataSourceMode == SqlDataSourceMode.DataReader && value)
				{
					throw new NotSupportedException();
				}
				this.enableCaching = value;
			}
		}

		/// <summary>Occurs when a delete operation has completed.</summary>
		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06002E5B RID: 11867 RVA: 0x0007A49B File Offset: 0x0007869B
		// (remove) Token: 0x06002E5C RID: 11868 RVA: 0x0007A4A9 File Offset: 0x000786A9
		public event SqlDataSourceStatusEventHandler Deleted
		{
			add
			{
				this.View.Deleted += value;
			}
			remove
			{
				this.View.Deleted -= value;
			}
		}

		/// <summary>Occurs before a delete operation.</summary>
		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06002E5D RID: 11869 RVA: 0x0007A4B7 File Offset: 0x000786B7
		// (remove) Token: 0x06002E5E RID: 11870 RVA: 0x0007A4C5 File Offset: 0x000786C5
		public event SqlDataSourceCommandEventHandler Deleting
		{
			add
			{
				this.View.Deleting += value;
			}
			remove
			{
				this.View.Deleting -= value;
			}
		}

		/// <summary>Occurs when an insert operation has completed.</summary>
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06002E5F RID: 11871 RVA: 0x0007A4D3 File Offset: 0x000786D3
		// (remove) Token: 0x06002E60 RID: 11872 RVA: 0x0007A4E1 File Offset: 0x000786E1
		public event SqlDataSourceStatusEventHandler Inserted
		{
			add
			{
				this.View.Inserted += value;
			}
			remove
			{
				this.View.Inserted -= value;
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06002E61 RID: 11873 RVA: 0x0007A4EF File Offset: 0x000786EF
		// (remove) Token: 0x06002E62 RID: 11874 RVA: 0x0007A4FD File Offset: 0x000786FD
		public event SqlDataSourceFilteringEventHandler Filtering
		{
			add
			{
				this.View.Filtering += value;
			}
			remove
			{
				this.View.Filtering -= value;
			}
		}

		/// <summary>Occurs before an insert operation.</summary>
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06002E63 RID: 11875 RVA: 0x0007A50B File Offset: 0x0007870B
		// (remove) Token: 0x06002E64 RID: 11876 RVA: 0x0007A519 File Offset: 0x00078719
		public event SqlDataSourceCommandEventHandler Inserting
		{
			add
			{
				this.View.Inserting += value;
			}
			remove
			{
				this.View.Inserting -= value;
			}
		}

		/// <summary>Occurs when a data retrieval operation has completed.</summary>
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06002E65 RID: 11877 RVA: 0x0007A527 File Offset: 0x00078727
		// (remove) Token: 0x06002E66 RID: 11878 RVA: 0x0007A535 File Offset: 0x00078735
		public event SqlDataSourceStatusEventHandler Selected
		{
			add
			{
				this.View.Selected += value;
			}
			remove
			{
				this.View.Selected -= value;
			}
		}

		/// <summary>Occurs before a data retrieval operation.</summary>
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06002E67 RID: 11879 RVA: 0x0007A543 File Offset: 0x00078743
		// (remove) Token: 0x06002E68 RID: 11880 RVA: 0x0007A551 File Offset: 0x00078751
		public event SqlDataSourceSelectingEventHandler Selecting
		{
			add
			{
				this.View.Selecting += value;
			}
			remove
			{
				this.View.Selecting -= value;
			}
		}

		/// <summary>Occurs when an update operation has completed.</summary>
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06002E69 RID: 11881 RVA: 0x0007A55F File Offset: 0x0007875F
		// (remove) Token: 0x06002E6A RID: 11882 RVA: 0x0007A56D File Offset: 0x0007876D
		public event SqlDataSourceStatusEventHandler Updated
		{
			add
			{
				this.View.Updated += value;
			}
			remove
			{
				this.View.Updated -= value;
			}
		}

		/// <summary>Occurs before an update operation.</summary>
		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06002E6B RID: 11883 RVA: 0x0007A57B File Offset: 0x0007877B
		// (remove) Token: 0x06002E6C RID: 11884 RVA: 0x0007A589 File Offset: 0x00078789
		public event SqlDataSourceCommandEventHandler Updating
		{
			add
			{
				this.View.Updating += value;
			}
			remove
			{
				this.View.Updating -= value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x0007A597 File Offset: 0x00078797
		private SqlDataSourceView View
		{
			get
			{
				if (this.view == null)
				{
					this.view = this.CreateDataSourceView("DefaultView");
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.view).TrackViewState();
					}
				}
				return this.view;
			}
		}

		// Token: 0x04001B9C RID: 7068
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001B9D RID: 7069
		private string providerName = string.Empty;

		// Token: 0x04001B9E RID: 7070
		private string connectionString = string.Empty;

		// Token: 0x04001B9F RID: 7071
		private SqlDataSourceMode dataSourceMode = SqlDataSourceMode.DataSet;

		// Token: 0x04001BA0 RID: 7072
		private int cacheDuration;

		// Token: 0x04001BA1 RID: 7073
		private bool enableCaching;

		// Token: 0x04001BA2 RID: 7074
		private string cacheKeyDependency;

		// Token: 0x04001BA3 RID: 7075
		private string sqlCacheDependency;

		// Token: 0x04001BA4 RID: 7076
		private DataSourceCacheManager cache;

		// Token: 0x04001BA5 RID: 7077
		private DataSourceCacheExpiry cacheExpirationPolicy;

		// Token: 0x04001BA6 RID: 7078
		private SqlDataSourceView view;
	}
}
