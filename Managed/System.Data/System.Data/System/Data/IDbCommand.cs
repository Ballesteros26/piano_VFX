using System;

namespace System.Data
{
	/// <summary>Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C9 RID: 201
	public interface IDbCommand : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B8F RID: 2959
		// (set) Token: 0x06000B90 RID: 2960
		IDbConnection Connection { get; set; }

		/// <summary>Gets or sets the transaction within which the Command object of a .NET Framework data provider executes.</summary>
		/// <returns>the Command object of a .NET Framework data provider executes. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B91 RID: 2961
		// (set) Token: 0x06000B92 RID: 2962
		IDbTransaction Transaction { get; set; }

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B93 RID: 2963
		// (set) Token: 0x06000B94 RID: 2964
		string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default value is 30 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The property value assigned is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B95 RID: 2965
		// (set) Token: 0x06000B96 RID: 2966
		int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.IDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B97 RID: 2967
		// (set) Token: 0x06000B98 RID: 2968
		CommandType CommandType { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000B99 RID: 2969
		IDataParameterCollection Parameters { get; }

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not <see cref="M:System.Data.OleDb.OleDbConnection.Open" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B9A RID: 2970
		void Prepare();

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B9B RID: 2971
		// (set) Token: 0x06000B9C RID: 2972
		UpdateRowSource UpdatedRowSource { get; set; }

		/// <summary>Attempts to cancels the execution of an <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B9D RID: 2973
		void Cancel();

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An IDbDataParameter object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B9E RID: 2974
		IDbDataParameter CreateParameter();

		/// <summary>Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B9F RID: 2975
		int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BA0 RID: 2976
		IDataReader ExecuteReader();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BA1 RID: 2977
		IDataReader ExecuteReader(CommandBehavior behavior);

		/// <summary>Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the resultset.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BA2 RID: 2978
		object ExecuteScalar();
	}
}
