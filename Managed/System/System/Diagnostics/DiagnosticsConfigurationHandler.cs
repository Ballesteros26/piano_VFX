using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Handles the diagnostics section of configuration files.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EB RID: 491
	[Obsolete("This class is obsoleted")]
	public class DiagnosticsConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DiagnosticsConfigurationHandler" /> class.</summary>
		// Token: 0x06000F7C RID: 3964 RVA: 0x00047544 File Offset: 0x00045744
		public DiagnosticsConfigurationHandler()
		{
			this.elementHandlers["assert"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddAssertNode);
			this.elementHandlers["performanceCounters"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddPerformanceCountersNode);
			this.elementHandlers["switches"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSwitchesNode);
			this.elementHandlers["trace"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddTraceNode);
			this.elementHandlers["sources"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSourcesNode);
		}

		/// <summary>Parses the configuration settings for the &lt;system.diagnostics&gt; Element section of configuration files.</summary>
		/// <returns>A new configuration object, in the form of a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <param name="parent">The object inherited from the parent path</param>
		/// <param name="configContext">Reserved. Used in ASP.NET to convey the virtual path of the configuration being evaluated.</param>
		/// <param name="section">The root XML node at the section to handle.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Switches could not be found.-or-Assert could not be found.-or-Trace could not be found.-or-Performance counters could not be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000F7D RID: 3965 RVA: 0x000475F0 File Offset: 0x000457F0
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			IDictionary dictionary;
			if (parent == null)
			{
				dictionary = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				dictionary = (IDictionary)((ICloneable)parent).Clone();
			}
			if (dictionary.Contains(".__TraceInfoSettingsKey__."))
			{
				this.configValues = (TraceImplSettings)dictionary[".__TraceInfoSettingsKey__."];
			}
			else
			{
				dictionary.Add(".__TraceInfoSettingsKey__.", this.configValues = new TraceImplSettings());
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType == XmlNodeType.Element && !(xmlNode.LocalName != "sharedListeners"))
				{
					this.AddTraceListeners(dictionary, xmlNode, this.GetSharedListeners(dictionary));
				}
			}
			foreach (object obj2 in section.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				XmlNodeType nodeType2 = xmlNode2.NodeType;
				if (nodeType2 != XmlNodeType.Element)
				{
					if (nodeType2 != XmlNodeType.Comment && nodeType2 != XmlNodeType.Whitespace)
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
				else if (!(xmlNode2.LocalName == "sharedListeners"))
				{
					DiagnosticsConfigurationHandler.ElementHandler elementHandler = (DiagnosticsConfigurationHandler.ElementHandler)this.elementHandlers[xmlNode2.Name];
					if (elementHandler != null)
					{
						elementHandler(dictionary, xmlNode2);
					}
					else
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00047788 File Offset: 0x00045988
		private void AddAssertNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "assertuienabled", false, node);
			string attribute2 = this.GetAttribute(attributes, "logfilename", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["assertuienabled"] = bool.Parse(attribute);
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `assertuienabled' attribute must be `true' or `false'", ex, node);
				}
			}
			if (attribute2 != null)
			{
				d["logfilename"] = attribute2;
			}
			DefaultTraceListener defaultTraceListener = (DefaultTraceListener)this.configValues.Listeners["Default"];
			if (defaultTraceListener != null)
			{
				if (attribute != null)
				{
					defaultTraceListener.AssertUiEnabled = (bool)d["assertuienabled"];
				}
				if (attribute2 != null)
				{
					defaultTraceListener.LogFileName = attribute2;
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00047870 File Offset: 0x00045A70
		private void AddPerformanceCountersNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "filemappingsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["filemappingsize"] = int.Parse(attribute);
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `filemappingsize' attribute must be an integral value.", ex, node);
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000478F4 File Offset: 0x00045AF4
		private void AddSwitchesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			IDictionary dictionary = new Hashtable();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									dictionary.Clear();
								}
							}
							else
							{
								string text = this.GetAttribute(attributes, "name", true, xmlNode);
								dictionary.Remove(text);
							}
						}
						else
						{
							string text = this.GetAttribute(attributes, "name", true, xmlNode);
							string attribute = this.GetAttribute(attributes, "value", true, xmlNode);
							dictionary[text] = DiagnosticsConfigurationHandler.GetSwitchValue(text, attribute);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
			d[node.Name] = dictionary;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0000206B File Offset: 0x0000026B
		private static object GetSwitchValue(string name, string value)
		{
			return value;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00047A40 File Offset: 0x00045C40
		private void AddTraceNode(IDictionary d, XmlNode node)
		{
			this.AddTraceAttributes(d, node);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, this.configValues.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00047AF4 File Offset: 0x00045CF4
		private void AddTraceAttributes(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "autoflush", false, node);
			string attribute2 = this.GetAttribute(attributes, "indentsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				bool flag = false;
				try
				{
					flag = bool.Parse(attribute);
					d["autoflush"] = flag;
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `autoflush' attribute must be `true' or `false'", ex, node);
				}
				this.configValues.AutoFlush = flag;
			}
			if (attribute2 != null)
			{
				int num = 0;
				try
				{
					num = int.Parse(attribute2);
					d["indentsize"] = num;
				}
				catch (Exception ex2)
				{
					throw new ConfigurationException("The `indentsize' attribute must be an integral value.", ex2, node);
				}
				this.configValues.IndentSize = num;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00047BC4 File Offset: 0x00045DC4
		private TraceListenerCollection GetSharedListeners(IDictionary d)
		{
			TraceListenerCollection traceListenerCollection = d["sharedListeners"] as TraceListenerCollection;
			if (traceListenerCollection == null)
			{
				traceListenerCollection = new TraceListenerCollection();
				d["sharedListeners"] = traceListenerCollection;
			}
			return traceListenerCollection;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00047BF8 File Offset: 0x00045DF8
		private void AddSourcesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			Hashtable hashtable = d["sources"] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				d["sources"] = hashtable;
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "source")
						{
							this.AddTraceSource(d, hashtable, xmlNode);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00047CC4 File Offset: 0x00045EC4
		private void AddTraceSource(IDictionary d, Hashtable sources, XmlNode node)
		{
			string text = null;
			SourceLevels sourceLevels = SourceLevels.Error;
			StringDictionary stringDictionary = new StringDictionary();
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (!(name == "name"))
				{
					if (!(name == "switchValue"))
					{
						stringDictionary[xmlAttribute.Name] = xmlAttribute.Value;
					}
					else
					{
						sourceLevels = (SourceLevels)Enum.Parse(typeof(SourceLevels), xmlAttribute.Value);
					}
				}
				else
				{
					text = xmlAttribute.Value;
				}
			}
			if (text == null)
			{
				throw new ConfigurationException("Mandatory attribute 'name' is missing in 'source' element.");
			}
			if (sources.ContainsKey(text))
			{
				return;
			}
			TraceSourceInfo traceSourceInfo = new TraceSourceInfo(text, sourceLevels, this.configValues);
			sources.Add(traceSourceInfo.Name, traceSourceInfo);
			foreach (object obj2 in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, traceSourceInfo.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00047E68 File Offset: 0x00046068
		private void AddTraceListeners(IDictionary d, XmlNode listenersNode, TraceListenerCollection listeners)
		{
			this.ValidateInvalidAttributes(listenersNode.Attributes, listenersNode);
			foreach (object obj in listenersNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									this.configValues.Listeners.Clear();
								}
							}
							else
							{
								string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
								this.RemoveTraceListener(attribute);
							}
						}
						else
						{
							this.AddTraceListener(d, xmlNode, attributes, listeners);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00047F7C File Offset: 0x0004617C
		private void AddTraceListener(IDictionary d, XmlNode child, XmlAttributeCollection attributes, TraceListenerCollection listeners)
		{
			string attribute = this.GetAttribute(attributes, "name", true, child);
			string attribute2 = this.GetAttribute(attributes, "type", false, child);
			if (attribute2 == null)
			{
				TraceListener traceListener = this.GetSharedListeners(d)[attribute];
				if (traceListener == null)
				{
					throw new ConfigurationException(string.Format("Shared trace listener {0} does not exist.", attribute));
				}
				if (attributes.Count != 0)
				{
					throw new ConfigurationErrorsException(string.Format("Listener '{0}' references a shared listener and can only have a 'Name' attribute.", attribute));
				}
				traceListener.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener);
				return;
			}
			else
			{
				Type type = Type.GetType(attribute2);
				if (type == null)
				{
					throw new ConfigurationException(string.Format("Invalid Type Specified: {0}", attribute2));
				}
				string attribute3 = this.GetAttribute(attributes, "initializeData", false, child);
				object[] array;
				Type[] array2;
				if (attribute3 != null)
				{
					array = new object[] { attribute3 };
					array2 = new Type[] { typeof(string) };
				}
				else
				{
					array = null;
					array2 = Type.EmptyTypes;
				}
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				if (type.Assembly == base.GetType().Assembly)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, array2, null);
				if (constructor == null)
				{
					throw new ConfigurationException("Couldn't find constructor for class " + attribute2);
				}
				TraceListener traceListener2 = (TraceListener)constructor.Invoke(array);
				traceListener2.Name = attribute;
				string attribute4 = this.GetAttribute(attributes, "traceOutputOptions", false, child);
				if (attribute4 != null)
				{
					if (attribute4 != attribute4.Trim())
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					TraceOptions traceOptions;
					try
					{
						traceOptions = (TraceOptions)Enum.Parse(typeof(TraceOptions), attribute4);
					}
					catch (ArgumentException)
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					traceListener2.TraceOutputOptions = traceOptions;
				}
				string[] supportedAttributes = traceListener2.GetSupportedAttributes();
				if (supportedAttributes != null)
				{
					foreach (string text in supportedAttributes)
					{
						string attribute5 = this.GetAttribute(attributes, text, false, child);
						if (attribute5 != null)
						{
							traceListener2.Attributes.Add(text, attribute5);
						}
					}
				}
				traceListener2.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener2);
				return;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000481A8 File Offset: 0x000463A8
		private void RemoveTraceListener(string name)
		{
			try
			{
				this.configValues.Listeners.Remove(name);
			}
			catch (ArgumentException)
			{
			}
			catch (Exception ex)
			{
				throw new ConfigurationException(string.Format("Unknown error removing listener: {0}", name), ex);
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000481FC File Offset: 0x000463FC
		private string GetAttribute(XmlAttributeCollection attrs, string attr, bool required, XmlNode node)
		{
			XmlAttribute xmlAttribute = attrs[attr];
			string text = null;
			if (xmlAttribute != null)
			{
				text = xmlAttribute.Value;
				if (required)
				{
					this.ValidateAttribute(attr, text, node);
				}
				attrs.Remove(xmlAttribute);
			}
			else if (required)
			{
				this.ThrowMissingAttribute(attr, node);
			}
			return text;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00048241 File Offset: 0x00046441
		private void ValidateAttribute(string attribute, string value, XmlNode node)
		{
			if (value == null || value.Length == 0)
			{
				throw new ConfigurationException(string.Format("Required attribute '{0}' cannot be empty.", attribute), node);
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00048260 File Offset: 0x00046460
		private void ValidateInvalidAttributes(XmlAttributeCollection c, XmlNode node)
		{
			if (c.Count != 0)
			{
				this.ThrowUnrecognizedAttribute(c[0].Name, node);
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0004827D File Offset: 0x0004647D
		private void ThrowMissingAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Required attribute '{0}' not found.", attribute), node);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00048290 File Offset: 0x00046490
		private void ThrowUnrecognizedNode(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized node `{0}'; nodeType={1}", node.Name, node.NodeType), node);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000482B3 File Offset: 0x000464B3
		private void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized element '{0}'.", node.Name), node);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000482CB File Offset: 0x000464CB
		private void ThrowUnrecognizedAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized attribute '{0}' on element <{1}/>.", attribute, node.Name), node);
		}

		// Token: 0x0400111F RID: 4383
		private TraceImplSettings configValues;

		// Token: 0x04001120 RID: 4384
		private IDictionary elementHandlers = new Hashtable();

		// Token: 0x020001EC RID: 492
		// (Invoke) Token: 0x06000F92 RID: 3986
		private delegate void ElementHandler(IDictionary d, XmlNode node);
	}
}
