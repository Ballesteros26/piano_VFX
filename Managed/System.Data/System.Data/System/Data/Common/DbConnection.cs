using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Common
{
	/// <summary>Represents a connection to a database. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000340 RID: 832
	public abstract class DbConnection : Component, IDbConnection, IDisposable
	{
		/// <summary>Gets or sets the string used to open the connection.</summary>
		/// <returns>The connection string used to establish the initial connection. The exact contents of the connection string depend on the specific data source for this connection. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060026E0 RID: 9952
		// (set) Token: 0x060026E1 RID: 9953
		[RecommendedAsConfigurable(true)]
		[SettingsBindable(true)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract string ConnectionString { get; set; }

		/// <summary>Gets the time to wait while establishing a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is determined by the specific type of connection that you are using.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000AE1DD File Offset: 0x000AC3DD
		public virtual int ConnectionTimeout
		{
			get
			{
				return 15;
			}
		}

		/// <summary>Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060026E3 RID: 9955
		public abstract string Database { get; }

		/// <summary>Gets the name of the database server to which to connect.</summary>
		/// <returns>The name of the database server to which to connect. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060026E4 RID: 9956
		public abstract string DataSource { get; }

		/// <summary>Gets the <see cref="T:System.Data.Common.DbProviderFactory" /> for this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A set of methods for creating instances of a provider's implementation of the data source classes.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00004526 File Offset: 0x00002726
		protected virtual DbProviderFactory DbProviderFactory
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a string that represents the version of the server to which the object is connected.</summary>
		/// <returns>The version of the database. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Data.Common.DbConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="Overload:System.Data.Common.DbConnection.OpenAsync" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060026E6 RID: 9958
		[Browsable(false)]
		public abstract string ServerVersion { get; }

		/// <summary>Gets a string that describes the state of the connection.</summary>
		/// <returns>The state of the connection. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060026E7 RID: 9959
		[Browsable(false)]
		public abstract ConnectionState State { get; }

		/// <summary>Occurs when the state of the event changes.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060026E8 RID: 9960 RVA: 0x000AE1E4 File Offset: 0x000AC3E4
		// (remove) Token: 0x060026E9 RID: 9961 RVA: 0x000AE21C File Offset: 0x000AC41C
		public virtual event StateChangeEventHandler StateChange;

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		// Token: 0x060026EA RID: 9962
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026EB RID: 9963 RVA: 0x000AE251 File Offset: 0x000AC451
		public DbTransaction BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026EC RID: 9964 RVA: 0x000AE25A File Offset: 0x000AC45A
		public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Begins a database transaction.</summary>
		/// <returns>An object that represents the new transaction.</returns>
		// Token: 0x060026ED RID: 9965 RVA: 0x000AE251 File Offset: 0x000AC451
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object that represents the new transaction.</returns>
		/// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
		// Token: 0x060026EE RID: 9966 RVA: 0x000AE25A File Offset: 0x000AC45A
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.Common.DbException">The connection-level error that occurred while opening the connection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026EF RID: 9967
		public abstract void Close();

		/// <summary>Changes the current database for an open connection.</summary>
		/// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060026F0 RID: 9968
		public abstract void ChangeDatabase(string databaseName);

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026F1 RID: 9969 RVA: 0x000AE263 File Offset: 0x000AC463
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the connection.</returns>
		// Token: 0x060026F2 RID: 9970 RVA: 0x000AE263 File Offset: 0x000AC463
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		// Token: 0x060026F3 RID: 9971
		protected abstract DbCommand CreateDbCommand();

		/// <summary>Enlists in the specified transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x060026F4 RID: 9972 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060026F5 RID: 9973 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual DataTable GetSchema()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060026F6 RID: 9974 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060026F7 RID: 9975 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Raises the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
		/// <param name="stateChange">A <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x060026F8 RID: 9976 RVA: 0x000AE26B File Offset: 0x000AC46B
		protected virtual void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (this._suppressStateChangeForReconnection)
			{
				return;
			}
			StateChangeEventHandler stateChange2 = this.StateChange;
			if (stateChange2 == null)
			{
				return;
			}
			stateChange2(this, stateChange);
		}

		/// <summary>Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060026F9 RID: 9977
		public abstract void Open();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />, which opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />. This method invokes the virtual method <see cref="M:System.Data.Common.DbConnection.OpenAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		// Token: 0x060026FA RID: 9978 RVA: 0x000AE288 File Offset: 0x000AC488
		public Task OpenAsync()
		{
			return this.OpenAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />. Providers should override with an appropriate implementation. The cancellation token can optionally be honored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbConnection.Open" /> call and returns a completed task. The default implementation will return a cancelled task if passed an already cancelled cancellationToken. Exceptions thrown by Open will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbConnection object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		// Token: 0x060026FB RID: 9979 RVA: 0x000AE298 File Offset: 0x000AC498
		public virtual Task OpenAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Open();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000AE2E0 File Offset: 0x000AC4E0
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.DbProviderFactory;
			}
		}

		// Token: 0x040018AF RID: 6319
		internal bool _suppressStateChangeForReconnection;
	}
}
