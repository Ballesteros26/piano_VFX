using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.AuthenticationTypes" /> enumeration specifies the types of authentication used in <see cref="N:System.DirectoryServices" />. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
	// Token: 0x02000011 RID: 17
	[Flags]
	[Serializable]
	public enum AuthenticationTypes
	{
		/// <summary>No authentication is performed.</summary>
		// Token: 0x0400004C RID: 76
		Anonymous = 16,
		/// <summary>Enables Active Directory Services Interface (ADSI) to delegate the user's security context, which is necessary for moving objects across domains.</summary>
		// Token: 0x0400004D RID: 77
		Delegation = 256,
		/// <summary>Attaches a cryptographic signature to the message that both identifies the sender and ensures that the message has not been modified in transit.</summary>
		// Token: 0x0400004E RID: 78
		Encryption = 2,
		/// <summary>Specifies that ADSI will not attempt to query the Active Directory Domain Services objectClass property.  Therefore, only the base interfaces that are supported by all ADSI objects will be exposed.  Other interfaces that the object supports will not be available. A user can use this option to boost the performance in a series of object manipulations that involve only methods of the base interfaces. However, ADSI does not verify if any of the request objects actually exist on the server. For more information, see the topic "Fast Binding Option for Batch Write/Modify Operations" in the MSDN Library at http://msdn.microsoft.com/library.  For more information about the objectClass property, see the "Object-Class" topic in the MSDN Library at http://msdn.microsoft.com/library.</summary>
		// Token: 0x0400004F RID: 79
		FastBind = 32,
		/// <summary>Equates to zero, which means to use basic authentication (simple bind) in the LDAP provider.</summary>
		// Token: 0x04000050 RID: 80
		None = 0,
		/// <summary>For a WinNT provider, ADSI tries to connect to a domain controller. For Active Directory Domain Services, this flag indicates that a writable server is not required for a serverless binding.</summary>
		// Token: 0x04000051 RID: 81
		ReadonlyServer = 4,
		/// <summary>Encrypts data using Kerberos. The <see cref="F:System.DirectoryServices.AuthenticationTypes.Secure" /> flag must also be set to use sealing.</summary>
		// Token: 0x04000052 RID: 82
		Sealing = 128,
		/// <summary>Requests secure authentication. When this flag is set, the WinNT provider uses NTLM to authenticate the client. Active Directory Domain Services uses Kerberos, and possibly NTLM, to authenticate the client. When the user name and password are a null reference (Nothing in Visual Basic), ADSI binds to the object using the security context of the calling thread, which is either the security context of the user account under which the application is running or of the client user account that the calling thread is impersonating.</summary>
		// Token: 0x04000053 RID: 83
		Secure = 1,
		/// <summary>Attaches a cryptographic signature to the message that both identifies the sender and ensures that the message has not been modified in transit. Active Directory Domain Services requires the Certificate Server be installed to support Secure Sockets Layer (SSL) encryption.</summary>
		// Token: 0x04000054 RID: 84
		SecureSocketsLayer = 2,
		/// <summary>If your ADsPath includes a server name, specify this flag when using the LDAP provider. Do not use this flag for paths that include a domain name or for serverless paths. Specifying a server name without also specifying this flag results in unnecessary network traffic.</summary>
		// Token: 0x04000055 RID: 85
		ServerBind = 512,
		/// <summary>Verifies data integrity to ensure that the data received is the same as the data sent. The <see cref="F:System.DirectoryServices.AuthenticationTypes.Secure" /> flag must also be set to use signing.</summary>
		// Token: 0x04000056 RID: 86
		Signing = 64
	}
}
