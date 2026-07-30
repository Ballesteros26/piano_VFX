using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that types that are ordinarily visible only within the current assembly are visible to a specified assembly.</summary>
	// Token: 0x0200087B RID: 2171
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> class with the name of the specified friend assembly. </summary>
		/// <param name="assemblyName">The name of a friend assembly.</param>
		// Token: 0x06005460 RID: 21600 RVA: 0x0012764E File Offset: 0x0012584E
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		/// <summary>Gets the name of the friend assembly to which all types and type members that are marked with the internal keyword are to be made visible. </summary>
		/// <returns>A string that represents the name of the friend assembly.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x00127664 File Offset: 0x00125864
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		/// <summary>This property is not implemented.</summary>
		/// <returns>This property does not return a value.</returns>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0012766C File Offset: 0x0012586C
		// (set) Token: 0x06005463 RID: 21603 RVA: 0x00127674 File Offset: 0x00125874
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04002BC1 RID: 11201
		private string _assemblyName;

		// Token: 0x04002BC2 RID: 11202
		private bool _allInternalsVisible = true;
	}
}
