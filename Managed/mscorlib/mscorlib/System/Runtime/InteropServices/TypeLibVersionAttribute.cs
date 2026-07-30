using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the version number of an exported type library.</summary>
	// Token: 0x020008CF RID: 2255
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVersionAttribute" /> class with the major and minor version numbers of the type library.</summary>
		/// <param name="major">The major version number of the type library. </param>
		/// <param name="minor">The minor version number of the type library. </param>
		// Token: 0x06005532 RID: 21810 RVA: 0x001289B9 File Offset: 0x00126BB9
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library.</summary>
		/// <returns>The major version number of the type library.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x001289CF File Offset: 0x00126BCF
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library.</summary>
		/// <returns>The minor version number of the type library.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x001289D7 File Offset: 0x00126BD7
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002CAF RID: 11439
		internal int _major;

		// Token: 0x04002CB0 RID: 11440
		internal int _minor;
	}
}
