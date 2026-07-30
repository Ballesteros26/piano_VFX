using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the paths that are used to search for DLLs that provide functions for platform invokes. </summary>
	// Token: 0x020008C6 RID: 2246
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute" /> class, specifying the paths to use when searching for the targets of platform invokes. </summary>
		/// <param name="paths">A bitwise combination of enumeration values that specify the paths that the LoadLibraryEx function searches during platform invokes. </param>
		// Token: 0x06005515 RID: 21781 RVA: 0x00128638 File Offset: 0x00126838
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this._paths = paths;
		}

		/// <summary>Gets a bitwise combination of enumeration values that specify the paths that the LoadLibraryEx function searches during platform invokes. </summary>
		/// <returns>A bitwise combination of enumeration values that specify search paths for platform invokes. </returns>
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x00128647 File Offset: 0x00126847
		public DllImportSearchPath Paths
		{
			get
			{
				return this._paths;
			}
		}

		// Token: 0x04002C98 RID: 11416
		internal DllImportSearchPath _paths;
	}
}
