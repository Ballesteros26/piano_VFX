using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines the properties, methods, and events that are shared by all ASP.NET server controls.</summary>
	// Token: 0x020001B5 RID: 437
	[DesignerCategory("Code")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(true)]
	[Themeable(false)]
	[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require)]
	[DefaultProperty("ID")]
	[ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Control : IComponent, IDisposable, IParserAccessor, IDataBindingsAccessor, IUrlResolutionService, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0002E34C File Offset: 0x0002C54C
		static Control()
		{
			Control.DataBindingEvent = new object();
			Control.DisposedEvent = new object();
			Control.InitEvent = new object();
			Control.LoadEvent = new object();
			Control.PreRenderEvent = new object();
			Control.UnloadEvent = new object();
			Control.defaultNameArray = new string[100];
			for (int i = 0; i < 100; i++)
			{
				Control.defaultNameArray[i] = "ctl" + i.ToString("D2");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Control" /> class.</summary>
		// Token: 0x060010CA RID: 4298 RVA: 0x0002E3CB File Offset: 0x0002C5CB
		public Control()
		{
			this.stateMask = 55;
			if (this is INamingContainer)
			{
				this.stateMask |= 64;
			}
			this.viewStateMode = ViewStateMode.Inherit;
		}

		/// <summary>Gets the browser-specific adapter for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> for this control. If the target browser does not require an adapter, returns null.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0002E40B File Offset: 0x0002C60B
		protected internal ControlAdapter Adapter
		{
			get
			{
				if (!this.did_adapter_lookup)
				{
					this.adapter = this.ResolveAdapter();
					if (this.adapter != null)
					{
						this.adapter.control = this;
					}
					this.did_adapter_lookup = true;
				}
				return this.adapter;
			}
		}

		/// <summary>Gets or sets the application-relative virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> object that contains this control.</summary>
		/// <returns>The application-relative virtual directory of the page or user control that contains this control.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0002E444 File Offset: 0x0002C644
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string AppRelativeTemplateSourceDirectory
		{
			get
			{
				if (this._appRelativeTemplateSourceDirectory != null)
				{
					return this._appRelativeTemplateSourceDirectory;
				}
				string text = null;
				TemplateControl templateControl = this.TemplateControl;
				if (templateControl != null)
				{
					string appRelativeVirtualPath = templateControl.AppRelativeVirtualPath;
					if (!string.IsNullOrEmpty(appRelativeVirtualPath))
					{
						text = VirtualPathUtility.GetDirectory(appRelativeVirtualPath, false);
					}
				}
				this._appRelativeTemplateSourceDirectory = ((text != null) ? text : VirtualPathUtility.ToAppRelative(this.TemplateSourceDirectory));
				return this._appRelativeTemplateSourceDirectory;
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this._appRelativeTemplateSourceDirectory = value;
				this._templateSourceDirectory = null;
			}
		}

		/// <summary>Gets the control that contains this control's data binding.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> that contains this control's data binding.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0002E4B0 File Offset: 0x0002C6B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control BindingContainer
		{
			get
			{
				Control control = this.NamingContainer;
				if ((control != null && control is INonBindingContainer) || (this.stateMask & 16) == 0)
				{
					control = control.BindingContainer;
				}
				return control;
			}
		}

		/// <summary>Gets the control ID for HTML markup that is generated by ASP.NET.</summary>
		/// <returns>The control ID for HTML markup that is generated by ASP.NET.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0002E4E2 File Offset: 0x0002C6E2
		[WebSysDescription("An Identification of the control that is rendered.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ClientID
		{
			get
			{
				if (this.clientID != null)
				{
					return this.clientID;
				}
				this.clientID = this.GetClientID();
				this.stateMask |= 1024;
				return this.clientID;
			}
		}

		/// <summary>Gets a value that specifies the ASP.NET version that rendered HTML will be compatible with.</summary>
		/// <returns>The ASP.NET version that rendered HTML will be compatible with.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0002E518 File Offset: 0x0002C718
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x0002E561 File Offset: 0x0002C761
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public virtual Version RenderingCompatibility
		{
			get
			{
				if (this.renderingCompatibility == null)
				{
					PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
					this.renderingCompatibility = ((pagesSection != null) ? pagesSection.ControlRenderingCompatibilityVersion : new Version(4, 0));
				}
				return this.renderingCompatibility;
			}
			set
			{
				this.renderingCompatibility = value;
				this.renderingCompatibilityOld = null;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0002E576 File Offset: 0x0002C776
		internal bool RenderingCompatibilityLessThan40
		{
			get
			{
				if (this.renderingCompatibilityOld == null)
				{
					this.renderingCompatibilityOld = new bool?(this.RenderingCompatibility < new Version(4, 0));
				}
				return this.renderingCompatibilityOld.Value;
			}
		}

		/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataItemContainer" />.</summary>
		/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataItemContainer" />, this property returns the naming container at the top of the hierarchy, or null if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataItemContainer" />.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control DataItemContainer
		{
			get
			{
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return null;
				}
				if (namingContainer is IDataItemContainer)
				{
					return namingContainer;
				}
				return namingContainer.DataItemContainer;
			}
		}

		/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataKeysControl" />.</summary>
		/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataKeysControl" />, the property returns the naming container at the top of the hierarchy, or null if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataKeysControl" />.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0002E5DC File Offset: 0x0002C7DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public Control DataKeysContainer
		{
			get
			{
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return null;
				}
				if (namingContainer is IDataKeysControl)
				{
					return namingContainer;
				}
				return namingContainer.DataKeysContainer;
			}
		}

		/// <summary>Gets or sets the algorithm that is used to generate the value of the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
		/// <returns>A value that indicates how the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default is <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0002E605 File Offset: 0x0002C805
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0002E621 File Offset: 0x0002C821
		[DefaultValue(ClientIDMode.Inherit)]
		[Themeable(false)]
		public virtual ClientIDMode ClientIDMode
		{
			get
			{
				if (this.clientIDMode == null)
				{
					return ClientIDMode.Inherit;
				}
				return this.clientIDMode.Value;
			}
			set
			{
				if (this.clientIDMode == null || this.clientIDMode.Value != value)
				{
					this.ClearCachedClientID();
					this.ClearEffectiveClientIDMode();
					this.clientIDMode = new ClientIDMode?(value);
				}
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0002E658 File Offset: 0x0002C858
		internal ClientIDMode EffectiveClientIDMode
		{
			get
			{
				if (this.effectiveClientIDMode != null)
				{
					return this.effectiveClientIDMode.Value;
				}
				ClientIDMode clientIDMode = this.ClientIDMode;
				if (clientIDMode != ClientIDMode.Inherit)
				{
					this.effectiveClientIDMode = new ClientIDMode?(clientIDMode);
					return clientIDMode;
				}
				Control namingContainer = this.NamingContainer;
				if (namingContainer != null)
				{
					this.effectiveClientIDMode = new ClientIDMode?(namingContainer.EffectiveClientIDMode);
					return this.effectiveClientIDMode.Value;
				}
				PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
				this.effectiveClientIDMode = new ClientIDMode?(pagesSection.ClientIDMode);
				return this.effectiveClientIDMode.Value;
			}
		}

		/// <summary>Sets the cached <see cref="P:System.Web.UI.Control.ClientID" /> value to null.</summary>
		// Token: 0x060010D8 RID: 4312 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		protected void ClearCachedClientID()
		{
			this.clientID = null;
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].ClearCachedClientID();
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.Control.ClientIDMode" /> property of the current control instance and of any child controls to <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</summary>
		// Token: 0x060010D9 RID: 4313 RVA: 0x0002E730 File Offset: 0x0002C930
		protected void ClearEffectiveClientIDMode()
		{
			this.effectiveClientIDMode = null;
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].ClearEffectiveClientIDMode();
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002E77C File Offset: 0x0002C97C
		private string GetClientID()
		{
			switch (this.EffectiveClientIDMode)
			{
			case ClientIDMode.AutoID:
				return this.UniqueID2ClientID(this.UniqueID);
			case ClientIDMode.Predictable:
				this.EnsureID();
				return this.GeneratePredictableClientID();
			case ClientIDMode.Static:
				this.EnsureID();
				return this.ID;
			default:
				throw new InvalidOperationException("Unsupported ClientIDMode value.");
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		private string GeneratePredictableClientID()
		{
			string text = this.ID;
			bool flag = !string.IsNullOrEmpty(text);
			char clientIDSeparator = this.ClientIDSeparator;
			StringBuilder stringBuilder = new StringBuilder();
			Control namingContainer = this.NamingContainer;
			if (this is INamingContainer && !flag)
			{
				if (namingContainer != null)
				{
					this.EnsureIDInternal();
				}
				text = this._userId;
			}
			if (namingContainer != null && namingContainer != this.Page)
			{
				if (!string.IsNullOrEmpty(namingContainer.ID))
				{
					stringBuilder.Append(namingContainer.GetClientID());
					stringBuilder.Append(clientIDSeparator);
				}
				else
				{
					stringBuilder.Append(namingContainer.GeneratePredictableClientID());
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(clientIDSeparator);
					}
				}
			}
			if (!flag)
			{
				if (this is INamingContainer || !this.AutoID)
				{
					stringBuilder.Append(text);
				}
				else
				{
					int length = stringBuilder.Length;
					if (length > 0 && stringBuilder[length - 1] == clientIDSeparator)
					{
						stringBuilder.Length = length - 1;
					}
				}
				return stringBuilder.ToString();
			}
			stringBuilder.Append(text);
			IDataItemContainer dataItemContainer = this.DataItemContainer as IDataItemContainer;
			if (dataItemContainer == null)
			{
				return stringBuilder.ToString();
			}
			IDataKeysControl dataKeysControl = this.DataKeysContainer as IDataKeysControl;
			this.GetDataBoundControlFieldValue(stringBuilder, clientIDSeparator, dataItemContainer, dataKeysControl);
			return stringBuilder.ToString();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002E904 File Offset: 0x0002CB04
		private void GetDataBoundControlFieldValue(StringBuilder sb, char separator, IDataItemContainer dataItemContainer, IDataKeysControl dataKeysContainer)
		{
			if (dataItemContainer is IDataBoundItemControl)
			{
				return;
			}
			int displayIndex = dataItemContainer.DisplayIndex;
			if (dataKeysContainer == null)
			{
				if (displayIndex >= 0)
				{
					sb.Append(separator);
					sb.Append(displayIndex);
				}
				return;
			}
			string[] clientIDRowSuffix = dataKeysContainer.ClientIDRowSuffix;
			DataKeyArray clientIDRowSuffixDataKeys = dataKeysContainer.ClientIDRowSuffixDataKeys;
			if (clientIDRowSuffixDataKeys == null || clientIDRowSuffix == null || clientIDRowSuffix.Length == 0)
			{
				sb.Append(separator);
				sb.Append(displayIndex);
				return;
			}
			DataKey dataKey = clientIDRowSuffixDataKeys[displayIndex];
			foreach (string text in clientIDRowSuffix)
			{
				sb.Append(separator);
				object obj = ((dataKey != null) ? dataKey[text] : null);
				if (obj != null)
				{
					sb.Append(obj.ToString());
				}
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
		internal string UniqueID2ClientID(string uniqueId)
		{
			if (string.IsNullOrEmpty(uniqueId))
			{
				return null;
			}
			return uniqueId.Replace(this.IdSeparator, this.ClientIDSeparator);
		}

		/// <summary>Gets a character value representing the separator character used in the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
		/// <returns>Always returns the underscore character (_).</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0002E9D2 File Offset: 0x0002CBD2
		protected char ClientIDSeparator
		{
			get
			{
				return '_';
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls for a specified server control in the UI hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0002E9D6 File Offset: 0x0002CBD6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("The child controls of this control.")]
		public virtual ControlCollection Controls
		{
			get
			{
				if (this._controls == null)
				{
					this._controls = this.CreateControlCollection();
				}
				return this._controls;
			}
		}

		/// <summary>Gets a value indicating whether a control is being used on a design surface.</summary>
		/// <returns>true if the control is being used in a designer; otherwise, false.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00008A69 File Offset: 0x00006C69
		[global::System.MonoTODO("revisit once we have a real design strategy")]
		protected internal bool DesignMode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server control persists its view state, and the view state of any child controls it contains, to the requesting client.</summary>
		/// <returns>true if the server control maintains its view state; otherwise false. The default is true.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0002E9F2 File Offset: 0x0002CBF2
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x0002E9FF File Offset: 0x0002CBFF
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("An Identification of the control that is rendered.")]
		public virtual bool EnableViewState
		{
			get
			{
				return (this.stateMask & 1) != 0;
			}
			set
			{
				this.SetMask(1, value);
			}
		}

		/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
		/// <returns>The programmatic identifier assigned to the control.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0002EA09 File Offset: 0x0002CC09
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0002EA21 File Offset: 0x0002CC21
		[Themeable(false)]
		[ParenthesizePropertyName(true)]
		[Filterable(false)]
		[WebSysDescription("The name of the control that is rendered.")]
		[MergableProperty(false)]
		public virtual string ID
		{
			get
			{
				if ((this.stateMask & 1024) == 0)
				{
					return null;
				}
				return this._userId;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					value = null;
				}
				this.stateMask |= 1024;
				this._userId = value;
				this.NullifyUniqueID();
			}
		}

		/// <summary>Gets a value indicating whether controls contained within this control have control state.</summary>
		/// <returns>true if children of this control do not use control state; otherwise, false.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0002EA50 File Offset: 0x0002CC50
		protected internal bool IsChildControlStateCleared
		{
			get
			{
				return this._isChildControlStateCleared;
			}
		}

		/// <summary>Gets a value indicating whether the control participates in loading its view state by <see cref="P:System.Web.UI.Control.ID" /> instead of index. </summary>
		/// <returns>true if the control loads its view state by <see cref="P:System.Web.UI.Control.ID" />; otherwise, false. The default value is false.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0002EA58 File Offset: 0x0002CC58
		protected bool LoadViewStateByID
		{
			get
			{
				if (this.loadViewStateByID == null)
				{
					this.loadViewStateByID = new bool?(this.IsLoadViewStateByID());
				}
				return this.loadViewStateByID.Value;
			}
		}

		/// <summary>Gets a value indicating whether view state is enabled for this control.</summary>
		/// <returns>true if view state is enabled for the control; otherwise, false.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0002EA84 File Offset: 0x0002CC84
		protected internal bool IsViewStateEnabled
		{
			get
			{
				for (Control control = this; control != null; control = control.Parent)
				{
					if (!control.EnableViewState)
					{
						return false;
					}
					ViewStateMode viewStateMode = control.ViewStateMode;
					if (viewStateMode != ViewStateMode.Inherit)
					{
						return viewStateMode == ViewStateMode.Enabled;
					}
				}
				return true;
			}
		}

		/// <summary>Gets the character used to separate control identifiers.</summary>
		/// <returns>The separator character. The default is "$".</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0002EAB9 File Offset: 0x0002CCB9
		protected char IdSeparator
		{
			get
			{
				return '$';
			}
		}

		/// <summary>Gets a reference to the server control's naming container, which creates a unique namespace for differentiating between server controls with the same <see cref="P:System.Web.UI.Control.ID" /> property value.</summary>
		/// <returns>The server control's naming container.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("The container that this control is part of. The control's name has to be unique within the container.")]
		public virtual Control NamingContainer
		{
			get
			{
				if (this._namingContainer == null && this._parent != null)
				{
					if ((this._parent.stateMask & 64) == 0)
					{
						this._namingContainer = this._parent.NamingContainer;
					}
					else
					{
						this._namingContainer = this._parent;
					}
				}
				return this._namingContainer;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</returns>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0002EB14 File Offset: 0x0002CD14
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0002EB63 File Offset: 0x0002CD63
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("The webpage that this control resides on.")]
		[Bindable(false)]
		public virtual Page Page
		{
			get
			{
				if (this._page == null)
				{
					if (this.NamingContainer != null)
					{
						this._page = this.NamingContainer.Page;
					}
					else if (this.Parent != null)
					{
						this._page = this.Parent.Page;
					}
				}
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		/// <summary>Gets a reference to the server control's parent control in the page control hierarchy.</summary>
		/// <returns>A reference to the server control's parent control.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		[Browsable(false)]
		[WebSysDescription("The parent control of this control.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public virtual Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		/// <summary>Gets information about the container that hosts the current control when rendered on a design surface.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> that contains information about the container that the control is hosted in.</returns>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0002EB74 File Offset: 0x0002CD74
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0002EB7C File Offset: 0x0002CD7C
		[WebSysDescription("The site this control is part of.")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Gets or sets a reference to the template that contains this control. </summary>
		/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> instance that contains this control. </returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0002EB85 File Offset: 0x0002CD85
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x0002EB8D File Offset: 0x0002CD8D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
		public TemplateControl TemplateControl
		{
			get
			{
				return this.TemplateControlInternal;
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this._templateControl = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0002EB96 File Offset: 0x0002CD96
		internal virtual TemplateControl TemplateControlInternal
		{
			get
			{
				if (this._templateControl != null)
				{
					return this._templateControl;
				}
				if (this._parent != null)
				{
					return this._parent.TemplateControl;
				}
				return null;
			}
		}

		/// <summary>Gets the virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> that contains the current server control.</summary>
		/// <returns>The virtual directory of the page or user control that contains the server control.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0002EBBC File Offset: 0x0002CDBC
		[WebSysDescription("A virtual directory containing the parent of the control.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string TemplateSourceDirectory
		{
			get
			{
				if (this._templateSourceDirectory == null)
				{
					TemplateControl templateControl = this.TemplateControl;
					if (templateControl == null)
					{
						HttpContext context = this.Context;
						if (context != null)
						{
							this._templateSourceDirectory = VirtualPathUtility.GetDirectory(context.Request.CurrentExecutionFilePath);
						}
					}
					else if (templateControl != this)
					{
						this._templateSourceDirectory = templateControl.TemplateSourceDirectory;
					}
					if (this._templateSourceDirectory == null && this is TemplateControl)
					{
						string appRelativeVirtualPath = ((TemplateControl)this).AppRelativeVirtualPath;
						if (appRelativeVirtualPath != null)
						{
							string directory = VirtualPathUtility.GetDirectory(VirtualPathUtility.ToAbsolute(appRelativeVirtualPath));
							int num = directory.Length;
							if (num <= 1)
							{
								return directory;
							}
							if (directory[--num] == '/')
							{
								this._templateSourceDirectory = directory.Substring(0, num);
							}
						}
						else
						{
							this._templateSourceDirectory = string.Empty;
						}
					}
					if (this._templateSourceDirectory == null)
					{
						this._templateSourceDirectory = string.Empty;
					}
				}
				return this._templateSourceDirectory;
			}
		}

		/// <summary>Gets the unique, hierarchically qualified identifier for the server control.</summary>
		/// <returns>The fully qualified identifier for the server control.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0002EC90 File Offset: 0x0002CE90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("The unique ID of the control.")]
		[Browsable(false)]
		public virtual string UniqueID
		{
			get
			{
				if (this.uniqueID != null)
				{
					return this.uniqueID;
				}
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return this._userId;
				}
				this.EnsureIDInternal();
				string text = namingContainer.UniqueID;
				if (namingContainer == this.Page || text == null)
				{
					this.uniqueID = this._userId;
					return this.uniqueID;
				}
				this.uniqueID = text + this.IdSeparator.ToString() + this._userId;
				return this.uniqueID;
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0002ED0F File Offset: 0x0002CF0F
		private void SetMask(int m, bool val)
		{
			if (val)
			{
				this.stateMask |= m;
				return;
			}
			this.stateMask &= ~m;
		}

		/// <summary>Gets or sets a value that indicates whether a server control is rendered as UI on the page.</summary>
		/// <returns>true if the control is visible on the page; otherwise false.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0002ED32 File Offset: 0x0002CF32
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0002ED55 File Offset: 0x0002CF55
		[DefaultValue(true)]
		[Bindable(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("Visiblity state of the control.")]
		public virtual bool Visible
		{
			get
			{
				return (this.stateMask & 2) != 0 && (this._parent == null || this._parent.Visible);
			}
			set
			{
				if (((value && (this.stateMask & 2) == 0) || (!value && (this.stateMask & 2) != 0)) && this.IsTrackingViewState)
				{
					this.stateMask |= 128;
				}
				this.SetMask(2, value);
			}
		}

		/// <summary>Gets a value that indicates whether the server control's child controls have been created.</summary>
		/// <returns>true if child controls have been created; otherwise, false.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0002ED93 File Offset: 0x0002CF93
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		protected bool ChildControlsCreated
		{
			get
			{
				return (this.stateMask & 512) != 0;
			}
			set
			{
				if (!value && (this.stateMask & 512) != 0)
				{
					ControlCollection controls = this.Controls;
					if (controls != null)
					{
						controls.Clear();
					}
				}
				this.SetMask(512, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object associated with the server control for the current Web request.</summary>
		/// <returns>The specified <see cref="T:System.Web.HttpContext" /> object associated with the current request.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected internal virtual HttpContext Context
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.Context;
				}
				return HttpContext.Current;
			}
		}

		/// <summary>Gets a list of event handler delegates for the control. This property is read-only.</summary>
		/// <returns>The list of event handler delegates.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0002EE03 File Offset: 0x0002D003
		protected EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		/// <summary>Gets a value indicating whether the current server control's child controls have any saved view-state settings.</summary>
		/// <returns>true if any child controls have saved view state information; otherwise, false.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0002EE1E File Offset: 0x0002D01E
		protected bool HasChildViewState
		{
			get
			{
				return this.pendingVS != null && this.pendingVS.Count > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0002EE38 File Offset: 0x0002D038
		protected bool IsTrackingViewState
		{
			get
			{
				return (this.stateMask & 256) != 0;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the server control's view-state information.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0002EE49 File Offset: 0x0002D049
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("ViewState")]
		[Browsable(false)]
		protected virtual StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag(this.ViewStateIgnoresCase);
				}
				if (this.IsTrackingViewState)
				{
					this._viewState.TrackViewState();
				}
				return this._viewState;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.StateBag" /> object is case-insensitive.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateBag" /> instance is case-insensitive; otherwise, false. The default is false.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00008A69 File Offset: 0x00006C69
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual bool ViewStateIgnoresCase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0002EE7D File Offset: 0x0002D07D
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0002EE8B File Offset: 0x0002D08B
		internal bool AutoEventWireup
		{
			get
			{
				return (this.stateMask & 32) != 0;
			}
			set
			{
				this.SetMask(32, value);
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002EE96 File Offset: 0x0002D096
		internal void SetBindingContainer(bool isBC)
		{
			this.SetMask(16, isBC);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002EEA1 File Offset: 0x0002D0A1
		internal void ResetChildNames()
		{
			this.ResetChildNames(-1);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0002EEAA File Offset: 0x0002D0AA
		internal void ResetChildNames(int value)
		{
			if (value < 0)
			{
				this.defaultNumberID = 0;
				return;
			}
			this.defaultNumberID = value;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002EEBF File Offset: 0x0002D0BF
		internal int GetDefaultNumberID()
		{
			return this.defaultNumberID;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		private string GetDefaultName()
		{
			string text;
			if (this.defaultNumberID > 99)
			{
				object obj = "ctl";
				int num = this.defaultNumberID;
				this.defaultNumberID = num + 1;
				text = obj + num;
			}
			else
			{
				string[] array = Control.defaultNameArray;
				int num = this.defaultNumberID;
				this.defaultNumberID = num + 1;
				text = array[num];
			}
			return text;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0002EF1C File Offset: 0x0002D11C
		private void NullifyUniqueID()
		{
			this.uniqueID = null;
			this.ClearCachedClientID();
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].NullifyUniqueID();
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0002EF68 File Offset: 0x0002D168
		private bool IsLoadViewStateByID()
		{
			if (Control.loadViewStateByIDCache == null)
			{
				Control.loadViewStateByIDCache = new Dictionary<Type, bool>();
			}
			Type type = base.GetType();
			bool flag;
			if (Control.loadViewStateByIDCache.TryGetValue(type, out flag))
			{
				return flag;
			}
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			flag = false;
			if (attributes != null)
			{
				using (IEnumerator enumerator = attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((Attribute)enumerator.Current) is ViewStateModeByIdAttribute)
						{
							flag = true;
							break;
						}
					}
				}
			}
			Control.loadViewStateByIDCache.Add(type, flag);
			return flag;
		}

		/// <summary>Called after a child control is added to the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been added. </param>
		/// <param name="index">The index of the control in the <see cref="P:System.Web.UI.Control.Controls" /> collection. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="control" /> is a <see cref="T:System.Web.UI.WebControls.Substitution" />  control.</exception>
		// Token: 0x06001108 RID: 4360 RVA: 0x0002F004 File Offset: 0x0002D204
		protected internal virtual void AddedControl(Control control, int index)
		{
			this.ResetControlsCache();
			if (control._parent != null)
			{
				control._parent.Controls.Remove(control);
			}
			control._parent = this;
			Control control2 = (((this.stateMask & 64) != 0) ? this : this.NamingContainer);
			if ((this.stateMask & 6144) != 0)
			{
				control.InitRecursive(control2);
				control.SetMask(262144, false);
				if ((this.stateMask & 24576) != 0 && this.pendingVS != null)
				{
					bool flag = this.LoadViewStateByID;
					string text;
					object obj;
					if (flag)
					{
						control.EnsureID();
						text = control.ID;
						obj = this.pendingVS[text];
					}
					else
					{
						text = null;
						obj = this.pendingVS[index];
					}
					if (obj != null)
					{
						if (flag)
						{
							this.pendingVS.Remove(text);
						}
						else
						{
							this.pendingVS.Remove(index);
						}
						if (this.pendingVS.Count == 0)
						{
							this.pendingVS = null;
						}
						control.LoadViewStateRecursive(obj);
					}
				}
				if ((this.stateMask & 16384) != 0)
				{
					control.LoadRecursive();
				}
				if ((this.stateMask & 32768) != 0)
				{
					control.PreRenderRecursiveInternal();
				}
				return;
			}
			control.SetNamingContainer(control2);
			control.SetMask(262144, false);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0002F13E File Offset: 0x0002D33E
		private void SetNamingContainer(Control nc)
		{
			if (nc != null)
			{
				this._namingContainer = nc;
				if (this.AutoID)
				{
					this.EnsureIDInternal();
				}
			}
		}

		/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		// Token: 0x0600110A RID: 4362 RVA: 0x0002F158 File Offset: 0x0002D358
		protected virtual void AddParsedSubObject(object obj)
		{
			Control control = obj as Control;
			if (control != null)
			{
				this.Controls.Add(control);
			}
		}

		/// <summary>Applies the style properties defined in the page style sheet to the control.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> containing the control.</param>
		/// <exception cref="T:System.InvalidOperationException">The style sheet is already applied.</exception>
		// Token: 0x0600110B RID: 4363 RVA: 0x0002F17C File Offset: 0x0002D37C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void ApplyStyleSheetSkin(Page page)
		{
			if (page == null)
			{
				return;
			}
			if (!this.EnableTheming)
			{
				return;
			}
			if (page.StyleSheetPageTheme != null)
			{
				ControlSkin controlSkin = page.StyleSheetPageTheme.GetControlSkin(base.GetType(), this.SkinID);
				if (controlSkin != null)
				{
					controlSkin.ApplySkin(this);
				}
			}
		}

		/// <summary>Gathers information about the server control and delivers it to the <see cref="P:System.Web.UI.Page.Trace" /> property to be displayed when tracing is enabled for the page.</summary>
		/// <param name="parentId">The identifier of the control's parent. </param>
		/// <param name="calcViewState">A Boolean that indicates whether the view-state size is calculated. </param>
		// Token: 0x0600110C RID: 4364 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO]
		protected void BuildProfileTree(string parentId, bool calcViewState)
		{
		}

		/// <summary>Deletes the control-state information for the server control's child controls. </summary>
		// Token: 0x0600110D RID: 4365 RVA: 0x0002F1C0 File Offset: 0x0002D3C0
		protected void ClearChildControlState()
		{
			this._isChildControlStateCleared = true;
		}

		/// <summary>Deletes the view-state and control-state information for all the server control's child controls.</summary>
		// Token: 0x0600110E RID: 4366 RVA: 0x0002F1C9 File Offset: 0x0002D3C9
		protected void ClearChildState()
		{
			this.ClearChildViewState();
			this.ClearChildControlState();
		}

		/// <summary>Deletes the view-state information for all the server control's child controls.</summary>
		// Token: 0x0600110F RID: 4367 RVA: 0x0002F1D7 File Offset: 0x0002D3D7
		protected void ClearChildViewState()
		{
			this.pendingVS = null;
		}

		/// <summary>Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.</summary>
		// Token: 0x06001110 RID: 4368 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void CreateChildControls()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x06001111 RID: 4369 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		protected virtual ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Determines whether the server control contains child controls. If it does not, it creates child controls.</summary>
		// Token: 0x06001112 RID: 4370 RVA: 0x0002F1E8 File Offset: 0x0002D3E8
		protected virtual void EnsureChildControls()
		{
			if (!this.ChildControlsCreated && (this.stateMask & 8) == 0)
			{
				this.stateMask |= 8;
				if (this.Adapter != null)
				{
					this.Adapter.CreateChildControls();
				}
				else
				{
					this.CreateChildControls();
				}
				this.ChildControlsCreated = true;
				this.stateMask &= -9;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0002F246 File Offset: 0x0002D446
		private void EnsureIDInternal()
		{
			if (this._userId != null)
			{
				return;
			}
			this._userId = this.NamingContainer.GetDefaultName();
			this.SetMask(131072, true);
		}

		/// <summary>Creates an identifier for controls that do not have an identifier assigned.</summary>
		// Token: 0x06001114 RID: 4372 RVA: 0x0002F26E File Offset: 0x0002D46E
		protected void EnsureID()
		{
			if (this.NamingContainer == null)
			{
				return;
			}
			this.EnsureIDInternal();
			this.SetMask(1024, true);
		}

		/// <summary>Returns a value indicating whether events are registered for the control or any child controls.</summary>
		/// <returns>true if events are registered; otherwise, false.</returns>
		// Token: 0x06001115 RID: 4373 RVA: 0x0002F28B File Offset: 0x0002D48B
		protected bool HasEvents()
		{
			return this._events != null;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0002F296 File Offset: 0x0002D496
		private void ResetControlsCache()
		{
			this._controlsCache = null;
			if ((this.stateMask & 64) == 0 && this.Parent != null)
			{
				this.Parent.ResetControlsCache();
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		private Hashtable InitControlsCache()
		{
			if (this._controlsCache != null)
			{
				return this._controlsCache;
			}
			if ((this.stateMask & 64) != 0 || this.Parent == null)
			{
				this._controlsCache = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				this._controlsCache = this.Parent.InitControlsCache();
			}
			return this._controlsCache;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002F318 File Offset: 0x0002D518
		private void EnsureControlsCache()
		{
			if (this._controlsCache != null)
			{
				return;
			}
			this.InitControlsCache();
			this.FillControlCache(this._controls);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002F338 File Offset: 0x0002D538
		private void FillControlCache(ControlCollection controls)
		{
			if (controls == null || controls.Count == 0)
			{
				return;
			}
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				try
				{
					if (control._userId != null)
					{
						this._controlsCache.Add(control._userId, control);
					}
				}
				catch (ArgumentException)
				{
					throw new HttpException("Multiple controls with the same ID '" + control._userId + "' were found. FindControl requires that controls have unique IDs. ");
				}
				if ((control.stateMask & 64) == 0 && control.HasControls())
				{
					this.FillControlCache(control.Controls);
				}
			}
		}

		/// <summary>Determines if the server control holds only literal content.</summary>
		/// <returns>true if the server control contains solely literal content; otherwise false.</returns>
		// Token: 0x0600111A RID: 4378 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		protected bool IsLiteralContent()
		{
			return this._controls != null && this._controls.Count == 1 && this._controls[0] is LiteralControl;
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found. </param>
		// Token: 0x0600111B RID: 4379 RVA: 0x0002F422 File Offset: 0x0002D622
		[WebSysDescription("")]
		public virtual Control FindControl(string id)
		{
			return this.FindControl(id, 0);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0002F42C File Offset: 0x0002D62C
		private Control LookForControlByName(string id)
		{
			this.EnsureControlsCache();
			return (Control)this._controlsCache[id];
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> and an integer, specified in the <paramref name="pathOffset" /> parameter, which aids in the search. You should not override this version of the <see cref="Overload:System.Web.UI.Control.FindControl" /> method.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found. </param>
		/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
		// Token: 0x0600111D RID: 4381 RVA: 0x0002F448 File Offset: 0x0002D648
		protected virtual Control FindControl(string id, int pathOffset)
		{
			this.EnsureChildControls();
			if ((this.stateMask & 64) == 0)
			{
				Control control = this.NamingContainer;
				if (control == null)
				{
					return null;
				}
				return control.FindControl(id, pathOffset);
			}
			else
			{
				if (!this.HasControls())
				{
					return null;
				}
				int num = id.IndexOf(this.IdSeparator, pathOffset);
				if (num == -1)
				{
					Control control2 = this.LookForControlByName((pathOffset > 0) ? id.Substring(pathOffset) : id);
					if (control2 != null)
					{
						return control2;
					}
					if (pathOffset == 0)
					{
						Control control = this.NamingContainer;
						if (control != null)
						{
							control2 = control.FindControl(id);
							if (control2 != null)
							{
								return control2;
							}
						}
					}
					return null;
				}
				else
				{
					string text = id.Substring(pathOffset, num - pathOffset);
					Control control = this.LookForControlByName(text);
					if (control == null)
					{
						return null;
					}
					return control.FindControl(id, num + 1);
				}
			}
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
		// Token: 0x0600111E RID: 4382 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
				object obj = this.ViewState["Visible"];
				if (obj != null)
				{
					this.SetMask(2, (bool)obj);
					this.stateMask |= 128;
				}
			}
		}

		/// <summary>Retrieves the physical path that a virtual path, either absolute or relative, maps to.</summary>
		/// <returns>The physical path to the requested file.</returns>
		/// <param name="virtualPath">A relative or root relative URL. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null or an empty string ("").</exception>
		// Token: 0x0600111F RID: 4383 RVA: 0x0002F544 File Offset: 0x0002D744
		protected string MapPathSecure(string virtualPath)
		{
			string text = UrlUtils.Combine(this.TemplateSourceDirectory, virtualPath);
			return this.Context.Request.MapPath(text);
		}

		/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001120 RID: 4384 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool OnBubbleEvent(object source, EventArgs args)
		{
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001121 RID: 4385 RVA: 0x0002F570 File Offset: 0x0002D770
		protected virtual void OnDataBinding(EventArgs e)
		{
			if ((this.event_mask & 1) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.DataBindingEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001122 RID: 4386 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		protected internal virtual void OnInit(EventArgs e)
		{
			if ((this.event_mask & 4) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.InitEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001123 RID: 4387 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		protected internal virtual void OnLoad(EventArgs e)
		{
			if ((this.event_mask & 8) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.LoadEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001124 RID: 4388 RVA: 0x0002F618 File Offset: 0x0002D818
		protected internal virtual void OnPreRender(EventArgs e)
		{
			if ((this.event_mask & 16) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.PreRenderEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data. </param>
		// Token: 0x06001125 RID: 4389 RVA: 0x0002F654 File Offset: 0x0002D854
		protected internal virtual void OnUnload(EventArgs e)
		{
			if ((this.event_mask & 32) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.UnloadEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> used to read a file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that references the desired file.</returns>
		/// <param name="path">The path to the desired file.</param>
		/// <exception cref="T:System.Web.HttpException">Access to the specified file was denied.</exception>
		// Token: 0x06001126 RID: 4390 RVA: 0x0002F690 File Offset: 0x0002D890
		protected internal Stream OpenFile(string path)
		{
			Stream stream;
			try
			{
				stream = File.OpenRead(this.Context.Server.MapPath(path));
			}
			catch (UnauthorizedAccessException)
			{
				throw new HttpException("Access to the specified file was denied.");
			}
			return stream;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0002F6D4 File Offset: 0x0002D8D4
		internal string GetPhysicalFilePath(string virtualPath)
		{
			Page page = this.Page;
			if (VirtualPathUtility.IsAbsolute(virtualPath))
			{
				if (page == null)
				{
					return this.Context.Server.MapPath(virtualPath);
				}
				return page.MapPath(virtualPath);
			}
			else
			{
				MasterPage masterPage = null;
				for (Control control = this.Parent; control != null; control = control.Parent)
				{
					if (control is MasterPage)
					{
						masterPage = control as MasterPage;
						break;
					}
				}
				string text;
				if (masterPage != null)
				{
					text = VirtualPathUtility.Combine(masterPage.TemplateSourceDirectory + "/", virtualPath);
				}
				else
				{
					text = VirtualPathUtility.Combine(this.TemplateSourceDirectory + "/", virtualPath);
				}
				if (page == null)
				{
					return this.Context.Server.MapPath(text);
				}
				return page.MapPath(text);
			}
		}

		/// <summary>Assigns any sources of the event and its information to the control's parent.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001128 RID: 4392 RVA: 0x0002F784 File Offset: 0x0002D984
		protected void RaiseBubbleEvent(object source, EventArgs args)
		{
			Control control = this.Parent;
			while (control != null && !control.OnBubbleEvent(source, args))
			{
				control = control.Parent;
			}
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
		// Token: 0x06001129 RID: 4393 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		/// <summary>Outputs the content of a server control's children to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content. </param>
		// Token: 0x0600112A RID: 4394 RVA: 0x0002F7B8 File Offset: 0x0002D9B8
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			if (this._renderMethodDelegate != null)
			{
				this._renderMethodDelegate(writer, this);
				return;
			}
			if (this._controls == null)
			{
				return;
			}
			int count = this._controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = this._controls[i];
				if (control != null)
				{
					ControlAdapter controlAdapter = control.Adapter;
					if (controlAdapter != null)
					{
						control.RenderControl(writer, controlAdapter);
					}
					else
					{
						control.RenderControl(writer);
					}
				}
			}
		}

		/// <summary>Gets the control adapter responsible for rendering the specified control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that will render the control.</returns>
		// Token: 0x0600112B RID: 4395 RVA: 0x0002F828 File Offset: 0x0002DA28
		protected virtual ControlAdapter ResolveAdapter()
		{
			HttpContext context = this.Context;
			if (context == null)
			{
				return null;
			}
			if (!context.Request.BrowserMightHaveAdapters)
			{
				return null;
			}
			IDictionary adapters = context.Request.Browser.Adapters;
			Type type = base.GetType();
			Type type2 = (Type)adapters[type];
			while (type2 == null && type != typeof(Control))
			{
				type = type.BaseType;
				type2 = (Type)adapters[type];
			}
			ControlAdapter controlAdapter = null;
			if (type2 != null)
			{
				controlAdapter = (ControlAdapter)Activator.CreateInstance(type2);
			}
			return controlAdapter;
		}

		/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x0600112C RID: 4396 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
		protected virtual object SaveViewState()
		{
			if ((this.stateMask & 128) != 0)
			{
				this.ViewState["Visible"] = (this.stateMask & 2) != 0;
			}
			else if (this._viewState == null)
			{
				return null;
			}
			return this._viewState.SaveViewState();
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600112D RID: 4397 RVA: 0x0002F916 File Offset: 0x0002DB16
		protected virtual void TrackViewState()
		{
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
			this.stateMask |= 256;
		}

		/// <summary>Enables a server control to perform final clean up before it is released from memory.</summary>
		// Token: 0x0600112E RID: 4398 RVA: 0x0002F940 File Offset: 0x0002DB40
		public virtual void Dispose()
		{
			if ((this.event_mask & 2) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.DisposedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the server control binds to a data source.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600112F RID: 4399 RVA: 0x0002F97C File Offset: 0x0002DB7C
		// (remove) Token: 0x06001130 RID: 4400 RVA: 0x0002F99D File Offset: 0x0002DB9D
		[WebSysDescription("Raised when the contols databound properties are evaluated.")]
		[WebCategory("FIXME")]
		public event EventHandler DataBinding
		{
			add
			{
				this.event_mask |= 1;
				this.Events.AddHandler(Control.DataBindingEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.DataBindingEvent, value);
			}
		}

		/// <summary>Occurs when a server control is released from memory, which is the last stage of the server control lifecycle when an ASP.NET page is requested.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001131 RID: 4401 RVA: 0x0002F9B0 File Offset: 0x0002DBB0
		// (remove) Token: 0x06001132 RID: 4402 RVA: 0x0002F9D1 File Offset: 0x0002DBD1
		[WebSysDescription("Raised when the contol is disposed.")]
		public event EventHandler Disposed
		{
			add
			{
				this.event_mask |= 2;
				this.Events.AddHandler(Control.DisposedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.DisposedEvent, value);
			}
		}

		/// <summary>Occurs when the server control is initialized, which is the first step in its lifecycle.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001133 RID: 4403 RVA: 0x0002F9E4 File Offset: 0x0002DBE4
		// (remove) Token: 0x06001134 RID: 4404 RVA: 0x0002FA05 File Offset: 0x0002DC05
		[WebSysDescription("Raised when the page containing the control is initialized.")]
		public event EventHandler Init
		{
			add
			{
				this.event_mask |= 4;
				this.Events.AddHandler(Control.InitEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.InitEvent, value);
			}
		}

		/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> object.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001135 RID: 4405 RVA: 0x0002FA18 File Offset: 0x0002DC18
		// (remove) Token: 0x06001136 RID: 4406 RVA: 0x0002FA39 File Offset: 0x0002DC39
		[WebSysDescription("Raised after the page containing the control has been loaded.")]
		public event EventHandler Load
		{
			add
			{
				this.event_mask |= 8;
				this.Events.AddHandler(Control.LoadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.LoadEvent, value);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Web.UI.Control" /> object is loaded but prior to rendering.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001137 RID: 4407 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		// (remove) Token: 0x06001138 RID: 4408 RVA: 0x0002FA6E File Offset: 0x0002DC6E
		[WebSysDescription("Raised before the page containing the control is rendered.")]
		public event EventHandler PreRender
		{
			add
			{
				this.event_mask |= 16;
				this.Events.AddHandler(Control.PreRenderEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.PreRenderEvent, value);
			}
		}

		/// <summary>Occurs when the server control is unloaded from memory.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001139 RID: 4409 RVA: 0x0002FA81 File Offset: 0x0002DC81
		// (remove) Token: 0x0600113A RID: 4410 RVA: 0x0002FAA3 File Offset: 0x0002DCA3
		[WebSysDescription("Raised when the page containing the control is unloaded.")]
		public event EventHandler Unload
		{
			add
			{
				this.event_mask |= 32;
				this.Events.AddHandler(Control.UnloadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.UnloadEvent, value);
			}
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
		// Token: 0x0600113B RID: 4411 RVA: 0x0002FAB6 File Offset: 0x0002DCB6
		public virtual void DataBind()
		{
			this.DataBind(true);
		}

		/// <summary>Binds a data source to the server control's child controls.</summary>
		// Token: 0x0600113C RID: 4412 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		protected virtual void DataBindChildren()
		{
			if (!this.HasControls())
			{
				return;
			}
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].DataBind();
			}
		}

		/// <summary>Determines if the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x0600113D RID: 4413 RVA: 0x0002FB0A File Offset: 0x0002DD0A
		public virtual bool HasControls()
		{
			return this._controls != null && this._controls.Count > 0;
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x0600113E RID: 4414 RVA: 0x0002FB24 File Offset: 0x0002DD24
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			if (this.adapter != null)
			{
				this.RenderControl(writer, this.adapter);
				return;
			}
			if ((this.stateMask & 2) != 0)
			{
				HttpContext context = this.Context;
				TraceContext traceContext = ((context != null) ? context.Trace : null);
				int num = 0;
				if (traceContext != null && traceContext.IsEnabled)
				{
					num = context.Response.GetOutputByteCount();
				}
				this.Render(writer);
				if (traceContext != null && traceContext.IsEnabled)
				{
					int num2 = context.Response.GetOutputByteCount() - num;
					traceContext.SaveSize(this, (num2 >= 0) ? num2 : 0);
				}
			}
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object using a provided <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the control content.</param>
		/// <param name="adapter">The <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that defines the rendering.</param>
		// Token: 0x0600113F RID: 4415 RVA: 0x0002FBAD File Offset: 0x0002DDAD
		protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
		{
			if ((this.stateMask & 2) != 0)
			{
				adapter.BeginRender(writer);
				adapter.Render(writer);
				adapter.EndRender(writer);
			}
		}

		/// <summary>Converts a URL into one that is usable on the requesting client.</summary>
		/// <returns>The converted URL.</returns>
		/// <param name="relativeUrl">The URL associated with the <see cref="P:System.Web.UI.Control.TemplateSourceDirectory" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="relativeUrl" /> parameter contains null. </exception>
		// Token: 0x06001140 RID: 4416 RVA: 0x0002FBD0 File Offset: 0x0002DDD0
		public string ResolveUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			if (relativeUrl == string.Empty)
			{
				return relativeUrl;
			}
			if (VirtualPathUtility.IsAbsolute(relativeUrl))
			{
				return relativeUrl;
			}
			if (relativeUrl[0] == '#')
			{
				return relativeUrl;
			}
			string appRelativeTemplateSourceDirectory = this.AppRelativeTemplateSourceDirectory;
			HttpContext context = this.Context;
			HttpResponse httpResponse = ((context != null) ? context.Response : null);
			if (appRelativeTemplateSourceDirectory == null || appRelativeTemplateSourceDirectory.Length == 0 || httpResponse == null || relativeUrl.IndexOf(':') >= 0)
			{
				return relativeUrl;
			}
			if (!VirtualPathUtility.IsAppRelative(relativeUrl))
			{
				relativeUrl = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(appRelativeTemplateSourceDirectory), relativeUrl);
			}
			return httpResponse.ApplyAppPathModifier(relativeUrl);
		}

		/// <summary>Gets a URL that can be used by the browser.</summary>
		/// <returns>A fully qualified URL to the specified resource suitable for use on the browser.</returns>
		/// <param name="relativeUrl">A URL relative to the current page.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="relativeUrl" /> is null.</exception>
		// Token: 0x06001141 RID: 4417 RVA: 0x0002FC64 File Offset: 0x0002DE64
		public string ResolveClientUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			if (relativeUrl.Length == 0)
			{
				return string.Empty;
			}
			if (VirtualPathUtility.IsAbsolute(relativeUrl) || relativeUrl.IndexOf(':') >= 0)
			{
				return relativeUrl;
			}
			HttpContext context = this.Context;
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			if (httpRequest == null)
			{
				return relativeUrl;
			}
			string templateSourceDirectory = this.TemplateSourceDirectory;
			if (templateSourceDirectory == null || templateSourceDirectory.Length == 0)
			{
				return relativeUrl;
			}
			string text = httpRequest.ClientFilePath;
			if (text.Length > 1 && text[text.Length - 1] != '/')
			{
				text = VirtualPathUtility.GetDirectory(text, false);
			}
			if (VirtualPathUtility.IsAppRelative(relativeUrl))
			{
				return VirtualPathUtility.MakeRelative(text, relativeUrl);
			}
			string text2 = VirtualPathUtility.AppendTrailingSlash(templateSourceDirectory);
			if (text.Length == text2.Length && string.CompareOrdinal(text, text2) == 0)
			{
				return relativeUrl;
			}
			relativeUrl = VirtualPathUtility.Combine(text2, relativeUrl);
			return VirtualPathUtility.MakeRelative(text, relativeUrl);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0002FD40 File Offset: 0x0002DF40
		internal bool HasRenderMethodDelegate()
		{
			return this._renderMethodDelegate != null;
		}

		/// <summary>Assigns an event handler delegate to render the server control and its content into its parent control.</summary>
		/// <param name="renderMethod">The information necessary to pass to the delegate so that it can render the server control. </param>
		// Token: 0x06001143 RID: 4419 RVA: 0x0002FD4B File Offset: 0x0002DF4B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this._renderMethodDelegate = renderMethod;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0002FD54 File Offset: 0x0002DF54
		internal void LoadRecursive()
		{
			if ((this.stateMask & 16384) == 0)
			{
				if (this.Adapter != null)
				{
					this.Adapter.OnLoad(EventArgs.Empty);
				}
				else
				{
					this.OnLoad(EventArgs.Empty);
				}
			}
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].LoadRecursive();
			}
			this.stateMask |= 16384;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		internal void UnloadRecursive(bool dispose)
		{
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].UnloadRecursive(dispose);
			}
			ControlAdapter controlAdapter = this.Adapter;
			if (controlAdapter != null)
			{
				controlAdapter.OnUnload(EventArgs.Empty);
			}
			else
			{
				this.OnUnload(EventArgs.Empty);
			}
			if (dispose)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0002FE4C File Offset: 0x0002E04C
		internal void PreRenderRecursiveInternal()
		{
			if (this.Visible)
			{
				this.SetMask(2, true);
				this.EnsureChildControls();
				if (this.Adapter != null)
				{
					this.Adapter.OnPreRender(EventArgs.Empty);
				}
				else
				{
					this.OnPreRender(EventArgs.Empty);
				}
				if (!this.HasControls())
				{
					return;
				}
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					this._controls[i].PreRenderRecursiveInternal();
				}
			}
			else
			{
				this.SetMask(2, false);
			}
			this.stateMask |= 32768;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		internal virtual void InitRecursive(Control namingContainer)
		{
			this.SetNamingContainer(namingContainer);
			if (this.HasControls())
			{
				if ((this.stateMask & 64) != 0)
				{
					namingContainer = this;
				}
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					this._controls[i].InitRecursive(namingContainer);
				}
			}
			if ((this.stateMask & 262144) == 0 && (this.stateMask & 2048) != 2048)
			{
				this.stateMask |= 4096;
				this.ApplyTheme();
				ControlAdapter controlAdapter = this.Adapter;
				if (controlAdapter != null)
				{
					controlAdapter.OnInit(EventArgs.Empty);
				}
				else
				{
					this.OnInit(EventArgs.Empty);
				}
				this.TrackViewState();
				this.stateMask |= 2048;
				this.stateMask &= -4097;
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		internal object SaveViewStateRecursive()
		{
			TraceContext traceContext = ((this.Context != null && this.Context.Trace.IsEnabled) ? this.Context.Trace : null);
			ArrayList arrayList = null;
			bool flag = this.LoadViewStateByID;
			if (this.HasControls())
			{
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					Control control = this._controls[i];
					object obj = control.SaveViewStateRecursive();
					if (obj != null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						if (flag)
						{
							control.EnsureID();
							arrayList.Add(new Pair(control.ID, obj));
						}
						else
						{
							arrayList.Add(new Pair(i, obj));
						}
					}
				}
			}
			object obj2 = null;
			if (this.Adapter != null)
			{
				obj2 = this.Adapter.SaveAdapterViewState();
			}
			object obj3 = null;
			if (this.IsViewStateEnabled)
			{
				obj3 = this.SaveViewState();
			}
			if (obj3 == null && arrayList == null)
			{
				if (traceContext != null)
				{
					traceContext.SaveViewState(this, null);
				}
				return null;
			}
			if (traceContext != null)
			{
				traceContext.SaveViewState(this, obj3);
			}
			obj3 = new object[] { obj3, obj2 };
			return new Pair(obj3, arrayList);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00030104 File Offset: 0x0002E304
		internal void LoadViewStateRecursive(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			Pair pair = (Pair)savedState;
			object[] array = (object[])pair.First;
			if (this.Adapter != null)
			{
				this.Adapter.LoadAdapterViewState(array[1]);
			}
			this.LoadViewState(array[0]);
			ArrayList arrayList = pair.Second as ArrayList;
			if (arrayList == null)
			{
				return;
			}
			int count = arrayList.Count;
			bool flag = this.LoadViewStateByID;
			for (int i = 0; i < count; i++)
			{
				Pair pair2 = arrayList[i] as Pair;
				if (pair2 != null)
				{
					if (flag)
					{
						string text = (string)pair2.First;
						bool flag2 = false;
						foreach (object obj in this.Controls)
						{
							Control control = (Control)obj;
							control.EnsureID();
							if (control.ID == text)
							{
								flag2 = true;
								control.LoadViewStateRecursive(pair2.Second);
								break;
							}
						}
						if (!flag2)
						{
							if (this.pendingVS == null)
							{
								this.pendingVS = new Hashtable();
							}
							this.pendingVS[text] = pair2.Second;
						}
					}
					else
					{
						int num = (int)pair2.First;
						if (num < this.Controls.Count)
						{
							this.Controls[num].LoadViewStateRecursive(pair2.Second);
						}
						else
						{
							if (this.pendingVS == null)
							{
								this.pendingVS = new Hashtable();
							}
							this.pendingVS[num] = pair2.Second;
						}
					}
				}
			}
			this.stateMask |= 8192;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000302C4 File Offset: 0x0002E4C4
		internal void ApplyTheme()
		{
			Page page = this.Page;
			if (page != null && page.PageTheme != null && this.EnableTheming)
			{
				ControlSkin controlSkin = page.PageTheme.GetControlSkin(base.GetType(), this.SkinID);
				if (controlSkin != null)
				{
					controlSkin.ApplySkin(this);
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0003030D File Offset: 0x0002E50D
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x0003031A File Offset: 0x0002E51A
		internal bool AutoID
		{
			get
			{
				return (this.stateMask & 4) != 0;
			}
			set
			{
				if (!value && (this.stateMask & 64) != 0)
				{
					return;
				}
				this.SetMask(4, value);
			}
		}

		/// <summary>Called after a child control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been removed. </param>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x0600114D RID: 4429 RVA: 0x00030334 File Offset: 0x0002E534
		protected internal virtual void RemovedControl(Control control)
		{
			control.UnloadRecursive(false);
			control._parent = null;
			control._page = null;
			control._namingContainer = null;
			if ((control.stateMask & 131072) != 0)
			{
				control._userId = null;
				control.SetMask(1024, false);
			}
			control.NullifyUniqueID();
			control.SetMask(262144, true);
			this.ResetControlsCache();
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is true. </returns>
		/// <exception cref="T:System.InvalidOperationException">The Page_PreInit event has already occurred.- or -The control has already been added to the Controls collection.</exception>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00030396 File Offset: 0x0002E596
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x000303C2 File Offset: 0x0002E5C2
		[Browsable(false)]
		[Themeable(false)]
		[DefaultValue(true)]
		public virtual bool EnableTheming
		{
			get
			{
				if ((this.stateMask & 65536) != 0)
				{
					return this._enableTheming;
				}
				return this._parent == null || this._parent.EnableTheming;
			}
			set
			{
				this.SetMask(65536, true);
				this._enableTheming = value;
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The style sheet has already been applied.- or -The Page_PreInit event has already occurred.- or -The control was already added to the Controls collection.</exception>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000303D7 File Offset: 0x0002E5D7
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000303DF File Offset: 0x0002E5DF
		[Browsable(false)]
		[DefaultValue("")]
		[Filterable(false)]
		public virtual string SkinID
		{
			get
			{
				return this.skinId;
			}
			set
			{
				this.skinId = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlBuilderAccessor.ControlBuilder" />. </summary>
		/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, null if no builder was used.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00003A1F File Offset: 0x00001C1F
		ControlBuilder IControlBuilderAccessor.ControlBuilder
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.GetDesignModeState" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of the control state.</returns>
		// Token: 0x06001153 RID: 4435 RVA: 0x00003A1F File Offset: 0x00001C1F
		IDictionary IControlDesignerAccessor.GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetDesignModeState(System.Collections.IDictionary)" />. </summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
		// Token: 0x06001154 RID: 4436 RVA: 0x000303E8 File Offset: 0x0002E5E8
		void IControlDesignerAccessor.SetDesignModeState(IDictionary designData)
		{
			this.SetDesignModeState(designData);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetOwnerControl(System.Web.UI.Control)" />. </summary>
		/// <param name="owner">The owner of the control. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="owner" /> is set to the current control.</exception>
		// Token: 0x06001155 RID: 4437 RVA: 0x00003A1F File Offset: 0x00001C1F
		void IControlDesignerAccessor.SetOwnerControl(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlDesignerAccessor.UserData" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00003A1F File Offset: 0x00001C1F
		IDictionary IControlDesignerAccessor.UserData
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.Expressions" />. </summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x000303F1 File Offset: 0x0002E5F1
		ExpressionBindingCollection IExpressionsAccessor.Expressions
		{
			get
			{
				if (this.expressionBindings == null)
				{
					this.expressionBindings = new ExpressionBindingCollection();
				}
				return this.expressionBindings;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.HasExpressions" />. </summary>
		/// <returns>true if the control has properties set through expressions; otherwise, false.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0003040C File Offset: 0x0002E60C
		bool IExpressionsAccessor.HasExpressions
		{
			get
			{
				return this.expressionBindings != null && this.expressionBindings.Count > 0;
			}
		}

		/// <summary>Sets input focus to a control.</summary>
		// Token: 0x06001159 RID: 4441 RVA: 0x00030426 File Offset: 0x0002E626
		public virtual void Focus()
		{
			this.Page.SetFocus(this);
		}

		/// <summary>Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
		// Token: 0x0600115A RID: 4442 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void LoadControlState(object savedState)
		{
		}

		/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns null.</returns>
		// Token: 0x0600115B RID: 4443 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected internal virtual object SaveControlState()
		{
			return null;
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event. </summary>
		/// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
		// Token: 0x0600115C RID: 4444 RVA: 0x00030434 File Offset: 0x0002E634
		protected virtual void DataBind(bool raiseOnDataBinding)
		{
			bool flag = false;
			if ((this.stateMask & 64) != 0 && this.Page != null)
			{
				object dataItem = DataBinder.GetDataItem(this, out flag);
				if (flag)
				{
					this.Page.PushDataItemContext(dataItem);
				}
			}
			try
			{
				if (raiseOnDataBinding)
				{
					this.OnDataBinding(EventArgs.Empty);
				}
				this.DataBindChildren();
			}
			finally
			{
				if (flag)
				{
					this.Page.PopDataItemContext();
				}
			}
		}

		/// <summary>Gets design-time data for a control.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</returns>
		// Token: 0x0600115D RID: 4445 RVA: 0x00003A1F File Offset: 0x00001C1F
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected virtual IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets design-time data for a control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
		// Token: 0x0600115E RID: 4446 RVA: 0x00003A1F File Offset: 0x00001C1F
		protected virtual void SetDesignModeState(IDictionary data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000304A4 File Offset: 0x0002E6A4
		internal bool IsInited
		{
			get
			{
				return (this.stateMask & 2048) != 0;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000304B5 File Offset: 0x0002E6B5
		internal bool IsLoaded
		{
			get
			{
				return (this.stateMask & 16384) != 0;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000304C6 File Offset: 0x0002E6C6
		internal bool IsPrerendered
		{
			get
			{
				return (this.stateMask & 32768) != 0;
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000304D7 File Offset: 0x0002E6D7
		private bool CheckForValidationSupport()
		{
			return base.GetType().GetCustomAttributes(typeof(SupportsEventValidationAttribute), false).Length != 0;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000304F4 File Offset: 0x0002E6F4
		internal void ValidateEvent(string uniqueId, string argument)
		{
			Page page = this.Page;
			if (page != null && this.CheckForValidationSupport())
			{
				page.ClientScript.ValidateEvent(uniqueId, argument);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IParserAccessor.AddParsedSubObject(System.Object)" />. </summary>
		/// <param name="obj">The object to add.</param>
		// Token: 0x06001164 RID: 4452 RVA: 0x00030520 File Offset: 0x0002E720
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			this.AddParsedSubObject(obj);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.DataBindings" />. </summary>
		/// <returns>The collection of data bindings.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00030529 File Offset: 0x0002E729
		DataBindingCollection IDataBindingsAccessor.DataBindings
		{
			get
			{
				if (this.dataBindings == null)
				{
					this.dataBindings = new DataBindingCollection();
				}
				return this.dataBindings;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.HasDataBindings" />. </summary>
		/// <returns>true if the control contains data-binding logic; otherwise, false.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00030544 File Offset: 0x0002E744
		bool IDataBindingsAccessor.HasDataBindings
		{
			get
			{
				return this.dataBindings != null && this.dataBindings.Count > 0;
			}
		}

		/// <summary>Gets or sets the view-state mode of this control.</summary>
		/// <returns>The view-state mode of this control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is not in the <see cref="T:System.Web.UI.ViewStateMode" /> enumeration.</exception>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0003055F File Offset: 0x0002E75F
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00030567 File Offset: 0x0002E767
		[Themeable(false)]
		[DefaultValue(ViewStateMode.Inherit)]
		public virtual ViewStateMode ViewStateMode
		{
			get
			{
				return this.viewStateMode;
			}
			set
			{
				if (value < ViewStateMode.Inherit || value > ViewStateMode.Disabled)
				{
					throw new ArgumentOutOfRangeException("An attempt was made to set this property to a value that is not in the ViewStateMode enumeration.");
				}
				this.viewStateMode = value;
			}
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
		/// <returns>The URL that corresponds to the specified route parameters.</returns>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x06001169 RID: 4457 RVA: 0x00030583 File Offset: 0x0002E783
		public string GetRouteUrl(object routeParameters)
		{
			return this.GetRouteUrl(null, new RouteValueDictionary(routeParameters));
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
		/// <returns>The URL that corresponds to the specified route parameters.</returns>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600116A RID: 4458 RVA: 0x00030592 File Offset: 0x0002E792
		public string GetRouteUrl(RouteValueDictionary routeParameters)
		{
			return this.GetRouteUrl(null, routeParameters);
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
		/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600116B RID: 4459 RVA: 0x0003059C File Offset: 0x0002E79C
		public string GetRouteUrl(string routeName, object routeParameters)
		{
			return this.GetRouteUrl(routeName, new RouteValueDictionary(routeParameters));
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
		/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600116C RID: 4460 RVA: 0x000305AC File Offset: 0x0002E7AC
		public string GetRouteUrl(string routeName, RouteValueDictionary routeParameters)
		{
			HttpContext httpContext = this.Context ?? HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest == null)
			{
				return null;
			}
			VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(httpRequest.RequestContext, routeName, routeParameters);
			if (virtualPath == null)
			{
				return null;
			}
			return virtualPath.VirtualPath;
		}

		/// <summary>Returns the prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</summary>
		/// <returns>The prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</returns>
		/// <param name="control">A control that is within a naming container.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Control.NamingContainer" /> property of <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x0600116D RID: 4461 RVA: 0x000305FC File Offset: 0x0002E7FC
		public string GetUniqueIDRelativeTo(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Control control2 = this;
			Control namingContainer = control.NamingContainer;
			if (namingContainer != null)
			{
				while (control2 != null && control2 != namingContainer)
				{
					control2 = control2.Parent;
				}
			}
			if (control2 != namingContainer)
			{
				throw new InvalidOperationException(string.Format("This control is not a descendant of the NamingContainer of '{0}'", control.UniqueID));
			}
			int num = control.UniqueID.LastIndexOf(this.IdSeparator);
			if (num < 0)
			{
				return this.UniqueID;
			}
			return this.UniqueID.Substring(num + 1);
		}

		/// <summary>Gets or sets a value that indicates whether the control checks client input from the browser for potentially dangerous values.</summary>
		/// <returns>A value that determines whether the control checks client input. Values can include <see cref="F:System.Web.UI.ValidateRequestMode.Disabled" />, <see cref="F:System.Web.UI.ValidateRequestMode.Enabled" />, and <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />. The default is <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />, which means that the control gets the value from its parent.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00030678 File Offset: 0x0002E878
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Begins design-time tracing of rendering data.</summary>
		/// <param name="writer">The object that writes trace data.</param>
		/// <param name="traceObject">The trace object.</param>
		// Token: 0x06001170 RID: 4464 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void BeginRenderTracing(TextWriter writer, object traceObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Ends design-time tracing of rendering data.</summary>
		/// <param name="writer">The object that writes trace data.</param>
		/// <param name="traceObject">The trace object.</param>
		// Token: 0x06001171 RID: 4465 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void EndRenderTracing(TextWriter writer, object traceObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets trace data for design-time tracing of rendering data, using the trace data key and the trace data value.</summary>
		/// <param name="traceDataKey">The trace data key.</param>
		/// <param name="traceDataValue">The trace data value.</param>
		// Token: 0x06001172 RID: 4466 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets trace data for design-time tracing of rendering data, using the traced object, the trace data key, and the trace data value.</summary>
		/// <param name="tracedObject">The traced object.</param>
		/// <param name="traceDataKey">The trace data key.</param>
		/// <param name="traceDataValue">The trace data value.</param>
		// Token: 0x06001173 RID: 4467 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040013A4 RID: 5028
		internal static string[] defaultNameArray;

		// Token: 0x040013A5 RID: 5029
		private int event_mask;

		// Token: 0x040013A6 RID: 5030
		private const int databinding_mask = 1;

		// Token: 0x040013A7 RID: 5031
		private const int disposed_mask = 2;

		// Token: 0x040013A8 RID: 5032
		private const int init_mask = 4;

		// Token: 0x040013A9 RID: 5033
		private const int load_mask = 8;

		// Token: 0x040013AA RID: 5034
		private const int prerender_mask = 16;

		// Token: 0x040013AB RID: 5035
		private const int unload_mask = 32;

		// Token: 0x040013AC RID: 5036
		[ThreadStatic]
		private static Dictionary<Type, bool> loadViewStateByIDCache;

		// Token: 0x040013AD RID: 5037
		private bool? loadViewStateByID;

		// Token: 0x040013AE RID: 5038
		private string uniqueID;

		// Token: 0x040013AF RID: 5039
		private string clientID;

		// Token: 0x040013B0 RID: 5040
		private string _userId;

		// Token: 0x040013B1 RID: 5041
		private ControlCollection _controls;

		// Token: 0x040013B2 RID: 5042
		private Control _namingContainer;

		// Token: 0x040013B3 RID: 5043
		private Page _page;

		// Token: 0x040013B4 RID: 5044
		private Control _parent;

		// Token: 0x040013B5 RID: 5045
		private ISite _site;

		// Token: 0x040013B6 RID: 5046
		private StateBag _viewState;

		// Token: 0x040013B7 RID: 5047
		private EventHandlerList _events;

		// Token: 0x040013B8 RID: 5048
		private RenderMethod _renderMethodDelegate;

		// Token: 0x040013B9 RID: 5049
		private Hashtable _controlsCache;

		// Token: 0x040013BA RID: 5050
		private int defaultNumberID;

		// Token: 0x040013BB RID: 5051
		private DataBindingCollection dataBindings;

		// Token: 0x040013BC RID: 5052
		private Hashtable pendingVS;

		// Token: 0x040013BD RID: 5053
		private TemplateControl _templateControl;

		// Token: 0x040013BE RID: 5054
		private bool _isChildControlStateCleared;

		// Token: 0x040013BF RID: 5055
		private string _templateSourceDirectory;

		// Token: 0x040013C0 RID: 5056
		private ViewStateMode viewStateMode;

		// Token: 0x040013C1 RID: 5057
		private ClientIDMode? clientIDMode;

		// Token: 0x040013C2 RID: 5058
		private ClientIDMode? effectiveClientIDMode;

		// Token: 0x040013C3 RID: 5059
		private Version renderingCompatibility;

		// Token: 0x040013C4 RID: 5060
		private bool? renderingCompatibilityOld;

		// Token: 0x040013C5 RID: 5061
		private int stateMask;

		// Token: 0x040013C6 RID: 5062
		private const int ENABLE_VIEWSTATE = 1;

		// Token: 0x040013C7 RID: 5063
		private const int VISIBLE = 2;

		// Token: 0x040013C8 RID: 5064
		private const int AUTOID = 4;

		// Token: 0x040013C9 RID: 5065
		private const int CREATING_CONTROLS = 8;

		// Token: 0x040013CA RID: 5066
		private const int BINDING_CONTAINER = 16;

		// Token: 0x040013CB RID: 5067
		private const int AUTO_EVENT_WIREUP = 32;

		// Token: 0x040013CC RID: 5068
		private const int IS_NAMING_CONTAINER = 64;

		// Token: 0x040013CD RID: 5069
		private const int VISIBLE_CHANGED = 128;

		// Token: 0x040013CE RID: 5070
		private const int TRACK_VIEWSTATE = 256;

		// Token: 0x040013CF RID: 5071
		private const int CHILD_CONTROLS_CREATED = 512;

		// Token: 0x040013D0 RID: 5072
		private const int ID_SET = 1024;

		// Token: 0x040013D1 RID: 5073
		private const int INITED = 2048;

		// Token: 0x040013D2 RID: 5074
		private const int INITING = 4096;

		// Token: 0x040013D3 RID: 5075
		private const int VIEWSTATE_LOADED = 8192;

		// Token: 0x040013D4 RID: 5076
		private const int LOADED = 16384;

		// Token: 0x040013D5 RID: 5077
		private const int PRERENDERED = 32768;

		// Token: 0x040013D6 RID: 5078
		private const int ENABLE_THEMING = 65536;

		// Token: 0x040013D7 RID: 5079
		private const int AUTOID_SET = 131072;

		// Token: 0x040013D8 RID: 5080
		private const int REMOVED = 262144;

		// Token: 0x040013D9 RID: 5081
		private ControlAdapter adapter;

		// Token: 0x040013DA RID: 5082
		private bool did_adapter_lookup;

		// Token: 0x040013DB RID: 5083
		private string _appRelativeTemplateSourceDirectory;

		// Token: 0x040013DC RID: 5084
		internal ControlSkin controlSkin;

		// Token: 0x040013DD RID: 5085
		private string skinId = string.Empty;

		// Token: 0x040013DE RID: 5086
		private bool _enableTheming = true;

		// Token: 0x040013DF RID: 5087
		private ExpressionBindingCollection expressionBindings;
	}
}
