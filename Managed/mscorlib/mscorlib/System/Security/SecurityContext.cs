using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
	/// <summary>Encapsulates and propagates all security-related data for execution contexts transferred across threads. This class cannot be inherited.</summary>
	// Token: 0x0200053A RID: 1338
	public sealed class SecurityContext : IDisposable
	{
		// Token: 0x06003C47 RID: 15431 RVA: 0x00002111 File Offset: 0x00000311
		private SecurityContext()
		{
		}

		/// <summary>Creates a copy of the current security context.</summary>
		/// <returns>The security context for the current thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current security context has been previously used, was marshaled across application domains, or was not acquired through the <see cref="M:System.Security.SecurityContext.Capture" /> method.</exception>
		// Token: 0x06003C48 RID: 15432 RVA: 0x00002119 File Offset: 0x00000319
		public SecurityContext CreateCopy()
		{
			return this;
		}

		/// <summary>Captures the security context for the current thread.</summary>
		/// <returns>The security context for the current thread.</returns>
		// Token: 0x06003C49 RID: 15433 RVA: 0x000D9510 File Offset: 0x000D7710
		public static SecurityContext Capture()
		{
			return new SecurityContext();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.SecurityContext" /> class.</summary>
		// Token: 0x06003C4A RID: 15434 RVA: 0x00002194 File Offset: 0x00000394
		public void Dispose()
		{
		}

		/// <summary>Determines whether the flow of the security context has been suppressed.</summary>
		/// <returns>true if the flow has been suppressed; otherwise, false. </returns>
		// Token: 0x06003C4B RID: 15435 RVA: 0x00015ED5 File Offset: 0x000140D5
		public static bool IsFlowSuppressed()
		{
			return false;
		}

		/// <summary>Determines whether the flow of the Windows identity portion of the current security context has been suppressed.</summary>
		/// <returns>true if the flow has been suppressed; otherwise, false. </returns>
		// Token: 0x06003C4C RID: 15436 RVA: 0x00015ED5 File Offset: 0x000140D5
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return false;
		}

		/// <summary>Restores the flow of the security context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The security context is null or an empty string.</exception>
		// Token: 0x06003C4D RID: 15437 RVA: 0x00002194 File Offset: 0x00000394
		public static void RestoreFlow()
		{
		}

		/// <summary>Runs the specified method in the specified security context on the current thread.</summary>
		/// <param name="securityContext">The security context to set.</param>
		/// <param name="callback">The delegate that represents the method to run in the specified security context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="securityContext" /> is null.-or-<paramref name="securityContext" /> was not acquired through a capture operation. -or-<paramref name="securityContext" /> has already been used as the argument to a <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> method call.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C4E RID: 15438 RVA: 0x000D9517 File Offset: 0x000D7717
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			callback(state);
		}

		/// <summary>Suppresses the flow of the security context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C4F RID: 15439 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public static AsyncFlowControl SuppressFlow()
		{
			throw new NotSupportedException();
		}

		/// <summary>Suppresses the flow of the Windows identity portion of the current security context across asynchronous threads.</summary>
		/// <returns>A structure for restoring the flow.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C50 RID: 15440 RVA: 0x00014B5A File Offset: 0x00012D5A
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			throw new NotSupportedException();
		}
	}
}
