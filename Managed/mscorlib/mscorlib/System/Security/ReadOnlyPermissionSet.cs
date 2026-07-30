using System;
using System.Collections;
using Unity;

namespace System.Security
{
	/// <summary>Represents a read-only collection that can contain many different types of permissions.</summary>
	// Token: 0x02000B5C RID: 2908
	[Serializable]
	public sealed class ReadOnlyPermissionSet : PermissionSet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.ReadOnlyPermissionSet" /> class. </summary>
		/// <param name="permissionSetXml">The XML element from which to take the value of the new <see cref="T:System.Security.ReadOnlyPermissionSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionSetXml" /> is null.</exception>
		// Token: 0x060065F4 RID: 26100 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public ReadOnlyPermissionSet(SecurityElement permissionSetXml)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x00032521 File Offset: 0x00030721
		protected override IPermission AddPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00032521 File Offset: 0x00030721
		protected override IEnumerator GetEnumeratorImpl()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x00032521 File Offset: 0x00030721
		protected override IPermission GetPermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x00032521 File Offset: 0x00030721
		protected override IPermission RemovePermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x00032521 File Offset: 0x00030721
		protected override IPermission SetPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
