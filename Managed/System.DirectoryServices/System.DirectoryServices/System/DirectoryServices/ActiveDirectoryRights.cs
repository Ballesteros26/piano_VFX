using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration specifies the access rights that are assigned to an Active Directory Domain Services object.</summary>
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum ActiveDirectoryRights
	{
		/// <summary>The right to delete the object.</summary>
		// Token: 0x04000032 RID: 50
		Delete = 65536,
		/// <summary>The right to read data from the security descriptor of the object, not including the data in the SACL.</summary>
		// Token: 0x04000033 RID: 51
		ReadControl = 131072,
		/// <summary>The right to modify the DACL in the object security descriptor.</summary>
		// Token: 0x04000034 RID: 52
		WriteDacl = 262144,
		/// <summary>The right to assume ownership of the object. The user must be an object trustee. The user cannot transfer the ownership to other users.</summary>
		// Token: 0x04000035 RID: 53
		WriteOwner = 524288,
		/// <summary>The right to use the object for synchronization. This right enables a thread to wait until that object is in the signaled state.</summary>
		// Token: 0x04000036 RID: 54
		Synchronize = 1048576,
		/// <summary>The right to get or set the SACL in the object security descriptor.</summary>
		// Token: 0x04000037 RID: 55
		AccessSystemSecurity = 16777216,
		/// <summary>The right to read permissions on this object, read all the properties on this object, list this object name when the parent container is listed, and list the contents of this object if it is a container.</summary>
		// Token: 0x04000038 RID: 56
		GenericRead = 131220,
		/// <summary>The right to read permissions on this object, write all the properties on this object, and perform all validated writes to this object.</summary>
		// Token: 0x04000039 RID: 57
		GenericWrite = 131112,
		/// <summary>The right to read permissions on, and list the contents of, a container object.</summary>
		// Token: 0x0400003A RID: 58
		GenericExecute = 131076,
		/// <summary>The right to create or delete children, delete a subtree, read and write properties, examine children and the object itself, add and remove the object from the directory, and read or write with an extended right.</summary>
		// Token: 0x0400003B RID: 59
		GenericAll = 983551,
		/// <summary>The right to create children of the object.</summary>
		// Token: 0x0400003C RID: 60
		CreateChild = 1,
		/// <summary>The right to delete children of the object.</summary>
		// Token: 0x0400003D RID: 61
		DeleteChild = 2,
		/// <summary>The right to list children of this object. For more information about this right, see the topic "Controlling Object Visibility" in the MSDN Library http://msdn.microsoft.com/library.</summary>
		// Token: 0x0400003E RID: 62
		ListChildren = 4,
		/// <summary>The right to perform an operation that is controlled by a validated write access right.</summary>
		// Token: 0x0400003F RID: 63
		Self = 8,
		/// <summary>The right to read properties of the object.</summary>
		// Token: 0x04000040 RID: 64
		ReadProperty = 16,
		/// <summary>The right to write properties of the object.</summary>
		// Token: 0x04000041 RID: 65
		WriteProperty = 32,
		/// <summary>The right to delete all children of this object, regardless of the permissions of the children.</summary>
		// Token: 0x04000042 RID: 66
		DeleteTree = 64,
		/// <summary>The right to list a particular object. For more information about this right, see the topic "Controlling Object Visibility" in the MSDN Library at http://msdn.microsoft.com/library.  </summary>
		// Token: 0x04000043 RID: 67
		ListObject = 128,
		/// <summary>A customized control access right. For a list of possible extended rights, see the topic "Extended Rights" in the MSDN Library at http://msdn.microsoft.com. For more information about extended rights, see the topic "Control Access Rights" in the MSDN Library at http://msdn.microsoft.com.</summary>
		// Token: 0x04000044 RID: 68
		ExtendedRight = 256
	}
}
