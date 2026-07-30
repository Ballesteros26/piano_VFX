using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for parallel loops and regions.</summary>
	// Token: 0x020004D6 RID: 1238
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Parallel
	{
		/// <summary>Executes each of the provided actions, possibly in parallel.</summary>
		/// <param name="actions">An array of <see cref="T:System.Action" /> to execute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a null element.</exception>
		// Token: 0x0600396E RID: 14702 RVA: 0x000CF3DC File Offset: 0x000CD5DC
		public static void Invoke(params Action[] actions)
		{
			Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
		}

		/// <summary>Executes each of the provided actions, possibly in parallel, unless the operation is cancelled by the user.</summary>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="actions">An array of actions to execute.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> is set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a null element.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600396F RID: 14703 RVA: 0x000CF3EC File Offset: 0x000CD5EC
		public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (parallelOptions.CancellationToken.CanBeCanceled && AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				parallelOptions.CancellationToken.ThrowIfSourceDisposed();
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			Action[] actionsCopy = new Action[actions.Length];
			for (int i = 0; i < actionsCopy.Length; i++)
			{
				actionsCopy[i] = actions[i];
				if (actionsCopy[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("One of the actions was null."));
				}
			}
			if (actionsCopy.Length < 1)
			{
				return;
			}
			if (actionsCopy.Length > 10 || (parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length))
			{
				ConcurrentQueue<Exception> exceptionQ = null;
				try
				{
					int actionIndex = 0;
					ParallelForReplicatingTask parallelForReplicatingTask = new ParallelForReplicatingTask(parallelOptions, delegate
					{
						for (int l = Interlocked.Increment(ref actionIndex); l <= actionsCopy.Length; l = Interlocked.Increment(ref actionIndex))
						{
							try
							{
								actionsCopy[l - 1]();
							}
							catch (Exception ex5)
							{
								LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
								exceptionQ.Enqueue(ex5);
							}
							if (parallelOptions.CancellationToken.IsCancellationRequested)
							{
								throw new OperationCanceledException(parallelOptions.CancellationToken);
							}
						}
					}, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
					parallelForReplicatingTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
					parallelForReplicatingTask.Wait();
				}
				catch (Exception ex)
				{
					LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
					AggregateException ex2 = ex as AggregateException;
					if (ex2 != null)
					{
						using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception ex3 = enumerator.Current;
								exceptionQ.Enqueue(ex3);
							}
							goto IL_0208;
						}
					}
					exceptionQ.Enqueue(ex);
					IL_0208:;
				}
				if (exceptionQ != null && exceptionQ.Count > 0)
				{
					Parallel.ThrowIfReducableToSingleOCE(exceptionQ, parallelOptions.CancellationToken);
					throw new AggregateException(exceptionQ);
				}
			}
			else
			{
				Task[] array = new Task[actionsCopy.Length];
				if (parallelOptions.CancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(parallelOptions.CancellationToken);
				}
				for (int j = 1; j < array.Length; j++)
				{
					array[j] = Task.Factory.StartNew(actionsCopy[j], parallelOptions.CancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, parallelOptions.EffectiveTaskScheduler);
				}
				array[0] = new Task(actionsCopy[0]);
				array[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				try
				{
					if (array.Length <= 4)
					{
						Task.FastWaitAll(array);
					}
					else
					{
						Task.WaitAll(array);
					}
				}
				catch (AggregateException ex4)
				{
					Parallel.ThrowIfReducableToSingleOCE(ex4.InnerExceptions, parallelOptions.CancellationToken);
					throw;
				}
				finally
				{
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].IsCompleted)
						{
							array[k].Dispose();
						}
					}
				}
			}
		}

		/// <summary>Executes a for (For in Visual Basic) loop in which iterations may run in parallel.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003970 RID: 14704 RVA: 0x000CF774 File Offset: 0x000CD974
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003971 RID: 14705 RVA: 0x000CF795 File Offset: 0x000CD995
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop in which iterations may run in parallel and loop options can be configured.</summary>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003972 RID: 14706 RVA: 0x000CF7B6 File Offset: 0x000CD9B6
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a for  (For in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and loop options can be configured.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003973 RID: 14707 RVA: 0x000CF7E1 File Offset: 0x000CD9E1
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop in which iterations may run in parallel and the state of the loop can be monitored and manipulated. </summary>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003974 RID: 14708 RVA: 0x000CF80C File Offset: 0x000CDA0C
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.ParallelLoopResult" /> structure that contains information on what portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003975 RID: 14709 RVA: 0x000CF82D File Offset: 0x000CDA2D
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003976 RID: 14710 RVA: 0x000CF84E File Offset: 0x000CDA4E
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic)  loop with 64-bit indexes in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003977 RID: 14711 RVA: 0x000CF879 File Offset: 0x000CDA79
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a for (For in Visual Basic) loop with thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003978 RID: 14712 RVA: 0x000CF8A4 File Offset: 0x000CDAA4
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a for (For in Visual Basic)  loop with 64-bit indexes and thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003979 RID: 14713 RVA: 0x000CF8E3 File Offset: 0x000CDAE3
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a for (For in Visual Basic)  loop with thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600397A RID: 14714 RVA: 0x000CF924 File Offset: 0x000CDB24
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a for (For in Visual Basic) loop with 64-bit indexes and thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each thread.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each thread.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600397B RID: 14715 RVA: 0x000CF97C File Offset: 0x000CDB7C
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000CF9D4 File Offset: 0x000CDBD4
		private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager((long)fromInclusive, (long)toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					int num2;
					int num3;
					if (!rangeWorker.FindNewWork32(out num2, out num3) || sharedPStateFlags.ShouldExitLoop(num2))
					{
						return;
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState32 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (int i = num2; i < num3; i++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(i);
								}
							}
							else if (bodyWithState != null)
							{
								for (int j = num2; j < num3; j++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(j))
									{
										break;
									}
									parallelLoopState.CurrentIteration = j;
									bodyWithState(j, parallelLoopState);
								}
							}
							else
							{
								int num4 = num2;
								while (num4 < num3 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num4)))
								{
									parallelLoopState.CurrentIteration = num4;
									tlocal = bodyWithLocal(num4, parallelLoopState, tlocal);
									num4++;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork32(out num2, out num3) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num2)))
							{
								goto IL_01FD;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_01FD:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?((long)sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000CFC38 File Offset: 0x000CDE38
		private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					long num2;
					long num3;
					if (!rangeWorker.FindNewWork(out num2, out num3) || sharedPStateFlags.ShouldExitLoop(num2))
					{
						return;
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState64 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (long num4 = num2; num4 < num3; num4 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(num4);
								}
							}
							else if (bodyWithState != null)
							{
								for (long num5 = num2; num5 < num3; num5 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num5))
									{
										break;
									}
									parallelLoopState.CurrentIteration = num5;
									bodyWithState(num5, parallelLoopState);
								}
							}
							else
							{
								long num6 = num2;
								while (num6 < num3 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num6)))
								{
									parallelLoopState.CurrentIteration = num6;
									tlocal = bodyWithLocal(num6, parallelLoopState, tlocal);
									num6 += 1L;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork(out num2, out num3) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num2)))
							{
								goto IL_0200;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_0200:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
			}
			return parallelLoopResult;
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600397E RID: 14718 RVA: 0x000CFE98 File Offset: 0x000CE098
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600397F RID: 14719 RVA: 0x000CFED4 File Offset: 0x000CE0D4
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003980 RID: 14720 RVA: 0x000CFF1C File Offset: 0x000CE11C
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003981 RID: 14721 RVA: 0x000CFF58 File Offset: 0x000CE158
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003982 RID: 14722 RVA: 0x000CFFA0 File Offset: 0x000CE1A0
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003983 RID: 14723 RVA: 0x000CFFDC File Offset: 0x000CE1DC
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003984 RID: 14724 RVA: 0x000D0024 File Offset: 0x000CE224
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated..</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003985 RID: 14725 RVA: 0x000D007C File Offset: 0x000CE27C
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003986 RID: 14726 RVA: 0x000D00E0 File Offset: 0x000CE2E0
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data and 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06003987 RID: 14727 RVA: 0x000D0138 File Offset: 0x000CE338
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000D019C File Offset: 0x000CE39C
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000D021C File Offset: 0x000CE41C
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			int lowerBound = array.GetLowerBound(0);
			int num = array.GetUpperBound(0) + 1;
			if (body != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, delegate(int i)
				{
					body(array[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(array[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(array[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(array[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(array[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000D0318 File Offset: 0x000CE518
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (body != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, delegate(int i)
				{
					body(list[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(list[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(list[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(list[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(list[i], state, (long)i, local), localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.-or-The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x0600398B RID: 14731 RVA: 0x000D042C File Offset: 0x000CE62C
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-A method in the <paramref name="source" /> partitioner returns null.-or-The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x0600398C RID: 14732 RVA: 0x000D0468 File Offset: 0x000CE668
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the source orderable partitioner returns false.-or-Any methods in the source orderable partitioner return null.</exception>
		// Token: 0x0600398D RID: 14733 RVA: 0x000D04A4 File Offset: 0x000CE6A4
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true."));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600398E RID: 14734 RVA: 0x000D04F8 File Offset: 0x000CE6F8
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600398F RID: 14735 RVA: 0x000D0550 File Offset: 0x000CE750
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true."));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a following examplereach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.</exception>
		// Token: 0x06003990 RID: 14736 RVA: 0x000D05C0 File Offset: 0x000CE7C0
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.</exception>
		// Token: 0x06003991 RID: 14737 RVA: 0x000D0608 File Offset: 0x000CE808
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> orderable partitioner return null.</exception>
		// Token: 0x06003992 RID: 14738 RVA: 0x000D0650 File Offset: 0x000CE850
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true."));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation  with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003993 RID: 14739 RVA: 0x000D06B0 File Offset: 0x000CE8B0
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a foreach (For Each in Visual Basic) operation with 64-bit indexes and  with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel , loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> or <paramref name="localFinally" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null  partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06003994 RID: 14740 RVA: 0x000D0714 File Offset: 0x000CE914
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true."));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000D0790 File Offset: 0x000CE990
		private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
			if (!source.SupportsDynamicPartitions)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The Partitioner used here must support dynamic partitioning."));
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			IEnumerable<TSource> partitionerSource = null;
			IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = null;
			if (orderedSource != null)
			{
				orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
				if (orderablePartitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The Partitioner used here returned a null partitioner source."));
				}
			}
			else
			{
				partitionerSource = source.GetDynamicPartitions();
				if (partitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The Partitioner used here returned a null partitioner source."));
				}
			}
			ParallelForReplicatingTask rootTask = null;
			Action action = delegate
			{
				Task internalCurrent = Task.InternalCurrent;
				TLocal tlocal = default(TLocal);
				bool flag = false;
				IDisposable disposable2 = null;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (bodyWithState != null || bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
					}
					else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						if (localInit != null)
						{
							tlocal = localInit();
							flag = true;
						}
					}
					bool flag2 = rootTask == internalCurrent;
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
					if (orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = orderablePartitionerSource.GetEnumerator();
							if (enumerator == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("The Partitioner source returned a null enumerator."));
							}
						}
						disposable2 = enumerator;
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (simpleBody != null)
							{
								simpleBody(value);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(value, parallelLoopState);
							}
							else if (bodyWithStateAndIndex != null)
							{
								bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator;
								disposable2 = null;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = partitionerSource.GetEnumerator();
							if (enumerator2 == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("The Partitioner source returned a null enumerator."));
							}
						}
						disposable2 = enumerator2;
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (simpleBody != null)
							{
								simpleBody(tsource);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(tsource, parallelLoopState);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE)
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator2;
								disposable2 = null;
								break;
							}
						}
					}
				}
				catch
				{
					sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (localFinally != null && flag)
					{
						localFinally(tlocal);
					}
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
			};
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, action, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				IDisposable disposable;
				if (orderablePartitionerSource != null)
				{
					disposable = orderablePartitionerSource as IDisposable;
				}
				else
				{
					disposable = partitionerSource as IDisposable;
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000D0A74 File Offset: 0x000CEC74
		internal static void ThrowIfReducableToSingleOCE(IEnumerable<Exception> excCollection, CancellationToken ct)
		{
			bool flag = false;
			if (ct.IsCancellationRequested)
			{
				foreach (Exception ex in excCollection)
				{
					flag = true;
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 == null || ex2.CancellationToken != ct)
					{
						return;
					}
				}
				if (flag)
				{
					throw new OperationCanceledException(ct);
				}
			}
		}

		// Token: 0x04001DFB RID: 7675
		internal static int s_forkJoinContextID;

		// Token: 0x04001DFC RID: 7676
		internal const int DEFAULT_LOOP_STRIDE = 16;

		// Token: 0x04001DFD RID: 7677
		internal static ParallelOptions s_defaultParallelOptions = new ParallelOptions();

		// Token: 0x020004D7 RID: 1239
		internal struct LoopTimer
		{
			// Token: 0x06003998 RID: 14744 RVA: 0x000D0AF0 File Offset: 0x000CECF0
			public LoopTimer(int nWorkerTaskIndex)
			{
				int num = 100 + nWorkerTaskIndex % PlatformHelper.ProcessorCount * 50;
				this.m_timeLimit = Environment.TickCount + num;
			}

			// Token: 0x06003999 RID: 14745 RVA: 0x000D0B18 File Offset: 0x000CED18
			public bool LimitExceeded()
			{
				return Environment.TickCount > this.m_timeLimit;
			}

			// Token: 0x04001DFE RID: 7678
			private const int s_BaseNotifyPeriodMS = 100;

			// Token: 0x04001DFF RID: 7679
			private const int s_NotifyPeriodIncrementMS = 50;

			// Token: 0x04001E00 RID: 7680
			private int m_timeLimit;
		}
	}
}
