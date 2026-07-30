using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Allows access to field names and field types of objects that support the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
	// Token: 0x020006FC RID: 1788
	[ComVisible(true)]
	public interface IFieldInfo
	{
		/// <summary>Gets or sets the field names of serialized objects.</summary>
		/// <returns>The field names of serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004B19 RID: 19225
		// (set) Token: 0x06004B1A RID: 19226
		string[] FieldNames
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets or sets the field types of the serialized objects.</summary>
		/// <returns>The field types of the serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004B1B RID: 19227
		// (set) Token: 0x06004B1C RID: 19228
		Type[] FieldTypes
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
