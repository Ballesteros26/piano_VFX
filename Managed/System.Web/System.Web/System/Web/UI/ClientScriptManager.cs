using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Handlers;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines methods for managing client scripts in Web applications.</summary>
	// Token: 0x020001AB RID: 427
	public sealed class ClientScriptManager
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0002CD40 File Offset: 0x0002AF40
		internal bool ScriptsPresent
		{
			get
			{
				return this._webFormClientScriptRequired || this._initCallBackRegistered || this._hasRegisteredForEventValidationOnCallback || this.clientScriptBlocks != null || this.startupScriptBlocks != null || this.submitStatements != null || this.registeredArrayDeclares != null || this.expandoAttributes != null;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0002CD90 File Offset: 0x0002AF90
		private Page OwnerPage
		{
			get
			{
				if (this.ownerPage == null)
				{
					throw new InvalidOperationException("Associated Page instance is required to complete this operation.");
				}
				return this.ownerPage;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002CDAB File Offset: 0x0002AFAB
		internal ClientScriptManager(Page page)
		{
			this.ownerPage = page;
		}

		/// <summary>Gets a reference, with javascript: appended to the beginning of it, that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
		/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
		/// <param name="control">The server control to process the postback.</param>
		/// <param name="argument">The parameter passed to the server control. </param>
		// Token: 0x0600106C RID: 4204 RVA: 0x0002CDBA File Offset: 0x0002AFBA
		public string GetPostBackClientHyperlink(Control control, string argument)
		{
			return "javascript:" + this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Gets a reference, with javascript: appended to the beginning of it, that can be used in a client event to post back to the server for the specified control with the specified event arguments and Boolean indication whether to register the post back for event validation.</summary>
		/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
		/// <param name="control">The server control to process the postback.</param>
		/// <param name="argument">The parameter passed to the server control.</param>
		/// <param name="registerForEventValidation">true to register the postback event for validation; false to not register the post back event for validation.</param>
		// Token: 0x0600106D RID: 4205 RVA: 0x0002CDCE File Offset: 0x0002AFCE
		public string GetPostBackClientHyperlink(Control control, string argument, bool registerForEventValidation)
		{
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(control.UniqueID, argument);
			}
			return "javascript:" + this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
		/// <param name="argument">A string of optional arguments to pass to the control that processes the postback.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is null.</exception>
		// Token: 0x0600106E RID: 4206 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public string GetPostBackEventReference(Control control, string argument)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Page page = this.OwnerPage;
			page.RequiresPostBackScript();
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { page.theForm, ".__doPostBack('", control.UniqueID, "','", argument, "')" });
			}
			return string.Concat(new string[] { "__doPostBack('", control.UniqueID, "','", argument, "')" });
		}

		/// <summary>Returns a string to use in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information. Optionally, registers the event reference for validation.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
		/// <param name="argument">A string of optional arguments to pass to <paramref name="control" />.</param>
		/// <param name="registerForEventValidation">true to register the event reference for validation; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is null.</exception>
		// Token: 0x0600106F RID: 4207 RVA: 0x0002CE8E File Offset: 0x0002B08E
		public string GetPostBackEventReference(Control control, string argument, bool registerForEventValidation)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(control.UniqueID, argument);
			}
			return this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> object. Optionally, registers the event reference for validation.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
		/// <param name="registerForEventValidation">true to register the event reference for validation; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> is null.</exception>
		// Token: 0x06001070 RID: 4208 RVA: 0x0002CEB6 File Offset: 0x0002B0B6
		public string GetPostBackEventReference(PostBackOptions options, bool registerForEventValidation)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(options);
			}
			return this.GetPostBackEventReference(options);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> instance.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> parameter is null</exception>
		// Token: 0x06001071 RID: 4209 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public string GetPostBackEventReference(PostBackOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string actionUrl = options.ActionUrl;
			if (actionUrl != null || options.ValidationGroup != null || options.TrackFocus || options.AutoPostBack || options.PerformValidation)
			{
				this.RegisterWebFormClientScript();
				Page page = this.OwnerPage;
				HttpRequest requestInternal = page.RequestInternal;
				Uri uri = ((requestInternal != null) ? requestInternal.Url : null);
				if (uri != null)
				{
					this.RegisterHiddenField("__PREVIOUSPAGE", uri.AbsolutePath);
				}
				if (options.TrackFocus)
				{
					this.RegisterHiddenField("__LASTFOCUS", string.Empty);
				}
				string text = (options.RequiresJavaScriptProtocol ? "javascript:" : string.Empty);
				if (page.IsMultiForm)
				{
					text = text + page.theForm + ".";
				}
				return string.Concat(new string[]
				{
					text,
					"WebForm_DoPostback(",
					ClientScriptManager.GetScriptLiteral(options.TargetControl.UniqueID),
					",",
					ClientScriptManager.GetScriptLiteral(options.Argument),
					",",
					ClientScriptManager.GetScriptLiteral(actionUrl),
					",",
					ClientScriptManager.GetScriptLiteral(options.AutoPostBack),
					",",
					ClientScriptManager.GetScriptLiteral(options.PerformValidation),
					",",
					ClientScriptManager.GetScriptLiteral(options.TrackFocus),
					",",
					ClientScriptManager.GetScriptLiteral(options.ClientSubmit),
					",",
					ClientScriptManager.GetScriptLiteral(options.ValidationGroup),
					")"
				});
			}
			if (!options.ClientSubmit)
			{
				return null;
			}
			if (options.RequiresJavaScriptProtocol)
			{
				return this.GetPostBackClientHyperlink(options.TargetControl, options.Argument);
			}
			return this.GetPostBackEventReference(options.TargetControl, options.Argument);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002D0C0 File Offset: 0x0002B2C0
		internal void RegisterWebFormClientScript()
		{
			if (this._webFormClientScriptRequired)
			{
				return;
			}
			this.OwnerPage.RequiresPostBackScript();
			this._webFormClientScriptRequired = true;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
		internal void WriteWebFormClientScript(HtmlTextWriter writer)
		{
			if (!this._webFormClientScriptRendered && this._webFormClientScriptRequired)
			{
				Page page = this.OwnerPage;
				writer.WriteLine();
				this.WriteClientScriptInclude(writer, this.GetWebResourceUrl(typeof(Page), "webform.js"), typeof(Page), "webform.js");
				ClientScriptManager.WriteBeginScriptBlock(writer);
				writer.WriteLine("WebForm_Initialize({0});", page.IsMultiForm ? page.theForm : "window");
				ClientScriptManager.WriteEndScriptBlock(writer);
				this._webFormClientScriptRendered = true;
			}
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to a server event. The client function for this overloaded method includes a specified control, argument, client script, and context.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001074 RID: 4212 RVA: 0x0002D168 File Offset: 0x0002B368
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, null, false);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <param name="useAsync">true to perform the callback asynchronously; false to perform the callback synchronously.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001075 RID: 4213 RVA: 0x0002D177 File Offset: 0x0002B377
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, bool useAsync)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, null, useAsync);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, error handler, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
		/// <param name="useAsync">true to perform the callback asynchronously; false to perform the callback synchronously. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001076 RID: 4214 RVA: 0x0002D188 File Offset: 0x0002B388
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!(control is ICallbackEventHandler))
			{
				throw new InvalidOperationException("The control must implement the ICallbackEventHandler interface and provide a RaiseCallbackEvent method.");
			}
			return this.GetCallbackEventReference("'" + control.UniqueID + "'", argument, clientCallback, context, clientErrorCallback, useAsync);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified target, argument, client script, context, error handler, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="target">The name of a server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method.</param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler.</param>
		/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
		/// <param name="useAsync">true  to perform the callback asynchronously; false to perform the callback synchronously.</param>
		// Token: 0x06001077 RID: 4215 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		public string GetCallbackEventReference(string target, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			this.RegisterWebFormClientScript();
			Page page = this.OwnerPage;
			if (!this._initCallBackRegistered)
			{
				this._initCallBackRegistered = true;
				this.RegisterStartupScript(typeof(Page), "WebForm_InitCallback", page.WebFormScriptReference + ".WebForm_InitCallback();", true);
			}
			return string.Concat(new string[]
			{
				page.WebFormScriptReference,
				".WebForm_DoCallback(",
				target,
				",",
				argument ?? "null",
				",",
				clientCallback,
				",",
				context ?? "null",
				",",
				clientErrorCallback ?? "null",
				",",
				useAsync ? "true" : "false",
				")"
			});
		}

		/// <summary>Gets a URL reference to a resource in an assembly.</summary>
		/// <returns>The URL reference to the resource.</returns>
		/// <param name="type">The type of the resource. </param>
		/// <param name="resourceName">The fully qualified name of the resource in the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The web resource type is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The web resource name is null.- or -The web resource name has a length of zero.</exception>
		// Token: 0x06001078 RID: 4216 RVA: 0x0002D2C1 File Offset: 0x0002B4C1
		public string GetWebResourceUrl(Type type, string resourceName)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (resourceName == null || resourceName.Length == 0)
			{
				throw new ArgumentNullException("type");
			}
			return AssemblyResourceLoader.GetResourceUrl(type, resourceName);
		}

		/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the client script block is registered; otherwise, false.</returns>
		/// <param name="key">The key of the client script block to search for.</param>
		// Token: 0x06001079 RID: 4217 RVA: 0x0002D2F4 File Offset: 0x0002B4F4
		public bool IsClientScriptBlockRegistered(string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, base.GetType(), key);
		}

		/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
		/// <returns>true if the client script block is registered; otherwise, false.</returns>
		/// <param name="type">The type of the client script block to search for.  </param>
		/// <param name="key">The key of the client script block to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script type is null.</exception>
		// Token: 0x0600107A RID: 4218 RVA: 0x0002D309 File Offset: 0x0002B509
		public bool IsClientScriptBlockRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, type, key);
		}

		/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key.</summary>
		/// <returns>true if the startup script is registered; otherwise, false.</returns>
		/// <param name="key">The key of the startup script to search for.</param>
		// Token: 0x0600107B RID: 4219 RVA: 0x0002D319 File Offset: 0x0002B519
		public bool IsStartupScriptRegistered(string key)
		{
			return this.IsScriptRegistered(this.startupScriptBlocks, base.GetType(), key);
		}

		/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
		/// <returns>true if the startup script is registered; otherwise, false.</returns>
		/// <param name="type">The type of the startup script to search for. </param>
		/// <param name="key">The key of the startup script to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">The startup script type is null.</exception>
		// Token: 0x0600107C RID: 4220 RVA: 0x0002D32E File Offset: 0x0002B52E
		public bool IsStartupScriptRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.startupScriptBlocks, type, key);
		}

		/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the OnSubmit statement is registered; otherwise, false.</returns>
		/// <param name="key">The key of the OnSubmit statement to search for.</param>
		// Token: 0x0600107D RID: 4221 RVA: 0x0002D33E File Offset: 0x0002B53E
		public bool IsOnSubmitStatementRegistered(string key)
		{
			return this.IsScriptRegistered(this.submitStatements, base.GetType(), key);
		}

		/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
		/// <returns>true if the OnSubmit statement is registered; otherwise, false.</returns>
		/// <param name="type">The type of the OnSubmit statement to search for. </param>
		/// <param name="key">The key of the OnSubmit statement to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The OnSubmit statement type is null.</exception>
		// Token: 0x0600107E RID: 4222 RVA: 0x0002D353 File Offset: 0x0002B553
		public bool IsOnSubmitStatementRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.submitStatements, type, key);
		}

		/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the client script include is registered; otherwise, false.</returns>
		/// <param name="key">The key of the client script include to search for. </param>
		// Token: 0x0600107F RID: 4223 RVA: 0x0002D363 File Offset: 0x0002B563
		public bool IsClientScriptIncludeRegistered(string key)
		{
			return this.IsClientScriptIncludeRegistered(base.GetType(), key);
		}

		/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
		/// <returns>true if the client script include is registered; otherwise, false.</returns>
		/// <param name="type">The type of the client script include to search for. </param>
		/// <param name="key">The key of the client script include to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script include type is null.</exception>
		// Token: 0x06001080 RID: 4224 RVA: 0x0002D372 File Offset: 0x0002B572
		public bool IsClientScriptIncludeRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, type, "include-" + key);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002D38C File Offset: 0x0002B58C
		private bool IsScriptRegistered(ClientScriptManager.ScriptEntry scriptList, Type type, string key)
		{
			while (scriptList != null)
			{
				if (scriptList.Type == type && scriptList.Key == key)
				{
					return true;
				}
				scriptList = scriptList.Next;
			}
			return false;
		}

		/// <summary>Registers a JavaScript array declaration with the <see cref="T:System.Web.UI.Page" /> object using an array name and array value.</summary>
		/// <param name="arrayName">The array name to register.</param>
		/// <param name="arrayValue">The array value or values to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayName" /> is null.</exception>
		// Token: 0x06001082 RID: 4226 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		public void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			if (this.registeredArrayDeclares == null)
			{
				this.registeredArrayDeclares = new Hashtable();
			}
			if (!this.registeredArrayDeclares.ContainsKey(arrayName))
			{
				this.registeredArrayDeclares.Add(arrayName, new ArrayList());
			}
			((ArrayList)this.registeredArrayDeclares[arrayName]).Add(arrayValue);
			this.OwnerPage.RequiresFormScriptDeclaration();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002D41E File Offset: 0x0002B61E
		private void RegisterScript(ref ClientScriptManager.ScriptEntry scriptList, Type type, string key, string script, bool addScriptTags)
		{
			this.RegisterScript(ref scriptList, type, key, script, addScriptTags ? ClientScriptManager.ScriptEntryFormat.AddScriptTag : ClientScriptManager.ScriptEntryFormat.None);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002D434 File Offset: 0x0002B634
		private void RegisterScript(ref ClientScriptManager.ScriptEntry scriptList, Type type, string key, string script, ClientScriptManager.ScriptEntryFormat format)
		{
			ClientScriptManager.ScriptEntry scriptEntry = null;
			ClientScriptManager.ScriptEntry scriptEntry2;
			for (scriptEntry2 = scriptList; scriptEntry2 != null; scriptEntry2 = scriptEntry2.Next)
			{
				if (scriptEntry2.Type == type && scriptEntry2.Key == key)
				{
					return;
				}
				scriptEntry = scriptEntry2;
			}
			scriptEntry2 = new ClientScriptManager.ScriptEntry(type, key, script, format);
			if (scriptEntry != null)
			{
				scriptEntry.Next = scriptEntry2;
				return;
			}
			scriptList = scriptEntry2;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002D48B File Offset: 0x0002B68B
		internal void RegisterClientScriptBlock(string key, string script)
		{
			this.RegisterScript(ref this.clientScriptBlocks, base.GetType(), key, script, false);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, and script literal.</summary>
		/// <param name="type">The type of the client script to register. </param>
		/// <param name="key">The key of the client script to register. </param>
		/// <param name="script">The client script literal to register. </param>
		// Token: 0x06001086 RID: 4230 RVA: 0x0002D4A2 File Offset: 0x0002B6A2
		public void RegisterClientScriptBlock(Type type, string key, string script)
		{
			this.RegisterClientScriptBlock(type, key, script, false);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, script literal, and Boolean value indicating whether to add script tags.</summary>
		/// <param name="type">The type of the client script to register. </param>
		/// <param name="key">The key of the client script to register. </param>
		/// <param name="script">The client script literal to register.  </param>
		/// <param name="addScriptTags">A Boolean value indicating whether to add script tags.</param>
		/// <exception cref="T:System.ArgumentNullException">The client script block type is null.</exception>
		// Token: 0x06001087 RID: 4231 RVA: 0x0002D4AE File Offset: 0x0002B6AE
		public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.clientScriptBlocks, type, key, script, addScriptTags);
		}

		/// <summary>Registers a hidden value with the <see cref="T:System.Web.UI.Page" /> object.</summary>
		/// <param name="hiddenFieldName">The name of the hidden field to register.</param>
		/// <param name="hiddenFieldInitialValue">The initial value of the field to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hiddenFieldName" /> is null.</exception>
		// Token: 0x06001088 RID: 4232 RVA: 0x0002D4D5 File Offset: 0x0002B6D5
		public void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			if (this.hiddenFields == null)
			{
				this.hiddenFields = new Hashtable();
			}
			if (!this.hiddenFields.ContainsKey(hiddenFieldName))
			{
				this.hiddenFields.Add(hiddenFieldName, hiddenFieldInitialValue);
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002D505 File Offset: 0x0002B705
		internal void RegisterOnSubmitStatement(string key, string script)
		{
			this.RegisterScript(ref this.submitStatements, base.GetType(), key, script, false);
		}

		/// <summary>Registers an OnSubmit statement with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal. The statement executes when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is submitted.</summary>
		/// <param name="type">The type of the OnSubmit statement to register. </param>
		/// <param name="key">The key of the OnSubmit statement to register. </param>
		/// <param name="script">The script literal of the OnSubmit statement to register. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600108A RID: 4234 RVA: 0x0002D51C File Offset: 0x0002B71C
		public void RegisterOnSubmitStatement(Type type, string key, string script)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.submitStatements, type, key, script, false);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0002D542 File Offset: 0x0002B742
		internal void RegisterStartupScript(string key, string script)
		{
			this.RegisterScript(ref this.startupScriptBlocks, base.GetType(), key, script, false);
		}

		/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal.</summary>
		/// <param name="type">The type of the startup script to register. </param>
		/// <param name="key">The key of the startup script to register. </param>
		/// <param name="script">The startup script literal to register. </param>
		// Token: 0x0600108C RID: 4236 RVA: 0x0002D559 File Offset: 0x0002B759
		public void RegisterStartupScript(Type type, string key, string script)
		{
			this.RegisterStartupScript(type, key, script, false);
		}

		/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, a script literal, and a Boolean value indicating whether to add script tags.</summary>
		/// <param name="type">The type of the startup script to register. </param>
		/// <param name="key">The key of the startup script to register. </param>
		/// <param name="script">The startup script literal to register. </param>
		/// <param name="addScriptTags">A Boolean value indicating whether to add script tags. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600108D RID: 4237 RVA: 0x0002D565 File Offset: 0x0002B765
		public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.startupScriptBlocks, type, key, script, addScriptTags);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a key and a URL, which enables the script to be called from the client.</summary>
		/// <param name="key">The key of the client script include to register. </param>
		/// <param name="url">The URL of the client script include to register. </param>
		// Token: 0x0600108E RID: 4238 RVA: 0x0002D58C File Offset: 0x0002B78C
		public void RegisterClientScriptInclude(string key, string url)
		{
			this.RegisterClientScriptInclude(base.GetType(), key, url);
		}

		/// <summary>Registers the client script include with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a URL.</summary>
		/// <param name="type">The type of the client script include to register. </param>
		/// <param name="key">The key of the client script include to register. </param>
		/// <param name="url">The URL of the client script include to register. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script include type is null.</exception>
		/// <exception cref="T:System.ArgumentException">The URL is null. - or -The URL is empty.</exception>
		// Token: 0x0600108F RID: 4239 RVA: 0x0002D59C File Offset: 0x0002B79C
		public void RegisterClientScriptInclude(Type type, string key, string url)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (url == null || url.Length == 0)
			{
				throw new ArgumentException("url");
			}
			this.RegisterScript(ref this.clientScriptBlocks, type, "include-" + key, url, ClientScriptManager.ScriptEntryFormat.Include);
		}

		/// <summary>Registers the client script resource with the <see cref="T:System.Web.UI.Page" /> object using a type and a resource name.</summary>
		/// <param name="type">The type of the client script resource to register. </param>
		/// <param name="resourceName">The name of the client script resource to register. </param>
		/// <exception cref="T:System.ArgumentNullException">The client resource type is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The client resource name is null.- or -The client resource name has a length of zero.</exception>
		// Token: 0x06001090 RID: 4240 RVA: 0x0002D5ED File Offset: 0x0002B7ED
		public void RegisterClientScriptResource(Type type, string resourceName)
		{
			this.RegisterScript(ref this.clientScriptBlocks, type, "resource-" + resourceName, this.GetWebResourceUrl(type, resourceName), ClientScriptManager.ScriptEntryFormat.Include);
		}

		/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, attribute name, and attribute value.</summary>
		/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute. </param>
		/// <param name="attributeName">The name of the custom attribute to register. </param>
		/// <param name="attributeValue">The value of the custom attribute. </param>
		// Token: 0x06001091 RID: 4241 RVA: 0x0002D610 File Offset: 0x0002B810
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, true);
		}

		/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, an attribute name, an attribute value, and a Boolean value indicating whether to encode the attribute value.</summary>
		/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute.</param>
		/// <param name="attributeName">The name of the custom attribute to register.</param>
		/// <param name="attributeValue">The value of the custom attribute.</param>
		/// <param name="encode">A Boolean value indicating whether to encode the custom attribute to register.</param>
		// Token: 0x06001092 RID: 4242 RVA: 0x0002D61C File Offset: 0x0002B81C
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			if (controlId == null)
			{
				throw new ArgumentNullException("controlId");
			}
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			if (this.expandoAttributes == null)
			{
				this.expandoAttributes = new Hashtable();
			}
			ListDictionary listDictionary = (ListDictionary)this.expandoAttributes[controlId];
			if (listDictionary == null)
			{
				listDictionary = new ListDictionary();
				this.expandoAttributes[controlId] = listDictionary;
			}
			listDictionary.Add(attributeName, encode ? StrUtils.EscapeQuotesAndBackslashes(attributeValue) : attributeValue);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002D694 File Offset: 0x0002B894
		private void EnsureEventValidationArray()
		{
			if (this.eventValidationValues == null || this.eventValidationValues.Length == 0)
			{
				this.eventValidationValues = new int[64];
			}
			int num = this.eventValidationValues.Length;
			if (this.eventValidationPos >= num)
			{
				int[] array = new int[num * 2];
				Array.Copy(this.eventValidationValues, array, num);
				this.eventValidationValues = array;
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002D6EE File Offset: 0x0002B8EE
		internal void ResetEventValidationState()
		{
			this._pageInRender = true;
			this.eventValidationPos = 0;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002D700 File Offset: 0x0002B900
		private int CalculateEventHash(string uniqueId, string argument)
		{
			int hashCode = uniqueId.GetHashCode();
			int num = (string.IsNullOrEmpty(argument) ? 0 : argument.GetHashCode());
			return hashCode ^ num;
		}

		/// <summary>Registers an event reference for validation with <see cref="T:System.Web.UI.PostBackOptions" />.</summary>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> object that specifies how client JavaScript is generated to initiate a postback event.</param>
		// Token: 0x06001096 RID: 4246 RVA: 0x0002D727 File Offset: 0x0002B927
		public void RegisterForEventValidation(PostBackOptions options)
		{
			this.RegisterForEventValidation(options.TargetControl.UniqueID, options.Argument);
		}

		/// <summary>Registers an event reference for validation with a unique control ID representing the client control generating the event.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		// Token: 0x06001097 RID: 4247 RVA: 0x0002D740 File Offset: 0x0002B940
		public void RegisterForEventValidation(string uniqueId)
		{
			this.RegisterForEventValidation(uniqueId, null);
		}

		/// <summary>Registers an event reference for validation with a unique control ID and event arguments representing the client control generating the event.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		/// <param name="argument">Event arguments passed with the client event.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called prior to the <see cref="M:System.Web.UI.Page.Render(System.Web.UI.HtmlTextWriter)" /> method.</exception>
		// Token: 0x06001098 RID: 4248 RVA: 0x0002D74C File Offset: 0x0002B94C
		public void RegisterForEventValidation(string uniqueId, string argument)
		{
			Page page = this.OwnerPage;
			if (!page.EnableEventValidation)
			{
				return;
			}
			if (uniqueId == null || uniqueId.Length == 0)
			{
				return;
			}
			if (page.IsCallback)
			{
				this._hasRegisteredForEventValidationOnCallback = true;
			}
			else if (!this._pageInRender)
			{
				throw new InvalidOperationException("RegisterForEventValidation may only be called from the Render method");
			}
			this.EnsureEventValidationArray();
			int num = this.CalculateEventHash(uniqueId, argument);
			for (int i = 0; i < this.eventValidationPos; i++)
			{
				if (this.eventValidationValues[i] == num)
				{
					return;
				}
			}
			int[] array = this.eventValidationValues;
			int num2 = this.eventValidationPos;
			this.eventValidationPos = num2 + 1;
			array[num2] = num;
		}

		/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String)" /> method.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		// Token: 0x06001099 RID: 4249 RVA: 0x0002D7DE File Offset: 0x0002B9DE
		public void ValidateEvent(string uniqueId)
		{
			this.ValidateEvent(uniqueId, null);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		private ArgumentException InvalidPostBackException()
		{
			return new ArgumentException("Invalid postback or callback argument. Event validation is enabled using <pages enableEventValidation=\"true\"/> in configuration or <%@ Page EnableEventValidation=\"true\" %> in a page. For security purposes, this feature verifies that arguments to postback or callback events originate from the server control that originally rendered them. If the data is valid and expected, use the ClientScriptManager.RegisterForEventValidation method in order to register the postback or callback data for validation.");
		}

		/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String,System.String)" /> method.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		/// <param name="argument">The event arguments passed with the client event.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uniqueId" /> is null or an empty string ("").</exception>
		// Token: 0x0600109B RID: 4251 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		public void ValidateEvent(string uniqueId, string argument)
		{
			if (uniqueId == null || uniqueId.Length == 0)
			{
				throw new ArgumentException("must not be null or empty", "uniqueId");
			}
			if (!this.OwnerPage.EnableEventValidation)
			{
				return;
			}
			if (this.eventValidationValues == null)
			{
				throw this.InvalidPostBackException();
			}
			int num = this.CalculateEventHash(uniqueId, argument);
			for (int i = 0; i < this.eventValidationValues.Length; i++)
			{
				if (this.eventValidationValues[i] == num)
				{
					return;
				}
			}
			throw this.InvalidPostBackException();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002D868 File Offset: 0x0002BA68
		private void WriteScripts(HtmlTextWriter writer, ClientScriptManager.ScriptEntry scriptList)
		{
			if (scriptList == null)
			{
				return;
			}
			writer.WriteLine();
			while (scriptList != null)
			{
				ClientScriptManager.ScriptEntryFormat format = scriptList.Format;
				if (format != ClientScriptManager.ScriptEntryFormat.AddScriptTag)
				{
					if (format != ClientScriptManager.ScriptEntryFormat.Include)
					{
						this.EnsureEndScriptBlock(writer);
						writer.WriteLine(scriptList.Script);
					}
					else
					{
						this.EnsureEndScriptBlock(writer);
						this.WriteClientScriptInclude(writer, scriptList.Script, scriptList.Type, scriptList.Key);
					}
				}
				else
				{
					this.EnsureBeginScriptBlock(writer);
					writer.Write(scriptList.Script);
				}
				scriptList = scriptList.Next;
			}
			this.EnsureEndScriptBlock(writer);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002D8EE File Offset: 0x0002BAEE
		private void EnsureBeginScriptBlock(HtmlTextWriter writer)
		{
			if (!this._scriptTagOpened)
			{
				ClientScriptManager.WriteBeginScriptBlock(writer);
				this._scriptTagOpened = true;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002D905 File Offset: 0x0002BB05
		private void EnsureEndScriptBlock(HtmlTextWriter writer)
		{
			if (this._scriptTagOpened)
			{
				ClientScriptManager.WriteEndScriptBlock(writer);
				this._scriptTagOpened = false;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002D91C File Offset: 0x0002BB1C
		internal void RestoreEventValidationState(string fieldValue)
		{
			Page page = this.OwnerPage;
			if (!page.EnableEventValidation || fieldValue == null || fieldValue.Length == 0)
			{
				return;
			}
			IStateFormatter formatter = page.GetFormatter();
			this.eventValidationValues = (int[])formatter.Deserialize(fieldValue);
			this.eventValidationPos = this.eventValidationValues.Length;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002D96C File Offset: 0x0002BB6C
		internal void SaveEventValidationState()
		{
			if (!this.OwnerPage.EnableEventValidation)
			{
				return;
			}
			string eventValidationStateFormatted = this.GetEventValidationStateFormatted();
			if (eventValidationStateFormatted == null)
			{
				return;
			}
			this.RegisterHiddenField("__EVENTVALIDATION", eventValidationStateFormatted);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		internal string GetEventValidationStateFormatted()
		{
			if (this.eventValidationValues == null || this.eventValidationValues.Length == 0)
			{
				return null;
			}
			Page page = this.OwnerPage;
			if (page.IsCallback && !this._hasRegisteredForEventValidationOnCallback)
			{
				return null;
			}
			IStateFormatter formatter = page.GetFormatter();
			int[] array = new int[this.eventValidationPos];
			Array.Copy(this.eventValidationValues, array, this.eventValidationPos);
			return formatter.Serialize(array);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002DA04 File Offset: 0x0002BC04
		internal void WriteExpandoAttributes(HtmlTextWriter writer)
		{
			if (this.expandoAttributes == null)
			{
				return;
			}
			writer.WriteLine();
			ClientScriptManager.WriteBeginScriptBlock(writer);
			foreach (object obj in this.expandoAttributes.Keys)
			{
				string text = (string)obj;
				writer.WriteLine("var {0} = document.all ? document.all [\"{0}\"] : document.getElementById (\"{0}\");", text);
				ListDictionary listDictionary = (ListDictionary)this.expandoAttributes[text];
				foreach (object obj2 in listDictionary.Keys)
				{
					string text2 = (string)obj2;
					writer.WriteLine("{0}.{1} = \"{2}\";", text, text2, listDictionary[text2]);
				}
			}
			ClientScriptManager.WriteEndScriptBlock(writer);
			writer.WriteLine();
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		internal static void WriteBeginScriptBlock(HtmlTextWriter writer)
		{
			writer.WriteLine("<script type=\"text/javascript\">//<![CDATA[");
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002DB09 File Offset: 0x0002BD09
		internal static void WriteEndScriptBlock(HtmlTextWriter writer)
		{
			writer.WriteLine("//]]></script>");
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002DB18 File Offset: 0x0002BD18
		internal void WriteHiddenFields(HtmlTextWriter writer)
		{
			if (this.hiddenFields == null)
			{
				return;
			}
			writer.WriteLine();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "aspNetHidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			int indent = writer.Indent;
			writer.Indent = 0;
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.hiddenFields.Keys)
			{
				string text = (string)obj;
				string text2 = this.hiddenFields[text] as string;
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.WriteLine();
				}
				stringBuilder.Append("<input type=\"hidden\" name=\"");
				stringBuilder.Append(text);
				stringBuilder.Append("\" id=\"");
				stringBuilder.Append(text);
				stringBuilder.Append("\" value=\"");
				stringBuilder.Append(HttpUtility.HtmlAttributeEncode(text2));
				stringBuilder.Append("\" />");
			}
			writer.Write(stringBuilder.ToString());
			writer.Indent = indent;
			writer.RenderEndTag();
			writer.WriteLine();
			this.hiddenFields = null;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002DC48 File Offset: 0x0002BE48
		internal void WriteClientScriptInclude(HtmlTextWriter writer, string path, Type type, string key)
		{
			if (!this.OwnerPage.IsMultiForm)
			{
				writer.WriteLine("<script src=\"{0}\" type=\"text/javascript\"></script>", path);
				return;
			}
			string text = "inc_" + (type.FullName + key).GetHashCode().ToString("X");
			writer.WriteLine("<script type=\"text/javascript\">");
			writer.WriteLine("//<![CDATA[");
			writer.WriteLine("if (!window.{0}) {{", text);
			writer.WriteLine("\twindow.{0} = true", text);
			writer.WriteLine("\tdocument.write('<script src=\"{0}\" type=\"text/javascript\"><\\/script>'); }}", path);
			writer.WriteLine("//]]>");
			writer.WriteLine("</script>");
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002DCE9 File Offset: 0x0002BEE9
		internal void WriteClientScriptBlocks(HtmlTextWriter writer)
		{
			this.WriteScripts(writer, this.clientScriptBlocks);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002DCF8 File Offset: 0x0002BEF8
		internal void WriteStartupScriptBlocks(HtmlTextWriter writer)
		{
			this.WriteScripts(writer, this.startupScriptBlocks);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002DD08 File Offset: 0x0002BF08
		internal void WriteArrayDeclares(HtmlTextWriter writer)
		{
			if (this.registeredArrayDeclares != null)
			{
				writer.WriteLine();
				ClientScriptManager.WriteBeginScriptBlock(writer);
				IDictionaryEnumerator enumerator = this.registeredArrayDeclares.GetEnumerator();
				Page page = this.OwnerPage;
				while (enumerator.MoveNext())
				{
					if (page.IsMultiForm)
					{
						writer.Write("\t" + page.theForm + ".");
					}
					else
					{
						writer.Write("\tvar ");
					}
					writer.Write(enumerator.Key);
					writer.Write(" =  new Array(");
					IEnumerator enumerator2 = ((ArrayList)enumerator.Value).GetEnumerator();
					bool flag = true;
					while (enumerator2.MoveNext())
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							writer.Write(", ");
						}
						writer.Write(enumerator2.Current);
					}
					writer.WriteLine(");");
				}
				ClientScriptManager.WriteEndScriptBlock(writer);
				writer.WriteLine();
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002DDE8 File Offset: 0x0002BFE8
		internal string GetClientValidationEvent(string validationGroup)
		{
			Page page = this.OwnerPage;
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { "if (typeof(", page.theForm, ".Page_ClientValidate) == 'function') ", page.theForm, ".Page_ClientValidate('", validationGroup, "');" });
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('" + validationGroup + "');";
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0002DE58 File Offset: 0x0002C058
		internal string GetClientValidationEvent()
		{
			Page page = this.OwnerPage;
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { "if (typeof(", page.theForm, ".Page_ClientValidate) == 'function') ", page.theForm, ".Page_ClientValidate();" });
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate();";
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		internal string WriteSubmitStatements()
		{
			if (this.submitStatements == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (ClientScriptManager.ScriptEntry next = this.submitStatements; next != null; next = next.Next)
			{
				stringBuilder.Append(ClientScriptManager.EnsureEndsWithSemicolon(next.Script));
			}
			Page page = this.OwnerPage;
			this.RegisterClientScriptBlock(base.GetType(), "HtmlForm-OnSubmitStatemen", string.Concat(new string[]
			{
				"\n",
				page.WebFormScriptReference,
				".WebForm_OnSubmit = function () {\n",
				stringBuilder.ToString(),
				"\nreturn true;\n}\n"
			}), true);
			return "javascript:return " + page.WebFormScriptReference + ".WebForm_OnSubmit();";
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002DF58 File Offset: 0x0002C158
		internal static string GetScriptLiteral(object ob)
		{
			if (ob == null)
			{
				return "null";
			}
			if (ob is string)
			{
				string text = (string)ob;
				bool flag = false;
				int length = text.Length;
				for (int i = 0; i < length; i++)
				{
					if (text[i] == '\\' || text[i] == '"')
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return "\"" + text + "\"";
				}
				StringBuilder stringBuilder = new StringBuilder(length + 10);
				stringBuilder.Append('"');
				for (int j = 0; j < length; j++)
				{
					if (text[j] == '"')
					{
						stringBuilder.Append("\\\"");
					}
					else if (text[j] == '\\')
					{
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append(text[j]);
					}
				}
				stringBuilder.Append('"');
				return stringBuilder.ToString();
			}
			else
			{
				if (ob is bool)
				{
					return ob.ToString().ToLower(Helpers.InvariantCulture);
				}
				return ob.ToString();
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0002E05E File Offset: 0x0002C25E
		internal static string EnsureEndsWithSemicolon(string value)
		{
			if (value != null && value.Length > 0 && value[value.Length - 1] != ';')
			{
				return value += ";";
			}
			return value;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal ClientScriptManager()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001372 RID: 4978
		internal const string EventStateFieldName = "__EVENTVALIDATION";

		// Token: 0x04001373 RID: 4979
		private Hashtable registeredArrayDeclares;

		// Token: 0x04001374 RID: 4980
		private ClientScriptManager.ScriptEntry clientScriptBlocks;

		// Token: 0x04001375 RID: 4981
		private ClientScriptManager.ScriptEntry startupScriptBlocks;

		// Token: 0x04001376 RID: 4982
		internal Hashtable hiddenFields;

		// Token: 0x04001377 RID: 4983
		private ClientScriptManager.ScriptEntry submitStatements;

		// Token: 0x04001378 RID: 4984
		private Page ownerPage;

		// Token: 0x04001379 RID: 4985
		private int[] eventValidationValues;

		// Token: 0x0400137A RID: 4986
		private int eventValidationPos;

		// Token: 0x0400137B RID: 4987
		private Hashtable expandoAttributes;

		// Token: 0x0400137C RID: 4988
		private bool _hasRegisteredForEventValidationOnCallback;

		// Token: 0x0400137D RID: 4989
		private bool _pageInRender;

		// Token: 0x0400137E RID: 4990
		private bool _initCallBackRegistered;

		// Token: 0x0400137F RID: 4991
		private bool _webFormClientScriptRendered;

		// Token: 0x04001380 RID: 4992
		private bool _webFormClientScriptRequired;

		// Token: 0x04001381 RID: 4993
		private bool _scriptTagOpened;

		// Token: 0x04001382 RID: 4994
		internal const string SCRIPT_BLOCK_START = "//<![CDATA[";

		// Token: 0x04001383 RID: 4995
		internal const string SCRIPT_BLOCK_END = "//]]>";

		// Token: 0x04001384 RID: 4996
		internal const string SCRIPT_ELEMENT_START = "<script type=\"text/javascript\">//<![CDATA[";

		// Token: 0x04001385 RID: 4997
		internal const string SCRIPT_ELEMENT_END = "//]]></script>";

		// Token: 0x020001AC RID: 428
		private sealed class ScriptEntry
		{
			// Token: 0x060010B0 RID: 4272 RVA: 0x0002E08E File Offset: 0x0002C28E
			public ScriptEntry(Type type, string key, string script, ClientScriptManager.ScriptEntryFormat format)
			{
				this.Key = key;
				this.Type = type;
				this.Script = script;
				this.Format = format;
			}

			// Token: 0x04001386 RID: 4998
			public readonly Type Type;

			// Token: 0x04001387 RID: 4999
			public readonly string Key;

			// Token: 0x04001388 RID: 5000
			public readonly string Script;

			// Token: 0x04001389 RID: 5001
			public readonly ClientScriptManager.ScriptEntryFormat Format;

			// Token: 0x0400138A RID: 5002
			public ClientScriptManager.ScriptEntry Next;
		}

		// Token: 0x020001AD RID: 429
		private enum ScriptEntryFormat
		{
			// Token: 0x0400138C RID: 5004
			None,
			// Token: 0x0400138D RID: 5005
			AddScriptTag,
			// Token: 0x0400138E RID: 5006
			Include
		}
	}
}
