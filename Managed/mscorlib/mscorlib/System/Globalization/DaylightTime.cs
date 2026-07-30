using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the period of daylight saving time.</summary>
	// Token: 0x0200040B RID: 1035
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x06003123 RID: 12579 RVA: 0x00002111 File Offset: 0x00000311
		private DaylightTime()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.DaylightTime" /> class with the specified start, end, and time difference information.</summary>
		/// <param name="start">The object that represents the date and time when daylight saving time begins. The value must be in local time. </param>
		/// <param name="end">The object that represents the date and time when daylight saving time ends. The value must be in local time. </param>
		/// <param name="delta">The object that represents the difference between standard time and daylight saving time, in ticks. </param>
		// Token: 0x06003124 RID: 12580 RVA: 0x000B0BCC File Offset: 0x000AEDCC
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period begins.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period begins. The value is in local time.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000B0BE9 File Offset: 0x000AEDE9
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period ends.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period ends. The value is in local time.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000B0BF1 File Offset: 0x000AEDF1
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		/// <summary>Gets the time interval that represents the difference between standard time and daylight saving time.</summary>
		/// <returns>The time interval that represents the difference between standard time and daylight saving time.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000B0BF9 File Offset: 0x000AEDF9
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x040019EE RID: 6638
		internal DateTime m_start;

		// Token: 0x040019EF RID: 6639
		internal DateTime m_end;

		// Token: 0x040019F0 RID: 6640
		internal TimeSpan m_delta;
	}
}
