using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D3 RID: 211
	public static class TimedObjectUtilities
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00017A4B File Offset: 0x00015C4B
		public static TTime TimeAs<TTime>(this ITimedObject obj, TempoMap tempoMap) where TTime : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo<TTime>(obj.Time, tempoMap);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00017A6F File Offset: 0x00015C6F
		public static ITimeSpan TimeAs(this ITimedObject obj, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo(obj.Time, timeType, tempoMap);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfTimeArgument.IsNegative("time", time);
			return objects.Where((TObject o) => o.Time == time);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long num = TimeConverter.ConvertFrom(time, tempoMap);
			return objects.AtTime(num);
		}
	}
}
