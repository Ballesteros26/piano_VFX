using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono.Security.Cryptography;
using Unity;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents an isolated storage area containing files and directories. </summary>
	// Token: 0x020003EB RID: 1003
	[ComVisible(true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	public sealed class IsolatedStorageFile : IsolatedStorage, IDisposable
	{
		/// <summary>Gets the enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within an isolated storage scope.</summary>
		/// <returns>Enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within the specified isolated storage scope.</returns>
		/// <param name="scope">Represents the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> for which to return isolated stores. User and User|Roaming are the only IsolatedStorageScope combinations supported. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F17 RID: 12055 RVA: 0x000A84B6 File Offset: 0x000A66B6
		public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.Demand(scope);
			if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
			{
				throw new ArgumentException(Locale.GetText("Invalid scope, only User, User|Roaming and Machine are valid"));
			}
			return new IsolatedStorageFileEnumerator(scope, IsolatedStorageFile.GetIsolatedStorageRoot(scope));
		}

		/// <summary>Obtains isolated storage corresponding to the given application domain and the assembly evidence objects and types.</summary>
		/// <returns>An object that represents the parameters.</returns>
		/// <param name="scope">A bitwise combination of the enumeration values. </param>
		/// <param name="domainEvidence">An object that contains the application domain identity. </param>
		/// <param name="domainEvidenceType">The identity type to choose from the application domain evidence. </param>
		/// <param name="assemblyEvidence">An object that contains the code assembly identity. </param>
		/// <param name="assemblyEvidenceType">The identity type to choose from the application code assembly evidence. </param>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="domainEvidence" /> or <paramref name="assemblyEvidence" /> identity has not been passed in. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized. -or-<paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned null.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002F18 RID: 12056 RVA: 0x000A84E8 File Offset: 0x000A66E8
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			bool flag = (scope & IsolatedStorageScope.Domain) > IsolatedStorageScope.None;
			if (flag && domainEvidence == null)
			{
				throw new ArgumentNullException("domainEvidence");
			}
			bool flag2 = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag2 && assemblyEvidence == null)
			{
				throw new ArgumentNullException("assemblyEvidence");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				if (domainEvidenceType == null)
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(domainEvidence);
				}
				else
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(domainEvidence, domainEvidenceType);
				}
				if (isolatedStorageFile._domainIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find domain identity."));
				}
			}
			if (flag2)
			{
				if (assemblyEvidenceType == null)
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(assemblyEvidence);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(assemblyEvidence, assemblyEvidenceType);
				}
				if (isolatedStorageFile._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find assembly identity."));
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains the isolated storage corresponding to the given application domain and assembly evidence objects.</summary>
		/// <returns>An object that represents the parameters.</returns>
		/// <param name="scope">A bitwise combination of the enumeration values. </param>
		/// <param name="domainIdentity">An object that contains evidence for the application domain identity. </param>
		/// <param name="assemblyIdentity">An object that contains evidence for the code assembly identity. </param>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.ArgumentNullException">Neither <paramref name="domainIdentity" /> nor <paramref name="assemblyIdentity" /> has been passed in. This verifies that the correct constructor is being used.-or- Either <paramref name="domainIdentity" /> or <paramref name="assemblyIdentity" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized. -or-<paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned null.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F19 RID: 12057 RVA: 0x000A85B8 File Offset: 0x000A67B8
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None && domainIdentity == null)
			{
				throw new ArgumentNullException("domainIdentity");
			}
			bool flag = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag && assemblyIdentity == null)
			{
				throw new ArgumentNullException("assemblyIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			}
			isolatedStorageFile._domainIdentity = domainIdentity;
			isolatedStorageFile._assemblyIdentity = assemblyIdentity;
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the isolated storage scope given the application domain and assembly evidence types.</summary>
		/// <returns>An object that represents the parameters.</returns>
		/// <param name="scope">A bitwise combination of the enumeration values. </param>
		/// <param name="domainEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <param name="assemblyEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The evidence type provided is missing in the assembly evidence list. -or-An isolated storage location cannot be initialized.-or-<paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned null.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.-or-<paramref name="scope" /> contains <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1A RID: 12058 RVA: 0x000A8624 File Offset: 0x000A6824
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
			{
				if (domainEvidenceType == null)
				{
					domainEvidenceType = typeof(Url);
				}
				isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(AppDomain.CurrentDomain.Evidence, domainEvidenceType);
			}
			if ((scope & IsolatedStorageScope.Assembly) != IsolatedStorageScope.None)
			{
				Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
				isolatedStorageFile._fullEvidences = evidence;
				if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
				{
					if (assemblyEvidenceType == null)
					{
						assemblyEvidenceType = typeof(Url);
					}
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(evidence, assemblyEvidenceType);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the given application identity.</summary>
		/// <returns>An object that represents the parameters.</returns>
		/// <param name="scope">A bitwise combination of the enumeration values. </param>
		/// <param name="applicationIdentity">An object that contains evidence for the application identity. </param>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.ArgumentNullException">The  <paramref name="applicationIdentity" /> identity has not been passed in. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized. -or-<paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined,because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned null.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1B RID: 12059 RVA: 0x000A86C1 File Offset: 0x000A68C1
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile._applicationIdentity = applicationIdentity;
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the isolation scope and the application identity object.</summary>
		/// <returns>An object that represents the parameters.</returns>
		/// <param name="scope">A bitwise combination of the enumeration values. </param>
		/// <param name="applicationEvidenceType">An object that contains the application identity. </param>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.ArgumentNullException">The   <paramref name="applicationEvidence" />  identity has not been passed in. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized. -or-<paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned null.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.-or-<paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1C RID: 12060 RVA: 0x000A86FA File Offset: 0x000A68FA
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile.InitStore(scope, applicationEvidenceType);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's application identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The application identity of the caller could not be determined.-or- The granted permission set for the application domain could not be determined.-or-An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1D RID: 12061 RVA: 0x000A8728 File Offset: 0x000A6928
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForApplication()
		{
			IsolatedStorageScope isolatedStorageScope = IsolatedStorageScope.Machine | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(isolatedStorageScope);
			isolatedStorageFile.InitStore(isolatedStorageScope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1E RID: 12062 RVA: 0x000A875C File Offset: 0x000A695C
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the application domain identity and the assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.-or- The assembly specified has insufficient permissions to create isolated stores.-or-The permissions for the application domain cannot be determined.-or-An isolated storage location cannot be initialized. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F1F RID: 12063 RVA: 0x000A8794 File Offset: 0x000A6994
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.-or-The application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> property returned null.-or-The permissions for the application domain cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F20 RID: 12064 RVA: 0x000A87E4 File Offset: 0x000A69E4
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForApplication()
		{
			IsolatedStorageScope isolatedStorageScope = IsolatedStorageScope.User | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(isolatedStorageScope);
			isolatedStorageFile.InitStore(isolatedStorageScope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.-or-The permissions for the calling assembly cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F21 RID: 12065 RVA: 0x000A8818 File Offset: 0x000A6A18
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Assembly);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the application domain identity and assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.-or- The assembly specified has insufficient permissions to create isolated stores.-or-An isolated storage location cannot be initialized. -or-The permissions for the application domain cannot be determined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F22 RID: 12066 RVA: 0x000A8850 File Offset: 0x000A6A50
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains a user-scoped isolated store for use by applications in a virtual host domain.</summary>
		/// <returns>The isolated storage file that corresponds to the isolated storage scope based on the calling code's application identity.</returns>
		// Token: 0x06002F23 RID: 12067 RVA: 0x00014B5A File Offset: 0x00012D5A
		[ComVisible(false)]
		public static IsolatedStorageFile GetUserStoreForSite()
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified isolated storage scope for all identities.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be removed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F24 RID: 12068 RVA: 0x000A889C File Offset: 0x000A6A9C
		public static void Remove(IsolatedStorageScope scope)
		{
			string isolatedStorageRoot = IsolatedStorageFile.GetIsolatedStorageRoot(scope);
			if (!Directory.Exists(isolatedStorageRoot))
			{
				return;
			}
			try
			{
				Directory.Delete(isolatedStorageRoot, true);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000A88E0 File Offset: 0x000A6AE0
		internal static string GetIsolatedStorageRoot(IsolatedStorageScope scope)
		{
			string text = null;
			if ((scope & IsolatedStorageScope.User) != IsolatedStorageScope.None)
			{
				if ((scope & IsolatedStorageScope.Roaming) != IsolatedStorageScope.None)
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
				}
				else
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
				}
			}
			else if ((scope & IsolatedStorageScope.Machine) != IsolatedStorageScope.None)
			{
				text = Environment.UnixGetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create);
			}
			if (text == null)
			{
				throw new IsolatedStorageException(string.Format(Locale.GetText("Couldn't access storage location for '{0}'."), scope));
			}
			return Path.Combine(text, ".isolated-storage");
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000A8953 File Offset: 0x000A6B53
		private static void Demand(IsolatedStorageScope scope)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new IsolatedStorageFilePermission(PermissionState.None)
				{
					UsageAllowed = IsolatedStorageFile.ScopeToContainment(scope)
				}.Demand();
			}
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000A8974 File Offset: 0x000A6B74
		private static IsolatedStorageContainment ScopeToContainment(IsolatedStorageScope scope)
		{
			if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
			{
				if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.AssemblyIsolationByUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.DomainIsolationByUser;
					}
				}
				else
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.AssemblyIsolationByRoamingUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.DomainIsolationByRoamingUser;
					}
				}
			}
			else if (scope <= (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
			{
				if (scope == (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.AssemblyIsolationByMachine;
				}
				if (scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.DomainIsolationByMachine;
				}
			}
			else
			{
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByUser;
				}
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByRoamingUser;
				}
				if (scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByMachine;
				}
			}
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000A89E4 File Offset: 0x000A6BE4
		internal static ulong GetDirectorySize(DirectoryInfo di)
		{
			ulong num = 0UL;
			foreach (FileInfo fileInfo in di.GetFiles())
			{
				num += (ulong)fileInfo.Length;
			}
			foreach (DirectoryInfo directoryInfo in di.GetDirectories())
			{
				num += IsolatedStorageFile.GetDirectorySize(directoryInfo);
			}
			return num;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000A8A3E File Offset: 0x000A6C3E
		private IsolatedStorageFile(IsolatedStorageScope scope)
		{
			this.storage_scope = scope;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000A8A4D File Offset: 0x000A6C4D
		internal IsolatedStorageFile(IsolatedStorageScope scope, string location)
		{
			this.storage_scope = scope;
			this.directory = new DirectoryInfo(location);
			if (!this.directory.Exists)
			{
				throw new IsolatedStorageException(Locale.GetText("Invalid storage."));
			}
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000A8A88 File Offset: 0x000A6C88
		~IsolatedStorageFile()
		{
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000A8AB0 File Offset: 0x000A6CB0
		private void PostInit()
		{
			string text = IsolatedStorageFile.GetIsolatedStorageRoot(base.Scope);
			string text2;
			if (this._applicationIdentity != null)
			{
				text2 = string.Format("a{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._applicationIdentity));
			}
			else if (this._domainIdentity != null)
			{
				text2 = string.Format("d{0}{1}{0}{2}", this.SeparatorInternal, this.GetNameFromIdentity(this._domainIdentity), this.GetNameFromIdentity(this._assemblyIdentity));
			}
			else
			{
				if (this._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("No code identity available."));
				}
				text2 = string.Format("d{0}none{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._assemblyIdentity));
			}
			text = Path.Combine(text, text2);
			this.directory = new DirectoryInfo(text);
			if (!this.directory.Exists)
			{
				try
				{
					this.directory.Create();
					this.SaveIdentities(text);
				}
				catch (IOException)
				{
				}
			}
		}

		/// <summary>Gets the current size of the isolated storage.</summary>
		/// <returns>The total number of bytes of storage currently in use within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. The current store has a roaming scope or is not open. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object size is undefined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000A8BB4 File Offset: 0x000A6DB4
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong CurrentSize
		{
			get
			{
				return IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage within the limits established by the quota.</summary>
		/// <returns>The limit of isolated storage space in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFile.MaximumSize" /> cannot be determined without evidence from the assembly's creation. The evidence could not be determined when the object was created. </exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage error occurred.</exception>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000A8BC4 File Offset: 0x000A6DC4
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong MaximumSize
		{
			get
			{
				if (!SecurityManager.SecurityEnabled)
				{
					return 9223372036854775807UL;
				}
				if (this._resolved)
				{
					return this._maxSize;
				}
				Evidence evidence;
				if (this._fullEvidences != null)
				{
					evidence = this._fullEvidences;
				}
				else
				{
					evidence = new Evidence();
					if (this._assemblyIdentity != null)
					{
						evidence.AddHost(this._assemblyIdentity);
					}
				}
				if (evidence.Count < 1)
				{
					throw new InvalidOperationException(Locale.GetText("Couldn't get the quota from the available evidences."));
				}
				PermissionSet permissionSet = null;
				PermissionSet permissionSet2 = SecurityManager.ResolvePolicy(evidence, null, null, null, out permissionSet);
				IsolatedStoragePermission permission = this.GetPermission(permissionSet2);
				if (permission == null)
				{
					if (!permissionSet2.IsUnrestricted())
					{
						throw new InvalidOperationException(Locale.GetText("No quota from the available evidences."));
					}
					this._maxSize = 9223372036854775807UL;
				}
				else
				{
					this._maxSize = (ulong)permission.UserQuota;
				}
				this._resolved = true;
				return this._maxSize;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000A8C92 File Offset: 0x000A6E92
		internal string Root
		{
			get
			{
				return this.directory.FullName;
			}
		}

		/// <summary>Gets a value that represents the amount of free space available for isolated storage.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000A8C9F File Offset: 0x000A6E9F
		[ComVisible(false)]
		public override long AvailableFreeSpace
		{
			get
			{
				this.CheckOpen();
				return long.MaxValue;
			}
		}

		/// <summary>Gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000A8CB0 File Offset: 0x000A6EB0
		[ComVisible(false)]
		public override long Quota
		{
			get
			{
				this.CheckOpen();
				return (long)this.MaximumSize;
			}
		}

		/// <summary>Gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000A8CBE File Offset: 0x000A6EBE
		[ComVisible(false)]
		public override long UsedSize
		{
			get
			{
				this.CheckOpen();
				return (long)IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		/// <summary>Gets a value that indicates whether isolated storage is enabled.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00003B29 File Offset: 0x00001D29
		[ComVisible(false)]
		public static bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000A8CD1 File Offset: 0x000A6ED1
		internal bool IsClosed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000A8CD9 File Offset: 0x000A6ED9
		internal bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		/// <summary>Closes a store previously opened with <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope,System.Type,System.Type)" />, <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly" />, or <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain" />.</summary>
		// Token: 0x06002F36 RID: 12086 RVA: 0x000A8CE1 File Offset: 0x000A6EE1
		public void Close()
		{
			this.closed = true;
		}

		/// <summary>Creates a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to create within the isolated storage scope. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The current code has insufficient permissions to create isolated storage directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F37 RID: 12087 RVA: 0x000A8CEC File Offset: 0x000A6EEC
		public void CreateDirectory(string dir)
		{
			if (dir == null)
			{
				throw new ArgumentNullException("dir");
			}
			if (dir.IndexOfAny(Path.PathSeparatorChars) >= 0)
			{
				string[] array = dir.Split(Path.PathSeparatorChars, StringSplitOptions.RemoveEmptyEntries);
				DirectoryInfo directoryInfo = this.directory;
				for (int i = 0; i < array.Length; i++)
				{
					if (directoryInfo.GetFiles(array[i]).Length != 0)
					{
						throw new IsolatedStorageException("Unable to create directory.");
					}
					directoryInfo = directoryInfo.CreateSubdirectory(array[i]);
				}
				return;
			}
			if (this.directory.GetFiles(dir).Length != 0)
			{
				throw new IsolatedStorageException("Unable to create directory.");
			}
			this.directory.CreateSubdirectory(dir);
		}

		/// <summary>Copies an existing file to a new file.  </summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName " />was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName " />was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.-or-<paramref name="destinationFileName" /> exists.-or-An I/O error has occurred.</exception>
		// Token: 0x06002F38 RID: 12088 RVA: 0x000A8D80 File Offset: 0x000A6F80
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			this.CopyFile(sourceFileName, destinationFileName, false);
		}

		/// <summary>Copies an existing file to a new file, and optionally overwrites an existing file.</summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory.</param>
		/// <param name="overwrite">true if the destination file can be overwritten; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName " />was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName " />was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.-or-An I/O error has occurred.</exception>
		// Token: 0x06002F39 RID: 12089 RVA: 0x000A8D8C File Offset: 0x000A6F8C
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (File.Exists(text2) && !overwrite)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Copy(text, text2, overwrite);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
		}

		/// <summary>Creates a file in the isolated store.</summary>
		/// <returns>A new isolated storage file.</returns>
		/// <param name="path">The relative path of the file to create.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06002F3A RID: 12090 RVA: 0x000A8EB4 File Offset: 0x000A70B4
		[ComVisible(false)]
		public IsolatedStorageFileStream CreateFile(string path)
		{
			return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
		}

		/// <summary>Deletes a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to delete within the isolated storage scope. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The directory could not be deleted. </exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path was null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F3B RID: 12091 RVA: 0x000A8EC0 File Offset: 0x000A70C0
		public void DeleteDirectory(string dir)
		{
			try
			{
				if (Path.IsPathRooted(dir))
				{
					dir = dir.Substring(1);
				}
				this.directory.CreateSubdirectory(dir).Delete();
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete directory '{0}'", new object[] { dir }));
			}
		}

		/// <summary>Deletes a file in the isolated storage scope.</summary>
		/// <param name="file">The relative path of the file to delete within the isolated storage scope. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The target file is open or the path is incorrect. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F3C RID: 12092 RVA: 0x000A8F1C File Offset: 0x000A711C
		public void DeleteFile(string file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (!File.Exists(Path.Combine(this.directory.FullName, file)))
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[] { file }));
			}
			try
			{
				File.Delete(Path.Combine(this.directory.FullName, file));
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[] { file }));
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />. </summary>
		// Token: 0x06002F3D RID: 12093 RVA: 0x000A8FB0 File Offset: 0x000A71B0
		public void Dispose()
		{
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Determines whether the specified path refers to an existing directory in the isolated store.</summary>
		/// <returns>true if <paramref name="path" /> refers to an existing directory in the isolated store and is not null; otherwise, false.</returns>
		/// <param name="path">The path to test.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		// Token: 0x06002F3E RID: 12094 RVA: 0x000A8FC0 File Offset: 0x000A71C0
		[ComVisible(false)]
		public bool DirectoryExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(text) && Directory.Exists(text);
		}

		/// <summary>Determines whether the specified path refers to an existing file in the isolated store.</summary>
		/// <returns>true if <paramref name="path" /> refers to an existing file in the isolated store and is not null; otherwise, false.</returns>
		/// <param name="path">The path and file name to test.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. </exception>
		// Token: 0x06002F3F RID: 12095 RVA: 0x000A9004 File Offset: 0x000A7204
		[ComVisible(false)]
		public bool FileExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(text) && File.Exists(text);
		}

		/// <summary>Returns the creation date and time of a specified file or directory.</summary>
		/// <returns>The creation date and time for the specified file or directory. This value is expressed in local time.</returns>
		/// <param name="path">The path to the file or directory for which to obtain creation date and time information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.</exception>
		// Token: 0x06002F40 RID: 12096 RVA: 0x000A9048 File Offset: 0x000A7248
		[ComVisible(false)]
		public DateTimeOffset GetCreationTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetCreationTime(text);
			}
			return Directory.GetCreationTime(text);
		}

		/// <summary>Returns the date and time a specified file or directory was last accessed.</summary>
		/// <returns>The date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <param name="path">The path to the file or directory for which to obtain last access date and time information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.</exception>
		// Token: 0x06002F41 RID: 12097 RVA: 0x000A90B4 File Offset: 0x000A72B4
		[ComVisible(false)]
		public DateTimeOffset GetLastAccessTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetLastAccessTime(text);
			}
			return Directory.GetLastAccessTime(text);
		}

		/// <summary>Returns the date and time a specified file or directory was last written to.</summary>
		/// <returns>The date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <param name="path">The path to the file or directory for which to obtain last write date and time information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.</exception>
		// Token: 0x06002F42 RID: 12098 RVA: 0x000A9120 File Offset: 0x000A7320
		[ComVisible(false)]
		public DateTimeOffset GetLastWriteTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetLastWriteTime(text);
			}
			return Directory.GetLastWriteTime(text);
		}

		/// <summary>Enumerates the directories in an isolated storage scope that match a given search pattern.</summary>
		/// <returns>An array of the relative paths of directories in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no directories that match.</returns>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories resolved from <paramref name="searchPattern" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory or directories specified by <paramref name="searchPattern" /> are not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F43 RID: 12099 RVA: 0x000A918C File Offset: 0x000A738C
		public string[] GetDirectoryNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			DirectoryInfo[] array = null;
			if (directoryName == null || directoryName.Length == 0)
			{
				array = this.directory.GetDirectories(searchPattern);
			}
			else
			{
				DirectoryInfo directoryInfo = this.directory.GetDirectories(directoryName)[0];
				if (directoryInfo.FullName.IndexOf(this.directory.FullName) >= 0)
				{
					array = directoryInfo.GetDirectories(fileName);
					string[] array2 = directoryName.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
					for (int i = array2.Length - 1; i >= 0; i--)
					{
						if (directoryInfo.Name != array2[i])
						{
							array = null;
							break;
						}
						directoryInfo = directoryInfo.Parent;
					}
				}
			}
			if (array == null)
			{
				throw new SecurityException();
			}
			return this.GetNames(array);
		}

		/// <summary>Enumerates the directories at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of directories at the root of the isolated store. A zero-length array specifies that there are no directories at the root.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">One or more directories are not found.</exception>
		// Token: 0x06002F44 RID: 12100 RVA: 0x000A9277 File Offset: 0x000A7477
		[ComVisible(false)]
		public string[] GetDirectoryNames()
		{
			return this.GetDirectoryNames("*");
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000A9284 File Offset: 0x000A7484
		private string[] GetNames(FileSystemInfo[] afsi)
		{
			string[] array = new string[afsi.Length];
			for (int num = 0; num != afsi.Length; num++)
			{
				array[num] = afsi[num].Name;
			}
			return array;
		}

		/// <summary>Gets the file names that match a search pattern.</summary>
		/// <returns>An array of relative paths of files in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no files that match.</returns>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The file path specified by <paramref name="searchPattern" /> cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F46 RID: 12102 RVA: 0x000A92B4 File Offset: 0x000A74B4
		public string[] GetFileNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			FileInfo[] array;
			if (directoryName == null || directoryName.Length == 0)
			{
				array = this.directory.GetFiles(searchPattern);
			}
			else
			{
				DirectoryInfo[] directories = this.directory.GetDirectories(directoryName);
				if (directories.Length != 1 || !(directories[0].Name == directoryName) || directories[0].FullName.IndexOf(this.directory.FullName) < 0)
				{
					throw new SecurityException();
				}
				array = directories[0].GetFiles(fileName);
			}
			return this.GetNames(array);
		}

		/// <summary>Enumerates the file names at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of files at the root of the isolated store.  A zero-length array specifies that there are no files at the root.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">File paths from the isolated store root cannot be determined.</exception>
		// Token: 0x06002F47 RID: 12103 RVA: 0x000A936D File Offset: 0x000A756D
		[ComVisible(false)]
		public string[] GetFileNames()
		{
			return this.GetFileNames("*");
		}

		/// <summary>Enables an application to explicitly request a larger quota size, in bytes. </summary>
		/// <returns>true if the new quota is accepted; otherwise, false.</returns>
		/// <param name="newQuotaSize">The requested size, in bytes.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newQuotaSize" /> is less than current quota size.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newQuotaSize" /> is less than zero, or less than or equal to the current quota size. </exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current scope is not for an application user.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.</exception>
		// Token: 0x06002F48 RID: 12104 RVA: 0x000A937A File Offset: 0x000A757A
		[ComVisible(false)]
		public override bool IncreaseQuotaTo(long newQuotaSize)
		{
			if (newQuotaSize < this.Quota)
			{
				throw new ArgumentException();
			}
			this.CheckOpen();
			return false;
		}

		/// <summary>Moves a specified directory and its contents to a new location.</summary>
		/// <param name="sourceDirectoryName">The name of the directory to move.</param>
		/// <param name="destinationDirectoryName">The path to the new location for <paramref name="sourceDirectoryName" />. This cannot be the path to an existing directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> does not exist.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.-or-<paramref name="destinationDirectoryName" /> already exists.-or-<paramref name="sourceDirectoryName" /> and <paramref name="destinationDirectoryName" /> refer to the same directory.</exception>
		// Token: 0x06002F49 RID: 12105 RVA: 0x000A9394 File Offset: 0x000A7594
		[ComVisible(false)]
		public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
		{
			if (sourceDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (sourceDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "sourceDirectoryName");
			}
			if (destinationDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "destinationDirectoryName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceDirectoryName);
			string text2 = Path.Combine(this.directory.FullName, destinationDirectoryName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(text))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceDirectoryName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + destinationDirectoryName + "'.");
			}
			try
			{
				Directory.Move(text, text2);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
		}

		/// <summary>Moves a specified file to a new location, and optionally lets you specify a new file name.</summary>
		/// <param name="sourceFileName">The name of the file to move.</param>
		/// <param name="destinationFileName">The path to the new location for the file. If a file name is included, the moved file will have that name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName " />or<paramref name=" destinationFileName " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.-or-Isolated storage is disabled.</exception>
		// Token: 0x06002F4A RID: 12106 RVA: 0x000A94A8 File Offset: 0x000A76A8
		[ComVisible(false)]
		public void MoveFile(string sourceFileName, string destinationFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Move(text, text2);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
		}

		/// <summary>Opens a file in the specified mode.</summary>
		/// <returns>A file that is opened in the specified mode, with read/write access, and is unshared.</returns>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06002F4B RID: 12107 RVA: 0x000A95B0 File Offset: 0x000A77B0
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
		{
			return new IsolatedStorageFileStream(path, mode, this);
		}

		/// <summary>Opens a file in the specified mode with the specified read/write access.</summary>
		/// <returns>A file that is opened in the specified mode and access, and is unshared.</returns>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06002F4C RID: 12108 RVA: 0x000A95BA File Offset: 0x000A77BA
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
		{
			return new IsolatedStorageFileStream(path, mode, access, this);
		}

		/// <summary>Opens a file in the specified mode, with the specified read/write access and sharing permission.</summary>
		/// <returns>A file that is opened in the specified mode and access, and with the specified sharing options.</returns>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open or create the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access</param>
		/// <param name="share">A bitwise combination of enumeration values that specify the type of access other <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />   objects have to this file.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed. -or-Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="M:System.IO.FileInfo.Open(System.IO.FileMode)" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06002F4D RID: 12109 RVA: 0x000A95C5 File Offset: 0x000A77C5
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new IsolatedStorageFileStream(path, mode, access, share, this);
		}

		/// <summary>Removes the isolated storage scope and all its contents.</summary>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be deleted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F4E RID: 12110 RVA: 0x000A95D4 File Offset: 0x000A77D4
		public override void Remove()
		{
			this.CheckOpen(false);
			try
			{
				this.directory.Delete(true);
			}
			catch
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
			this.Close();
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000A9618 File Offset: 0x000A7818
		protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			if (ps == null)
			{
				return null;
			}
			return (IsolatedStoragePermission)ps.GetPermission(typeof(IsolatedStorageFilePermission));
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000A9634 File Offset: 0x000A7834
		private void CheckOpen()
		{
			this.CheckOpen(true);
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000A9640 File Offset: 0x000A7840
		private void CheckOpen(bool checkDirExists)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("IsolatedStorageFile");
			}
			if (this.closed)
			{
				throw new InvalidOperationException("Storage needs to be open for this operation.");
			}
			if (checkDirExists && !Directory.Exists(this.directory.FullName))
			{
				throw new IsolatedStorageException("Isolated storage has been removed or disabled.");
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000A9693 File Offset: 0x000A7893
		private bool IsPathInStorage(string path)
		{
			return Path.GetFullPath(path).StartsWith(this.directory.FullName);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000A96AC File Offset: 0x000A78AC
		private string GetNameFromIdentity(object identity)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(identity.ToString());
			Array array = SHA1.Create().ComputeHash(bytes, 0, bytes.Length);
			byte[] array2 = new byte[10];
			Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
			return CryptoConvert.ToHex(array2);
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000A96F4 File Offset: 0x000A78F4
		private static object GetTypeFromEvidence(Evidence e, Type t)
		{
			foreach (object obj in e)
			{
				if (obj.GetType() == t)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000A9754 File Offset: 0x000A7954
		internal static object GetAssemblyIdentityFromEvidence(Evidence e)
		{
			object obj = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Publisher));
			if (obj != null)
			{
				return obj;
			}
			obj = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(StrongName));
			if (obj != null)
			{
				return obj;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000A97A0 File Offset: 0x000A79A0
		internal static object GetDomainIdentityFromEvidence(Evidence e)
		{
			object typeFromEvidence = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(ApplicationDirectory));
			if (typeFromEvidence != null)
			{
				return typeFromEvidence;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000A97D4 File Offset: 0x000A79D4
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void SaveIdentities(string root)
		{
			IsolatedStorageFile.Identities identities = new IsolatedStorageFile.Identities(this._applicationIdentity, this._assemblyIdentity, this._domainIdentity);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			IsolatedStorageFile.mutex.WaitOne();
			try
			{
				using (FileStream fileStream = File.Create(root + ".storage"))
				{
					binaryFormatter.Serialize(fileStream, identities);
				}
			}
			finally
			{
				IsolatedStorageFile.mutex.ReleaseMutex();
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal IsolatedStorageFile()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001858 RID: 6232
		private bool _resolved;

		// Token: 0x04001859 RID: 6233
		private ulong _maxSize;

		// Token: 0x0400185A RID: 6234
		private Evidence _fullEvidences;

		// Token: 0x0400185B RID: 6235
		private static readonly Mutex mutex = new Mutex();

		// Token: 0x0400185C RID: 6236
		private bool closed;

		// Token: 0x0400185D RID: 6237
		private bool disposed;

		// Token: 0x0400185E RID: 6238
		private DirectoryInfo directory;

		// Token: 0x020003EC RID: 1004
		[Serializable]
		private struct Identities
		{
			// Token: 0x06002F5A RID: 12122 RVA: 0x000A9868 File Offset: 0x000A7A68
			public Identities(object application, object assembly, object domain)
			{
				this.Application = application;
				this.Assembly = assembly;
				this.Domain = domain;
			}

			// Token: 0x0400185F RID: 6239
			public object Application;

			// Token: 0x04001860 RID: 6240
			public object Assembly;

			// Token: 0x04001861 RID: 6241
			public object Domain;
		}
	}
}
