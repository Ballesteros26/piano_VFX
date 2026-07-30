using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x020002C6 RID: 710
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title. </param>
		// Token: 0x06002029 RID: 8233 RVA: 0x0007DE55 File Offset: 0x0007C055
		public AssemblyTitleAttribute(string title)
		{
			this.m_title = title;
		}

		/// <summary>Gets assembly title information.</summary>
		/// <returns>The assembly title. </returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0007DE64 File Offset: 0x0007C064
		public string Title
		{
			get
			{
				return this.m_title;
			}
		}

		// Token: 0x04001166 RID: 4454
		private string m_title;
	}
}
