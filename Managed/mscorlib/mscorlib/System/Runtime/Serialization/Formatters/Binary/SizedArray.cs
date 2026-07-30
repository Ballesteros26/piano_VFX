using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x06004D06 RID: 19718 RVA: 0x00116272 File Offset: 0x00114472
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00116293 File Offset: 0x00114493
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x001162B4 File Offset: 0x001144B4
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x00116311 File Offset: 0x00114511
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x17000CD8 RID: 3288
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
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
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x001163A8 File Offset: 0x001145A8
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					object[] array = new object[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					object[] array2 = new object[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x0400295D RID: 10589
		internal object[] objects;

		// Token: 0x0400295E RID: 10590
		internal object[] negObjects;
	}
}
