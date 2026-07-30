using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200005B RID: 91
	public abstract class LengthedObjectsRandomizer<TObject, TSettings> : Randomizer<TObject, TSettings> where TObject : ILengthedObject where TSettings : LengthedObjectsRandomizingSettings<TObject>, new()
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00009C3C File Offset: 0x00007E3C
		public void Randomize(IEnumerable<TObject> objects, IBounds bounds, TempoMap tempoMap, TSettings settings = default(TSettings))
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			base.RandomizeInternal(objects, bounds, tempoMap, settings);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009C6C File Offset: 0x00007E6C
		protected sealed override long GetObjectTime(TObject obj, TSettings settings)
		{
			LengthedObjectTarget randomizingTarget = settings.RandomizingTarget;
			if (randomizingTarget == LengthedObjectTarget.Start)
			{
				return obj.Time;
			}
			if (randomizingTarget != LengthedObjectTarget.End)
			{
				throw new NotSupportedException(string.Format("{0} randomization target is not supported to get time.", randomizingTarget));
			}
			return obj.Time + obj.Length;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009CD0 File Offset: 0x00007ED0
		protected sealed override void SetObjectTime(TObject obj, long time, TSettings settings)
		{
			LengthedObjectTarget randomizingTarget = settings.RandomizingTarget;
			if (randomizingTarget == LengthedObjectTarget.Start)
			{
				TimeSetter.SetObjectTime<TObject>(obj, time);
				return;
			}
			if (randomizingTarget != LengthedObjectTarget.End)
			{
				throw new NotSupportedException(string.Format("{0} randomization target is not supported to set time.", randomizingTarget));
			}
			TimeSetter.SetObjectTime<TObject>(obj, time - obj.Length);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009D28 File Offset: 0x00007F28
		protected override TimeProcessingInstruction OnObjectRandomizing(TObject obj, long time, TSettings settings)
		{
			LengthedObjectTarget randomizingTarget = settings.RandomizingTarget;
			if (randomizingTarget != LengthedObjectTarget.Start)
			{
				if (randomizingTarget == LengthedObjectTarget.End)
				{
					if (settings.FixOppositeEnd)
					{
						LengthSetter.SetObjectLength<TObject>(obj, time - obj.Time);
					}
				}
			}
			else if (settings.FixOppositeEnd)
			{
				LengthSetter.SetObjectLength<TObject>(obj, obj.Time + obj.Length - time);
			}
			return new TimeProcessingInstruction(time);
		}
	}
}
