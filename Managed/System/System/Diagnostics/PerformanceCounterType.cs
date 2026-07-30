using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the formula used to calculate the <see cref="M:System.Diagnostics.PerformanceCounter.NextValue" /> method for a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000212 RID: 530
	[TypeConverter(typeof(AlphabeticalEnumConverter))]
	public enum PerformanceCounterType
	{
		/// <summary>An instantaneous counter that shows the most recently observed value in hexadecimal format. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x040011AE RID: 4526
		NumberOfItemsHEX32,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as NumberOfItemsHEX32 except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x040011AF RID: 4527
		NumberOfItemsHEX64 = 256,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x040011B0 RID: 4528
		NumberOfItems32 = 65536,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as NumberOfItems32 except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x040011B1 RID: 4529
		NumberOfItems64 = 65792,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals.</summary>
		// Token: 0x040011B2 RID: 4530
		CounterDelta32 = 4195328,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals. It is the same as the CounterDelta32 counter type except that is uses larger fields to accomodate larger values.</summary>
		// Token: 0x040011B3 RID: 4531
		CounterDelta64 = 4195584,
		/// <summary>An average counter that shows the average number of operations completed in one second. When a counter of this type samples the data, each sampling interrupt returns one or zero. The counter data is the number of ones that were sampled. It measures time in units of ticks of the system performance timer.</summary>
		// Token: 0x040011B4 RID: 4532
		SampleCounter = 4260864,
		/// <summary>An average counter designed to monitor the average length of a queue to a resource over time. It shows the difference between the queue lengths observed during the last two sample intervals divided by the duration of the interval. This type of counter is typically used to track the number of items that are queued or waiting.</summary>
		// Token: 0x040011B5 RID: 4533
		CountPerTimeInterval32 = 4523008,
		/// <summary>An average counter that monitors the average length of a queue to a resource over time. Counters of this type display the difference between the queue lengths observed during the last two sample intervals, divided by the duration of the interval. This counter type is the same as CountPerTimeInterval32 except that it uses larger fields to accommodate larger values. This type of counter is typically used to track a high-volume or very large number of items that are queued or waiting.</summary>
		// Token: 0x040011B6 RID: 4534
		CountPerTimeInterval64 = 4523264,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock.</summary>
		// Token: 0x040011B7 RID: 4535
		RateOfCountsPerSecond32 = 272696320,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. This counter type is the same as the RateOfCountsPerSecond32 type, but it uses larger fields to accommodate larger values to track a high-volume number of items or operations per second, such as a byte-transmission rate.</summary>
		// Token: 0x040011B8 RID: 4536
		RateOfCountsPerSecond64 = 272696576,
		/// <summary>An instantaneous percentage counter that shows the ratio of a subset to its set as a percentage. For example, it compares the number of bytes in use on a disk to the total number of bytes on the disk. Counters of this type display the current percentage only, not an average over time.</summary>
		// Token: 0x040011B9 RID: 4537
		RawFraction = 537003008,
		/// <summary>A percentage counter that shows the average time that a component is active as a percentage of the total sample time.</summary>
		// Token: 0x040011BA RID: 4538
		CounterTimer = 541132032,
		/// <summary>A percentage counter that shows the active time of a component as a percentage of the total elapsed time of the sample interval. It measures time in units of 100 nanoseconds (ns). Counters of this type are designed to measure the activity of one component at a time.</summary>
		// Token: 0x040011BB RID: 4539
		Timer100Ns = 542180608,
		/// <summary>A percentage counter that shows the average ratio of hits to all operations during the last two sample intervals.</summary>
		// Token: 0x040011BC RID: 4540
		SampleFraction = 549585920,
		/// <summary>A percentage counter that displays the average percentage of active time observed during sample interval. The value of these counters is calculated by monitoring the percentage of time that the service was inactive and then subtracting that value from 100 percent.</summary>
		// Token: 0x040011BD RID: 4541
		CounterTimerInverse = 557909248,
		/// <summary>A percentage counter that shows the average percentage of active time observed during the sample interval.</summary>
		// Token: 0x040011BE RID: 4542
		Timer100NsInverse = 558957824,
		/// <summary>A percentage counter that displays the active time of one or more components as a percentage of the total time of the sample interval. Because the numerator records the active time of components operating simultaneously, the resulting percentage can exceed 100 percent.</summary>
		// Token: 0x040011BF RID: 4543
		CounterMultiTimer = 574686464,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It measures time in 100 nanosecond (ns) units.</summary>
		// Token: 0x040011C0 RID: 4544
		CounterMultiTimer100Ns = 575735040,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It derives the active time by measuring the time that the components were not active and subtracting the result from 100 percent by the number of objects monitored.</summary>
		// Token: 0x040011C1 RID: 4545
		CounterMultiTimerInverse = 591463680,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. Counters of this type measure time in 100 nanosecond (ns) units. They derive the active time by measuring the time that the components were not active and subtracting the result from multiplying 100 percent by the number of objects monitored.</summary>
		// Token: 0x040011C2 RID: 4546
		CounterMultiTimer100NsInverse = 592512256,
		/// <summary>An average counter that measures the time it takes, on average, to complete a process or operation. Counters of this type display a ratio of the total elapsed time of the sample interval to the number of processes or operations completed during that time. This counter type measures time in ticks of the system clock.</summary>
		// Token: 0x040011C3 RID: 4547
		AverageTimer32 = 805438464,
		/// <summary>A difference timer that shows the total time between when the component or process started and the time when this value is calculated.</summary>
		// Token: 0x040011C4 RID: 4548
		ElapsedTime = 807666944,
		/// <summary>An average counter that shows how many items are processed, on average, during an operation. Counters of this type display a ratio of the items processed to the number of operations completed. The ratio is calculated by comparing the number of items processed during the last interval to the number of operations completed during the last interval.</summary>
		// Token: 0x040011C5 RID: 4549
		AverageCount64 = 1073874176,
		/// <summary>A base counter that stores the number of sampling interrupts taken and is used as a denominator in the sampling fraction. The sampling fraction is the number of samples that were 1 (or true) for a sample interrupt. Check that this value is greater than zero before using it as the denominator in a calculation of SampleFraction.</summary>
		// Token: 0x040011C6 RID: 4550
		SampleBase = 1073939457,
		/// <summary>A base counter that is used in the calculation of time or count averages, such as AverageTimer32 and AverageCount64. Stores the denominator for calculating a counter to present "time per operation" or "count per operation".</summary>
		// Token: 0x040011C7 RID: 4551
		AverageBase,
		/// <summary>A base counter that stores the denominator of a counter that presents a general arithmetic fraction. Check that this value is greater than zero before using it as the denominator in a RawFraction value calculation.</summary>
		// Token: 0x040011C8 RID: 4552
		RawBase,
		/// <summary>A base counter that indicates the number of items sampled. It is used as the denominator in the calculations to get an average among the items sampled when taking timings of multiple, but similar items. Used with CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, and CounterMultiTimer100NsInverse.</summary>
		// Token: 0x040011C9 RID: 4553
		CounterMultiBase = 1107494144
	}
}
