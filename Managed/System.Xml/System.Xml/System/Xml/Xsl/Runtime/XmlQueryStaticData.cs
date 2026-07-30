using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl.IlGen;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000613 RID: 1555
	internal class XmlQueryStaticData
	{
		// Token: 0x06003D12 RID: 15634 RVA: 0x00152B0C File Offset: 0x00150D0C
		public XmlQueryStaticData(XmlWriterSettings defaultWriterSettings, IList<WhitespaceRule> whitespaceRules, StaticDataManager staticData)
		{
			this.defaultWriterSettings = defaultWriterSettings;
			this.whitespaceRules = whitespaceRules;
			this.names = staticData.Names;
			this.prefixMappingsList = staticData.PrefixMappingsList;
			this.filters = staticData.NameFilters;
			this.types = staticData.XmlTypes;
			this.collations = staticData.Collations;
			this.globalNames = staticData.GlobalNames;
			this.earlyBound = staticData.EarlyBound;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x00152B84 File Offset: 0x00150D84
		public XmlQueryStaticData(byte[] data, Type[] ebTypes)
		{
			XmlQueryDataReader xmlQueryDataReader = new XmlQueryDataReader(new MemoryStream(data, false));
			if ((xmlQueryDataReader.ReadInt32Encoded() & -256) > 0)
			{
				throw new NotSupportedException();
			}
			this.defaultWriterSettings = new XmlWriterSettings(xmlQueryDataReader);
			int num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.whitespaceRules = new WhitespaceRule[num];
				for (int i = 0; i < num; i++)
				{
					this.whitespaceRules[i] = new WhitespaceRule(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.names = new string[num];
				for (int j = 0; j < num; j++)
				{
					this.names[j] = xmlQueryDataReader.ReadString();
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.prefixMappingsList = new StringPair[num][];
				for (int k = 0; k < num; k++)
				{
					int num2 = xmlQueryDataReader.ReadInt32();
					this.prefixMappingsList[k] = new StringPair[num2];
					for (int l = 0; l < num2; l++)
					{
						this.prefixMappingsList[k][l] = new StringPair(xmlQueryDataReader.ReadString(), xmlQueryDataReader.ReadString());
					}
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.filters = new Int32Pair[num];
				for (int m = 0; m < num; m++)
				{
					this.filters[m] = new Int32Pair(xmlQueryDataReader.ReadInt32Encoded(), xmlQueryDataReader.ReadInt32Encoded());
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.types = new XmlQueryType[num];
				for (int n = 0; n < num; n++)
				{
					this.types[n] = XmlQueryTypeFactory.Deserialize(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.collations = new XmlCollation[num];
				for (int num3 = 0; num3 < num; num3++)
				{
					this.collations[num3] = new XmlCollation(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.globalNames = new string[num];
				for (int num4 = 0; num4 < num; num4++)
				{
					this.globalNames[num4] = xmlQueryDataReader.ReadString();
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.earlyBound = new EarlyBoundInfo[num];
				for (int num5 = 0; num5 < num; num5++)
				{
					this.earlyBound[num5] = new EarlyBoundInfo(xmlQueryDataReader.ReadString(), ebTypes[num5]);
				}
			}
			xmlQueryDataReader.Close();
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x00152DC0 File Offset: 0x00150FC0
		public void GetObjectData(out byte[] data, out Type[] ebTypes)
		{
			MemoryStream memoryStream = new MemoryStream(4096);
			XmlQueryDataWriter xmlQueryDataWriter = new XmlQueryDataWriter(memoryStream);
			xmlQueryDataWriter.WriteInt32Encoded(0);
			this.defaultWriterSettings.GetObjectData(xmlQueryDataWriter);
			if (this.whitespaceRules == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.whitespaceRules.Count);
				foreach (WhitespaceRule whitespaceRule in this.whitespaceRules)
				{
					whitespaceRule.GetObjectData(xmlQueryDataWriter);
				}
			}
			if (this.names == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.names.Length);
				foreach (string text in this.names)
				{
					xmlQueryDataWriter.Write(text);
				}
			}
			if (this.prefixMappingsList == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.prefixMappingsList.Length);
				foreach (StringPair[] array3 in this.prefixMappingsList)
				{
					xmlQueryDataWriter.Write(array3.Length);
					foreach (StringPair stringPair in array3)
					{
						xmlQueryDataWriter.Write(stringPair.Left);
						xmlQueryDataWriter.Write(stringPair.Right);
					}
				}
			}
			if (this.filters == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.filters.Length);
				foreach (Int32Pair int32Pair in this.filters)
				{
					xmlQueryDataWriter.WriteInt32Encoded(int32Pair.Left);
					xmlQueryDataWriter.WriteInt32Encoded(int32Pair.Right);
				}
			}
			if (this.types == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.types.Length);
				foreach (XmlQueryType xmlQueryType in this.types)
				{
					XmlQueryTypeFactory.Serialize(xmlQueryDataWriter, xmlQueryType);
				}
			}
			if (this.collations == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.collations.Length);
				XmlCollation[] array7 = this.collations;
				for (int i = 0; i < array7.Length; i++)
				{
					array7[i].GetObjectData(xmlQueryDataWriter);
				}
			}
			if (this.globalNames == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.globalNames.Length);
				foreach (string text2 in this.globalNames)
				{
					xmlQueryDataWriter.Write(text2);
				}
			}
			if (this.earlyBound == null)
			{
				xmlQueryDataWriter.Write(0);
				ebTypes = null;
			}
			else
			{
				xmlQueryDataWriter.Write(this.earlyBound.Length);
				ebTypes = new Type[this.earlyBound.Length];
				int num = 0;
				foreach (EarlyBoundInfo earlyBoundInfo in this.earlyBound)
				{
					xmlQueryDataWriter.Write(earlyBoundInfo.NamespaceUri);
					ebTypes[num++] = earlyBoundInfo.EarlyBoundType;
				}
			}
			xmlQueryDataWriter.Close();
			data = memoryStream.ToArray();
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x001530D4 File Offset: 0x001512D4
		public XmlWriterSettings DefaultWriterSettings
		{
			get
			{
				return this.defaultWriterSettings;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x001530DC File Offset: 0x001512DC
		public IList<WhitespaceRule> WhitespaceRules
		{
			get
			{
				return this.whitespaceRules;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x001530E4 File Offset: 0x001512E4
		public string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x001530EC File Offset: 0x001512EC
		public StringPair[][] PrefixMappingsList
		{
			get
			{
				return this.prefixMappingsList;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x001530F4 File Offset: 0x001512F4
		public Int32Pair[] Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x001530FC File Offset: 0x001512FC
		public XmlQueryType[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x00153104 File Offset: 0x00151304
		public XmlCollation[] Collations
		{
			get
			{
				return this.collations;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x0015310C File Offset: 0x0015130C
		public string[] GlobalNames
		{
			get
			{
				return this.globalNames;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x00153114 File Offset: 0x00151314
		public EarlyBoundInfo[] EarlyBound
		{
			get
			{
				return this.earlyBound;
			}
		}

		// Token: 0x040027B9 RID: 10169
		public const string DataFieldName = "staticData";

		// Token: 0x040027BA RID: 10170
		public const string TypesFieldName = "ebTypes";

		// Token: 0x040027BB RID: 10171
		private const int CurrentFormatVersion = 0;

		// Token: 0x040027BC RID: 10172
		private XmlWriterSettings defaultWriterSettings;

		// Token: 0x040027BD RID: 10173
		private IList<WhitespaceRule> whitespaceRules;

		// Token: 0x040027BE RID: 10174
		private string[] names;

		// Token: 0x040027BF RID: 10175
		private StringPair[][] prefixMappingsList;

		// Token: 0x040027C0 RID: 10176
		private Int32Pair[] filters;

		// Token: 0x040027C1 RID: 10177
		private XmlQueryType[] types;

		// Token: 0x040027C2 RID: 10178
		private XmlCollation[] collations;

		// Token: 0x040027C3 RID: 10179
		private string[] globalNames;

		// Token: 0x040027C4 RID: 10180
		private EarlyBoundInfo[] earlyBound;
	}
}
