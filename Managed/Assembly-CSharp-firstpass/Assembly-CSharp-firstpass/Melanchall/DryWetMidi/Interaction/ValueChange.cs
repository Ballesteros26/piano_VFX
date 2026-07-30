using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000DD RID: 221
	public sealed class ValueChange<TValue> : ITimedObject
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00018118 File Offset: 0x00016318
		internal ValueChange(long time, TValue value)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("value", value);
			this.Time = time;
			this.Value = value;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00018149 File Offset: 0x00016349
		public long Time { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00018151 File Offset: 0x00016351
		public TValue Value { get; }

		// Token: 0x06000563 RID: 1379 RVA: 0x0001815C File Offset: 0x0001635C
		public static bool operator ==(ValueChange<TValue> change1, ValueChange<TValue> change2)
		{
			if (change1 == change2)
			{
				return true;
			}
			if (change1 == null || change2 == null)
			{
				return false;
			}
			if (change1.Time == change2.Time)
			{
				TValue value = change1.Value;
				return value.Equals(change2.Value);
			}
			return false;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000181A6 File Offset: 0x000163A6
		public static bool operator !=(ValueChange<TValue> change1, ValueChange<TValue> change2)
		{
			return !(change1 == change2);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000181B2 File Offset: 0x000163B2
		public override string ToString()
		{
			return string.Format("{0} at {1}", this.Value, this.Time);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000181D4 File Offset: 0x000163D4
		public override bool Equals(object obj)
		{
			return this == obj as ValueChange<TValue>;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000181E4 File Offset: 0x000163E4
		public override int GetHashCode()
		{
			int num = (17 * 23 + this.Time.GetHashCode()) * 23;
			TValue value = this.Value;
			return num + value.GetHashCode();
		}
	}
}
