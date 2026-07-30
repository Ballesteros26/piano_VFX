using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class represents the weekly schedule for an Active Directory Domain Services replication.</summary>
	// Token: 0x02000039 RID: 57
	public class ActiveDirectorySchedule
	{
		/// <summary>Gets or sets a three-dimensional array that indicates at what time during the week that the source is available for replication.</summary>
		/// <returns>A three-dimensional array of <see cref="T:System.Boolean" /> elements in which the element is true if the source is available for replication during that specific 15-minute interval. The element is false if the source is not available for replication.The array is in the form RawSchedule[&lt;day-of-week&gt;, &lt;hour&gt;, &lt;15-minute interval&gt;]. All of these values are zero-based and the week begins at 00:00 on Sunday morning, Coordinated Universal Time.The following are the valid values for the day-of-week.Day-of-week valueIndicated day of the week.0Sunday1Monday2Tuesday3Wednesday4Thursday5Friday6SaturdayThe hour is zero-based and specified in 24-hour format. For example, 2 P.M. would be specified as 14. Valid values are 0-23.The 15-minute interval specifies the 15-minute block within the hour that the source is available for replication. The following table identifies the possible values for the 15-minute interval.15-minute intervalDescription0The source is available for replication from 0 to 14 minutes after the hour.1The source is available for replication from 15 to 29 minutes after the hour.2The source is available for replication from 30 to 44 minutes after the hour.3The source is available for replication from 45 to 59 minutes after the hour.The following C# example shows how to use this property to determine if the source is available for replication at 15:50 Coordinated Universal Time on Tuesday.C# Copy CodeBoolean isAvailable = scheduleObject.RawSchedule[2, 15, 3];The following C# example shows how to use this property to calculate the 15-minute interval at runtime by dividing the minutes by 15.C# Copy CodeBoolean isAvailable = scheduleObject.RawSchedule[2, 15, (Int32)50/15];</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000208C File Offset: 0x0000028C
		public bool[,,] RawSchedule
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class.</summary>
		// Token: 0x060001C9 RID: 457 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public ActiveDirectorySchedule()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object. </summary>
		/// <param name="schedule">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object that is copied to this object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schedule" /> is null.</exception>
		// Token: 0x060001CA RID: 458 RVA: 0x00004AD5 File Offset: 0x00002CD5
		public ActiveDirectorySchedule(ActiveDirectorySchedule schedule)
			: this()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a range of times for a single day to the schedule.</summary>
		/// <param name="day">One of the <see cref="T:System.DayOfWeek" /> members that specifies the day of the week that the source will be available for replication.</param>
		/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
		/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
		/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
		/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
		/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="days" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
		// Token: 0x060001CB RID: 459 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetSchedule(DayOfWeek day, HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a range of times for multiple days to the schedule.</summary>
		/// <param name="days">One of the <see cref="T:System.DayOfWeek" /> members that specifies the day of the week that the source will be available for replication.</param>
		/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
		/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
		/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
		/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
		/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="days" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
		// Token: 0x060001CC RID: 460 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetSchedule(DayOfWeek[] days, HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a range of times for every day of the week to the schedule.</summary>
		/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
		/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
		/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
		/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
		// Token: 0x060001CD RID: 461 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetDailySchedule(HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Resets the entire schedule to unavailable.</summary>
		// Token: 0x060001CE RID: 462 RVA: 0x0000208C File Offset: 0x0000028C
		public void ResetSchedule()
		{
			throw new NotImplementedException();
		}
	}
}
