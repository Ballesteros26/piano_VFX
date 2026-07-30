using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x020002C7 RID: 711
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration. </param>
		// Token: 0x0600202B RID: 8235 RVA: 0x0007DE6C File Offset: 0x0007C06C
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.m_configuration = configuration;
		}

		/// <summary>Gets assembly configuration information.</summary>
		/// <returns>A string containing the assembly configuration information.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0007DE7B File Offset: 0x0007C07B
		public string Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x04001167 RID: 4455
		private string m_configuration;
	}
}
