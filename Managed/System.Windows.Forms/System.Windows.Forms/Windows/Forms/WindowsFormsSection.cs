using System;
using System.Configuration;

namespace System.Windows.Forms
{
	/// <summary>Defines a new <see cref="T:System.Configuration.ConfigurationSection" /> for parsing application settings. This class cannot be inherited. </summary>
	// Token: 0x020003C3 RID: 963
	public sealed class WindowsFormsSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WindowsFormsSection" /> class. </summary>
		// Token: 0x0600456F RID: 17775 RVA: 0x0010EEB8 File Offset: 0x0010D0B8
		public WindowsFormsSection()
		{
			this.properties = new ConfigurationPropertyCollection();
			this.jit_debugging = new ConfigurationProperty("jitDebugging", typeof(bool), false);
			this.properties.Add(this.jit_debugging);
		}

		/// <summary>Gets or sets a value indicating whether just-in-time (JIT) debugging is used.</summary>
		/// <returns>true if JIT debugging is used; otherwise, false.</returns>
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x0010EF08 File Offset: 0x0010D108
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x0010EF1C File Offset: 0x0010D11C
		[ConfigurationProperty("jitDebugging", DefaultValue = "False")]
		public bool JitDebugging
		{
			get
			{
				return (bool)base[this.jit_debugging];
			}
			set
			{
				base[this.jit_debugging] = value;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x0010EF30 File Offset: 0x0010D130
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001D48 RID: 7496
		private ConfigurationPropertyCollection properties;

		// Token: 0x04001D49 RID: 7497
		private ConfigurationProperty jit_debugging;
	}
}
