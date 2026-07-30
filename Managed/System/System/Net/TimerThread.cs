using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200049D RID: 1181
	internal static class TimerThread
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x00087108 File Offset: 0x00085308
		static TimerThread()
		{
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00087180 File Offset: 0x00085380
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (linkedList)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference weakReference = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(weakReference);
			}
			return timerQueue;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000871F0 File Offset: 0x000853F0
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
				lock (linkedList)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00087394 File Offset: 0x00085594
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000873C8 File Offset: 0x000855C8
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> linkedList = TimerThread.s_Queues;
			lock (linkedList)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> linkedList2 = TimerThread.s_NewQueues;
									lock (linkedList2)
									{
										for (LinkedListNode<WeakReference> linkedListNode = TimerThread.s_NewQueues.First; linkedListNode != null; linkedListNode = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(linkedListNode);
											TimerThread.s_Queues.AddLast(linkedListNode);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode2 = TimerThread.s_Queues.First;
								while (linkedListNode2 != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode2.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode2.Next;
										TimerThread.s_Queues.Remove(linkedListNode2);
										linkedListNode2 = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode2 = linkedListNode2.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int num3 = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num4 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, num3, false);
								if (num4 == 0)
								{
									break;
								}
								if (num4 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_01A8;
									}
								}
							}
							flag2 = false;
							continue;
							IL_01A8:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							bool on = Logging.On;
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000875FC File Offset: 0x000857FC
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00087615 File Offset: 0x00085815
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00087634 File Offset: 0x00085834
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x04001F33 RID: 7987
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x04001F34 RID: 7988
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x04001F35 RID: 7989
		private const int c_TickCountResolution = 15;

		// Token: 0x04001F36 RID: 7990
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x04001F37 RID: 7991
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x04001F38 RID: 7992
		private static int s_ThreadState = 0;

		// Token: 0x04001F39 RID: 7993
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x04001F3A RID: 7994
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x04001F3B RID: 7995
		private static WaitHandle[] s_ThreadEvents = new WaitHandle[]
		{
			TimerThread.s_ThreadShutdownEvent,
			TimerThread.s_ThreadReadyEvent
		};

		// Token: 0x04001F3C RID: 7996
		private static int s_CacheScanIteration;

		// Token: 0x04001F3D RID: 7997
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x0200049E RID: 1182
		internal abstract class Queue
		{
			// Token: 0x060022EC RID: 8940 RVA: 0x0008765C File Offset: 0x0008585C
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x060022ED RID: 8941 RVA: 0x0008766B File Offset: 0x0008586B
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x060022EE RID: 8942 RVA: 0x00087673 File Offset: 0x00085873
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x060022EF RID: 8943
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04001F3E RID: 7998
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200049F RID: 1183
		internal abstract class Timer : IDisposable
		{
			// Token: 0x060022F0 RID: 8944 RVA: 0x0008767D File Offset: 0x0008587D
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x060022F1 RID: 8945 RVA: 0x00087697 File Offset: 0x00085897
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0008769F File Offset: 0x0008589F
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000876A7 File Offset: 0x000858A7
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000876B8 File Offset: 0x000858B8
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x060022F5 RID: 8949
			internal abstract bool Cancel();

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x060022F6 RID: 8950
			internal abstract bool HasExpired { get; }

			// Token: 0x060022F7 RID: 8951 RVA: 0x00087713 File Offset: 0x00085913
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04001F3F RID: 7999
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04001F40 RID: 8000
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x020004A0 RID: 1184
		// (Invoke) Token: 0x060022F9 RID: 8953
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x020004A1 RID: 1185
		private enum TimerThreadState
		{
			// Token: 0x04001F42 RID: 8002
			Idle,
			// Token: 0x04001F43 RID: 8003
			Running,
			// Token: 0x04001F44 RID: 8004
			Stopped
		}

		// Token: 0x020004A2 RID: 1186
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x060022FC RID: 8956 RVA: 0x0008771C File Offset: 0x0008591C
			internal TimerQueue(int durationMilliseconds)
				: base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x060022FD RID: 8957 RVA: 0x00087754 File Offset: 0x00085954
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x060022FE RID: 8958 RVA: 0x00087820 File Offset: 0x00085A20
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode timerNode;
				do
				{
					timerNode = this.m_Timers.Next;
					if (timerNode == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							timerNode = this.m_Timers.Next;
							if (timerNode == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (timerNode.Fire());
				nextExpiration = timerNode.Expiration;
				return true;
			}

			// Token: 0x04001F45 RID: 8005
			private IntPtr m_ThisHandle;

			// Token: 0x04001F46 RID: 8006
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x020004A3 RID: 1187
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x060022FF RID: 8959 RVA: 0x000878D4 File Offset: 0x00085AD4
			internal InfiniteTimerQueue()
				: base(-1)
			{
			}

			// Token: 0x06002300 RID: 8960 RVA: 0x000878DD File Offset: 0x00085ADD
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x020004A4 RID: 1188
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x06002301 RID: 8961 RVA: 0x000878E4 File Offset: 0x00085AE4
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock)
				: base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x06002302 RID: 8962 RVA: 0x0008790D File Offset: 0x00085B0D
			internal TimerNode()
				: base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x06002303 RID: 8963 RVA: 0x0008791D File Offset: 0x00085B1D
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06002304 RID: 8964 RVA: 0x00087928 File Offset: 0x00085B28
			// (set) Token: 0x06002305 RID: 8965 RVA: 0x00087930 File Offset: 0x00085B30
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06002306 RID: 8966 RVA: 0x00087939 File Offset: 0x00085B39
			// (set) Token: 0x06002307 RID: 8967 RVA: 0x00087941 File Offset: 0x00085B41
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x06002308 RID: 8968 RVA: 0x0008794C File Offset: 0x00085B4C
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06002309 RID: 8969 RVA: 0x000879E4 File Offset: 0x00085BE4
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = this.m_Callback != null;
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						bool on = Logging.On;
					}
				}
				return true;
			}

			// Token: 0x04001F47 RID: 8007
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04001F48 RID: 8008
			private TimerThread.Callback m_Callback;

			// Token: 0x04001F49 RID: 8009
			private object m_Context;

			// Token: 0x04001F4A RID: 8010
			private object m_QueueLock;

			// Token: 0x04001F4B RID: 8011
			private TimerThread.TimerNode next;

			// Token: 0x04001F4C RID: 8012
			private TimerThread.TimerNode prev;

			// Token: 0x020004A5 RID: 1189
			private enum TimerState
			{
				// Token: 0x04001F4E RID: 8014
				Ready,
				// Token: 0x04001F4F RID: 8015
				Fired,
				// Token: 0x04001F50 RID: 8016
				Cancelled,
				// Token: 0x04001F51 RID: 8017
				Sentinel
			}
		}

		// Token: 0x020004A6 RID: 1190
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x0600230A RID: 8970 RVA: 0x00087AD8 File Offset: 0x00085CD8
			internal InfiniteTimer()
				: base(-1)
			{
			}

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x0600230B RID: 8971 RVA: 0x00004240 File Offset: 0x00002440
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600230C RID: 8972 RVA: 0x00087AE1 File Offset: 0x00085CE1
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04001F52 RID: 8018
			private int cancelled;
		}
	}
}
