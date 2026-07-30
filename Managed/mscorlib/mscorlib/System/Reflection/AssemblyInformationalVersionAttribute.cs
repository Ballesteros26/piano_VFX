using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines additional version information for an assembly manifest.</summary>
	// Token: 0x020002C9 RID: 713
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> class.</summary>
		/// <param name="informationalVersion">The assembly version information. </param>
		// Token: 0x0600202F RID: 8239 RVA: 0x0007DE9A File Offset: 0x0007C09A
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.m_informationalVersion = informationalVersion;
		}

		/// <summary>Gets version information.</summary>
		/// <returns>A string containing the version information.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0007DEA9 File Offset: 0x0007C0A9
		public string InformationalVersion
		{
			get
			{
				return this.m_informationalVersion;
			}
		}

		// Token: 0x04001169 RID: 4457
		private string m_informationalVersion;
	}
}
