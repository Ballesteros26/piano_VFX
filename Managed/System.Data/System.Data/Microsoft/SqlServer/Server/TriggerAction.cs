using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>The <see cref="T:Microsoft.SqlServer.Server.TriggerAction" /> enumeration is used by the <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> class to indicate what action fired the trigger. </summary>
	// Token: 0x020003C0 RID: 960
	public enum TriggerAction
	{
		/// <summary>An invalid trigger action, one that is not exposed to the user, occurred.</summary>
		// Token: 0x04001BBC RID: 7100
		Invalid,
		/// <summary>An INSERT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BBD RID: 7101
		Insert,
		/// <summary>An UPDATE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BBE RID: 7102
		Update,
		/// <summary>A DELETE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BBF RID: 7103
		Delete,
		/// <summary>A CREATE TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC0 RID: 7104
		CreateTable = 21,
		/// <summary>An ALTER TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC1 RID: 7105
		AlterTable,
		/// <summary>A DROP TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC2 RID: 7106
		DropTable,
		/// <summary>A CREATE INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC3 RID: 7107
		CreateIndex,
		/// <summary>An ALTER INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC4 RID: 7108
		AlterIndex,
		/// <summary>A DROP INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC5 RID: 7109
		DropIndex,
		/// <summary>A CREATE SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC6 RID: 7110
		CreateSynonym = 34,
		/// <summary>A DROP SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x04001BC7 RID: 7111
		DropSynonym = 36,
		/// <summary>Not available.</summary>
		// Token: 0x04001BC8 RID: 7112
		CreateSecurityExpression = 31,
		/// <summary>Not available.</summary>
		// Token: 0x04001BC9 RID: 7113
		DropSecurityExpression = 33,
		/// <summary>A CREATE VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCA RID: 7114
		CreateView = 41,
		/// <summary>An ALTER VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCB RID: 7115
		AlterView,
		/// <summary>A DROP VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCC RID: 7116
		DropView,
		/// <summary>A CREATE PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCD RID: 7117
		CreateProcedure = 51,
		/// <summary>An ALTER PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCE RID: 7118
		AlterProcedure,
		/// <summary>A DROP PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BCF RID: 7119
		DropProcedure,
		/// <summary>A CREATE FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD0 RID: 7120
		CreateFunction = 61,
		/// <summary>An ALTER FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD1 RID: 7121
		AlterFunction,
		/// <summary>A DROP FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD2 RID: 7122
		DropFunction,
		/// <summary>A CREATE TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD3 RID: 7123
		CreateTrigger = 71,
		/// <summary>An ALTER TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD4 RID: 7124
		AlterTrigger,
		/// <summary>A DROP TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD5 RID: 7125
		DropTrigger,
		/// <summary>A CREATE EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD6 RID: 7126
		CreateEventNotification,
		/// <summary>A DROP EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD7 RID: 7127
		DropEventNotification = 76,
		/// <summary>A CREATE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD8 RID: 7128
		CreateType = 91,
		/// <summary>A DROP TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BD9 RID: 7129
		DropType = 93,
		/// <summary>A CREATE ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDA RID: 7130
		CreateAssembly = 101,
		/// <summary>An ALTER ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDB RID: 7131
		AlterAssembly,
		/// <summary>A DROP ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDC RID: 7132
		DropAssembly,
		/// <summary>A CREATE USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDD RID: 7133
		CreateUser = 131,
		/// <summary>An ALTER USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDE RID: 7134
		AlterUser,
		/// <summary>A DROP USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001BDF RID: 7135
		DropUser,
		/// <summary>A CREATE ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE0 RID: 7136
		CreateRole,
		/// <summary>An ALTER ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE1 RID: 7137
		AlterRole,
		/// <summary>A DROP ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE2 RID: 7138
		DropRole,
		/// <summary>A CREATE APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE3 RID: 7139
		CreateAppRole,
		/// <summary>An ALTER APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE4 RID: 7140
		AlterAppRole,
		/// <summary>A DROP APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE5 RID: 7141
		DropAppRole,
		/// <summary>A CREATE SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE6 RID: 7142
		CreateSchema = 141,
		/// <summary>An ALTER SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE7 RID: 7143
		AlterSchema,
		/// <summary>A DROP SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE8 RID: 7144
		DropSchema,
		/// <summary>A CREATE LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001BE9 RID: 7145
		CreateLogin,
		/// <summary>An ALTER LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001BEA RID: 7146
		AlterLogin,
		/// <summary>A DROP LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001BEB RID: 7147
		DropLogin,
		/// <summary>A CREATE MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BEC RID: 7148
		CreateMsgType = 151,
		/// <summary>A DROP MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BED RID: 7149
		DropMsgType = 153,
		/// <summary>A CREATE CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BEE RID: 7150
		CreateContract,
		/// <summary>A DROP CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BEF RID: 7151
		DropContract = 156,
		/// <summary>A CREATE QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF0 RID: 7152
		CreateQueue,
		/// <summary>An ALTER QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF1 RID: 7153
		AlterQueue,
		/// <summary>A DROP QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF2 RID: 7154
		DropQueue,
		/// <summary>A CREATE SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF3 RID: 7155
		CreateService = 161,
		/// <summary>An ALTER SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF4 RID: 7156
		AlterService,
		/// <summary>A DROP SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF5 RID: 7157
		DropService,
		/// <summary>A CREATE ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF6 RID: 7158
		CreateRoute,
		/// <summary>An ALTER ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF7 RID: 7159
		AlterRoute,
		/// <summary>A DROP ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF8 RID: 7160
		DropRoute,
		/// <summary>A GRANT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BF9 RID: 7161
		GrantStatement,
		/// <summary>A DENY Transact-SQL statement was executed.</summary>
		// Token: 0x04001BFA RID: 7162
		DenyStatement,
		/// <summary>A REVOKE Transact-SQL statement was executed.</summary>
		// Token: 0x04001BFB RID: 7163
		RevokeStatement,
		/// <summary>A GRANT OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BFC RID: 7164
		GrantObject,
		/// <summary>A DENY Object Permissions Transact-SQL statement was executed.</summary>
		// Token: 0x04001BFD RID: 7165
		DenyObject,
		/// <summary>A REVOKE OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x04001BFE RID: 7166
		RevokeObject,
		/// <summary>A CREATE_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001BFF RID: 7167
		CreateBinding = 174,
		/// <summary>An ALTER_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001C00 RID: 7168
		AlterBinding,
		/// <summary>A DROP_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001C01 RID: 7169
		DropBinding,
		/// <summary>A CREATE PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C02 RID: 7170
		CreatePartitionFunction = 191,
		/// <summary>An ALTER PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C03 RID: 7171
		AlterPartitionFunction,
		/// <summary>A DROP PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C04 RID: 7172
		DropPartitionFunction,
		/// <summary>A CREATE PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C05 RID: 7173
		CreatePartitionScheme,
		/// <summary>An ALTER PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C06 RID: 7174
		AlterPartitionScheme,
		/// <summary>A DROP PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C07 RID: 7175
		DropPartitionScheme
	}
}
