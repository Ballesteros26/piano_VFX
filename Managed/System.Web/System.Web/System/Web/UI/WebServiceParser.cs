using System;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides a parser for Web service handlers. </summary>
	// Token: 0x02000250 RID: 592
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebServiceParser : SimpleWebHandlerParser
	{
		// Token: 0x06001826 RID: 6182 RVA: 0x00040E42 File Offset: 0x0003F042
		private WebServiceParser(HttpContext context, string virtualPath, string physicalPath)
			: base(context, virtualPath, physicalPath)
		{
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00040EF3 File Offset: 0x0003F0F3
		internal WebServiceParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
			: this(context, virtualPath, null, reader)
		{
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00040E59 File Offset: 0x0003F059
		internal WebServiceParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
			: base(context, virtualPath.Original, physicalPath, reader)
		{
		}

		/// <summary>Returns the compiled type for a given input file.</summary>
		/// <returns>A <see cref="T:System.Type" /> as specified by the <see cref="T:System.Web.HttpContext" />.</returns>
		/// <param name="inputFile">The file to be compiled. </param>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object for the current request. </param>
		// Token: 0x06001829 RID: 6185 RVA: 0x0003597A File Offset: 0x00033B7A
		public static Type GetCompiledType(string inputFile, HttpContext context)
		{
			return BuildManager.GetCompiledType(inputFile);
		}

		/// <summary>Gets the default directive name.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the default directive name.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00040EFF File Offset: 0x0003F0FF
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webservice";
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebServiceParser()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
