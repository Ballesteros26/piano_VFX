using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000165 RID: 357
	internal class ConfigHelper
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x000394DC File Offset: 0x000376DC
		internal static IDictionary GetDictionary(IDictionary prev, XmlNode region, string nameAtt, string valueAtt)
		{
			Hashtable hashtable;
			if (prev == null)
			{
				hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				hashtable = (Hashtable)((Hashtable)prev).Clone();
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(hashtable);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as IDictionary;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00039530 File Offset: 0x00037730
		internal static ConfigNameValueCollection GetNameValueCollection(NameValueCollection prev, XmlNode region, string nameAtt, string valueAtt)
		{
			ConfigNameValueCollection configNameValueCollection = new ConfigNameValueCollection(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			if (prev != null)
			{
				configNameValueCollection.Add(prev);
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(configNameValueCollection);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as ConfigNameValueCollection;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00039578 File Offset: 0x00037778
		private static ConfigHelper.CollectionWrapper GoGetThem(ConfigHelper.CollectionWrapper result, XmlNode region, string nameAtt, string valueAtt)
		{
			if (region.Attributes != null && region.Attributes.Count != 0 && (region.Attributes.Count != 1 || region.Attributes[0].Name != "xmlns"))
			{
				throw new ConfigurationException("Unknown attribute", region);
			}
			foreach (object obj in region.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						throw new ConfigurationException("Only XmlElement allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Clear();
					}
					else if (name == "remove")
					{
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Remove(xmlNode2.Value);
					}
					else
					{
						if (!(name == "add"))
						{
							throw new ConfigurationException("Unknown element", xmlNode);
						}
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						XmlNode xmlNode3 = xmlNode.Attributes.RemoveNamedItem(valueAtt);
						if (xmlNode3 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result[xmlNode2.Value] = xmlNode3.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x02000166 RID: 358
		private class CollectionWrapper
		{
			// Token: 0x06000ADA RID: 2778 RVA: 0x000397C0 File Offset: 0x000379C0
			public CollectionWrapper(IDictionary dict)
			{
				this.dict = dict;
				this.isDict = true;
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x000397D6 File Offset: 0x000379D6
			public CollectionWrapper(NameValueCollection collection)
			{
				this.collection = collection;
				this.isDict = false;
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x000397EC File Offset: 0x000379EC
			public void Remove(string s)
			{
				if (this.isDict)
				{
					this.dict.Remove(s);
					return;
				}
				this.collection.Remove(s);
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x0003980F File Offset: 0x00037A0F
			public void Clear()
			{
				if (this.isDict)
				{
					this.dict.Clear();
					return;
				}
				this.collection.Clear();
			}

			// Token: 0x170001B1 RID: 433
			public string this[string key]
			{
				set
				{
					if (this.isDict)
					{
						this.dict[key] = value;
						return;
					}
					this.collection[key] = value;
				}
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00039855 File Offset: 0x00037A55
			public object UnWrap()
			{
				if (this.isDict)
				{
					return this.dict;
				}
				return this.collection;
			}

			// Token: 0x04000F78 RID: 3960
			private IDictionary dict;

			// Token: 0x04000F79 RID: 3961
			private NameValueCollection collection;

			// Token: 0x04000F7A RID: 3962
			private bool isDict;
		}
	}
}
