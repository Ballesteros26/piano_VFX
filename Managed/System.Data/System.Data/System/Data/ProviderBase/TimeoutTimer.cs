using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000319 RID: 793
	internal class TimeoutTimer
	{
		// Token: 0x0600232E RID: 9006 RVA: 0x000A3B3A File Offset: 0x000A1D3A
		internal static TimeoutTimer StartSecondsTimeout(int seconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer.SetTimeoutSeconds(seconds);
			return timeoutTimer;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000A3B48 File Offset: 0x000A1D48
		internal static TimeoutTimer StartMillisecondsTimeout(long milliseconds)
		{
			return new TimeoutTimer
			{
				_timerExpire = checked(ADP.TimerCurrent() + milliseconds * 10000L),
				_isInfiniteTimeout = false
			};
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000A3B6A File Offset: 0x000A1D6A
		internal void SetTimeoutSeconds(int seconds)
		{
			if (TimeoutTimer.InfiniteTimeout == (long)seconds)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._timerExpire = checked(ADP.TimerCurrent() + ADP.TimerFromSeconds(seconds));
			this._isInfiniteTimeout = false;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000A3B96 File Offset: 0x000A1D96
		internal bool IsExpired
		{
			get
			{
				return !this.IsInfinite && ADP.TimerHasExpired(this._timerExpire);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x000A3BAD File Offset: 0x000A1DAD
		internal bool IsInfinite
		{
			get
			{
				return this._isInfiniteTimeout;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x000A3BB5 File Offset: 0x000A1DB5
		internal long LegacyTimerExpire
		{
			get
			{
				if (!this._isInfiniteTimeout)
				{
					return this._timerExpire;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x000A3BD0 File Offset: 0x000A1DD0
		internal long MillisecondsRemaining
		{
			get
			{
				long num;
				if (this._isInfiniteTimeout)
				{
					num = long.MaxValue;
				}
				else
				{
					num = ADP.TimerRemainingMilliseconds(this._timerExpire);
					if (0L > num)
					{
						num = 0L;
					}
				}
				return num;
			}
		}

		// Token: 0x04001754 RID: 5972
		private long _timerExpire;

		// Token: 0x04001755 RID: 5973
		private bool _isInfiniteTimeout;

		// Token: 0x04001756 RID: 5974
		internal static readonly long InfiniteTimeout;
	}
}
