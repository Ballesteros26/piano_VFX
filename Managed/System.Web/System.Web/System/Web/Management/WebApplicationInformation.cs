using System;

namespace System.Web.Management
{
	/// <summary>Provides information associated with health events.</summary>
	// Token: 0x0200052F RID: 1327
	public sealed class WebApplicationInformation
	{
		// Token: 0x06003A35 RID: 14901 RVA: 0x00002050 File Offset: 0x00000250
		internal WebApplicationInformation()
		{
		}

		/// <summary>Gets the current application domain name.</summary>
		/// <returns>Gets the application domain name.</returns>
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x0009D66C File Offset: 0x0009B86C
		public string ApplicationDomain
		{
			get
			{
				return this.application_domain;
			}
		}

		/// <summary>Gets the application physical path.</summary>
		/// <returns>The application physical path.</returns>
		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x0009D674 File Offset: 0x0009B874
		public string ApplicationPath
		{
			get
			{
				return this.application_path;
			}
		}

		/// <summary>Gets the application logical path.</summary>
		/// <returns>The application logical path.</returns>
		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x0009D67C File Offset: 0x0009B87C
		public string ApplicationVirtualPath
		{
			get
			{
				return this.application_virtual_path;
			}
		}

		/// <summary>Gets the application machine name.</summary>
		/// <returns>The name of the machine where the application is running.</returns>
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x0009D684 File Offset: 0x0009B884
		public string MachineName
		{
			get
			{
				return this.machine_name;
			}
		}

		/// <summary>Gets the application trust level.</summary>
		/// <returns>The application trust level.</returns>
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x0009D68C File Offset: 0x0009B88C
		public string TrustLevel
		{
			get
			{
				return this.trust_level;
			}
		}

		/// <summary>Formats the application information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06003A3B RID: 14907 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void FormatToString(WebEventFormatter formatter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x06003A3C RID: 14908 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F72 RID: 8050
		private string application_domain;

		// Token: 0x04001F73 RID: 8051
		private string application_path;

		// Token: 0x04001F74 RID: 8052
		private string application_virtual_path;

		// Token: 0x04001F75 RID: 8053
		private string machine_name;

		// Token: 0x04001F76 RID: 8054
		private string trust_level;
	}
}
