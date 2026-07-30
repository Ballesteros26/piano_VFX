using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x0200022D RID: 557
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public static class AsyncOperationManager
	{
		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. </param>
		// Token: 0x060011FA RID: 4602 RVA: 0x0004CD11 File Offset: 0x0004AF11
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004CD1E File Offset: 0x0004AF1E
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x0004CD36 File Offset: 0x0004AF36
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
}
