using System;
using Unity;

namespace System.Timers
{
	/// <summary>Provides data for the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	// Token: 0x02000132 RID: 306
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x00028728 File Offset: 0x00026928
		internal ElapsedEventArgs(DateTime time)
		{
			this.time = time;
		}

		/// <summary>Gets the time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</summary>
		/// <returns>The time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00028737 File Offset: 0x00026937
		public DateTime SignalTime
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal ElapsedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000DA9 RID: 3497
		private DateTime time;
	}
}
