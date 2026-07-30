using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000438 RID: 1080
	internal static class NclUtilities
	{
		// Token: 0x0600208D RID: 8333 RVA: 0x0007EC18 File Offset: 0x0007CE18
		internal static bool IsThreadPoolLow()
		{
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return num < 2;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0007EC32 File Offset: 0x0007CE32
		internal static bool HasShutdownStarted
		{
			get
			{
				return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0007EC48 File Offset: 0x0007CE48
		internal static bool IsCredentialFailure(SecurityStatus error)
		{
			return error == SecurityStatus.LogonDenied || error == SecurityStatus.UnknownCredentials || error == SecurityStatus.NoImpersonation || error == SecurityStatus.NoAuthenticatingAuthority || error == SecurityStatus.UntrustedRoot || error == SecurityStatus.CertExpired || error == SecurityStatus.SmartcardLogonRequired || error == SecurityStatus.BadBinding;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0007EC98 File Offset: 0x0007CE98
		internal static bool IsClientFault(SecurityStatus error)
		{
			return error == SecurityStatus.InvalidToken || error == SecurityStatus.CannotPack || error == SecurityStatus.QopNotSupported || error == SecurityStatus.NoCredentials || error == SecurityStatus.MessageAltered || error == SecurityStatus.OutOfSequence || error == SecurityStatus.IncompleteMessage || error == SecurityStatus.IncompleteCredentials || error == SecurityStatus.WrongPrincipal || error == SecurityStatus.TimeSkew || error == SecurityStatus.IllegalMessage || error == SecurityStatus.CertUnknown || error == SecurityStatus.AlgorithmMismatch || error == SecurityStatus.SecurityQosFailed || error == SecurityStatus.UnsupportedPreauth;
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0007ED1F File Offset: 0x0007CF1F
		internal static ContextCallback ContextRelativeDemandCallback
		{
			get
			{
				if (NclUtilities.s_ContextRelativeDemandCallback == null)
				{
					NclUtilities.s_ContextRelativeDemandCallback = new ContextCallback(NclUtilities.DemandCallback);
				}
				return NclUtilities.s_ContextRelativeDemandCallback;
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000027E8 File Offset: 0x000009E8
		private static void DemandCallback(object state)
		{
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0007ED44 File Offset: 0x0007CF44
		internal static bool GuessWhetherHostIsLoopback(string host)
		{
			string text = host.ToLowerInvariant();
			return text == "localhost" || text == "loopback";
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0007ED75 File Offset: 0x0007CF75
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0007ED98 File Offset: 0x0007CF98
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007EDC8 File Offset: 0x0007CFC8
		private static IPHostEntry GetLocalHost()
		{
			return Dns.GetHostByName(Dns.GetHostName());
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0007EDD4 File Offset: 0x0007CFD4
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				IPAddress[] array = NclUtilities._LocalAddresses;
				if (array != null)
				{
					return array;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] array2;
				lock (localAddressesLock)
				{
					array = NclUtilities._LocalAddresses;
					if (array != null)
					{
						array2 = array;
					}
					else
					{
						List<IPAddress> list = new List<IPAddress>();
						try
						{
							IPHostEntry localHost = NclUtilities.GetLocalHost();
							if (localHost != null)
							{
								if (localHost.HostName != null)
								{
									int num = localHost.HostName.IndexOf('.');
									if (num != -1)
									{
										NclUtilities._LocalDomainName = localHost.HostName.Substring(num);
									}
								}
								IPAddress[] addressList = localHost.AddressList;
								if (addressList != null)
								{
									foreach (IPAddress ipaddress in addressList)
									{
										list.Add(ipaddress);
									}
								}
							}
						}
						catch
						{
						}
						array = new IPAddress[list.Count];
						int num2 = 0;
						foreach (IPAddress ipaddress2 in list)
						{
							array[num2] = ipaddress2;
							num2++;
						}
						NclUtilities._LocalAddresses = array;
						array2 = array;
					}
				}
				return array2;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0007EF14 File Offset: 0x0007D114
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x04001CB1 RID: 7345
		private static volatile ContextCallback s_ContextRelativeDemandCallback;

		// Token: 0x04001CB2 RID: 7346
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x04001CB3 RID: 7347
		private static object _LocalAddressesLock;

		// Token: 0x04001CB4 RID: 7348
		private const int HostNameBufferLength = 256;

		// Token: 0x04001CB5 RID: 7349
		internal static string _LocalDomainName;
	}
}
