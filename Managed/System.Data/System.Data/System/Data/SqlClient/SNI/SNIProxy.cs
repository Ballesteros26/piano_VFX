using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000250 RID: 592
	internal class SNIProxy
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x00005E03 File Offset: 0x00004003
		public void Terminate()
		{
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00084828 File Offset: 0x00082A28
		public uint EnableSsl(SNIHandle handle, uint options)
		{
			uint num;
			try
			{
				num = handle.EnableSsl(options);
			}
			catch (Exception ex)
			{
				num = SNICommon.ReportSNIError(SNIProviders.SSL_PROV, 31U, ex);
			}
			return num;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00084860 File Offset: 0x00082A60
		public uint DisableSsl(SNIHandle handle)
		{
			handle.DisableSsl();
			return 0U;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0008486C File Offset: 0x00082A6C
		public void GenSspiClientContext(SspiClientContextStatus sspiClientContextStatus, byte[] receivedBuff, ref byte[] sendBuff, byte[] serverName)
		{
			SafeDeleteContext securityContext = sspiClientContextStatus.SecurityContext;
			ContextFlagsPal contextFlags = sspiClientContextStatus.ContextFlags;
			SafeFreeCredentials safeFreeCredentials = sspiClientContextStatus.CredentialsHandle;
			string text = "Negotiate";
			if (securityContext == null)
			{
				safeFreeCredentials = NegotiateStreamPal.AcquireDefaultCredential(text, false);
			}
			SecurityBuffer[] array;
			if (receivedBuff != null)
			{
				array = new SecurityBuffer[]
				{
					new SecurityBuffer(receivedBuff, SecurityBufferType.SECBUFFER_TOKEN)
				};
			}
			else
			{
				array = new SecurityBuffer[0];
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(NegotiateStreamPal.QueryMaxTokenSize(text), SecurityBufferType.SECBUFFER_TOKEN);
			ContextFlagsPal contextFlagsPal = ContextFlagsPal.MutualAuth | ContextFlagsPal.Confidentiality | ContextFlagsPal.Connection;
			string @string = Encoding.UTF8.GetString(serverName);
			SecurityStatusPal securityStatusPal = NegotiateStreamPal.InitializeSecurityContext(safeFreeCredentials, ref securityContext, @string, contextFlagsPal, array, securityBuffer, ref contextFlags);
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded || securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompAndContinue)
			{
				array = new SecurityBuffer[] { securityBuffer };
				securityStatusPal = NegotiateStreamPal.CompleteAuthToken(ref securityContext, array);
				securityBuffer.token = null;
			}
			sendBuff = securityBuffer.token;
			if (sendBuff == null)
			{
				sendBuff = Array.Empty<byte>();
			}
			sspiClientContextStatus.SecurityContext = securityContext;
			sspiClientContextStatus.ContextFlags = contextFlags;
			sspiClientContextStatus.CredentialsHandle = safeFreeCredentials;
			if (!SNIProxy.IsErrorStatus(securityStatusPal.ErrorCode))
			{
				return;
			}
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.InternalError)
			{
				throw new Exception(SQLMessage.KerberosTicketMissingError() + "\n" + securityStatusPal);
			}
			throw new Exception(SQLMessage.SSPIGenerateError() + "\n" + securityStatusPal);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000849A6 File Offset: 0x00082BA6
		private static bool IsErrorStatus(SecurityStatusPalErrorCode errorCode)
		{
			return errorCode != SecurityStatusPalErrorCode.NotSet && errorCode != SecurityStatusPalErrorCode.OK && errorCode != SecurityStatusPalErrorCode.ContinueNeeded && errorCode != SecurityStatusPalErrorCode.CompleteNeeded && errorCode != SecurityStatusPalErrorCode.CompAndContinue && errorCode != SecurityStatusPalErrorCode.ContextExpired && errorCode != SecurityStatusPalErrorCode.CredentialsNeeded && errorCode != SecurityStatusPalErrorCode.Renegotiate;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		public uint InitializeSspiPackage(ref uint maxLength)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000849CC File Offset: 0x00082BCC
		public uint SetConnectionBufferSize(SNIHandle handle, uint bufferSize)
		{
			handle.SetBufferSize((int)bufferSize);
			return 0U;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000849D8 File Offset: 0x00082BD8
		public uint PacketGetData(SNIPacket packet, byte[] inBuff, ref uint dataSize)
		{
			int num = 0;
			packet.GetData(inBuff, ref num);
			dataSize = (uint)num;
			return 0U;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000849F4 File Offset: 0x00082BF4
		public uint ReadSyncOverAsync(SNIHandle handle, out SNIPacket packet, int timeout)
		{
			return handle.Receive(out packet, timeout);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000849FE File Offset: 0x00082BFE
		public uint GetConnectionId(SNIHandle handle, ref Guid clientConnectionId)
		{
			clientConnectionId = handle.ConnectionId;
			return 0U;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00084A0D File Offset: 0x00082C0D
		public uint WritePacket(SNIHandle handle, SNIPacket packet, bool sync)
		{
			if (sync)
			{
				return handle.Send(packet.Clone());
			}
			return handle.SendAsync(packet.Clone(), null);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00084A2C File Offset: 0x00082C2C
		public SNIHandle CreateConnectionHandle(object callbackObject, string fullServerName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			instanceName = new byte[1];
			bool flag;
			string localDBDataSource = this.GetLocalDBDataSource(fullServerName, out flag);
			if (flag)
			{
				return null;
			}
			fullServerName = localDBDataSource ?? fullServerName;
			DataSource dataSource = DataSource.ParseServerName(fullServerName);
			if (dataSource == null)
			{
				return null;
			}
			SNIHandle snihandle = null;
			switch (dataSource.ConnectionProtocol)
			{
			case DataSource.Protocol.TCP:
			case DataSource.Protocol.None:
			case DataSource.Protocol.Admin:
				snihandle = this.CreateTcpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			case DataSource.Protocol.NP:
				snihandle = this.CreateNpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			}
			if (isIntegratedSecurity)
			{
				try
				{
					spnBuffer = SNIProxy.GetSqlServerSPN(dataSource);
				}
				catch (Exception ex)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 44U, ex);
				}
			}
			return snihandle;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00084AE0 File Offset: 0x00082CE0
		private static byte[] GetSqlServerSPN(DataSource dataSource)
		{
			string serverName = dataSource.ServerName;
			string text = null;
			if (dataSource.Port != -1)
			{
				text = dataSource.Port.ToString();
			}
			else if (!string.IsNullOrWhiteSpace(dataSource.InstanceName))
			{
				text = dataSource.InstanceName;
			}
			else if (dataSource.ConnectionProtocol == DataSource.Protocol.TCP)
			{
				text = 1433.ToString();
			}
			return SNIProxy.GetSqlServerSPN(serverName, text);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00084B44 File Offset: 0x00082D44
		private static byte[] GetSqlServerSPN(string hostNameOrAddress, string portOrInstanceName)
		{
			string hostName = Dns.GetHostEntry(hostNameOrAddress).HostName;
			string text = "MSSQLSvc/" + hostName;
			if (!string.IsNullOrWhiteSpace(portOrInstanceName))
			{
				text = text + ":" + portOrInstanceName;
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00084B8C File Offset: 0x00082D8C
		private SNITCPHandle CreateTcpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			string serverName = details.ServerName;
			if (string.IsNullOrWhiteSpace(serverName))
			{
				SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 25U, string.Empty);
				return null;
			}
			int num = -1;
			bool flag = details.ConnectionProtocol == DataSource.Protocol.Admin;
			if (details.IsSsrpRequired)
			{
				try
				{
					num = (flag ? SSRP.GetDacPortByInstanceName(serverName, details.InstanceName) : SSRP.GetPortByInstanceName(serverName, details.InstanceName));
					goto IL_0098;
				}
				catch (SocketException ex)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 25U, ex);
					return null;
				}
			}
			if (details.Port != -1)
			{
				num = details.Port;
			}
			else
			{
				num = (flag ? 1434 : 1433);
			}
			IL_0098:
			return new SNITCPHandle(serverName, num, timerExpire, callbackObject, parallel);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00084C50 File Offset: 0x00082E50
		private SNINpHandle CreateNpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			if (parallel)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 49U, string.Empty);
				return null;
			}
			return new SNINpHandle(details.PipeHostName, details.PipeName, timerExpire, callbackObject);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00084C7A File Offset: 0x00082E7A
		public uint ReadAsync(SNIHandle handle, out SNIPacket packet)
		{
			packet = new SNIPacket(null);
			return handle.ReceiveAsync(ref packet);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00084C8B File Offset: 0x00082E8B
		public void PacketSetData(SNIPacket packet, byte[] data, int length)
		{
			packet.SetData(data, length);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00084C95 File Offset: 0x00082E95
		public void PacketRelease(SNIPacket packet)
		{
			packet.Release();
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00084C9D File Offset: 0x00082E9D
		public uint CheckConnection(SNIHandle handle)
		{
			return handle.CheckConnection();
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00084CA5 File Offset: 0x00082EA5
		public SNIError GetLastError()
		{
			return SNILoadHandle.SingletonInstance.LastError;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00084CB4 File Offset: 0x00082EB4
		private string GetLocalDBDataSource(string fullServerName, out bool error)
		{
			string text = null;
			bool flag;
			string localDBInstance = DataSource.GetLocalDBInstance(fullServerName, out flag);
			if (flag)
			{
				error = true;
				return null;
			}
			if (!string.IsNullOrEmpty(localDBInstance))
			{
				text = LocalDB.GetLocalDBConnectionString(localDBInstance);
				if (fullServerName == null)
				{
					error = true;
					return null;
				}
			}
			error = false;
			return text;
		}

		// Token: 0x040012D1 RID: 4817
		private const int DefaultSqlServerPort = 1433;

		// Token: 0x040012D2 RID: 4818
		private const int DefaultSqlServerDacPort = 1434;

		// Token: 0x040012D3 RID: 4819
		private const string SqlServerSpnHeader = "MSSQLSvc";

		// Token: 0x040012D4 RID: 4820
		public static readonly SNIProxy Singleton = new SNIProxy();

		// Token: 0x02000251 RID: 593
		internal class SspiClientContextResult
		{
			// Token: 0x040012D5 RID: 4821
			internal const uint OK = 0U;

			// Token: 0x040012D6 RID: 4822
			internal const uint Failed = 1U;

			// Token: 0x040012D7 RID: 4823
			internal const uint KerberosTicketMissing = 2U;
		}
	}
}
