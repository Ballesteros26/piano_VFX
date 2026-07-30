using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides a text description for an assembly.</summary>
	// Token: 0x020002C5 RID: 709
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> class.</summary>
		/// <param name="description">The assembly description. </param>
		// Token: 0x06002027 RID: 8231 RVA: 0x0007DE3E File Offset: 0x0007C03E
		public AssemblyDescriptionAttribute(string description)
		{
			this.m_description = description;
		}

		/// <summary>Gets assembly description information.</summary>
		/// <returns>A string containing the assembly description.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0007DE4D File Offset: 0x0007C04D
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x04001165 RID: 4453
		private string m_description;
	}
}
