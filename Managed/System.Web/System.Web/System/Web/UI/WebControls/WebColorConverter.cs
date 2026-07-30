using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts a predefined color name or an RGB color value to and from a <see cref="T:System.Drawing.Color" /> object.</summary>
	// Token: 0x02000329 RID: 809
	public class WebColorConverter : ColorConverter
	{
		/// <summary>Converts the given value to the type of the converter.</summary>
		/// <returns>The object resulting from conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter.</param>
		/// <param name="value">The object to convert.</param>
		// Token: 0x06001C1F RID: 7199 RVA: 0x00046388 File Offset: 0x00044588
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				Color empty = Color.Empty;
				if (string.IsNullOrEmpty(text))
				{
					return empty;
				}
				if (text[0] == '#')
				{
					return base.ConvertFrom(context, culture, value);
				}
				if (StringUtil.EqualsIgnoreCase(text, "LightGrey"))
				{
					return Color.LightGray;
				}
				if (WebColorConverter.htmlSysColorTable == null)
				{
					WebColorConverter.InitializeHTMLSysColorTable();
				}
				object obj = WebColorConverter.htmlSysColorTable[text];
				if (obj != null)
				{
					return (Color)obj;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to a specified type.</summary>
		/// <returns>The object resulting from conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		// Token: 0x06001C20 RID: 7200 RVA: 0x0004641C File Offset: 0x0004461C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				Color color = (Color)value;
				if (color == Color.Empty)
				{
					return string.Empty;
				}
				if (!color.IsKnownColor)
				{
					StringBuilder stringBuilder = new StringBuilder("#", 7);
					stringBuilder.Append(color.R.ToString("X2", CultureInfo.InvariantCulture));
					stringBuilder.Append(color.G.ToString("X2", CultureInfo.InvariantCulture));
					stringBuilder.Append(color.B.ToString("X2", CultureInfo.InvariantCulture));
					return stringBuilder.ToString();
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000464FC File Offset: 0x000446FC
		private static void InitializeHTMLSysColorTable()
		{
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			hashtable["activeborder"] = Color.FromKnownColor(KnownColor.ActiveBorder);
			hashtable["activecaption"] = Color.FromKnownColor(KnownColor.ActiveCaption);
			hashtable["appworkspace"] = Color.FromKnownColor(KnownColor.AppWorkspace);
			hashtable["background"] = Color.FromKnownColor(KnownColor.Desktop);
			hashtable["buttonface"] = Color.FromKnownColor(KnownColor.Control);
			hashtable["buttonhighlight"] = Color.FromKnownColor(KnownColor.ControlLightLight);
			hashtable["buttonshadow"] = Color.FromKnownColor(KnownColor.ControlDark);
			hashtable["buttontext"] = Color.FromKnownColor(KnownColor.ControlText);
			hashtable["captiontext"] = Color.FromKnownColor(KnownColor.ActiveCaptionText);
			hashtable["graytext"] = Color.FromKnownColor(KnownColor.GrayText);
			hashtable["highlight"] = Color.FromKnownColor(KnownColor.Highlight);
			hashtable["highlighttext"] = Color.FromKnownColor(KnownColor.HighlightText);
			hashtable["inactiveborder"] = Color.FromKnownColor(KnownColor.InactiveBorder);
			hashtable["inactivecaption"] = Color.FromKnownColor(KnownColor.InactiveCaption);
			hashtable["inactivecaptiontext"] = Color.FromKnownColor(KnownColor.InactiveCaptionText);
			hashtable["infobackground"] = Color.FromKnownColor(KnownColor.Info);
			hashtable["infotext"] = Color.FromKnownColor(KnownColor.InfoText);
			hashtable["menu"] = Color.FromKnownColor(KnownColor.Menu);
			hashtable["menutext"] = Color.FromKnownColor(KnownColor.MenuText);
			hashtable["scrollbar"] = Color.FromKnownColor(KnownColor.ScrollBar);
			hashtable["threeddarkshadow"] = Color.FromKnownColor(KnownColor.ControlDarkDark);
			hashtable["threedface"] = Color.FromKnownColor(KnownColor.Control);
			hashtable["threedhighlight"] = Color.FromKnownColor(KnownColor.ControlLight);
			hashtable["threedlightshadow"] = Color.FromKnownColor(KnownColor.ControlLightLight);
			hashtable["window"] = Color.FromKnownColor(KnownColor.Window);
			hashtable["windowframe"] = Color.FromKnownColor(KnownColor.WindowFrame);
			hashtable["windowtext"] = Color.FromKnownColor(KnownColor.WindowText);
			WebColorConverter.htmlSysColorTable = hashtable;
		}

		// Token: 0x040017D8 RID: 6104
		private static Hashtable htmlSysColorTable;
	}
}
