using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Maps configuration file virtual paths to physical paths.</summary>
	// Token: 0x02000781 RID: 1921
	public class UserMapPath : IConfigMapPath
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UserMapPath" /> class.</summary>
		/// <param name="fileMap">The configuration file mapping for the machine configuration file.</param>
		// Token: 0x06004E14 RID: 19988 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public UserMapPath(ConfigurationFileMap fileMap)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the virtual directory name that is associated with a specific site.</summary>
		/// <returns>The <paramref name="siteID" /> value must be unique. The <paramref name="siteID" /> value distinguishes sites that have the same name.</returns>
		/// <param name="siteID">A unique identifier for the site.</param>
		/// <param name="path">The URL that is associated with the site.</param>
		// Token: 0x06004E15 RID: 19989 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetAppPathForPath(string siteID, string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Populates the default site name and the site ID.</summary>
		/// <param name="siteName">The name of the default site.</param>
		/// <param name="siteID">A unique identifier for the site.</param>
		// Token: 0x06004E16 RID: 19990 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the machine-configuration file name.</summary>
		/// <returns>The machine-configuration file name.</returns>
		// Token: 0x06004E17 RID: 19991 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetMachineConfigFilename()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Populates the directory and name of the configuration file based on the site ID and site path.</summary>
		/// <param name="siteID">A unique identifier for the site.</param>
		/// <param name="path">The URL that is associated with the site.</param>
		/// <param name="directory">The physical path of the configuration file.</param>
		/// <param name="baseName">The name of the configuration file.</param>
		// Token: 0x06004E18 RID: 19992 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the physical path of the configuration file that is at the Web application root.</summary>
		/// <returns>The physical path of the Web.config file at the Web application root.</returns>
		// Token: 0x06004E19 RID: 19993 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetRootWebConfigFilename()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the physical path based on the site ID and on the URL that is associated with the site.</summary>
		/// <returns>The physical path of the site.</returns>
		/// <param name="siteID">A unique identifier for the site.</param>
		/// <param name="path">The URL that is associated with the site.</param>
		// Token: 0x06004E1A RID: 19994 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string MapPath(string siteID, string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Populates the site name and site ID based on a site argument value.</summary>
		/// <param name="siteArgument">The site name or site identifier.</param>
		/// <param name="siteName">The default site name.</param>
		/// <param name="siteID">A unique identifier for the site.</param>
		// Token: 0x06004E1B RID: 19995 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
