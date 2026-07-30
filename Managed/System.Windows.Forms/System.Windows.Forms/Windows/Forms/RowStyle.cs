using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the look and feel of a row in a table layout.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002C2 RID: 706
	public class RowStyle : TableLayoutStyle
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class to its default state.</summary>
		// Token: 0x06002ECE RID: 11982 RVA: 0x000B4BAC File Offset: 0x000B2DAC
		public RowStyle()
		{
			this.height = 0f;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
		// Token: 0x06002ECF RID: 11983 RVA: 0x000B4BC0 File Offset: 0x000B2DC0
		public RowStyle(SizeType sizeType)
		{
			this.height = 0f;
			base.SizeType = sizeType;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and height values.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
		/// <param name="height">The preferred height in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on <paramref name="sizeType" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="height" /> is less than 0.</exception>
		// Token: 0x06002ED0 RID: 11984 RVA: 0x000B4BDC File Offset: 0x000B2DDC
		public RowStyle(SizeType sizeType, float height)
		{
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			base.SizeType = sizeType;
			this.height = height;
		}

		/// <summary>Gets or sets the height of a row.</summary>
		/// <returns>The preferred height of a row in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000B4C14 File Offset: 0x000B2E14
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000B4C1C File Offset: 0x000B2E1C
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.height != value)
				{
					this.height = value;
					if (base.Owner != null)
					{
						base.Owner.PerformLayout();
					}
				}
			}
		}

		// Token: 0x04001675 RID: 5749
		private float height;
	}
}
