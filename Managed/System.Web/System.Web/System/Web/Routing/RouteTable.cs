using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Stores the URL routes for an application.</summary>
	// Token: 0x020004F7 RID: 1271
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteTable
	{
		/// <summary>Gets a collection of objects that derive from the <see cref="T:System.Web.Routing.RouteBase" /> class.</summary>
		/// <returns>An object that contains all the routes in the collection.</returns>
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x00099808 File Offset: 0x00097A08
		public static RouteCollection Routes
		{
			get
			{
				return RouteTable._instance;
			}
		}

		// Token: 0x04001F05 RID: 7941
		private static RouteCollection _instance = new RouteCollection();
	}
}
