using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for database-specific classes that represent commands. <see cref="Overload:System.Data.Common.DbCommand.ExecuteNonQueryAsync" /></summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200033E RID: 830
	public abstract class DbCommand : Component, IDbCommand, IDisposable
	{
		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060026AB RID: 9899
		// (set) Token: 0x060026AC RID: 9900
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public abstract string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060026AD RID: 9901
		// (set) Token: 0x060026AE RID: 9902
		public abstract int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060026AF RID: 9903
		// (set) Token: 0x060026B0 RID: 9904
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(CommandType.Text)]
		public abstract CommandType CommandType { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000ADF32 File Offset: 0x000AC132
		// (set) Token: 0x060026B2 RID: 9906 RVA: 0x000ADF3A File Offset: 0x000AC13A
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000ADF32 File Offset: 0x000AC132
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x000ADF43 File Offset: 0x000AC143
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = (DbConnection)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060026B5 RID: 9909
		// (set) Token: 0x060026B6 RID: 9910
		protected abstract DbConnection DbConnection { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060026B7 RID: 9911
		protected abstract DbParameterCollection DbParameterCollection { get; }

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060026B8 RID: 9912
		// (set) Token: 0x060026B9 RID: 9913
		protected abstract DbTransaction DbTransaction { get; set; }

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060026BA RID: 9914
		// (set) Token: 0x060026BB RID: 9915
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[DefaultValue(true)]
		public abstract bool DesignTimeVisible { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000ADF51 File Offset: 0x000AC151
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000ADF51 File Offset: 0x000AC151
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000ADF59 File Offset: 0x000AC159
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x000ADF61 File Offset: 0x000AC161
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000ADF59 File Offset: 0x000AC159
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x000ADF6A File Offset: 0x000AC16A
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = (DbTransaction)value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060026C2 RID: 9922
		// (set) Token: 0x060026C3 RID: 9923
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x060026C4 RID: 9924 RVA: 0x000ADF78 File Offset: 0x000AC178
		internal void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		/// <summary>Attempts to cancels the execution of a <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026C5 RID: 9925
		public abstract void Cancel();

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026C6 RID: 9926 RVA: 0x000ADFA0 File Offset: 0x000AC1A0
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An IDbDataParameter object.</returns>
		// Token: 0x060026C7 RID: 9927 RVA: 0x000ADFA0 File Offset: 0x000AC1A0
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x060026C8 RID: 9928
		protected abstract DbParameter CreateDbParameter();

		/// <summary>Executes the command text against the connection.</summary>
		/// <returns>A task representing the operation.</returns>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026C9 RID: 9929
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		/// <summary>Executes a SQL statement against a connection object.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026CA RID: 9930
		public abstract int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026CB RID: 9931 RVA: 0x000ADFA8 File Offset: 0x000AC1A8
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x060026CC RID: 9932 RVA: 0x000ADFA8 File Offset: 0x000AC1A8
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values. </summary>
		/// <returns>An <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026CD RID: 9933 RVA: 0x000ADFB1 File Offset: 0x000AC1B1
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x060026CE RID: 9934 RVA: 0x000ADFB1 File Offset: 0x000AC1B1
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />, which executes a SQL statement against a connection object.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteNonQueryAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060026CF RID: 9935 RVA: 0x000ADFBA File Offset: 0x000AC1BA
		public Task<int> ExecuteNonQueryAsync()
		{
			return this.ExecuteNonQueryAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token.  Exceptions thrown by <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbCommand object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060026D0 RID: 9936 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
		public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<int>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<int> task;
			try
			{
				task = Task.FromResult<int>(this.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026D1 RID: 9937 RVA: 0x000AE05C File Offset: 0x000AC25C
		public Task<DbDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />. This method propagates a notification that operations should be canceled.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026D2 RID: 9938 RVA: 0x000AE06A File Offset: 0x000AC26A
		public Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026D3 RID: 9939 RVA: 0x000AE074 File Offset: 0x000AC274
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		/// <summary>Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026D4 RID: 9940 RVA: 0x000AE082 File Offset: 0x000AC282
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteDbDataReaderAsync(behavior, cancellationToken);
		}

		/// <summary>Providers should implement this method to provide a non-default implementation for <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" /> overloads.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteReader" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteReader will be communicated via the returned Task Exception property.This method accepts a cancellation token that can be used to request the operation to be cancelled early. Implementations may ignore this request.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">Options for statement execution and data retrieval.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x060026D5 RID: 9941 RVA: 0x000AE08C File Offset: 0x000AC28C
		protected virtual Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<DbDataReader>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<DbDataReader> task;
			try
			{
				task = Task.FromResult<DbDataReader>(this.ExecuteReader(behavior));
			}
			catch (Exception ex)
			{
				task = Task.FromException<DbDataReader>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />, which executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060026D6 RID: 9942 RVA: 0x000AE120 File Offset: 0x000AC320
		public Task<object> ExecuteScalarAsync()
		{
			return this.ExecuteScalarAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteScalar will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbCommand object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060026D7 RID: 9943 RVA: 0x000AE130 File Offset: 0x000AC330
		public virtual Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<object>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<object> task;
			try
			{
				task = Task.FromResult<object>(this.ExecuteScalar());
			}
			catch (Exception ex)
			{
				task = Task.FromException<object>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026D8 RID: 9944
		public abstract object ExecuteScalar();

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026D9 RID: 9945
		public abstract void Prepare();
	}
}
