using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings group or property contains distinct values for each user of an application. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A6 RID: 422
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class UserScopedSettingAttribute : SettingAttribute
	{
	}
}
