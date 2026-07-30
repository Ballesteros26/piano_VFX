using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to native objects without direct manipulation of Access Control Lists (ACLs). Native object types are defined by the <see cref="T:System.Security.AccessControl.ResourceType" /> enumeration.</summary>
	// Token: 0x020005F8 RID: 1528
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x000EBE3B File Offset: 0x000EA03B
		internal NativeObjectSecurity(CommonSecurityDescriptor securityDescriptor, ResourceType resourceType)
			: base(securityDescriptor)
		{
			this.resource_type = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		// Token: 0x060042EF RID: 17135 RVA: 0x000EBE4B File Offset: 0x000EA04B
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType)
			: this(isContainer, resourceType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class by using the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions. </param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060042F0 RID: 17136 RVA: 0x000EBE57 File Offset: 0x000EA057
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer)
		{
			this.exception_from_error_code = exceptionFromErrorCode;
			this.resource_type = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x060042F1 RID: 17137 RVA: 0x000EBE6E File Offset: 0x000EA06E
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
			: this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x060042F2 RID: 17138 RVA: 0x000EBE7D File Offset: 0x000EA07D
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: this(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions. </param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060042F3 RID: 17139 RVA: 0x000EBE8C File Offset: 0x000EA08C
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(handle, includeSections), null, handle, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions. </param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060042F4 RID: 17140 RVA: 0x000EBEB3 File Offset: 0x000EA0B3
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(name, includeSections), name, null, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000EBEDC File Offset: 0x000EA0DC
		private void ClearAccessControlSectionsModified()
		{
			base.WriteLock();
			try
			{
				base.AccessControlSectionsModified = AccessControlSections.None;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060042F6 RID: 17142 RVA: 0x000EBF10 File Offset: 0x000EA110
		protected sealed override void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			this.Persist(handle, includeSections, null);
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060042F7 RID: 17143 RVA: 0x000EBF1B File Offset: 0x000EA11B
		protected sealed override void Persist(string name, AccessControlSections includeSections)
		{
			this.Persist(name, includeSections, null);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000EBF28 File Offset: 0x000EA128
		internal void PersistModifications(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060042F9 RID: 17145 RVA: 0x000EBF64 File Offset: 0x000EA164
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(handle, includeSections), null, handle, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000EBFB0 File Offset: 0x000EA1B0
		internal void PersistModifications(string name)
		{
			base.WriteLock();
			try
			{
				this.Persist(name, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060042FB RID: 17147 RVA: 0x000EBFEC File Offset: 0x000EA1EC
		protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(name, includeSections), name, null, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000EC048 File Offset: 0x000EA248
		internal static Exception DefaultExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			switch (errorCode)
			{
			case 2:
				return new FileNotFoundException();
			case 3:
				return new DirectoryNotFoundException();
			case 4:
				break;
			case 5:
				return new UnauthorizedAccessException();
			default:
				if (errorCode == 1314)
				{
					return new PrivilegeNotHeldException();
				}
				break;
			}
			return new InvalidOperationException("OS error code " + errorCode.ToString());
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000EC0A5 File Offset: 0x000EA2A5
		private void RaiseExceptionOnFailure(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 0)
			{
				return;
			}
			throw (this.exception_from_error_code ?? new NativeObjectSecurity.ExceptionFromErrorCode(NativeObjectSecurity.DefaultExceptionFromErrorCode))(errorCode, name, handle, context);
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000EC0CC File Offset: 0x000EA2CC
		internal virtual int InternalGet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(handle, this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000EC114 File Offset: 0x000EA314
		internal virtual int InternalGet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000EC15C File Offset: 0x000EA35C
		internal virtual int InternalSet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetSecurityInfo(handle, this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000EC1A4 File Offset: 0x000EA3A4
		internal virtual int InternalSet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x000EC1EB File Offset: 0x000EA3EB
		internal ResourceType ResourceType
		{
			get
			{
				return this.resource_type;
			}
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x000EC1F4 File Offset: 0x000EA3F4
		private int Win32GetHelper(NativeObjectSecurity.GetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			bool flag = (includeSections & AccessControlSections.Owner) > AccessControlSections.None;
			bool flag2 = (includeSections & AccessControlSections.Group) > AccessControlSections.None;
			bool flag3 = (includeSections & AccessControlSections.Access) > AccessControlSections.None;
			bool flag4 = (includeSections & AccessControlSections.Audit) > AccessControlSections.None;
			SecurityInfos securityInfos = (SecurityInfos)0;
			if (flag)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if (flag2)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if (flag3)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			if (flag4)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			IntPtr intPtr5;
			int num = nativeCall(securityInfos, out intPtr, out intPtr2, out intPtr3, out intPtr4, out intPtr5);
			if (num != 0)
			{
				return num;
			}
			try
			{
				int num2 = 0;
				if (NativeObjectSecurity.IsValidSecurityDescriptor(intPtr5))
				{
					num2 = NativeObjectSecurity.GetSecurityDescriptorLength(intPtr5);
				}
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr5, array, 0, num2);
				base.SetSecurityDescriptorBinaryForm(array, includeSections);
			}
			finally
			{
				NativeObjectSecurity.LocalFree(intPtr5);
			}
			return 0;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000EC2A4 File Offset: 0x000EA4A4
		private int Win32SetHelper(NativeObjectSecurity.SetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			if (includeSections == AccessControlSections.None)
			{
				return 0;
			}
			SecurityInfos securityInfos = (SecurityInfos)0;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)base.GetOwner(typeof(SecurityIdentifier));
				if (null != securityIdentifier)
				{
					array = new byte[securityIdentifier.BinaryLength];
					securityIdentifier.GetBinaryForm(array, 0);
				}
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)base.GetGroup(typeof(SecurityIdentifier));
				if (null != securityIdentifier2)
				{
					array2 = new byte[securityIdentifier2.BinaryLength];
					securityIdentifier2.GetBinaryForm(array2, 0);
				}
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
				if (base.AreAccessRulesProtected)
				{
					securityInfos |= (SecurityInfos)(-2147483648);
				}
				else
				{
					securityInfos |= (SecurityInfos)536870912;
				}
				array3 = new byte[this.descriptor.DiscretionaryAcl.BinaryLength];
				this.descriptor.DiscretionaryAcl.GetBinaryForm(array3, 0);
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.descriptor.SystemAcl != null)
			{
				securityInfos |= SecurityInfos.SystemAcl;
				if (base.AreAuditRulesProtected)
				{
					securityInfos |= (SecurityInfos)1073741824;
				}
				else
				{
					securityInfos |= (SecurityInfos)268435456;
				}
				array4 = new byte[this.descriptor.SystemAcl.BinaryLength];
				this.descriptor.SystemAcl.GetBinaryForm(array4, 0);
			}
			return nativeCall(securityInfos, array, array2, array3, array4);
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000EC3F2 File Offset: 0x000EA5F2
		private string Win32FixName(string name)
		{
			if (this.ResourceType == ResourceType.RegistryKey)
			{
				if (!name.StartsWith("HKEY_"))
				{
					throw new InvalidOperationException();
				}
				name = name.Substring("HKEY_".Length);
			}
			return name;
		}

		// Token: 0x06004306 RID: 17158
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x06004307 RID: 17159
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x06004308 RID: 17160
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06004309 RID: 17161
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x0600430A RID: 17162
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x0600430B RID: 17163
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityDescriptorLength(IntPtr descriptor);

		// Token: 0x0600430C RID: 17164
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsValidSecurityDescriptor(IntPtr descriptor);

		// Token: 0x040021CC RID: 8652
		private NativeObjectSecurity.ExceptionFromErrorCode exception_from_error_code;

		// Token: 0x040021CD RID: 8653
		private ResourceType resource_type;

		/// <summary>Provides a way for integrators to map numeric error codes to specific exceptions that they create.</summary>
		/// <returns>The <see cref="T:System.Exception" /> this delegate creates.</returns>
		/// <param name="errorCode">The numeric error code.</param>
		/// <param name="name">The name of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="context">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x020005F9 RID: 1529
		// (Invoke) Token: 0x0600430E RID: 17166
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);

		// Token: 0x020005FA RID: 1530
		// (Invoke) Token: 0x06004312 RID: 17170
		private delegate int GetSecurityInfoNativeCall(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x020005FB RID: 1531
		// (Invoke) Token: 0x06004316 RID: 17174
		private delegate int SetSecurityInfoNativeCall(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);
	}
}
