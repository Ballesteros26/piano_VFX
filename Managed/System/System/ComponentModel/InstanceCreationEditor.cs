using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />. </summary>
	// Token: 0x02000293 RID: 659
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class InstanceCreationEditor
	{
		/// <summary>Gets the specified text.</summary>
		/// <returns>The specified text.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00052E54 File Offset: 0x00051054
		public virtual string Text
		{
			get
			{
				return global::SR.GetString("(New...)");
			}
		}

		/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
		/// <returns>An instance of the specified type or null.</returns>
		/// <param name="context">The context information.</param>
		/// <param name="instanceType">The specified type.</param>
		// Token: 0x0600149B RID: 5275
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);
	}
}
