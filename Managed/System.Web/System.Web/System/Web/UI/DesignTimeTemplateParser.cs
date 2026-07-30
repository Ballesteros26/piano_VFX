using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides parsing at design time.</summary>
	// Token: 0x020001CA RID: 458
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public static class DesignTimeTemplateParser
	{
		/// <summary>Parses and builds one control at design time.</summary>
		/// <returns>The built <see cref="T:System.Web.UI.Control" /> object.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x060012B3 RID: 4787 RVA: 0x00033048 File Offset: 0x00031248
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static Control ParseControl(DesignTimeParseData data)
		{
			TemplateParser templateParser = DesignTimeTemplateParser.InitParser(data);
			templateParser.RootBuilder.Text = data.ParseText;
			if (templateParser.RootBuilder.Children == null)
			{
				return null;
			}
			using (IEnumerator enumerator = templateParser.RootBuilder.Children.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return (Control)((ControlBuilder)enumerator.Current).CreateInstance();
				}
			}
			return null;
		}

		/// <summary>Parses a template at design time.</summary>
		/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the template.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x060012B4 RID: 4788 RVA: 0x000330D8 File Offset: 0x000312D8
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static ITemplate ParseTemplate(DesignTimeParseData data)
		{
			TemplateParser templateParser = DesignTimeTemplateParser.InitParser(data);
			templateParser.RootBuilder.Text = data.ParseText;
			return templateParser.RootBuilder;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000330F6 File Offset: 0x000312F6
		[global::System.MonoTODO]
		private static TemplateParser InitParser(DesignTimeParseData data)
		{
			return new PageParser();
		}

		/// <summary>Parses and builds one or more controls at design time.</summary>
		/// <returns>An array of built <see cref="T:System.Web.UI.Control" /> objects.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x060012B6 RID: 4790 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static Control[] ParseControls(DesignTimeParseData data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Parses a theme at design time.</summary>
		/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the theme.</returns>
		/// <param name="host">Manages designer components.</param>
		/// <param name="theme">The content to parse.</param>
		/// <param name="themePath">The path to the theme, which is used in creating the parser.</param>
		/// <exception cref="T:System.Exception">An error occurred while parsing the theme.</exception>
		// Token: 0x060012B7 RID: 4791 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static ControlBuilder ParseTheme(IDesignerHost host, string theme, string themePath)
		{
			throw new NotImplementedException();
		}
	}
}
