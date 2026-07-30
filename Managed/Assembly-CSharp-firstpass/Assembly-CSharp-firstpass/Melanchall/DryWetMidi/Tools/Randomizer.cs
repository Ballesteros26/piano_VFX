using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000055 RID: 85
	public abstract class Randomizer<TObject, TSettings> where TSettings : RandomizingSettings<TObject>, new()
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00009A34 File Offset: 0x00007C34
		protected void RandomizeInternal(IEnumerable<TObject> objects, IBounds bounds, TempoMap tempoMap, TSettings settings)
		{
			Randomizer<TObject, TSettings>.<>c__DisplayClass1_0 CS$<>8__locals1 = new Randomizer<TObject, TSettings>.<>c__DisplayClass1_0();
			CS$<>8__locals1.settings = settings;
			Randomizer<TObject, TSettings>.<>c__DisplayClass1_0 CS$<>8__locals2 = CS$<>8__locals1;
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
			foreach (TObject tobject in objects.Where(func))
			{
				long num = this.GetObjectTime(tobject, CS$<>8__locals1.settings);
				num = Randomizer<TObject, TSettings>.RandomizeTime(num, bounds, this._random, tempoMap);
				TimeProcessingInstruction timeProcessingInstruction = this.OnObjectRandomizing(tobject, num, CS$<>8__locals1.settings);
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

		// Token: 0x060001D7 RID: 471
		protected abstract long GetObjectTime(TObject obj, TSettings settings);

		// Token: 0x060001D8 RID: 472
		protected abstract void SetObjectTime(TObject obj, long time, TSettings settings);

		// Token: 0x060001D9 RID: 473
		protected abstract TimeProcessingInstruction OnObjectRandomizing(TObject obj, long time, TSettings settings);

		// Token: 0x060001DA RID: 474 RVA: 0x00009B0C File Offset: 0x00007D0C
		private static long RandomizeTime(long time, IBounds bounds, Random random, TempoMap tempoMap)
		{
			Tuple<long, long> bounds2 = bounds.GetBounds(time, tempoMap);
			long num = Math.Max(0L, bounds2.Item1) - 1L;
			int num2 = (int)Math.Abs(bounds2.Item2 - num);
			return num + (long)random.Next(num2) + 1L;
		}

		// Token: 0x040000ED RID: 237
		private readonly Random _random = new Random();
	}
}
