using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the version of the .NET Framework that a particular assembly was compiled against.</summary>
	// Token: 0x020006C5 RID: 1733
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> class by specifying the .NET Framework version against which an assembly was built.</summary>
		/// <param name="frameworkName">The version of the .NET Framework against which the assembly was built.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is null.</exception>
		// Token: 0x0600498B RID: 18827 RVA: 0x00107F71 File Offset: 0x00106171
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		/// <summary>Gets the name of the .NET Framework version against which a particular assembly was compiled.</summary>
		/// <returns>The name of the .NET Framework version with which the assembly was compiled.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x00107F8E File Offset: 0x0010618E
		public string FrameworkName
		{
			get
			{
				return this._frameworkName;
			}
		}

		/// <summary>Gets the display name of the .NET Framework version against which an assembly was built.</summary>
		/// <returns>The display name of the .NET Framework version.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x00107F96 File Offset: 0x00106196
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x00107F9E File Offset: 0x0010619E
		public string FrameworkDisplayName
		{
			get
			{
				return this._frameworkDisplayName;
			}
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x0400269B RID: 9883
		private string _frameworkName;

		// Token: 0x0400269C RID: 9884
		private string _frameworkDisplayName;
	}
}
