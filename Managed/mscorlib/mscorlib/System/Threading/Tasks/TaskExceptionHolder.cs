using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000519 RID: 1305
	internal class TaskExceptionHolder
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x000D7091 File Offset: 0x000D5291
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
			TaskExceptionHolder.EnsureADUnloadCallbackRegistered();
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000D70A5 File Offset: 0x000D52A5
		[SecuritySafeCritical]
		private static bool ShouldFailFastOnUnobservedException()
		{
			return CLRConfig.CheckThrowUnobservedTaskExceptions();
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000D70AC File Offset: 0x000D52AC
		private static void EnsureADUnloadCallbackRegistered()
		{
			if (TaskExceptionHolder.s_adUnloadEventHandler == null && Interlocked.CompareExchange<EventHandler>(ref TaskExceptionHolder.s_adUnloadEventHandler, new EventHandler(TaskExceptionHolder.AppDomainUnloadCallback), null) == null)
			{
				AppDomain.CurrentDomain.DomainUnload += TaskExceptionHolder.s_adUnloadEventHandler;
			}
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000D70E1 File Offset: 0x000D52E1
		private static void AppDomainUnloadCallback(object sender, EventArgs e)
		{
			TaskExceptionHolder.s_domainUnloadStarted = true;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000D70EC File Offset: 0x000D52EC
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !TaskExceptionHolder.s_domainUnloadStarted)
				{
					foreach (ExceptionDispatchInfo exceptionDispatchInfo in this.m_faultExceptions)
					{
						Exception sourceException = exceptionDispatchInfo.SourceException;
						AggregateException ex = sourceException as AggregateException;
						if (ex != null)
						{
							using (IEnumerator<Exception> enumerator2 = ex.Flatten().InnerExceptions.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current is ThreadAbortException)
									{
										return;
									}
								}
								continue;
							}
						}
						if (sourceException is ThreadAbortException)
						{
							return;
						}
					}
					AggregateException ex2 = new AggregateException(Environment.GetResourceString("A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread."), this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex2);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex2;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000D7254 File Offset: 0x000D5454
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000D7261 File Offset: 0x000D5461
		internal void Add(object exceptionObject)
		{
			this.Add(exceptionObject, false);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000D726B File Offset: 0x000D546B
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000D7280 File Offset: 0x000D5480
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = exceptionDispatchInfo;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000D72BC File Offset: 0x000D54BC
		private void AddFaultException(object exceptionObject)
		{
			List<ExceptionDispatchInfo> list = this.m_faultExceptions;
			if (list == null)
			{
				list = (this.m_faultExceptions = new List<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				list.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					list.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception ex2 = enumerator.Current;
								list.Add(ExceptionDispatchInfo.Capture(ex2));
							}
							goto IL_00B3;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException(Environment.GetResourceString("(Internal)Expected an Exception or an IEnumerable<Exception>"), "exceptionObject");
					}
					list.AddRange(enumerable2);
				}
			}
			IL_00B3:
			for (int i = 0; i < list.Count; i++)
			{
				Type type = list[i].SourceException.GetType();
				if (type != typeof(ThreadAbortException) && type != typeof(AppDomainUnloadedException))
				{
					this.MarkAsUnhandled();
					return;
				}
				if (i == list.Count - 1)
				{
					this.MarkAsHandled(false);
				}
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000D73F8 File Offset: 0x000D55F8
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000D7413 File Offset: 0x000D5613
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000D7434 File Offset: 0x000D5634
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000D7496 File Offset: 0x000D5696
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			IList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000D74AC File Offset: 0x000D56AC
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x04001EFB RID: 7931
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04001EFC RID: 7932
		private static volatile bool s_domainUnloadStarted;

		// Token: 0x04001EFD RID: 7933
		private static volatile EventHandler s_adUnloadEventHandler;

		// Token: 0x04001EFE RID: 7934
		private readonly Task m_task;

		// Token: 0x04001EFF RID: 7935
		private volatile List<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04001F00 RID: 7936
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04001F01 RID: 7937
		private volatile bool m_isHandled;
	}
}
