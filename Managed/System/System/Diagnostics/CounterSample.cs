using System;

namespace System.Diagnostics
{
	/// <summary>Defines a structure that holds the raw data for a performance counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E5 RID: 485
	public struct CounterSample
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to 0 (zero).</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		// Token: 0x06000F4F RID: 3919 RVA: 0x00046BB0 File Offset: 0x00044DB0
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this = new CounterSample(rawValue, baseValue, counterFrequency, systemFrequency, timeStamp, timeStamp100nSec, counterType, 0L);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to the value that is passed in.</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		/// <param name="counterTimeStamp">The time at which the sample was taken. </param>
		// Token: 0x06000F50 RID: 3920 RVA: 0x00046BD0 File Offset: 0x00044DD0
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.counterFrequency = counterFrequency;
			this.systemFrequency = systemFrequency;
			this.timeStamp = timeStamp;
			this.timeStamp100nSec = timeStamp100nSec;
			this.counterType = counterType;
			this.counterTimeStamp = counterTimeStamp;
		}

		/// <summary>Gets an optional, base raw value for the counter.</summary>
		/// <returns>The base raw value, which is used only if the sample is based on multiple counters.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00046C0F File Offset: 0x00044E0F
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		/// <summary>Gets the raw counter frequency.</summary>
		/// <returns>The frequency with which the counter is read.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00046C17 File Offset: 0x00044E17
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		/// <summary>Gets the counter's time stamp.</summary>
		/// <returns>The time at which the sample was taken.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00046C1F File Offset: 0x00044E1F
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		/// <summary>Gets the performance counter type.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00046C27 File Offset: 0x00044E27
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		/// <summary>Gets the raw value of the counter.</summary>
		/// <returns>The numeric value that is associated with the performance counter sample.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00046C2F File Offset: 0x00044E2F
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		/// <summary>Gets the raw system frequency.</summary>
		/// <returns>The frequency with which the system reads from the counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00046C37 File Offset: 0x00044E37
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		/// <summary>Gets the raw time stamp.</summary>
		/// <returns>The system time stamp.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00046C3F File Offset: 0x00044E3F
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		/// <summary>Gets the raw, high-fidelity time stamp.</summary>
		/// <returns>The system time stamp, represented within 0.1 millisecond.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00046C47 File Offset: 0x00044E47
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		/// <summary>Calculates the performance data of the counter, using a single sample point. This method is generally used for uncalculated performance counter types.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F59 RID: 3929 RVA: 0x00046C4F File Offset: 0x00044E4F
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		/// <summary>Calculates the performance data of the counter, using two sample points. This method is generally used for calculated performance counter types, such as averages.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <param name="nextCounterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as an ending point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F5A RID: 3930 RVA: 0x00046C57 File Offset: 0x00044E57
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		/// <summary>Indicates whether the specified structure is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current instance; otherwise, false. </returns>
		/// <param name="o">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with the current structure.</param>
		// Token: 0x06000F5B RID: 3931 RVA: 0x00046C60 File Offset: 0x00044E60
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Diagnostics.CounterSample" /> structure is equal to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="sample" /> is equal to the current instance; otherwise, false. </returns>
		/// <param name="sample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with this instance.</param>
		// Token: 0x06000F5C RID: 3932 RVA: 0x00046C78 File Offset: 0x00044E78
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.counterFrequency && this.counterFrequency == sample.counterFrequency && this.systemFrequency == sample.systemFrequency && this.timeStamp == sample.timeStamp && this.timeStamp100nSec == sample.timeStamp100nSec && this.counterTimeStamp == sample.counterTimeStamp && this.counterType == sample.counterType;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x06000F5D RID: 3933 RVA: 0x00046CF7 File Offset: 0x00044EF7
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x06000F5E RID: 3934 RVA: 0x00046D01 File Offset: 0x00044F01
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		/// <summary>Gets a hash code for the current counter sample.</summary>
		/// <returns>A hash code for the current counter sample.</returns>
		// Token: 0x06000F5F RID: 3935 RVA: 0x00046D10 File Offset: 0x00044F10
		public override int GetHashCode()
		{
			return (int)((this.rawValue << 28) ^ ((this.baseValue << 24) ^ ((this.counterFrequency << 20) ^ ((this.systemFrequency << 16) ^ ((this.timeStamp << 8) ^ ((this.timeStamp100nSec << 4) ^ (this.counterTimeStamp ^ (long)this.counterType)))))));
		}

		// Token: 0x04001109 RID: 4361
		private long rawValue;

		// Token: 0x0400110A RID: 4362
		private long baseValue;

		// Token: 0x0400110B RID: 4363
		private long counterFrequency;

		// Token: 0x0400110C RID: 4364
		private long systemFrequency;

		// Token: 0x0400110D RID: 4365
		private long timeStamp;

		// Token: 0x0400110E RID: 4366
		private long timeStamp100nSec;

		// Token: 0x0400110F RID: 4367
		private long counterTimeStamp;

		// Token: 0x04001110 RID: 4368
		private PerformanceCounterType counterType;

		/// <summary>Defines an empty, uninitialized performance counter sample of type NumberOfItems32.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001111 RID: 4369
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32, 0L);
	}
}
