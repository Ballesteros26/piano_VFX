using System;

namespace System.Configuration
{
	/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000190 RID: 400
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
		/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003C8A6 File Offset: 0x0003AAA6
		public SettingsGroupNameAttribute(string groupName)
		{
			this.group_name = groupName;
		}

		/// <summary>Gets the name of the application settings property group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0003C8B5 File Offset: 0x0003AAB5
		public string GroupName
		{
			get
			{
				return this.group_name;
			}
		}

		// Token: 0x04000FE1 RID: 4065
		private string group_name;
	}
}
