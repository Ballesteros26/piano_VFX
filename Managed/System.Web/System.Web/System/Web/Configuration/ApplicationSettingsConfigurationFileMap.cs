using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000580 RID: 1408
	internal class ApplicationSettingsConfigurationFileMap : ConfigurationFileMap
	{
		// Token: 0x06003B7F RID: 15231 RVA: 0x0009F4FC File Offset: 0x0009D6FC
		public ApplicationSettingsConfigurationFileMap()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest != null)
			{
				base.MachineConfigFilename = WebConfigurationHost.GetWebConfigFileName(httpRequest.MapPath(WebConfigurationManager.FindWebConfig(httpRequest.CurrentExecutionFilePath)));
				return;
			}
			base.MachineConfigFilename = null;
		}
	}
}
