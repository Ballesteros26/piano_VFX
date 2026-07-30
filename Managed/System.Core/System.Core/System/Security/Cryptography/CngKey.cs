using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	/// <summary>Defines the core functionality for keys that are used with Cryptography Next Generation (CNG) objects.</summary>
	// Token: 0x02000063 RID: 99
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngKey : IDisposable
	{
		/// <summary>Gets the algorithm group that is used by the key.</summary>
		/// <returns>An object that specifies the name of an encryption algorithm group.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000227E File Offset: 0x0000047E
		public CngAlgorithmGroup AlgorithmGroup
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the algorithm that is used by the key.</summary>
		/// <returns>An object that specifies the name of an encryption algorithm.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000227E File Offset: 0x0000047E
		public CngAlgorithm Algorithm
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the export policy that is used by the key.</summary>
		/// <returns>An object that specifies the export policy for the key.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000227E File Offset: 0x0000047E
		public CngExportPolicies ExportPolicy
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a safe handle that represents a native key (NCRYPT_KEY_HANDLE). </summary>
		/// <returns>A safe handle that represents the key.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000227E File Offset: 0x0000047E
		public SafeNCryptKeyHandle Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the persistence state of the key.</summary>
		/// <returns>true if the key is ephemeral; otherwise, false. </returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsEphemeral
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			[SecurityCritical]
			private set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the scope (machine or user) of the key.</summary>
		/// <returns>true if the key is available on a machine-wide basis; false if the key is only for the current user.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsMachineKey
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the key.</summary>
		/// <returns>The name of the key. If the key is ephemeral, the value is null.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000227E File Offset: 0x0000047E
		public string KeyName
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the key size in bits.</summary>
		/// <returns>The key size in bits.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000227E File Offset: 0x0000047E
		public int KeySize
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the cryptographic operations specified by the key.</summary>
		/// <returns>A bitwise combination of the enumeration values that specify the usages allowed for the key.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000227E File Offset: 0x0000047E
		public CngKeyUsages KeyUsage
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the window handle (HWND) that should be used for user interface (UI) prompts caused by accessing the key.</summary>
		/// <returns>The parent window handle for the key.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000227E File Offset: 0x0000047E
		public IntPtr ParentWindowHandle
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the key storage provider (KSP) that manages the key.</summary>
		/// <returns>The KSP that manages the key.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000227E File Offset: 0x0000047E
		public CngProvider Provider
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a native handle (an NCRYPT_PROV_HANDLE) to the key storage provider (KSP).</summary>
		/// <returns>A handle to the KSP.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000227E File Offset: 0x0000047E
		public SafeNCryptProviderHandle ProviderHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the unique name for the key.</summary>
		/// <returns>An alternate name for the key. If the key is ephemeral, the value is null.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000227E File Offset: 0x0000047E
		public string UniqueName
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets parameters that control the user interface (UI) for accessing the key.  </summary>
		/// <returns>An object that contains configuration parameters for displaying the UI.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000227E File Offset: 0x0000047E
		public CngUIPolicy UIPolicy
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a <see cref="T:System.Security.Cryptography.CngKey" /> object that can be used with the specified algorithm.</summary>
		/// <returns>An ephemeral key.</returns>
		/// <param name="algorithm">The algorithm that the key will be used with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="algorithm" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600021C RID: 540 RVA: 0x0000227E File Offset: 0x0000047E
		public static CngKey Create(CngAlgorithm algorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a named <see cref="T:System.Security.Cryptography.CngKey" /> object that provides the specified algorithm.</summary>
		/// <returns>A persisted or ephemeral key that provides the specified algorithm.</returns>
		/// <param name="algorithm">The algorithm that the key will be used with.</param>
		/// <param name="keyName">The key name. If a name is not provided, the key will not be persisted.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="algorithm" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600021D RID: 541 RVA: 0x0000227E File Offset: 0x0000047E
		public static CngKey Create(CngAlgorithm algorithm, string keyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a named <see cref="T:System.Security.Cryptography.CngKey" /> object that provides the specified algorithm, using the supplied key creation parameters.</summary>
		/// <returns>A persisted or ephemeral key that provides the specified algorithm.</returns>
		/// <param name="algorithm">The algorithm that the key will be used with.</param>
		/// <param name="keyName">The key name. If a name is not provided, the key will not be persisted.</param>
		/// <param name="creationParameters">An object that specifies advanced parameters for the method, including the <see cref="T:System.Security.Cryptography.CngProvider" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="algorithm" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600021E RID: 542 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public static CngKey Create(CngAlgorithm algorithm, string keyName, CngKeyCreationParameters creationParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the key that is associated with the object.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to access a deleted key.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600021F RID: 543 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public void Delete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.CngKey" /> class.</summary>
		// Token: 0x06000220 RID: 544 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Checks to see whether a named key exists in the default key storage provider (KSP).</summary>
		/// <returns>true if the named key exists in the default KSP; otherwise, false.</returns>
		/// <param name="keyName">The key name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000221 RID: 545 RVA: 0x0000227E File Offset: 0x0000047E
		public static bool Exists(string keyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Checks to see whether a named key exists in the specified key storage provider (KSP).</summary>
		/// <returns>true if the named key exists in the specified provider; otherwise, false.</returns>
		/// <param name="keyName">The key name.</param>
		/// <param name="provider">The KSP to check for the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> or <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000222 RID: 546 RVA: 0x0000227E File Offset: 0x0000047E
		public static bool Exists(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Checks to see whether a named key exists in the specified key storage provider (KSP), according to the specified options.</summary>
		/// <returns>true if the named key exists in the specified provider; otherwise, false.</returns>
		/// <param name="keyName">The key name.</param>
		/// <param name="provider">The KSP to search for the key.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for opening a key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> or <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000223 RID: 547 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public static bool Exists(string keyName, CngProvider provider, CngKeyOpenOptions options)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new key by importing the specified key material into the default key storage provider (KSP) and using the specified format.</summary>
		/// <returns>A new key.</returns>
		/// <param name="keyBlob">An array that contains the key information.</param>
		/// <param name="format">An object that specifies the format of the <paramref name="keyBlob" /> array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyBlob" /> or <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000224 RID: 548 RVA: 0x0000227E File Offset: 0x0000047E
		public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new key by importing the specified key material into the specified key storage provider (KSP), using the specified format.</summary>
		/// <returns>A new key.</returns>
		/// <param name="keyBlob">An array that contains the key information.</param>
		/// <param name="format">An object that specifies the format of the <paramref name="keyBlob" /> array.</param>
		/// <param name="provider">The KSP.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyBlob" />, <paramref name="format" />, or <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000225 RID: 549 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Exports the key material into a BLOB, in the specified format.</summary>
		/// <returns>A BLOB that contains the key material in the specified format.</returns>
		/// <param name="format">An object that specifies the format of the key BLOB.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors. Typically, the <see cref="P:System.Security.Cryptography.CngKey.ExportPolicy" /> does not allow the key to be exported. </exception>
		// Token: 0x06000226 RID: 550 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public byte[] Export(CngKeyBlobFormat format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a property, given a name and a set of property options.</summary>
		/// <returns>An object that contains the raw value of the specified property.</returns>
		/// <param name="name">The name of the desired property.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for the named property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000227 RID: 551 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public CngProperty GetProperty(string name, CngPropertyOptions options)
		{
			throw new NotImplementedException();
		}

		/// <summary>Checks to see whether the specified property exists on the key.</summary>
		/// <returns>true if the specified property is found; otherwise, false.</returns>
		/// <param name="name">The property name to check.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for the named property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000228 RID: 552 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public bool HasProperty(string name, CngPropertyOptions options)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of an <see cref="T:System.Security.Cryptography.CngKey" /> object that represents an existing named key.</summary>
		/// <returns>An existing key.</returns>
		/// <param name="keyName">The name of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06000229 RID: 553 RVA: 0x0000227E File Offset: 0x0000047E
		public static CngKey Open(string keyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of an <see cref="T:System.Security.Cryptography.CngKey" /> object that represents an existing named key, using the specified key storage provider (KSP).</summary>
		/// <returns>An existing key.</returns>
		/// <param name="keyName">The name of the key.</param>
		/// <param name="provider">The KSP that contains the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> or <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600022A RID: 554 RVA: 0x0000227E File Offset: 0x0000047E
		public static CngKey Open(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of an <see cref="T:System.Security.Cryptography.CngKey" /> object that represents an existing named key, using the specified key storage provider (KSP) and key open options.</summary>
		/// <returns>An existing key.</returns>
		/// <param name="keyName">The name of the key.</param>
		/// <param name="provider">The KSP that contains the key.</param>
		/// <param name="openOptions">A bitwise combination of the enumeration values that specify options for opening the key, such as where the key is opened from (machine or user storage) and whether to suppress UI prompting.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyName" /> or <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600022B RID: 555 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		public static CngKey Open(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of an <see cref="T:System.Security.Cryptography.CngKey" /> object by using a handle to an existing key.</summary>
		/// <returns>An existing key.</returns>
		/// <param name="keyHandle">A handle to an existing key.</param>
		/// <param name="keyHandleOpenOptions">One of the enumeration values that indicates whether <paramref name="keyHandle" /> represents an ephemeral key or a named key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyHandle" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyHandle" /> is invalid or malformed, or it is already closed. This exception is also thrown if the key is an ephemeral key that is created by the common language runtime (CLR), but the <see cref="F:System.Security.Cryptography.CngKeyHandleOpenOptions.EphemeralKey" /> value is not specified.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) is not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x0600022C RID: 556 RVA: 0x0000227E File Offset: 0x0000047E
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static CngKey Open(SafeNCryptKeyHandle keyHandle, CngKeyHandleOpenOptions keyHandleOpenOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a named property on the key.</summary>
		/// <param name="property">The key property to set.</param>
		// Token: 0x0600022D RID: 557 RVA: 0x0000227E File Offset: 0x0000047E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void SetProperty(CngProperty property)
		{
			throw new NotImplementedException();
		}
	}
}
