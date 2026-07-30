using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>Manages the execution context for the current thread. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000474 RID: 1140
	[Serializable]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000C7253 File Offset: 0x000C5453
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x000C7260 File Offset: 0x000C5460
		internal bool isNewCapture
		{
			get
			{
				return (this._flags & (ExecutionContext.Flags)5) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsNewCapture;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-2);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000C7283 File Offset: 0x000C5483
		// (set) Token: 0x060035E4 RID: 13796 RVA: 0x000C7290 File Offset: 0x000C5490
		internal bool isFlowSuppressed
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-3);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000C72B3 File Offset: 0x000C54B3
		internal static ExecutionContext PreAllocatedDefault
		{
			[SecuritySafeCritical]
			get
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000C72BA File Offset: 0x000C54BA
		internal bool IsPreAllocatedDefault
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00002111 File Offset: 0x00000311
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext()
		{
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000C72C9 File Offset: 0x000C54C9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext(bool isPreAllocatedDefault)
		{
			if (isPreAllocatedDefault)
			{
				this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000C72DC File Offset: 0x000C54DC
		[SecurityCritical]
		internal static object GetLocalValue(IAsyncLocal local)
		{
			return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000C72FC File Offset: 0x000C54FC
		[SecurityCritical]
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			object obj = null;
			bool flag = mutableExecutionContext._localValues != null && mutableExecutionContext._localValues.TryGetValue(local, out obj);
			if (obj == newValue)
			{
				return;
			}
			if (mutableExecutionContext._localValues == null)
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>();
			}
			else
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>(mutableExecutionContext._localValues);
			}
			mutableExecutionContext._localValues[local] = newValue;
			if (needChangeNotifications)
			{
				if (!flag)
				{
					if (mutableExecutionContext._localChangeNotifications == null)
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>();
					}
					else
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>(mutableExecutionContext._localChangeNotifications);
					}
					mutableExecutionContext._localChangeNotifications.Add(local);
				}
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000C73AC File Offset: 0x000C55AC
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
		{
			List<IAsyncLocal> list = ((previous == null) ? null : previous._localChangeNotifications);
			if (list != null)
			{
				foreach (IAsyncLocal asyncLocal in list)
				{
					object obj = null;
					if (previous != null && previous._localValues != null)
					{
						previous._localValues.TryGetValue(asyncLocal, out obj);
					}
					object obj2 = null;
					if (current != null && current._localValues != null)
					{
						current._localValues.TryGetValue(asyncLocal, out obj2);
					}
					if (obj != obj2)
					{
						asyncLocal.OnValueChanged(obj, obj2, true);
					}
				}
			}
			List<IAsyncLocal> list2 = ((current == null) ? null : current._localChangeNotifications);
			if (list2 != null && list2 != list)
			{
				try
				{
					foreach (IAsyncLocal asyncLocal2 in list2)
					{
						object obj3 = null;
						if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(asyncLocal2, out obj3))
						{
							object obj4 = null;
							if (current != null && current._localValues != null)
							{
								current._localValues.TryGetValue(asyncLocal2, out obj4);
							}
							if (obj3 != obj4)
							{
								asyncLocal2.OnValueChanged(obj3, obj4, true);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Environment.FailFast(Environment.GetResourceString("An exception was not handled in an AsyncLocal<T> notification callback."), ex);
				}
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000C7514 File Offset: 0x000C5714
		// (set) Token: 0x060035ED RID: 13805 RVA: 0x000C752F File Offset: 0x000C572F
		internal LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._logicalCallContext == null)
				{
					this._logicalCallContext = new LogicalCallContext();
				}
				return this._logicalCallContext;
			}
			[SecurityCritical]
			set
			{
				this._logicalCallContext = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060035EE RID: 13806 RVA: 0x000C7538 File Offset: 0x000C5738
		// (set) Token: 0x060035EF RID: 13807 RVA: 0x000C7553 File Offset: 0x000C5753
		internal IllogicalCallContext IllogicalCallContext
		{
			get
			{
				if (this._illogicalCallContext == null)
				{
					this._illogicalCallContext = new IllogicalCallContext();
				}
				return this._illogicalCallContext;
			}
			set
			{
				this._illogicalCallContext = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000C755C File Offset: 0x000C575C
		// (set) Token: 0x060035F1 RID: 13809 RVA: 0x000C7564 File Offset: 0x000C5764
		internal SynchronizationContext SynchronizationContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000C756D File Offset: 0x000C576D
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x000C7575 File Offset: 0x000C5775
		internal SynchronizationContext SynchronizationContextNoFlow
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContextNoFlow;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContextNoFlow = value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ExecutionContext" /> class.</summary>
		// Token: 0x060035F4 RID: 13812 RVA: 0x000C757E File Offset: 0x000C577E
		public void Dispose()
		{
			bool isPreAllocatedDefault = this.IsPreAllocatedDefault;
		}

		/// <summary>Runs a method in a specified execution context on the current thread.</summary>
		/// <param name="executionContext">The <see cref="T:System.Threading.ExecutionContext" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> delegate that represents the method to be run in the provided execution context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="executionContext" /> is null.-or-<paramref name="executionContext" /> was not acquired through a capture operation. -or-<paramref name="executionContext" /> has already been used as the argument to a <see cref="M:System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext,System.Threading.ContextCallback,System.Object)" /> call.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060035F5 RID: 13813 RVA: 0x000C7587 File Offset: 0x000C5787
		[SecurityCritical]
		public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot call Set on a null context"));
			}
			if (!executionContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot apply a context that has been marshaled across AppDomains, that was not acquired through a Capture operation or that has already been the argument to a Set call."));
			}
			ExecutionContext.Run(executionContext, callback, state, false);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000C75BD File Offset: 0x000C57BD
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000C75C8 File Offset: 0x000C57C8
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000C7668 File Offset: 0x000C5868
		[SecurityCritical]
		internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
		{
			ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000C7676 File Offset: 0x000C5876
		[SecurityCritical]
		private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
		{
			ecsw.outerEC = currentThread.GetExecutionContextReader();
			ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			currentThread.ExecutionContextBelongsToCurrentScope = false;
			ecsw.thread = currentThread;
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000C76A0 File Offset: 0x000C58A0
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
		{
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
			executionContextSwitcher.thread = currentThread;
			executionContextSwitcher.outerEC = executionContextReader;
			executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			if (preserveSyncCtx)
			{
				executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
			}
			executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
			currentThread.SetExecutionContext(executionContext, true);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
			}
			catch
			{
				executionContextSwitcher.UndoNoThrow();
				throw;
			}
			return executionContextSwitcher;
		}

		/// <summary>Creates a copy of the current execution context.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the current execution context.</returns>
		/// <exception cref="T:System.InvalidOperationException">This context cannot be copied because it is used. Only newly captured contexts can be copied.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060035FB RID: 13819 RVA: 0x000C7734 File Offset: 0x000C5934
		[SecuritySafeCritical]
		public ExecutionContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Only newly captured contexts can be copied"));
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.isNewCapture = true;
			executionContext._syncContext = ((this._syncContext == null) ? null : this._syncContext.CreateCopy());
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			return executionContext;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000C77BC File Offset: 0x000C59BC
		[SecuritySafeCritical]
		internal ExecutionContext CreateMutableCopy()
		{
			ExecutionContext executionContext = new ExecutionContext();
			executionContext._syncContext = this._syncContext;
			executionContext._syncContextNoFlow = this._syncContextNoFlow;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			if (this._illogicalCallContext != null)
			{
				executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
			}
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext.isFlowSuppressed = this.isFlowSuppressed;
			return executionContext;
		}

		/// <summary>Suppresses the flow of the execution context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <exception cref="T:System.InvalidOperationException">The context flow is already suppressed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060035FD RID: 13821 RVA: 0x000C7844 File Offset: 0x000C5A44
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			if (ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(Environment.GetResourceString("Context flow is already suppressed."));
			}
			AsyncFlowControl asyncFlowControl = default(AsyncFlowControl);
			asyncFlowControl.Setup();
			return asyncFlowControl;
		}

		/// <summary>Restores the flow of the execution context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context flow cannot be restored because it is not being suppressed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060035FE RID: 13822 RVA: 0x000C7878 File Offset: 0x000C5A78
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (!mutableExecutionContext.isFlowSuppressed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot restore context flow when it is not suppressed."));
			}
			mutableExecutionContext.isFlowSuppressed = false;
		}

		/// <summary>Indicates whether the flow of the execution context is currently suppressed.</summary>
		/// <returns>true if the flow is suppressed; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060035FF RID: 13823 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
		}

		/// <summary>Captures the execution context from the current thread.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the execution context for the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003600 RID: 13824 RVA: 0x000C78C4 File Offset: 0x000C5AC4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ExecutionContext Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.None);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000C78DC File Offset: 0x000C5ADC
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContext FastCapture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000C78F4 File Offset: 0x000C5AF4
		[SecurityCritical]
		internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
		{
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (executionContextReader.IsFlowSuppressed)
			{
				return null;
			}
			SynchronizationContext synchronizationContext = null;
			LogicalCallContext logicalCallContext = null;
			if (!executionContextReader.IsNull)
			{
				if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
				{
					synchronizationContext = ((executionContextReader.SynchronizationContext == null) ? null : executionContextReader.SynchronizationContext.CreateCopy());
				}
				if (executionContextReader.LogicalCallContext.HasInfo)
				{
					logicalCallContext = executionContextReader.LogicalCallContext.Clone();
				}
			}
			Dictionary<IAsyncLocal, object> dictionary = null;
			List<IAsyncLocal> list = null;
			if (!executionContextReader.IsNull)
			{
				dictionary = executionContextReader.DangerousGetRawExecutionContext()._localValues;
				list = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
			}
			if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && synchronizationContext == null && (logicalCallContext == null || !logicalCallContext.HasInfo) && dictionary == null && list == null)
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
			return new ExecutionContext
			{
				_syncContext = synchronizationContext,
				LogicalCallContext = logicalCallContext,
				_localValues = dictionary,
				_localChangeNotifications = list,
				isNewCapture = true
			};
		}

		/// <summary>Sets the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of the current execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003603 RID: 13827 RVA: 0x000C79D7 File Offset: 0x000C5BD7
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._logicalCallContext != null)
			{
				info.AddValue("LogicalCallContext", this._logicalCallContext, typeof(LogicalCallContext));
			}
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000C7A0C File Offset: 0x000C5C0C
		[SecurityCritical]
		private ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("LogicalCallContext"))
				{
					this._logicalCallContext = (LogicalCallContext)enumerator.Value;
				}
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000C7A53 File Offset: 0x000C5C53
		[SecurityCritical]
		internal bool IsDefaultFTContext(bool ignoreSyncCtx)
		{
			return (ignoreSyncCtx || this._syncContext == null) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
		}

		// Token: 0x04001CB3 RID: 7347
		private SynchronizationContext _syncContext;

		// Token: 0x04001CB4 RID: 7348
		private SynchronizationContext _syncContextNoFlow;

		// Token: 0x04001CB5 RID: 7349
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04001CB6 RID: 7350
		private IllogicalCallContext _illogicalCallContext;

		// Token: 0x04001CB7 RID: 7351
		private ExecutionContext.Flags _flags;

		// Token: 0x04001CB8 RID: 7352
		private Dictionary<IAsyncLocal, object> _localValues;

		// Token: 0x04001CB9 RID: 7353
		private List<IAsyncLocal> _localChangeNotifications;

		// Token: 0x04001CBA RID: 7354
		private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);

		// Token: 0x02000475 RID: 1141
		private enum Flags
		{
			// Token: 0x04001CBC RID: 7356
			None,
			// Token: 0x04001CBD RID: 7357
			IsNewCapture,
			// Token: 0x04001CBE RID: 7358
			IsFlowSuppressed,
			// Token: 0x04001CBF RID: 7359
			IsPreAllocatedDefault = 4
		}

		// Token: 0x02000476 RID: 1142
		internal struct Reader
		{
			// Token: 0x06003607 RID: 13831 RVA: 0x000C7A9E File Offset: 0x000C5C9E
			public Reader(ExecutionContext ec)
			{
				this.m_ec = ec;
			}

			// Token: 0x06003608 RID: 13832 RVA: 0x000C7AA7 File Offset: 0x000C5CA7
			public ExecutionContext DangerousGetRawExecutionContext()
			{
				return this.m_ec;
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x06003609 RID: 13833 RVA: 0x000C7AAF File Offset: 0x000C5CAF
			public bool IsNull
			{
				get
				{
					return this.m_ec == null;
				}
			}

			// Token: 0x0600360A RID: 13834 RVA: 0x000C7ABA File Offset: 0x000C5CBA
			[SecurityCritical]
			public bool IsDefaultFTContext(bool ignoreSyncCtx)
			{
				return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x0600360B RID: 13835 RVA: 0x000C7AC8 File Offset: 0x000C5CC8
			public bool IsFlowSuppressed
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsNull && this.m_ec.isFlowSuppressed;
				}
			}

			// Token: 0x0600360C RID: 13836 RVA: 0x000C7ADF File Offset: 0x000C5CDF
			public bool IsSame(ExecutionContext.Reader other)
			{
				return this.m_ec == other.m_ec;
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x0600360D RID: 13837 RVA: 0x000C7AEF File Offset: 0x000C5CEF
			public SynchronizationContext SynchronizationContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContext;
					}
					return null;
				}
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x0600360E RID: 13838 RVA: 0x000C7B06 File Offset: 0x000C5D06
			public SynchronizationContext SynchronizationContextNoFlow
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContextNoFlow;
					}
					return null;
				}
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x0600360F RID: 13839 RVA: 0x000C7B1D File Offset: 0x000C5D1D
			public LogicalCallContext.Reader LogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new LogicalCallContext.Reader(this.IsNull ? null : this.m_ec.LogicalCallContext);
				}
			}

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x06003610 RID: 13840 RVA: 0x000C7B3A File Offset: 0x000C5D3A
			public IllogicalCallContext.Reader IllogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new IllogicalCallContext.Reader(this.IsNull ? null : this.m_ec.IllogicalCallContext);
				}
			}

			// Token: 0x06003611 RID: 13841 RVA: 0x000C7B58 File Offset: 0x000C5D58
			[SecurityCritical]
			public object GetLocalValue(IAsyncLocal local)
			{
				if (this.IsNull)
				{
					return null;
				}
				if (this.m_ec._localValues == null)
				{
					return null;
				}
				object obj;
				this.m_ec._localValues.TryGetValue(local, out obj);
				return obj;
			}

			// Token: 0x06003612 RID: 13842 RVA: 0x000C7B94 File Offset: 0x000C5D94
			[SecurityCritical]
			public bool HasSameLocalValues(ExecutionContext other)
			{
				Dictionary<IAsyncLocal, object> dictionary = (this.IsNull ? null : this.m_ec._localValues);
				Dictionary<IAsyncLocal, object> dictionary2 = ((other == null) ? null : other._localValues);
				return dictionary == dictionary2;
			}

			// Token: 0x06003613 RID: 13843 RVA: 0x000C7BC7 File Offset: 0x000C5DC7
			[SecurityCritical]
			public bool HasLocalValues()
			{
				return !this.IsNull && this.m_ec._localValues != null;
			}

			// Token: 0x04001CC0 RID: 7360
			private ExecutionContext m_ec;
		}

		// Token: 0x02000477 RID: 1143
		[Flags]
		internal enum CaptureOptions
		{
			// Token: 0x04001CC2 RID: 7362
			None = 0,
			// Token: 0x04001CC3 RID: 7363
			IgnoreSyncCtx = 1,
			// Token: 0x04001CC4 RID: 7364
			OptimizeDefaultCase = 2
		}
	}
}
