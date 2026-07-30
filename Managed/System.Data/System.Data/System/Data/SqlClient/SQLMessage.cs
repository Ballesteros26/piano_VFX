using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001F5 RID: 501
	internal sealed class SQLMessage
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x00005C14 File Offset: 0x00003E14
		private SQLMessage()
		{
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00071E6B File Offset: 0x0007006B
		internal static string CultureIdError()
		{
			return SR.GetString("The Collation specified by SQL Server is not supported.");
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00071E77 File Offset: 0x00070077
		internal static string EncryptionNotSupportedByClient()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to requires encryption but this machine does not support it.");
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00071E83 File Offset: 0x00070083
		internal static string EncryptionNotSupportedByServer()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to does not support encryption.");
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00071E8F File Offset: 0x0007008F
		internal static string OperationCancelled()
		{
			return SR.GetString("Operation cancelled by user.");
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00071E9B File Offset: 0x0007009B
		internal static string SevereError()
		{
			return SR.GetString("A severe error occurred on the current command.  The results, if any, should be discarded.");
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00071EA7 File Offset: 0x000700A7
		internal static string SSPIInitializeError()
		{
			return SR.GetString("Cannot initialize SSPI package.");
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00071EB3 File Offset: 0x000700B3
		internal static string SSPIGenerateError()
		{
			return SR.GetString("Failed to generate SSPI context.");
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00071EBF File Offset: 0x000700BF
		internal static string SqlServerBrowserNotAccessible()
		{
			return SR.GetString("Cannot connect to SQL Server Browser. Ensure SQL Server Browser has been started.");
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00071ECB File Offset: 0x000700CB
		internal static string KerberosTicketMissingError()
		{
			return SR.GetString("Cannot access Kerberos ticket. Ensure Kerberos has been initialized with 'kinit'.");
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00071ED7 File Offset: 0x000700D7
		internal static string Timeout()
		{
			return SR.GetString("Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.");
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00071EE3 File Offset: 0x000700E3
		internal static string Timeout_PreLogin_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the pre-login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00071EEF File Offset: 0x000700EF
		internal static string Timeout_PreLogin_InitializeConnection()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to create and initialize a socket to the server.  This could be either because the server was unreachable or unable to respond back in time.");
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00071EFB File Offset: 0x000700FB
		internal static string Timeout_PreLogin_SendHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while making a pre-login handshake request.  This could be because the server was unable to respond back in time.");
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00071F07 File Offset: 0x00070107
		internal static string Timeout_PreLogin_ConsumeHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement.  This could be because the pre-login handshake failed or the server was unable to respond back in time.");
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00071F13 File Offset: 0x00070113
		internal static string Timeout_Login_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00071F1F File Offset: 0x0007011F
		internal static string Timeout_Login_ProcessConnectionAuth()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to authenticate the login.  This could be because the server failed to authenticate the user or the server was unable to respond back in time.");
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00071F2B File Offset: 0x0007012B
		internal static string Timeout_PostLogin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed during the post-login phase.  The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.");
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00071F37 File Offset: 0x00070137
		internal static string Timeout_FailoverInfo()
		{
			return SR.GetString("This failure occurred while attempting to connect to the {0} server.");
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00071F43 File Offset: 0x00070143
		internal static string Timeout_RoutingDestination()
		{
			return SR.GetString("This failure occurred while attempting to connect to the routing destination. The duration spent while attempting to connect to the original server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4};  ");
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00071F4F File Offset: 0x0007014F
		internal static string Duration_PreLogin_Begin(long PreLoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0};", new object[] { PreLoginBeginDuration });
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00071F6A File Offset: 0x0007016A
		internal static string Duration_PreLoginHandshake(long PreLoginBeginDuration, long PreLoginHandshakeDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration });
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00071F8E File Offset: 0x0007018E
		internal static string Duration_Login_Begin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration });
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00071FBB File Offset: 0x000701BB
		internal static string Duration_Login_ProcessConnectionAuth(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration });
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00071FF1 File Offset: 0x000701F1
		internal static string Duration_PostLogin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration, long PostLoginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration, PostLoginDuration });
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00072031 File Offset: 0x00070231
		internal static string UserInstanceFailure()
		{
			return SR.GetString("A user instance was requested in the connection string but the server specified does not support this option.");
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0007203D File Offset: 0x0007023D
		internal static string PreloginError()
		{
			return SR.GetString("A connection was successfully established with the server, but then an error occurred during the pre-login handshake.");
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00072049 File Offset: 0x00070249
		internal static string ExClientConnectionId()
		{
			return SR.GetString("ClientConnectionId:{0}");
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00072055 File Offset: 0x00070255
		internal static string ExErrorNumberStateClass()
		{
			return SR.GetString("Error Number:{0},State:{1},Class:{2}");
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00072061 File Offset: 0x00070261
		internal static string ExOriginalClientConnectionId()
		{
			return SR.GetString("ClientConnectionId before routing:{0}");
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007206D File Offset: 0x0007026D
		internal static string ExRoutingDestination()
		{
			return SR.GetString("Routing Destination:{0}");
		}
	}
}
