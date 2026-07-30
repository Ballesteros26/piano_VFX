using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x020003B1 RID: 945
	public class X509Chain : IDisposable
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0007202C File Offset: 0x0007022C
		internal X509ChainImpl Impl
		{
			get
			{
				X509Helper2.ThrowIfContextInvalid(this.impl);
				return this.impl;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0007203F File Offset: 0x0007023F
		internal bool IsValid
		{
			get
			{
				return X509Helper2.IsValid(this.impl);
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0007204C File Offset: 0x0007024C
		internal void ThrowIfContextInvalid()
		{
			X509Helper2.ThrowIfContextInvalid(this.impl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x06001CBC RID: 7356 RVA: 0x00072059 File Offset: 0x00070259
		public X509Chain()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">true to use the machine context; false to use the current user context. </param>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00072062 File Offset: 0x00070262
		public X509Chain(bool useMachineContext)
		{
			this.impl = X509Helper2.CreateChainImpl(useMachineContext);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00072076 File Offset: 0x00070276
		internal X509Chain(X509ChainImpl impl)
		{
			X509Helper2.ThrowIfContextInvalid(impl);
			this.impl = impl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x06001CBF RID: 7359 RVA: 0x0007208B File Offset: 0x0007028B
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509Chain(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a handle to an X.509 chain.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00072098 File Offset: 0x00070298
		[MonoTODO("Mono's X509Chain is fully managed. Always returns IntPtr.Zero.")]
		public IntPtr ChainContext
		{
			get
			{
				if (this.impl != null && this.impl.IsValid)
				{
					return this.impl.Handle;
				}
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x000720C0 File Offset: 0x000702C0
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.Impl.ChainElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000720CD File Offset: 0x000702CD
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x000720DA File Offset: 0x000702DA
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.Impl.ChainPolicy;
			}
			set
			{
				this.Impl.ChainPolicy = value;
			}
		}

		/// <summary>Gets the status of each element in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000720E8 File Offset: 0x000702E8
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				return this.Impl.ChainStatus;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00004239 File Offset: 0x00002439
		public SafeX509ChainHandle SafeHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <returns>true if the X.509 certificate is valid; otherwise, false.</returns>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable. </exception>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x000720F5 File Offset: 0x000702F5
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public bool Build(X509Certificate2 certificate)
		{
			return this.Impl.Build(certificate);
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x06001CC7 RID: 7367 RVA: 0x00072103 File Offset: 0x00070303
		public void Reset()
		{
			this.Impl.Reset();
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00072110 File Offset: 0x00070310
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00072121 File Offset: 0x00070321
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00072130 File Offset: 0x00070330
		protected virtual void Dispose(bool disposing)
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0007214C File Offset: 0x0007034C
		~X509Chain()
		{
			this.Dispose(false);
		}

		// Token: 0x040019A2 RID: 6562
		private X509ChainImpl impl;
	}
}
