using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Defines the counter type, name, and Help string for a custom counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E3 RID: 483
	[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of type NumberOfItems32, and with empty name and help strings.</summary>
		// Token: 0x06000F39 RID: 3897 RVA: 0x000469B1 File Offset: 0x00044BB1
		public CounterCreationData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of the specified type, using the specified counter name and Help strings.</summary>
		/// <param name="counterName">The name of the counter, which must be unique within its category. </param>
		/// <param name="counterHelp">The text that describes the counter's behavior. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that identifies the counter's behavior. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a value for <paramref name="counterType" /> that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="counterHelp" /> is null. </exception>
		// Token: 0x06000F3A RID: 3898 RVA: 0x000469C4 File Offset: 0x00044BC4
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
			this.CounterType = counterType;
		}

		/// <summary>Gets or sets the custom counter's description.</summary>
		/// <returns>The text that describes the counter's behavior.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x000469EC File Offset: 0x00044BEC
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x000469F4 File Offset: 0x00044BF4
		[DefaultValue("")]
		[MonitoringDescription("Description of this counter.")]
		public string CounterHelp
		{
			get
			{
				return this.help;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.help = value;
			}
		}

		/// <summary>Gets or sets the name of the custom counter.</summary>
		/// <returns>A name for the counter, which is unique in its category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is null.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value is not between 1 and 80 characters long or contains double quotes, control characters or leading or trailing spaces.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00046A0B File Offset: 0x00044C0B
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x00046A13 File Offset: 0x00044C13
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[MonitoringDescription("Name of this counter.")]
		public string CounterName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "")
				{
					throw new ArgumentException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the performance counter type of the custom counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that defines the behavior of the performance counter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a type that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00046A42 File Offset: 0x00044C42
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x00046A4A File Offset: 0x00044C4A
		[MonitoringDescription("Type of this counter.")]
		[DefaultValue(typeof(PerformanceCounterType), "NumberOfItems32")]
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
				{
					throw new InvalidEnumArgumentException();
				}
				this.type = value;
			}
		}

		// Token: 0x04001106 RID: 4358
		private string help = string.Empty;

		// Token: 0x04001107 RID: 4359
		private string name;

		// Token: 0x04001108 RID: 4360
		private PerformanceCounterType type;
	}
}
