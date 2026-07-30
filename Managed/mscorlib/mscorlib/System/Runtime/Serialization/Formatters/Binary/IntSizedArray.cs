using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000740 RID: 1856
	[Serializable]
	internal sealed class IntSizedArray : ICloneable
	{
		// Token: 0x06004D0D RID: 19725 RVA: 0x0011644C File Offset: 0x0011464C
		public IntSizedArray()
		{
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00116470 File Offset: 0x00114670
		private IntSizedArray(IntSizedArray sizedArray)
		{
			this.objects = new int[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new int[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x001164E6 File Offset: 0x001146E6
		public object Clone()
		{
			return new IntSizedArray(this);
		}

		// Token: 0x17000CD9 RID: 3289
		internal int this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return 0;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return 0;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				this.objects[index] = value;
			}
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x00116578 File Offset: 0x00114778
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int[] array = new int[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					int[] array2 = new int[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x0400295F RID: 10591
		internal int[] objects = new int[16];

		// Token: 0x04002960 RID: 10592
		internal int[] negObjects = new int[4];
	}
}
