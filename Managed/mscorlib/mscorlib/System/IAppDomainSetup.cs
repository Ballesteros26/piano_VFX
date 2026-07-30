using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000181 RID: 385
	[Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAppDomainSetup
	{
		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>A <see cref="T:System.String" /> containg the name of the application base directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001077 RID: 4215
		// (set) Token: 0x06001078 RID: 4216
		string ApplicationBase { get; set; }

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001079 RID: 4217
		// (set) Token: 0x0600107A RID: 4218
		string ApplicationName { get; set; }

		/// <summary>Gets and sets the name of an area specific to the application where files are shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the fully-qualified name of the directory path and file name where files are shadow copied.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600107B RID: 4219
		// (set) Token: 0x0600107C RID: 4220
		string CachePath { get; set; }

		/// <summary>Gets and sets the name of the configuration file for an application domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the configuration file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600107D RID: 4221
		// (set) Token: 0x0600107E RID: 4222
		string ConfigurationFile { get; set; }

		/// <summary>Gets or sets the directory where dynamically generated files are stored and accessed.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the directory containing dynamic assemblies.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600107F RID: 4223
		// (set) Token: 0x06001080 RID: 4224
		string DynamicBase { get; set; }

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the license file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001081 RID: 4225
		// (set) Token: 0x06001082 RID: 4226
		string LicenseFile { get; set; }

		/// <summary>Gets or sets the list of directories that is combined with the <see cref="P:System.AppDomainSetup.ApplicationBase" /> directory to probe for private assemblies.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001083 RID: 4227
		// (set) Token: 0x06001084 RID: 4228
		string PrivateBinPath { get; set; }

		/// <summary>Gets or sets the private binary directory path used to locate an application.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001085 RID: 4229
		// (set) Token: 0x06001086 RID: 4230
		string PrivateBinPathProbe { get; set; }

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001087 RID: 4231
		// (set) Token: 0x06001088 RID: 4232
		string ShadowCopyDirectories { get; set; }

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001089 RID: 4233
		// (set) Token: 0x0600108A RID: 4234
		string ShadowCopyFiles { get; set; }
	}
}
