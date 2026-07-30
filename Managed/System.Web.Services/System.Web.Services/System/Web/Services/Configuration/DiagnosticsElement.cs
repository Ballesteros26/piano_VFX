using System;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the &lt;diagnostics&gt; element in the Web.config configuration file.</summary>
	// Token: 0x0200013D RID: 317
	public sealed class DiagnosticsElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> class. </summary>
		// Token: 0x060009B4 RID: 2484 RVA: 0x00043570 File Offset: 0x00041770
		public DiagnosticsElement()
		{
			this.properties.Add(this.suppressReturningExceptions);
		}

		/// <summary>Gets or sets a value that indicates whether the service returns exceptions.</summary>
		/// <returns>true if the service returns exceptions; otherwise, false. The default is false.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000435BF File Offset: 0x000417BF
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000435D2 File Offset: 0x000417D2
		[ConfigurationProperty("suppressReturningExceptions", DefaultValue = false)]
		public bool SuppressReturningExceptions
		{
			get
			{
				return (bool)base[this.suppressReturningExceptions];
			}
			set
			{
				base[this.suppressReturningExceptions] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000435E6 File Offset: 0x000417E6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000598 RID: 1432
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000599 RID: 1433
		private readonly ConfigurationProperty suppressReturningExceptions = new ConfigurationProperty("suppressReturningExceptions", typeof(bool), false);
	}
}
