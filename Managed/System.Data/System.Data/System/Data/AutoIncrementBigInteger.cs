using System;
using System.Data.Common;
using System.Numerics;

namespace System.Data
{
	// Token: 0x02000062 RID: 98
	internal sealed class AutoIncrementBigInteger : AutoIncrementValue
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00012BA2 File Offset: 0x00010DA2
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00012BAF File Offset: 0x00010DAF
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (BigInteger)value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00012BBD File Offset: 0x00010DBD
		internal override Type DataType
		{
			get
			{
				return typeof(BigInteger);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00012BC9 File Offset: 0x00010DC9
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00012BD1 File Offset: 0x00010DD1
		internal override long Seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				if (this._current == this._seed || this.BoundaryCheck(value))
				{
					this._current = value;
				}
				this._seed = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00012C07 File Offset: 0x00010E07
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00012C14 File Offset: 0x00010E14
		internal override long Step
		{
			get
			{
				return (long)this._step;
			}
			set
			{
				if (value == 0L)
				{
					throw ExceptionBuilder.AutoIncrementSeed();
				}
				if (this._step != value)
				{
					if (this._current != this.Seed)
					{
						this._current = this._current - this._step + value;
					}
					this._step = value;
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012C79 File Offset: 0x00010E79
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012C92 File Offset: 0x00010E92
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = BigIntegerStorage.ConvertToBigInteger(value, formatProvider);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012CA4 File Offset: 0x00010EA4
		internal override void SetCurrentAndIncrement(object value)
		{
			BigInteger bigInteger = (BigInteger)value;
			if (this.BoundaryCheck(bigInteger))
			{
				this._current = bigInteger + this._step;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012CD3 File Offset: 0x00010ED3
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x0400053D RID: 1341
		private BigInteger _current;

		// Token: 0x0400053E RID: 1342
		private long _seed;

		// Token: 0x0400053F RID: 1343
		private BigInteger _step = 1;
	}
}
