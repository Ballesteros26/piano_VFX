using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides methods for creating a Web server <see cref="T:System.Web.UI.Control" /> control or <see cref="T:System.Web.UI.ITemplate" /> interface from a string of markup that represents a persisted control or template.</summary>
	// Token: 0x0200005D RID: 93
	public sealed class ControlParser
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00002352 File Offset: 0x00000552
		private ControlParser()
		{
		}

		/// <summary>Creates a control from the specified markup using the specified designer host.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> that controlText represents; otherwise, null, if the parser cannot build the control.</returns>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page. </param>
		/// <param name="controlText">The HTML markup for the control. </param>
		/// <exception cref="T:System.ArgumentNullException">A parameter is not valid. </exception>
		// Token: 0x06000306 RID: 774 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static Control ParseControl(IDesignerHost designerHost, string controlText)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a control from the specified markup using the specified designer host and directives.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> that <paramref name="controlText" /> represents.</returns>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
		/// <param name="controlText">The text of the HTML markup for the control.</param>
		/// <param name="directives">The page directives to include in the code for the control.</param>
		/// <exception cref="T:System.ArgumentNullException">A parameter is not valid. </exception>
		// Token: 0x06000307 RID: 775 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static Control ParseControl(IDesignerHost designerHost, string controlText, string directives)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an array of controls from the specified markup using the specified designer host.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Control" /> elements, parsed from <paramref name="controlText" />; otherwise, null, if the parser cannot build the controls.</returns>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
		/// <param name="controlText">A string that represents a collection of markup for controls.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designerHost" /> is null.- or -<paramref name="controlText" /> is null or an empty string ("").</exception>
		// Token: 0x06000308 RID: 776 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static Control[] ParseControls(IDesignerHost designerHost, string controlText)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.ITemplate" /> interface from the specified template markup.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> instance created by parsing <paramref name="templateText" />.</returns>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page. </param>
		/// <param name="templateText">A string containing the template markup. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designerHost" /> is null.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText)
		{
			throw new NotImplementedException();
		}

		/// <summary>Parses the specified template markup and creates an <see cref="T:System.Web.UI.ITemplate" /> interface.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> instance created by parsing <paramref name="templateText" />.</returns>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page. </param>
		/// <param name="templateText">A string containing the template markup. </param>
		/// <param name="directives">Any directives to add to the beginning of the code for the template. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designerHost" /> is null.</exception>
		// Token: 0x0600030A RID: 778 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText, string directives)
		{
			throw new NotImplementedException();
		}
	}
}
