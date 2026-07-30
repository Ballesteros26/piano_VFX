using System;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	// Token: 0x0200017F RID: 383
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDataSourceDesigner : DataSourceDesigner
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> class.</summary>
		// Token: 0x06000B14 RID: 2836 RVA: 0x00009519 File Offset: 0x00007719
		public SqlDataSourceDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>The string used to open a database connection at runtime.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00009519 File Offset: 0x00007719
		public string ConnectionString
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Indicates that this <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> supports delete queries.</summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.DataSourceOperation.Delete" />.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00016628 File Offset: 0x00014828
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00009519 File Offset: 0x00007719
		public DataSourceOperation DeleteQuery
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return DataSourceOperation.Delete;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Indicates that this <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> supports insert queries.</summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.DataSourceOperation.Insert" />.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00016644 File Offset: 0x00014844
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00009519 File Offset: 0x00007719
		public DataSourceOperation InsertQuery
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return DataSourceOperation.Delete;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the .NET Framework data provider that the associated <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying data source.</summary>
		/// <returns>A string containing the name of the data provider.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00009519 File Offset: 0x00007719
		public string ProviderName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the SQL query in the associated <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> for selecting data from the underlying database.</summary>
		/// <returns>An SQL query.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00009519 File Offset: 0x00007719
		public string SelectCommand
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Indicates that this <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> supports select queries.</summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.DataSourceOperation.Select" />.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00016660 File Offset: 0x00014860
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00009519 File Offset: 0x00007719
		public DataSourceOperation SelectQuery
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return DataSourceOperation.Delete;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Indicates that this <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> supports update queries.</summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.DataSourceOperation.Update" />.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001667C File Offset: 0x0001487C
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00009519 File Offset: 0x00007719
		public DataSourceOperation UpdateQuery
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return DataSourceOperation.Delete;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.Design.WebControls.SqlDesignerDataSourceView" /> instance using the specified name.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.WebControls.SqlDesignerDataSourceView" /> object.</returns>
		/// <param name="viewName">The name of the view to create.</param>
		// Token: 0x06000B23 RID: 2851 RVA: 0x0000970B File Offset: 0x0000790B
		protected virtual SqlDesignerDataSourceView CreateView(string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Derives the parameters from the specified SQL command and adds corresponding <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the command's parameters collection.</summary>
		/// <param name="providerName">The name of the data provider.</param>
		/// <param name="command">A <see cref="T:System.Data.Common.DbCommand" /> object.</param>
		// Token: 0x06000B24 RID: 2852 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void DeriveParameters(string providerName, DbCommand command)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the connection string that is valid at design time for the control that is associated with this designer.</summary>
		/// <returns>A connection string.</returns>
		// Token: 0x06000B25 RID: 2853 RVA: 0x0000970B File Offset: 0x0000790B
		protected virtual string GetConnectionString()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns an array of parameters using the specified connection, command text, and command type.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
		/// <param name="connection">A <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" />  object</param>
		/// <param name="commandText">The text of the command.</param>
		/// <param name="commandType">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> indicating the type of command.</param>
		// Token: 0x06000B26 RID: 2854 RVA: 0x0000970B File Offset: 0x0000790B
		protected internal virtual Parameter[] InferParameterNames(DesignerDataConnection connection, string commandText, SqlDataSourceCommandType commandType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
