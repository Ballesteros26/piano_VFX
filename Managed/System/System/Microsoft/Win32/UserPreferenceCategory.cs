using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent categories of user preferences.</summary>
	// Token: 0x020000DA RID: 218
	public enum UserPreferenceCategory
	{
		/// <summary>Indicates user preferences associated with accessibility features of the system for users with disabilities.</summary>
		// Token: 0x04000B9F RID: 2975
		Accessibility = 1,
		/// <summary>Indicates user preferences associated with system colors. This category includes such as the default color of windows or menus.</summary>
		// Token: 0x04000BA0 RID: 2976
		Color,
		/// <summary>Indicates user preferences associated with the system desktop. This category includes the background image or background image layout of the desktop.</summary>
		// Token: 0x04000BA1 RID: 2977
		Desktop,
		/// <summary>Indicates user preferences that are not associated with any other category.</summary>
		// Token: 0x04000BA2 RID: 2978
		General,
		/// <summary>Indicates user preferences for icon settings, including icon height and spacing.</summary>
		// Token: 0x04000BA3 RID: 2979
		Icon,
		/// <summary>Indicates user preferences for keyboard settings, such as the key down repeat rate and delay.</summary>
		// Token: 0x04000BA4 RID: 2980
		Keyboard,
		/// <summary>Indicates user preferences for menu settings, such as menu delays and text alignment.</summary>
		// Token: 0x04000BA5 RID: 2981
		Menu,
		/// <summary>Indicates user preferences for mouse settings, such as double-click time and mouse sensitivity.</summary>
		// Token: 0x04000BA6 RID: 2982
		Mouse,
		/// <summary>Indicates user preferences for policy settings, such as user rights and access levels.</summary>
		// Token: 0x04000BA7 RID: 2983
		Policy,
		/// <summary>Indicates the user preferences for system power settings. This category includes power feature settings, such as the idle time before the system automatically enters low power mode.</summary>
		// Token: 0x04000BA8 RID: 2984
		Power,
		/// <summary>Indicates user preferences associated with the screensaver.</summary>
		// Token: 0x04000BA9 RID: 2985
		Screensaver,
		/// <summary>Indicates user preferences associated with the dimensions and characteristics of windows on the system.</summary>
		// Token: 0x04000BAA RID: 2986
		Window,
		/// <summary>Indicates changes in user preferences for regional settings, such as the character encoding and culture strings.</summary>
		// Token: 0x04000BAB RID: 2987
		Locale,
		/// <summary>Indicates user preferences associated with visual styles, such as enabling or disabling visual styles and switching from one visual style to another.</summary>
		// Token: 0x04000BAC RID: 2988
		VisualStyle
	}
}
