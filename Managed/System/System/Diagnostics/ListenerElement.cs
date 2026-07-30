using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020001B3 RID: 435
	internal class ListenerElement : TypedElement
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003E240 File Offset: 0x0003C440
		public ListenerElement(bool allowReferences)
			: base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003E2E0 File Offset: 0x0003C4E0
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0003E300 File Offset: 0x0003C500
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0003E312 File Offset: 0x0003C512
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0003E324 File Offset: 0x0003C524
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003E332 File Offset: 0x0003C532
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0003E344 File Offset: 0x0003C544
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
			set
			{
				base[ListenerElement._propOutputOpts] = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003E357 File Offset: 0x0003C557
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003E36A File Offset: 0x0003C56A
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003E37C File Offset: 0x0003C57C
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003E3F7 File Offset: 0x0003C5F7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003E400 File Offset: 0x0003C600
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener traceListener;
			try
			{
				if (string.IsNullOrEmpty(this.TypeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(global::SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(global::SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(global::SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					traceListener = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener2 = (TraceListener)base.BaseGetRuntimeObject();
					traceListener2.initializeData = base.InitData;
					traceListener2.Name = this.Name;
					traceListener2.SetAttributes(this.Attributes);
					traceListener2.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener2.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener2 as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener2;
					traceListener = traceListener2;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(global::SR.GetString("Couldn't create listener '{0}'.", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003E639 File Offset: 0x0003C839
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003E660 File Offset: 0x0003C860
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003E694 File Offset: 0x0003C894
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				this._properties.Clear();
				this._properties.Add(this._propListenerTypeName);
				this._properties.Add(ListenerElement._propFilter);
				this._properties.Add(ListenerElement._propName);
				this._properties.Add(ListenerElement._propOutputOpts);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003E700 File Offset: 0x0003C900
		internal TraceListener RefreshRuntimeObject(TraceListener listener)
		{
			this._runtimeObject = null;
			TraceListener traceListener;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(global::SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(global::SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(global::SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.RefreshRuntimeObject(listener);
					traceListener = (TraceListener)this._runtimeObject;
				}
				else if (Type.GetType(typeName) != listener.GetType() || base.InitData != listener.initializeData)
				{
					traceListener = this.GetRuntimeObject();
				}
				else
				{
					listener.SetAttributes(this.Attributes);
					listener.TraceOutputOptions = this.TraceOutputOptions;
					if (listener.Filter != null)
					{
						if (base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.Inherited)
						{
							listener.Filter = this.Filter.RefreshRuntimeObject(listener.Filter);
						}
						else
						{
							listener.Filter = null;
						}
					}
					this._runtimeObject = listener;
					traceListener = listener;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(global::SR.GetString("Couldn't create listener '{0}'.", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x04001017 RID: 4119
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001018 RID: 4120
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001019 RID: 4121
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x0400101A RID: 4122
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x0400101B RID: 4123
		private bool _allowReferences;

		// Token: 0x0400101C RID: 4124
		private Hashtable _attributes;

		// Token: 0x0400101D RID: 4125
		internal bool _isAddedByDefault;
	}
}
