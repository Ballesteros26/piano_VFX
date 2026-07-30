using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
	// Token: 0x020002AD RID: 685
	internal sealed class StackGuard
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x0003CDB2 File Offset: 0x0003AFB2
		public bool TryEnterOnCurrentStack()
		{
			if (RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				return true;
			}
			if (this._executionStackCount < 1024)
			{
				return false;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003CDD1 File Offset: 0x0003AFD1
		public void RunOnEmptyStack<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2>, T1, T2> tuple = (Tuple<Action<T1, T2>, T1, T2>)s;
				tuple.Item1(tuple.Item2, tuple.Item3);
				return null;
			}, Tuple.Create<Action<T1, T2>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003CE01 File Offset: 0x0003B001
		public void RunOnEmptyStack<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2, T3>, T1, T2, T3> tuple = (Tuple<Action<T1, T2, T3>, T1, T2, T3>)s;
				tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
				return null;
			}, Tuple.Create<Action<T1, T2, T3>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003CE33 File Offset: 0x0003B033
		public R RunOnEmptyStack<T1, T2, R>(Func<T1, T2, R> action, T1 arg1, T2 arg2)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, R>, T1, T2> tuple = (Tuple<Func<T1, T2, R>, T1, T2>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3);
			}, Tuple.Create<Func<T1, T2, R>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003CE62 File Offset: 0x0003B062
		public R RunOnEmptyStack<T1, T2, T3, R>(Func<T1, T2, T3, R> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, T3, R>, T1, T2, T3> tuple = (Tuple<Func<T1, T2, T3, R>, T1, T2, T3>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
			}, Tuple.Create<Func<T1, T2, T3, R>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0003CE94 File Offset: 0x0003B094
		private R RunOnEmptyStackCore<R>(Func<object, R> action, object state)
		{
			this._executionStackCount++;
			R result;
			try
			{
				Task<R> task = Task.Factory.StartNew<R>(action, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
				TaskAwaiter<R> awaiter = task.GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					((IAsyncResult)task).AsyncWaitHandle.WaitOne();
				}
				result = awaiter.GetResult();
			}
			finally
			{
				this._executionStackCount--;
			}
			return result;
		}

		// Token: 0x040009BA RID: 2490
		private const int MaxExecutionStackCount = 1024;

		// Token: 0x040009BB RID: 2491
		private int _executionStackCount;
	}
}
