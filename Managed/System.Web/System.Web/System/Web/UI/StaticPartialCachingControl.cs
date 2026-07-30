using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.UserControl" /> class that is specified for output caching and included declaratively in a page or another user control.</summary>
	// Token: 0x0200022B RID: 555
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class StaticPartialCachingControl : BasePartialCachingControl
	{
		/// <summary>Creates the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> object to contain the cached server control content.</summary>
		/// <param name="ctrlID">The identifier assigned to the server control by ASP.NET. </param>
		/// <param name="guid">The globally unique identifier passed from the server control. </param>
		/// <param name="duration">The length of time the server control is to remain in the cache. </param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the user control in the cache. </param>
		/// <param name="varyByControls">A string of the server control properties by which to vary the user control in the cache. </param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="buildMethod">A delegate that calls the method to build the <see cref="T:System.Web.UI.StaticPartialCachingControl" />. </param>
		// Token: 0x060016E2 RID: 5858 RVA: 0x0003D7B9 File Offset: 0x0003B9B9
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, BuildMethod buildMethod)
		{
			base.CtrlID = ctrlID;
			base.Guid = guid;
			base.Duration = duration;
			base.VaryByParams = varyByParams;
			base.VaryByControls = varyByControls;
			base.VaryByCustom = varyByCustom;
			this.buildMethod = buildMethod;
		}

		/// <summary>Creates the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> object to contain the cached server control content.</summary>
		/// <param name="ctrlID">The identifier assigned to the server control by ASP.NET. </param>
		/// <param name="guid">The globally unique identifier passed from the server control. </param>
		/// <param name="duration">The length of time the server control is to remain in the cache. </param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the user control in the cache. </param>
		/// <param name="varyByControls">A string of the server control properties by which to vary the user control in the cache. </param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="sqlDependency">A semicolon-delimited string that specifies which databases and tables to use for the Microsoft SQL Server cache dependency.</param>
		/// <param name="buildMethod">A delegate that calls the method to build the <see cref="T:System.Web.UI.StaticPartialCachingControl" />. </param>
		// Token: 0x060016E3 RID: 5859 RVA: 0x0003D7F6 File Offset: 0x0003B9F6
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod)
			: this(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, buildMethod)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> class for a control that is defined in an .ascx file.</summary>
		/// <param name="ctrlID">The ID that is assigned to the control by ASP.NET.</param>
		/// <param name="guid">The globally unique identifier (GUID) for the cached control. </param>
		/// <param name="duration">The length of time that the control's output is cached.</param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the user control in the cache.</param>
		/// <param name="varyByControls">A string that lists the server-control properties by which to vary the user control in the cache.</param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="sqlDependency">A semicolon-delimited string that specifies which databases and tables to use for the Microsoft SQL Server cache dependency.</param>
		/// <param name="buildMethod">A delegate that calls the method that builds the control. </param>
		/// <param name="providerName">The name of the provider that is used to store the output-cached control. </param>
		// Token: 0x060016E4 RID: 5860 RVA: 0x0003D80C File Offset: 0x0003BA0C
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod, string providerName)
			: this(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod)
		{
			base.ProviderName = providerName;
		}

		/// <summary>Builds a <see cref="T:System.Web.UI.StaticPartialCachingControl" /> object with the parameters specified in the user control (.ascx file) and adds it as a parsed sub-object to the containing server control.</summary>
		/// <param name="parent">The server control to contain the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> instance. </param>
		/// <param name="ctrlID">The identifier assigned to the control by ASP.NET. </param>
		/// <param name="guid">The globally unique identifier for the cached control. </param>
		/// <param name="duration">The length of time the control's output is cached. </param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the server control in the cache. </param>
		/// <param name="varyByControls">A string that lists the server-control properties by which to vary the user control in the cache.</param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="sqlDependency">A semicolon-delimited string that specifies which databases and tables to use for the Microsoft SQL Server cache dependency.</param>
		/// <param name="buildMethod">A delegate that calls the method to build the control.</param>
		// Token: 0x060016E5 RID: 5861 RVA: 0x0003D834 File Offset: 0x0003BA34
		[global::System.MonoTODO("Consider sqlDependency parameter")]
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod)
		{
			StaticPartialCachingControl staticPartialCachingControl = new StaticPartialCachingControl(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod);
			if (parent != null)
			{
				parent.Controls.Add(staticPartialCachingControl);
			}
		}

		/// <summary>Builds a <see cref="T:System.Web.UI.StaticPartialCachingControl" /> object with the parameters specified in the user control (.ascx file) and adds it as a parsed sub-object to the containing server control.</summary>
		/// <param name="parent">The server control to contain the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> instance. </param>
		/// <param name="ctrlID">The identifier assigned to the control by ASP.NET. </param>
		/// <param name="guid">The globally unique identifier for the cached control. </param>
		/// <param name="duration">The length of time the control's output is cached. </param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the server control in the cache. </param>
		/// <param name="varyByControls">A string that lists the server-control properties by which to vary the user control in the cache.</param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="buildMethod">A delegate that calls the method to build the control. </param>
		// Token: 0x060016E6 RID: 5862 RVA: 0x0003D864 File Offset: 0x0003BA64
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, BuildMethod buildMethod)
		{
			StaticPartialCachingControl.BuildCachedControl(parent, ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, null, buildMethod);
		}

		/// <summary>Builds a new instance of the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> class, for a control that is defined in an .ascx file.</summary>
		/// <param name="parent">The server control that is used as the container for the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> instance.</param>
		/// <param name="ctrlID">The ID that is assigned to the control by ASP.NET.</param>
		/// <param name="guid">The globally unique identifier (GUID) for the cached control. </param>
		/// <param name="duration">The length of time that the control's output is cached.  </param>
		/// <param name="varyByParams">A string of the query string or form POST parameters by which to vary the user control in the cache.  </param>
		/// <param name="varyByControls">A string that lists the server-control properties by which to vary the user control in the cache. </param>
		/// <param name="varyByCustom">A user-defined string that contains custom output-cache parameter values.</param>
		/// <param name="sqlDependency">A semicolon-delimited string that specifies which databases and tables to use for the Microsoft SQL Server cache dependency.</param>
		/// <param name="buildMethod">A delegate that calls the method that builds the control. </param>
		/// <param name="providerName">The name of the provider that is used to store the output-cached control.  </param>
		// Token: 0x060016E7 RID: 5863 RVA: 0x0003D884 File Offset: 0x0003BA84
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod, string providerName)
		{
			StaticPartialCachingControl staticPartialCachingControl = new StaticPartialCachingControl(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod, providerName);
			if (parent != null)
			{
				parent.Controls.Add(staticPartialCachingControl);
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0003D8B5 File Offset: 0x0003BAB5
		internal override Control CreateControl()
		{
			return this.buildMethod();
		}

		// Token: 0x04001584 RID: 5508
		private BuildMethod buildMethod;
	}
}
