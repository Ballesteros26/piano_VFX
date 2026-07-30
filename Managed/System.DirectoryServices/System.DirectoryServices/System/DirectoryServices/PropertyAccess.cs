using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration is used with the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> and <see cref="T:System.DirectoryServices.PropertySetAccessRule" /> classes to indicate the type of access that is applied to an Active Directory property or property set. </summary>
	// Token: 0x02000024 RID: 36
	public enum PropertyAccess
	{
		/// <summary>Indicates permission to read properties.</summary>
		// Token: 0x04000096 RID: 150
		Read,
		/// <summary>Indicates permission to write properties.</summary>
		// Token: 0x04000097 RID: 151
		Write
	}
}
