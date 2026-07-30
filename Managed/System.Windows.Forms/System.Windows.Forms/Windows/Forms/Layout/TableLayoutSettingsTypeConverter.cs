using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;

namespace System.Windows.Forms.Layout
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x020004A0 RID: 1184
	public class TableLayoutSettingsTypeConverter : TypeConverter
	{
		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type by using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		// Token: 0x06004B8E RID: 19342 RVA: 0x0012BEFC File Offset: 0x0012A0FC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Determines whether this converter can convert an object in the given source type to the native type of this converter.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		// Token: 0x06004B8F RID: 19343 RVA: 0x0012BF18 File Offset: 0x0012A118
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given value object to the specified type by using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		// Token: 0x06004B90 RID: 19344 RVA: 0x0012BF34 File Offset: 0x0012A134
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(value is TableLayoutSettings) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			TableLayoutSettings tableLayoutSettings = value as TableLayoutSettings;
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartDocument();
			List<ControlInfo> controls = tableLayoutSettings.GetControls();
			xmlTextWriter.WriteStartElement("TableLayoutSettings");
			xmlTextWriter.WriteStartElement("Controls");
			foreach (ControlInfo controlInfo in controls)
			{
				xmlTextWriter.WriteStartElement("Control");
				xmlTextWriter.WriteAttributeString("Name", controlInfo.Control.ToString());
				xmlTextWriter.WriteAttributeString("Row", controlInfo.Row.ToString());
				xmlTextWriter.WriteAttributeString("RowSpan", controlInfo.RowSpan.ToString());
				xmlTextWriter.WriteAttributeString("Column", controlInfo.Col.ToString());
				xmlTextWriter.WriteAttributeString("ColumnSpan", controlInfo.ColSpan.ToString());
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			List<string> list = new List<string>();
			foreach (object obj in tableLayoutSettings.ColumnStyles)
			{
				ColumnStyle columnStyle = (ColumnStyle)obj;
				list.Add(columnStyle.SizeType.ToString());
				list.Add(columnStyle.Width.ToString(CultureInfo.InvariantCulture));
			}
			xmlTextWriter.WriteStartElement("Columns");
			xmlTextWriter.WriteAttributeString("Styles", string.Join(",", list.ToArray()));
			xmlTextWriter.WriteEndElement();
			list.Clear();
			foreach (object obj2 in tableLayoutSettings.RowStyles)
			{
				RowStyle rowStyle = (RowStyle)obj2;
				list.Add(rowStyle.SizeType.ToString());
				list.Add(rowStyle.Height.ToString(CultureInfo.InvariantCulture));
			}
			xmlTextWriter.WriteStartElement("Rows");
			xmlTextWriter.WriteAttributeString("Styles", string.Join(",", list.ToArray()));
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();
			xmlTextWriter.Close();
			return stringWriter.ToString();
		}

		/// <summary>Converts the given object to the type of this converter by using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		// Token: 0x06004B91 RID: 19345 RVA: 0x0012C220 File Offset: 0x0012A420
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(value as string);
			TableLayoutSettings tableLayoutSettings = new TableLayoutSettings(null);
			int num = this.ParseControl(xmlDocument, tableLayoutSettings);
			this.ParseColumnStyle(xmlDocument, tableLayoutSettings);
			this.ParseRowStyle(xmlDocument, tableLayoutSettings);
			tableLayoutSettings.RowCount = num;
			return tableLayoutSettings;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0012C27C File Offset: 0x0012A47C
		private int ParseControl(XmlDocument xmldoc, TableLayoutSettings settings)
		{
			int num = 0;
			foreach (object obj in xmldoc.GetElementsByTagName("Control"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes.get_ItemOf("Name") != null && !string.IsNullOrEmpty(xmlNode.Attributes.get_ItemOf("Name").Value))
				{
					if (xmlNode.Attributes.get_ItemOf("Row") != null)
					{
						settings.SetRow(xmlNode.Attributes.get_ItemOf("Name").Value, this.GetValue(xmlNode.Attributes.get_ItemOf("Row").Value));
						num++;
					}
					if (xmlNode.Attributes.get_ItemOf("RowSpan") != null)
					{
						settings.SetRowSpan(xmlNode.Attributes.get_ItemOf("Name").Value, this.GetValue(xmlNode.Attributes.get_ItemOf("RowSpan").Value));
					}
					if (xmlNode.Attributes.get_ItemOf("Column") != null)
					{
						settings.SetColumn(xmlNode.Attributes.get_ItemOf("Name").Value, this.GetValue(xmlNode.Attributes.get_ItemOf("Column").Value));
					}
					if (xmlNode.Attributes.get_ItemOf("ColumnSpan") != null)
					{
						settings.SetColumnSpan(xmlNode.Attributes.get_ItemOf("Name").Value, this.GetValue(xmlNode.Attributes.get_ItemOf("ColumnSpan").Value));
					}
				}
			}
			return num;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0012C458 File Offset: 0x0012A658
		private void ParseColumnStyle(XmlDocument xmldoc, TableLayoutSettings settings)
		{
			foreach (object obj in xmldoc.GetElementsByTagName("Columns"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes.get_ItemOf("Styles") != null)
				{
					string value = xmlNode.Attributes.get_ItemOf("Styles").Value;
					if (!string.IsNullOrEmpty(value))
					{
						string[] array = this.BuggySplit(value);
						for (int i = 0; i < array.Length; i += 2)
						{
							float num = 0f;
							SizeType sizeType = (SizeType)((int)Enum.Parse(typeof(SizeType), array[i]));
							float.TryParse(array[i + 1], 167, CultureInfo.InvariantCulture, ref num);
							settings.ColumnStyles.Add(new ColumnStyle(sizeType, num));
						}
					}
				}
			}
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0012C578 File Offset: 0x0012A778
		private void ParseRowStyle(XmlDocument xmldoc, TableLayoutSettings settings)
		{
			foreach (object obj in xmldoc.GetElementsByTagName("Rows"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes.get_ItemOf("Styles") != null)
				{
					string value = xmlNode.Attributes.get_ItemOf("Styles").Value;
					if (!string.IsNullOrEmpty(value))
					{
						string[] array = this.BuggySplit(value);
						for (int i = 0; i < array.Length; i += 2)
						{
							float num = 0f;
							SizeType sizeType = (SizeType)((int)Enum.Parse(typeof(SizeType), array[i]));
							float.TryParse(array[i + 1], 167, CultureInfo.InvariantCulture, ref num);
							settings.RowStyles.Add(new RowStyle(sizeType, num));
						}
					}
				}
			}
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0012C698 File Offset: 0x0012A898
		private int GetValue(string attValue)
		{
			int num = -1;
			if (!string.IsNullOrEmpty(attValue))
			{
				int.TryParse(attValue, ref num);
			}
			return num;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0012C6BC File Offset: 0x0012A8BC
		private string[] BuggySplit(string s)
		{
			List<string> list = new List<string>();
			string[] array = s.Split(new char[] { ',' });
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i].ToLowerInvariant();
				if (text == null)
				{
					goto IL_009D;
				}
				if (TableLayoutSettingsTypeConverter.<>f__switch$mapE == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("autosize", 0);
					dictionary.Add("absolute", 0);
					dictionary.Add("percent", 0);
					TableLayoutSettingsTypeConverter.<>f__switch$mapE = dictionary;
				}
				int num;
				if (!TableLayoutSettingsTypeConverter.<>f__switch$mapE.TryGetValue(text, ref num))
				{
					goto IL_009D;
				}
				if (num != 0)
				{
					goto IL_009D;
				}
				list.Add(array[i]);
				IL_00F6:
				i++;
				continue;
				IL_009D:
				if (i + 1 < array.Length)
				{
					float num2;
					if (float.TryParse(array[i + 1], ref num2))
					{
						list.Add(string.Format("{0}.{1}", array[i], array[i + 1]));
						i++;
					}
					else
					{
						list.Add(array[i]);
					}
				}
				else
				{
					list.Add(array[i]);
				}
				goto IL_00F6;
			}
			return list.ToArray();
		}
	}
}
