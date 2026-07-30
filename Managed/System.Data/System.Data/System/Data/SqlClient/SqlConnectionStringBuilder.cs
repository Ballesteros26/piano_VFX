using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.SqlClient.SqlConnection" /> class. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019A RID: 410
	public sealed class SqlConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		private static string[] CreateValidKeywords()
		{
			string[] array = new string[29];
			array[25] = "ApplicationIntent";
			array[20] = "Application Name";
			array[2] = "AttachDbFilename";
			array[14] = "Connect Timeout";
			array[21] = "Current Language";
			array[0] = "Data Source";
			array[15] = "Encrypt";
			array[8] = "Enlist";
			array[1] = "Failover Partner";
			array[3] = "Initial Catalog";
			array[4] = "Integrated Security";
			array[17] = "Load Balance Timeout";
			array[11] = "Max Pool Size";
			array[10] = "Min Pool Size";
			array[12] = "MultipleActiveResultSets";
			array[26] = "MultiSubnetFailover";
			array[18] = "Packet Size";
			array[7] = "Password";
			array[5] = "Persist Security Info";
			array[9] = "Pooling";
			array[13] = "Replication";
			array[24] = "Transaction Binding";
			array[16] = "TrustServerCertificate";
			array[19] = "Type System Version";
			array[6] = "User ID";
			array[23] = "User Instance";
			array[22] = "Workstation ID";
			array[27] = "ConnectRetryCount";
			array[28] = "ConnectRetryInterval";
			return array;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0005E7FC File Offset: 0x0005C9FC
		private static Dictionary<string, SqlConnectionStringBuilder.Keywords> CreateKeywordsDictionary()
		{
			return new Dictionary<string, SqlConnectionStringBuilder.Keywords>(47, StringComparer.OrdinalIgnoreCase)
			{
				{
					"ApplicationIntent",
					SqlConnectionStringBuilder.Keywords.ApplicationIntent
				},
				{
					"Application Name",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"AttachDbFilename",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"Connect Timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"Current Language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"Data Source",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"Encrypt",
					SqlConnectionStringBuilder.Keywords.Encrypt
				},
				{
					"Enlist",
					SqlConnectionStringBuilder.Keywords.Enlist
				},
				{
					"Failover Partner",
					SqlConnectionStringBuilder.Keywords.FailoverPartner
				},
				{
					"Initial Catalog",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"Integrated Security",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"Load Balance Timeout",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"MultipleActiveResultSets",
					SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets
				},
				{
					"Max Pool Size",
					SqlConnectionStringBuilder.Keywords.MaxPoolSize
				},
				{
					"Min Pool Size",
					SqlConnectionStringBuilder.Keywords.MinPoolSize
				},
				{
					"MultiSubnetFailover",
					SqlConnectionStringBuilder.Keywords.MultiSubnetFailover
				},
				{
					"Packet Size",
					SqlConnectionStringBuilder.Keywords.PacketSize
				},
				{
					"Password",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"Persist Security Info",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"Pooling",
					SqlConnectionStringBuilder.Keywords.Pooling
				},
				{
					"Replication",
					SqlConnectionStringBuilder.Keywords.Replication
				},
				{
					"Transaction Binding",
					SqlConnectionStringBuilder.Keywords.TransactionBinding
				},
				{
					"TrustServerCertificate",
					SqlConnectionStringBuilder.Keywords.TrustServerCertificate
				},
				{
					"Type System Version",
					SqlConnectionStringBuilder.Keywords.TypeSystemVersion
				},
				{
					"User ID",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"User Instance",
					SqlConnectionStringBuilder.Keywords.UserInstance
				},
				{
					"Workstation ID",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				},
				{
					"ConnectRetryCount",
					SqlConnectionStringBuilder.Keywords.ConnectRetryCount
				},
				{
					"ConnectRetryInterval",
					SqlConnectionStringBuilder.Keywords.ConnectRetryInterval
				},
				{
					"app",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"extended properties",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"initial file name",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"connection timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"addr",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"network address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"server",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"database",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"trusted_connection",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"connection lifetime",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"pwd",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"persistsecurityinfo",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"uid",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"user",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"wsid",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				}
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> class.</summary>
		// Token: 0x060012FE RID: 4862 RVA: 0x0005EA63 File Offset: 0x0005CC63
		public SqlConnectionStringBuilder()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into name/value pairs. Invalid key names raise <see cref="T:System.Collections.Generic.KeyNotFoundException" />.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Invalid key name within the connection string.</exception>
		/// <exception cref="T:System.FormatException">Invalid value within the connection string (specifically, when a Boolean or numeric value was expected but not supplied).</exception>
		/// <exception cref="T:System.ArgumentException">The supplied <paramref name="connectionString" /> is not valid.</exception>
		// Token: 0x060012FF RID: 4863 RVA: 0x0005EA6C File Offset: 0x0005CC6C
		public SqlConnectionStringBuilder(string connectionString)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key. In C#, this property is the indexer.</summary>
		/// <returns>The value associated with the specified key. </returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Tried to add a key that does not exist within the available keys.</exception>
		/// <exception cref="T:System.FormatException">Invalid value within the connection string (specifically, a Boolean or numeric value was expected but not supplied).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000376 RID: 886
		public override object this[string keyword]
		{
			get
			{
				SqlConnectionStringBuilder.Keywords index = this.GetIndex(keyword);
				return this.GetAt(index);
			}
			set
			{
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				switch (this.GetIndex(keyword))
				{
				case SqlConnectionStringBuilder.Keywords.DataSource:
					this.DataSource = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.FailoverPartner:
					this.FailoverPartner = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
					this.AttachDBFilename = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.InitialCatalog:
					this.InitialCatalog = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
					this.IntegratedSecurity = SqlConnectionStringBuilder.ConvertToIntegratedSecurity(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
					this.PersistSecurityInfo = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserID:
					this.UserID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Password:
					this.Password = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Enlist:
					this.Enlist = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Pooling:
					this.Pooling = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MinPoolSize:
					this.MinPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
					this.MaxPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
					this.MultipleActiveResultSets = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Replication:
					this.Replication = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
					this.ConnectTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Encrypt:
					this.Encrypt = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
					this.TrustServerCertificate = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
					this.LoadBalanceTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PacketSize:
					this.PacketSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
					this.TypeSystemVersion = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationName:
					this.ApplicationName = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
					this.CurrentLanguage = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.WorkstationID:
					this.WorkstationID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserInstance:
					this.UserInstance = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TransactionBinding:
					this.TransactionBinding = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
					this.ApplicationIntent = SqlConnectionStringBuilder.ConvertToApplicationIntent(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
					this.MultiSubnetFailover = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
					this.ConnectRetryCount = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
					this.ConnectRetryInterval = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				default:
					throw this.UnsupportedKeyword(keyword);
				}
			}
		}

		/// <summary>Declares the application workload type when connecting to a database in an SQL Server Availability Group. You can set the value of this property with <see cref="T:System.Data.SqlClient.ApplicationIntent" />. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.</summary>
		/// <returns>Returns the current value of the property (a value of type <see cref="T:System.Data.SqlClient.ApplicationIntent" />).</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0005ED80 File Offset: 0x0005CF80
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x0005ED88 File Offset: 0x0005CF88
		public ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)value);
				}
				this.SetApplicationIntentValue(value);
				this._applicationIntent = value;
			}
		}

		/// <summary>Gets or sets the name of the application associated with the connection string.</summary>
		/// <returns>The name of the application, or ".NET SqlClient Data Provider" if no name has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0005EDB1 File Offset: 0x0005CFB1
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x0005EDB9 File Offset: 0x0005CFB9
		public string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
			set
			{
				this.SetValue("Application Name", value);
				this._applicationName = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.</summary>
		/// <returns>The value of the AttachDBFilename property, or String.Empty if no value has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0005EDCE File Offset: 0x0005CFCE
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x0005EDD6 File Offset: 0x0005CFD6
		public string AttachDBFilename
		{
			get
			{
				return this._attachDBFilename;
			}
			set
			{
				this.SetValue("AttachDbFilename", value);
				this._attachDBFilename = value;
			}
		}

		/// <summary>Gets or sets the length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout" /> property, or 15 seconds if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0005EDEB File Offset: 0x0005CFEB
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x0005EDF3 File Offset: 0x0005CFF3
		public int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Connect Timeout");
				}
				this.SetValue("Connect Timeout", value);
				this._connectTimeout = value;
			}
		}

		/// <summary>Gets or sets the SQL Server Language record name.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.CurrentLanguage" /> property, or String.Empty if no value has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0005EE17 File Offset: 0x0005D017
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0005EE1F File Offset: 0x0005D01F
		public string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
			set
			{
				this.SetValue("Current Language", value);
				this._currentLanguage = value;
			}
		}

		/// <summary>Gets or sets the name or network address of the instance of SQL Server to connect to.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.DataSource" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0005EE34 File Offset: 0x0005D034
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0005EE3C File Offset: 0x0005D03C
		public string DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this.SetValue("Data Source", value);
				this._dataSource = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Encrypt" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0005EE51 File Offset: 0x0005D051
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x0005EE59 File Offset: 0x0005D059
		public bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
			set
			{
				this.SetValue("Encrypt", value);
				this._encrypt = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.</summary>
		/// <returns>A Boolean. Recognized values are true, false, yes, and no. </returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0005EE6E File Offset: 0x0005D06E
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0005EE76 File Offset: 0x0005D076
		public bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
			set
			{
				this.SetValue("TrustServerCertificate", value);
				this._trustServerCertificate = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread's current transaction context.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Enlist" /> property, or true if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0005EE8B File Offset: 0x0005D08B
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x0005EE93 File Offset: 0x0005D093
		public bool Enlist
		{
			get
			{
				return this._enlist;
			}
			set
			{
				this.SetValue("Enlist", value);
				this._enlist = value;
			}
		}

		/// <summary>Gets or sets the name or address of the partner server to connect to if the primary server is down.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.FailoverPartner" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
		public string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
			set
			{
				this.SetValue("Failover Partner", value);
				this._failoverPartner = value;
			}
		}

		/// <summary>Gets or sets the name of the database associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.InitialCatalog" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0005EEC5 File Offset: 0x0005D0C5
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0005EECD File Offset: 0x0005D0CD
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlInitialCatalogConverter))]
		public string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
			set
			{
				this.SetValue("Initial Catalog", value);
				this._initialCatalog = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.IntegratedSecurity" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0005EEE2 File Offset: 0x0005D0E2
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0005EEEA File Offset: 0x0005D0EA
		public bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
			set
			{
				this.SetValue("Integrated Security", value);
				this._integratedSecurity = value;
			}
		}

		/// <summary>Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.LoadBalanceTimeout" /> property, or 0 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0005EEFF File Offset: 0x0005D0FF
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x0005EF07 File Offset: 0x0005D107
		public int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Load Balance Timeout");
				}
				this.SetValue("Load Balance Timeout", value);
				this._loadBalanceTimeout = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed in the connection pool for this specific connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize" /> property, or 100 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0005EF2B File Offset: 0x0005D12B
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x0005EF33 File Offset: 0x0005D133
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
			set
			{
				if (value < 1)
				{
					throw ADP.InvalidConnectionOptionValue("Max Pool Size");
				}
				this.SetValue("Max Pool Size", value);
				this._maxPoolSize = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0005EF57 File Offset: 0x0005D157
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0005EF5F File Offset: 0x0005D15F
		public int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw ADP.InvalidConnectionOptionValue("ConnectRetryCount");
				}
				this.SetValue("ConnectRetryCount", value);
				this._connectRetryCount = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0005EF8B File Offset: 0x0005D18B
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0005EF93 File Offset: 0x0005D193
		public int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
			set
			{
				if (value < 1 || value > 60)
				{
					throw ADP.InvalidConnectionOptionValue("ConnectRetryInterval");
				}
				this.SetValue("ConnectRetryInterval", value);
				this._connectRetryInterval = value;
			}
		}

		/// <summary>Gets or sets the minimum number of connections allowed in the connection pool for this specific connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize" /> property, or 0 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x0005EFC4 File Offset: 0x0005D1C4
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Min Pool Size");
				}
				this.SetValue("Min Pool Size", value);
				this._minPoolSize = value;
			}
		}

		/// <summary>When true, an application can maintain multiple active result sets (MARS). When false, an application must process or cancel all result sets from one batch before it can execute any other batch on that connection.For more information, see Multiple Active Result Sets (MARS).</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MultipleActiveResultSets" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0005EFE8 File Offset: 0x0005D1E8
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0005EFF0 File Offset: 0x0005D1F0
		public bool MultipleActiveResultSets
		{
			get
			{
				return this._multipleActiveResultSets;
			}
			set
			{
				this.SetValue("MultipleActiveResultSets", value);
				this._multipleActiveResultSets = value;
			}
		}

		/// <summary>If your application is connecting to an AlwaysOn availability group (AG) on different subnets, setting MultiSubnetFailover=true provides faster detection of and connection to the (currently) active server. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" /> indicating the current value of the property.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0005F005 File Offset: 0x0005D205
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0005F00D File Offset: 0x0005D20D
		public bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
			set
			{
				this.SetValue("MultiSubnetFailover", value);
				this._multiSubnetFailover = value;
			}
		}

		/// <summary>Gets or sets the size in bytes of the network packets used to communicate with an instance of SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize" /> property, or 8000 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0005F022 File Offset: 0x0005D222
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0005F02A File Offset: 0x0005D22A
		public int PacketSize
		{
			get
			{
				return this._packetSize;
			}
			set
			{
				if (value < 512 || 32768 < value)
				{
					throw SQL.InvalidPacketSizeValue();
				}
				this.SetValue("Packet Size", value);
				this._packetSize = value;
			}
		}

		/// <summary>Gets or sets the password for the SQL Server account.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Password" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">The password was incorrectly set to null.  See code sample below.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0005F055 File Offset: 0x0005D255
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0005F05D File Offset: 0x0005D25D
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this.SetValue("Password", value);
				this._password = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.PersistSecurityInfo" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0005F072 File Offset: 0x0005D272
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0005F07A File Offset: 0x0005D27A
		public bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
			set
			{
				this.SetValue("Persist Security Info", value);
				this._persistSecurityInfo = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Pooling" /> property, or true if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0005F08F File Offset: 0x0005D28F
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x0005F097 File Offset: 0x0005D297
		public bool Pooling
		{
			get
			{
				return this._pooling;
			}
			set
			{
				this.SetValue("Pooling", value);
				this._pooling = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether replication is supported using the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Replication" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0005F0AC File Offset: 0x0005D2AC
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0005F0B4 File Offset: 0x0005D2B4
		public bool Replication
		{
			get
			{
				return this._replication;
			}
			set
			{
				this.SetValue("Replication", value);
				this._replication = value;
			}
		}

		/// <summary>Gets or sets a string value that indicates how the connection maintains its association with an enlisted System.Transactions transaction.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding" /> property, or String.Empty if none has been supplied.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0005F0C9 File Offset: 0x0005D2C9
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0005F0D1 File Offset: 0x0005D2D1
		public string TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
			set
			{
				this.SetValue("Transaction Binding", value);
				this._transactionBinding = value;
			}
		}

		/// <summary>Gets or sets a string value that indicates the type system the application expects.</summary>
		/// <returns>The following table shows the possible values for the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.TypeSystemVersion" /> property:ValueDescriptionSQL Server 2005Uses the SQL Server 2005 type system. No conversions are made for the current version of ADO.NET.SQL Server 2008Uses the SQL Server 2008 type system.LatestUse the latest version than this client-server pair can handle. This will automatically move forward as the client and server components are upgraded.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0005F0E6 File Offset: 0x0005D2E6
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x0005F0EE File Offset: 0x0005D2EE
		public string TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
			set
			{
				this.SetValue("Type System Version", value);
				this._typeSystemVersion = value;
			}
		}

		/// <summary>Gets or sets the user ID to be used when connecting to SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.UserID" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0005F103 File Offset: 0x0005D303
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x0005F10B File Offset: 0x0005D30B
		public string UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this.SetValue("User ID", value);
				this._userID = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to redirect the connection from the default SQL Server Express instance to a runtime-initiated instance running under the account of the caller.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.UserInstance" /> property, or False if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0005F120 File Offset: 0x0005D320
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x0005F128 File Offset: 0x0005D328
		public bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
			set
			{
				this.SetValue("User Instance", value);
				this._userInstance = value;
			}
		}

		/// <summary>Gets or sets the name of the workstation connecting to SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.WorkstationID" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0005F13D File Offset: 0x0005D33D
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x0005F145 File Offset: 0x0005D345
		public string WorkstationID
		{
			get
			{
				return this._workstationID;
			}
			set
			{
				this.SetValue("Workstation ID", value);
				this._workstationID = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0005F15A File Offset: 0x0005D35A
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(SqlConnectionStringBuilder.s_validKeywords);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0005F168 File Offset: 0x0005D368
		public override ICollection Values
		{
			get
			{
				object[] array = new object[SqlConnectionStringBuilder.s_validKeywords.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAt((SqlConnectionStringBuilder.Keywords)i);
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600133E RID: 4926 RVA: 0x0005F1A0 File Offset: 0x0005D3A0
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < SqlConnectionStringBuilder.s_validKeywords.Length; i++)
			{
				this.Reset((SqlConnectionStringBuilder.Keywords)i);
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600133F RID: 4927 RVA: 0x0005F1CC File Offset: 0x0005D3CC
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return SqlConnectionStringBuilder.s_keywords.ContainsKey(keyword);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005F1E4 File Offset: 0x0005D3E4
		private static bool ConvertToBoolean(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToBoolean(value);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0005F1EC File Offset: 0x0005D3EC
		private static int ConvertToInt32(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToInt32(value);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
		private static bool ConvertToIntegratedSecurity(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToIntegratedSecurity(value);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005F204 File Offset: 0x0005D404
		private static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToApplicationIntent(keyword, value);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0005F210 File Offset: 0x0005D410
		private object GetAt(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				return this.DataSource;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				return this.FailoverPartner;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				return this.AttachDBFilename;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				return this.InitialCatalog;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				return this.IntegratedSecurity;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				return this.PersistSecurityInfo;
			case SqlConnectionStringBuilder.Keywords.UserID:
				return this.UserID;
			case SqlConnectionStringBuilder.Keywords.Password:
				return this.Password;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				return this.Enlist;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				return this.Pooling;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				return this.MinPoolSize;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				return this.MaxPoolSize;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				return this.MultipleActiveResultSets;
			case SqlConnectionStringBuilder.Keywords.Replication:
				return this.Replication;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				return this.ConnectTimeout;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				return this.Encrypt;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				return this.TrustServerCertificate;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				return this.LoadBalanceTimeout;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				return this.PacketSize;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				return this.TypeSystemVersion;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				return this.ApplicationName;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				return this.CurrentLanguage;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				return this.WorkstationID;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				return this.UserInstance;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				return this.TransactionBinding;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				return this.ApplicationIntent;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				return this.MultiSubnetFailover;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				return this.ConnectRetryCount;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				return this.ConnectRetryInterval;
			default:
				throw this.UnsupportedKeyword(SqlConnectionStringBuilder.s_validKeywords[(int)index]);
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0005F3D0 File Offset: 0x0005D5D0
		private SqlConnectionStringBuilder.Keywords GetIndex(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
			{
				return keywords;
			}
			throw this.UnsupportedKeyword(keyword);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001347 RID: 4935 RVA: 0x0005F400 File Offset: 0x0005D600
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords) && base.Remove(SqlConnectionStringBuilder.s_validKeywords[(int)keywords]))
			{
				this.Reset(keywords);
				return true;
			}
			return false;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0005F440 File Offset: 0x0005D640
		private void Reset(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				this._dataSource = "";
				return;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				this._failoverPartner = "";
				return;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				this._attachDBFilename = "";
				return;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				this._initialCatalog = "";
				return;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				this._integratedSecurity = false;
				return;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				this._persistSecurityInfo = false;
				return;
			case SqlConnectionStringBuilder.Keywords.UserID:
				this._userID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Password:
				this._password = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				this._enlist = true;
				return;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				this._pooling = true;
				return;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				this._minPoolSize = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				this._maxPoolSize = 100;
				return;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				this._multipleActiveResultSets = false;
				return;
			case SqlConnectionStringBuilder.Keywords.Replication:
				this._replication = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				this._connectTimeout = 15;
				return;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				this._encrypt = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				this._trustServerCertificate = false;
				return;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				this._loadBalanceTimeout = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				this._packetSize = 8000;
				return;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				this._typeSystemVersion = "Latest";
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				this._applicationName = "Core .Net SqlClient Data Provider";
				return;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				this._currentLanguage = "";
				return;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				this._workstationID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				this._userInstance = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				this._transactionBinding = "Implicit Unbind";
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				this._applicationIntent = ApplicationIntent.ReadWrite;
				return;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				this._multiSubnetFailover = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				this._connectRetryCount = 1;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				this._connectRetryInterval = 10;
				return;
			default:
				throw this.UnsupportedKeyword(SqlConnectionStringBuilder.s_validKeywords[(int)index]);
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		private void SetValue(string keyword, bool value)
		{
			base[keyword] = value.ToString();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0005F604 File Offset: 0x0005D804
		private void SetValue(string keyword, int value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0005F615 File Offset: 0x0005D815
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0005F626 File Offset: 0x0005D826
		private void SetApplicationIntentValue(ApplicationIntent value)
		{
			base["ApplicationIntent"] = DbConnectionStringBuilderUtil.ApplicationIntentToString(value);
		}

		/// <summary>Indicates whether the specified key exists in this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134D RID: 4941 RVA: 0x0005F63C File Offset: 0x0005D83C
		public override bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			return SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords) && base.ShouldSerialize(SqlConnectionStringBuilder.s_validKeywords[(int)keywords]);
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string; otherwise, false.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to <paramref name="keyword." /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> contains a null value (Nothing in Visual Basic).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600134E RID: 4942 RVA: 0x0005F674 File Offset: 0x0005D874
		public override bool TryGetValue(string keyword, out object value)
		{
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
			{
				value = this.GetAt(keywords);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005F69F File Offset: 0x0005D89F
		private Exception UnsupportedKeyword(string keyword)
		{
			if (SqlConnectionStringBuilder.s_notSupportedKeywords.Contains(keyword, StringComparer.OrdinalIgnoreCase))
			{
				return SQL.UnsupportedKeyword(keyword);
			}
			if (SqlConnectionStringBuilder.s_notSupportedNetworkLibraryKeywords.Contains(keyword, StringComparer.OrdinalIgnoreCase))
			{
				return SQL.NetworkLibraryKeywordNotSupported();
			}
			return ADP.KeywordNotSupported(keyword);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether asynchronous processing is allowed by the connection created by using this connection string.</summary>
		/// <returns>CautionThis property is ignored beginning in .NET Framework 4.5. For more information about SqlClient support for asynchronous programming, see Asynchronous Programming.The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.AsynchronousProcessing" /> property, or false if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0005F6D8 File Offset: 0x0005D8D8
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x0005F6E0 File Offset: 0x0005D8E0
		[Obsolete("This property is ignored beginning in .NET Framework 4.5.For more information about SqlClient support for asynchronous programming, seehttps://docs.microsoft.com/en-us/dotnet/framework/data/adonet/asynchronous-programming")]
		public bool AsynchronousProcessing { get; set; }

		/// <summary>Obsolete. Gets or sets a Boolean value that indicates whether the connection is reset when drawn from the connection pool.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ConnectionReset" /> property, or true if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0005F6E9 File Offset: 0x0005D8E9
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x0005F6F1 File Offset: 0x0005D8F1
		[Obsolete("ConnectionReset has been deprecated.  SqlConnection will ignore the 'connection reset'keyword and always reset the connection")]
		public bool ConnectionReset { get; set; }

		/// <summary>Gets or sets a value that indicates whether a client/server or in-process connection to SQL Server should be made.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection" /> property, or False if none has been supplied.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO("Not implemented in corefx: https://github.com/dotnet/corefx/issues/22474")]
		public bool ContextConnection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a string that contains the name of the network library used to establish a connection to the SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.NetworkLibrary" /> property, or String.Empty if none has been supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">To set the value to null, use <see cref="F:System.DBNull.Value" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO("Not implemented in corefx: https://github.com/dotnet/corefx/issues/22474")]
		public string NetworkLibrary
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO("Not implemented in corefx: https://github.com/dotnet/corefx/issues/22474")]
		public PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO("Not implemented in corefx: https://github.com/dotnet/corefx/issues/22474")]
		public bool TransparentNetworkIPResolution
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO("Not implemented in corefx: https://github.com/dotnet/corefx/issues/22474")]
		public SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0005F774 File Offset: 0x0005D974
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlAuthenticationMethod Authentication
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SqlAuthenticationMethod.NotSpecified;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000CBE RID: 3262
		internal const int KeywordsCount = 29;

		// Token: 0x04000CBF RID: 3263
		internal const int DeprecatedKeywordsCount = 4;

		// Token: 0x04000CC0 RID: 3264
		private static readonly string[] s_validKeywords = SqlConnectionStringBuilder.CreateValidKeywords();

		// Token: 0x04000CC1 RID: 3265
		private static readonly Dictionary<string, SqlConnectionStringBuilder.Keywords> s_keywords = SqlConnectionStringBuilder.CreateKeywordsDictionary();

		// Token: 0x04000CC2 RID: 3266
		private ApplicationIntent _applicationIntent;

		// Token: 0x04000CC3 RID: 3267
		private string _applicationName = "Core .Net SqlClient Data Provider";

		// Token: 0x04000CC4 RID: 3268
		private string _attachDBFilename = "";

		// Token: 0x04000CC5 RID: 3269
		private string _currentLanguage = "";

		// Token: 0x04000CC6 RID: 3270
		private string _dataSource = "";

		// Token: 0x04000CC7 RID: 3271
		private string _failoverPartner = "";

		// Token: 0x04000CC8 RID: 3272
		private string _initialCatalog = "";

		// Token: 0x04000CC9 RID: 3273
		private string _password = "";

		// Token: 0x04000CCA RID: 3274
		private string _transactionBinding = "Implicit Unbind";

		// Token: 0x04000CCB RID: 3275
		private string _typeSystemVersion = "Latest";

		// Token: 0x04000CCC RID: 3276
		private string _userID = "";

		// Token: 0x04000CCD RID: 3277
		private string _workstationID = "";

		// Token: 0x04000CCE RID: 3278
		private int _connectTimeout = 15;

		// Token: 0x04000CCF RID: 3279
		private int _loadBalanceTimeout;

		// Token: 0x04000CD0 RID: 3280
		private int _maxPoolSize = 100;

		// Token: 0x04000CD1 RID: 3281
		private int _minPoolSize;

		// Token: 0x04000CD2 RID: 3282
		private int _packetSize = 8000;

		// Token: 0x04000CD3 RID: 3283
		private int _connectRetryCount = 1;

		// Token: 0x04000CD4 RID: 3284
		private int _connectRetryInterval = 10;

		// Token: 0x04000CD5 RID: 3285
		private bool _encrypt;

		// Token: 0x04000CD6 RID: 3286
		private bool _trustServerCertificate;

		// Token: 0x04000CD7 RID: 3287
		private bool _enlist = true;

		// Token: 0x04000CD8 RID: 3288
		private bool _integratedSecurity;

		// Token: 0x04000CD9 RID: 3289
		private bool _multipleActiveResultSets;

		// Token: 0x04000CDA RID: 3290
		private bool _multiSubnetFailover;

		// Token: 0x04000CDB RID: 3291
		private bool _persistSecurityInfo;

		// Token: 0x04000CDC RID: 3292
		private bool _pooling = true;

		// Token: 0x04000CDD RID: 3293
		private bool _replication;

		// Token: 0x04000CDE RID: 3294
		private bool _userInstance;

		// Token: 0x04000CDF RID: 3295
		private static readonly string[] s_notSupportedKeywords = new string[] { "Asynchronous Processing", "Connection Reset", "Context Connection", "Transaction Binding", "async" };

		// Token: 0x04000CE0 RID: 3296
		private static readonly string[] s_notSupportedNetworkLibraryKeywords = new string[] { "Network Library", "net", "network" };

		// Token: 0x0200019B RID: 411
		private enum Keywords
		{
			// Token: 0x04000CE4 RID: 3300
			DataSource,
			// Token: 0x04000CE5 RID: 3301
			FailoverPartner,
			// Token: 0x04000CE6 RID: 3302
			AttachDBFilename,
			// Token: 0x04000CE7 RID: 3303
			InitialCatalog,
			// Token: 0x04000CE8 RID: 3304
			IntegratedSecurity,
			// Token: 0x04000CE9 RID: 3305
			PersistSecurityInfo,
			// Token: 0x04000CEA RID: 3306
			UserID,
			// Token: 0x04000CEB RID: 3307
			Password,
			// Token: 0x04000CEC RID: 3308
			Enlist,
			// Token: 0x04000CED RID: 3309
			Pooling,
			// Token: 0x04000CEE RID: 3310
			MinPoolSize,
			// Token: 0x04000CEF RID: 3311
			MaxPoolSize,
			// Token: 0x04000CF0 RID: 3312
			MultipleActiveResultSets,
			// Token: 0x04000CF1 RID: 3313
			Replication,
			// Token: 0x04000CF2 RID: 3314
			ConnectTimeout,
			// Token: 0x04000CF3 RID: 3315
			Encrypt,
			// Token: 0x04000CF4 RID: 3316
			TrustServerCertificate,
			// Token: 0x04000CF5 RID: 3317
			LoadBalanceTimeout,
			// Token: 0x04000CF6 RID: 3318
			PacketSize,
			// Token: 0x04000CF7 RID: 3319
			TypeSystemVersion,
			// Token: 0x04000CF8 RID: 3320
			ApplicationName,
			// Token: 0x04000CF9 RID: 3321
			CurrentLanguage,
			// Token: 0x04000CFA RID: 3322
			WorkstationID,
			// Token: 0x04000CFB RID: 3323
			UserInstance,
			// Token: 0x04000CFC RID: 3324
			TransactionBinding,
			// Token: 0x04000CFD RID: 3325
			ApplicationIntent,
			// Token: 0x04000CFE RID: 3326
			MultiSubnetFailover,
			// Token: 0x04000CFF RID: 3327
			ConnectRetryCount,
			// Token: 0x04000D00 RID: 3328
			ConnectRetryInterval,
			// Token: 0x04000D01 RID: 3329
			KeywordsCount
		}

		// Token: 0x0200019C RID: 412
		private sealed class SqlInitialCatalogConverter : StringConverter
		{
			// Token: 0x06001362 RID: 4962 RVA: 0x0005F78F File Offset: 0x0005D98F
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return this.GetStandardValuesSupportedInternal(context);
			}

			// Token: 0x06001363 RID: 4963 RVA: 0x0005F798 File Offset: 0x0005D998
			private bool GetStandardValuesSupportedInternal(ITypeDescriptorContext context)
			{
				bool flag = false;
				if (context != null)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = context.Instance as SqlConnectionStringBuilder;
					if (sqlConnectionStringBuilder != null && 0 < sqlConnectionStringBuilder.DataSource.Length && (sqlConnectionStringBuilder.IntegratedSecurity || 0 < sqlConnectionStringBuilder.UserID.Length))
					{
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x06001364 RID: 4964 RVA: 0x000061D5 File Offset: 0x000043D5
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001365 RID: 4965 RVA: 0x0005F7E0 File Offset: 0x0005D9E0
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (this.GetStandardValuesSupportedInternal(context))
				{
					List<string> list = new List<string>();
					try
					{
						SqlConnectionStringBuilder sqlConnectionStringBuilder = (SqlConnectionStringBuilder)context.Instance;
						using (SqlConnection sqlConnection = new SqlConnection())
						{
							sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
							sqlConnection.Open();
							foreach (object obj in sqlConnection.GetSchema("DATABASES").Rows)
							{
								string text = (string)((DataRow)obj)["database_name"];
								list.Add(text);
							}
						}
					}
					catch (SqlException ex)
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					return new TypeConverter.StandardValuesCollection(list);
				}
				return null;
			}
		}
	}
}
