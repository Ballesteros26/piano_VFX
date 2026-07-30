using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Util;
using System.Xml;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides the <see cref="T:System.Web.UI.Page" /> class and the <see cref="T:System.Web.UI.UserControl" /> class with a base set of functionality.</summary>
	// Token: 0x02000232 RID: 562
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class TemplateControl : Control, INamingContainer, IFilterResolutionService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateControl" /> class.</summary>
		// Token: 0x06001703 RID: 5891 RVA: 0x0003DACF File Offset: 0x0003BCCF
		protected TemplateControl()
		{
			base.TemplateControl = this;
			this.Construct();
		}

		/// <summary>The <see cref="P:System.Web.UI.TemplateControl.AutoHandlers" /> property has been deprecated in ASP.NET NET 2.0. It is used by generated classes and is not intended for use within your code.</summary>
		/// <returns>Always 0. </returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x0000393A File Offset: 0x00001B3A
		[Obsolete]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual int AutoHandlers
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.TemplateControl" /> control supports automatic events.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00008B66 File Offset: 0x00006D66
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual bool SupportAutoEvents
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the application-relative, virtual directory path to the file from which the control is parsed and compiled. </summary>
		/// <returns>A string representing the path.</returns>
		/// <exception cref="T:System.ArgumentNullException">The path that is set is null. </exception>
		/// <exception cref="T:System.ArgumentException">The path that is set is not rooted. </exception>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0003DAEC File Offset: 0x0003BCEC
		public string AppRelativeVirtualPath
		{
			get
			{
				return this._appRelativeVirtualPath;
			}
			set
			{
				this._appRelativeVirtualPath = value;
			}
		}

		/// <summary>Performs design-time logic.</summary>
		// Token: 0x06001709 RID: 5897 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void Construct()
		{
		}

		/// <summary>Accesses literal strings stored in a resource. The <see cref="M:System.Web.UI.TemplateControl.CreateResourceBasedLiteralControl(System.Int32,System.Int32,System.Boolean)" /> method is not intended for use from within your code.</summary>
		/// <returns>A <see cref="T:System.Web.UI.LiteralControl" /> representing a literal string in a resource.</returns>
		/// <param name="offset">The offset of the start of the string in the resource. </param>
		/// <param name="size">The size of the string in bytes. </param>
		/// <param name="fAsciiOnly">A Boolean value indicating if the string in the resource contains only 7-bit ASCII characters. </param>
		// Token: 0x0600170A RID: 5898 RVA: 0x0003DAF5 File Offset: 0x0003BCF5
		protected LiteralControl CreateResourceBasedLiteralControl(int offset, int size, bool fAsciiOnly)
		{
			if (this.resource_data == null)
			{
				return null;
			}
			if (offset > this.resource_data.MaxOffset - size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return new ResourceBasedLiteralControl(TemplateControl.AddOffset(this.resource_data.Ptr, offset), size);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0003DB34 File Offset: 0x0003BD34
		internal void WireupAutomaticEvents()
		{
			if (!this.SupportAutoEvents || !base.AutoEventWireup)
			{
				return;
			}
			Type type = base.GetType();
			ArrayList arrayList = TemplateControl.auto_event_info.InsertOrGet((uint)type.GetHashCode(), type, null, new Func<ArrayList>(this.CollectAutomaticEventInfo));
			for (int i = 0; i < arrayList.Count; i++)
			{
				TemplateControl.EvtInfo evtInfo = (TemplateControl.EvtInfo)arrayList[i];
				if (evtInfo.noParams)
				{
					NoParamsInvoker noParamsInvoker = new NoParamsInvoker(this, evtInfo.method);
					evtInfo.evt.AddEventHandler(this, noParamsInvoker.FakeDelegate);
				}
				else
				{
					evtInfo.evt.AddEventHandler(this, Delegate.CreateDelegate(typeof(EventHandler), this, evtInfo.method));
				}
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		private ArrayList CollectAutomaticEventInfo()
		{
			ArrayList arrayList = new ArrayList();
			foreach (string text in TemplateControl.methodNames)
			{
				MethodInfo methodInfo = null;
				Type type = base.GetType();
				while (type.Assembly != TemplateControl._System_Web_Assembly)
				{
					methodInfo = type.GetMethod(text, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (methodInfo != null)
					{
						break;
					}
					type = type.BaseType;
				}
				if (!(methodInfo == null) && (!(methodInfo.DeclaringType != type) || methodInfo.IsPublic || methodInfo.IsFamilyOrAssembly || methodInfo.IsFamilyAndAssembly || methodInfo.IsFamily) && !(methodInfo.ReturnType != typeof(void)))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					int num = parameters.Length;
					bool flag = num == 0;
					if (flag || (num == 2 && !(parameters[0].ParameterType != typeof(object)) && !(parameters[1].ParameterType != typeof(EventArgs))))
					{
						int num2 = text.IndexOf('_');
						string text2 = text.Substring(num2 + 1);
						EventInfo @event = type.GetEvent(text2);
						if (!(@event == null))
						{
							arrayList.Add(new TemplateControl.EvtInfo
							{
								method = methodInfo,
								methodName = text,
								evt = @event,
								noParams = flag
							});
						}
					}
				}
			}
			return arrayList;
		}

		/// <summary>Initializes the control that is derived from the <see cref="T:System.Web.UI.TemplateControl" /> class.</summary>
		// Token: 0x0600170D RID: 5901 RVA: 0x0000393A File Offset: 0x00001B3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void FrameworkInitialize()
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0003DD68 File Offset: 0x0003BF68
		private Type GetTypeFromControlPath(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			return BuildManager.GetCompiledType(UrlUtils.Combine(this.TemplateSourceDirectory, virtualPath));
		}

		/// <summary>Loads a <see cref="T:System.Web.UI.Control" /> object from a file based on a specified virtual path.</summary>
		/// <returns>Returns the specified <see cref="T:System.Web.UI.Control" />.</returns>
		/// <param name="virtualPath">The virtual path to a control file. </param>
		/// <exception cref="T:System.ArgumentNullException">The virtual path is null or empty.</exception>
		// Token: 0x0600170F RID: 5903 RVA: 0x0003DD8C File Offset: 0x0003BF8C
		public Control LoadControl(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			Type typeFromControlPath = this.GetTypeFromControlPath(virtualPath);
			return this.LoadControl(typeFromControlPath, null);
		}

		/// <summary>Loads a <see cref="T:System.Web.UI.Control" /> object based on a specified type and constructor parameters.</summary>
		/// <returns>Returns the specified <see cref="T:System.Web.UI.UserControl" />.</returns>
		/// <param name="t">The type of the control.</param>
		/// <param name="parameters">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="parameters" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
		// Token: 0x06001710 RID: 5904 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		public Control LoadControl(Type t, object[] parameters)
		{
			object[] array = null;
			if (t != null)
			{
				t.GetCustomAttributes(typeof(PartialCachingAttribute), true);
			}
			if (array != null && array.Length == 1)
			{
				PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)array[0];
				return new PartialCachingControl(t, parameters)
				{
					VaryByParams = partialCachingAttribute.VaryByParams,
					VaryByControls = partialCachingAttribute.VaryByControls,
					VaryByCustom = partialCachingAttribute.VaryByCustom
				};
			}
			object obj = Activator.CreateInstance(t, parameters);
			if (obj is UserControl)
			{
				((UserControl)obj).InitializeAsUserControl(this.Page);
			}
			return (Control)obj;
		}

		/// <summary>Obtains an instance of the <see cref="T:System.Web.UI.ITemplate" /> interface from an external file.</summary>
		/// <returns>An instance of the specified template.</returns>
		/// <param name="virtualPath">The virtual path to a user control file. </param>
		// Token: 0x06001711 RID: 5905 RVA: 0x0003DE47 File Offset: 0x0003C047
		public ITemplate LoadTemplate(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			return new TemplateControl.SimpleTemplate(this.GetTypeFromControlPath(virtualPath));
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.AbortTransaction" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001712 RID: 5906 RVA: 0x0003DE64 File Offset: 0x0003C064
		protected virtual void OnAbortTransaction(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TemplateControl.abortTransaction] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.CommitTransaction" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001713 RID: 5907 RVA: 0x0003DE94 File Offset: 0x0003C094
		protected virtual void OnCommitTransaction(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TemplateControl.commitTransaction] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001714 RID: 5908 RVA: 0x0003DEC4 File Offset: 0x0003C0C4
		protected virtual void OnError(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TemplateControl.error] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Parses an input string into a <see cref="T:System.Web.UI.Control" /> object on the Web Forms page or user control.</summary>
		/// <returns>The parsed <see cref="T:System.Web.UI.Control" />.</returns>
		/// <param name="content">A string that contains a user control. </param>
		// Token: 0x06001715 RID: 5909 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		public Control ParseControl(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			Type compiledType = UserControlParser.GetCompiledType(new StringReader(content), new int?(content.GetHashCode()), HttpContext.Current);
			if (compiledType == null)
			{
				return null;
			}
			TemplateControl templateControl = Activator.CreateInstance(compiledType, null) as TemplateControl;
			if (templateControl == null)
			{
				return null;
			}
			if (this is Page)
			{
				templateControl.Page = (Page)this;
			}
			templateControl.FrameworkInitialize();
			Control control = new Control();
			int count = templateControl.Controls.Count;
			Control[] array = new Control[count];
			templateControl.Controls.CopyTo(array, 0);
			for (int i = 0; i < count; i++)
			{
				control.Controls.Add(array[i]);
			}
			return control;
		}

		/// <summary>Parses an input string into a <see cref="T:System.Web.UI.Control" /> object on the ASP.NET Web page or user control.</summary>
		/// <returns>The parsed control.</returns>
		/// <param name="content">A string that contains a user control.</param>
		/// <param name="ignoreParserFilter">A value that specifies whether to ignore the parser filter.</param>
		// Token: 0x06001716 RID: 5910 RVA: 0x0003DFAD File Offset: 0x0003C1AD
		[global::System.MonoTODO("Parser filters not implemented yet. Calls ParseControl (string) for now.")]
		public Control ParseControl(string content, bool ignoreParserFilter)
		{
			return this.ParseControl(content);
		}

		/// <summary>Reads a string resource. The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource" /> method is not intended for use from within your code.</summary>
		/// <returns>An object representing the resource.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource" /> is no longer supported.</exception>
		// Token: 0x06001717 RID: 5911 RVA: 0x0003DFB6 File Offset: 0x0003C1B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object ReadStringResource()
		{
			return TemplateControl.ReadStringResource(base.GetType());
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties. </summary>
		/// <returns>An object representing the requested resource object; otherwise, null.</returns>
		/// <param name="className">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" />.</param>
		/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
		// Token: 0x06001718 RID: 5912 RVA: 0x0000FD29 File Offset: 0x0000DF29
		protected object GetGlobalResourceObject(string className, string resourceKey)
		{
			return HttpContext.GetGlobalResourceObject(className, resourceKey);
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, object type, and property name of the resource.</summary>
		/// <returns>An object representing the requested resource object; otherwise, null.</returns>
		/// <param name="className">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" />. </param>
		/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />. </param>
		/// <param name="objType">The type of object in the resource to get. </param>
		/// <param name="propName">The property name of the object to get.</param>
		// Token: 0x06001719 RID: 5913 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
		protected object GetGlobalResourceObject(string className, string resourceKey, Type objType, string propName)
		{
			if (string.IsNullOrEmpty(resourceKey) || string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(className) || objType == null)
			{
				return null;
			}
			object globalResourceObject = this.GetGlobalResourceObject(className, resourceKey);
			if (globalResourceObject == null)
			{
				return null;
			}
			TypeConverter converter = TypeDescriptor.GetProperties(objType)[propName].Converter;
			if (converter == null || !converter.CanConvertFrom(globalResourceObject.GetType()))
			{
				return null;
			}
			return converter.ConvertFrom(globalResourceObject);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> property.</summary>
		/// <returns>An object representing the requested resource object; otherwise, null.</returns>
		/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
		// Token: 0x0600171A RID: 5914 RVA: 0x0003E030 File Offset: 0x0003C230
		protected object GetLocalResourceObject(string resourceKey)
		{
			return HttpContext.GetLocalResourceObject(VirtualPathUtility.ToAbsolute(this.AppRelativeVirtualPath), resourceKey);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> property, object type, and property name.</summary>
		/// <returns>An object representing the requested resource object; otherwise, null.</returns>
		/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
		/// <param name="objType">The type of the resource object to get.</param>
		/// <param name="propName">The property name of the resource object to get.</param>
		// Token: 0x0600171B RID: 5915 RVA: 0x0003E044 File Offset: 0x0003C244
		protected object GetLocalResourceObject(string resourceKey, Type objType, string propName)
		{
			if (string.IsNullOrEmpty(resourceKey) || string.IsNullOrEmpty(propName) || objType == null)
			{
				return null;
			}
			object localResourceObject = this.GetLocalResourceObject(resourceKey);
			if (localResourceObject == null)
			{
				return null;
			}
			TypeConverter converter = TypeDescriptor.GetProperties(objType)[propName].Converter;
			if (converter == null || !converter.CanConvertFrom(localResourceObject.GetType()))
			{
				return null;
			}
			return converter.ConvertFrom(localResourceObject);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00002058 File Offset: 0x00000258
		internal override TemplateControl TemplateControlInternal
		{
			get
			{
				return this;
			}
		}

		/// <summary>Reads a string resource. The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource(System.Type)" /> method is not intended for use from within your code.</summary>
		/// <returns>An object representing the resource.</returns>
		/// <param name="t">The <see cref="T:System.Type" /> of the resource to read.</param>
		// Token: 0x0600171D RID: 5917 RVA: 0x0003E0A8 File Offset: 0x0003C2A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static object ReadStringResource(Type t)
		{
			TemplateControl.StringResourceData stringResourceData = new TemplateControl.StringResourceData();
			if (ICalls.GetUnmanagedResourcesPtr(t.Assembly, out stringResourceData.Ptr, out stringResourceData.Length))
			{
				return stringResourceData;
			}
			throw new HttpException("Unable to load the string resources.");
		}

		/// <summary>Sets a pointer to a string resource. The <see cref="M:System.Web.UI.TemplateControl.SetStringResourcePointer(System.Object,System.Int32)" /> method is used by generated classes and is not intended for use from within your code.</summary>
		/// <param name="stringResourcePointer">An object representing the pointer to the string resource.</param>
		/// <param name="maxResourceOffset">The resource size. </param>
		// Token: 0x0600171E RID: 5918 RVA: 0x0003E0E0 File Offset: 0x0003C2E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SetStringResourcePointer(object stringResourcePointer, int maxResourceOffset)
		{
			TemplateControl.StringResourceData stringResourceData = stringResourcePointer as TemplateControl.StringResourceData;
			if (stringResourceData == null)
			{
				return;
			}
			if (maxResourceOffset < 0 || maxResourceOffset > stringResourceData.Length)
			{
				throw new ArgumentOutOfRangeException("maxResourceOffset");
			}
			this.resource_data = new TemplateControl.StringResourceData();
			this.resource_data.Ptr = stringResourceData.Ptr;
			this.resource_data.Length = stringResourceData.Length;
			this.resource_data.MaxOffset = ((maxResourceOffset > 0) ? Math.Min(maxResourceOffset, stringResourceData.Length) : stringResourceData.Length);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0003E160 File Offset: 0x0003C360
		private static IntPtr AddOffset(IntPtr ptr, int offset)
		{
			if (offset == 0)
			{
				return ptr;
			}
			if (IntPtr.Size == 4)
			{
				int num = ptr.ToInt32() + offset;
				ptr = new IntPtr(num);
			}
			else
			{
				long num2 = ptr.ToInt64() + (long)offset;
				ptr = new IntPtr(num2);
			}
			return ptr;
		}

		/// <summary>Writes a resource string to an <see cref="T:System.Web.UI.HtmlTextWriter" /> control. The <see cref="M:System.Web.UI.TemplateControl.WriteUTF8ResourceString(System.Web.UI.HtmlTextWriter,System.Int32,System.Int32,System.Boolean)" /> method is used by generated classes and is not intended for use from within your code.</summary>
		/// <param name="output">The control to write to.</param>
		/// <param name="offset">The starting position within <paramref name="value" />.</param>
		/// <param name="size">The number of characters within <paramref name="value" /> to use.</param>
		/// <param name="fAsciiOnly">true to bypass re-encoding; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Data that is not valid is being accessed; <paramref name="offset" /> or <paramref name="size" /> is less than zero.- or -The sum of <paramref name="offset" /> and <paramref name="size" /> is greater than the resource size.</exception>
		// Token: 0x06001720 RID: 5920 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void WriteUTF8ResourceString(HtmlTextWriter output, int offset, int size, bool fAsciiOnly)
		{
			if (this.resource_data == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (offset > this.resource_data.MaxOffset - size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IntPtr intPtr = TemplateControl.AddOffset(this.resource_data.Ptr, offset);
			HttpWriter httpWriter = output.GetHttpWriter();
			if (httpWriter == null || httpWriter.Response.ContentEncoding.CodePage != 65001)
			{
				byte[] array = new byte[size];
				Marshal.Copy(intPtr, array, 0, size);
				output.Write(Encoding.UTF8.GetString(array));
				return;
			}
			httpWriter.WriteUTF8Ptr(intPtr, size);
		}

		/// <summary>Occurs when a user ends a transaction.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001721 RID: 5921 RVA: 0x0003E242 File Offset: 0x0003C442
		// (remove) Token: 0x06001722 RID: 5922 RVA: 0x0003E255 File Offset: 0x0003C455
		[WebSysDescription("Raised when the user aborts a transaction.")]
		public event EventHandler AbortTransaction
		{
			add
			{
				base.Events.AddHandler(TemplateControl.abortTransaction, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.abortTransaction, value);
			}
		}

		/// <summary>Occurs when a transaction completes.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001723 RID: 5923 RVA: 0x0003E268 File Offset: 0x0003C468
		// (remove) Token: 0x06001724 RID: 5924 RVA: 0x0003E27B File Offset: 0x0003C47B
		[WebSysDescription("Raised when the user initiates a transaction.")]
		public event EventHandler CommitTransaction
		{
			add
			{
				base.Events.AddHandler(TemplateControl.commitTransaction, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.commitTransaction, value);
			}
		}

		/// <summary>Occurs when an unhandled exception is thrown.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001725 RID: 5925 RVA: 0x0003E28E File Offset: 0x0003C48E
		// (remove) Token: 0x06001726 RID: 5926 RVA: 0x0003E2A1 File Offset: 0x0003C4A1
		[WebSysDescription("Raised when an exception occurs that cannot be handled.")]
		public event EventHandler Error
		{
			add
			{
				base.Events.AddHandler(TemplateControl.error, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.error, value);
			}
		}

		/// <summary>Evaluates a data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
		/// <param name="expression">The navigation path from the container to the public property value to place in the bound control property.</param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null. - or -<paramref name="expression" /> is an empty string ("").</exception>
		// Token: 0x06001727 RID: 5927 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
		protected internal object Eval(string expression)
		{
			return DataBinder.Eval(this.Page.GetDataItem(), expression);
		}

		/// <summary>Evaluates a data-binding expression using the specified format string to display the result.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="expression">The navigation path from the container to the public property value to place in the bound control property.</param>
		/// <param name="format">A .NET Framework format string to apply to the result.</param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can only be used for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null. - or -<paramref name="expression" /> is an empty string ("").</exception>
		// Token: 0x06001728 RID: 5928 RVA: 0x0003E2C7 File Offset: 0x0003C4C7
		protected internal string Eval(string expression, string format)
		{
			return DataBinder.Eval(this.Page.GetDataItem(), expression, format);
		}

		/// <summary>Evaluates an XPath data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		// Token: 0x06001729 RID: 5929 RVA: 0x0003E2DB File Offset: 0x0003C4DB
		protected internal object XPath(string xPathExpression)
		{
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression. </returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		// Token: 0x0600172A RID: 5930 RVA: 0x0003E2EE File Offset: 0x0003C4EE
		protected internal object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, null, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified format string to display the result. </summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		// Token: 0x0600172B RID: 5931 RVA: 0x0003E303 File Offset: 0x0003C503
		protected internal string XPath(string xPathExpression, string format)
		{
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, format);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and the specified format string to display the result.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution. </param>
		/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
		// Token: 0x0600172C RID: 5932 RVA: 0x0003E317 File Offset: 0x0003C517
		protected internal string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
		{
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, format, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> node list. </returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		// Token: 0x0600172D RID: 5933 RVA: 0x0003E32C File Offset: 0x0003C52C
		protected internal IEnumerable XPathSelect(string xPathExpression)
		{
			return XPathBinder.Select(this.Page.GetDataItem(), xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> node list. </returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution. </param>
		// Token: 0x0600172E RID: 5934 RVA: 0x0003E33F File Offset: 0x0003C53F
		protected internal IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return XPathBinder.Select(this.Page.GetDataItem(), xPathExpression, resolver);
		}

		/// <summary>Returns a value that indicates whether a parent/child relationship exists between two specified device filters. </summary>
		/// <returns>1, if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1, if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; otherwise, 0, if there is no parent/child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
		/// <param name="filter1">A device filter name. </param>
		/// <param name="filter2">A device filter name. </param>
		// Token: 0x0600172F RID: 5935 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value that indicates whether the specified filter is a type of the current filter object.</summary>
		/// <returns>true if the specified filter is a type applicable to the current filter object; otherwise, false.</returns>
		/// <param name="filterName">The name of a device filter.</param>
		// Token: 0x06001730 RID: 5936 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a Boolean value indicating whether a device filter applies to the HTTP request.</summary>
		/// <returns>true if the client browser specified in <paramref name="filterName" /> is the same as the specified browser; otherwise, false. The default is false.</returns>
		/// <param name="filterName">The browser name to test. </param>
		// Token: 0x06001732 RID: 5938 RVA: 0x0003E42C File Offset: 0x0003C62C
		public virtual bool TestDeviceFilter(string filterName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001595 RID: 5525
		private static readonly Assembly _System_Web_Assembly = typeof(TemplateControl).Assembly;

		// Token: 0x04001596 RID: 5526
		private static object abortTransaction = new object();

		// Token: 0x04001597 RID: 5527
		private static object commitTransaction = new object();

		// Token: 0x04001598 RID: 5528
		private static object error = new object();

		// Token: 0x04001599 RID: 5529
		private static string[] methodNames = new string[]
		{
			"Page_Init", "Page_PreInit", "Page_PreLoad", "Page_LoadComplete", "Page_PreRenderComplete", "Page_SaveStateComplete", "Page_InitComplete", "Page_Load", "Page_DataBind", "Page_PreRender",
			"Page_Disposed", "Page_Error", "Page_Unload", "Page_AbortTransaction", "Page_CommitTransaction"
		};

		// Token: 0x0400159A RID: 5530
		private const BindingFlags bflags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400159B RID: 5531
		private string _appRelativeVirtualPath;

		// Token: 0x0400159C RID: 5532
		private TemplateControl.StringResourceData resource_data;

		// Token: 0x0400159D RID: 5533
		private static SplitOrderedList<Type, ArrayList> auto_event_info = new SplitOrderedList<Type, ArrayList>(EqualityComparer<Type>.Default);

		// Token: 0x02000233 RID: 563
		private class EvtInfo
		{
			// Token: 0x0400159E RID: 5534
			public MethodInfo method;

			// Token: 0x0400159F RID: 5535
			public string methodName;

			// Token: 0x040015A0 RID: 5536
			public EventInfo evt;

			// Token: 0x040015A1 RID: 5537
			public bool noParams;
		}

		// Token: 0x02000234 RID: 564
		private class StringResourceData
		{
			// Token: 0x040015A2 RID: 5538
			public IntPtr Ptr;

			// Token: 0x040015A3 RID: 5539
			public int Length;

			// Token: 0x040015A4 RID: 5540
			public int MaxOffset;
		}

		// Token: 0x02000235 RID: 565
		private class SimpleTemplate : ITemplate
		{
			// Token: 0x06001735 RID: 5941 RVA: 0x0003E447 File Offset: 0x0003C647
			public SimpleTemplate(Type type)
			{
				this.type = type;
			}

			// Token: 0x06001736 RID: 5942 RVA: 0x0003E458 File Offset: 0x0003C658
			public void InstantiateIn(Control control)
			{
				Control control2 = Activator.CreateInstance(this.type) as Control;
				control2.SetBindingContainer(false);
				control.Controls.Add(control2);
			}

			// Token: 0x040015A5 RID: 5541
			private Type type;
		}
	}
}
