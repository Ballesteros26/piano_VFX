using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000033 RID: 51
	public abstract class LengthedObjectsSplitter<TObject> where TObject : ILengthedObject
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000074E4 File Offset: 0x000056E4
		public IEnumerable<TObject> SplitByStep(IEnumerable<TObject> objects, ITimeSpan step, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject tobject in objects)
			{
				if (tobject == null)
				{
					TObject tobject2 = default(TObject);
				}
				else if (tobject.Length == 0L)
				{
					yield return this.CloneObject(tobject);
				}
				else
				{
					long time2 = tobject.Time;
					long endTime = time2 + tobject.Length;
					long time = time2;
					TObject tobject3 = this.CloneObject(tobject);
					while (time < endTime && tobject3 != null)
					{
						long num = LengthConverter.ConvertFrom(step, time, tempoMap);
						if (num == 0L)
						{
							throw new InvalidOperationException("Step is too small.");
						}
						time += num;
						SplittedLengthedObject<TObject> parts = this.SplitObject(tobject3, time);
						yield return parts.LeftPart;
						tobject3 = parts.RightPart;
						parts = null;
					}
				}
			}
			IEnumerator<TObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007509 File Offset: 0x00005709
		public IEnumerable<TObject> SplitByPartsNumber(IEnumerable<TObject> objects, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject obj in objects)
			{
				if (obj == null)
				{
					TObject tobject = default(TObject);
				}
				else if (partsNumber == 1)
				{
					yield return this.CloneObject(obj);
				}
				else if (obj.Length == 0L)
				{
					foreach (int num in Enumerable.Range(0, partsNumber))
					{
						yield return this.CloneObject(obj);
					}
					IEnumerator<int> enumerator2 = null;
				}
				else
				{
					long time = obj.Time;
					TObject tobject2 = this.CloneObject(obj);
					int partsRemaining = partsNumber;
					while (partsRemaining > 1 && tobject2 != null)
					{
						ITimeSpan timeSpan = tobject2.LengthAs(lengthType, tempoMap).Divide((double)partsRemaining);
						time += LengthConverter.ConvertFrom(timeSpan, time, tempoMap);
						SplittedLengthedObject<TObject> parts = this.SplitObject(tobject2, time);
						yield return parts.LeftPart;
						tobject2 = parts.RightPart;
						parts = null;
						int num2 = partsRemaining;
						partsRemaining = num2 - 1;
					}
					if (tobject2 != null)
					{
						yield return tobject2;
					}
					obj = default(TObject);
				}
			}
			IEnumerator<TObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007536 File Offset: 0x00005736
		public IEnumerable<TObject> SplitByGrid(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long lastObjectEndTime = (from o in objects
				where o != null
				select o.Time + o.Length).DefaultIfEmpty<long>().Max();
			List<long> times = grid.GetTimes(tempoMap).TakeWhile((long t) => t < lastObjectEndTime).Distinct<long>()
				.ToList<long>();
			times.Sort();
			foreach (TObject tobject in objects)
			{
				if (tobject == null)
				{
					TObject tobject2 = default(TObject);
				}
				else
				{
					long startTime = tobject.Time;
					long endTime = startTime + tobject.Length;
					IEnumerable<long> enumerable = times.SkipWhile((long t) => t <= startTime).TakeWhile((long t) => t < endTime);
					TObject tobject3 = this.CloneObject(tobject);
					foreach (long num in enumerable)
					{
						SplittedLengthedObject<TObject> parts = this.SplitObject(tobject3, num);
						yield return parts.LeftPart;
						tobject3 = parts.RightPart;
						parts = null;
					}
					IEnumerator<long> enumerator2 = null;
					yield return tobject3;
				}
			}
			IEnumerator<TObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000755B File Offset: 0x0000575B
		public IEnumerable<TObject> SplitAtDistance(IEnumerable<TObject> objects, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject tobject in objects)
			{
				if (tobject == null)
				{
					TObject tobject2 = default(TObject);
				}
				else
				{
					SplittedLengthedObject<TObject> parts = this.SplitObjectAtDistance(tobject, distance, from, tempoMap);
					if (parts.LeftPart != null)
					{
						yield return parts.LeftPart;
					}
					if (parts.RightPart != null)
					{
						yield return parts.RightPart;
					}
					parts = null;
				}
			}
			IEnumerator<TObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007588 File Offset: 0x00005788
		public IEnumerable<TObject> SplitAtDistance(IEnumerable<TObject> objects, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject tobject in objects)
			{
				if (tobject == null)
				{
					TObject tobject2 = default(TObject);
				}
				else
				{
					ITimeSpan timeSpan = tobject.LengthAs(lengthType, tempoMap).Multiply(ratio);
					SplittedLengthedObject<TObject> parts = this.SplitObjectAtDistance(tobject, timeSpan, from, tempoMap);
					if (parts.LeftPart != null)
					{
						yield return parts.LeftPart;
					}
					if (parts.RightPart != null)
					{
						yield return parts.RightPart;
					}
					parts = null;
				}
			}
			IEnumerator<TObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000139 RID: 313
		protected abstract TObject CloneObject(TObject obj);

		// Token: 0x0600013A RID: 314
		protected abstract SplittedLengthedObject<TObject> SplitObject(TObject obj, long time);

		// Token: 0x0600013B RID: 315 RVA: 0x000075C0 File Offset: 0x000057C0
		private SplittedLengthedObject<TObject> SplitObjectAtDistance(TObject obj, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ITimeSpan timeSpan = ((from == LengthedObjectTarget.Start) ? ((MidiTimeSpan)obj.Time).Add(distance, TimeSpanMode.TimeLength) : ((MidiTimeSpan)(obj.Time + obj.Length)).Subtract(distance, TimeSpanMode.TimeLength));
			return this.SplitObject(obj, TimeConverter.ConvertFrom(timeSpan, tempoMap));
		}

		// Token: 0x040000BE RID: 190
		internal const double ZeroRatio = 0.0;

		// Token: 0x040000BF RID: 191
		internal const double FullLengthRatio = 1.0;
	}
}
