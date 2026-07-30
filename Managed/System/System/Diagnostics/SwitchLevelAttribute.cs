using System;

namespace System.Diagnostics
{
	/// <summary>Identifies the level type for a switch. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001BF RID: 447
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchLevelAttribute" /> class, specifying the type that determines whether a trace should be written.</summary>
		/// <param name="switchLevelType">The <see cref="T:System.Type" /> that determines whether a trace should be written.</param>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0003F655 File Offset: 0x0003D855
		public SwitchLevelAttribute(Type switchLevelType)
		{
			this.SwitchLevelType = switchLevelType;
		}

		/// <summary>Gets or sets the type that determines whether a trace should be written.</summary>
		/// <returns>The <see cref="T:System.Type" /> that determines whether a trace should be written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The set operation failed because the value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0003F664 File Offset: 0x0003D864
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0003F66C File Offset: 0x0003D86C
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x04001045 RID: 4165
		private Type type;
	}
}
