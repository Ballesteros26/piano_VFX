using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000194 RID: 404
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested. </param>
		// Token: 0x06000BFA RID: 3066 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this.manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical OR operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0003C8E3 File Offset: 0x0003AAE3
		public SettingsManageability Manageability
		{
			get
			{
				return this.manageability;
			}
		}

		// Token: 0x04000FE5 RID: 4069
		private SettingsManageability manageability;
	}
}
