using System;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates a metadata property to be included in an image file. Not inheritable.</summary>
	// Token: 0x02000110 RID: 272
	public sealed class PropertyItem
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00002050 File Offset: 0x00000250
		internal PropertyItem()
		{
		}

		/// <summary>Gets or sets the ID of the property.</summary>
		/// <returns>The integer that represents the ID of the property.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0001C720 File Offset: 0x0001A920
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0001C728 File Offset: 0x0001A928
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>Gets or sets the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that represents the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> byte array.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0001C731 File Offset: 0x0001A931
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0001C739 File Offset: 0x0001A939
		public int Len
		{
			get
			{
				return this._len;
			}
			set
			{
				this._len = value;
			}
		}

		/// <summary>Gets or sets an integer that defines the type of data contained in the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that defines the type of data contained in <see cref="P:System.Drawing.Imaging.PropertyItem.Value" />.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0001C742 File Offset: 0x0001A942
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0001C74A File Offset: 0x0001A94A
		public short Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the value of the property item.</summary>
		/// <returns>A byte array that represents the value of the property item.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0001C753 File Offset: 0x0001A953
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0001C75B File Offset: 0x0001A95B
		public byte[] Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000A1C RID: 2588
		private int _id;

		// Token: 0x04000A1D RID: 2589
		private int _len;

		// Token: 0x04000A1E RID: 2590
		private short _type;

		// Token: 0x04000A1F RID: 2591
		private byte[] _value;
	}
}
