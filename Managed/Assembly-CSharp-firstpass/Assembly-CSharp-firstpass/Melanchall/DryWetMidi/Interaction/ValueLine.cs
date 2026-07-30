using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000DE RID: 222
	public sealed class ValueLine<TValue> : IEnumerable<ValueChange<TValue>>, IEnumerable where TValue : class
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000568 RID: 1384 RVA: 0x00018220 File Offset: 0x00016420
		// (remove) Token: 0x06000569 RID: 1385 RVA: 0x00018258 File Offset: 0x00016458
		internal event EventHandler ValuesChanged;

		// Token: 0x0600056A RID: 1386 RVA: 0x0001828D File Offset: 0x0001648D
		internal ValueLine(TValue defaultValue)
		{
			this._defaultValue = defaultValue;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000182B0 File Offset: 0x000164B0
		public TValue AtTime(long time)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ValueChange<TValue> valueChange = this.TakeWhile((ValueChange<TValue> p) => p.Time <= time).LastOrDefault<ValueChange<TValue>>();
			TValue tvalue;
			if ((tvalue = ((valueChange != null) ? valueChange.Value : default(TValue))) == null)
			{
				tvalue = this._defaultValue;
			}
			return tvalue;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018314 File Offset: 0x00016514
		internal void SetValue(long time, TValue value)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("value", value);
			if (this.AtTime(time).Equals(value))
			{
				return;
			}
			this._values.RemoveAll((ValueChange<TValue> v) => v.Time == time);
			this._values.Add(new ValueChange<TValue>(time, value));
			this.OnValuesChanged();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000183A2 File Offset: 0x000165A2
		internal void DeleteValues(long startTime)
		{
			this.DeleteValues(startTime, long.MaxValue);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000183B4 File Offset: 0x000165B4
		internal void DeleteValues(long startTime, long endTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			ThrowIfTimeArgument.EndIsNegative("endTime", endTime);
			this._values.RemoveAll((ValueChange<TValue> v) => v.Time >= startTime && v.Time <= endTime);
			this.OnValuesChanged();
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018413 File Offset: 0x00016613
		internal void Clear()
		{
			this._values.Clear();
			this.OnValuesChanged();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018426 File Offset: 0x00016626
		internal void ReplaceValues(ValueLine<TValue> valueLine)
		{
			this._values.Clear();
			this._values.AddRange(valueLine._values);
			this.OnValuesChanged();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001844C File Offset: 0x0001664C
		internal ValueLine<TValue> Reverse(long centerTime)
		{
			long maxTime = 2L * centerTime;
			IEnumerable<ValueChange<TValue>> enumerable = this.TakeWhile((ValueChange<TValue> c) => c.Time <= maxTime);
			IEnumerable<TValue> enumerable2 = new TValue[] { this._defaultValue }.Concat(enumerable.Select((ValueChange<TValue> c) => c.Value)).Reverse<TValue>();
			IEnumerable<long> enumerable3 = new long[1].Concat(enumerable.Select((ValueChange<TValue> c) => maxTime - c.Time).Reverse<long>());
			ValueLine<TValue> valueLine = new ValueLine<TValue>(this._defaultValue);
			valueLine._values.AddRange(enumerable2.Zip(enumerable3, (TValue v, long t) => new ValueChange<TValue>(t, v)));
			return valueLine;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001851E File Offset: 0x0001671E
		private void OnValuesChanged()
		{
			this.OnValuesNeedSorting();
			EventHandler valuesChanged = this.ValuesChanged;
			if (valuesChanged == null)
			{
				return;
			}
			valuesChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001853C File Offset: 0x0001673C
		private void OnValuesNeedSorting()
		{
			this._valuesChanged = true;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018545 File Offset: 0x00016745
		private void OnValuesSortingCompleted()
		{
			this._valuesChanged = false;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001854E File Offset: 0x0001674E
		public IEnumerator<ValueChange<TValue>> GetEnumerator()
		{
			if (this._valuesChanged)
			{
				this._values.Sort(new TimedObjectsComparer<ValueChange<TValue>>());
				this.OnValuesSortingCompleted();
			}
			return this._values.GetEnumerator();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001857E File Offset: 0x0001677E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000732 RID: 1842
		private readonly List<ValueChange<TValue>> _values = new List<ValueChange<TValue>>();

		// Token: 0x04000733 RID: 1843
		private readonly TValue _defaultValue;

		// Token: 0x04000734 RID: 1844
		private bool _valuesChanged = true;
	}
}
