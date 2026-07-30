using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Enables communication with a debugger. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A6B RID: 2667
	[MonoTODO("The Debugger class is not functional")]
	[ComVisible(true)]
	public sealed class Debugger
	{
		/// <summary>Gets a value that indicates whether a debugger is attached to the process.</summary>
		/// <returns>true if a debugger is attached; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06006191 RID: 24977 RVA: 0x00140241 File Offset: 0x0013E441
		public static bool IsAttached
		{
			get
			{
				return Debugger.IsAttached_internal();
			}
		}

		// Token: 0x06006192 RID: 24978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAttached_internal();

		/// <summary>Signals a breakpoint to an attached debugger.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to break into the debugger. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06006193 RID: 24979 RVA: 0x00002194 File Offset: 0x00000394
		public static void Break()
		{
		}

		/// <summary>Checks to see if logging is enabled by an attached debugger.</summary>
		/// <returns>true if a debugger is attached and logging is enabled; otherwise, false. The attached debugger is the registered managed debugger in the DbgManagedDebugger registry key. For more information on this key, see Enabling JIT-Attach Debugging.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06006194 RID: 24980
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		/// <summary>Launches and attaches a debugger to the process.</summary>
		/// <returns>true if the startup is successful or if the debugger is already attached; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to start the debugger. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06006195 RID: 24981 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public static bool Launch()
		{
			throw new NotImplementedException();
		}

		/// <summary>Posts a message for the attached debugger.</summary>
		/// <param name="level">A description of the importance of the message. </param>
		/// <param name="category">The category of the message. </param>
		/// <param name="message">The message to show. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06006196 RID: 24982
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Log(int level, string category, string message);

		/// <summary>Notifies a debugger that execution is about to enter a path that involves a cross-thread dependency.</summary>
		// Token: 0x06006197 RID: 24983 RVA: 0x00002194 File Offset: 0x00000394
		public static void NotifyOfCrossThreadDependency()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Debugger" /> class. </summary>
		// Token: 0x06006198 RID: 24984 RVA: 0x00002111 File Offset: 0x00000311
		[Obsolete("Call the static methods directly on this type", true)]
		public Debugger()
		{
		}

		/// <summary>Represents the default category of message with a constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040030BC RID: 12476
		public static readonly string DefaultCategory = "";
	}
}
