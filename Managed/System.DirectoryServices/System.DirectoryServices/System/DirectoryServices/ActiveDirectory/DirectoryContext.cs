using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> class identifies a specific directory and the credentials that are used to access the directory.</summary>
	// Token: 0x0200004E RID: 78
	[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
	public class DirectoryContext
	{
		/// <summary>Gets the name of the context.</summary>
		/// <returns>The name of the context.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the user name of the context.</summary>
		/// <returns>The user name to use for access by this context.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000208C File Offset: 0x0000028C
		public string UserName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type of the context object.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> members that specifies the type of context.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryContextType ContextType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> class of the specified type that contains the credentials of the current user context.</summary>
		/// <param name="contextType">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> members that specifies the type of context to create.  For this constructor, the context type must be <see cref="F:System.DirectoryServices.ActiveDirectory.DirectoryContextType.Domain" /> or <see cref="F:System.DirectoryServices.ActiveDirectory.DirectoryContextType.Forest" />.  Other types of directory contexts cannot be created using this constructor because other types require a constructor that includes a parameter specifying the directory or target name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextType" /> is not valid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This class does not work on the current platform.</exception>
		// Token: 0x060002FD RID: 765 RVA: 0x00004AC8 File Offset: 0x00002CC8
		[DirectoryServicesPermission(SecurityAction.Demand, Unrestricted = true)]
		public DirectoryContext(DirectoryContextType contextType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> class of the specified type that contains the specified name and the credentials of the current user context.</summary>
		/// <param name="contextType">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> members that specifies the type of context to create.</param>
		/// <param name="name">The target of the directory context. This string can take any of the formats defined in the Remarks section of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> topic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="contextType" /> is not valid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This class does not work on the current platform.</exception>
		// Token: 0x060002FE RID: 766 RVA: 0x00004AC8 File Offset: 0x00002CC8
		[DirectoryServicesPermission(SecurityAction.Demand, Unrestricted = true)]
		public DirectoryContext(DirectoryContextType contextType, string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> class of the specified type that contains the specified user name and password.</summary>
		/// <param name="contextType">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> members that specifies the type of context to create. For this constructor, this parameter must be <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" />.</param>
		/// <param name="username">The user name to use for access.</param>
		/// <param name="password">The password to use for access.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextType" /> is not valid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This class does not work on the current platform.</exception>
		// Token: 0x060002FF RID: 767 RVA: 0x00004AC8 File Offset: 0x00002CC8
		[DirectoryServicesPermission(SecurityAction.Demand, Unrestricted = true)]
		public DirectoryContext(DirectoryContextType contextType, string username, string password)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> class of the specified type that contains the specified target, user name, and password.</summary>
		/// <param name="contextType">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContextType" /> members that specifies the type of context to create.</param>
		/// <param name="name">The target of the directory context. This string can take any of the formats defined in the Remarks section of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> topic.</param>
		/// <param name="username">The user name to use for access.</param>
		/// <param name="password">The password to use for access.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="contextType" /> is not valid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This class does not work on the current platform.</exception>
		// Token: 0x06000300 RID: 768 RVA: 0x00004AC8 File Offset: 0x00002CC8
		[DirectoryServicesPermission(SecurityAction.Demand, Unrestricted = true)]
		public DirectoryContext(DirectoryContextType contextType, string name, string username, string password)
		{
			throw new NotImplementedException();
		}
	}
}
