using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000099 RID: 153
	public static class LengthedObjectUtilities
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public static TLength LengthAs<TLength>(this ILengthedObject obj, TempoMap tempoMap) where TLength : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return LengthConverter.ConvertTo<TLength>(obj.Length, obj.Time, tempoMap);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010FD2 File Offset: 0x0000F1D2
		public static ITimeSpan LengthAs(this ILengthedObject obj, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return LengthConverter.ConvertTo(obj.Length, lengthType, obj.Time, tempoMap);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00011008 File Offset: 0x0000F208
		public static TTime EndTimeAs<TTime>(this ILengthedObject obj, TempoMap tempoMap) where TTime : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo<TTime>(obj.Time + obj.Length, tempoMap);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00011033 File Offset: 0x0000F233
		public static ITimeSpan EndTimeAs(this ILengthedObject obj, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo(obj.Time + obj.Length, timeType, tempoMap);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001106A File Offset: 0x0000F26A
		public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ILengthedObject
		{
			return objects.AtTime(time, LengthedObjectPart.Start);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00011074 File Offset: 0x0000F274
		public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ILengthedObject
		{
			return objects.AtTime(time, LengthedObjectPart.End);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001107E File Offset: 0x0000F27E
		public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ILengthedObject
		{
			return objects.AtTime(time, tempoMap, LengthedObjectPart.Start);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011089 File Offset: 0x0000F289
		public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ILengthedObject
		{
			return objects.AtTime(time, tempoMap, LengthedObjectPart.End);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00011094 File Offset: 0x0000F294
		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, long time, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectPart>("matchBy", matchBy);
			return objects.Where((TObject o) => o != null && LengthedObjectUtilities.IsObjectAtTime<TObject>(o, time, matchBy));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectPart>("matchBy", matchBy);
			long num = TimeConverter.ConvertFrom(time, tempoMap);
			return objects.AtTime(num, matchBy);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011140 File Offset: 0x0000F340
		private static bool IsObjectAtTime<TObject>(TObject obj, long time, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			long time2 = obj.Time;
			if (time2 == time && (matchBy == LengthedObjectPart.Start || matchBy == LengthedObjectPart.Entire))
			{
				return true;
			}
			long num = time2 + obj.Length;
			return (num == time && (matchBy == LengthedObjectPart.End || matchBy == LengthedObjectPart.Entire)) || (matchBy == LengthedObjectPart.Entire && time >= time2 && time <= num);
		}
	}
}
