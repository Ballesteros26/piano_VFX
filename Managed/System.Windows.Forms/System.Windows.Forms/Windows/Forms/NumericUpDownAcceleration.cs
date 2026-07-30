using System;

namespace System.Windows.Forms
{
	/// <summary>Provides information specifying how acceleration should be performed on a spin box (also known as an up-down control) when the up or down button is pressed for specified time period.</summary>
	// Token: 0x02000279 RID: 633
	public class NumericUpDownAcceleration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> class.</summary>
		/// <param name="seconds">The number of seconds the up or down button is pressed before the acceleration starts. </param>
		/// <param name="increment">The quantity the value displayed in the control should be incremented or decremented during acceleration.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> or <paramref name="increment" /> is less than 0.</exception>
		// Token: 0x06002966 RID: 10598 RVA: 0x0009FD6C File Offset: 0x0009DF6C
		public NumericUpDownAcceleration(int seconds, decimal increment)
		{
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("Invalid seconds value. The seconds value must be equal or greater than zero.");
			}
			if (increment < 0m)
			{
				throw new ArgumentOutOfRangeException("Invalid increment value. The increment value must be equal or greater than zero.");
			}
			this.increment = increment;
			this.seconds = seconds;
		}

		/// <summary>Gets or sets the quantity to increment or decrement the displayed value during acceleration.</summary>
		/// <returns>The quantity to increment or decrement the displayed value during acceleration.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x0009FDBC File Offset: 0x0009DFBC
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x0009FDC4 File Offset: 0x0009DFC4
		public decimal Increment
		{
			get
			{
				return this.increment;
			}
			set
			{
				this.increment = value;
			}
		}

		/// <summary>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</summary>
		/// <returns>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0009FDD0 File Offset: 0x0009DFD0
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x0009FDD8 File Offset: 0x0009DFD8
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
			set
			{
				this.seconds = value;
			}
		}

		// Token: 0x04001498 RID: 5272
		private decimal increment;

		// Token: 0x04001499 RID: 5273
		private int seconds;
	}
}
