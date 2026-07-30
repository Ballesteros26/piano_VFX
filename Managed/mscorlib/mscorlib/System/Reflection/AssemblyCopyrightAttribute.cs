using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a copyright custom attribute for an assembly manifest.</summary>
	// Token: 0x020002C1 RID: 705
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCopyrightAttribute" /> class.</summary>
		/// <param name="copyright">The copyright information. </param>
		// Token: 0x0600201F RID: 8223 RVA: 0x0007DDE2 File Offset: 0x0007BFE2
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.m_copyright = copyright;
		}

		/// <summary>Gets copyright information.</summary>
		/// <returns>A string containing the copyright information.</returns>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0007DDF1 File Offset: 0x0007BFF1
		public string Copyright
		{
			get
			{
				return this.m_copyright;
			}
		}

		// Token: 0x04001161 RID: 4449
		private string m_copyright;
	}
}
