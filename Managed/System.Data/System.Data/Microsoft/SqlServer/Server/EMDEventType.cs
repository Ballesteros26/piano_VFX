using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BF RID: 959
	internal enum EMDEventType
	{
		// Token: 0x04001B5E RID: 7006
		x_eet_Invalid,
		// Token: 0x04001B5F RID: 7007
		x_eet_Insert,
		// Token: 0x04001B60 RID: 7008
		x_eet_Update,
		// Token: 0x04001B61 RID: 7009
		x_eet_Delete,
		// Token: 0x04001B62 RID: 7010
		x_eet_Create_Table = 21,
		// Token: 0x04001B63 RID: 7011
		x_eet_Alter_Table,
		// Token: 0x04001B64 RID: 7012
		x_eet_Drop_Table,
		// Token: 0x04001B65 RID: 7013
		x_eet_Create_Index,
		// Token: 0x04001B66 RID: 7014
		x_eet_Alter_Index,
		// Token: 0x04001B67 RID: 7015
		x_eet_Drop_Index,
		// Token: 0x04001B68 RID: 7016
		x_eet_Create_Stats,
		// Token: 0x04001B69 RID: 7017
		x_eet_Update_Stats,
		// Token: 0x04001B6A RID: 7018
		x_eet_Drop_Stats,
		// Token: 0x04001B6B RID: 7019
		x_eet_Create_Secexpr = 31,
		// Token: 0x04001B6C RID: 7020
		x_eet_Drop_Secexpr = 33,
		// Token: 0x04001B6D RID: 7021
		x_eet_Create_Synonym,
		// Token: 0x04001B6E RID: 7022
		x_eet_Drop_Synonym = 36,
		// Token: 0x04001B6F RID: 7023
		x_eet_Create_View = 41,
		// Token: 0x04001B70 RID: 7024
		x_eet_Alter_View,
		// Token: 0x04001B71 RID: 7025
		x_eet_Drop_View,
		// Token: 0x04001B72 RID: 7026
		x_eet_Create_Procedure = 51,
		// Token: 0x04001B73 RID: 7027
		x_eet_Alter_Procedure,
		// Token: 0x04001B74 RID: 7028
		x_eet_Drop_Procedure,
		// Token: 0x04001B75 RID: 7029
		x_eet_Create_Function = 61,
		// Token: 0x04001B76 RID: 7030
		x_eet_Alter_Function,
		// Token: 0x04001B77 RID: 7031
		x_eet_Drop_Function,
		// Token: 0x04001B78 RID: 7032
		x_eet_Create_Trigger = 71,
		// Token: 0x04001B79 RID: 7033
		x_eet_Alter_Trigger,
		// Token: 0x04001B7A RID: 7034
		x_eet_Drop_Trigger,
		// Token: 0x04001B7B RID: 7035
		x_eet_Create_Event_Notification,
		// Token: 0x04001B7C RID: 7036
		x_eet_Drop_Event_Notification = 76,
		// Token: 0x04001B7D RID: 7037
		x_eet_Create_Type = 91,
		// Token: 0x04001B7E RID: 7038
		x_eet_Drop_Type = 93,
		// Token: 0x04001B7F RID: 7039
		x_eet_Create_Assembly = 101,
		// Token: 0x04001B80 RID: 7040
		x_eet_Alter_Assembly,
		// Token: 0x04001B81 RID: 7041
		x_eet_Drop_Assembly,
		// Token: 0x04001B82 RID: 7042
		x_eet_Create_User = 131,
		// Token: 0x04001B83 RID: 7043
		x_eet_Alter_User,
		// Token: 0x04001B84 RID: 7044
		x_eet_Drop_User,
		// Token: 0x04001B85 RID: 7045
		x_eet_Create_Role,
		// Token: 0x04001B86 RID: 7046
		x_eet_Alter_Role,
		// Token: 0x04001B87 RID: 7047
		x_eet_Drop_Role,
		// Token: 0x04001B88 RID: 7048
		x_eet_Create_AppRole,
		// Token: 0x04001B89 RID: 7049
		x_eet_Alter_AppRole,
		// Token: 0x04001B8A RID: 7050
		x_eet_Drop_AppRole,
		// Token: 0x04001B8B RID: 7051
		x_eet_Create_Schema = 141,
		// Token: 0x04001B8C RID: 7052
		x_eet_Alter_Schema,
		// Token: 0x04001B8D RID: 7053
		x_eet_Drop_Schema,
		// Token: 0x04001B8E RID: 7054
		x_eet_Create_Login,
		// Token: 0x04001B8F RID: 7055
		x_eet_Alter_Login,
		// Token: 0x04001B90 RID: 7056
		x_eet_Drop_Login,
		// Token: 0x04001B91 RID: 7057
		x_eet_Create_MsgType = 151,
		// Token: 0x04001B92 RID: 7058
		x_eet_Alter_MsgType,
		// Token: 0x04001B93 RID: 7059
		x_eet_Drop_MsgType,
		// Token: 0x04001B94 RID: 7060
		x_eet_Create_Contract,
		// Token: 0x04001B95 RID: 7061
		x_eet_Alter_Contract,
		// Token: 0x04001B96 RID: 7062
		x_eet_Drop_Contract,
		// Token: 0x04001B97 RID: 7063
		x_eet_Create_Queue,
		// Token: 0x04001B98 RID: 7064
		x_eet_Alter_Queue,
		// Token: 0x04001B99 RID: 7065
		x_eet_Drop_Queue,
		// Token: 0x04001B9A RID: 7066
		x_eet_Create_Service = 161,
		// Token: 0x04001B9B RID: 7067
		x_eet_Alter_Service,
		// Token: 0x04001B9C RID: 7068
		x_eet_Drop_Service,
		// Token: 0x04001B9D RID: 7069
		x_eet_Create_Route,
		// Token: 0x04001B9E RID: 7070
		x_eet_Alter_Route,
		// Token: 0x04001B9F RID: 7071
		x_eet_Drop_Route,
		// Token: 0x04001BA0 RID: 7072
		x_eet_Grant_Statement,
		// Token: 0x04001BA1 RID: 7073
		x_eet_Deny_Statement,
		// Token: 0x04001BA2 RID: 7074
		x_eet_Revoke_Statement,
		// Token: 0x04001BA3 RID: 7075
		x_eet_Grant_Object,
		// Token: 0x04001BA4 RID: 7076
		x_eet_Deny_Object,
		// Token: 0x04001BA5 RID: 7077
		x_eet_Revoke_Object,
		// Token: 0x04001BA6 RID: 7078
		x_eet_Activation,
		// Token: 0x04001BA7 RID: 7079
		x_eet_Create_Binding,
		// Token: 0x04001BA8 RID: 7080
		x_eet_Alter_Binding,
		// Token: 0x04001BA9 RID: 7081
		x_eet_Drop_Binding,
		// Token: 0x04001BAA RID: 7082
		x_eet_Create_XmlSchema,
		// Token: 0x04001BAB RID: 7083
		x_eet_Alter_XmlSchema,
		// Token: 0x04001BAC RID: 7084
		x_eet_Drop_XmlSchema,
		// Token: 0x04001BAD RID: 7085
		x_eet_Create_HttpEndpoint = 181,
		// Token: 0x04001BAE RID: 7086
		x_eet_Alter_HttpEndpoint,
		// Token: 0x04001BAF RID: 7087
		x_eet_Drop_HttpEndpoint,
		// Token: 0x04001BB0 RID: 7088
		x_eet_Create_Partition_Function = 191,
		// Token: 0x04001BB1 RID: 7089
		x_eet_Alter_Partition_Function,
		// Token: 0x04001BB2 RID: 7090
		x_eet_Drop_Partition_Function,
		// Token: 0x04001BB3 RID: 7091
		x_eet_Create_Partition_Scheme,
		// Token: 0x04001BB4 RID: 7092
		x_eet_Alter_Partition_Scheme,
		// Token: 0x04001BB5 RID: 7093
		x_eet_Drop_Partition_Scheme,
		// Token: 0x04001BB6 RID: 7094
		x_eet_Create_Database = 201,
		// Token: 0x04001BB7 RID: 7095
		x_eet_Alter_Database,
		// Token: 0x04001BB8 RID: 7096
		x_eet_Drop_Database,
		// Token: 0x04001BB9 RID: 7097
		x_eet_Trace_Start = 1000,
		// Token: 0x04001BBA RID: 7098
		x_eet_Trace_End = 1999
	}
}
