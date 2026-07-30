using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed assembly is a primary interop assembly.</summary>
	// Token: 0x020008CC RID: 2252
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> class with the major and minor version numbers of the type library for which this assembly is the primary interop assembly.</summary>
		/// <param name="major">The major version of the type library for which this assembly is the primary interop assembly. </param>
		/// <param name="minor">The minor version of the type library for which this assembly is the primary interop assembly. </param>
		// Token: 0x0600552A RID: 21802 RVA: 0x00128956 File Offset: 0x00126B56
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The major version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x0012896C File Offset: 0x00126B6C
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The minor version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x00128974 File Offset: 0x00126B74
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002CAA RID: 11434
		internal int _major;

		// Token: 0x04002CAB RID: 11435
		internal int _minor;
	}
}
