using System;
using System.Data.Common;
using System.Globalization;
using System.Numerics;

namespace System.Data
{
	// Token: 0x02000061 RID: 97
	internal sealed class AutoIncrementInt64 : AutoIncrementValue
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00012A51 File Offset: 0x00010C51
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00012A5E File Offset: 0x00010C5E
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (long)value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012A6C File Offset: 0x00010C6C
		internal override Type DataType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00012A78 File Offset: 0x00010C78
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00012A80 File Offset: 0x00010C80
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00012AAC File Offset: 0x00010CAC
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00012AB4 File Offset: 0x00010CB4
		internal override long Step
		{
			get
			{
				return this._step;
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

		// Token: 0x060003A2 RID: 930 RVA: 0x00012AF2 File Offset: 0x00010CF2
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00012B07 File Offset: 0x00010D07
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = Convert.ToInt64(value, formatProvider);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012B18 File Offset: 0x00010D18
		internal override void SetCurrentAndIncrement(object value)
		{
			long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), CultureInfo.InvariantCulture);
			if (this.BoundaryCheck(num))
			{
				this._current = num + this._step;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00012B5E File Offset: 0x00010D5E
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x0400053A RID: 1338
		private long _current;

		// Token: 0x0400053B RID: 1339
		private long _seed;

		// Token: 0x0400053C RID: 1340
		private long _step = 1L;
	}
}
