using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates to a COM client that all classes in the current version of an assembly are compatible with classes in an earlier version of the assembly.</summary>
	// Token: 0x020008D0 RID: 2256
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> class with the major version, minor version, build, and revision numbers of the assembly.</summary>
		/// <param name="major">The major version number of the assembly. </param>
		/// <param name="minor">The minor version number of the assembly. </param>
		/// <param name="build">The build number of the assembly. </param>
		/// <param name="revision">The revision number of the assembly. </param>
		// Token: 0x06005535 RID: 21813 RVA: 0x001289DF File Offset: 0x00126BDF
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		/// <summary>Gets the major version number of the assembly.</summary>
		/// <returns>The major version number of the assembly.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x00128A04 File Offset: 0x00126C04
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the assembly.</summary>
		/// <returns>The minor version number of the assembly.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x00128A0C File Offset: 0x00126C0C
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		/// <summary>Gets the build number of the assembly.</summary>
		/// <returns>The build number of the assembly.</returns>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x00128A14 File Offset: 0x00126C14
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		/// <summary>Gets the revision number of the assembly.</summary>
		/// <returns>The revision number of the assembly.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005539 RID: 21817 RVA: 0x00128A1C File Offset: 0x00126C1C
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002CB1 RID: 11441
		internal int _major;

		// Token: 0x04002CB2 RID: 11442
		internal int _minor;

		// Token: 0x04002CB3 RID: 11443
		internal int _build;

		// Token: 0x04002CB4 RID: 11444
		internal int _revision;
	}
}
