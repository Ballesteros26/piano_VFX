using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Contains border styles for the cells in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D5 RID: 213
	public sealed class DataGridViewAdvancedBorderStyle : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> class. </summary>
		// Token: 0x06001114 RID: 4372 RVA: 0x00044AA0 File Offset: 0x00042CA0
		public DataGridViewAdvancedBorderStyle()
		{
			this.All = DataGridViewAdvancedCellBorderStyle.None;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06001115 RID: 4373 RVA: 0x00044AB0 File Offset: 0x00042CB0
		object ICloneable.Clone()
		{
			return new DataGridViewAdvancedBorderStyle
			{
				bottom = this.bottom,
				left = this.left,
				right = this.right,
				top = this.top
			};
		}

		/// <summary>Gets or sets the border style for all of the borders of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" />, <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetPartial" />, or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance was retrieved through the <see cref="P:System.Windows.Forms.DataGridView.AdvancedCellBorderStyle" /> property.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00044AF4 File Offset: 0x00042CF4
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00044B34 File Offset: 0x00042D34
		public DataGridViewAdvancedCellBorderStyle All
		{
			get
			{
				if (this.bottom == this.left && this.left == this.right && this.right == this.top)
				{
					return this.bottom;
				}
				return DataGridViewAdvancedCellBorderStyle.NotSet;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
				}
				this.top = value;
				this.right = value;
				this.left = value;
				this.bottom = value;
			}
		}

		/// <summary>Gets or sets the style for the bottom border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00044B88 File Offset: 0x00042D88
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00044B90 File Offset: 0x00042D90
		public DataGridViewAdvancedCellBorderStyle Bottom
		{
			get
			{
				return this.bottom;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException("Invlid Bottom value.");
				}
				this.bottom = value;
			}
		}

		/// <summary>Gets the style for the left border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of true.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00044BD0 File Offset: 0x00042DD0
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00044BD8 File Offset: 0x00042DD8
		public DataGridViewAdvancedCellBorderStyle Left
		{
			get
			{
				return this.left;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException("Invlid Left value.");
				}
				this.left = value;
			}
		}

		/// <summary>Gets the style for the right border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of false.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00044C18 File Offset: 0x00042E18
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00044C20 File Offset: 0x00042E20
		public DataGridViewAdvancedCellBorderStyle Right
		{
			get
			{
				return this.right;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException("Invlid Right value.");
				}
				this.right = value;
			}
		}

		/// <summary>Gets the style for the top border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00044C60 File Offset: 0x00042E60
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00044C68 File Offset: 0x00042E68
		public DataGridViewAdvancedCellBorderStyle Top
		{
			get
			{
				return this.top;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException("Invlid Top value.");
				}
				this.top = value;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
		/// <returns>true if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> and the values for the <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Top" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Bottom" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Left" />, and <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Right" /> properties are equal to their counterpart in the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />; otherwise, false.</returns>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001120 RID: 4384 RVA: 0x00044CA8 File Offset: 0x00042EA8
		public override bool Equals(object other)
		{
			if (other is DataGridViewAdvancedBorderStyle)
			{
				DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = (DataGridViewAdvancedBorderStyle)other;
				return this.bottom == dataGridViewAdvancedBorderStyle.bottom && this.left == dataGridViewAdvancedBorderStyle.left && this.right == dataGridViewAdvancedBorderStyle.right && this.top == dataGridViewAdvancedBorderStyle.top;
			}
			return false;
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06001121 RID: 4385 RVA: 0x00044D10 File Offset: 0x00042F10
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
		/// <returns>A string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001122 RID: 4386 RVA: 0x00044D18 File Offset: 0x00042F18
		public override string ToString()
		{
			return string.Format("DataGridViewAdvancedBorderStyle { All={0}, Left={1}, Right={2}, Top={3}, Bottom={4} }", new object[] { this.All, this.Left, this.Right, this.Top, this.Bottom });
		}

		// Token: 0x04000A97 RID: 2711
		private DataGridViewAdvancedCellBorderStyle bottom;

		// Token: 0x04000A98 RID: 2712
		private DataGridViewAdvancedCellBorderStyle left;

		// Token: 0x04000A99 RID: 2713
		private DataGridViewAdvancedCellBorderStyle right;

		// Token: 0x04000A9A RID: 2714
		private DataGridViewAdvancedCellBorderStyle top;
	}
}
