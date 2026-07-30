using System;

namespace System.Web.Hosting
{
	/// <summary>Creates and stops application domains for a Web-application manager. This class cannot be inherited.</summary>
	// Token: 0x02000547 RID: 1351
	public sealed class AppManagerAppDomainFactory : IAppManagerAppDomainFactory
	{
		/// <summary>Creates a new application domain for the specified Web application.</summary>
		/// <returns>A new application domain for the specified Web application.</returns>
		/// <param name="appId">The unique identifier for the new Web application.</param>
		/// <param name="appPath">The path to the new Web application's files.</param>
		// Token: 0x06003A8D RID: 14989 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object Create(string appId, string appPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Stops all application domains associated with this application manager. </summary>
		// Token: 0x06003A8E RID: 14990 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Stop()
		{
		}
	}
}
