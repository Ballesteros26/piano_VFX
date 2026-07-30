using System;
using System.Collections;
using System.IO;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Mono.Net.Security.Private;
using Mono.Security.Authenticode;
using Mono.Security.Interface;

namespace System.Net
{
	/// <summary>Provides a simple, programmatically controlled HTTP protocol listener. This class cannot be inherited.</summary>
	// Token: 0x0200051F RID: 1311
	public sealed class HttpListener : IDisposable
	{
		// Token: 0x060027C1 RID: 10177 RVA: 0x0009986D File Offset: 0x00097A6D
		internal HttpListener(X509Certificate certificate, MonoTlsProvider tlsProvider, MonoTlsSettings tlsSettings)
			: this()
		{
			this.certificate = certificate;
			this.tlsProvider = tlsProvider;
			this.tlsSettings = tlsSettings;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0009988C File Offset: 0x00097A8C
		internal X509Certificate LoadCertificateAndKey(IPAddress addr, int port)
		{
			object internalLock = this._internalLock;
			X509Certificate x509Certificate;
			lock (internalLock)
			{
				if (this.certificate != null)
				{
					x509Certificate = this.certificate;
				}
				else
				{
					try
					{
						string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						text = Path.Combine(text, "httplistener");
						string text2 = Path.Combine(text, string.Format("{0}.cer", port));
						if (!File.Exists(text2))
						{
							x509Certificate = null;
						}
						else
						{
							string text3 = Path.Combine(text, string.Format("{0}.pvk", port));
							if (!File.Exists(text3))
							{
								x509Certificate = null;
							}
							else
							{
								this.certificate = new X509Certificate2(text2)
								{
									PrivateKey = PrivateKey.CreateFromFile(text3).RSA
								};
								x509Certificate = this.certificate;
							}
						}
					}
					catch
					{
						this.certificate = null;
						x509Certificate = null;
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00099988 File Offset: 0x00097B88
		internal SslStream CreateSslStream(Stream innerStream, bool ownsStream, RemoteCertificateValidationCallback callback)
		{
			object internalLock = this._internalLock;
			SslStream sslStream;
			lock (internalLock)
			{
				if (this.tlsProvider == null)
				{
					this.tlsProvider = MonoTlsProviderFactory.GetProvider();
				}
				MonoTlsSettings monoTlsSettings = (this.tlsSettings ?? MonoTlsSettings.DefaultSettings).Clone();
				monoTlsSettings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(callback);
				sslStream = new SslStream(innerStream, ownsStream, this.tlsProvider, monoTlsSettings);
			}
			return sslStream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListener" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">This class cannot be used on the current operating system. Windows Server 2003 or Windows XP SP2 is required to use instances of this class.</exception>
		// Token: 0x060027C4 RID: 10180 RVA: 0x00099A08 File Offset: 0x00097C08
		public HttpListener()
		{
			this._internalLock = new object();
			this.prefixes = new HttpListenerPrefixCollection(this);
			this.registry = new Hashtable();
			this.connections = Hashtable.Synchronized(new Hashtable());
			this.ctx_queue = new ArrayList();
			this.wait_queue = new ArrayList();
			this.auth_schemes = AuthenticationSchemes.Anonymous;
			this.defaultServiceNames = new ServiceNameStore();
			this.extendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
		}

		/// <summary>Gets or sets the scheme used to authenticate clients.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Net.AuthenticationSchemes" /> enumeration values that indicates how clients are to be authenticated. The default value is <see cref="F:System.Net.AuthenticationSchemes.Anonymous" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00099A85 File Offset: 0x00097C85
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x00099A8D File Offset: 0x00097C8D
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.auth_schemes;
			}
			set
			{
				this.CheckDisposed();
				this.auth_schemes = value;
			}
		}

		/// <summary>Gets or sets the delegate called to determine the protocol used to authenticate clients.</summary>
		/// <returns>An <see cref="T:System.Net.AuthenticationSchemeSelector" /> delegate that invokes the method used to select an authentication protocol. The default value is null.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x00099A9C File Offset: 0x00097C9C
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x00099AA4 File Offset: 0x00097CA4
		public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
		{
			get
			{
				return this.auth_selector;
			}
			set
			{
				this.CheckDisposed();
				this.auth_selector = value;
			}
		}

		/// <summary>Get or set the delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each request. </summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property must be null. </exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property on a platform that does not support extended protection.</exception>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00099AB3 File Offset: 0x00097CB3
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x00099ABB File Offset: 0x00097CBB
		public HttpListener.ExtendedProtectionSelector ExtendedProtectionSelectorDelegate
		{
			get
			{
				return this.extendedProtectionSelectorDelegate;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection)
				{
					throw new PlatformNotSupportedException(global::SR.GetString("This operation requires OS support for extended protection."));
				}
				this.extendedProtectionSelectorDelegate = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether your application receives exceptions that occur when an <see cref="T:System.Net.HttpListener" /> sends the response to the client.</summary>
		/// <returns>true if this <see cref="T:System.Net.HttpListener" /> should not return exceptions that occur when sending the response to the client; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00099AEA File Offset: 0x00097CEA
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x00099AF2 File Offset: 0x00097CF2
		public bool IgnoreWriteExceptions
		{
			get
			{
				return this.ignore_write_exceptions;
			}
			set
			{
				this.CheckDisposed();
				this.ignore_write_exceptions = value;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> has been started.</summary>
		/// <returns>true if the <see cref="T:System.Net.HttpListener" /> was started; otherwise, false.</returns>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x00099B01 File Offset: 0x00097D01
		public bool IsListening
		{
			get
			{
				return this.listening;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> can be used with the current operating system.</summary>
		/// <returns>true if <see cref="T:System.Net.HttpListener" /> is supported; otherwise, false.</returns>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x000027E2 File Offset: 0x000009E2
		public static bool IsSupported
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) prefixes handled by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerPrefixCollection" /> that contains the URI prefixes that this <see cref="T:System.Net.HttpListener" /> object is configured to handle. </returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x00099B09 File Offset: 0x00097D09
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				this.CheckDisposed();
				return this.prefixes;
			}
		}

		/// <summary>The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.HttpListenerTimeoutManager" />.The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public HttpListenerTimeoutManager TimeoutManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Get or set the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for extended protection for a session. </summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property was not null. </exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.PolicyEnforcement" /> property was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x00099B17 File Offset: 0x00097D17
		// (set) Token: 0x060027D2 RID: 10194 RVA: 0x00099B20 File Offset: 0x00097D20
		[MonoTODO("not used anywhere in the implementation")]
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection && value.PolicyEnforcement == PolicyEnforcement.Always)
				{
					throw new PlatformNotSupportedException(global::SR.GetString("This operation requires OS support for extended protection."));
				}
				if (value.CustomChannelBinding != null)
				{
					throw new ArgumentException(global::SR.GetString("Custom channel bindings are not supported."), "CustomChannelBinding");
				}
				this.extendedProtectionPolicy = value;
			}
		}

		/// <summary>Gets a default list of Service Provider Names (SPNs) as determined by registered prefixes.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains a list of SPNs.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x00099B85 File Offset: 0x00097D85
		public ServiceNameCollection DefaultServiceNames
		{
			get
			{
				return this.defaultServiceNames.ServiceNames;
			}
		}

		/// <summary>Gets or sets the realm, or resource partition, associated with this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the name of the realm associated with the <see cref="T:System.Net.HttpListener" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x00099B92 File Offset: 0x00097D92
		// (set) Token: 0x060027D5 RID: 10197 RVA: 0x00099B9A File Offset: 0x00097D9A
		public string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				this.CheckDisposed();
				this.realm = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether, when NTLM is used, additional requests using the same Transmission Control Protocol (TCP) connection are required to authenticate.</summary>
		/// <returns>true if the <see cref="T:System.Security.Principal.IIdentity" /> of the first request will be used for subsequent requests on the same connection; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00099BA9 File Offset: 0x00097DA9
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x00099BB1 File Offset: 0x00097DB1
		[MonoTODO("Support for NTLM needs some loving.")]
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.unsafe_ntlm_auth;
			}
			set
			{
				this.CheckDisposed();
				this.unsafe_ntlm_auth = value;
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" /> object immediately, discarding all currently queued requests.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027D8 RID: 10200 RVA: 0x00099BC0 File Offset: 0x00097DC0
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027D9 RID: 10201 RVA: 0x00099BDB File Offset: 0x00097DDB
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				this.disposed = true;
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x00099C04 File Offset: 0x00097E04
		private void Close(bool force)
		{
			this.CheckDisposed();
			EndPointManager.RemoveListener(this);
			this.Cleanup(force);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00099C1C File Offset: 0x00097E1C
		private void Cleanup(bool close_existing)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				if (close_existing)
				{
					ICollection keys = this.registry.Keys;
					HttpListenerContext[] array = new HttpListenerContext[keys.Count];
					keys.CopyTo(array, 0);
					this.registry.Clear();
					for (int i = array.Length - 1; i >= 0; i--)
					{
						array[i].Connection.Close(true);
					}
				}
				object syncRoot = this.connections.SyncRoot;
				lock (syncRoot)
				{
					ICollection keys2 = this.connections.Keys;
					HttpConnection[] array2 = new HttpConnection[keys2.Count];
					keys2.CopyTo(array2, 0);
					this.connections.Clear();
					for (int j = array2.Length - 1; j >= 0; j--)
					{
						array2[j].Close(true);
					}
				}
				ArrayList arrayList = this.ctx_queue;
				lock (arrayList)
				{
					HttpListenerContext[] array3 = (HttpListenerContext[])this.ctx_queue.ToArray(typeof(HttpListenerContext));
					this.ctx_queue.Clear();
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						array3[k].Connection.Close(true);
					}
				}
				arrayList = this.wait_queue;
				lock (arrayList)
				{
					Exception ex = new ObjectDisposedException("listener");
					foreach (object obj in this.wait_queue)
					{
						((ListenerAsyncResult)obj).Complete(ex);
					}
					this.wait_queue.Clear();
				}
			}
		}

		/// <summary>Begins asynchronously retrieving an incoming request.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when a client request is available.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060027DC RID: 10204 RVA: 0x00099E68 File Offset: 0x00098068
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Please, call Start before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = new ListenerAsyncResult(callback, state);
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				ArrayList arrayList2 = this.ctx_queue;
				lock (arrayList2)
				{
					HttpListenerContext contextFromQueue = this.GetContextFromQueue();
					if (contextFromQueue != null)
					{
						listenerAsyncResult.Complete(contextFromQueue, true);
						return listenerAsyncResult;
					}
				}
				this.wait_queue.Add(listenerAsyncResult);
			}
			return listenerAsyncResult;
		}

		/// <summary>Completes an asynchronous operation to retrieve an incoming client request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents the client request.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that was obtained when the asynchronous operation was started.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling the <see cref="M:System.Net.HttpListener.BeginGetContext(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpListener.EndGetContext(System.IAsyncResult)" /> method was already called for the specified <paramref name="asyncResult" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060027DD RID: 10205 RVA: 0x00099F1C File Offset: 0x0009811C
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ListenerAsyncResult listenerAsyncResult = asyncResult as ListenerAsyncResult;
			if (listenerAsyncResult == null)
			{
				throw new ArgumentException("Wrong IAsyncResult.", "asyncResult");
			}
			if (listenerAsyncResult.EndCalled)
			{
				throw new ArgumentException("Cannot reuse this IAsyncResult");
			}
			listenerAsyncResult.EndCalled = true;
			if (!listenerAsyncResult.IsCompleted)
			{
				listenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				int num = this.wait_queue.IndexOf(listenerAsyncResult);
				if (num >= 0)
				{
					this.wait_queue.RemoveAt(num);
				}
			}
			HttpListenerContext context = listenerAsyncResult.GetContext();
			context.ParseAuthentication(this.SelectAuthenticationScheme(context));
			return context;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00099FE8 File Offset: 0x000981E8
		internal AuthenticationSchemes SelectAuthenticationScheme(HttpListenerContext context)
		{
			if (this.AuthenticationSchemeSelectorDelegate != null)
			{
				return this.AuthenticationSchemeSelectorDelegate(context.Request);
			}
			return this.auth_schemes;
		}

		/// <summary>Waits for an incoming request and returns when one is received.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.-or-The <see cref="T:System.Net.HttpListener" /> does not have any Uniform Resource Identifier (URI) prefixes to respond to. See Remarks.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060027DF RID: 10207 RVA: 0x0009A00C File Offset: 0x0009820C
		public HttpListenerContext GetContext()
		{
			if (this.prefixes.Count == 0)
			{
				throw new InvalidOperationException("Please, call AddPrefix before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)this.BeginGetContext(null, null);
			listenerAsyncResult.InGet = true;
			return this.EndGetContext(listenerAsyncResult);
		}

		/// <summary>Allows this instance to receive incoming requests.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027E0 RID: 10208 RVA: 0x0009A04D File Offset: 0x0009824D
		public void Start()
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			EndPointManager.AddListener(this);
			this.listening = true;
		}

		/// <summary>Causes this instance to stop receiving incoming requests.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027E1 RID: 10209 RVA: 0x0009A06B File Offset: 0x0009826B
		public void Stop()
		{
			this.CheckDisposed();
			this.listening = false;
			this.Close(false);
		}

		/// <summary>Releases the resources held by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		// Token: 0x060027E2 RID: 10210 RVA: 0x0009A081 File Offset: 0x00098281
		void IDisposable.Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		/// <summary>Waits for an incoming request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		// Token: 0x060027E3 RID: 10211 RVA: 0x0009A09A File Offset: 0x0009829A
		public Task<HttpListenerContext> GetContextAsync()
		{
			return Task<HttpListenerContext>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetContext), new Func<IAsyncResult, HttpListenerContext>(this.EndGetContext), null);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0009A0BF File Offset: 0x000982BF
		internal void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0009A0DA File Offset: 0x000982DA
		private HttpListenerContext GetContextFromQueue()
		{
			if (this.ctx_queue.Count == 0)
			{
				return null;
			}
			HttpListenerContext httpListenerContext = (HttpListenerContext)this.ctx_queue[0];
			this.ctx_queue.RemoveAt(0);
			return httpListenerContext;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0009A108 File Offset: 0x00098308
		internal void RegisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry[context] = context;
			}
			ListenerAsyncResult listenerAsyncResult = null;
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				if (this.wait_queue.Count == 0)
				{
					ArrayList arrayList2 = this.ctx_queue;
					lock (arrayList2)
					{
						this.ctx_queue.Add(context);
						goto IL_00A3;
					}
				}
				listenerAsyncResult = (ListenerAsyncResult)this.wait_queue[0];
				this.wait_queue.RemoveAt(0);
			}
			IL_00A3:
			if (listenerAsyncResult != null)
			{
				listenerAsyncResult.Complete(context);
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0009A1EC File Offset: 0x000983EC
		internal void UnregisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry.Remove(context);
			}
			ArrayList arrayList = this.ctx_queue;
			lock (arrayList)
			{
				int num = this.ctx_queue.IndexOf(context);
				if (num >= 0)
				{
					this.ctx_queue.RemoveAt(num);
				}
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0009A278 File Offset: 0x00098478
		internal void AddConnection(HttpConnection cnc)
		{
			this.connections[cnc] = cnc;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0009A287 File Offset: 0x00098487
		internal void RemoveConnection(HttpConnection cnc)
		{
			this.connections.Remove(cnc);
		}

		// Token: 0x040021A3 RID: 8611
		private MonoTlsProvider tlsProvider;

		// Token: 0x040021A4 RID: 8612
		private MonoTlsSettings tlsSettings;

		// Token: 0x040021A5 RID: 8613
		private X509Certificate certificate;

		// Token: 0x040021A6 RID: 8614
		private AuthenticationSchemes auth_schemes;

		// Token: 0x040021A7 RID: 8615
		private HttpListenerPrefixCollection prefixes;

		// Token: 0x040021A8 RID: 8616
		private AuthenticationSchemeSelector auth_selector;

		// Token: 0x040021A9 RID: 8617
		private string realm;

		// Token: 0x040021AA RID: 8618
		private bool ignore_write_exceptions;

		// Token: 0x040021AB RID: 8619
		private bool unsafe_ntlm_auth;

		// Token: 0x040021AC RID: 8620
		private bool listening;

		// Token: 0x040021AD RID: 8621
		private bool disposed;

		// Token: 0x040021AE RID: 8622
		private readonly object _internalLock;

		// Token: 0x040021AF RID: 8623
		private Hashtable registry;

		// Token: 0x040021B0 RID: 8624
		private ArrayList ctx_queue;

		// Token: 0x040021B1 RID: 8625
		private ArrayList wait_queue;

		// Token: 0x040021B2 RID: 8626
		private Hashtable connections;

		// Token: 0x040021B3 RID: 8627
		private ServiceNameStore defaultServiceNames;

		// Token: 0x040021B4 RID: 8628
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x040021B5 RID: 8629
		private HttpListener.ExtendedProtectionSelector extendedProtectionSelectorDelegate;

		/// <summary>A delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each <see cref="T:System.Net.HttpListener" /> request.</summary>
		/// <returns>An <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object that specifies the extended protection policy to use for this request.</returns>
		/// <param name="request">The <see cref="T:System.Net.HttpListenerRequest" /> to determine the extended protection policy that the <see cref="T:System.Net.HttpListener" /> instance will use to provide extended protection.</param>
		// Token: 0x02000520 RID: 1312
		// (Invoke) Token: 0x060027EB RID: 10219
		public delegate ExtendedProtectionPolicy ExtendedProtectionSelector(HttpListenerRequest request);
	}
}
