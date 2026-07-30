using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200004A RID: 74
	public abstract class LengthedObjectsQuantizer<TObject, TSettings> : Quantizer<TObject, TSettings> where TObject : ILengthedObject where TSettings : LengthedObjectsQuantizingSettings<TObject>, new()
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x000092DA File Offset: 0x000074DA
		public void Quantize(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap, TSettings settings = default(TSettings))
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			base.QuantizeInternal(objects, grid, tempoMap, settings);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009308 File Offset: 0x00007508
		private static TimeProcessingInstruction CorrectObjectOnStartQuantizing(TObject obj, long time, TempoMap tempoMap, TSettings settings)
		{
			if (settings.FixOppositeEnd)
			{
				long num = obj.Time + obj.Length;
				if (time > num)
				{
					TimeProcessingInstruction timeProcessingInstruction = LengthedObjectsQuantizer<TObject, TSettings>.ProcessQuantizingBeyondFixedEnd(ref time, ref num, settings.QuantizingBeyondFixedEndPolicy, "Start time is going to be beyond the end one.");
					if (timeProcessingInstruction != null)
					{
						return timeProcessingInstruction;
					}
				}
				LengthSetter.SetObjectLength<TObject>(obj, num - time);
			}
			else
			{
				ITimeSpan timeSpan = obj.LengthAs(settings.LengthType, tempoMap);
				LengthSetter.SetObjectLength<TObject>(obj, LengthConverter.ConvertFrom(timeSpan, time, tempoMap));
			}
			return new TimeProcessingInstruction(time);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009398 File Offset: 0x00007598
		private static TimeProcessingInstruction CorrectObjectOnEndQuantizing(TObject obj, long time, TempoMap tempoMap, TSettings settings)
		{
			if (settings.FixOppositeEnd)
			{
				long time2 = obj.Time;
				if (time < time2)
				{
					TimeProcessingInstruction timeProcessingInstruction = LengthedObjectsQuantizer<TObject, TSettings>.ProcessQuantizingBeyondFixedEnd(ref time, ref time2, settings.QuantizingBeyondFixedEndPolicy, "End time is going to be beyond the start one.");
					if (timeProcessingInstruction != null)
					{
						return timeProcessingInstruction;
					}
				}
				LengthSetter.SetObjectLength<TObject>(obj, time - time2);
			}
			else
			{
				ITimeSpan timeSpan = obj.LengthAs(settings.LengthType, tempoMap);
				long num = ((settings.LengthType == TimeSpanType.Midi) ? (time - obj.Length) : TimeConverter.ConvertFrom(((MidiTimeSpan)time).Subtract(timeSpan, TimeSpanMode.TimeLength), tempoMap));
				if (num < 0L)
				{
					switch (settings.QuantizingBeyondZeroPolicy)
					{
					case QuantizingBeyondZeroPolicy.FixAtZero:
						LengthSetter.SetObjectLength<TObject>(obj, time);
						break;
					case QuantizingBeyondZeroPolicy.Skip:
						return TimeProcessingInstruction.Skip;
					case QuantizingBeyondZeroPolicy.Abort:
						throw new InvalidOperationException("Object is going to be moved beyond zero.");
					}
				}
				else
				{
					LengthSetter.SetObjectLength<TObject>(obj, LengthConverter.ConvertFrom(timeSpan, num, tempoMap));
				}
			}
			return new TimeProcessingInstruction(time);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009494 File Offset: 0x00007694
		private static TimeProcessingInstruction ProcessQuantizingBeyondFixedEnd(ref long newTime, ref long oldTime, QuantizingBeyondFixedEndPolicy quantizingBeyondFixedEndPolicy, string errorMessage)
		{
			switch (quantizingBeyondFixedEndPolicy)
			{
			case QuantizingBeyondFixedEndPolicy.CollapseAndFix:
				newTime = oldTime;
				break;
			case QuantizingBeyondFixedEndPolicy.CollapseAndMove:
				oldTime = newTime;
				break;
			case QuantizingBeyondFixedEndPolicy.SwapEnds:
			{
				long num = newTime;
				newTime = oldTime;
				oldTime = num;
				break;
			}
			case QuantizingBeyondFixedEndPolicy.Skip:
				return TimeProcessingInstruction.Skip;
			case QuantizingBeyondFixedEndPolicy.Abort:
				throw new InvalidOperationException(errorMessage);
			}
			return null;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000094E4 File Offset: 0x000076E4
		protected sealed override long GetObjectTime(TObject obj, TSettings settings)
		{
			LengthedObjectTarget quantizingTarget = settings.QuantizingTarget;
			if (quantizingTarget == LengthedObjectTarget.Start)
			{
				return obj.Time;
			}
			if (quantizingTarget != LengthedObjectTarget.End)
			{
				throw new NotSupportedException(string.Format("{0} quantization target is not supported to get time.", quantizingTarget));
			}
			return obj.Time + obj.Length;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009548 File Offset: 0x00007748
		protected sealed override void SetObjectTime(TObject obj, long time, TSettings settings)
		{
			LengthedObjectTarget quantizingTarget = settings.QuantizingTarget;
			if (quantizingTarget == LengthedObjectTarget.Start)
			{
				TimeSetter.SetObjectTime<TObject>(obj, time);
				return;
			}
			if (quantizingTarget != LengthedObjectTarget.End)
			{
				throw new NotSupportedException(string.Format("{0} quantization target is not supported to set time.", quantizingTarget));
			}
			TimeSetter.SetObjectTime<TObject>(obj, time - obj.Length);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000095A0 File Offset: 0x000077A0
		protected override TimeProcessingInstruction OnObjectQuantizing(TObject obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TSettings settings)
		{
			long newTime = quantizedTime.NewTime;
			LengthedObjectTarget quantizingTarget = settings.QuantizingTarget;
			if (quantizingTarget == LengthedObjectTarget.Start)
			{
				return LengthedObjectsQuantizer<TObject, TSettings>.CorrectObjectOnStartQuantizing(obj, newTime, tempoMap, settings);
			}
			if (quantizingTarget != LengthedObjectTarget.End)
			{
				return new TimeProcessingInstruction(newTime);
			}
			return LengthedObjectsQuantizer<TObject, TSettings>.CorrectObjectOnEndQuantizing(obj, newTime, tempoMap, settings);
		}
	}
}
