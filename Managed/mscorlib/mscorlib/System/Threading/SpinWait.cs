using System;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides support for spin-based waiting.</summary>
	// Token: 0x02000460 RID: 1120
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinWait
	{
		/// <summary>Gets the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</summary>
		/// <returns>Returns an integer that represents the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</returns>
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000C6017 File Offset: 0x000C4217
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		/// <summary>Gets whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</summary>
		/// <returns>Whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000C601F File Offset: 0x000C421F
		public bool NextSpinWillYield
		{
			get
			{
				return this.m_count > 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		/// <summary>Performs a single spin.</summary>
		// Token: 0x06003583 RID: 13699 RVA: 0x000C6034 File Offset: 0x000C4234
		public void SpinOnce()
		{
			if (this.NextSpinWillYield)
			{
				int num = ((this.m_count >= 10) ? (this.m_count - 10) : this.m_count);
				if (num % 20 == 19)
				{
					Thread.Sleep(1);
				}
				else if (num % 5 == 4)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
			}
			else
			{
				Thread.SpinWait(4 << this.m_count);
			}
			this.m_count = ((this.m_count == int.MaxValue) ? 10 : (this.m_count + 1));
		}

		/// <summary>Resets the spin counter.</summary>
		// Token: 0x06003584 RID: 13700 RVA: 0x000C60BA File Offset: 0x000C42BA
		public void Reset()
		{
			this.m_count = 0;
		}

		/// <summary>Spins until the specified condition is satisfied.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		// Token: 0x06003585 RID: 13701 RVA: 0x000C60C3 File Offset: 0x000C42C3
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003586 RID: 13702 RVA: 0x000C60D0 File Offset: 0x000C42D0
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return SpinWait.SpinUntil(condition, (int)timeout.TotalMilliseconds);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003587 RID: 13703 RVA: 0x000C6120 File Offset: 0x000C4320
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", Environment.GetResourceString("The condition argument is null."));
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001C86 RID: 7302
		internal const int YIELD_THRESHOLD = 10;

		// Token: 0x04001C87 RID: 7303
		internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;

		// Token: 0x04001C88 RID: 7304
		internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;

		// Token: 0x04001C89 RID: 7305
		private int m_count;
	}
}
