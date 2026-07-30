using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides a mechanism for executing a method at specified intervals. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020004AE RID: 1198
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the Timer class, using a 32-bit signed integer to specify the time interval.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x06003824 RID: 14372 RVA: 0x000CBDB7 File Offset: 0x000C9FB7
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		/// <summary>Initializes a new instance of the Timer class, using 64-bit signed integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		// Token: 0x06003825 RID: 14373 RVA: 0x000CBDCC File Offset: 0x000C9FCC
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			this.Init(callback, state, dueTime, period);
		}

		/// <summary>Initializes a new instance of the Timer class, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The <see cref="T:System.TimeSpan" /> representing the amount of time to delay before the <paramref name="callback" /> parameter invokes its methods. Specify negative one (-1) milliseconds to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the methods referenced by <paramref name="callback" />. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime" /> or <paramref name="period" /> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite" />, or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x06003826 RID: 14374 RVA: 0x000CBDDF File Offset: 0x000C9FDF
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		/// <summary>Initializes a new instance of the Timer class, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x06003827 RID: 14375 RVA: 0x000CBE00 File Offset: 0x000CA000
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			this.Init(callback, state, num, num2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Timer" /> class with an infinite period and an infinite due time, using the newly created <see cref="T:System.Threading.Timer" /> object as the state object. </summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		// Token: 0x06003828 RID: 14376 RVA: 0x000CBE35 File Offset: 0x000CA035
		public Timer(TimerCallback callback)
		{
			this.Init(callback, this, -1L, -1L);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000CBE49 File Offset: 0x000CA049
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.Change(dueTime, period, true);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600382A RID: 14378 RVA: 0x000CBE72 File Offset: 0x000CA072
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">A <see cref="T:System.TimeSpan" /> representing the amount of time to delay before invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600382B RID: 14379 RVA: 0x000CBE7F File Offset: 0x000CA07F
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600382C RID: 14380 RVA: 0x000CBE98 File Offset: 0x000CA098
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			return this.Change(num, num2, false);
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600382D RID: 14381 RVA: 0x000CBEC4 File Offset: 0x000CA0C4
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 64-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600382E RID: 14382 RVA: 0x000CBEE1 File Offset: 0x000CA0E1
		public bool Change(long dueTime, long period)
		{
			return this.Change(dueTime, period, false);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000CBEEC File Offset: 0x000CA0EC
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTime", "Due time too large");
			}
			if (period > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("period", "Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long num;
			if (dueTime == 0L)
			{
				num = 0L;
			}
			else if (dueTime < 0L)
			{
				num = long.MaxValue;
				if (first)
				{
					this.next_run = num;
					return true;
				}
			}
			else
			{
				num = dueTime * 10000L + Timer.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, num);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" /> and signals when the timer has been disposed of.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="notifyObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled when the Timer has been disposed of. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003830 RID: 14384 RVA: 0x000CBFAB File Offset: 0x000CA1AB
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			this.Dispose();
			NativeEventCalls.SetEvent(notifyObject.SafeWaitHandle);
			return true;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00002194 File Offset: 0x00000394
		internal void KeepRootedWhileScheduled()
		{
		}

		// Token: 0x06003832 RID: 14386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetTimeMonotonic();

		// Token: 0x04001D83 RID: 7555
		private static readonly Timer.Scheduler scheduler = Timer.Scheduler.Instance;

		// Token: 0x04001D84 RID: 7556
		private TimerCallback callback;

		// Token: 0x04001D85 RID: 7557
		private object state;

		// Token: 0x04001D86 RID: 7558
		private long due_time_ms;

		// Token: 0x04001D87 RID: 7559
		private long period_ms;

		// Token: 0x04001D88 RID: 7560
		private long next_run;

		// Token: 0x04001D89 RID: 7561
		private bool disposed;

		// Token: 0x04001D8A RID: 7562
		private const long MaxValue = 4294967294L;

		// Token: 0x020004AF RID: 1199
		private sealed class TimerComparer : IComparer
		{
			// Token: 0x06003834 RID: 14388 RVA: 0x000CBFDC File Offset: 0x000CA1DC
			public int Compare(object x, object y)
			{
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				long num = timer.next_run - timer2.next_run;
				if (num == 0L)
				{
					if (x != y)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (num <= 0L)
					{
						return -1;
					}
					return 1;
				}
			}
		}

		// Token: 0x020004B0 RID: 1200
		private sealed class Scheduler
		{
			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x06003837 RID: 14391 RVA: 0x000CC02E File Offset: 0x000CA22E
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x06003838 RID: 14392 RVA: 0x000CC038 File Offset: 0x000CA238
			private Scheduler()
			{
				this.changed = new ManualResetEvent(false);
				this.list = new SortedList(new Timer.TimerComparer(), 1024);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x06003839 RID: 14393 RVA: 0x000CC08C File Offset: 0x000CA28C
			public void Remove(Timer timer)
			{
				if (timer.next_run == 0L || timer.next_run == 9223372036854775807L)
				{
					return;
				}
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x0600383A RID: 14394 RVA: 0x000CC0E4 File Offset: 0x000CA2E4
			public void Change(Timer timer, long new_next_run)
			{
				bool flag = false;
				lock (this)
				{
					this.InternalRemove(timer);
					if (new_next_run == 9223372036854775807L)
					{
						timer.next_run = new_next_run;
						return;
					}
					if (!timer.disposed)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						flag = this.list.GetByIndex(0) == timer;
					}
				}
				if (flag)
				{
					this.changed.Set();
				}
			}

			// Token: 0x0600383B RID: 14395 RVA: 0x000CC170 File Offset: 0x000CA370
			private int FindByDueTime(long nr)
			{
				int i = 0;
				int num = this.list.Count - 1;
				if (num < 0)
				{
					return -1;
				}
				if (num < 20)
				{
					while (i <= num)
					{
						Timer timer = (Timer)this.list.GetByIndex(i);
						if (timer.next_run == nr)
						{
							return i;
						}
						if (timer.next_run > nr)
						{
							return -1;
						}
						i++;
					}
					return -1;
				}
				while (i <= num)
				{
					int num2 = i + (num - i >> 1);
					Timer timer2 = (Timer)this.list.GetByIndex(num2);
					if (nr == timer2.next_run)
					{
						return num2;
					}
					if (nr > timer2.next_run)
					{
						i = num2 + 1;
					}
					else
					{
						num = num2 - 1;
					}
				}
				return -1;
			}

			// Token: 0x0600383C RID: 14396 RVA: 0x000CC20C File Offset: 0x000CA40C
			private void Add(Timer timer)
			{
				int num = this.FindByDueTime(timer.next_run);
				if (num != -1)
				{
					bool flag = long.MaxValue - timer.next_run > 20000L;
					do
					{
						num++;
						if (flag)
						{
							timer.next_run += 1L;
						}
						else
						{
							timer.next_run -= 1L;
						}
					}
					while (num < this.list.Count && ((Timer)this.list.GetByIndex(num)).next_run == timer.next_run);
				}
				this.list.Add(timer, timer);
			}

			// Token: 0x0600383D RID: 14397 RVA: 0x000CC2A8 File Offset: 0x000CA4A8
			private int InternalRemove(Timer timer)
			{
				int num = this.list.IndexOfKey(timer);
				if (num >= 0)
				{
					this.list.RemoveAt(num);
				}
				return num;
			}

			// Token: 0x0600383E RID: 14398 RVA: 0x000CC2D4 File Offset: 0x000CA4D4
			private static void TimerCB(object o)
			{
				Timer timer = (Timer)o;
				timer.callback(timer.state);
			}

			// Token: 0x0600383F RID: 14399 RVA: 0x000CC2FC File Offset: 0x000CA4FC
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				List<Timer> list = new List<Timer>(512);
				for (;;)
				{
					int num = -1;
					long timeMonotonic = Timer.GetTimeMonotonic();
					lock (this)
					{
						this.changed.Reset();
						int num2 = this.list.Count;
						for (int i = 0; i < num2; i++)
						{
							Timer timer = (Timer)this.list.GetByIndex(i);
							if (timer.next_run > timeMonotonic)
							{
								break;
							}
							this.list.RemoveAt(i);
							num2--;
							i--;
							ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(Timer.Scheduler.TimerCB), timer);
							long period_ms = timer.period_ms;
							long due_time_ms = timer.due_time_ms;
							if (period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L))
							{
								timer.next_run = long.MaxValue;
							}
							else
							{
								timer.next_run = Timer.GetTimeMonotonic() + 10000L * timer.period_ms;
								list.Add(timer);
							}
						}
						num2 = list.Count;
						for (int i = 0; i < num2; i++)
						{
							Timer timer2 = list[i];
							this.Add(timer2);
						}
						list.Clear();
						this.ShrinkIfNeeded(list, 512);
						int capacity = this.list.Capacity;
						num2 = this.list.Count;
						if (capacity > 1024 && num2 > 0 && capacity / num2 > 3)
						{
							this.list.Capacity = num2 * 2;
						}
						long num3 = long.MaxValue;
						if (this.list.Count > 0)
						{
							num3 = ((Timer)this.list.GetByIndex(0)).next_run;
						}
						num = -1;
						if (num3 != 9223372036854775807L)
						{
							long num4 = (num3 - Timer.GetTimeMonotonic()) / 10000L;
							if (num4 > 2147483647L)
							{
								num = 2147483646;
							}
							else
							{
								num = (int)num4;
								if (num < 0)
								{
									num = 0;
								}
							}
						}
					}
					this.changed.WaitOne(num);
				}
			}

			// Token: 0x06003840 RID: 14400 RVA: 0x000CC544 File Offset: 0x000CA744
			private void ShrinkIfNeeded(List<Timer> list, int initial)
			{
				int capacity = list.Capacity;
				int count = list.Count;
				if (capacity > initial && count > 0 && capacity / count > 3)
				{
					list.Capacity = count * 2;
				}
			}

			// Token: 0x04001D8B RID: 7563
			private static Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04001D8C RID: 7564
			private SortedList list;

			// Token: 0x04001D8D RID: 7565
			private ManualResetEvent changed;
		}
	}
}
