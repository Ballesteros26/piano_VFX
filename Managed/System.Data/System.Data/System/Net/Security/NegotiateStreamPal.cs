using System;
using System.ComponentModel;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000046 RID: 70
	internal static class NegotiateStreamPal
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
		internal static string QueryContextClientSpecifiedSpn(SafeDeleteContext securityContext)
		{
			throw new PlatformNotSupportedException("Server implementation is not supported.");
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E2BF File Offset: 0x0000C4BF
		internal static string QueryContextAuthenticationPackage(SafeDeleteContext securityContext)
		{
			if (!((SafeDeleteNegoContext)securityContext).IsNtlmUsed)
			{
				return "Kerberos";
			}
			return "NTLM";
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		private static byte[] GssWrap(SafeGssContextHandle context, bool encrypt, byte[] buffer, int offset, int count)
		{
			Interop.NetSecurityNative.GssBuffer gssBuffer = default(Interop.NetSecurityNative.GssBuffer);
			byte[] array;
			try
			{
				Interop.NetSecurityNative.Status status2;
				Interop.NetSecurityNative.Status status = Interop.NetSecurityNative.WrapBuffer(out status2, context, encrypt, buffer, offset, count, ref gssBuffer);
				if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE)
				{
					throw new Interop.NetSecurityNative.GssApiException(status, status2);
				}
				array = gssBuffer.ToByteArray();
			}
			finally
			{
				gssBuffer.Dispose();
			}
			return array;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E330 File Offset: 0x0000C530
		private static int GssUnwrap(SafeGssContextHandle context, byte[] buffer, int offset, int count)
		{
			Interop.NetSecurityNative.GssBuffer gssBuffer = default(Interop.NetSecurityNative.GssBuffer);
			int num;
			try
			{
				Interop.NetSecurityNative.Status status2;
				Interop.NetSecurityNative.Status status = Interop.NetSecurityNative.UnwrapBuffer(out status2, context, buffer, offset, count, ref gssBuffer);
				if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE)
				{
					throw new Interop.NetSecurityNative.GssApiException(status, status2);
				}
				num = gssBuffer.Copy(buffer, offset);
			}
			finally
			{
				gssBuffer.Dispose();
			}
			return num;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E384 File Offset: 0x0000C584
		private static bool GssInitSecurityContext(ref SafeGssContextHandle context, SafeGssCredHandle credential, bool isNtlm, SafeGssNameHandle targetName, Interop.NetSecurityNative.GssFlags inFlags, byte[] buffer, out byte[] outputBuffer, out uint outFlags, out int isNtlmUsed)
		{
			outputBuffer = null;
			outFlags = 0U;
			if (context == null)
			{
				context = new SafeGssContextHandle();
			}
			Interop.NetSecurityNative.GssBuffer gssBuffer = default(Interop.NetSecurityNative.GssBuffer);
			Interop.NetSecurityNative.Status status;
			try
			{
				Interop.NetSecurityNative.Status status2;
				status = Interop.NetSecurityNative.InitSecContext(out status2, credential, ref context, isNtlm, targetName, (uint)inFlags, buffer, (buffer == null) ? 0 : buffer.Length, ref gssBuffer, out outFlags, out isNtlmUsed);
				if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE && status != Interop.NetSecurityNative.Status.GSS_S_CONTINUE_NEEDED)
				{
					throw new Interop.NetSecurityNative.GssApiException(status, status2);
				}
				outputBuffer = gssBuffer.ToByteArray();
			}
			finally
			{
				gssBuffer.Dispose();
			}
			return status == Interop.NetSecurityNative.Status.GSS_S_COMPLETE;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E404 File Offset: 0x0000C604
		private static SecurityStatusPal EstablishSecurityContext(SafeFreeNegoCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlagsPal inFlags, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlagsPal outFlags)
		{
			bool isNtlmOnly = credential.IsNtlmOnly;
			if (context == null)
			{
				context = (isNtlmOnly ? new SafeDeleteNegoContext(credential, credential.UserName) : new SafeDeleteNegoContext(credential, targetName));
			}
			SafeDeleteNegoContext safeDeleteNegoContext = (SafeDeleteNegoContext)context;
			SecurityStatusPal securityStatusPal;
			try
			{
				Interop.NetSecurityNative.GssFlags interopFromContextFlagsPal = ContextFlagsAdapterPal.GetInteropFromContextFlagsPal(inFlags, false);
				SafeGssContextHandle gssContext = safeDeleteNegoContext.GssContext;
				uint num;
				int num2;
				bool flag = NegotiateStreamPal.GssInitSecurityContext(ref gssContext, credential.GssCredential, isNtlmOnly, safeDeleteNegoContext.TargetName, interopFromContextFlagsPal, (inputBuffer != null) ? inputBuffer.token : null, out outputBuffer.token, out num, out num2);
				outputBuffer.size = outputBuffer.token.Length;
				outputBuffer.offset = 0;
				outFlags = ContextFlagsAdapterPal.GetContextFlagsPalFromInterop((Interop.NetSecurityNative.GssFlags)num, false);
				if (safeDeleteNegoContext.GssContext == null)
				{
					safeDeleteNegoContext.SetGssContext(gssContext);
				}
				if (flag)
				{
					safeDeleteNegoContext.SetAuthenticationPackage(Convert.ToBoolean(num2));
				}
				securityStatusPal = new SecurityStatusPal(flag ? ((safeDeleteNegoContext.IsNtlmUsed && outputBuffer.size > 0) ? SecurityStatusPalErrorCode.OK : SecurityStatusPalErrorCode.CompleteNeeded) : SecurityStatusPalErrorCode.ContinueNeeded, null);
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, ex, "EstablishSecurityContext");
				}
				securityStatusPal = new SecurityStatusPal(SecurityStatusPalErrorCode.InternalError, ex);
			}
			return securityStatusPal;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000E518 File Offset: 0x0000C718
		internal static SecurityStatusPal InitializeSecurityContext(SafeFreeCredentials credentialsHandle, ref SafeDeleteContext securityContext, string spn, ContextFlagsPal requestedContextFlags, SecurityBuffer[] inSecurityBufferArray, SecurityBuffer outSecurityBuffer, ref ContextFlagsPal contextFlags)
		{
			if (inSecurityBufferArray != null && inSecurityBufferArray.Length > 1)
			{
				throw new PlatformNotSupportedException("No support for channel binding on operating systems other than Windows.");
			}
			SafeFreeNegoCredentials safeFreeNegoCredentials = (SafeFreeNegoCredentials)credentialsHandle;
			if (safeFreeNegoCredentials.IsDefault && string.IsNullOrEmpty(spn))
			{
				throw new PlatformNotSupportedException("Target name should be non-empty if default credentials are passed.");
			}
			SecurityStatusPal securityStatusPal = NegotiateStreamPal.EstablishSecurityContext(safeFreeNegoCredentials, ref securityContext, spn, requestedContextFlags, (inSecurityBufferArray != null && inSecurityBufferArray.Length != 0) ? inSecurityBufferArray[0] : null, outSecurityBuffer, ref contextFlags);
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded)
			{
				ContextFlagsPal contextFlagsPal = ContextFlagsPal.Confidentiality;
				if ((requestedContextFlags & contextFlagsPal) != (contextFlags & contextFlagsPal))
				{
					throw new PlatformNotSupportedException("Requested protection level is not supported with the GSSAPI implementation currently installed.");
				}
			}
			return securityStatusPal;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
		internal static SecurityStatusPal AcceptSecurityContext(SafeFreeCredentials credentialsHandle, ref SafeDeleteContext securityContext, ContextFlagsPal requestedContextFlags, SecurityBuffer[] inSecurityBufferArray, SecurityBuffer outSecurityBuffer, ref ContextFlagsPal contextFlags)
		{
			throw new PlatformNotSupportedException("Server implementation is not supported.");
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000E59C File Offset: 0x0000C79C
		internal static Win32Exception CreateExceptionFromError(SecurityStatusPal statusCode)
		{
			return new Win32Exception(-2146893792, (statusCode.Exception != null) ? statusCode.Exception.Message : statusCode.ErrorCode.ToString());
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000061D5 File Offset: 0x000043D5
		internal static int QueryMaxTokenSize(string package)
		{
			return 0;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		internal static SafeFreeCredentials AcquireDefaultCredential(string package, bool isServer)
		{
			return NegotiateStreamPal.AcquireCredentialsHandle(package, isServer, new NetworkCredential(string.Empty, string.Empty, string.Empty));
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		internal static SafeFreeCredentials AcquireCredentialsHandle(string package, bool isServer, NetworkCredential credential)
		{
			if (isServer)
			{
				throw new PlatformNotSupportedException("Server implementation is not supported.");
			}
			bool flag = string.IsNullOrWhiteSpace(credential.UserName) || string.IsNullOrWhiteSpace(credential.Password);
			bool flag2 = string.Equals(package, "NTLM", StringComparison.OrdinalIgnoreCase);
			if (flag2 && flag)
			{
				throw new PlatformNotSupportedException("NTLM authentication is not possible with default credentials on this platform.");
			}
			SafeFreeCredentials safeFreeCredentials;
			try
			{
				safeFreeCredentials = (flag ? new SafeFreeNegoCredentials(false, string.Empty, string.Empty, string.Empty) : new SafeFreeNegoCredentials(flag2, credential.UserName, credential.Password, credential.Domain));
			}
			catch (Exception ex)
			{
				throw new Win32Exception(-2146893792, ex.Message);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		internal static SecurityStatusPal CompleteAuthToken(ref SafeDeleteContext securityContext, SecurityBuffer[] inSecurityBufferArray)
		{
			return new SecurityStatusPal(SecurityStatusPalErrorCode.OK, null);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		internal static int Encrypt(SafeDeleteContext securityContext, byte[] buffer, int offset, int count, bool isConfidential, bool isNtlm, ref byte[] output, uint sequenceNumber)
		{
			byte[] array = NegotiateStreamPal.GssWrap(((SafeDeleteNegoContext)securityContext).GssContext, isConfidential, buffer, offset, count);
			output = new byte[array.Length + 4];
			Array.Copy(array, 0, output, 4, array.Length);
			int num = array.Length;
			output[0] = (byte)(num & 255);
			output[1] = (byte)((num >> 8) & 255);
			output[2] = (byte)((num >> 16) & 255);
			output[3] = (byte)((num >> 24) & 255);
			return num + 4;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000E73C File Offset: 0x0000C93C
		internal static int Decrypt(SafeDeleteContext securityContext, byte[] buffer, int offset, int count, bool isConfidential, bool isNtlm, out int newOffset, uint sequenceNumber)
		{
			if (offset < 0 || offset > ((buffer == null) ? 0 : buffer.Length))
			{
				NetEventSource.Fail(securityContext, "Argument 'offset' out of range", "Decrypt");
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((buffer == null) ? 0 : (buffer.Length - offset)))
			{
				NetEventSource.Fail(securityContext, "Argument 'count' out of range.", "Decrypt");
				throw new ArgumentOutOfRangeException("count");
			}
			newOffset = offset;
			return NegotiateStreamPal.GssUnwrap(((SafeDeleteNegoContext)securityContext).GssContext, buffer, offset, count);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		internal static int VerifySignature(SafeDeleteContext securityContext, byte[] buffer, int offset, int count)
		{
			if (offset < 0 || offset > ((buffer == null) ? 0 : buffer.Length))
			{
				NetEventSource.Fail(securityContext, "Argument 'offset' out of range", "VerifySignature");
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((buffer == null) ? 0 : (buffer.Length - offset)))
			{
				NetEventSource.Fail(securityContext, "Argument 'count' out of range.", "VerifySignature");
				throw new ArgumentOutOfRangeException("count");
			}
			return NegotiateStreamPal.GssUnwrap(((SafeDeleteNegoContext)securityContext).GssContext, buffer, offset, count);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000E830 File Offset: 0x0000CA30
		internal static int MakeSignature(SafeDeleteContext securityContext, byte[] buffer, int offset, int count, ref byte[] output)
		{
			byte[] array = NegotiateStreamPal.GssWrap(((SafeDeleteNegoContext)securityContext).GssContext, false, buffer, offset, count);
			output = new byte[array.Length + 4];
			Array.Copy(array, 0, output, 4, array.Length);
			int num = array.Length;
			output[0] = (byte)(num & 255);
			output[1] = (byte)((num >> 8) & 255);
			output[2] = (byte)((num >> 16) & 255);
			output[3] = (byte)((num >> 24) & 255);
			return num + 4;
		}

		// Token: 0x040004B9 RID: 1209
		private const int NTE_FAIL = -2146893792;
	}
}
