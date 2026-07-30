using System;

namespace System.Configuration
{
	/// <summary>Specifies that a settings provider should disable any logic that gets invoked when an application upgrade is detected. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NoSettingsVersionUpgradeAttribute : Attribute
	{
	}
}
