using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path. This class cannot be inherited.</summary>
	// Token: 0x020007EF RID: 2031
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x06005189 RID: 20873 RVA: 0x00002111 File Offset: 0x00000311
		private CallContext()
		{
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0000A42E File Offset: 0x0000862E
		internal static object SetCurrentCallContext(LogicalCallContext ctx)
		{
			return null;
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00120CC4 File Offset: 0x0011EEC4
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		/// <summary>Empties a data slot with the specified name.</summary>
		/// <param name="name">The name of the data slot to empty. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600518C RID: 20876 RVA: 0x00120CE9 File Offset: 0x0011EEE9
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
		}

		/// <summary>Retrieves an object with the specified name from the logical call context.</summary>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x0600518D RID: 20877 RVA: 0x00120D0C File Offset: 0x0011EF0C
		[SecurityCritical]
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00120D34 File Offset: 0x0011EF34
		private static object IllogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x00120D5C File Offset: 0x0011EF5C
		// (set) Token: 0x06005190 RID: 20880 RVA: 0x00120D83 File Offset: 0x0011EF83
		internal static IPrincipal Principal
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
			}
			[SecurityCritical]
			set
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
			}
		}

		/// <summary>Gets or sets the host context associated with the current thread.</summary>
		/// <returns>The host context associated with the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x00120D9C File Offset: 0x0011EF9C
		// (set) Token: 0x06005192 RID: 20882 RVA: 0x00120DD8 File Offset: 0x0011EFD8
		public static object HostContext
		{
			[SecurityCritical]
			get
			{
				ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
				object obj = executionContextReader.IllogicalCallContext.HostContext;
				if (obj == null)
				{
					obj = executionContextReader.LogicalCallContext.HostContext;
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				if (value is ILogicalThreadAffinative)
				{
					mutableExecutionContext.IllogicalCallContext.HostContext = null;
					mutableExecutionContext.LogicalCallContext.HostContext = value;
					return;
				}
				mutableExecutionContext.IllogicalCallContext.HostContext = value;
				mutableExecutionContext.LogicalCallContext.HostContext = null;
			}
		}

		/// <summary>Retrieves an object with the specified name from the <see cref="T:System.Runtime.Remoting.Messaging.CallContext" />.</summary>
		/// <returns>The object in the call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005193 RID: 20883 RVA: 0x00120E2C File Offset: 0x0011F02C
		[SecurityCritical]
		public static object GetData(string name)
		{
			object obj = CallContext.LogicalGetData(name);
			if (obj == null)
			{
				return CallContext.IllogicalGetData(name);
			}
			return obj;
		}

		/// <summary>Stores a given object and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context. </param>
		/// <param name="data">The object to store in the call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005194 RID: 20884 RVA: 0x00120E4B File Offset: 0x0011F04B
		[SecurityCritical]
		public static void SetData(string name, object data)
		{
			if (data is ILogicalThreadAffinative)
			{
				CallContext.LogicalSetData(name, data);
				return;
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.SetData(name, data);
		}

		/// <summary>Stores a given object in the logical call context and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the logical call context. </param>
		/// <param name="data">The object to store in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06005195 RID: 20885 RVA: 0x00120E7F File Offset: 0x0011F07F
		[SecurityCritical]
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}

		/// <summary>Returns the headers that are sent along with the method call.</summary>
		/// <returns>The headers that are sent along with the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005196 RID: 20886 RVA: 0x00120EA3 File Offset: 0x0011F0A3
		[SecurityCritical]
		public static Header[] GetHeaders()
		{
			return Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalGetHeaders();
		}

		/// <summary>Sets the headers that are sent along with the method call.</summary>
		/// <param name="headers">A <see cref="T:System.Runtime.Remoting.Messaging.Header" /> array of the headers that are to be sent along with the method call. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005197 RID: 20887 RVA: 0x00120EB9 File Offset: 0x0011F0B9
		[SecurityCritical]
		public static void SetHeaders(Header[] headers)
		{
			Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalSetHeaders(headers);
		}
	}
}
