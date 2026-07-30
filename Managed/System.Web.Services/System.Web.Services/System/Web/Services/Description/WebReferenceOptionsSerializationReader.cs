using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000137 RID: 311
	internal class WebReferenceOptionsSerializationReader : XmlSerializationReader
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00041328 File Offset: 0x0003F528
		internal Hashtable CodeGenerationOptionsValues
		{
			get
			{
				if (this._CodeGenerationOptionsValues == null)
				{
					this._CodeGenerationOptionsValues = new Hashtable
					{
						{ "properties", 1L },
						{ "newAsync", 2L },
						{ "oldAsync", 4L },
						{ "order", 8L },
						{ "enableDataBinding", 16L }
					};
				}
				return this._CodeGenerationOptionsValues;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000413AB File Offset: 0x0003F5AB
		private CodeGenerationOptions Read1_CodeGenerationOptions(string s)
		{
			return (CodeGenerationOptions)XmlSerializationReader.ToEnum(s, this.CodeGenerationOptionsValues, "System.Xml.Serialization.CodeGenerationOptions");
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000413C0 File Offset: 0x0003F5C0
		private ServiceDescriptionImportStyle Read2_ServiceDescriptionImportStyle(string s)
		{
			if (s == "client")
			{
				return ServiceDescriptionImportStyle.Client;
			}
			if (s == "server")
			{
				return ServiceDescriptionImportStyle.Server;
			}
			if (!(s == "serverInterface"))
			{
				throw base.CreateUnknownConstantException(s, typeof(ServiceDescriptionImportStyle));
			}
			return ServiceDescriptionImportStyle.ServerInterface;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00041410 File Offset: 0x0003F610
		private WebReferenceOptions Read4_WebReferenceOptions(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_webReferenceOptions || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			WebReferenceOptions webReferenceOptions = new WebReferenceOptions();
			StringCollection schemaImporterExtensions = webReferenceOptions.SchemaImporterExtensions;
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(webReferenceOptions);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return webReferenceOptions;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_codeGenerationOptions && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (base.Reader.IsEmptyElement)
						{
							base.Reader.Skip();
						}
						else
						{
							webReferenceOptions.CodeGenerationOptions = this.Read1_CodeGenerationOptions(base.Reader.ReadElementString());
						}
						array[0] = true;
					}
					else if (base.Reader.LocalName == this.id4_schemaImporterExtensions && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							StringCollection schemaImporterExtensions2 = webReferenceOptions.SchemaImporterExtensions;
							if (schemaImporterExtensions2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num2 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id5_type && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (base.ReadNull())
											{
												schemaImporterExtensions2.Add(null);
											}
											else
											{
												schemaImporterExtensions2.Add(base.Reader.ReadElementString());
											}
										}
										else
										{
											base.UnknownNode(null, "http://microsoft.com/webReference/:type");
										}
									}
									else
									{
										base.UnknownNode(null, "http://microsoft.com/webReference/:type");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num2, ref readerCount2);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (!array[2] && base.Reader.LocalName == this.id6_style && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (base.Reader.IsEmptyElement)
						{
							base.Reader.Skip();
						}
						else
						{
							webReferenceOptions.Style = this.Read2_ServiceDescriptionImportStyle(base.Reader.ReadElementString());
						}
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id7_verbose && base.Reader.NamespaceURI == this.id2_Item)
					{
						webReferenceOptions.Verbose = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[3] = true;
					}
					else
					{
						base.UnknownNode(webReferenceOptions, "http://microsoft.com/webReference/:codeGenerationOptions, http://microsoft.com/webReference/:schemaImporterExtensions, http://microsoft.com/webReference/:style, http://microsoft.com/webReference/:verbose");
					}
				}
				else
				{
					base.UnknownNode(webReferenceOptions, "http://microsoft.com/webReference/:codeGenerationOptions, http://microsoft.com/webReference/:schemaImporterExtensions, http://microsoft.com/webReference/:style, http://microsoft.com/webReference/:verbose");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return webReferenceOptions;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void InitCallbacks()
		{
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000417A8 File Offset: 0x0003F9A8
		internal object Read5_webReferenceOptions()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_webReferenceOptions || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = this.Read4_WebReferenceOptions(true, true);
			}
			else
			{
				base.UnknownNode(null, "http://microsoft.com/webReference/:webReferenceOptions");
			}
			return obj;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00041818 File Offset: 0x0003FA18
		protected override void InitIDs()
		{
			this.id2_Item = base.Reader.NameTable.Add("http://microsoft.com/webReference/");
			this.id5_type = base.Reader.NameTable.Add("type");
			this.id4_schemaImporterExtensions = base.Reader.NameTable.Add("schemaImporterExtensions");
			this.id3_codeGenerationOptions = base.Reader.NameTable.Add("codeGenerationOptions");
			this.id6_style = base.Reader.NameTable.Add("style");
			this.id7_verbose = base.Reader.NameTable.Add("verbose");
			this.id1_webReferenceOptions = base.Reader.NameTable.Add("webReferenceOptions");
		}

		// Token: 0x04000587 RID: 1415
		private Hashtable _CodeGenerationOptionsValues;

		// Token: 0x04000588 RID: 1416
		private string id2_Item;

		// Token: 0x04000589 RID: 1417
		private string id5_type;

		// Token: 0x0400058A RID: 1418
		private string id4_schemaImporterExtensions;

		// Token: 0x0400058B RID: 1419
		private string id3_codeGenerationOptions;

		// Token: 0x0400058C RID: 1420
		private string id6_style;

		// Token: 0x0400058D RID: 1421
		private string id7_verbose;

		// Token: 0x0400058E RID: 1422
		private string id1_webReferenceOptions;
	}
}
