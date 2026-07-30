using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x020002CC RID: 716
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyVersionAttribute class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly. </param>
		// Token: 0x06002035 RID: 8245 RVA: 0x0007DEED File Offset: 0x0007C0ED
		public AssemblyVersionAttribute(string version)
		{
			this.m_version = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0007DEFC File Offset: 0x0007C0FC
		public string Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x0400116C RID: 4460
		private string m_version;
	}
}
