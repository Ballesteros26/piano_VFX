using System;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control and provides an interface for data-bound controls to perform data retrieval using Structured Query Language (SQL) against a Microsoft Access database.</summary>
	// Token: 0x0200032F RID: 815
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AccessDataSourceView : SqlDataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> class setting the specified <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control as the owner of the current view.</summary>
		/// <param name="owner">The data source control with which the <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> is associated. </param>
		/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it. </param>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" />. </param>
		// Token: 0x06001C3C RID: 7228 RVA: 0x0004692D File Offset: 0x00044B2D
		public AccessDataSourceView(AccessDataSource owner, string name, HttpContext context)
			: base(owner, name, context)
		{
			this.dataSource = owner;
			this.oleConnection = new OleDbConnection(owner.ConnectionString);
		}

		/// <summary>Retrieves data from the underlying data storage using the SQL string in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property and any parameters in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.AccessDataSource.DataFile" /> property is null or an empty string ("").</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.AccessDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		// Token: 0x06001C3D RID: 7229 RVA: 0x00046950 File Offset: 0x00044B50
		[global::System.MonoTODO("Handle arguments")]
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			this.oleCommand = new OleDbCommand(base.SelectCommand, this.oleConnection);
			SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs = new SqlDataSourceSelectingEventArgs(this.oleCommand, arguments);
			this.OnSelecting(sqlDataSourceSelectingEventArgs);
			IEnumerable enumerable = null;
			Exception ex = null;
			OleDbDataReader oleDbDataReader = null;
			try
			{
				File.OpenRead(this.dataSource.DataFile).Close();
				this.oleConnection.Open();
				oleDbDataReader = this.oleCommand.ExecuteReader();
				throw new NotImplementedException("OleDbDataReader doesnt implements GetEnumerator method yet");
			}
			catch (Exception ex)
			{
			}
			SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(this.oleCommand, oleDbDataReader.RecordsAffected, ex);
			this.OnSelected(sqlDataSourceStatusEventArgs);
			if (ex != null)
			{
				throw ex;
			}
			return enumerable;
		}

		// Token: 0x040017E7 RID: 6119
		private OleDbConnection oleConnection;

		// Token: 0x040017E8 RID: 6120
		private OleDbCommand oleCommand;

		// Token: 0x040017E9 RID: 6121
		private AccessDataSource dataSource;
	}
}
