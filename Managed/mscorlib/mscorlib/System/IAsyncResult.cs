using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents the status of an asynchronous operation. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	public interface IAsyncResult
	{
		/// <summary>Gets a value that indicates whether the asynchronous operation has completed.</summary>
		/// <returns>true if the operation is complete; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600108B RID: 4235
		bool IsCompleted { get; }

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600108C RID: 4236
		WaitHandle AsyncWaitHandle { get; }

		/// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600108D RID: 4237
		object AsyncState { get; }

		/// <summary>Gets a value that indicates whether the asynchronous operation completed synchronously.</summary>
		/// <returns>true if the asynchronous operation completed synchronously; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600108E RID: 4238
		bool CompletedSynchronously { get; }
	}
}
