using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method message interface.</summary>
	// Token: 0x0200080E RID: 2062
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>The number of arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600525C RID: 21084
		int ArgCount { get; }

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing the arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600525D RID: 21085
		object[] Args { get; }

		/// <summary>Gets a value indicating whether the message has variable arguments.</summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600525E RID: 21086
		bool HasVarArgs { get; }

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600525F RID: 21087
		LogicalCallContext LogicalCallContext { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06005260 RID: 21088
		MethodBase MethodBase { get; }

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>The name of the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06005261 RID: 21089
		string MethodName { get; }

		/// <summary>Gets an object containing the method signature.</summary>
		/// <returns>An object containing the method signature.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06005262 RID: 21090
		object MethodSignature { get; }

		/// <summary>Gets the full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</summary>
		/// <returns>The full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06005263 RID: 21091
		string TypeName { get; }

		/// <summary>Gets the URI of the specific object that the call is destined for.</summary>
		/// <returns>The URI of the remote object that contains the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06005264 RID: 21092
		string Uri { get; }

		/// <summary>Gets a specific argument as an <see cref="T:System.Object" />.</summary>
		/// <returns>The argument passed to the method.</returns>
		/// <param name="argNum">The number of the requested argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06005265 RID: 21093
		object GetArg(int argNum);

		/// <summary>Gets the name of the argument passed to the method.</summary>
		/// <returns>The name of the specified argument passed to the method, or null if the current method is not implemented.</returns>
		/// <param name="index">The number of the requested argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06005266 RID: 21094
		string GetArgName(int index);
	}
}
