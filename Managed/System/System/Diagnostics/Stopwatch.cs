using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000217 RID: 535
	public class Stopwatch
	{
		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001173 RID: 4467
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTimestamp();

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001174 RID: 4468 RVA: 0x0004B5B6 File Offset: 0x000497B6
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004B5C3 File Offset: 0x000497C3
		public TimeSpan Elapsed
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return TimeSpan.FromTicks(this.ElapsedTicks / (Stopwatch.Frequency / 10000000L));
				}
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004B5F0 File Offset: 0x000497F0
		public long ElapsedMilliseconds
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return this.ElapsedTicks / (Stopwatch.Frequency / 1000L);
				}
				return checked((long)this.Elapsed.TotalMilliseconds);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004B627 File Offset: 0x00049827
		public long ElapsedTicks
		{
			get
			{
				if (!this.is_running)
				{
					return this.elapsed;
				}
				return Stopwatch.GetTimestamp() - this.started + this.elapsed;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004B64B File Offset: 0x0004984B
		public bool IsRunning
		{
			get
			{
				return this.is_running;
			}
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600117A RID: 4474 RVA: 0x0004B653 File Offset: 0x00049853
		public void Reset()
		{
			this.elapsed = 0L;
			this.is_running = false;
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600117B RID: 4475 RVA: 0x0004B664 File Offset: 0x00049864
		public void Start()
		{
			if (this.is_running)
			{
				return;
			}
			this.started = Stopwatch.GetTimestamp();
			this.is_running = true;
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600117C RID: 4476 RVA: 0x0004B681 File Offset: 0x00049881
		public void Stop()
		{
			if (!this.is_running)
			{
				return;
			}
			this.elapsed += Stopwatch.GetTimestamp() - this.started;
			if (this.elapsed < 0L)
			{
				this.elapsed = 0L;
			}
			this.is_running = false;
		}

		/// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
		// Token: 0x0600117D RID: 4477 RVA: 0x0004B6BE File Offset: 0x000498BE
		public void Restart()
		{
			this.started = Stopwatch.GetTimestamp();
			this.elapsed = 0L;
			this.is_running = true;
		}

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040011DC RID: 4572
		public static readonly long Frequency = 10000000L;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040011DD RID: 4573
		public static readonly bool IsHighResolution = true;

		// Token: 0x040011DE RID: 4574
		private long elapsed;

		// Token: 0x040011DF RID: 4575
		private long started;

		// Token: 0x040011E0 RID: 4576
		private bool is_running;
	}
}
