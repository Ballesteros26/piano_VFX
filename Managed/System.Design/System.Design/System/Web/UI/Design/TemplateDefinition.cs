using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods that define a template element in a Web server control at design time.</summary>
	// Token: 0x020000A2 RID: 162
	public class TemplateDefinition : DesignerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, and property name.</summary>
		/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
		/// <param name="name">The name of the template.</param>
		/// <param name="templatedObject">The object that contains the template.</param>
		/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null,-or-<paramref name="templatedObject" /> is null.</exception>
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000903A File Offset: 0x0000723A
		[MonoNotSupported("")]
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName)
			: base(designer, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, and whether to limit the template contents to Web server controls.</summary>
		/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
		/// <param name="name">The name of the template.</param>
		/// <param name="templatedObject">The object that contains the template.</param>
		/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
		/// <param name="serverControlsOnly">A Boolean value indicating whether the template content should allow only Web server controls.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null,-or-<paramref name="templatedObject" /> is null.</exception>
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000903A File Offset: 0x0000723A
		[MonoNotSupported("")]
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, bool serverControlsOnly)
			: base(designer, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, and <see cref="T:System.Web.UI.WebControls.Style" /> object.</summary>
		/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
		/// <param name="name">The name of the template.</param>
		/// <param name="templatedObject">The object that contains the template.</param>
		/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> object to apply to each template.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null,-or-<paramref name="templatedObject" /> is null.</exception>
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000903A File Offset: 0x0000723A
		[MonoNotSupported("")]
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style)
			: base(designer, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, <see cref="T:System.Web.UI.WebControls.Style" /> object, and whether to limit content to Web server controls.</summary>
		/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
		/// <param name="name">The name of the template.</param>
		/// <param name="templatedObject">The object that contains the template.</param>
		/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> object to apply to each template.</param>
		/// <param name="serverControlsOnly">A Boolean value indicating whether the template should limit content to Web server controls.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null,-or-<paramref name="templatedObject" /> is null.</exception>
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000903A File Offset: 0x0000723A
		[MonoNotSupported("")]
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style, bool serverControlsOnly)
			: base(designer, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether the template should enable editing of its contents.</summary>
		/// <returns>true if editing is allowed; otherwise, false. The default is true.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool AllowEditing
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the HTML markup representing the content of the template.</summary>
		/// <returns>HTML markup for the content of the template.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string Content
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves a value indicating whether the template should limit content to Web server controls, as set in the <see cref="Overload:System.Web.UI.Design.TemplateDefinition.#ctor" /> constructor. This property is read-only.</summary>
		/// <returns>true if content is limited to Web server controls; otherwise, false.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool ServerControlsOnly
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the style that should be applied to the template as set in the <see cref="Overload:System.Web.UI.Design.TemplateDefinition.#ctor" /> constructor. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public Style Style
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves or sets a value indicating whether the template supports data binding.</summary>
		/// <returns>true if the template supports data binding; otherwise, false.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool SupportsDataBinding
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the component in which the template resides. This property is read-only.</summary>
		/// <returns>The component as set when this <see cref="T:System.Web.UI.Design.TemplateDefinition" /> was created.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public object TemplatedObject
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the property name for the template that the design host should display in the property grid.</summary>
		/// <returns>The name of the template as it should appear in the Properties list of the design host.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string TemplatePropertyName
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
