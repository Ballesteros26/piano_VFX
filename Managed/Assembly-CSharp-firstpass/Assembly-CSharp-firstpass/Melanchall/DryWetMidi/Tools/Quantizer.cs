using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000046 RID: 70
	public abstract class Quantizer<TObject, TSettings> where TSettings : QuantizingSettings<TObject>, new()
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00009020 File Offset: 0x00007220
		protected void QuantizeInternal(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap, TSettings settings)
		{
			Quantizer<TObject, TSettings>.<>c__DisplayClass0_0 CS$<>8__locals1 = new Quantizer<TObject, TSettings>.<>c__DisplayClass0_0();
			CS$<>8__locals1.settings = settings;
			CS$<>8__locals1.<>4__this = this;
			Quantizer<TObject, TSettings>.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
			TSettings tsettings;
			if ((tsettings = CS$<>8__locals1.settings) == null)
			{
				tsettings = new TSettings();
			}
			CS$<>8__locals2.settings = tsettings;
			Func<TObject, bool> func = delegate(TObject o)
			{
				if (o != null)
				{
					Predicate<TObject> filter = CS$<>8__locals1.settings.Filter;
					return filter == null || filter(o);
				}
				return false;
			};
			long num = (from o in objects.Where(func)
				select CS$<>8__locals1.<>4__this.GetObjectTime(o, CS$<>8__locals1.settings)).DefaultIfEmpty<long>().Max();
			List<long> list = Quantizer<TObject, TSettings>.GetGridTimes(grid, num, tempoMap).ToList<long>();
			foreach (TObject tobject in objects.Where(func))
			{
				long objectTime = this.GetObjectTime(tobject, CS$<>8__locals1.settings);
				QuantizedTime quantizedTime = Quantizer<TObject, TSettings>.FindNearestTime(list, objectTime, CS$<>8__locals1.settings.DistanceCalculationType, CS$<>8__locals1.settings.QuantizingLevel, tempoMap);
				TimeProcessingInstruction timeProcessingInstruction = this.OnObjectQuantizing(tobject, quantizedTime, grid, tempoMap, CS$<>8__locals1.settings);
				TimeProcessingAction action = timeProcessingInstruction.Action;
				if (action != TimeProcessingAction.Apply)
				{
					if (action != TimeProcessingAction.Skip)
					{
					}
				}
				else
				{
					this.SetObjectTime(tobject, timeProcessingInstruction.Time, CS$<>8__locals1.settings);
				}
			}
		}

		// Token: 0x060001A3 RID: 419
		protected abstract long GetObjectTime(TObject obj, TSettings settings);

		// Token: 0x060001A4 RID: 420
		protected abstract void SetObjectTime(TObject obj, long time, TSettings settings);

		// Token: 0x060001A5 RID: 421
		protected abstract TimeProcessingInstruction OnObjectQuantizing(TObject obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TSettings settings);

		// Token: 0x060001A6 RID: 422 RVA: 0x00009158 File Offset: 0x00007358
		private static IEnumerable<long> GetGridTimes(IGrid grid, long lastTime, TempoMap tempoMap)
		{
			IEnumerable<long> times = grid.GetTimes(tempoMap);
			using (IEnumerator<long> enumerator = times.GetEnumerator())
			{
				while (enumerator.MoveNext() && enumerator.Current < lastTime)
				{
					yield return enumerator.Current;
				}
				yield return enumerator.Current;
			}
			IEnumerator<long> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009178 File Offset: 0x00007378
		private static QuantizedTime FindNearestTime(IReadOnlyList<long> grid, long time, TimeSpanType distanceCalculationType, double quantizingLevel, TempoMap tempoMap)
		{
			long num = -1L;
			ITimeSpan timeSpan = TimeSpanUtilities.GetMaxTimeSpan(distanceCalculationType);
			long num2 = -1L;
			for (int i = 0; i < grid.Count; i++)
			{
				long num3 = grid[i];
				long num4 = Math.Abs(time - num3);
				ITimeSpan timeSpan2 = LengthConverter.ConvertTo(num4, distanceCalculationType, Math.Min(time, num3), tempoMap);
				if (timeSpan2.CompareTo(timeSpan) >= 0)
				{
					break;
				}
				num = num4;
				timeSpan = timeSpan2;
				num2 = num3;
			}
			ITimeSpan timeSpan3 = timeSpan.Multiply(quantizingLevel);
			ITimeSpan timeSpan4 = TimeConverter.ConvertTo(time, distanceCalculationType, tempoMap);
			return new QuantizedTime(TimeConverter.ConvertFrom((num2 > time) ? timeSpan4.Add(timeSpan3, TimeSpanMode.TimeLength) : timeSpan4.Subtract(timeSpan3, TimeSpanMode.TimeLength), tempoMap), num2, timeSpan3, num, timeSpan);
		}
	}
}
