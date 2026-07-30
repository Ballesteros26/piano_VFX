using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004D3 RID: 1235
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x04001DF3 RID: 7667
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(task);
		};

		// Token: 0x04001DF4 RID: 7668
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> task2 = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(task2);
			return default(TResult);
		};

		// Token: 0x04001DF5 RID: 7669
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			return ((Func<Task<TAntecedentResult>[], TResult>)state)(wrappedAntecedents.Result);
		};

		// Token: 0x04001DF6 RID: 7670
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			((Action<Task<TAntecedentResult>[]>)state)(wrappedAntecedents.Result);
			return default(TResult);
		};
	}
}
