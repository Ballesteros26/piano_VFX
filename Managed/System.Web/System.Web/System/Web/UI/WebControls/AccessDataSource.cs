using System;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a Microsoft Access database for use with data-bound controls.</summary>
	// Token: 0x0200032E RID: 814
	[Designer("System.Web.UI.Design.WebControls.AccessDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxBitmap("")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AccessDataSource : SqlDataSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class.</summary>
		// Token: 0x06001C2F RID: 7215 RVA: 0x00046812 File Offset: 0x00044A12
		public AccessDataSource()
		{
			base.ProviderName = "System.Data.OleDb";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class with the specified data file path and Select command.</summary>
		/// <param name="dataFile">The location of the Access .mdb file. The location can be relative to the current Web form's folder, an absolute physical path, or a virtual path.</param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the Access database. If the SQL query is a parameterized SQL string, add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataFile" /> is null or an empty string.</exception>
		// Token: 0x06001C30 RID: 7216 RVA: 0x00046825 File Offset: 0x00044A25
		public AccessDataSource(string dataFile, string selectCommand)
			: base(string.Empty, selectCommand)
		{
			this.ProviderName = "System.Data.OleDb";
		}

		/// <summary>Creates a data source view object that is associated with the data source control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> instance.</returns>
		/// <param name="viewName">The name of the data source view.</param>
		// Token: 0x06001C31 RID: 7217 RVA: 0x00046840 File Offset: 0x00044A40
		protected override SqlDataSourceView CreateDataSourceView(string viewName)
		{
			AccessDataSourceView accessDataSourceView = new AccessDataSourceView(this, viewName, this.Context);
			if (base.IsTrackingViewState)
			{
				((IStateManager)accessDataSourceView).TrackViewState();
			}
			return accessDataSourceView;
		}

		/// <summary>The <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" /> property overrides the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SqlCacheDependency" /> property.</summary>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" />, in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to get or set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" />  property.</exception>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0004686A File Offset: 0x00044A6A
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0004686A File Offset: 0x00044A6A
		[Browsable(false)]
		[global::System.MonoTODO("AccessDataSource does not support SQL Cache Dependencies")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SqlCacheDependency
		{
			get
			{
				throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
			}
			set
			{
				throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the .NET data provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbFactory" /> object.</returns>
		// Token: 0x06001C34 RID: 7220 RVA: 0x00046876 File Offset: 0x00044A76
		[global::System.MonoTODO("why override?  maybe it doesn't call DbProviderFactories.GetFactory?")]
		protected override DbProviderFactory GetDbProviderFactory()
		{
			return DbProviderFactories.GetFactory("System.Data.OleDb");
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00046882 File Offset: 0x00044A82
		private string GetPhysicalDataFilePath()
		{
			if (string.IsNullOrEmpty(this.DataFile))
			{
				return string.Empty;
			}
			return HttpContext.Current.Request.MapPath(this.DataFile);
		}

		/// <summary>Gets the connection string that is used to connect to the Microsoft Access database.</summary>
		/// <returns>The OLE DB connection string that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to an Access database, through the <see cref="N:System.Data.OleDb" /> .NET data provider.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ConnectionString" /> property.</exception>
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000468AC File Offset: 0x00044AAC
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x000468DC File Offset: 0x00044ADC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ConnectionString
		{
			get
			{
				if (this.connectionString == null)
				{
					this.connectionString = "Provider=" + "Microsoft.Jet.OLEDB.4.0" + "; Data Source=" + this.GetPhysicalDataFilePath();
				}
				return this.connectionString;
			}
			set
			{
				throw new InvalidOperationException("The ConnectionString is automatically generated for AccessDataSource and hence cannot be set.");
			}
		}

		/// <summary>Gets or sets the location of the Microsoft Access .mdb file.</summary>
		/// <returns>The location of the Access .mdb file. Absolute, relative, and virtual paths are supported.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid path was given.</exception>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000468E8 File Offset: 0x00044AE8
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x000468FF File Offset: 0x00044AFF
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("MS Office Access database file name")]
		[Editor("System.Web.UI.Design.MdbDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataFile
		{
			get
			{
				return this.ViewState.GetString("DataFile", string.Empty);
			}
			set
			{
				this.ViewState["DataFile"] = value;
				this.connectionString = null;
			}
		}

		/// <summary>Gets the name of the .NET data provider that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to a Microsoft Access database.</summary>
		/// <returns>The string "System.Data.OleDb".</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ProviderName" /> property. </exception>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00046919 File Offset: 0x00044B19
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00046921 File Offset: 0x00044B21
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ProviderName
		{
			get
			{
				return base.ProviderName;
			}
			set
			{
				throw new InvalidOperationException("Setting ProviderName on an AccessDataSource is not allowed");
			}
		}

		// Token: 0x040017E4 RID: 6116
		private const string PROVIDER_NAME = "System.Data.OleDb";

		// Token: 0x040017E5 RID: 6117
		private const string PROVIDER_STRING = "Microsoft.Jet.OLEDB.4.0";

		// Token: 0x040017E6 RID: 6118
		private string connectionString;
	}
}
