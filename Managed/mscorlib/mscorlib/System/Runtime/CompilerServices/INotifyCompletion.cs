using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an operation that schedules continuations when it completes.</summary>
	// Token: 0x0200084C RID: 2124
	public interface INotifyCompletion
	{
		/// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
		/// <param name="continuation">The action to invoke when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null (Nothing in Visual Basic).</exception>
		// Token: 0x060053F9 RID: 21497
		void OnCompleted(Action continuation);
	}
}
