using System;

namespace System.Web.Services.Configuration
{
	/// <summary>Specifies the values that can be used to set the priority and group attributes for a SOAP extension in the Web Services configuration file.</summary>
	// Token: 0x0200013E RID: 318
	public enum PriorityGroup
	{
		/// <summary>Represents the value 1. Indicates that the SOAP extension executes in the group of SOAP extensions with the highest priority.</summary>
		// Token: 0x0400059B RID: 1435
		High,
		/// <summary>Represents the value 0. Indicates that the SOAP extension executes in the group of SOAP extensions with the lowest priority.</summary>
		// Token: 0x0400059C RID: 1436
		Low
	}
}
