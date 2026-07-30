using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs a compiler to use a specific version number for the Win32 file version resource. The Win32 file version is not required to be the same as the assembly's version number.</summary>
	// Token: 0x020002CA RID: 714
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> class, specifying the file version.</summary>
		/// <param name="version">The file version. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		// Token: 0x06002031 RID: 8241 RVA: 0x0007DEB1 File Offset: 0x0007C0B1
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		/// <summary>Gets the Win32 file version resource name.</summary>
		/// <returns>A string containing the file version resource name.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0007DECE File Offset: 0x0007C0CE
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0400116A RID: 4458
		private string _version;
	}
}
