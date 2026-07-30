using System;

namespace System.Web.Hosting
{
	/// <summary>Defines an interface that can be implemented in a type in order to preload the type in an ASP.NET application that is running on IIS 7.0. </summary>
	// Token: 0x0200053D RID: 1341
	public interface IProcessHostPreloadClient
	{
		/// <summary>Provides initialization data that is required in order to preload the application.</summary>
		/// <param name="parameters">Data to initialize the application. This parameter can be null or an empty array.</param>
		// Token: 0x06003A77 RID: 14967
		void Preload(string[] parameters);
	}
}
