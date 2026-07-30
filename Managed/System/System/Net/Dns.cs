using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Mono.Net.Dns;

namespace System.Net
{
	/// <summary>Provides simple domain name resolution functionality.</summary>
	// Token: 0x02000507 RID: 1287
	public static class Dns
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x00094CF4 File Offset: 0x00092EF4
		static Dns()
		{
			if (Environment.GetEnvironmentVariable("MONO_DNS") != null)
			{
				Dns.resolver = new SimpleResolver();
				Dns.use_mono_dns = true;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00094D12 File Offset: 0x00092F12
		internal static bool UseMonoDns
		{
			get
			{
				return Dns.use_mono_dns;
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00094D1C File Offset: 0x00092F1C
		private static void OnCompleted(object sender, SimpleResolverEventArgs e)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)e.UserToken;
			IPHostEntry hostEntry = e.HostEntry;
			if (hostEntry == null || e.ResolverError != ResolverError.NoError)
			{
				dnsAsyncResult.SetCompleted(false, new Exception("Error: " + e.ResolverError));
				return;
			}
			dnsAsyncResult.SetCompleted(false, hostEntry);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00094D74 File Offset: 0x00092F74
		private static IAsyncResult BeginAsyncCallAddresses(string host, AsyncCallback callback, object state)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = new SimpleResolverEventArgs();
			simpleResolverEventArgs.Completed += Dns.OnCompleted;
			simpleResolverEventArgs.HostName = host;
			DnsAsyncResult dnsAsyncResult = new DnsAsyncResult(callback, state);
			simpleResolverEventArgs.UserToken = dnsAsyncResult;
			if (!Dns.resolver.GetHostAddressesAsync(simpleResolverEventArgs))
			{
				dnsAsyncResult.SetCompleted(true, simpleResolverEventArgs.HostEntry);
			}
			return dnsAsyncResult;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00094DCC File Offset: 0x00092FCC
		private static IAsyncResult BeginAsyncCall(string host, AsyncCallback callback, object state)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = new SimpleResolverEventArgs();
			simpleResolverEventArgs.Completed += Dns.OnCompleted;
			simpleResolverEventArgs.HostName = host;
			DnsAsyncResult dnsAsyncResult = new DnsAsyncResult(callback, state);
			simpleResolverEventArgs.UserToken = dnsAsyncResult;
			if (!Dns.resolver.GetHostEntryAsync(simpleResolverEventArgs))
			{
				dnsAsyncResult.SetCompleted(true, simpleResolverEventArgs.HostEntry);
			}
			return dnsAsyncResult;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00094E24 File Offset: 0x00093024
		private static IPHostEntry EndAsyncCall(DnsAsyncResult ares)
		{
			if (ares == null)
			{
				throw new ArgumentException("Invalid asyncResult");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (ares.Exception != null)
			{
				throw ares.Exception;
			}
			IPHostEntry hostEntry = ares.HostEntry;
			if (hostEntry == null || hostEntry.AddressList == null || hostEntry.AddressList.Length == 0)
			{
				Dns.Error_11001(hostEntry.HostName);
			}
			return hostEntry;
		}

		/// <summary>Begins an asynchronous request for <see cref="T:System.Net.IPHostEntry" /> information about the specified DNS host name.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered executing the DNS query. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002685 RID: 9861 RVA: 0x00094E89 File Offset: 0x00093089
		[Obsolete("Use BeginGetHostEntry instead")]
		public static IAsyncResult BeginGetHostByName(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostName, requestCallback, stateObject);
			}
			return new Dns.GetHostByNameCallback(Dns.GetHostByName).BeginInvoke(hostName, requestCallback, stateObject);
		}

		/// <summary>Begins an asynchronous request to resolve a DNS host name or IP address to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The caller does not have permission to access DNS information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002686 RID: 9862 RVA: 0x00094EBD File Offset: 0x000930BD
		[Obsolete("Use BeginGetHostEntry instead")]
		public static IAsyncResult BeginResolve(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostName, requestCallback, stateObject);
			}
			return new Dns.ResolveCallback(Dns.Resolve).BeginInvoke(hostName, requestCallback, stateObject);
		}

		/// <summary>Asynchronously returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06002687 RID: 9863 RVA: 0x00094EF4 File Offset: 0x000930F4
		public static IAsyncResult BeginGetHostAddresses(string hostNameOrAddress, AsyncCallback requestCallback, object state)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCallAddresses(hostNameOrAddress, requestCallback, state);
			}
			return new Dns.GetHostAddressesCallback(Dns.GetHostAddresses).BeginInvoke(hostNameOrAddress, requestCallback, state);
		}

		/// <summary>Asynchronously resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06002688 RID: 9864 RVA: 0x00094F60 File Offset: 0x00093160
		public static IAsyncResult BeginGetHostEntry(string hostNameOrAddress, AsyncCallback requestCallback, object stateObject)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostNameOrAddress, requestCallback, stateObject);
			}
			return new Dns.GetHostEntryNameCallback(Dns.GetHostEntry).BeginInvoke(hostNameOrAddress, requestCallback, stateObject);
		}

		/// <summary>Asynchronously resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="address">The IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x06002689 RID: 9865 RVA: 0x00094FC9 File Offset: 0x000931C9
		public static IAsyncResult BeginGetHostEntry(IPAddress address, AsyncCallback requestCallback, object stateObject)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(address.ToString(), requestCallback, stateObject);
			}
			return new Dns.GetHostEntryIPCallback(Dns.GetHostEntry).BeginInvoke(address, requestCallback, stateObject);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostByName(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600268A RID: 9866 RVA: 0x00095002 File Offset: 0x00093202
		[Obsolete("Use EndGetHostEntry instead")]
		public static IPHostEntry EndGetHostByName(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			return ((Dns.GetHostByNameCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginResolve(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600268B RID: 9867 RVA: 0x0009503B File Offset: 0x0009323B
		[Obsolete("Use EndGetHostEntry instead")]
		public static IPHostEntry EndResolve(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			return ((Dns.ResolveCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host specified by the <paramref name="hostNameOrAddress" /> parameter of <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		// Token: 0x0600268C RID: 9868 RVA: 0x00095074 File Offset: 0x00093274
		public static IPAddress[] EndGetHostAddresses(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!Dns.use_mono_dns)
			{
				return ((Dns.GetHostAddressesCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
			}
			IPHostEntry iphostEntry = Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			if (iphostEntry == null)
			{
				return null;
			}
			return iphostEntry.AddressList;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to an <see cref="Overload:System.Net.Dns.BeginGetHostEntry" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x0600268D RID: 9869 RVA: 0x000950C4 File Offset: 0x000932C4
		public static IPHostEntry EndGetHostEntry(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			if (asyncResult2.AsyncDelegate is Dns.GetHostEntryIPCallback)
			{
				return ((Dns.GetHostEntryIPCallback)asyncResult2.AsyncDelegate).EndInvoke(asyncResult);
			}
			return ((Dns.GetHostEntryNameCallback)asyncResult2.AsyncDelegate).EndInvoke(asyncResult);
		}

		// Token: 0x0600268E RID: 9870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByName_internal(string host, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x0600268F RID: 9871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByAddr_internal(string addr, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x06002690 RID: 9872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostName_internal(out string h_name);

		// Token: 0x06002691 RID: 9873 RVA: 0x00095129 File Offset: 0x00093329
		private static void Error_11001(string hostName)
		{
			throw new SocketException(11001, string.Format("Could not resolve host '{0}'", hostName));
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00095140 File Offset: 0x00093340
		private static IPHostEntry hostent_to_IPHostEntry(string originalHostName, string h_name, string[] h_aliases, string[] h_addrlist)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			ArrayList arrayList = new ArrayList();
			iphostEntry.HostName = h_name;
			iphostEntry.Aliases = h_aliases;
			for (int i = 0; i < h_addrlist.Length; i++)
			{
				try
				{
					IPAddress ipaddress = IPAddress.Parse(h_addrlist[i]);
					if ((Socket.SupportsIPv6 && ipaddress.AddressFamily == AddressFamily.InterNetworkV6) || (Socket.SupportsIPv4 && ipaddress.AddressFamily == AddressFamily.InterNetwork))
					{
						arrayList.Add(ipaddress);
					}
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (arrayList.Count == 0)
			{
				Dns.Error_11001(originalHostName);
			}
			iphostEntry.AddressList = arrayList.ToArray(typeof(IPAddress)) as IPAddress[];
			return iphostEntry;
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from the specified <see cref="T:System.Net.IPAddress" />.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" />.An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002693 RID: 9875 RVA: 0x000951E8 File Offset: 0x000933E8
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByAddress(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from an IP address.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <param name="address">An IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not a valid IP address. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002694 RID: 9876 RVA: 0x00095204 File Offset: 0x00093404
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address, true);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0009521C File Offset: 0x0009341C
		private static IPHostEntry GetHostByAddressFromString(string address, bool parse)
		{
			if (address.Equals("0.0.0.0"))
			{
				address = "127.0.0.1";
				parse = false;
			}
			if (parse)
			{
				IPAddress.Parse(address);
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByAddr_internal(address, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(address);
			}
			return Dns.hostent_to_IPHostEntry(address, text, array, array2);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address. </exception>
		// Token: 0x06002696 RID: 9878 RVA: 0x0009526C File Offset: 0x0009346C
		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return Dns.GetHostEntry(ipaddress);
			}
			return Dns.GetHostByName(hostNameOrAddress);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <param name="address">An IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x06002697 RID: 9879 RVA: 0x000951E8 File Offset: 0x000933E8
		public static IPHostEntry GetHostEntry(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06002698 RID: 9880 RVA: 0x000952D4 File Offset: 0x000934D4
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return new IPAddress[] { ipaddress };
			}
			return Dns.GetHostEntry(hostNameOrAddress).AddressList;
		}

		/// <summary>Gets the DNS information for the specified DNS host name.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains host information for the address specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002699 RID: 9881 RVA: 0x00095344 File Offset: 0x00093544
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByName_internal(hostName, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(hostName);
			}
			return Dns.hostent_to_IPHostEntry(hostName, text, array, array2);
		}

		/// <summary>Gets the host name of the local computer.</summary>
		/// <returns>A string that contains the DNS host name of the local computer.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600269A RID: 9882 RVA: 0x00095384 File Offset: 0x00093584
		public static string GetHostName()
		{
			string text;
			if (!Dns.GetHostName_internal(out text))
			{
				Dns.Error_11001(text);
			}
			return text;
		}

		/// <summary>Resolves a DNS host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">A DNS-style host name or IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600269B RID: 9883 RVA: 0x000953A4 File Offset: 0x000935A4
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry Resolve(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.GetHostByAddress(hostName);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				iphostEntry = Dns.GetHostByName(hostName);
			}
			return iphostEntry;
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x0600269C RID: 9884 RVA: 0x000953E8 File Offset: 0x000935E8
		public static Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress)
		{
			return Task<IPAddress[]>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostAddresses), new Func<IAsyncResult, IPAddress[]>(Dns.EndGetHostAddresses), hostNameOrAddress, null);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <param name="address">An IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x0600269D RID: 9885 RVA: 0x0009540E File Offset: 0x0009360E
		public static Task<IPHostEntry> GetHostEntryAsync(IPAddress address)
		{
			return Task<IPHostEntry>.Factory.FromAsync<IPAddress>(new Func<IPAddress, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), address, null);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address. </exception>
		// Token: 0x0600269E RID: 9886 RVA: 0x00095434 File Offset: 0x00093634
		public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
		{
			return Task<IPHostEntry>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), hostNameOrAddress, null);
		}

		// Token: 0x04002112 RID: 8466
		private static bool use_mono_dns;

		// Token: 0x04002113 RID: 8467
		private static SimpleResolver resolver;

		// Token: 0x02000508 RID: 1288
		// (Invoke) Token: 0x060026A0 RID: 9888
		private delegate IPHostEntry GetHostByNameCallback(string hostName);

		// Token: 0x02000509 RID: 1289
		// (Invoke) Token: 0x060026A4 RID: 9892
		private delegate IPHostEntry ResolveCallback(string hostName);

		// Token: 0x0200050A RID: 1290
		// (Invoke) Token: 0x060026A8 RID: 9896
		private delegate IPHostEntry GetHostEntryNameCallback(string hostName);

		// Token: 0x0200050B RID: 1291
		// (Invoke) Token: 0x060026AC RID: 9900
		private delegate IPHostEntry GetHostEntryIPCallback(IPAddress hostAddress);

		// Token: 0x0200050C RID: 1292
		// (Invoke) Token: 0x060026B0 RID: 9904
		private delegate IPAddress[] GetHostAddressesCallback(string hostName);
	}
}
