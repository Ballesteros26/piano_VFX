using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether a column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. This class cannot be inherited. </summary>
	// Token: 0x02000100 RID: 256
	[AttributeUsage(4, AllowMultiple = false, Inherited = true)]
	public sealed class DataGridViewColumnDesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the default <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value of true. </summary>
		// Token: 0x06001367 RID: 4967 RVA: 0x0004A8DC File Offset: 0x00048ADC
		public DataGridViewColumnDesignTimeVisibleAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property. </summary>
		/// <param name="visible">The value of the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property.</param>
		// Token: 0x06001368 RID: 4968 RVA: 0x0004A8E4 File Offset: 0x00048AE4
		public DataGridViewColumnDesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		/// <summary>Gets a value indicating whether the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
		/// <returns>true to indicate that the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer; otherwise, false.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004A918 File Offset: 0x00048B18
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
		/// <returns>true to indicate that the specified object is a <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> instance with the same <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value as this instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x0600136B RID: 4971 RVA: 0x0004A920 File Offset: 0x00048B20
		public override bool Equals(object obj)
		{
			return obj is DataGridViewColumnDesignTimeVisibleAttribute && (obj as DataGridViewColumnDesignTimeVisibleAttribute).visible == this.visible && base.Equals(obj);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600136C RID: 4972 RVA: 0x0004A95C File Offset: 0x00048B5C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether this attribute instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> attribute value.</summary>
		/// <returns>true to indicate that this instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> instance; otherwise, false.</returns>
		// Token: 0x0600136D RID: 4973 RVA: 0x0004A964 File Offset: 0x00048B64
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataGridViewColumnDesignTimeVisibleAttribute.Default);
		}

		/// <summary>The default <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value, which is <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Yes" />, indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
		// Token: 0x04000B5E RID: 2910
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Default = new DataGridViewColumnDesignTimeVisibleAttribute(true);

		/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is not visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
		// Token: 0x04000B5F RID: 2911
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute No = new DataGridViewColumnDesignTimeVisibleAttribute(false);

		/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
		// Token: 0x04000B60 RID: 2912
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Yes = new DataGridViewColumnDesignTimeVisibleAttribute(true);

		// Token: 0x04000B61 RID: 2913
		private bool visible;
	}
}
