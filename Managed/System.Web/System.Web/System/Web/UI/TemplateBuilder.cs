using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in building a template and the child controls it contains.</summary>
	// Token: 0x0200022E RID: 558
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateBuilder : ControlBuilder, ITemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</summary>
		// Token: 0x060016F0 RID: 5872 RVA: 0x0002B246 File Offset: 0x00029446
		public TemplateBuilder()
		{
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0003D948 File Offset: 0x0003BB48
		internal TemplateBuilder(ICustomAttributeProvider prov)
		{
			object[] array = prov.GetCustomAttributes(typeof(TemplateContainerAttribute), true);
			if (array.Length != 0)
			{
				this.containerAttribute = (TemplateContainerAttribute)array[0];
			}
			array = prov.GetCustomAttributes(typeof(TemplateInstanceAttribute), true);
			if (array.Length != 0)
			{
				this.instanceAttribute = (TemplateInstanceAttribute)array[0];
			}
		}

		/// <summary>Gets or sets the text between the opening and closing tags of the template.</summary>
		/// <returns>The text that appears between the opening and closing tags of the template.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0003D9A3 File Offset: 0x0003BBA3
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x0003D9AB File Offset: 0x0003BBAB
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
		internal Type ContainerType
		{
			get
			{
				if (this.containerAttribute == null)
				{
					return null;
				}
				return this.containerAttribute.ContainerType;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0003D9CC File Offset: 0x0003BBCC
		internal TemplateInstance? TemplateInstance
		{
			get
			{
				if (this.instanceAttribute == null)
				{
					return null;
				}
				return new TemplateInstance?(this.instanceAttribute.Instances);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0003D9FB File Offset: 0x0003BBFB
		internal BindingDirection BindingDirection
		{
			get
			{
				if (this.containerAttribute == null)
				{
					return BindingDirection.TwoWay;
				}
				return this.containerAttribute.BindingDirection;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0003DA12 File Offset: 0x0003BC12
		internal void RegisterBoundProperty(Type controlType, string controlProperty, string controlId, string fieldName)
		{
			if (this.bindings == null)
			{
				this.bindings = new List<TemplateBinding>();
			}
			this.bindings.Add(new TemplateBinding(controlType, controlProperty, controlId, fieldName));
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		internal ICollection Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		/// <summary>Used during design time to build the template and its child controls. </summary>
		/// <returns>A reference to the instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</returns>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0003DA44 File Offset: 0x0003BC44
		public override object BuildObject()
		{
			return base.BuildObject();
		}

		/// <summary>Initializes the template builder when a Web request is made.</summary>
		/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control. </param>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> responsible for building the control. </param>
		/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create. </param>
		/// <param name="tagName">The name of the tag to build. This allows the builder to support multiple tag types. </param>
		/// <param name="ID">The <see cref="P:System.Web.UI.ControlBuilder.ID" /> assigned to the control. </param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> that holds all the specified tag attributes. </param>
		// Token: 0x060016FA RID: 5882 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			if (parser != null)
			{
				base.FileName = parser.InputFile;
			}
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
		}

		/// <summary>Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to in design time.</summary>
		/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to contain the instances of controls from the inline template.</param>
		// Token: 0x060016FB RID: 5883 RVA: 0x0003DA6C File Offset: 0x0003BC6C
		public virtual void InstantiateIn(Control container)
		{
			this.CreateChildren(container);
		}

		/// <summary>Determines if the control builder needs to get its inner text.</summary>
		/// <returns>true if the control builder needs to get its inner text. The default is false.</returns>
		// Token: 0x060016FC RID: 5884 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool NeedsTagInnerText()
		{
			return false;
		}

		/// <summary>Saves the inner text of the template tag.</summary>
		/// <param name="text">The inner text of the template.</param>
		// Token: 0x060016FD RID: 5885 RVA: 0x0003D9AB File Offset: 0x0003BBAB
		public override void SetTagInnerText(string text)
		{
			this.text = text;
		}

		// Token: 0x04001588 RID: 5512
		private string text;

		// Token: 0x04001589 RID: 5513
		private TemplateContainerAttribute containerAttribute;

		// Token: 0x0400158A RID: 5514
		private TemplateInstanceAttribute instanceAttribute;

		// Token: 0x0400158B RID: 5515
		private List<TemplateBinding> bindings;
	}
}
