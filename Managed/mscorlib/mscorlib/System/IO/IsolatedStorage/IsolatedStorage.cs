using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents the abstract base class from which all isolated storage implementations must derive. </summary>
	// Token: 0x020003E9 RID: 1001
	[ComVisible(true)]
	public abstract class IsolatedStorage : MarshalByRefObject
	{
		/// <summary>Gets an application identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the application <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000A8344 File Offset: 0x000A6544
		[MonoTODO("Does not currently use the manifest support")]
		[ComVisible(false)]
		public object ApplicationIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Application) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._applicationIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				throw new NotImplementedException(Locale.GetText("CAS related"));
			}
		}

		/// <summary>Gets an assembly identity used to scope isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="T:System.Reflection.Assembly" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. </exception>
		/// <exception cref="T:System.InvalidOperationException">The assembly is not defined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000A8393 File Offset: 0x000A6593
		public object AssemblyIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Assembly) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._assemblyIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._assemblyIdentity;
			}
		}

		/// <summary>Gets a value representing the current size of isolated storage.</summary>
		/// <returns>The number of storage units currently used within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current size of the isolated store is undefined. </exception>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000A83CD File Offset: 0x000A65CD
		[CLSCompliant(false)]
		[Obsolete]
		public virtual ulong CurrentSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset CurrentSize."));
			}
		}

		/// <summary>Gets a domain identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the domain <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000A83DE File Offset: 0x000A65DE
		public object DomainIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Domain) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._domainIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._domainIdentity;
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage. When overridden in a derived class, this value can take different units of measure.</summary>
		/// <returns>The maximum amount of isolated storage space in bytes. Derived classes can return different units of value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The quota has not been defined. </exception>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000A8418 File Offset: 0x000A6618
		[Obsolete]
		[CLSCompliant(false)]
		public virtual ulong MaximumSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset MaximumSize."));
			}
		}

		/// <summary>Gets an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> enumeration value specifying the scope used to isolate the store.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />  values specifying the scope used to isolate the store.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000A8429 File Offset: 0x000A6629
		public IsolatedStorageScope Scope
		{
			get
			{
				return this.storage_scope;
			}
		}

		/// <summary>When overridden in a derived class, gets the available free space for isolated storage, in bytes.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000A8431 File Offset: 0x000A6631
		[ComVisible(false)]
		public virtual long AvailableFreeSpace
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000A8431 File Offset: 0x000A6631
		[ComVisible(false)]
		public virtual long Quota
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used amount of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000A8431 File Offset: 0x000A6631
		[ComVisible(false)]
		public virtual long UsedSize
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>Gets a backslash character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '\' (backslash) character.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000A843D File Offset: 0x000A663D
		protected virtual char SeparatorExternal
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		/// <summary>Gets a period character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '.' (period) character.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000A8444 File Offset: 0x000A6644
		protected virtual char SeparatorInternal
		{
			get
			{
				return '.';
			}
		}

		/// <summary>When implemented by a derived class, returns a permission that represents access to isolated storage from within a permission set.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> object.</returns>
		/// <param name="ps">The <see cref="T:System.Security.PermissionSet" /> object that contains the set of permissions granted to code attempting to use isolated storage. </param>
		// Token: 0x06002F0E RID: 12046
		protected abstract IsolatedStoragePermission GetPermission(PermissionSet ps);

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values. </param>
		/// <param name="domainEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <param name="assemblyEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the assembly of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores. </exception>
		// Token: 0x06002F0F RID: 12047 RVA: 0x000A8448 File Offset: 0x000A6648
		protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
			{
				throw new NotImplementedException(scope.ToString());
			}
			throw new ArgumentException(scope.ToString());
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values. </param>
		/// <param name="appEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> for the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores. </exception>
		// Token: 0x06002F10 RID: 12048 RVA: 0x000A8477 File Offset: 0x000A6677
		[MonoTODO("requires manifest support")]
		protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
		{
			if (AppDomain.CurrentDomain.ApplicationIdentity == null)
			{
				throw new IsolatedStorageException(Locale.GetText("No ApplicationIdentity available for AppDomain."));
			}
			appEvidenceType == null;
			this.storage_scope = scope;
		}

		/// <summary>When overridden in a derived class, removes the individual isolated store and all contained data.</summary>
		// Token: 0x06002F11 RID: 12049
		public abstract void Remove();

		/// <summary>When overridden in a derived class, prompts a user to approve a larger quota size, in bytes, for isolated storage.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="newQuotaSize">The requested new quota size, in bytes, for the user to approve.</param>
		// Token: 0x06002F12 RID: 12050 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		public virtual bool IncreaseQuotaTo(long newQuotaSize)
		{
			return false;
		}

		// Token: 0x04001854 RID: 6228
		internal IsolatedStorageScope storage_scope;

		// Token: 0x04001855 RID: 6229
		internal object _assemblyIdentity;

		// Token: 0x04001856 RID: 6230
		internal object _domainIdentity;

		// Token: 0x04001857 RID: 6231
		internal object _applicationIdentity;
	}
}
