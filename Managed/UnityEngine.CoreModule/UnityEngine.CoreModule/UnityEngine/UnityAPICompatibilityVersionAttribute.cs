using System;

namespace UnityEngine
{
	// Token: 0x020001C3 RID: 451
	[AttributeUsage(1, AllowMultiple = false)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x00020E96 File Offset: 0x0001F096
		[Obsolete("This overload of the attribute has been deprecated. Use the constructor that takes the version and a boolean", true)]
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		public UnityAPICompatibilityVersionAttribute(string version, bool checkOnlyUnityVersion)
		{
			bool flag = !checkOnlyUnityVersion;
			if (flag)
			{
				throw new ArgumentException("You must pass 'true' to checkOnlyUnityVersion parameter.");
			}
			this._version = version;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00020ED7 File Offset: 0x0001F0D7
		public UnityAPICompatibilityVersionAttribute(string version, string[] configurationAssembliesHashes)
		{
			this._version = version;
			this._configurationAssembliesHashes = configurationAssembliesHashes;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00020F08 File Offset: 0x0001F108
		internal string[] configurationAssembliesHashes
		{
			get
			{
				return this._configurationAssembliesHashes;
			}
		}

		// Token: 0x0400066A RID: 1642
		private string _version;

		// Token: 0x0400066B RID: 1643
		private string[] _configurationAssembliesHashes;
	}
}
