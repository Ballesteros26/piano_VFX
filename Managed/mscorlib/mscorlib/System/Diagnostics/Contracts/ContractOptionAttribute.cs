using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables you to set contract and tool options at assembly, type, or method granularity.</summary>
	// Token: 0x02000A88 RID: 2696
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public sealed class ContractOptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and enable/disable value.</summary>
		/// <param name="category">The category for the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="enabled">true to enable the option; false to disable the option.</param>
		// Token: 0x06006222 RID: 25122 RVA: 0x00140C2A File Offset: 0x0013EE2A
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and value.</summary>
		/// <param name="category">The category of the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="value">The value for the setting.</param>
		// Token: 0x06006223 RID: 25123 RVA: 0x00140C47 File Offset: 0x0013EE47
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		/// <summary>Gets the category of the option.</summary>
		/// <returns>The category of the option.</returns>
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x00140C64 File Offset: 0x0013EE64
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		/// <summary>Gets the setting for the option.</summary>
		/// <returns>The setting for the option.</returns>
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x00140C6C File Offset: 0x0013EE6C
		public string Setting
		{
			get
			{
				return this._setting;
			}
		}

		/// <summary>Determines if an option is enabled.</summary>
		/// <returns>true if the option is enabled; otherwise, false.</returns>
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x00140C74 File Offset: 0x0013EE74
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		/// <summary>Gets the value for the option.</summary>
		/// <returns>The value for the option.</returns>
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x00140C7C File Offset: 0x0013EE7C
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040030EE RID: 12526
		private string _category;

		// Token: 0x040030EF RID: 12527
		private string _setting;

		// Token: 0x040030F0 RID: 12528
		private bool _enabled;

		// Token: 0x040030F1 RID: 12529
		private string _value;
	}
}
