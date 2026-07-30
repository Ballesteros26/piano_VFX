using System;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public class GUILayout
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000795A File Offset: 0x00005B5A
		public static void Label(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007974 File Offset: 0x00005B74
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000798E File Offset: 0x00005B8E
		public static void Label(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, GUI.skin.label, options);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000079A3 File Offset: 0x00005BA3
		public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000079B4 File Offset: 0x00005BB4
		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000079C5 File Offset: 0x00005BC5
		public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, style, options);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000079D1 File Offset: 0x00005BD1
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000079E4 File Offset: 0x00005BE4
		public static void Box(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), GUI.skin.box, options);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000079FE File Offset: 0x00005BFE
		public static void Box(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), GUI.skin.box, options);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007A18 File Offset: 0x00005C18
		public static void Box(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, GUI.skin.box, options);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007A2D File Offset: 0x00005C2D
		public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007A3E File Offset: 0x00005C3E
		public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007A4F File Offset: 0x00005C4F
		public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, style, options);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007A5B File Offset: 0x00005C5B
		private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007A70 File Offset: 0x00005C70
		public static bool Button(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007A98 File Offset: 0x00005C98
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public static bool Button(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, GUI.skin.button, options);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007B04 File Offset: 0x00005D04
		public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007B24 File Offset: 0x00005D24
		public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, style, options);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007B40 File Offset: 0x00005D40
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007B64 File Offset: 0x00005D64
		public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007B8C File Offset: 0x00005D8C
		public static bool RepeatButton(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, GUI.skin.button, options);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007C18 File Offset: 0x00005E18
		public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, style, options);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007C34 File Offset: 0x00005E34
		private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007C58 File Offset: 0x00005E58
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007C80 File Offset: 0x00005E80
		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, style, options);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, style, options);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, GUI.skin.textField, options);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007D08 File Offset: 0x00005F08
		public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007D30 File Offset: 0x00005F30
		public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, style, options);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007D4C File Offset: 0x00005F4C
		public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIContent guicontent = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
			return GUI.PasswordField(GUILayoutUtility.GetRect(guicontent, GUI.skin.textField, options), password, maskChar, maxLength, style);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007D88 File Offset: 0x00005F88
		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, GUI.skin.textArea, options);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, style, options);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007E10 File Offset: 0x00006010
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			bool flag = GUIUtility.keyboardControl != controlID;
			if (flag)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + GUIUtility.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			bool flag2 = GUIUtility.keyboardControl == controlID;
			if (flag2)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007E8C File Offset: 0x0000608C
		public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007EB8 File Offset: 0x000060B8
		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007EE4 File Offset: 0x000060E4
		public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, GUI.skin.toggle, options);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007F08 File Offset: 0x00006108
		public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007F28 File Offset: 0x00006128
		public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007F48 File Offset: 0x00006148
		public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, style, options);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007F64 File Offset: 0x00006164
		private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007F88 File Offset: 0x00006188
		public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007FB4 File Offset: 0x000061B4
		public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007FE0 File Offset: 0x000061E0
		public static int Toolbar(int selected, GUIContent[] contents, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, GUI.skin.button, options);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008004 File Offset: 0x00006204
		public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, options);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008024 File Offset: 0x00006224
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, options);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008044 File Offset: 0x00006244
		public static int Toolbar(int selected, string[] texts, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, buttonSize, options);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008068 File Offset: 0x00006268
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, buttonSize, options);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000808C File Offset: 0x0000628C
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, style, GUI.ToolbarButtonSize.Fixed, options);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000080A8 File Offset: 0x000062A8
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, null, style, buttonSize, options);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000080C8 File Offset: 0x000062C8
		public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, enabled, style, GUI.ToolbarButtonSize.Fixed, options);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000080E8 File Offset: 0x000062E8
		public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			Vector2 vector = default(Vector2);
			int num = contents.Length;
			GUIStyle guistyle4 = ((num > 1) ? guistyle : style);
			GUIStyle guistyle5 = ((num > 1) ? guistyle2 : style);
			GUIStyle guistyle6 = ((num > 1) ? guistyle3 : style);
			float num2 = 0f;
			for (int i = 0; i < contents.Length; i++)
			{
				bool flag = i == num - 2;
				if (flag)
				{
					guistyle5 = guistyle6;
				}
				Vector2 vector2 = guistyle4.CalcSize(contents[i]);
				if (buttonSize != GUI.ToolbarButtonSize.Fixed)
				{
					if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
					{
						vector.x += vector2.x;
					}
				}
				else
				{
					bool flag2 = vector2.x > vector.x;
					if (flag2)
					{
						vector.x = vector2.x;
					}
				}
				bool flag3 = vector2.y > vector.y;
				if (flag3)
				{
					vector.y = vector2.y;
				}
				bool flag4 = i == num - 1;
				if (flag4)
				{
					num2 += (float)guistyle4.margin.right;
				}
				else
				{
					num2 += (float)Mathf.Max(guistyle4.margin.right, guistyle5.margin.left);
				}
				guistyle4 = guistyle5;
			}
			if (buttonSize != GUI.ToolbarButtonSize.Fixed)
			{
				if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
				{
					vector.x += num2;
				}
			}
			else
			{
				vector.x = vector.x * (float)contents.Length + num2;
			}
			return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, null, style, buttonSize, enabled);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000829C File Offset: 0x0000649C
		public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000082C8 File Offset: 0x000064C8
		public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000082F4 File Offset: 0x000064F4
		public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, content, xCount, GUI.skin.button, options);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000831C File Offset: 0x0000651C
		public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008340 File Offset: 0x00006540
		public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008364 File Offset: 0x00006564
		public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000838C File Offset: 0x0000658C
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000083BC File Offset: 0x000065BC
		public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000083DC File Offset: 0x000065DC
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000840C File Offset: 0x0000660C
		public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000843C File Offset: 0x0000663C
		public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000845C File Offset: 0x0000665C
		private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000848C File Offset: 0x0000668C
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000084B4 File Offset: 0x000066B4
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000084E4 File Offset: 0x000066E4
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000850C File Offset: 0x0000670C
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000853C File Offset: 0x0000673C
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			bool isVertical = GUILayoutUtility.current.topLevel.isVertical;
			if (isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Height(pixels) });
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Width(pixels) });
			}
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000085E8 File Offset: 0x000067E8
		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			bool isVertical = GUILayoutUtility.current.topLevel.isVertical;
			GUILayoutOption guilayoutOption;
			if (isVertical)
			{
				guilayoutOption = GUILayout.ExpandHeight(true);
			}
			else
			{
				guilayoutOption = GUILayout.ExpandWidth(true);
			}
			guilayoutOption.value = 10000;
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { guilayoutOption });
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008691 File Offset: 0x00006891
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000086A5 File Offset: 0x000068A5
		public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, style, options);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000086B5 File Offset: 0x000068B5
		public static void BeginHorizontal(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000086C6 File Offset: 0x000068C6
		public static void BeginHorizontal(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000086D8 File Offset: 0x000068D8
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			bool flag = style != GUIStyle.none || content != GUIContent.none;
			if (flag)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008727 File Offset: 0x00006927
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008730 File Offset: 0x00006930
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008744 File Offset: 0x00006944
		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, style, options);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008754 File Offset: 0x00006954
		public static void BeginVertical(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008765 File Offset: 0x00006965
		public static void BeginVertical(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008778 File Offset: 0x00006978
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			bool flag = style != GUIStyle.none || content != GUIContent.none;
			if (flag)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008727 File Offset: 0x00006927
		public static void EndVertical()
		{
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000087C7 File Offset: 0x000069C7
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000087DB File Offset: 0x000069DB
		public static void BeginArea(Rect screenRect, string text)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000087F0 File Offset: 0x000069F0
		public static void BeginArea(Rect screenRect, Texture image)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008805 File Offset: 0x00006A05
		public static void BeginArea(Rect screenRect, GUIContent content)
		{
			GUILayout.BeginArea(screenRect, content, GUIStyle.none);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008815 File Offset: 0x00006A15
		public static void BeginArea(Rect screenRect, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, style);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008825 File Offset: 0x00006A25
		public static void BeginArea(Rect screenRect, string text, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), style);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008836 File Offset: 0x00006A36
		public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), style);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008848 File Offset: 0x00006A48
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000088F0 File Offset: 0x00006AF0
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Used;
			if (!flag)
			{
				GUILayoutUtility.current.layoutGroups.Pop();
				GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				GUI.EndGroup();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000894C File Offset: 0x00006B4C
		public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008988 File Offset: 0x00006B88
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000089C4 File Offset: 0x00006BC4
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000089EC File Offset: 0x00006BEC
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
		{
			GUILayoutOption[] array = null;
			return GUILayout.BeginScrollView(scrollPosition, style, array);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008A08 File Offset: 0x00006C08
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
		{
			string name = style.name;
			GUIStyle guistyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
			bool flag = guistyle == null;
			if (flag)
			{
				guistyle = GUI.skin.verticalScrollbar;
			}
			GUIStyle guistyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
			bool flag2 = guistyle2 == null;
			if (flag2)
			{
				guistyle2 = GUI.skin.horizontalScrollbar;
			}
			return GUILayout.BeginScrollView(scrollPosition, false, false, guistyle2, guistyle, style, options);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008A84 File Offset: 0x00006C84
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUIScrollGroup guiscrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				guiscrollGroup.resetCoords = true;
				guiscrollGroup.isVertical = true;
				guiscrollGroup.stretchWidth = 1;
				guiscrollGroup.stretchHeight = 1;
				guiscrollGroup.verticalScrollbar = verticalScrollbar;
				guiscrollGroup.horizontalScrollbar = horizontalScrollbar;
				guiscrollGroup.needsVerticalScrollbar = alwaysShowVertical;
				guiscrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
				guiscrollGroup.ApplyOptions(options);
			}
			return GUI.BeginScrollView(guiscrollGroup.rect, scrollPosition, new Rect(0f, 0f, guiscrollGroup.clientWidth, guiscrollGroup.clientHeight), alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008B65 File Offset: 0x00006D65
		public static void EndScrollView()
		{
			GUILayout.EndScrollView(true);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008B6F File Offset: 0x00006D6F
		internal static void EndScrollView(bool handleScrollWheel)
		{
			GUILayoutUtility.EndLayoutGroup();
			GUI.EndScrollView(handleScrollWheel);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008B80 File Offset: 0x00006D80
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008BAC File Offset: 0x00006DAC
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, GUI.skin.window, options);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008C00 File Offset: 0x00006E00
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008C24 File Offset: 0x00006E24
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008C48 File Offset: 0x00006E48
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, style, options);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008C68 File Offset: 0x00006E68
		private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUILayout.LayoutedWindow layoutedWindow = new GUILayout.LayoutedWindow(func, screenRect, content, options, style);
			return GUI.Window(id, screenRect, new GUI.WindowFunction(layoutedWindow.DoWindow), content, style);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008CC4 File Offset: 0x00006EC4
		public static GUILayoutOption MinWidth(float minWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008D04 File Offset: 0x00006F04
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008D24 File Offset: 0x00006F24
		public static GUILayoutOption MinHeight(float minHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008D44 File Offset: 0x00006F44
		public static GUILayoutOption MaxHeight(float maxHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008D64 File Offset: 0x00006F64
		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, expand ? 1 : 0);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008D88 File Offset: 0x00006F88
		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, expand ? 1 : 0);
		}

		// Token: 0x0200001A RID: 26
		private sealed class LayoutedWindow
		{
			// Token: 0x0600021D RID: 541 RVA: 0x00008DAC File Offset: 0x00006FAC
			internal LayoutedWindow(GUI.WindowFunction f, Rect screenRect, GUIContent content, GUILayoutOption[] options, GUIStyle style)
			{
				this.m_Func = f;
				this.m_ScreenRect = screenRect;
				this.m_Options = options;
				this.m_Style = style;
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00008DD4 File Offset: 0x00006FD4
			public void DoWindow(int windowID)
			{
				GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
				EventType type = Event.current.type;
				if (type != EventType.Layout)
				{
					topLevel.ResetCursor();
				}
				else
				{
					topLevel.resetCoords = true;
					topLevel.rect = this.m_ScreenRect;
					bool flag = this.m_Options != null;
					if (flag)
					{
						topLevel.ApplyOptions(this.m_Options);
					}
					topLevel.isWindow = true;
					topLevel.windowID = windowID;
					topLevel.style = this.m_Style;
				}
				this.m_Func(windowID);
			}

			// Token: 0x0400007C RID: 124
			private readonly GUI.WindowFunction m_Func;

			// Token: 0x0400007D RID: 125
			private readonly Rect m_ScreenRect;

			// Token: 0x0400007E RID: 126
			private readonly GUILayoutOption[] m_Options;

			// Token: 0x0400007F RID: 127
			private readonly GUIStyle m_Style;
		}

		// Token: 0x0200001B RID: 27
		public class HorizontalScope : GUI.Scope
		{
			// Token: 0x0600021F RID: 543 RVA: 0x00008E5E File Offset: 0x0000705E
			public HorizontalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(options);
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00008E6F File Offset: 0x0000706F
			public HorizontalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(style, options);
			}

			// Token: 0x06000221 RID: 545 RVA: 0x00008E81 File Offset: 0x00007081
			public HorizontalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(text, style, options);
			}

			// Token: 0x06000222 RID: 546 RVA: 0x00008E94 File Offset: 0x00007094
			public HorizontalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(image, style, options);
			}

			// Token: 0x06000223 RID: 547 RVA: 0x00008EA7 File Offset: 0x000070A7
			public HorizontalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(content, style, options);
			}

			// Token: 0x06000224 RID: 548 RVA: 0x00008EBA File Offset: 0x000070BA
			protected override void CloseScope()
			{
				GUILayout.EndHorizontal();
			}
		}

		// Token: 0x0200001C RID: 28
		public class VerticalScope : GUI.Scope
		{
			// Token: 0x06000225 RID: 549 RVA: 0x00008EC3 File Offset: 0x000070C3
			public VerticalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(options);
			}

			// Token: 0x06000226 RID: 550 RVA: 0x00008ED4 File Offset: 0x000070D4
			public VerticalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(style, options);
			}

			// Token: 0x06000227 RID: 551 RVA: 0x00008EE6 File Offset: 0x000070E6
			public VerticalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(text, style, options);
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00008EF9 File Offset: 0x000070F9
			public VerticalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(image, style, options);
			}

			// Token: 0x06000229 RID: 553 RVA: 0x00008F0C File Offset: 0x0000710C
			public VerticalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(content, style, options);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00008F1F File Offset: 0x0000711F
			protected override void CloseScope()
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x0200001D RID: 29
		public class AreaScope : GUI.Scope
		{
			// Token: 0x0600022B RID: 555 RVA: 0x00008F28 File Offset: 0x00007128
			public AreaScope(Rect screenRect)
			{
				GUILayout.BeginArea(screenRect);
			}

			// Token: 0x0600022C RID: 556 RVA: 0x00008F39 File Offset: 0x00007139
			public AreaScope(Rect screenRect, string text)
			{
				GUILayout.BeginArea(screenRect, text);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00008F4B File Offset: 0x0000714B
			public AreaScope(Rect screenRect, Texture image)
			{
				GUILayout.BeginArea(screenRect, image);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x00008F5D File Offset: 0x0000715D
			public AreaScope(Rect screenRect, GUIContent content)
			{
				GUILayout.BeginArea(screenRect, content);
			}

			// Token: 0x0600022F RID: 559 RVA: 0x00008F6F File Offset: 0x0000716F
			public AreaScope(Rect screenRect, string text, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, text, style);
			}

			// Token: 0x06000230 RID: 560 RVA: 0x00008F82 File Offset: 0x00007182
			public AreaScope(Rect screenRect, Texture image, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, image, style);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x00008F95 File Offset: 0x00007195
			public AreaScope(Rect screenRect, GUIContent content, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, content, style);
			}

			// Token: 0x06000232 RID: 562 RVA: 0x00008FA8 File Offset: 0x000071A8
			protected override void CloseScope()
			{
				GUILayout.EndArea();
			}
		}

		// Token: 0x0200001E RID: 30
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000233 RID: 563 RVA: 0x00008FB1 File Offset: 0x000071B1
			// (set) Token: 0x06000234 RID: 564 RVA: 0x00008FB9 File Offset: 0x000071B9
			public Vector2 scrollPosition { get; private set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000235 RID: 565 RVA: 0x00008FC2 File Offset: 0x000071C2
			// (set) Token: 0x06000236 RID: 566 RVA: 0x00008FCA File Offset: 0x000071CA
			public bool handleScrollWheel { get; set; }

			// Token: 0x06000237 RID: 567 RVA: 0x00008FD3 File Offset: 0x000071D3
			public ScrollViewScope(Vector2 scrollPosition, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, options);
			}

			// Token: 0x06000238 RID: 568 RVA: 0x00008FF3 File Offset: 0x000071F3
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
			}

			// Token: 0x06000239 RID: 569 RVA: 0x00009016 File Offset: 0x00007216
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x0600023A RID: 570 RVA: 0x00009039 File Offset: 0x00007239
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, style, options);
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000905A File Offset: 0x0000725A
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x0600023C RID: 572 RVA: 0x00009081 File Offset: 0x00007281
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
			}

			// Token: 0x0600023D RID: 573 RVA: 0x000090AA File Offset: 0x000072AA
			protected override void CloseScope()
			{
				GUILayout.EndScrollView(this.handleScrollWheel);
			}
		}
	}
}
