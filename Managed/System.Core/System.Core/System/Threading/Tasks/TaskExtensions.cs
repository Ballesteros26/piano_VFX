using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides a set of static (Shared in Visual Basic) methods for working with specific kinds of <see cref="T:System.Threading.Tasks.Task" /> instances.</summary>
	// Token: 0x02000027 RID: 39
	public static class TaskExtensions
	{
		/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a <see cref="M:System.Threading.Tasks.TaskScheduler.TryExecuteTaskInline(System.Threading.Tasks.Task,System.Boolean)" />.</summary>
		/// <returns>A Task that represents the asynchronous operation of the provided System.Threading.Tasks.Task(Of Task).</returns>
		/// <param name="task">The Task&lt;Task&gt; (C#) or Task (Of Task) (Visual Basic) to unwrap.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
		// Token: 0x060000AB RID: 171 RVA: 0x000032FC File Offset: 0x000014FC
		public static Task Unwrap(this Task<Task> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			return Task.CreateUnwrapPromise<TaskExtensions.VoidResult>(task, false);
		}

		/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a Task&lt;Task&lt;T&gt;&gt; (C#) or Task (Of Task(Of T)) (Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of the provided Task&lt;Task&lt;T&gt;&gt; (C#) or Task (Of Task(Of T)) (Visual Basic).</returns>
		/// <param name="task">The Task&lt;Task&lt;T&gt;&gt; (C#) or Task (Of Task(Of T)) (Visual Basic) to unwrap.</param>
		/// <typeparam name="TResult">The type of the task's result.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
		// Token: 0x060000AC RID: 172 RVA: 0x00003313 File Offset: 0x00001513
		public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			return Task.CreateUnwrapPromise<TResult>(task, false);
		}

		// Token: 0x02000028 RID: 40
		private struct VoidResult
		{
		}
	}
}
