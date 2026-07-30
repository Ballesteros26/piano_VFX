using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />, where the <see cref="P:System.Runtime.InteropServices.DispatchWrapper.WrappedObject" /> is null.</summary>
	// Token: 0x02000879 RID: 2169
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the IDispatch constant stored in this attribute.</summary>
		/// <returns>The IDispatch constant stored in this attribute. Only null is allowed for an IDispatch constant value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x00127646 File Offset: 0x00125846
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}
