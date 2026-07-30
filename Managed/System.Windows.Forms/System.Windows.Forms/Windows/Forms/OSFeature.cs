using System;

namespace System.Windows.Forms
{
	/// <summary>Provides operating-system specific feature queries.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027B RID: 635
	public class OSFeature : FeatureSupport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class. </summary>
		// Token: 0x06002977 RID: 10615 RVA: 0x0009FF18 File Offset: 0x0009E118
		protected OSFeature()
		{
		}

		/// <summary>Gets a static instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class to use for feature queries. This property is read-only. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.OSFeature" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x0009FF2C File Offset: 0x0009E12C
		public static OSFeature Feature
		{
			get
			{
				return OSFeature.feature;
			}
		}

		/// <summary>Retrieves a value indicating whether the operating system supports the specified feature or metric. </summary>
		/// <returns>true if the feature is available on the system; otherwise, false.</returns>
		/// <param name="enumVal">A <see cref="T:System.Windows.Forms.SystemParameter" /> representing the feature to search for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600297A RID: 10618 RVA: 0x0009FF34 File Offset: 0x0009E134
		public static bool IsPresent(SystemParameter enumVal)
		{
			switch (enumVal)
			{
			case SystemParameter.DropShadow:
				try
				{
					object obj = SystemInformation.IsDropShadowEnabled;
					return true;
				}
				catch (Exception)
				{
					return false;
				}
				break;
			case SystemParameter.FlatMenu:
				break;
			case SystemParameter.FontSmoothingContrastMetric:
				goto IL_0081;
			case SystemParameter.FontSmoothingTypeMetric:
				goto IL_00A5;
			case SystemParameter.MenuFadeEnabled:
				goto IL_00C9;
			case SystemParameter.SelectionFade:
				goto IL_00ED;
			case SystemParameter.ToolTipAnimationMetric:
				goto IL_0111;
			case SystemParameter.UIEffects:
				goto IL_0135;
			case SystemParameter.CaretWidthMetric:
				goto IL_0159;
			case SystemParameter.VerticalFocusThicknessMetric:
				goto IL_017D;
			case SystemParameter.HorizontalFocusThicknessMetric:
				goto IL_01A1;
			default:
				return false;
			}
			try
			{
				object obj = SystemInformation.IsFlatMenuEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_0081:
				object obj = SystemInformation.FontSmoothingContrast;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_00A5:
				object obj = SystemInformation.FontSmoothingType;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_00C9:
				object obj = SystemInformation.IsMenuFadeEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_00ED:
				object obj = SystemInformation.IsSelectionFadeEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_0111:
				object obj = SystemInformation.IsToolTipAnimationEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_0135:
				object obj = SystemInformation.UIEffectsEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_0159:
				object obj = SystemInformation.CaretWidth;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_017D:
				object obj = SystemInformation.VerticalFocusThickness;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			try
			{
				IL_01A1:
				object obj = SystemInformation.HorizontalFocusThickness;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			return false;
		}

		/// <summary>Retrieves the version of the specified feature currently available on the system. </summary>
		/// <returns>A <see cref="T:System.Version" /> representing the version of the specified operating system feature currently available on the system; or null if the feature cannot be found.</returns>
		/// <param name="feature">The feature whose version is requested, either <see cref="F:System.Windows.Forms.OSFeature.LayeredWindows" /> or <see cref="F:System.Windows.Forms.OSFeature.Themes" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600297B RID: 10619 RVA: 0x000A0218 File Offset: 0x0009E418
		public override Version GetVersionPresent(object feature)
		{
			if (feature == OSFeature.Themes)
			{
				return ThemeEngine.Current.Version;
			}
			return null;
		}

		// Token: 0x0400149B RID: 5275
		private static OSFeature feature = new OSFeature();

		/// <summary>Represents the layered, top-level windows feature. This field is read-only. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400149C RID: 5276
		public static readonly object LayeredWindows;

		/// <summary>Represents the operating system themes feature. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400149D RID: 5277
		public static readonly object Themes;
	}
}
