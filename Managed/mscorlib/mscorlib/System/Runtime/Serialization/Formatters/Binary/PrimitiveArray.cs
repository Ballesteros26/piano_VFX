using System;
using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000745 RID: 1861
	internal sealed class PrimitiveArray
	{
		// Token: 0x06004D20 RID: 19744 RVA: 0x0011687C File Offset: 0x00114A7C
		internal PrimitiveArray(InternalPrimitiveTypeE code, Array array)
		{
			this.Init(code, array);
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0011688C File Offset: 0x00114A8C
		internal void Init(InternalPrimitiveTypeE code, Array array)
		{
			this.code = code;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA = (bool[])array;
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				this.charA = (char[])array;
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA = (double[])array;
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A = (short[])array;
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A = (int[])array;
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A = (long[])array;
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA = (sbyte[])array;
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA = (float[])array;
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A = (ushort[])array;
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A = (uint[])array;
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A = (ulong[])array;
				break;
			default:
				return;
			}
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x00116978 File Offset: 0x00114B78
		internal void SetValue(string value, int index)
		{
			switch (this.code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA[index] = bool.Parse(value);
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				if (value[0] == '_' && value.Equals("_0x00_"))
				{
					this.charA[index] = '\0';
					return;
				}
				this.charA[index] = char.Parse(value);
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA[index] = double.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A[index] = short.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A[index] = int.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A[index] = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA[index] = sbyte.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA[index] = float.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A[index] = ushort.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A[index] = uint.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A[index] = ulong.Parse(value, CultureInfo.InvariantCulture);
				break;
			default:
				return;
			}
		}

		// Token: 0x0400297C RID: 10620
		private InternalPrimitiveTypeE code;

		// Token: 0x0400297D RID: 10621
		private bool[] booleanA;

		// Token: 0x0400297E RID: 10622
		private char[] charA;

		// Token: 0x0400297F RID: 10623
		private double[] doubleA;

		// Token: 0x04002980 RID: 10624
		private short[] int16A;

		// Token: 0x04002981 RID: 10625
		private int[] int32A;

		// Token: 0x04002982 RID: 10626
		private long[] int64A;

		// Token: 0x04002983 RID: 10627
		private sbyte[] sbyteA;

		// Token: 0x04002984 RID: 10628
		private float[] singleA;

		// Token: 0x04002985 RID: 10629
		private ushort[] uint16A;

		// Token: 0x04002986 RID: 10630
		private uint[] uint32A;

		// Token: 0x04002987 RID: 10631
		private ulong[] uint64A;
	}
}
