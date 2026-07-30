using System;

namespace System.Web.Management
{
	/// <summary>Defines the codes associated with the ASP.NET health monitoring events.</summary>
	// Token: 0x02000531 RID: 1329
	public sealed class WebEventCodes
	{
		/// <summary>Represents the event code indicating that the event code value is not allowed. This field is constant.</summary>
		// Token: 0x04001F7B RID: 8059
		public const int InvalidEventCode = -1;

		/// <summary>Represents the event code indicating that the major event code value is not defined. This field is constant.</summary>
		// Token: 0x04001F7C RID: 8060
		public const int UndefinedEventCode = 0;

		/// <summary>Represents the event code indicating that the detail event code value is not defined. This field is constant.</summary>
		// Token: 0x04001F7D RID: 8061
		public const int UndefinedEventDetailCode = 0;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring application event codes. This field is constant.</summary>
		// Token: 0x04001F7E RID: 8062
		public const int ApplicationCodeBase = 1000;

		/// <summary>Represents the event code indicating that an application has started. This field is constant.</summary>
		// Token: 0x04001F7F RID: 8063
		public const int ApplicationStart = 1001;

		/// <summary>Represents the event code indicating that an application has shut down. This field is constant.</summary>
		// Token: 0x04001F80 RID: 8064
		public const int ApplicationShutdown = 1002;

		/// <summary>Represents the event code indicating that the compilation of the application has started. This field is constant. </summary>
		// Token: 0x04001F81 RID: 8065
		public const int ApplicationCompilationStart = 1003;

		/// <summary>Represents the event code indicating that the compilation of the application has finished. This field is constant. </summary>
		// Token: 0x04001F82 RID: 8066
		public const int ApplicationCompilationEnd = 1004;

		/// <summary>Represents the event code indicating that a heartbeat event occurred. This field is constant.</summary>
		// Token: 0x04001F83 RID: 8067
		public const int ApplicationHeartbeat = 1005;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring Web-request event codes. This field is constant.</summary>
		// Token: 0x04001F84 RID: 8068
		public const int RequestCodeBase = 2000;

		/// <summary>Represents the event code indicating that the Web request was completed. This field is constant.</summary>
		// Token: 0x04001F85 RID: 8069
		public const int RequestTransactionComplete = 2001;

		/// <summary>Represents the event code indicating that the Web request was aborted. This field is constant.</summary>
		// Token: 0x04001F86 RID: 8070
		public const int RequestTransactionAbort = 2002;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring error event codes. This field is constant.</summary>
		// Token: 0x04001F87 RID: 8071
		public const int ErrorCodeBase = 3000;

		/// <summary>Represents the event code indicating that the Web request has been aborted.</summary>
		// Token: 0x04001F88 RID: 8072
		public const int RuntimeErrorRequestAbort = 3001;

		/// <summary>Represents the event code indicating that a view-state failure occurred. This field is constant.</summary>
		// Token: 0x04001F89 RID: 8073
		public const int RuntimeErrorViewStateFailure = 3002;

		/// <summary>Represents the event code indicating that a validation error occurred. This field is constant.</summary>
		// Token: 0x04001F8A RID: 8074
		public const int RuntimeErrorValidationFailure = 3003;

		/// <summary>Represents the event code indicating that the size of the posted information exceeded the allowed limits. This field is constant.</summary>
		// Token: 0x04001F8B RID: 8075
		public const int RuntimeErrorPostTooLarge = 3004;

		/// <summary>Represents the event code indicating an unhandled exception occurred. This field is constant.</summary>
		// Token: 0x04001F8C RID: 8076
		public const int RuntimeErrorUnhandledException = 3005;

		/// <summary>Represents the event code indicating a parser error occurred.</summary>
		// Token: 0x04001F8D RID: 8077
		public const int WebErrorParserError = 3006;

		/// <summary>Indicates that a compilation error occurred.</summary>
		// Token: 0x04001F8E RID: 8078
		public const int WebErrorCompilationError = 3007;

		/// <summary>Indicates that a configuration error occurred. This field is constant.</summary>
		// Token: 0x04001F8F RID: 8079
		public const int WebErrorConfigurationError = 3008;

		/// <summary>Represents the event code indicating that an unclassified error occurred. This field is constant.</summary>
		// Token: 0x04001F90 RID: 8080
		public const int WebErrorOtherError = 3009;

		/// <summary>Represents the event code indicating that there was an error during the deserialization of a property. This field is constant.</summary>
		// Token: 0x04001F91 RID: 8081
		public const int WebErrorPropertyDeserializationError = 3010;

		/// <summary>Represents the event code indicating that there was an error during the deserialization of the type or value of an object. This field is constant.</summary>
		// Token: 0x04001F92 RID: 8082
		public const int WebErrorObjectStateFormatterDeserializationError = 3011;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring audit event codes. This field is constant.</summary>
		// Token: 0x04001F93 RID: 8083
		public const int AuditCodeBase = 4000;

		/// <summary>Represents the event code indicating a form-authentication success occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F94 RID: 8084
		public const int AuditFormsAuthenticationSuccess = 4001;

		/// <summary>Represents the event code indicating that a membership-authentication success occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F95 RID: 8085
		public const int AuditMembershipAuthenticationSuccess = 4002;

		/// <summary>Represents the event code indicating a URL-authorization success occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F96 RID: 8086
		public const int AuditUrlAuthorizationSuccess = 4003;

		/// <summary>Represents the event code indicating that a file-authorization success occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F97 RID: 8087
		public const int AuditFileAuthorizationSuccess = 4004;

		/// <summary>Represents the event code indicating a form authentication failure occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F98 RID: 8088
		public const int AuditFormsAuthenticationFailure = 4005;

		/// <summary>Represents the event code indicating that a membership-authentication failure occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F99 RID: 8089
		public const int AuditMembershipAuthenticationFailure = 4006;

		/// <summary>Represents the event code indicating that a URL-authorization failure occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F9A RID: 8090
		public const int AuditUrlAuthorizationFailure = 4007;

		/// <summary>Represents the event code indicating that a file-authorization failure occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F9B RID: 8091
		public const int AuditFileAuthorizationFailure = 4008;

		/// <summary>Represents the event code indicating that the view-state verification failed. This field is constant.</summary>
		// Token: 0x04001F9C RID: 8092
		public const int AuditInvalidViewStateFailure = 4009;

		/// <summary>Represents the event code indicating that an unhandled security exception occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F9D RID: 8093
		public const int AuditUnhandledSecurityException = 4010;

		/// <summary>Represents the event code indicating that an unhandled access exception occurred during a Web request. This field is constant.</summary>
		// Token: 0x04001F9E RID: 8094
		public const int AuditUnhandledAccessException = 4011;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring Web miscellaneous event codes. This field is constant.</summary>
		// Token: 0x04001F9F RID: 8095
		public const int MiscCodeBase = 6000;

		/// <summary>Represents the event code used by providers to record nonstandard information about an event. This field is constant.</summary>
		// Token: 0x04001FA0 RID: 8096
		public const int WebEventProviderInformation = 6001;

		/// <summary>Identifies the offset for the application detail event codes. This field is constant.</summary>
		// Token: 0x04001FA1 RID: 8097
		public const int ApplicationDetailCodeBase = 50000;

		/// <summary>Represents the event code indicating that the application shutdown reason is unknown. This field is constant.</summary>
		// Token: 0x04001FA2 RID: 8098
		public const int ApplicationShutdownUnknown = 50001;

		/// <summary>Represents the event code indicating that the hosting environment is shutting down. This field is constant.</summary>
		// Token: 0x04001FA3 RID: 8099
		public const int ApplicationShutdownHostingEnvironment = 50002;

		/// <summary>Represents the event code indicating that the Global.asax file has changed. This field is constant.</summary>
		// Token: 0x04001FA4 RID: 8100
		public const int ApplicationShutdownChangeInGlobalAsax = 50003;

		/// <summary>Represents the event code indicating that the configuration file has changed. This field is constant.</summary>
		// Token: 0x04001FA5 RID: 8101
		public const int ApplicationShutdownConfigurationChange = 50004;

		/// <summary>Represents the event code indicating that the application domain was explicitly unloaded. This field is constant.</summary>
		// Token: 0x04001FA6 RID: 8102
		public const int ApplicationShutdownUnloadAppDomainCalled = 50005;

		/// <summary>Represents the event code indicating that the security policy file has changed. This field is constant.</summary>
		// Token: 0x04001FA7 RID: 8103
		public const int ApplicationShutdownChangeInSecurityPolicyFile = 50006;

		/// <summary>Represents the event code indicating a subdirectory in the application's Bin directory was changed or renamed. This field is constant.</summary>
		// Token: 0x04001FA8 RID: 8104
		public const int ApplicationShutdownBinDirChangeOrDirectoryRename = 50007;

		/// <summary>Represents the event code indicating a subdirectory in the Browsers application directory was changed or renamed. This field is constant.</summary>
		// Token: 0x04001FA9 RID: 8105
		public const int ApplicationShutdownBrowsersDirChangeOrDirectoryRename = 50008;

		/// <summary>Represents the event code indicating a subdirectory in the App_Code directory was changed or renamed. This field is constant.</summary>
		// Token: 0x04001FAA RID: 8106
		public const int ApplicationShutdownCodeDirChangeOrDirectoryRename = 50009;

		/// <summary>Represents the event code indicating a subdirectory in the App_Resources directory was changed or renamed. This field is constant.</summary>
		// Token: 0x04001FAB RID: 8107
		public const int ApplicationShutdownResourcesDirChangeOrDirectoryRename = 50010;

		/// <summary>Represents the event code indicating that the idle time-out was exceeded. This field is constant.</summary>
		// Token: 0x04001FAC RID: 8108
		public const int ApplicationShutdownIdleTimeout = 50011;

		/// <summary>Represents the event code indicating that the physical path of the application has changed. This field is constant.</summary>
		// Token: 0x04001FAD RID: 8109
		public const int ApplicationShutdownPhysicalApplicationPathChanged = 50012;

		/// <summary>Represents the event code indicating that the ASP.NET run time was explicitly closed. This field is constant.</summary>
		// Token: 0x04001FAE RID: 8110
		public const int ApplicationShutdownHttpRuntimeClose = 50013;

		/// <summary>Represents the event code indicating an application-initialization error occurred. This field is constant.</summary>
		// Token: 0x04001FAF RID: 8111
		public const int ApplicationShutdownInitializationError = 50014;

		/// <summary>Represents the event code indicating that the maximum number of recompilations was reached. This field is constant.</summary>
		// Token: 0x04001FB0 RID: 8112
		public const int ApplicationShutdownMaxRecompilationsReached = 50015;

		/// <summary>Represents the event code indicating that an error occurred while communicating with the state server. This field is constant.</summary>
		// Token: 0x04001FB1 RID: 8113
		public const int StateServerConnectionError = 50016;

		/// <summary>Identifies the offset for the ASP.NET audit-detail event codes. This field is constant.</summary>
		// Token: 0x04001FB2 RID: 8114
		public const int AuditDetailCodeBase = 50200;

		/// <summary>Represents the event code indicating that the supplied ticket is invalid. This field is constant.</summary>
		// Token: 0x04001FB3 RID: 8115
		public const int InvalidTicketFailure = 50201;

		/// <summary>Represents the event code indicating that the supplied ticket is expired. This field is constant.</summary>
		// Token: 0x04001FB4 RID: 8116
		public const int ExpiredTicketFailure = 50202;

		/// <summary>Represents the event code indicating that the supplied view state failed the integrity check. This field is constant.</summary>
		// Token: 0x04001FB5 RID: 8117
		public const int InvalidViewStateMac = 50203;

		/// <summary>Represents the event code indicating that the supplied view state is invalid. This field is constant.</summary>
		// Token: 0x04001FB6 RID: 8118
		public const int InvalidViewState = 50204;

		/// <summary>Identifies the offset for the ASP.NET health-monitoring Web-detail event codes. </summary>
		// Token: 0x04001FB7 RID: 8119
		public const int WebEventDetailCodeBase = 50300;

		/// <summary>Represents the event code indicating that the SQL provider dropped events. This field is constant.</summary>
		// Token: 0x04001FB8 RID: 8120
		public const int SqlProviderEventsDropped = 50301;

		/// <summary>Identifies the offset for the custom event codes. This field is constant.</summary>
		// Token: 0x04001FB9 RID: 8121
		public const int WebExtendedBase = 100000;

		/// <summary>Represents the event code indicating that the build manager has made a change that requires the application domain to be shut down.</summary>
		// Token: 0x04001FBA RID: 8122
		public const int ApplicationShutdownBuildManagerChange = 50017;

		/// <summary>Represents the event code indicating that there was an error accessing a web resource. This field is constant.</summary>
		// Token: 0x04001FBB RID: 8123
		public const int RuntimeErrorWebResourceFailure = 3012;
	}
}
