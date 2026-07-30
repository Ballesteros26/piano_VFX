using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Instructs a <see cref="T:System.Resources.ResourceManager" /> object to ask for a particular version of a satellite assembly.</summary>
	// Token: 0x020002B2 RID: 690
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> class.</summary>
		/// <param name="version">A string that specifies the version of the satellite assemblies to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="version" /> parameter is null. </exception>
		// Token: 0x06001FAB RID: 8107 RVA: 0x0007CAB0 File Offset: 0x0007ACB0
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		/// <summary>Gets the version of the satellite assemblies with the required resources.</summary>
		/// <returns>A string that contains the version of the satellite assemblies with the required resources.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x0007CACD File Offset: 0x0007ACCD
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x04001126 RID: 4390
		private string _version;
	}
}
