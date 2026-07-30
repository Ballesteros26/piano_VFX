using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the look and feel of a column in a table layout.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008E RID: 142
	public class ColumnStyle : TableLayoutStyle
	{
		/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class to its default state.</summary>
		// Token: 0x06000662 RID: 1634 RVA: 0x0001D78C File Offset: 0x0001B98C
		public ColumnStyle()
		{
			this.width = 0f;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
		// Token: 0x06000663 RID: 1635 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
		public ColumnStyle(SizeType sizeType)
		{
			this.width = 0f;
			base.SizeType = sizeType;
		}

		/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and width values.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
		/// <param name="width">The preferred width, in pixels or percentage, depending on the <paramref name="sizeType" /> parameter.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="width" /> is less than 0.</exception>
		// Token: 0x06000664 RID: 1636 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public ColumnStyle(SizeType sizeType, float width)
		{
			if (width < 0f)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			base.SizeType = sizeType;
			this.width = width;
		}

		/// <summary>Gets or sets the width value for a column.</summary>
		/// <returns>The preferred width, in pixels or percentage, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.width != value)
				{
					this.width = value;
					if (base.Owner != null)
					{
						base.Owner.PerformLayout();
					}
				}
			}
		}

		// Token: 0x0400073D RID: 1853
		private float width;
	}
}
