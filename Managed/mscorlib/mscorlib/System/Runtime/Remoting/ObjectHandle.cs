using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Wraps marshal-by-value object references, allowing them to be returned through an indirection.</summary>
	// Token: 0x02000753 RID: 1875
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Remoting.ObjectHandle" /> class, wrapping the given object <paramref name="o" />.</summary>
		/// <param name="o">The object that is wrapped by the new <see cref="T:System.Runtime.Remoting.ObjectHandle" />. </param>
		// Token: 0x06004D76 RID: 19830 RVA: 0x00117399 File Offset: 0x00115599
		public ObjectHandle(object o)
		{
			this._wrapped = o;
		}

		/// <summary>Initializes the lifetime lease of the wrapped object.</summary>
		/// <returns>An initialized <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> that allows you to control the lifetime of the wrapped object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D77 RID: 19831 RVA: 0x001173A8 File Offset: 0x001155A8
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		/// <summary>Returns the wrapped object.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x06004D78 RID: 19832 RVA: 0x001173B0 File Offset: 0x001155B0
		public object Unwrap()
		{
			return this._wrapped;
		}

		// Token: 0x040029A2 RID: 10658
		private object _wrapped;
	}
}
