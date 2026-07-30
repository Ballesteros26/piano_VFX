using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Drawing
{
	/// <summary>Converts colors from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000045 RID: 69
	public class ColorConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked. </param>
		/// <param name="sourceType">The type from which you want to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A8 RID: 680 RVA: 0x000065D8 File Offset: 0x000047D8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A9 RID: 681 RVA: 0x000065F6 File Offset: 0x000047F6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006614 File Offset: 0x00004814
		internal static Color StaticConvertFromString(ITypeDescriptorContext context, string s, CultureInfo culture)
		{
			if (culture == null)
			{
				culture = CultureInfo.InvariantCulture;
			}
			s = s.Trim();
			if (s.Length == 0)
			{
				return Color.Empty;
			}
			if (char.IsLetter(s[0]))
			{
				KnownColor knownColor;
				try
				{
					knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), s, true);
				}
				catch (Exception ex)
				{
					string text = Locale.GetText("Invalid color name '{0}'.", new object[] { s });
					throw new Exception(text, new FormatException(text, ex));
				}
				return KnownColors.FromKnownColor(knownColor);
			}
			string listSeparator = culture.TextInfo.ListSeparator;
			Color color = Color.Empty;
			if (s.IndexOf(listSeparator) == -1)
			{
				bool flag = s[0] == '#';
				int num = (flag ? 1 : 0);
				bool flag2 = false;
				if (s.Length > num + 1 && s[num] == '0')
				{
					flag2 = s[num + 1] == 'x' || s[num + 1] == 'X';
					if (flag2)
					{
						num += 2;
					}
				}
				if (flag || flag2)
				{
					s = s.Substring(num);
					int num2;
					try
					{
						num2 = int.Parse(s, NumberStyles.HexNumber);
					}
					catch (Exception ex2)
					{
						throw new Exception(Locale.GetText("Invalid Int32 value '{0}'.", new object[] { s }), ex2);
					}
					if (s.Length < 6 || (s.Length == 6 && flag && flag2))
					{
						num2 &= 16777215;
					}
					else if (num2 >> 24 == 0)
					{
						num2 |= -16777216;
					}
					color = Color.FromArgb(num2);
				}
			}
			if (color.IsEmpty)
			{
				Int32Converter int32Converter = new Int32Converter();
				string[] array = s.Split(listSeparator.ToCharArray());
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (int)int32Converter.ConvertFrom(context, culture, array[i]);
				}
				switch (array.Length)
				{
				case 1:
					color = Color.FromArgb(array2[0]);
					goto IL_022A;
				case 3:
					color = Color.FromArgb(array2[0], array2[1], array2[2]);
					goto IL_022A;
				case 4:
					color = Color.FromArgb(array2[0], array2[1], array2[2], array2[3]);
					goto IL_022A;
				}
				throw new ArgumentException(s + " is not a valid color value.");
			}
			IL_022A:
			if (!color.IsEmpty)
			{
				Color color2 = KnownColors.FindColorMatch(color);
				if (!color2.IsEmpty)
				{
					return color2;
				}
			}
			return color;
		}

		/// <summary>Converts the given object to the converter's native type.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentException">The conversion cannot be performed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002AB RID: 683 RVA: 0x00006888 File Offset: 0x00004A88
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			return ColorConverter.StaticConvertFromString(context, text, culture);
		}

		/// <summary>Converts the specified object to another type. </summary>
		/// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
		/// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is null. Also, properties on the context object may return null. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationtype" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002AC RID: 684 RVA: 0x000068B8 File Offset: 0x00004AB8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Color)
			{
				Color color = (Color)value;
				if (destinationType == typeof(string))
				{
					if (color == Color.Empty)
					{
						return string.Empty;
					}
					if (color.IsKnownColor || color.IsNamedColor)
					{
						return color.Name;
					}
					string listSeparator = culture.TextInfo.ListSeparator;
					StringBuilder stringBuilder = new StringBuilder();
					if (color.A != 255)
					{
						stringBuilder.Append(color.A);
						stringBuilder.Append(listSeparator);
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(color.R);
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(color.G);
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(color.B);
					return stringBuilder.ToString();
				}
				else if (destinationType == typeof(InstanceDescriptor))
				{
					if (color.IsEmpty)
					{
						return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetField("Empty"), null);
					}
					if (color.IsSystemColor)
					{
						return new InstanceDescriptor(typeof(SystemColors).GetTypeInfo().GetProperty(color.Name), null);
					}
					if (color.IsKnownColor)
					{
						return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetProperty(color.Name), null);
					}
					return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetMethod("FromArgb", new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					}), new object[] { color.A, color.R, color.G, color.B });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Retrieves a collection containing a set of standard values for the data type for which this validator is designed. This will return null if the data type does not support a standard set of values.</summary>
		/// <returns>A collection containing null or a standard set of valid values. The default implementation always returns null.</returns>
		/// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is null. Also, properties on the context object may return null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002AD RID: 685 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object obj = ColorConverter.creatingCached;
			lock (obj)
			{
				if (ColorConverter.cached != null)
				{
					return ColorConverter.cached;
				}
				Array array = Array.CreateInstance(typeof(Color), KnownColors.ArgbValues.Length - 1);
				for (int i = 1; i < KnownColors.ArgbValues.Length; i++)
				{
					array.SetValue(KnownColors.FromKnownColor((KnownColor)i), i - 1);
				}
				Array.Sort(array, 0, array.Length, new ColorConverter.CompareColors());
				ColorConverter.cached = new TypeConverter.StandardValuesCollection(array);
			}
			return ColorConverter.cached;
		}

		/// <summary>Determines if this object supports a standard set of values that can be chosen from a list.</summary>
		/// <returns>true if <see cref="Overload:System.Drawing.ColorConverter.GetStandardValues" /> must be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002AE RID: 686 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400035C RID: 860
		private static TypeConverter.StandardValuesCollection cached;

		// Token: 0x0400035D RID: 861
		private static object creatingCached = new object();

		// Token: 0x02000046 RID: 70
		private sealed class CompareColors : IComparer
		{
			// Token: 0x060002B0 RID: 688 RVA: 0x00006BB4 File Offset: 0x00004DB4
			public int Compare(object x, object y)
			{
				return string.Compare(((Color)x).Name, ((Color)y).Name);
			}
		}
	}
}
