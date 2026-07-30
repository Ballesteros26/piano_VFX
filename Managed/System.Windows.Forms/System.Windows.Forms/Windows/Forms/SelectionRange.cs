using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a date selection range in a month calendar control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D8 RID: 728
	[TypeConverter(typeof(SelectionRangeConverter))]
	public sealed class SelectionRange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class.</summary>
		// Token: 0x06002FFA RID: 12282 RVA: 0x000B994C File Offset: 0x000B7B4C
		public SelectionRange()
		{
			DateTime maxValue = DateTime.MaxValue;
			this.end = maxValue.Date;
			DateTime minValue = DateTime.MinValue;
			this.start = minValue.Date;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified selection range.</summary>
		/// <param name="range">The existing <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
		// Token: 0x06002FFB RID: 12283 RVA: 0x000B9988 File Offset: 0x000B7B88
		public SelectionRange(SelectionRange range)
		{
			this.end = range.End;
			this.start = range.Start;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified beginning and ending dates.</summary>
		/// <param name="lower">The starting date in the <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
		/// <param name="upper">The ending date in the <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000B99A8 File Offset: 0x000B7BA8
		public SelectionRange(DateTime lower, DateTime upper)
		{
			if (lower <= upper)
			{
				this.end = upper.Date;
				this.start = lower.Date;
			}
			else
			{
				this.end = lower.Date;
				this.start = upper.Date;
			}
		}

		/// <summary>Gets or sets the ending date and time of the selection range.</summary>
		/// <returns>The ending <see cref="T:System.DateTime" /> value of the range.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000B9A1C File Offset: 0x000B7C1C
		// (set) Token: 0x06002FFD RID: 12285 RVA: 0x000B9A00 File Offset: 0x000B7C00
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				if (this.end != value)
				{
					this.end = value;
				}
			}
		}

		/// <summary>Gets or sets the starting date and time of the selection range.</summary>
		/// <returns>The starting <see cref="T:System.DateTime" /> value of the range.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000B9A40 File Offset: 0x000B7C40
		// (set) Token: 0x06002FFF RID: 12287 RVA: 0x000B9A24 File Offset: 0x000B7C24
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				if (this.start != value)
				{
					this.start = value;
				}
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.SelectionRange" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.SelectionRange" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003001 RID: 12289 RVA: 0x000B9A48 File Offset: 0x000B7C48
		public override string ToString()
		{
			return "SelectionRange: Start: " + this.Start.ToString() + ", End: " + this.End.ToString();
		}

		// Token: 0x04001702 RID: 5890
		private DateTime end;

		// Token: 0x04001703 RID: 5891
		private DateTime start;
	}
}
