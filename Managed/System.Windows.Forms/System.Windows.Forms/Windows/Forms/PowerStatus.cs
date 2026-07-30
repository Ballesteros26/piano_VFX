using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates current system power status information.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200028D RID: 653
	public class PowerStatus
	{
		// Token: 0x06002A81 RID: 10881 RVA: 0x000A3E54 File Offset: 0x000A2054
		internal PowerStatus(BatteryChargeStatus batteryChargeStatus, int batteryFullLifetime, float batteryLifePercent, int batteryLifeRemaining, PowerLineStatus powerLineStatus)
		{
			this.battery_charge_status = batteryChargeStatus;
			this.battery_full_lifetime = batteryFullLifetime;
			this.battery_life_percent = batteryLifePercent;
			this.battery_life_remaining = batteryLifeRemaining;
			this.power_line_status = powerLineStatus;
		}

		/// <summary>Gets the current battery charge status.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BatteryChargeStatus" /> values indicating the current battery charge level or charging status.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000A3E84 File Offset: 0x000A2084
		public BatteryChargeStatus BatteryChargeStatus
		{
			get
			{
				return this.battery_charge_status;
			}
		}

		/// <summary>Gets the reported full charge lifetime of the primary battery power source in seconds.</summary>
		/// <returns>The reported number of seconds of battery life available when the battery is fully charged, or -1 if the battery life is unknown.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000A3E8C File Offset: 0x000A208C
		public int BatteryFullLifetime
		{
			get
			{
				return this.battery_full_lifetime;
			}
		}

		/// <summary>Gets the approximate amount of full battery charge remaining.</summary>
		/// <returns>The approximate amount, from 0.0 to 1.0, of full battery charge remaining.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000A3E94 File Offset: 0x000A2094
		public float BatteryLifePercent
		{
			get
			{
				return this.battery_life_percent;
			}
		}

		/// <summary>Gets the approximate number of seconds of battery time remaining.</summary>
		/// <returns>The approximate number of seconds of battery life remaining, or –1 if the approximate remaining battery life is unknown.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x000A3E9C File Offset: 0x000A209C
		public int BatteryLifeRemaining
		{
			get
			{
				return this.battery_life_remaining;
			}
		}

		/// <summary>Gets the current system power status.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PowerLineStatus" /> values indicating the current system power status.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000A3EA4 File Offset: 0x000A20A4
		public PowerLineStatus PowerLineStatus
		{
			get
			{
				return this.power_line_status;
			}
		}

		// Token: 0x0400150C RID: 5388
		private BatteryChargeStatus battery_charge_status;

		// Token: 0x0400150D RID: 5389
		private int battery_full_lifetime;

		// Token: 0x0400150E RID: 5390
		private float battery_life_percent;

		// Token: 0x0400150F RID: 5391
		private int battery_life_remaining;

		// Token: 0x04001510 RID: 5392
		private PowerLineStatus power_line_status;
	}
}
