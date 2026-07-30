using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control and provides an interface for data-bound controls to perform SQL data operations against relational databases.</summary>
	// Token: 0x0200040F RID: 1039
	public class SqlDataSourceView : DataSourceView, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> class setting the specified <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control as the owner of the current view.</summary>
		/// <param name="owner">The data source control with which the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> is associated. </param>
		/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it. </param>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" />.</param>
		// Token: 0x06002E6F RID: 11887 RVA: 0x0007A5E0 File Offset: 0x000787E0
		public SqlDataSourceView(SqlDataSource owner, string name, HttpContext context)
			: base(owner, name)
		{
			this.owner = owner;
			this.name = name;
			this.context = context;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0007A648 File Offset: 0x00078848
		private void InitConnection()
		{
			if (this.factory == null)
			{
				this.factory = this.owner.GetDbProviderFactoryInternal();
			}
			if (this.connection == null)
			{
				this.connection = this.factory.CreateConnection();
				this.connection.ConnectionString = this.owner.ConnectionString;
			}
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteParameters" /> collection, and the values that are in the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row key values for the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation to delete.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated only if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanDelete" /> property is false. </exception>
		// Token: 0x06002E71 RID: 11889 RVA: 0x000720D2 File Offset: 0x000702D2
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteParameters" /> collection, and the values that are in the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row key values for the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation to delete.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated only if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed. </exception>
		/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to access to the database.- or -The instance of the control is an <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control and access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.AccessDataSource.DataFile" /> property.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanDelete" /> property is false. - or -A design-time relative path was not mapped correctly by the designer before using an instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control.</exception>
		// Token: 0x06002E72 RID: 11890 RVA: 0x0007A6A0 File Offset: 0x000788A0
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException("Delete operation is not supported");
			}
			if (oldValues == null && this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				throw new InvalidOperationException("oldValues parameters should be specified when ConflictOptions is set to CompareAllValues");
			}
			this.InitConnection();
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = this.DeleteCommand;
			dbCommand.Connection = this.connection;
			if (this.DeleteCommandType == SqlDataSourceCommandType.Text)
			{
				dbCommand.CommandType = CommandType.Text;
			}
			else
			{
				dbCommand.CommandType = CommandType.StoredProcedure;
			}
			IDictionary dictionary;
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				dictionary = new Hashtable();
				if (keys != null)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (oldValues == null)
				{
					goto IL_011E;
				}
				using (IDictionaryEnumerator dictionaryEnumerator = oldValues.GetEnumerator())
				{
					while (dictionaryEnumerator.MoveNext())
					{
						object obj2 = dictionaryEnumerator.Current;
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
					goto IL_011E;
				}
			}
			dictionary = keys;
			IL_011E:
			this.InitializeParameters(dbCommand, this.DeleteParameters, null, dictionary, true);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnDeleting(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return -1;
			}
			bool flag = this.connection.State == ConnectionState.Closed;
			if (flag)
			{
				this.connection.Open();
			}
			Exception ex = null;
			int num = -1;
			try
			{
				num = dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
			if (flag)
			{
				this.connection.Close();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, num, ex);
			this.OnDeleted(sqlDataSourceStatusEventArgs);
			if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw ex;
			}
			return num;
		}

		/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertParameters" /> collection, and the values that are in the specified <paramref name="values" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of parameters for the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property to use to perform the insert database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> is not a parameterized SQL query, pass null. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanInsert" /> property is false. </exception>
		// Token: 0x06002E73 RID: 11891 RVA: 0x000720DC File Offset: 0x000702DC
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertParameters" /> collection, and the values that are in the specified <paramref name="values" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values used with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property to perform the insert database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property is not a parameterized SQL query, pass null.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to gain access to the database.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanInsert" /> property is false. </exception>
		// Token: 0x06002E74 RID: 11892 RVA: 0x0007A88C File Offset: 0x00078A8C
		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException("Insert operation is not supported");
			}
			this.InitConnection();
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = this.InsertCommand;
			dbCommand.Connection = this.connection;
			if (this.InsertCommandType == SqlDataSourceCommandType.Text)
			{
				dbCommand.CommandType = CommandType.Text;
			}
			else
			{
				dbCommand.CommandType = CommandType.StoredProcedure;
			}
			this.InitializeParameters(dbCommand, this.InsertParameters, values, null, false);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnInserting(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return -1;
			}
			bool flag = this.connection.State == ConnectionState.Closed;
			if (flag)
			{
				this.connection.Open();
			}
			Exception ex = null;
			int num = -1;
			try
			{
				num = dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
			if (flag)
			{
				this.connection.Close();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			this.OnInserted(new SqlDataSourceStatusEventArgs(dbCommand, num, ex));
			if (ex != null)
			{
				throw ex;
			}
			return num;
		}

		/// <summary>Retrieves data from the underlying database using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="selectArgs" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002E75 RID: 11893 RVA: 0x000720BE File Offset: 0x000702BE
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		/// <summary>Retrieves data from the underlying database using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.- or -Caching is enabled but the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DataSourceMode" /> property of the data source is not set to <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataSet" />.- or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SortParameterName" /> property is set but <see cref="P:System.Data.SqlClient.SqlCommand.CommandType" /> is not set to <see cref="F:System.Data.CommandType.StoredProcedure" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The data source cannot create a database connection.- or -Caching is enabled but the internal cache and command types do not match.</exception>
		// Token: 0x06002E76 RID: 11894 RVA: 0x0007A984 File Offset: 0x00078B84
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			if (this.SortParameterName.Length > 0 && this.SelectCommandType == SqlDataSourceCommandType.Text)
			{
				throw new NotSupportedException("The SortParameterName property is only supported with stored procedure commands in SqlDataSource");
			}
			if (arguments.SortExpression.Length > 0 && this.owner.DataSourceMode == SqlDataSourceMode.DataReader)
			{
				throw new NotSupportedException("SqlDataSource cannot sort. Set DataSourceMode to DataSet to enable sorting.");
			}
			if (arguments.StartRowIndex > 0 || arguments.MaximumRows > 0)
			{
				throw new NotSupportedException("SqlDataSource does not have paging enabled. Set the DataSourceMode to DataSet to enable paging.");
			}
			if (this.FilterExpression.Length > 0 && this.owner.DataSourceMode == SqlDataSourceMode.DataReader)
			{
				throw new NotSupportedException("SqlDataSource only supports filtering when the data source's DataSourceMode is set to DataSet.");
			}
			this.InitConnection();
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = this.SelectCommand;
			dbCommand.Connection = this.connection;
			if (this.SelectCommandType == SqlDataSourceCommandType.Text)
			{
				dbCommand.CommandType = CommandType.Text;
			}
			else
			{
				dbCommand.CommandType = CommandType.StoredProcedure;
				if (this.SortParameterName.Length > 0 && arguments.SortExpression.Length > 0)
				{
					dbCommand.Parameters.Add(this.CreateDbParameter(this.SortParameterName, arguments.SortExpression));
				}
			}
			if (this.SelectParameters.Count > 0)
			{
				this.InitializeParameters(dbCommand, this.SelectParameters, null, null, false);
			}
			Exception ex = null;
			if (this.owner.DataSourceMode == SqlDataSourceMode.DataSet)
			{
				DataView dataView = null;
				if (this.owner.EnableCaching)
				{
					dataView = (DataView)this.owner.Cache.GetCachedObject(this.SelectCommand, this.SelectParameters);
				}
				if (dataView == null)
				{
					SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs = new SqlDataSourceSelectingEventArgs(dbCommand, arguments);
					this.OnSelecting(sqlDataSourceSelectingEventArgs);
					if (sqlDataSourceSelectingEventArgs.Cancel || !SqlDataSourceView.PrepareNullParameters(dbCommand, this.CancelSelectOnNullParameter))
					{
						return null;
					}
					try
					{
						DbDataAdapter dbDataAdapter = this.factory.CreateDataAdapter();
						DataSet dataSet = new DataSet();
						dbDataAdapter.SelectCommand = dbCommand;
						dbDataAdapter.Fill(dataSet, this.name);
						dataView = dataSet.Tables[0].DefaultView;
						if (dataView == null)
						{
							throw new InvalidOperationException();
						}
					}
					catch (Exception ex)
					{
					}
					int num = ((dataView == null) ? 0 : dataView.Count);
					SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, num, ex);
					this.OnSelected(sqlDataSourceStatusEventArgs);
					if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
					{
						throw ex;
					}
					if (this.owner.EnableCaching)
					{
						this.owner.Cache.SetCachedObject(this.SelectCommand, this.selectParameters, dataView);
					}
				}
				if (this.SortParameterName.Length == 0 || this.SelectCommandType == SqlDataSourceCommandType.Text)
				{
					dataView.Sort = arguments.SortExpression;
				}
				if (this.FilterExpression.Length > 0)
				{
					IOrderedDictionary values = this.FilterParameters.GetValues(this.context, this.owner);
					SqlDataSourceFilteringEventArgs sqlDataSourceFilteringEventArgs = new SqlDataSourceFilteringEventArgs(values);
					this.OnFiltering(sqlDataSourceFilteringEventArgs);
					if (!sqlDataSourceFilteringEventArgs.Cancel)
					{
						object[] array = new object[values.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = values[i];
							if (array[i] == null)
							{
								return dataView;
							}
						}
						dataView.RowFilter = string.Format(this.FilterExpression, array);
					}
				}
				return dataView;
			}
			SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs2 = new SqlDataSourceSelectingEventArgs(dbCommand, arguments);
			this.OnSelecting(sqlDataSourceSelectingEventArgs2);
			if (sqlDataSourceSelectingEventArgs2.Cancel || !SqlDataSourceView.PrepareNullParameters(dbCommand, this.CancelSelectOnNullParameter))
			{
				return null;
			}
			DbDataReader dbDataReader = null;
			bool flag = this.connection.State == ConnectionState.Closed;
			if (flag)
			{
				this.connection.Open();
			}
			try
			{
				dbDataReader = dbCommand.ExecuteReader(flag ? CommandBehavior.CloseConnection : CommandBehavior.Default);
			}
			catch (Exception ex)
			{
			}
			int num2 = ((dbDataReader == null) ? 0 : dbDataReader.RecordsAffected);
			SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs2 = new SqlDataSourceStatusEventArgs(dbCommand, num2, ex);
			this.OnSelected(sqlDataSourceStatusEventArgs2);
			if (ex != null && !sqlDataSourceStatusEventArgs2.ExceptionHandled)
			{
				throw ex;
			}
			return dbDataReader;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0007AD2C File Offset: 0x00078F2C
		private static bool PrepareNullParameters(DbCommand command, bool cancelIfHas)
		{
			for (int i = 0; i < command.Parameters.Count; i++)
			{
				DbParameter dbParameter = command.Parameters[i];
				if (dbParameter.Value == null && (dbParameter.Direction & ParameterDirection.Input) != (ParameterDirection)0)
				{
					if (cancelIfHas)
					{
						return false;
					}
					dbParameter.Value = DBNull.Value;
				}
			}
			return true;
		}

		/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> SQL string, any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateParameters" /> collection, and the values that are in the specified <paramref name="keys" />, <paramref name="values" />, and <paramref name="oldValues" /> collections.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no keys associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass null.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass null. </param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that represents the original values in the database. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass null.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanUpdate" /> property is false. </exception>
		// Token: 0x06002E78 RID: 11896 RVA: 0x000720C7 File Offset: 0x000702C7
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> SQL string, any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateParameters" /> collection, and the values that are in the specified <paramref name="keys" />, <paramref name="values" />, and <paramref name="oldValues" /> collections.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no keys associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is not a parameterized SQL query, pass null.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass null. </param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that represents the original values in the database. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is not a parameterized SQL query, pass null.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed.</exception>
		/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to gain access to the database.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanUpdate" /> property is false. </exception>
		// Token: 0x06002E79 RID: 11897 RVA: 0x0007AD80 File Offset: 0x00078F80
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException("Update operation is not supported");
			}
			if (oldValues == null && this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				throw new InvalidOperationException("oldValues parameters should be specified when ConflictOptions is set to CompareAllValues");
			}
			this.InitConnection();
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = this.UpdateCommand;
			dbCommand.Connection = this.connection;
			if (this.UpdateCommandType == SqlDataSourceCommandType.Text)
			{
				dbCommand.CommandType = CommandType.Text;
			}
			else
			{
				dbCommand.CommandType = CommandType.StoredProcedure;
			}
			IDictionary dictionary;
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				dictionary = new OrderedDictionary();
				if (keys != null)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (oldValues == null)
				{
					goto IL_011E;
				}
				using (IDictionaryEnumerator dictionaryEnumerator = oldValues.GetEnumerator())
				{
					while (dictionaryEnumerator.MoveNext())
					{
						object obj2 = dictionaryEnumerator.Current;
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
					goto IL_011E;
				}
			}
			dictionary = keys;
			IL_011E:
			this.InitializeParameters(dbCommand, this.UpdateParameters, values, dictionary, this.ConflictDetection == ConflictOptions.OverwriteChanges);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnUpdating(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return -1;
			}
			bool flag = this.connection.State == ConnectionState.Closed;
			if (flag)
			{
				this.connection.Open();
			}
			Exception ex = null;
			int num = -1;
			try
			{
				num = dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
			if (flag)
			{
				this.connection.Close();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, num, ex);
			this.OnUpdated(sqlDataSourceStatusEventArgs);
			if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw ex;
			}
			return num;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0007AF78 File Offset: 0x00079178
		private string FormatOldParameter(string name)
		{
			string text = this.OldValuesParameterFormatString;
			if (text.Length > 0)
			{
				return string.Format(text, name);
			}
			return name;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0007AFA0 File Offset: 0x000791A0
		private object FindValueByName(string parameterName, IDictionary values, bool format)
		{
			if (values == null)
			{
				return null;
			}
			foreach (object obj in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (format ? this.FormatOldParameter(dictionaryEntry.Key.ToString()) : dictionaryEntry.Key.ToString());
				if (string.Compare(parameterName, text, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return values[dictionaryEntry.Key];
				}
			}
			return null;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0007B038 File Offset: 0x00079238
		private void InitializeParameters(DbCommand command, ParameterCollection parameters, IDictionary values, IDictionary oldValues, bool parametersMayMatchOldValues)
		{
			IOrderedDictionary values2 = parameters.GetValues(this.context, this.owner);
			foreach (object obj in values2.Keys)
			{
				string text = (string)obj;
				Parameter parameter = parameters[text];
				object obj2 = this.FindValueByName(text, values, false);
				string text2 = text;
				if (obj2 == null)
				{
					obj2 = this.FindValueByName(text, oldValues, true);
				}
				if (obj2 == null && parametersMayMatchOldValues)
				{
					obj2 = this.FindValueByName(text, oldValues, false);
					text2 = this.FormatOldParameter(text);
				}
				if (obj2 != null)
				{
					object obj3 = parameter.ConvertValue(obj2);
					DbParameter dbParameter = this.CreateDbParameter(text2, obj3, parameter.Direction, parameter.Size);
					if (!command.Parameters.Contains(dbParameter.ParameterName))
					{
						command.Parameters.Add(dbParameter);
					}
				}
				else
				{
					command.Parameters.Add(this.CreateDbParameter(parameter.Name, values2[text], parameter.Direction, parameter.Size));
				}
			}
			if (values != null)
			{
				foreach (object obj4 in values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj4;
					if (!command.Parameters.Contains(this.ParameterPrefix + (string)dictionaryEntry.Key))
					{
						command.Parameters.Add(this.CreateDbParameter((string)dictionaryEntry.Key, dictionaryEntry.Value));
					}
				}
			}
			if (oldValues != null)
			{
				foreach (object obj5 in oldValues)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj5;
					if (!command.Parameters.Contains(this.ParameterPrefix + this.FormatOldParameter((string)dictionaryEntry2.Key)))
					{
						command.Parameters.Add(this.CreateDbParameter(this.FormatOldParameter((string)dictionaryEntry2.Key), dictionaryEntry2.Value));
					}
				}
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0007B294 File Offset: 0x00079494
		private DbParameter CreateDbParameter(string name, object value)
		{
			return this.CreateDbParameter(name, value, ParameterDirection.Input, -1);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0007B2A0 File Offset: 0x000794A0
		private DbParameter CreateDbParameter(string name, object value, ParameterDirection dir, int size)
		{
			DbParameter dbParameter = this.factory.CreateParameter();
			dbParameter.ParameterName = this.ParameterPrefix + name;
			dbParameter.Value = value;
			dbParameter.Direction = dir;
			if (size != -1)
			{
				dbParameter.Size = size;
			}
			return dbParameter;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> state to restore.</param>
		// Token: 0x06002E7F RID: 11903 RVA: 0x0007B2E7 File Offset: 0x000794E7
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> view state; otherwise, null, if there is no view state associated with the object.</returns>
		// Token: 0x06002E80 RID: 11904 RVA: 0x0007B2F0 File Offset: 0x000794F0
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x06002E81 RID: 11905 RVA: 0x0007B2F8 File Offset: 0x000794F8
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0007B300 File Offset: 0x00079500
		private NotSupportedException CreateNotSupportedException(string capabilityName)
		{
			return new NotSupportedException("Data source does not have the '" + capabilityName + "' capability enabled.");
		}

		/// <summary>Compares the capabilities that are requested for an <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the view supports and is called by the <see cref="M:System.Web.UI.DataSourceSelectArguments.RaiseUnsupportedCapabilitiesError(System.Web.UI.DataSourceView)" /> method.</summary>
		/// <param name="capability">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values that is compared against the capabilities that the view supports.</param>
		/// <exception cref="T:System.NotSupportedException">The data source does not have the selected <paramref name="capability" /> enabled.</exception>
		// Token: 0x06002E83 RID: 11907 RVA: 0x0007B318 File Offset: 0x00079518
		protected internal override void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
		{
			if ((capability & DataSourceCapabilities.Sort) != DataSourceCapabilities.None && !this.CanSort)
			{
				throw this.CreateNotSupportedException("Sort");
			}
			if ((capability & DataSourceCapabilities.Page) != DataSourceCapabilities.None && !this.CanPage)
			{
				throw this.CreateNotSupportedException("Page");
			}
			if ((capability & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None && !this.CanRetrieveTotalRowCount)
			{
				throw this.CreateNotSupportedException("RetrieveTotalRowCount");
			}
		}

		/// <summary>Restores the previously saved view state for the data source view.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> state to restore. </param>
		// Token: 0x06002E84 RID: 11908 RVA: 0x0007B370 File Offset: 0x00079570
		protected virtual void LoadViewState(object savedState)
		{
			object[] array = savedState as object[];
			if (array == null)
			{
				return;
			}
			if (array[0] != null)
			{
				((IStateManager)this.deleteParameters).LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				((IStateManager)this.filterParameters).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.insertParameters).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.selectParameters).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.updateParameters).LoadViewState(array[4]);
			}
		}

		/// <summary>Saves the changes to the view state for the  <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> control since the time that the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> view state; otherwise, null, if there is no view state associated with the object.</returns>
		// Token: 0x06002E85 RID: 11909 RVA: 0x0007B3E8 File Offset: 0x000795E8
		protected virtual object SaveViewState()
		{
			object[] array = new object[5];
			if (this.deleteParameters != null)
			{
				array[0] = ((IStateManager)this.deleteParameters).SaveViewState();
			}
			if (this.filterParameters != null)
			{
				array[1] = ((IStateManager)this.filterParameters).SaveViewState();
			}
			if (this.insertParameters != null)
			{
				array[2] = ((IStateManager)this.insertParameters).SaveViewState();
			}
			if (this.selectParameters != null)
			{
				array[3] = ((IStateManager)this.selectParameters).SaveViewState();
			}
			if (this.updateParameters != null)
			{
				array[4] = ((IStateManager)this.updateParameters).SaveViewState();
			}
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control and persisted across requests for the same page.</summary>
		// Token: 0x06002E86 RID: 11910 RVA: 0x0007B482 File Offset: 0x00079682
		protected virtual void TrackViewState()
		{
			this.tracking = true;
			if (this.filterParameters != null)
			{
				((IStateManager)this.filterParameters).TrackViewState();
			}
			if (this.selectParameters != null)
			{
				((IStateManager)this.selectParameters).TrackViewState();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x0007B4B1 File Offset: 0x000796B1
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Gets or sets a value indicating whether a data retrieval operation is canceled when any parameter that is contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection evaluates to null.</summary>
		/// <returns>true, if a data retrieval operation is canceled when a parameter contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection evaluated to null; otherwise, false. The default is true.</returns>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0007B4B9 File Offset: 0x000796B9
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x0007B4C1 File Offset: 0x000796C1
		public bool CancelSelectOnNullParameter
		{
			get
			{
				return this.cancelSelectOnNullParameter;
			}
			set
			{
				if (this.CancelSelectOnNullParameter == value)
				{
					return;
				}
				this.cancelSelectOnNullParameter = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the delete operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x0007B4DF File Offset: 0x000796DF
		public override bool CanDelete
		{
			get
			{
				return this.DeleteCommand != null && this.DeleteCommand != "";
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the insert operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false.</returns>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x0007B4FB File Offset: 0x000796FB
		public override bool CanInsert
		{
			get
			{
				return this.InsertCommand != null && this.InsertCommand != "";
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the paging of retrieved data.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanPage
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports retrieving the total number of data rows, in addition to the set of data.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports a sorted view on the retrieved data.</summary>
		/// <returns>true, if sorting is supported; otherwise, false.</returns>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x0007B517 File Offset: 0x00079717
		public override bool CanSort
		{
			get
			{
				return this.owner.DataSourceMode == SqlDataSourceMode.DataSet || (this.SortParameterName != null && this.SortParameterName != "");
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the update operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x0007B543 File Offset: 0x00079743
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateCommand != null && this.UpdateCommand != "";
			}
		}

		/// <summary>Gets or sets the value indicating how the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control performs updates and deletes when data in a row in the underlying database changes during the time of the operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.ConflictOptions" /> values.</exception>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x0007B55F File Offset: 0x0007975F
		// (set) Token: 0x06002E91 RID: 11921 RVA: 0x0007B567 File Offset: 0x00079767
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.conflictDetection;
			}
			set
			{
				if (this.ConflictDetection == value)
				{
					return;
				}
				this.conflictDetection = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to delete data from the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to delete data.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x0007B585 File Offset: 0x00079785
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x0007B58D File Offset: 0x0007978D
		public string DeleteCommand
		{
			get
			{
				return this.deleteCommand;
			}
			set
			{
				this.deleteCommand = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x0007B596 File Offset: 0x00079796
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x0007B59E File Offset: 0x0007979E
		public SqlDataSourceCommandType DeleteCommandType
		{
			get
			{
				return this.deleteCommandType;
			}
			set
			{
				this.deleteCommandType = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x0007B5A7 File Offset: 0x000797A7
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.GetParameterCollection(ref this.deleteParameters, false, false);
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Select" /> method is called.</summary>
		/// <returns>A string that represents a filtering expression applied when data is retrieved using the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> property was set when the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> is in the <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataReader" /> mode. </exception>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x0007B5B7 File Offset: 0x000797B7
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x0007B5C8 File Offset: 0x000797C8
		public string FilterExpression
		{
			get
			{
				return this.filterExpression ?? string.Empty;
			}
			set
			{
				if (this.FilterExpression == value)
				{
					return;
				}
				this.filterExpression = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> property.</returns>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x0007B5EB File Offset: 0x000797EB
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.GetParameterCollection(ref this.filterParameters, true, true);
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to insert data into the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to insert data.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x0007B5FB File Offset: 0x000797FB
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x0007B603 File Offset: 0x00079803
		public string InsertCommand
		{
			get
			{
				return this.insertCommand;
			}
			set
			{
				this.insertCommand = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The value is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x0007B60C File Offset: 0x0007980C
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x0007B614 File Offset: 0x00079814
		public SqlDataSourceCommandType InsertCommandType
		{
			get
			{
				return this.insertCommandType;
			}
			set
			{
				this.insertCommandType = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property.</returns>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x0007B61D File Offset: 0x0007981D
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.GetParameterCollection(ref this.insertParameters, false, false);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object is saving changes to its view state.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x0007B62D File Offset: 0x0007982D
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of any parameters that are passed to the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Update" /> method. </summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> parameters passed to the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Update" /> methods. The default is "{0}".</returns>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x0007B635 File Offset: 0x00079835
		// (set) Token: 0x06002EA1 RID: 11937 RVA: 0x0007B63D File Offset: 0x0007983D
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.oldValuesParameterFormatString;
			}
			set
			{
				if (this.OldValuesParameterFormatString == value)
				{
					return;
				}
				this.oldValuesParameterFormatString = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to retrieve data from the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to retrieve data.</returns>
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x0007B660 File Offset: 0x00079860
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x0007B676 File Offset: 0x00079876
		public string SelectCommand
		{
			get
			{
				if (this.selectCommand == null)
				{
					return string.Empty;
				}
				return this.selectCommand;
			}
			set
			{
				if (this.SelectCommand == value)
				{
					return;
				}
				this.selectCommand = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property is a SQL query or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x0007B699 File Offset: 0x00079899
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x0007B6A1 File Offset: 0x000798A1
		public SqlDataSourceCommandType SelectCommandType
		{
			get
			{
				return this.selectCommandType;
			}
			set
			{
				this.selectCommandType = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property.</returns>
		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x0007B6AA File Offset: 0x000798AA
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.GetParameterCollection(ref this.selectParameters, true, true);
			}
		}

		/// <summary>Gets or sets the name of a stored procedure parameter that is used to sort retrieved data when data retrieval is performed using a stored procedure.</summary>
		/// <returns>The name of a stored procedure parameter used to sort retrieved data when data retrieval is performed using a stored procedure.</returns>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x0007B6BA File Offset: 0x000798BA
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x0007B6C2 File Offset: 0x000798C2
		public string SortParameterName
		{
			get
			{
				return this.sortParameterName;
			}
			set
			{
				if (this.SortParameterName == value)
				{
					return;
				}
				this.sortParameterName = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to update data in the underlying database.</summary>
		/// <returns>A SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to update data.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x0007B6E5 File Offset: 0x000798E5
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x0007B6ED File Offset: 0x000798ED
		public string UpdateCommand
		{
			get
			{
				return this.updateCommand;
			}
			set
			{
				this.updateCommand = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x0007B6F6 File Offset: 0x000798F6
		// (set) Token: 0x06002EAC RID: 11948 RVA: 0x0007B6FE File Offset: 0x000798FE
		public SqlDataSourceCommandType UpdateCommandType
		{
			get
			{
				return this.updateCommandType;
			}
			set
			{
				this.updateCommandType = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property.</returns>
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x0007B707 File Offset: 0x00079907
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.GetParameterCollection(ref this.updateParameters, false, false);
			}
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00032D64 File Offset: 0x00030F64
		private void ParametersChanged(object source, EventArgs args)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0007B717 File Offset: 0x00079917
		private ParameterCollection GetParameterCollection(ref ParameterCollection output, bool propagateTrackViewState, bool subscribeChanged)
		{
			if (output != null)
			{
				return output;
			}
			output = new ParameterCollection();
			if (subscribeChanged)
			{
				output.ParametersChanged += this.ParametersChanged;
			}
			if (this.IsTrackingViewState && propagateTrackViewState)
			{
				((IStateManager)output).TrackViewState();
			}
			return output;
		}

		/// <summary>Gets the string that is used to prefix a parameter placeholder in a parameterized SQL query.</summary>
		/// <returns>The "@" string.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0007B750 File Offset: 0x00079950
		protected virtual string ParameterPrefix
		{
			get
			{
				string providerName = this.owner.ProviderName;
				if ((providerName != null && providerName.Length == 0) || providerName == "System.Data.SqlClient")
				{
					return "@";
				}
				if (!(providerName == "System.Data.OracleClient"))
				{
					return "";
				}
				return ":";
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Deleted" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed a delete operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EB1 RID: 11953 RVA: 0x0007B7A4 File Offset: 0x000799A4
		protected virtual void OnDeleted(SqlDataSourceStatusEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventDeleted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		/// <summary>Occurs when a delete operation has completed.</summary>
		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06002EB2 RID: 11954 RVA: 0x0007B7DB File Offset: 0x000799DB
		// (remove) Token: 0x06002EB3 RID: 11955 RVA: 0x0007B7EE File Offset: 0x000799EE
		public event SqlDataSourceStatusEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventDeleted, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Deleting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts a delete operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EB4 RID: 11956 RVA: 0x0007B804 File Offset: 0x00079A04
		protected virtual void OnDeleting(SqlDataSourceCommandEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventDeleting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs before a delete operation.</summary>
		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06002EB5 RID: 11957 RVA: 0x0007B83B File Offset: 0x00079A3B
		// (remove) Token: 0x06002EB6 RID: 11958 RVA: 0x0007B84E File Offset: 0x00079A4E
		public event SqlDataSourceCommandEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventDeleting, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Filtering" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control filters the results of a select operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceFilteringEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EB7 RID: 11959 RVA: 0x0007B864 File Offset: 0x00079A64
		protected virtual void OnFiltering(SqlDataSourceFilteringEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceFilteringEventHandler sqlDataSourceFilteringEventHandler = base.Events[SqlDataSourceView.EventFiltering] as SqlDataSourceFilteringEventHandler;
			if (sqlDataSourceFilteringEventHandler != null)
			{
				sqlDataSourceFilteringEventHandler(this, e);
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06002EB8 RID: 11960 RVA: 0x0007B89B File Offset: 0x00079A9B
		// (remove) Token: 0x06002EB9 RID: 11961 RVA: 0x0007B8AE File Offset: 0x00079AAE
		public event SqlDataSourceFilteringEventHandler Filtering
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventFiltering, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventFiltering, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Inserted" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed an insert operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EBA RID: 11962 RVA: 0x0007B8C4 File Offset: 0x00079AC4
		protected virtual void OnInserted(SqlDataSourceStatusEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventInserted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		/// <summary>Occurs when an insert operation has completed.</summary>
		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06002EBB RID: 11963 RVA: 0x0007B8FB File Offset: 0x00079AFB
		// (remove) Token: 0x06002EBC RID: 11964 RVA: 0x0007B90E File Offset: 0x00079B0E
		public event SqlDataSourceStatusEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventInserted, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Inserting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts an insert operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EBD RID: 11965 RVA: 0x0007B924 File Offset: 0x00079B24
		protected virtual void OnInserting(SqlDataSourceCommandEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventInserting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs before an insert operation.</summary>
		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06002EBE RID: 11966 RVA: 0x0007B95B File Offset: 0x00079B5B
		// (remove) Token: 0x06002EBF RID: 11967 RVA: 0x0007B96E File Offset: 0x00079B6E
		public event SqlDataSourceCommandEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventInserting, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Selected" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed a data retrieval operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EC0 RID: 11968 RVA: 0x0007B984 File Offset: 0x00079B84
		protected virtual void OnSelected(SqlDataSourceStatusEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventSelected] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		/// <summary>Occurs when a data retrieval operation has completed.</summary>
		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06002EC1 RID: 11969 RVA: 0x0007B9BB File Offset: 0x00079BBB
		// (remove) Token: 0x06002EC2 RID: 11970 RVA: 0x0007B9CE File Offset: 0x00079BCE
		public event SqlDataSourceStatusEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventSelected, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventSelected, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Selecting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts a data retrieval operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x0007B9E4 File Offset: 0x00079BE4
		protected virtual void OnSelecting(SqlDataSourceSelectingEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceSelectingEventHandler sqlDataSourceSelectingEventHandler = base.Events[SqlDataSourceView.EventSelecting] as SqlDataSourceSelectingEventHandler;
			if (sqlDataSourceSelectingEventHandler != null)
			{
				sqlDataSourceSelectingEventHandler(this, e);
			}
		}

		/// <summary>Occurs before a data retrieval operation.</summary>
		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06002EC4 RID: 11972 RVA: 0x0007BA1B File Offset: 0x00079C1B
		// (remove) Token: 0x06002EC5 RID: 11973 RVA: 0x0007BA2E File Offset: 0x00079C2E
		public event SqlDataSourceSelectingEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventSelecting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventSelecting, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Updated" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed an update operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EC6 RID: 11974 RVA: 0x0007BA44 File Offset: 0x00079C44
		protected virtual void OnUpdated(SqlDataSourceStatusEventArgs e)
		{
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventUpdated] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		/// <summary>Occurs when an update operation has completed.</summary>
		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06002EC7 RID: 11975 RVA: 0x0007BA98 File Offset: 0x00079C98
		// (remove) Token: 0x06002EC8 RID: 11976 RVA: 0x0007BAAB File Offset: 0x00079CAB
		public event SqlDataSourceStatusEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventUpdated, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Updating" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts an update operation.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EC9 RID: 11977 RVA: 0x0007BAC0 File Offset: 0x00079CC0
		protected virtual void OnUpdating(SqlDataSourceCommandEventArgs e)
		{
			if (!base.HasEvents())
			{
				return;
			}
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventUpdating] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs before an update operation.</summary>
		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06002ECA RID: 11978 RVA: 0x0007BAF7 File Offset: 0x00079CF7
		// (remove) Token: 0x06002ECB RID: 11979 RVA: 0x0007BB0A File Offset: 0x00079D0A
		public event SqlDataSourceCommandEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventUpdating, value);
			}
		}

		// Token: 0x04001BA7 RID: 7079
		private HttpContext context;

		// Token: 0x04001BA8 RID: 7080
		private DbProviderFactory factory;

		// Token: 0x04001BA9 RID: 7081
		private DbConnection connection;

		// Token: 0x04001BAA RID: 7082
		private bool cancelSelectOnNullParameter = true;

		// Token: 0x04001BAB RID: 7083
		private ConflictOptions conflictDetection;

		// Token: 0x04001BAC RID: 7084
		private string deleteCommand = string.Empty;

		// Token: 0x04001BAD RID: 7085
		private SqlDataSourceCommandType deleteCommandType;

		// Token: 0x04001BAE RID: 7086
		private string filterExpression;

		// Token: 0x04001BAF RID: 7087
		private string insertCommand = string.Empty;

		// Token: 0x04001BB0 RID: 7088
		private SqlDataSourceCommandType insertCommandType;

		// Token: 0x04001BB1 RID: 7089
		private string oldValuesParameterFormatString = "{0}";

		// Token: 0x04001BB2 RID: 7090
		private string selectCommand;

		// Token: 0x04001BB3 RID: 7091
		private SqlDataSourceCommandType selectCommandType;

		// Token: 0x04001BB4 RID: 7092
		private string sortParameterName = string.Empty;

		// Token: 0x04001BB5 RID: 7093
		private string updateCommand = string.Empty;

		// Token: 0x04001BB6 RID: 7094
		private SqlDataSourceCommandType updateCommandType;

		// Token: 0x04001BB7 RID: 7095
		private ParameterCollection deleteParameters;

		// Token: 0x04001BB8 RID: 7096
		private ParameterCollection filterParameters;

		// Token: 0x04001BB9 RID: 7097
		private ParameterCollection insertParameters;

		// Token: 0x04001BBA RID: 7098
		private ParameterCollection selectParameters;

		// Token: 0x04001BBB RID: 7099
		private ParameterCollection updateParameters;

		// Token: 0x04001BBC RID: 7100
		private bool tracking;

		// Token: 0x04001BBD RID: 7101
		private string name;

		// Token: 0x04001BBE RID: 7102
		private SqlDataSource owner;

		// Token: 0x04001BBF RID: 7103
		private static readonly object EventDeleted = new object();

		// Token: 0x04001BC0 RID: 7104
		private static readonly object EventDeleting = new object();

		// Token: 0x04001BC1 RID: 7105
		private static readonly object EventFiltering = new object();

		// Token: 0x04001BC2 RID: 7106
		private static readonly object EventInserted = new object();

		// Token: 0x04001BC3 RID: 7107
		private static readonly object EventInserting = new object();

		// Token: 0x04001BC4 RID: 7108
		private static readonly object EventSelected = new object();

		// Token: 0x04001BC5 RID: 7109
		private static readonly object EventSelecting = new object();

		// Token: 0x04001BC6 RID: 7110
		private static readonly object EventUpdated = new object();

		// Token: 0x04001BC7 RID: 7111
		private static readonly object EventUpdating = new object();
	}
}
