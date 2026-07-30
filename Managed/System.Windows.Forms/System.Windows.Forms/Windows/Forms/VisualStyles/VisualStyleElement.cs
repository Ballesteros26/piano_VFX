using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies a control or user interface (UI) element that is drawn with visual styles.</summary>
	// Token: 0x02000539 RID: 1337
	public class VisualStyleElement
	{
		// Token: 0x06004DFA RID: 19962 RVA: 0x00135A74 File Offset: 0x00133C74
		internal VisualStyleElement(string className, int part, int state)
		{
			this.class_name = className;
			this.part = part;
			this.state = state;
		}

		/// <summary>Gets the class name of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A string that represents the class name of a visual style element.</returns>
		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x00135A94 File Offset: 0x00133C94
		public string ClassName
		{
			get
			{
				return this.class_name;
			}
		}

		/// <summary>Gets a value indicating the part of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the part of a visual style element.</returns>
		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x00135A9C File Offset: 0x00133C9C
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		/// <summary>Gets a value indicating the state of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the state of a visual style element.</returns>
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x00135AA4 File Offset: 0x00133CA4
		public int State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Creates a new visual style element from the specified class, part, and state values.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> with the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.State" /> properties initialized to the <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> parameters.</returns>
		/// <param name="className">A string that represents the class name of the visual style element to be created.</param>
		/// <param name="part">A value that represents the part of the visual style element to be created.</param>
		/// <param name="state">A value that represents the state of the visual style element to be created.</param>
		// Token: 0x06004DFE RID: 19966 RVA: 0x00135AAC File Offset: 0x00133CAC
		public static VisualStyleElement CreateElement(string className, int part, int state)
		{
			return new VisualStyleElement(className, part, state);
		}

		// Token: 0x04002C3F RID: 11327
		private const string BUTTON = "BUTTON";

		// Token: 0x04002C40 RID: 11328
		private const string CLOCK = "CLOCK";

		// Token: 0x04002C41 RID: 11329
		private const string COMBOBOX = "COMBOBOX";

		// Token: 0x04002C42 RID: 11330
		private const string DATEPICKER = "DATEPICKER";

		// Token: 0x04002C43 RID: 11331
		private const string EDIT = "EDIT";

		// Token: 0x04002C44 RID: 11332
		private const string EXPLORERBAR = "EXPLORERBAR";

		// Token: 0x04002C45 RID: 11333
		private const string HEADER = "HEADER";

		// Token: 0x04002C46 RID: 11334
		private const string LISTVIEW = "LISTVIEW";

		// Token: 0x04002C47 RID: 11335
		private const string MENU = "MENU";

		// Token: 0x04002C48 RID: 11336
		private const string MENUBAND = "MENUBAND";

		// Token: 0x04002C49 RID: 11337
		private const string PAGE = "PAGE";

		// Token: 0x04002C4A RID: 11338
		private const string PROGRESS = "PROGRESS";

		// Token: 0x04002C4B RID: 11339
		private const string REBAR = "REBAR";

		// Token: 0x04002C4C RID: 11340
		private const string SCROLLBAR = "SCROLLBAR";

		// Token: 0x04002C4D RID: 11341
		private const string SPIN = "SPIN";

		// Token: 0x04002C4E RID: 11342
		private const string STARTPANEL = "STARTPANEL";

		// Token: 0x04002C4F RID: 11343
		private const string STATUS = "STATUS";

		// Token: 0x04002C50 RID: 11344
		private const string TAB = "TAB";

		// Token: 0x04002C51 RID: 11345
		private const string TASKBAND = "TASKBAND";

		// Token: 0x04002C52 RID: 11346
		private const string TASKBAR = "TASKBAR";

		// Token: 0x04002C53 RID: 11347
		private const string TOOLBAR = "TOOLBAR";

		// Token: 0x04002C54 RID: 11348
		private const string TOOLTIP = "TOOLTIP";

		// Token: 0x04002C55 RID: 11349
		private const string TRACKBAR = "TRACKBAR";

		// Token: 0x04002C56 RID: 11350
		private const string TRAYNOTIFY = "TRAYNOTIFY";

		// Token: 0x04002C57 RID: 11351
		private const string TREEVIEW = "TREEVIEW";

		// Token: 0x04002C58 RID: 11352
		private const string WINDOW = "WINDOW";

		// Token: 0x04002C59 RID: 11353
		private string class_name;

		// Token: 0x04002C5A RID: 11354
		private int part;

		// Token: 0x04002C5B RID: 11355
		private int state;

		// Token: 0x0200053A RID: 1338
		private enum DATEPICKERPARTS
		{
			// Token: 0x04002C5D RID: 11357
			DP_DATEBORDER = 2,
			// Token: 0x04002C5E RID: 11358
			DP_SHOWCALENDARBUTTONRIGHT
		}

		// Token: 0x0200053B RID: 1339
		private enum DATEBORDERSTATES
		{
			// Token: 0x04002C60 RID: 11360
			DPDB_NORMAL = 1,
			// Token: 0x04002C61 RID: 11361
			DPDB_HOT,
			// Token: 0x04002C62 RID: 11362
			DPDB_FOCUSED,
			// Token: 0x04002C63 RID: 11363
			DPDB_DISABLED
		}

		// Token: 0x0200053C RID: 1340
		private enum SHOWCALENDARBUTTONRIGHTSTATES
		{
			// Token: 0x04002C65 RID: 11365
			DPSCBR_NORMAL = 1,
			// Token: 0x04002C66 RID: 11366
			DPSCBR_HOT,
			// Token: 0x04002C67 RID: 11367
			DPSCBR_PRESSED,
			// Token: 0x04002C68 RID: 11368
			DPSCBR_DISABLED
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for button-related controls. This class cannot be inherited. </summary>
		// Token: 0x0200053D RID: 1341
		public static class Button
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the check box control. This class cannot be inherited. </summary>
			// Token: 0x0200053E RID: 1342
			public static class CheckBox
			{
				/// <summary>Gets a visual style element that represents a disabled check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the checked state.</returns>
				// Token: 0x1700137A RID: 4986
				// (get) Token: 0x06004DFF RID: 19967 RVA: 0x00135AB8 File Offset: 0x00133CB8
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 8);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the checked state.</returns>
				// Token: 0x1700137B RID: 4987
				// (get) Token: 0x06004E00 RID: 19968 RVA: 0x00135AC8 File Offset: 0x00133CC8
				public static VisualStyleElement CheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the checked state.</returns>
				// Token: 0x1700137C RID: 4988
				// (get) Token: 0x06004E01 RID: 19969 RVA: 0x00135AD8 File Offset: 0x00133CD8
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the checked state.</returns>
				// Token: 0x1700137D RID: 4989
				// (get) Token: 0x06004E02 RID: 19970 RVA: 0x00135AE8 File Offset: 0x00133CE8
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the indeterminate state.</returns>
				// Token: 0x1700137E RID: 4990
				// (get) Token: 0x06004E03 RID: 19971 RVA: 0x00135AF8 File Offset: 0x00133CF8
				public static VisualStyleElement MixedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 12);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the indeterminate state.</returns>
				// Token: 0x1700137F RID: 4991
				// (get) Token: 0x06004E04 RID: 19972 RVA: 0x00135B08 File Offset: 0x00133D08
				public static VisualStyleElement MixedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 10);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the indeterminate state.</returns>
				// Token: 0x17001380 RID: 4992
				// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00135B18 File Offset: 0x00133D18
				public static VisualStyleElement MixedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 9);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the indeterminate state.</returns>
				// Token: 0x17001381 RID: 4993
				// (get) Token: 0x06004E06 RID: 19974 RVA: 0x00135B28 File Offset: 0x00133D28
				public static VisualStyleElement MixedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 11);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the unchecked state.</returns>
				// Token: 0x17001382 RID: 4994
				// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00135B38 File Offset: 0x00133D38
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the unchecked state.</returns>
				// Token: 0x17001383 RID: 4995
				// (get) Token: 0x06004E08 RID: 19976 RVA: 0x00135B48 File Offset: 0x00133D48
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the unchecked state.</returns>
				// Token: 0x17001384 RID: 4996
				// (get) Token: 0x06004E09 RID: 19977 RVA: 0x00135B58 File Offset: 0x00133D58
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the unchecked state. </returns>
				// Token: 0x17001385 RID: 4997
				// (get) Token: 0x06004E0A RID: 19978 RVA: 0x00135B68 File Offset: 0x00133D68
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the group box control. This class cannot be inherited. </summary>
			// Token: 0x0200053F RID: 1343
			public static class GroupBox
			{
				/// <summary>Gets a visual style element that represents a disabled group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled group box.</returns>
				// Token: 0x17001386 RID: 4998
				// (get) Token: 0x06004E0B RID: 19979 RVA: 0x00135B78 File Offset: 0x00133D78
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal group box.</returns>
				// Token: 0x17001387 RID: 4999
				// (get) Token: 0x06004E0C RID: 19980 RVA: 0x00135B88 File Offset: 0x00133D88
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 4, 1);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the button control. This class cannot be inherited. </summary>
			// Token: 0x02000540 RID: 1344
			public static class PushButton
			{
				/// <summary>Gets a visual style element that represents a default button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a default button.</returns>
				// Token: 0x17001388 RID: 5000
				// (get) Token: 0x06004E0D RID: 19981 RVA: 0x00135B98 File Offset: 0x00133D98
				public static VisualStyleElement Default
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled button.</returns>
				// Token: 0x17001389 RID: 5001
				// (get) Token: 0x06004E0E RID: 19982 RVA: 0x00135BA8 File Offset: 0x00133DA8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot button. </returns>
				// Token: 0x1700138A RID: 5002
				// (get) Token: 0x06004E0F RID: 19983 RVA: 0x00135BB8 File Offset: 0x00133DB8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal button.</returns>
				// Token: 0x1700138B RID: 5003
				// (get) Token: 0x06004E10 RID: 19984 RVA: 0x00135BC8 File Offset: 0x00133DC8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed button.</returns>
				// Token: 0x1700138C RID: 5004
				// (get) Token: 0x06004E11 RID: 19985 RVA: 0x00135BD8 File Offset: 0x00133DD8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the radio button control. This class cannot be inherited. </summary>
			// Token: 0x02000541 RID: 1345
			public static class RadioButton
			{
				/// <summary>Gets a visual style element that represents a disabled radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the checked state.</returns>
				// Token: 0x1700138D RID: 5005
				// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00135BE8 File Offset: 0x00133DE8
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 8);
					}
				}

				/// <summary>Gets a visual style element that represents a hot radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the checked state.</returns>
				// Token: 0x1700138E RID: 5006
				// (get) Token: 0x06004E13 RID: 19987 RVA: 0x00135BF8 File Offset: 0x00133DF8
				public static VisualStyleElement CheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a normal radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the checked state.</returns>
				// Token: 0x1700138F RID: 5007
				// (get) Token: 0x06004E14 RID: 19988 RVA: 0x00135C08 File Offset: 0x00133E08
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the checked state.</returns>
				// Token: 0x17001390 RID: 5008
				// (get) Token: 0x06004E15 RID: 19989 RVA: 0x00135C18 File Offset: 0x00133E18
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the unchecked state.</returns>
				// Token: 0x17001391 RID: 5009
				// (get) Token: 0x06004E16 RID: 19990 RVA: 0x00135C28 File Offset: 0x00133E28
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the unchecked state.</returns>
				// Token: 0x17001392 RID: 5010
				// (get) Token: 0x06004E17 RID: 19991 RVA: 0x00135C38 File Offset: 0x00133E38
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the unchecked state.</returns>
				// Token: 0x17001393 RID: 5011
				// (get) Token: 0x06004E18 RID: 19992 RVA: 0x00135C48 File Offset: 0x00133E48
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the unchecked state. </returns>
				// Token: 0x17001394 RID: 5012
				// (get) Token: 0x06004E19 RID: 19993 RVA: 0x00135C58 File Offset: 0x00133E58
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 2, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a user button. This class cannot be inherited.</summary>
			// Token: 0x02000542 RID: 1346
			public static class UserButton
			{
				/// <summary>Gets a visual style element that represents a user button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a user button. </returns>
				// Token: 0x17001395 RID: 5013
				// (get) Token: 0x06004E1A RID: 19994 RVA: 0x00135C68 File Offset: 0x00133E68
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 5, 0);
					}
				}
			}
		}

		/// <summary>Contains a class that provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the drop-down arrow of the combo box control. This class cannot be inherited.</summary>
		// Token: 0x02000543 RID: 1347
		public static class ComboBox
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the drop-down arrow of the combo box control. This class cannot be inherited. </summary>
			// Token: 0x02000544 RID: 1348
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the disabled state.</returns>
				// Token: 0x17001396 RID: 5014
				// (get) Token: 0x06004E1B RID: 19995 RVA: 0x00135C78 File Offset: 0x00133E78
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the hot state.</returns>
				// Token: 0x17001397 RID: 5015
				// (get) Token: 0x06004E1C RID: 19996 RVA: 0x00135C88 File Offset: 0x00133E88
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the normal state. </returns>
				// Token: 0x17001398 RID: 5016
				// (get) Token: 0x06004E1D RID: 19997 RVA: 0x00135C98 File Offset: 0x00133E98
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the pressed state.</returns>
				// Token: 0x17001399 RID: 5017
				// (get) Token: 0x06004E1E RID: 19998 RVA: 0x00135CA8 File Offset: 0x00133EA8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 3);
					}
				}
			}

			// Token: 0x02000545 RID: 1349
			internal static class Border
			{
				// Token: 0x1700139A RID: 5018
				// (get) Token: 0x06004E1F RID: 19999 RVA: 0x00135CB8 File Offset: 0x00133EB8
				public static VisualStyleElement Normal
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 1);
					}
				}

				// Token: 0x1700139B RID: 5019
				// (get) Token: 0x06004E20 RID: 20000 RVA: 0x00135CC8 File Offset: 0x00133EC8
				public static VisualStyleElement Hot
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 2);
					}
				}

				// Token: 0x1700139C RID: 5020
				// (get) Token: 0x06004E21 RID: 20001 RVA: 0x00135CD8 File Offset: 0x00133ED8
				public static VisualStyleElement Focused
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 3);
					}
				}

				// Token: 0x1700139D RID: 5021
				// (get) Token: 0x06004E22 RID: 20002 RVA: 0x00135CE8 File Offset: 0x00133EE8
				public static VisualStyleElement Disabled
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 4);
					}
				}
			}
		}

		// Token: 0x02000546 RID: 1350
		internal static class DatePicker
		{
			// Token: 0x02000547 RID: 1351
			public static class DateBorder
			{
				// Token: 0x1700139E RID: 5022
				// (get) Token: 0x06004E23 RID: 20003 RVA: 0x00135CF8 File Offset: 0x00133EF8
				public static VisualStyleElement Normal
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 2, 1);
					}
				}

				// Token: 0x1700139F RID: 5023
				// (get) Token: 0x06004E24 RID: 20004 RVA: 0x00135D08 File Offset: 0x00133F08
				public static VisualStyleElement Hot
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 2, 2);
					}
				}

				// Token: 0x170013A0 RID: 5024
				// (get) Token: 0x06004E25 RID: 20005 RVA: 0x00135D18 File Offset: 0x00133F18
				public static VisualStyleElement Focused
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 2, 3);
					}
				}

				// Token: 0x170013A1 RID: 5025
				// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00135D28 File Offset: 0x00133F28
				public static VisualStyleElement Disabled
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 2, 4);
					}
				}
			}

			// Token: 0x02000548 RID: 1352
			public static class ShowCalendarButtonRight
			{
				// Token: 0x170013A2 RID: 5026
				// (get) Token: 0x06004E27 RID: 20007 RVA: 0x00135D38 File Offset: 0x00133F38
				public static VisualStyleElement Normal
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 3, 1);
					}
				}

				// Token: 0x170013A3 RID: 5027
				// (get) Token: 0x06004E28 RID: 20008 RVA: 0x00135D48 File Offset: 0x00133F48
				public static VisualStyleElement Hot
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 3, 2);
					}
				}

				// Token: 0x170013A4 RID: 5028
				// (get) Token: 0x06004E29 RID: 20009 RVA: 0x00135D58 File Offset: 0x00133F58
				public static VisualStyleElement Pressed
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 3, 3);
					}
				}

				// Token: 0x170013A5 RID: 5029
				// (get) Token: 0x06004E2A RID: 20010 RVA: 0x00135D68 File Offset: 0x00133F68
				public static VisualStyleElement Disabled
				{
					get
					{
						return new VisualStyleElement("DATEPICKER", 3, 4);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the Explorer Bar. This class cannot be inherited.</summary>
		// Token: 0x02000549 RID: 1353
		public static class ExplorerBar
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the Explorer Bar. This class cannot be inherited. </summary>
			// Token: 0x0200054A RID: 1354
			public static class HeaderBackground
			{
				/// <summary>Gets a visual style element that represents the background of the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the Explorer Bar. </returns>
				// Token: 0x170013A6 RID: 5030
				// (get) Token: 0x06004E2B RID: 20011 RVA: 0x00135D78 File Offset: 0x00133F78
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 1, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x0200054B RID: 1355
			public static class HeaderClose
			{
				/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
				// Token: 0x170013A7 RID: 5031
				// (get) Token: 0x06004E2C RID: 20012 RVA: 0x00135D88 File Offset: 0x00133F88
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
				// Token: 0x170013A8 RID: 5032
				// (get) Token: 0x06004E2D RID: 20013 RVA: 0x00135D98 File Offset: 0x00133F98
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state. </returns>
				// Token: 0x170013A9 RID: 5033
				// (get) Token: 0x06004E2E RID: 20014 RVA: 0x00135DA8 File Offset: 0x00133FA8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Auto Hide button (which is displayed as a push pin) of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x0200054C RID: 1356
			public static class HeaderPin
			{
				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the hot state.</returns>
				// Token: 0x170013AA RID: 5034
				// (get) Token: 0x06004E2F RID: 20015 RVA: 0x00135DB8 File Offset: 0x00133FB8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the normal state.</returns>
				// Token: 0x170013AB RID: 5035
				// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00135DC8 File Offset: 0x00133FC8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the pressed state.</returns>
				// Token: 0x170013AC RID: 5036
				// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00135DD8 File Offset: 0x00133FD8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 3);
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the hot state.</returns>
				// Token: 0x170013AD RID: 5037
				// (get) Token: 0x06004E32 RID: 20018 RVA: 0x00135DE8 File Offset: 0x00133FE8
				public static VisualStyleElement SelectedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the normal state.</returns>
				// Token: 0x170013AE RID: 5038
				// (get) Token: 0x06004E33 RID: 20019 RVA: 0x00135DF8 File Offset: 0x00133FF8
				public static VisualStyleElement SelectedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the pressed state.</returns>
				// Token: 0x170013AF RID: 5039
				// (get) Token: 0x06004E34 RID: 20020 RVA: 0x00135E08 File Offset: 0x00134008
				public static VisualStyleElement SelectedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 3, 6);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expanded-menu arrow of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x0200054D RID: 1357
			public static class IEBarMenu
			{
				/// <summary>Gets a visual style element that represents a hot menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot menu button.</returns>
				// Token: 0x170013B0 RID: 5040
				// (get) Token: 0x06004E35 RID: 20021 RVA: 0x00135E18 File Offset: 0x00134018
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal menu button.</returns>
				// Token: 0x170013B1 RID: 5041
				// (get) Token: 0x06004E36 RID: 20022 RVA: 0x00135E28 File Offset: 0x00134028
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed menu button. </returns>
				// Token: 0x170013B2 RID: 5042
				// (get) Token: 0x06004E37 RID: 20023 RVA: 0x00135E38 File Offset: 0x00134038
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 4, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x0200054E RID: 1358
			public static class NormalGroupBackground
			{
				/// <summary>Gets a visual style element that represents the background of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a common group of items in the Explorer Bar. </returns>
				// Token: 0x170013B3 RID: 5043
				// (get) Token: 0x06004E38 RID: 20024 RVA: 0x00135E48 File Offset: 0x00134048
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 5, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x0200054F RID: 1359
			public static class NormalGroupCollapse
			{
				/// <summary>Gets a visual style element that represents a hot collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
				// Token: 0x170013B4 RID: 5044
				// (get) Token: 0x06004E39 RID: 20025 RVA: 0x00135E58 File Offset: 0x00134058
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 6, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
				// Token: 0x170013B5 RID: 5045
				// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00135E68 File Offset: 0x00134068
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
				// Token: 0x170013B6 RID: 5046
				// (get) Token: 0x06004E3B RID: 20027 RVA: 0x00135E78 File Offset: 0x00134078
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 6, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000550 RID: 1360
			public static class NormalGroupExpand
			{
				/// <summary>Gets a visual style element that represents a hot expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
				// Token: 0x170013B7 RID: 5047
				// (get) Token: 0x06004E3C RID: 20028 RVA: 0x00135E88 File Offset: 0x00134088
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 7, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
				// Token: 0x170013B8 RID: 5048
				// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00135E98 File Offset: 0x00134098
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button. </returns>
				// Token: 0x170013B9 RID: 5049
				// (get) Token: 0x06004E3E RID: 20030 RVA: 0x00135EA8 File Offset: 0x001340A8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 7, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000551 RID: 1361
			public static class NormalGroupHead
			{
				/// <summary>Gets a visual style element that represents the title bar of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a common group of items in the Explorer Bar. </returns>
				// Token: 0x170013BA RID: 5050
				// (get) Token: 0x06004E3F RID: 20031 RVA: 0x00135EB8 File Offset: 0x001340B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 8, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000552 RID: 1362
			public static class SpecialGroupBackground
			{
				/// <summary>Gets a visual style element that represents the background of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a special group of items in the Explorer Bar. </returns>
				// Token: 0x170013BB RID: 5051
				// (get) Token: 0x06004E40 RID: 20032 RVA: 0x00135EC8 File Offset: 0x001340C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 9, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000553 RID: 1363
			public static class SpecialGroupCollapse
			{
				/// <summary>Gets a visual style element that represents a hot collapse button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
				// Token: 0x170013BC RID: 5052
				// (get) Token: 0x06004E41 RID: 20033 RVA: 0x00135ED8 File Offset: 0x001340D8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 10, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal collapse button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
				// Token: 0x170013BD RID: 5053
				// (get) Token: 0x06004E42 RID: 20034 RVA: 0x00135EE8 File Offset: 0x001340E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 10, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed collapse button of a special group of items in the Explorer Bar. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
				// Token: 0x170013BE RID: 5054
				// (get) Token: 0x06004E43 RID: 20035 RVA: 0x00135EF8 File Offset: 0x001340F8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 10, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000554 RID: 1364
			public static class SpecialGroupExpand
			{
				/// <summary>Gets a visual style element that represents a hot expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
				// Token: 0x170013BF RID: 5055
				// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00135F08 File Offset: 0x00134108
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 11, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
				// Token: 0x170013C0 RID: 5056
				// (get) Token: 0x06004E45 RID: 20037 RVA: 0x00135F18 File Offset: 0x00134118
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 11, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button. </returns>
				// Token: 0x170013C1 RID: 5057
				// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00135F28 File Offset: 0x00134128
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 11, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000555 RID: 1365
			public static class SpecialGroupHead
			{
				/// <summary>Gets a visual style element that represents the title bar of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a special group of items in the Explorer Bar. </returns>
				// Token: 0x170013C2 RID: 5058
				// (get) Token: 0x06004E47 RID: 20039 RVA: 0x00135F38 File Offset: 0x00134138
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EXPLORERBAR", 12, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the header control. This class cannot be inherited.</summary>
		// Token: 0x02000556 RID: 1366
		public static class Header
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the header control. This class cannot be inherited. </summary>
			// Token: 0x02000557 RID: 1367
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a hot header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot header item.</returns>
				// Token: 0x170013C3 RID: 5059
				// (get) Token: 0x06004E48 RID: 20040 RVA: 0x00135F48 File Offset: 0x00134148
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal header item.</returns>
				// Token: 0x170013C4 RID: 5060
				// (get) Token: 0x06004E49 RID: 20041 RVA: 0x00135F58 File Offset: 0x00134158
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed header item. </returns>
				// Token: 0x170013C5 RID: 5061
				// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00135F68 File Offset: 0x00134168
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the leftmost item of the header control. This class cannot be inherited. </summary>
			// Token: 0x02000558 RID: 1368
			public static class ItemLeft
			{
				/// <summary>Gets a visual style element that represents the leftmost header item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the hot state.</returns>
				// Token: 0x170013C6 RID: 5062
				// (get) Token: 0x06004E4B RID: 20043 RVA: 0x00135F78 File Offset: 0x00134178
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the leftmost header item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the normal state.</returns>
				// Token: 0x170013C7 RID: 5063
				// (get) Token: 0x06004E4C RID: 20044 RVA: 0x00135F88 File Offset: 0x00134188
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the leftmost header item in the pressed state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the pressed state.</returns>
				// Token: 0x170013C8 RID: 5064
				// (get) Token: 0x06004E4D RID: 20045 RVA: 0x00135F98 File Offset: 0x00134198
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the rightmost item of the header control. This class cannot be inherited. </summary>
			// Token: 0x02000559 RID: 1369
			public static class ItemRight
			{
				/// <summary>Gets a visual style element that represents the rightmost header item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the hot state.</returns>
				// Token: 0x170013C9 RID: 5065
				// (get) Token: 0x06004E4E RID: 20046 RVA: 0x00135FA8 File Offset: 0x001341A8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the rightmost header item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the normal state.</returns>
				// Token: 0x170013CA RID: 5066
				// (get) Token: 0x06004E4F RID: 20047 RVA: 0x00135FB8 File Offset: 0x001341B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the rightmost header item in the pressed state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the pressed state.</returns>
				// Token: 0x170013CB RID: 5067
				// (get) Token: 0x06004E50 RID: 20048 RVA: 0x00135FC8 File Offset: 0x001341C8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the sort arrow of a header item. This class cannot be inherited. </summary>
			// Token: 0x0200055A RID: 1370
			public static class SortArrow
			{
				/// <summary>Gets a visual style element that represents a downward-pointing sort arrow.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing sort arrow.</returns>
				// Token: 0x170013CC RID: 5068
				// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00135FD8 File Offset: 0x001341D8
				public static VisualStyleElement SortedDown
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing sort arrow.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing sort arrow. </returns>
				// Token: 0x170013CD RID: 5069
				// (get) Token: 0x06004E52 RID: 20050 RVA: 0x00135FE8 File Offset: 0x001341E8
				public static VisualStyleElement SortedUp
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 4, 1);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the list view control. This class cannot be inherited.</summary>
		// Token: 0x0200055B RID: 1371
		public static class ListView
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view in detail view. This class cannot be inherited.</summary>
			// Token: 0x0200055C RID: 1372
			public static class Detail
			{
				/// <summary>Gets a visual style element that represents a list view in detail view. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a list view in detail view.</returns>
				// Token: 0x170013CE RID: 5070
				// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00135FF8 File Offset: 0x001341F8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the text area of a list view that contains no items. This class cannot be inherited.</summary>
			// Token: 0x0200055D RID: 1373
			public static class EmptyText
			{
				/// <summary>Gets a visual style element that represents the text area of a list view that contains no items. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the text area that accompanies an empty list view.</returns>
				// Token: 0x170013CF RID: 5071
				// (get) Token: 0x06004E54 RID: 20052 RVA: 0x00136008 File Offset: 0x00134208
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view item group. This class cannot be inherited.</summary>
			// Token: 0x0200055E RID: 1374
			public static class Group
			{
				/// <summary>Gets a visual style element that represents a list view item group. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group of list view items.</returns>
				// Token: 0x170013D0 RID: 5072
				// (get) Token: 0x06004E55 RID: 20053 RVA: 0x00136018 File Offset: 0x00134218
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 2, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the list view control. This class cannot be inherited. </summary>
			// Token: 0x0200055F RID: 1375
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a disabled list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled list view item.</returns>
				// Token: 0x170013D1 RID: 5073
				// (get) Token: 0x06004E56 RID: 20054 RVA: 0x00136028 File Offset: 0x00134228
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot list view item.</returns>
				// Token: 0x170013D2 RID: 5074
				// (get) Token: 0x06004E57 RID: 20055 RVA: 0x00136038 File Offset: 0x00134238
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal list view item.</returns>
				// Token: 0x170013D3 RID: 5075
				// (get) Token: 0x06004E58 RID: 20056 RVA: 0x00136048 File Offset: 0x00134248
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a selected list view item that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item that has focus.</returns>
				// Token: 0x170013D4 RID: 5076
				// (get) Token: 0x06004E59 RID: 20057 RVA: 0x00136058 File Offset: 0x00134258
				public static VisualStyleElement Selected
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 1, 3);
					}
				}

				/// <summary>Gets a visual style element that represents a selected list view item without focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item without focus. </returns>
				// Token: 0x170013D5 RID: 5077
				// (get) Token: 0x06004E5A RID: 20058 RVA: 0x00136068 File Offset: 0x00134268
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 1, 5);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a sorted list view control in detail view This class cannot be inherited.</summary>
			// Token: 0x02000560 RID: 1376
			public static class SortedDetail
			{
				/// <summary>Gets a visual style element that represents a sorted list view control in detail view. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sorted list view control in detail view.</returns>
				// Token: 0x170013D6 RID: 5078
				// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00136078 File Offset: 0x00134278
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("LISTVIEW", 4, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu. This class cannot be inherited. </summary>
		// Token: 0x02000561 RID: 1377
		public static class Menu
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu bar. This class cannot be inherited. </summary>
			// Token: 0x02000562 RID: 1378
			public static class BarDropDown
			{
				/// <summary>Gets a visual style element that represents the drop-down arrow of a menu bar. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu bar.</returns>
				// Token: 0x170013D7 RID: 5079
				// (get) Token: 0x06004E5C RID: 20060 RVA: 0x00136088 File Offset: 0x00134288
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 4, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu bar item. This class cannot be inherited. </summary>
			// Token: 0x02000563 RID: 1379
			public static class BarItem
			{
				/// <summary>Gets a visual style element that represents a menu bar item. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu bar item.</returns>
				// Token: 0x170013D8 RID: 5080
				// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00136098 File Offset: 0x00134298
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the chevron of a menu. This class cannot be inherited. </summary>
			// Token: 0x02000564 RID: 1380
			public static class Chevron
			{
				/// <summary>Gets a visual style element that represents a menu chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu chevron. </returns>
				// Token: 0x170013D9 RID: 5081
				// (get) Token: 0x06004E5E RID: 20062 RVA: 0x001360A8 File Offset: 0x001342A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu. This class cannot be inherited. </summary>
			// Token: 0x02000565 RID: 1381
			public static class DropDown
			{
				/// <summary>Gets a visual style element that represents the drop-down arrow of a menu. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu.</returns>
				// Token: 0x170013DA RID: 5082
				// (get) Token: 0x06004E5F RID: 20063 RVA: 0x001360B8 File Offset: 0x001342B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 2, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a menu item. This class cannot be inherited. </summary>
			// Token: 0x02000566 RID: 1382
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a menu item that has been demoted.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item that has been demoted.</returns>
				// Token: 0x170013DB RID: 5083
				// (get) Token: 0x06004E60 RID: 20064 RVA: 0x001360C8 File Offset: 0x001342C8
				public static VisualStyleElement Demoted
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 1, 3);
					}
				}

				/// <summary>Gets a visual style element that represents a menu item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the normal state.</returns>
				// Token: 0x170013DC RID: 5084
				// (get) Token: 0x06004E61 RID: 20065 RVA: 0x001360D8 File Offset: 0x001342D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a menu item in the selected state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the selected state.</returns>
				// Token: 0x170013DD RID: 5085
				// (get) Token: 0x06004E62 RID: 20066 RVA: 0x001360E8 File Offset: 0x001342E8
				public static VisualStyleElement Selected
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 1, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu item separator. This class cannot be inherited. </summary>
			// Token: 0x02000567 RID: 1383
			public static class Separator
			{
				/// <summary>Gets a visual style element that represents a menu item separator.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item separator. </returns>
				// Token: 0x170013DE RID: 5086
				// (get) Token: 0x06004E63 RID: 20067 RVA: 0x001360F8 File Offset: 0x001342F8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENU", 6, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu band. This class cannot be inherited.</summary>
		// Token: 0x02000568 RID: 1384
		public static class MenuBand
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the new application button of a menu band. This class cannot be inherited. </summary>
			// Token: 0x02000569 RID: 1385
			public static class NewApplicationButton
			{
				/// <summary>Gets a visual style element that represents the new application button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the checked state.</returns>
				// Token: 0x170013DF RID: 5087
				// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00136108 File Offset: 0x00134308
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the disabled state.</returns>
				// Token: 0x170013E0 RID: 5088
				// (get) Token: 0x06004E65 RID: 20069 RVA: 0x00136118 File Offset: 0x00134318
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot state.</returns>
				// Token: 0x170013E1 RID: 5089
				// (get) Token: 0x06004E66 RID: 20070 RVA: 0x00136128 File Offset: 0x00134328
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot and checked states.</returns>
				// Token: 0x170013E2 RID: 5090
				// (get) Token: 0x06004E67 RID: 20071 RVA: 0x00136138 File Offset: 0x00134338
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the normal state.</returns>
				// Token: 0x170013E3 RID: 5091
				// (get) Token: 0x06004E68 RID: 20072 RVA: 0x00136148 File Offset: 0x00134348
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the pressed state. </returns>
				// Token: 0x170013E4 RID: 5092
				// (get) Token: 0x06004E69 RID: 20073 RVA: 0x00136158 File Offset: 0x00134358
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 1, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu band separator. This class cannot be inherited. </summary>
			// Token: 0x0200056A RID: 1386
			public static class Separator
			{
				/// <summary>Gets a visual style element that represents a separator between items in a menu band.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a separator between items in a menu band.</returns>
				// Token: 0x170013E5 RID: 5093
				// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00136168 File Offset: 0x00134368
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("MENUBAND", 2, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a page. This class cannot be inherited.</summary>
		// Token: 0x0200056B RID: 1387
		public static class Page
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a down indicator in an up-down or spin box control. This class cannot be inherited. </summary>
			// Token: 0x0200056C RID: 1388
			public static class Down
			{
				/// <summary>Gets a visual style element that represents the disabled state of the down indicator in an up-down or spin box control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box control in the disabled state.</returns>
				// Token: 0x170013E6 RID: 5094
				// (get) Token: 0x06004E6B RID: 20075 RVA: 0x00136178 File Offset: 0x00134378
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a down indicator of an up-down or spin box control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the down indicator of an up-down or spin box in the hot state.</returns>
				// Token: 0x170013E7 RID: 5095
				// (get) Token: 0x06004E6C RID: 20076 RVA: 0x00136188 File Offset: 0x00134388
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator up an up-down or spin box control in the normal state.</returns>
				// Token: 0x170013E8 RID: 5096
				// (get) Token: 0x06004E6D RID: 20077 RVA: 0x00136198 File Offset: 0x00134398
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 2, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box in the pressed state. </returns>
				// Token: 0x170013E9 RID: 5097
				// (get) Token: 0x06004E6E RID: 20078 RVA: 0x001361A8 File Offset: 0x001343A8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 2, 1);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page backward indicator in a pager control. This class cannot be inherited. </summary>
			// Token: 0x0200056D RID: 1389
			public static class DownHorizontal
			{
				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the disabled state.</returns>
				// Token: 0x170013EA RID: 5098
				// (get) Token: 0x06004E6F RID: 20079 RVA: 0x001361B8 File Offset: 0x001343B8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the hot state.</returns>
				// Token: 0x170013EB RID: 5099
				// (get) Token: 0x06004E70 RID: 20080 RVA: 0x001361C8 File Offset: 0x001343C8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the normal state.</returns>
				// Token: 0x170013EC RID: 5100
				// (get) Token: 0x06004E71 RID: 20081 RVA: 0x001361D8 File Offset: 0x001343D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents page backward indicator of a pager control in the pressed state. </returns>
				// Token: 0x170013ED RID: 5101
				// (get) Token: 0x06004E72 RID: 20082 RVA: 0x001361E8 File Offset: 0x001343E8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page up indicator of an up-down or spin box control. This class cannot be inherited. </summary>
			// Token: 0x0200056E RID: 1390
			public static class Up
			{
				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the disabled state.</returns>
				// Token: 0x170013EE RID: 5102
				// (get) Token: 0x06004E73 RID: 20083 RVA: 0x001361F8 File Offset: 0x001343F8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the hot state.</returns>
				// Token: 0x170013EF RID: 5103
				// (get) Token: 0x06004E74 RID: 20084 RVA: 0x00136208 File Offset: 0x00134408
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the normal state.</returns>
				// Token: 0x170013F0 RID: 5104
				// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00136218 File Offset: 0x00134418
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the pressed state. </returns>
				// Token: 0x170013F1 RID: 5105
				// (get) Token: 0x06004E76 RID: 20086 RVA: 0x00136228 File Offset: 0x00134428
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page forward indicator of a pager control. This class cannot be inherited. </summary>
			// Token: 0x0200056F RID: 1391
			public static class UpHorizontal
			{
				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the disabled state.</returns>
				// Token: 0x170013F2 RID: 5106
				// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00136238 File Offset: 0x00134438
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the hot state.</returns>
				// Token: 0x170013F3 RID: 5107
				// (get) Token: 0x06004E78 RID: 20088 RVA: 0x00136248 File Offset: 0x00134448
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the normal state.</returns>
				// Token: 0x170013F4 RID: 5108
				// (get) Token: 0x06004E79 RID: 20089 RVA: 0x00136258 File Offset: 0x00134458
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the pressed state. </returns>
				// Token: 0x170013F5 RID: 5109
				// (get) Token: 0x06004E7A RID: 20090 RVA: 0x00136268 File Offset: 0x00134468
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("PAGE", 3, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the progress bar control. This class cannot be inherited.</summary>
		// Token: 0x02000570 RID: 1392
		public static class ProgressBar
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a horizontal progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000571 RID: 1393
			public static class Bar
			{
				/// <summary>Gets a visual style element that represents a horizontal progress bar frame.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal progress bar frame. </returns>
				// Token: 0x170013F6 RID: 5110
				// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00136278 File Offset: 0x00134478
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PROGRESS", 1, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a vertical progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000572 RID: 1394
			public static class BarVertical
			{
				/// <summary>Gets a visual style element that represents a vertical progress bar frame.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical progress bar frame.</returns>
				// Token: 0x170013F7 RID: 5111
				// (get) Token: 0x06004E7C RID: 20092 RVA: 0x00136288 File Offset: 0x00134488
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PROGRESS", 2, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a horizontal progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000573 RID: 1395
			public static class Chunk
			{
				/// <summary>Gets a visual style element that represents the pieces that fill a horizontal progress bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a horizontal progress bar. </returns>
				// Token: 0x170013F8 RID: 5112
				// (get) Token: 0x06004E7D RID: 20093 RVA: 0x00136298 File Offset: 0x00134498
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PROGRESS", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a vertical progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000574 RID: 1396
			public static class ChunkVertical
			{
				/// <summary>Gets a visual style element that represents the pieces that fill a vertical progress bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a vertical progress bar. </returns>
				// Token: 0x170013F9 RID: 5113
				// (get) Token: 0x06004E7E RID: 20094 RVA: 0x001362A8 File Offset: 0x001344A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("PROGRESS", 4, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the rebar control. This class cannot be inherited.</summary>
		// Token: 0x02000575 RID: 1397
		public static class Rebar
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a rebar band. This class cannot be inherited.</summary>
			// Token: 0x02000576 RID: 1398
			public static class Band
			{
				/// <summary>Gets a visual style element that represents a rebar band. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a rebar band.</returns>
				// Token: 0x170013FA RID: 5114
				// (get) Token: 0x06004E7F RID: 20095 RVA: 0x001362B8 File Offset: 0x001344B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 3, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal chevron. This class cannot be inherited. </summary>
			// Token: 0x02000577 RID: 1399
			public static class Chevron
			{
				/// <summary>Gets a visual style element that represents a hot chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
				// Token: 0x170013FB RID: 5115
				// (get) Token: 0x06004E80 RID: 20096 RVA: 0x001362C8 File Offset: 0x001344C8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
				// Token: 0x170013FC RID: 5116
				// (get) Token: 0x06004E81 RID: 20097 RVA: 0x001362D8 File Offset: 0x001344D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron.</returns>
				// Token: 0x170013FD RID: 5117
				// (get) Token: 0x06004E82 RID: 20098 RVA: 0x001362E8 File Offset: 0x001344E8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a chevron. This class cannot be inherited. </summary>
			// Token: 0x02000578 RID: 1400
			public static class ChevronVertical
			{
				/// <summary>Gets a visual style element that represents a hot chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
				// Token: 0x170013FE RID: 5118
				// (get) Token: 0x06004E83 RID: 20099 RVA: 0x001362F8 File Offset: 0x001344F8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 5, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
				// Token: 0x170013FF RID: 5119
				// (get) Token: 0x06004E84 RID: 20100 RVA: 0x00136308 File Offset: 0x00134508
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron. </returns>
				// Token: 0x17001400 RID: 5120
				// (get) Token: 0x06004E85 RID: 20101 RVA: 0x00136318 File Offset: 0x00134518
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 5, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a horizontal rebar control. This class cannot be inherited.</summary>
			// Token: 0x02000579 RID: 1401
			public static class Gripper
			{
				/// <summary>Gets a visual style element that represents a gripper bar for a horizontal rebar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a horizontal rebar. </returns>
				// Token: 0x17001401 RID: 5121
				// (get) Token: 0x06004E86 RID: 20102 RVA: 0x00136328 File Offset: 0x00134528
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 1, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a vertical rebar. This class cannot be inherited.</summary>
			// Token: 0x0200057A RID: 1402
			public static class GripperVertical
			{
				/// <summary>Gets a visual style element that represents a gripper bar for a vertical rebar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a vertical rebar.</returns>
				// Token: 0x17001402 RID: 5122
				// (get) Token: 0x06004E87 RID: 20103 RVA: 0x00136338 File Offset: 0x00134538
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("REBAR", 2, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the scroll bar control. This class cannot be inherited.</summary>
		// Token: 0x0200057B RID: 1403
		public static class ScrollBar
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state and direction of a scroll arrow. This class cannot be inherited. </summary>
			// Token: 0x0200057C RID: 1404
			public static class ArrowButton
			{
				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17001403 RID: 5123
				// (get) Token: 0x06004E88 RID: 20104 RVA: 0x00136348 File Offset: 0x00134548
				public static VisualStyleElement DownDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 8);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17001404 RID: 5124
				// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00136358 File Offset: 0x00134558
				public static VisualStyleElement DownHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001405 RID: 5125
				// (get) Token: 0x06004E8A RID: 20106 RVA: 0x00136368 File Offset: 0x00134568
				public static VisualStyleElement DownNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x17001406 RID: 5126
				// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00136378 File Offset: 0x00134578
				public static VisualStyleElement DownPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17001407 RID: 5127
				// (get) Token: 0x06004E8C RID: 20108 RVA: 0x00136388 File Offset: 0x00134588
				public static VisualStyleElement LeftDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 12);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17001408 RID: 5128
				// (get) Token: 0x06004E8D RID: 20109 RVA: 0x00136398 File Offset: 0x00134598
				public static VisualStyleElement LeftHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 10);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001409 RID: 5129
				// (get) Token: 0x06004E8E RID: 20110 RVA: 0x001363A8 File Offset: 0x001345A8
				public static VisualStyleElement LeftNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 9);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x1700140A RID: 5130
				// (get) Token: 0x06004E8F RID: 20111 RVA: 0x001363B8 File Offset: 0x001345B8
				public static VisualStyleElement LeftPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 11);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x1700140B RID: 5131
				// (get) Token: 0x06004E90 RID: 20112 RVA: 0x001363C8 File Offset: 0x001345C8
				public static VisualStyleElement RightDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 16);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the hot state.</returns>
				// Token: 0x1700140C RID: 5132
				// (get) Token: 0x06004E91 RID: 20113 RVA: 0x001363D8 File Offset: 0x001345D8
				public static VisualStyleElement RightHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 14);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the normal state.</returns>
				// Token: 0x1700140D RID: 5133
				// (get) Token: 0x06004E92 RID: 20114 RVA: 0x001363E8 File Offset: 0x001345E8
				public static VisualStyleElement RightNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 13);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x1700140E RID: 5134
				// (get) Token: 0x06004E93 RID: 20115 RVA: 0x001363F8 File Offset: 0x001345F8
				public static VisualStyleElement RightPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 15);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x1700140F RID: 5135
				// (get) Token: 0x06004E94 RID: 20116 RVA: 0x00136408 File Offset: 0x00134608
				public static VisualStyleElement UpDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17001410 RID: 5136
				// (get) Token: 0x06004E95 RID: 20117 RVA: 0x00136418 File Offset: 0x00134618
				public static VisualStyleElement UpHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001411 RID: 5137
				// (get) Token: 0x06004E96 RID: 20118 RVA: 0x00136428 File Offset: 0x00134628
				public static VisualStyleElement UpNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the pressed state. </returns>
				// Token: 0x17001412 RID: 5138
				// (get) Token: 0x06004E97 RID: 20119 RVA: 0x00136438 File Offset: 0x00134638
				public static VisualStyleElement UpPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 3);
					}
				}

				// Token: 0x17001413 RID: 5139
				// (get) Token: 0x06004E98 RID: 20120 RVA: 0x00136448 File Offset: 0x00134648
				internal static VisualStyleElement DownHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 18);
					}
				}

				// Token: 0x17001414 RID: 5140
				// (get) Token: 0x06004E99 RID: 20121 RVA: 0x00136458 File Offset: 0x00134658
				internal static VisualStyleElement LeftHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 19);
					}
				}

				// Token: 0x17001415 RID: 5141
				// (get) Token: 0x06004E9A RID: 20122 RVA: 0x00136468 File Offset: 0x00134668
				internal static VisualStyleElement RightHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 20);
					}
				}

				// Token: 0x17001416 RID: 5142
				// (get) Token: 0x06004E9B RID: 20123 RVA: 0x00136478 File Offset: 0x00134678
				internal static VisualStyleElement UpHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 17);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a horizontal scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x0200057D RID: 1405
			public static class GripperHorizontal
			{
				/// <summary>Gets a visual style element that represents a grip for a horizontal scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a horizontal scroll box. </returns>
				// Token: 0x17001417 RID: 5143
				// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00136488 File Offset: 0x00134688
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 8, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x0200057E RID: 1406
			public static class GripperVertical
			{
				/// <summary>Gets a visual style element that represents a grip for a vertical scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a vertical scroll box. </returns>
				// Token: 0x17001418 RID: 5144
				// (get) Token: 0x06004E9D RID: 20125 RVA: 0x00136498 File Offset: 0x00134698
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 9, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left part of a horizontal scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x0200057F RID: 1407
			public static class LeftTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x17001419 RID: 5145
				// (get) Token: 0x06004E9E RID: 20126 RVA: 0x001364A8 File Offset: 0x001346A8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the hot state.</returns>
				// Token: 0x1700141A RID: 5146
				// (get) Token: 0x06004E9F RID: 20127 RVA: 0x001364B8 File Offset: 0x001346B8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x1700141B RID: 5147
				// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x001364C8 File Offset: 0x001346C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x1700141C RID: 5148
				// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x001364D8 File Offset: 0x001346D8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the lower part of a vertical scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x02000580 RID: 1408
			public static class LowerTrackVertical
			{
				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the disabled state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x1700141D RID: 5149
				// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x001364E8 File Offset: 0x001346E8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the hot state.</returns>
				// Token: 0x1700141E RID: 5150
				// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x001364F8 File Offset: 0x001346F8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the normal state.</returns>
				// Token: 0x1700141F RID: 5151
				// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x00136508 File Offset: 0x00134708
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the pressed state. </returns>
				// Token: 0x17001420 RID: 5152
				// (get) Token: 0x06004EA5 RID: 20133 RVA: 0x00136518 File Offset: 0x00134718
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right part of a horizontal scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x02000581 RID: 1409
			public static class RightTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x17001421 RID: 5153
				// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x00136528 File Offset: 0x00134728
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the hot state.</returns>
				// Token: 0x17001422 RID: 5154
				// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x00136538 File Offset: 0x00134738
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x17001423 RID: 5155
				// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x00136548 File Offset: 0x00134748
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the pressed state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x17001424 RID: 5156
				// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x00136558 File Offset: 0x00134758
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the sizing handle of a scroll bar. This class cannot be inherited. </summary>
			// Token: 0x02000582 RID: 1410
			public static class SizeBox
			{
				/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the left.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the left.</returns>
				// Token: 0x17001425 RID: 5157
				// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00136568 File Offset: 0x00134768
				public static VisualStyleElement LeftAlign
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 10, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the right.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the right. </returns>
				// Token: 0x17001426 RID: 5158
				// (get) Token: 0x06004EAB RID: 20139 RVA: 0x00136578 File Offset: 0x00134778
				public static VisualStyleElement RightAlign
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 10, 1);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal scroll box (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x02000583 RID: 1411
			public static class ThumbButtonHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
				// Token: 0x17001427 RID: 5159
				// (get) Token: 0x06004EAC RID: 20140 RVA: 0x00136588 File Offset: 0x00134788
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
				// Token: 0x17001428 RID: 5160
				// (get) Token: 0x06004EAD RID: 20141 RVA: 0x00136598 File Offset: 0x00134798
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
				// Token: 0x17001429 RID: 5161
				// (get) Token: 0x06004EAE RID: 20142 RVA: 0x001365A8 File Offset: 0x001347A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
				// Token: 0x1700142A RID: 5162
				// (get) Token: 0x06004EAF RID: 20143 RVA: 0x001365B8 File Offset: 0x001347B8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000584 RID: 1412
			public static class ThumbButtonVertical
			{
				/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
				// Token: 0x1700142B RID: 5163
				// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x001365C8 File Offset: 0x001347C8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
				// Token: 0x1700142C RID: 5164
				// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x001365D8 File Offset: 0x001347D8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
				// Token: 0x1700142D RID: 5165
				// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x001365E8 File Offset: 0x001347E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state. </returns>
				// Token: 0x1700142E RID: 5166
				// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x001365F8 File Offset: 0x001347F8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upper part of a vertical scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x02000585 RID: 1413
			public static class UpperTrackVertical
			{
				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x1700142F RID: 5167
				// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00136608 File Offset: 0x00134808
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 7, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the hot state.</returns>
				// Token: 0x17001430 RID: 5168
				// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x00136618 File Offset: 0x00134818
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 7, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the normal state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the normal state.</returns>
				// Token: 0x17001431 RID: 5169
				// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x00136628 File Offset: 0x00134828
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the pressed state. </returns>
				// Token: 0x17001432 RID: 5170
				// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00136638 File Offset: 0x00134838
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 7, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the arrows of a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
		// Token: 0x02000586 RID: 1414
		public static class Spin
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
			// Token: 0x02000587 RID: 1415
			public static class Down
			{
				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x17001433 RID: 5171
				// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x00136648 File Offset: 0x00134848
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the hot state.</returns>
				// Token: 0x17001434 RID: 5172
				// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00136658 File Offset: 0x00134858
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001435 RID: 5173
				// (get) Token: 0x06004EBA RID: 20154 RVA: 0x00136668 File Offset: 0x00134868
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the pressed state.</returns>
				// Token: 0x17001436 RID: 5174
				// (get) Token: 0x06004EBB RID: 20155 RVA: 0x00136678 File Offset: 0x00134878
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
			// Token: 0x02000588 RID: 1416
			public static class DownHorizontal
			{
				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the disabled state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x17001437 RID: 5175
				// (get) Token: 0x06004EBC RID: 20156 RVA: 0x00136688 File Offset: 0x00134888
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the hot state.</returns>
				// Token: 0x17001438 RID: 5176
				// (get) Token: 0x06004EBD RID: 20157 RVA: 0x00136698 File Offset: 0x00134898
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001439 RID: 5177
				// (get) Token: 0x06004EBE RID: 20158 RVA: 0x001366A8 File Offset: 0x001348A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the pressed state. </returns>
				// Token: 0x1700143A RID: 5178
				// (get) Token: 0x06004EBF RID: 20159 RVA: 0x001366B8 File Offset: 0x001348B8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
			// Token: 0x02000589 RID: 1417
			public static class Up
			{
				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x1700143B RID: 5179
				// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x001366C8 File Offset: 0x001348C8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the hot state.</returns>
				// Token: 0x1700143C RID: 5180
				// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001366D8 File Offset: 0x001348D8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the normal state. </returns>
				// Token: 0x1700143D RID: 5181
				// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x001366E8 File Offset: 0x001348E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the pressed state. </returns>
				// Token: 0x1700143E RID: 5182
				// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x001366F8 File Offset: 0x001348F8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
			// Token: 0x0200058A RID: 1418
			public static class UpHorizontal
			{
				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x1700143F RID: 5183
				// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x00136708 File Offset: 0x00134908
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the hot state.</returns>
				// Token: 0x17001440 RID: 5184
				// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x00136718 File Offset: 0x00134918
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001441 RID: 5185
				// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x00136728 File Offset: 0x00134928
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the pressed state. </returns>
				// Token: 0x17001442 RID: 5186
				// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x00136738 File Offset: 0x00134938
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SPIN", 3, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the Start menu. This class cannot be inherited.</summary>
		// Token: 0x0200058B RID: 1419
		public static class StartPanel
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bottom border of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x0200058C RID: 1420
			public static class LogOff
			{
				/// <summary>Gets a visual style element that represents the bottom border of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of the Start menu. </returns>
				// Token: 0x17001443 RID: 5187
				// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x00136748 File Offset: 0x00134948
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 8, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Log Off and Shut Down buttons in the Start menu. This class cannot be inherited. </summary>
			// Token: 0x0200058D RID: 1421
			public static class LogOffButtons
			{
				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the hot state.</returns>
				// Token: 0x17001444 RID: 5188
				// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x00136758 File Offset: 0x00134958
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 9, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the normal state.</returns>
				// Token: 0x17001445 RID: 5189
				// (get) Token: 0x06004ECA RID: 20170 RVA: 0x00136768 File Offset: 0x00134968
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 9, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the pressed state. </returns>
				// Token: 0x17001446 RID: 5190
				// (get) Token: 0x06004ECB RID: 20171 RVA: 0x00136778 File Offset: 0x00134978
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 9, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the All Programs item in the Start menu. This class cannot be inherited. </summary>
			// Token: 0x0200058E RID: 1422
			public static class MorePrograms
			{
				/// <summary>Gets a visual style element that represents the background of the All Programs menu item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the All Programs menu item. </returns>
				// Token: 0x17001447 RID: 5191
				// (get) Token: 0x06004ECC RID: 20172 RVA: 0x00136788 File Offset: 0x00134988
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 2, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the All Programs arrow in the Start menu. This class cannot be inherited.</summary>
			// Token: 0x0200058F RID: 1423
			public static class MoreProgramsArrow
			{
				/// <summary>Gets a visual style element that represents the All Programs arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the hot state.</returns>
				// Token: 0x17001448 RID: 5192
				// (get) Token: 0x06004ECD RID: 20173 RVA: 0x00136798 File Offset: 0x00134998
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the All Programs arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the normal state.</returns>
				// Token: 0x17001449 RID: 5193
				// (get) Token: 0x06004ECE RID: 20174 RVA: 0x001367A8 File Offset: 0x001349A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the All Programs arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the pressed state.</returns>
				// Token: 0x1700144A RID: 5194
				// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001367B8 File Offset: 0x001349B8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 3, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the right side of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000590 RID: 1424
			public static class PlaceList
			{
				/// <summary>Gets a visual style element that represents the background of the right side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the right side of the Start menu. </returns>
				// Token: 0x1700144B RID: 5195
				// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x001367C8 File Offset: 0x001349C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 6, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the right side of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000591 RID: 1425
			public static class PlaceListSeparator
			{
				/// <summary>Gets a visual style element that represents the bar that separates groups of items in the right side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the right side of the Start menu. </returns>
				// Token: 0x1700144C RID: 5196
				// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x001367D8 File Offset: 0x001349D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 7, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the preview area of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000592 RID: 1426
			public static class Preview
			{
				/// <summary>Gets a visual style element that represents the preview area of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the preview area of the Start menu. </returns>
				// Token: 0x1700144D RID: 5197
				// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x001367E8 File Offset: 0x001349E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 11, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the left side of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000593 RID: 1427
			public static class ProgList
			{
				/// <summary>Gets a visual style element that represents the background of the left side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the left side of the Start menu. </returns>
				// Token: 0x1700144E RID: 5198
				// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x001367F8 File Offset: 0x001349F8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 4, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the left side of the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000594 RID: 1428
			public static class ProgListSeparator
			{
				/// <summary>Gets a visual style element that represents the bar that separates groups of items in the left side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the left side of the Start menu.</returns>
				// Token: 0x1700144F RID: 5199
				// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x00136808 File Offset: 0x00134A08
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the top border of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000595 RID: 1429
			public static class UserPane
			{
				/// <summary>Gets a visual style element that represents the top border of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the top border of the Start menu.</returns>
				// Token: 0x17001450 RID: 5200
				// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x00136818 File Offset: 0x00134A18
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 1, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the user picture on the Start menu. This class cannot be inherited. </summary>
			// Token: 0x02000596 RID: 1430
			public static class UserPicture
			{
				/// <summary>Gets a visual style element that represents the background of the user picture on the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the user picture on the Start menu. </returns>
				// Token: 0x17001451 RID: 5201
				// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x00136828 File Offset: 0x00134A28
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STARTPANEL", 10, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the status bar. This class cannot be inherited.</summary>
		// Token: 0x02000597 RID: 1431
		public static class Status
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the status bar. This class cannot be inherited.</summary>
			// Token: 0x02000598 RID: 1432
			public static class Bar
			{
				/// <summary>Gets a visual style element that represents the background of the status bar. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the status bar. </returns>
				// Token: 0x17001452 RID: 5202
				// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x00136838 File Offset: 0x00134A38
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STATUS", 0, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of the status bar. This class cannot be inherited.</summary>
			// Token: 0x02000599 RID: 1433
			public static class Gripper
			{
				/// <summary>Gets a visual style element that represents the status bar grip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the status bar grip. </returns>
				// Token: 0x17001453 RID: 5203
				// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x00136848 File Offset: 0x00134A48
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STATUS", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pane of the grip in the status bar. This class cannot be inherited.</summary>
			// Token: 0x0200059A RID: 1434
			public static class GripperPane
			{
				/// <summary>Gets a visual style element that represents a pane for the status bar grip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pane for the status bar grip. </returns>
				// Token: 0x17001454 RID: 5204
				// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x00136858 File Offset: 0x00134A58
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STATUS", 2, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a status bar pane. This class cannot be inherited.</summary>
			// Token: 0x0200059B RID: 1435
			public static class Pane
			{
				/// <summary>Gets a visual style element that represents a status bar pane.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a status bar pane.</returns>
				// Token: 0x17001455 RID: 5205
				// (get) Token: 0x06004EDA RID: 20186 RVA: 0x00136868 File Offset: 0x00134A68
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("STATUS", 1, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a tab control. This class cannot be inherited.</summary>
		// Token: 0x0200059C RID: 1436
		public static class Tab
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the interior of a tab control page. This class cannot be inherited.</summary>
			// Token: 0x0200059D RID: 1437
			public static class Body
			{
				/// <summary>Gets a visual style element that represents the interior of a tab control page. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the interior of a tab control page. </returns>
				// Token: 0x17001456 RID: 5206
				// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00136878 File Offset: 0x00134A78
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 10, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the border of a tab control page. This class cannot be inherited.</summary>
			// Token: 0x0200059E RID: 1438
			public static class Pane
			{
				/// <summary>Gets a visual style element that represents the border of a tab control page.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the border of a tab control page.</returns>
				// Token: 0x17001457 RID: 5207
				// (get) Token: 0x06004EDC RID: 20188 RVA: 0x00136888 File Offset: 0x00134A88
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 9, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top, left, and right borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x0200059F RID: 1439
			public static class TabItem
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x17001458 RID: 5208
				// (get) Token: 0x06004EDD RID: 20189 RVA: 0x00136898 File Offset: 0x00134A98
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x17001459 RID: 5209
				// (get) Token: 0x06004EDE RID: 20190 RVA: 0x001368A8 File Offset: 0x00134AA8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x1700145A RID: 5210
				// (get) Token: 0x06004EDF RID: 20191 RVA: 0x001368B8 File Offset: 0x00134AB8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top, left, and right borders with other tab controls. </returns>
				// Token: 0x1700145B RID: 5211
				// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x001368C8 File Offset: 0x00134AC8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 1, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its top border with another tab control. This class cannot be inherited. </summary>
			// Token: 0x020005A0 RID: 1440
			public static class TabItemBothEdges
			{
				/// <summary>Gets a visual style element that represents a tab control that shares its top border with another tab control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its top border with another tab control. </returns>
				// Token: 0x1700145C RID: 5212
				// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x001368D8 File Offset: 0x00134AD8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 4, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and right borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x020005A1 RID: 1441
			public static class TabItemLeftEdge
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x1700145D RID: 5213
				// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x001368E8 File Offset: 0x00134AE8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x1700145E RID: 5214
				// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x001368F8 File Offset: 0x00134AF8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x1700145F RID: 5215
				// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x00136908 File Offset: 0x00134B08
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and right borders with other tab controls. </returns>
				// Token: 0x17001460 RID: 5216
				// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x00136918 File Offset: 0x00134B18
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and left borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x020005A2 RID: 1442
			public static class TabItemRightEdge
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x17001461 RID: 5217
				// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x00136928 File Offset: 0x00134B28
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x17001462 RID: 5218
				// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x00136938 File Offset: 0x00134B38
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x17001463 RID: 5219
				// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x00136948 File Offset: 0x00134B48
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and left borders with other tab controls. </returns>
				// Token: 0x17001464 RID: 5220
				// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x00136958 File Offset: 0x00134B58
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom, left, and right borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x020005A3 RID: 1443
			public static class TopTabItem
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x17001465 RID: 5221
				// (get) Token: 0x06004EEA RID: 20202 RVA: 0x00136968 File Offset: 0x00134B68
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 5, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x17001466 RID: 5222
				// (get) Token: 0x06004EEB RID: 20203 RVA: 0x00136978 File Offset: 0x00134B78
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 5, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom, left, and right borders with other tab controls. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x17001467 RID: 5223
				// (get) Token: 0x06004EEC RID: 20204 RVA: 0x00136988 File Offset: 0x00134B88
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x17001468 RID: 5224
				// (get) Token: 0x06004EED RID: 20205 RVA: 0x00136998 File Offset: 0x00134B98
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 5, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its bottom border with another tab control. This class cannot be inherited. </summary>
			// Token: 0x020005A4 RID: 1444
			public static class TopTabItemBothEdges
			{
				/// <summary>Gets a visual style element that represents a tab control that shares its bottom border with another tab control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its bottom border with another tab control. </returns>
				// Token: 0x17001469 RID: 5225
				// (get) Token: 0x06004EEE RID: 20206 RVA: 0x001369A8 File Offset: 0x00134BA8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 8, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and right borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x020005A5 RID: 1445
			public static class TopTabItemLeftEdge
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x1700146A RID: 5226
				// (get) Token: 0x06004EEF RID: 20207 RVA: 0x001369B8 File Offset: 0x00134BB8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 6, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x1700146B RID: 5227
				// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x001369C8 File Offset: 0x00134BC8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 6, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x1700146C RID: 5228
				// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x001369D8 File Offset: 0x00134BD8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and right borders with other tab controls. </returns>
				// Token: 0x1700146D RID: 5229
				// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x001369E8 File Offset: 0x00134BE8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 6, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and left borders with other tab controls. This class cannot be inherited. </summary>
			// Token: 0x020005A6 RID: 1446
			public static class TopTabItemRightEdge
			{
				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x1700146E RID: 5230
				// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x001369F8 File Offset: 0x00134BF8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 7, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x1700146F RID: 5231
				// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x00136A08 File Offset: 0x00134C08
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 7, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x17001470 RID: 5232
				// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00136A18 File Offset: 0x00134C18
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and left borders with other tab controls. </returns>
				// Token: 0x17001471 RID: 5233
				// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x00136A28 File Offset: 0x00134C28
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TAB", 7, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for parts of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x020005A7 RID: 1447
		public static class TaskBand
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing window button in the taskbar. This class cannot be inherited. </summary>
			// Token: 0x020005A8 RID: 1448
			public static class FlashButton
			{
				/// <summary>Gets a visual style element that represents a flashing window button in the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing window button in the taskbar. </returns>
				// Token: 0x17001472 RID: 5234
				// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00136A38 File Offset: 0x00134C38
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAND", 2, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing menu item of a window button in the taskbar. This class cannot be inherited. </summary>
			// Token: 0x020005A9 RID: 1449
			public static class FlashButtonGroupMenu
			{
				/// <summary>Gets a visual style element that represents a flashing menu item for a window button in the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing menu item for a window button in the taskbar.</returns>
				// Token: 0x17001473 RID: 5235
				// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00136A48 File Offset: 0x00134C48
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAND", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a group counter of the taskbar. This class cannot be inherited.  </summary>
			// Token: 0x020005AA RID: 1450
			public static class GroupCount
			{
				/// <summary>Gets a visual style element that represents a group counter for the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group counter for the taskbar. </returns>
				// Token: 0x17001474 RID: 5236
				// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00136A58 File Offset: 0x00134C58
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAND", 1, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x020005AB RID: 1451
		public static class Taskbar
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the bottom of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005AC RID: 1452
			public static class BackgroundBottom
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the bottom of the screen. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the bottom of the screen. </returns>
				// Token: 0x17001475 RID: 5237
				// (get) Token: 0x06004EFA RID: 20218 RVA: 0x00136A68 File Offset: 0x00134C68
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 1, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the left side of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005AD RID: 1453
			public static class BackgroundLeft
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the left side of the screen. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the left side of the screen. </returns>
				// Token: 0x17001476 RID: 5238
				// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00136A78 File Offset: 0x00134C78
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 4, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the right side of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005AE RID: 1454
			public static class BackgroundRight
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the right side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the right side of the screen.</returns>
				// Token: 0x17001477 RID: 5239
				// (get) Token: 0x06004EFC RID: 20220 RVA: 0x00136A88 File Offset: 0x00134C88
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 2, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the top of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005AF RID: 1455
			public static class BackgroundTop
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the top of the screen. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the top of the screen. </returns>
				// Token: 0x17001478 RID: 5240
				// (get) Token: 0x06004EFD RID: 20221 RVA: 0x00136A98 File Offset: 0x00134C98
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 3, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the bottom of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005B0 RID: 1456
			public static class SizingBarBottom
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</returns>
				// Token: 0x17001479 RID: 5241
				// (get) Token: 0x06004EFE RID: 20222 RVA: 0x00136AA8 File Offset: 0x00134CA8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the left side of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005B1 RID: 1457
			public static class SizingBarLeft
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the left side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the left side of the screen.</returns>
				// Token: 0x1700147A RID: 5242
				// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00136AB8 File Offset: 0x00134CB8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 8, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the right side of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005B2 RID: 1458
			public static class SizingBarRight
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the right side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the right side of the screen.</returns>
				// Token: 0x1700147B RID: 5243
				// (get) Token: 0x06004F00 RID: 20224 RVA: 0x00136AC8 File Offset: 0x00134CC8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 6, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the top of the screen. This class cannot be inherited. </summary>
			// Token: 0x020005B3 RID: 1459
			public static class SizingBarTop
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the top of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the top of the screen.</returns>
				// Token: 0x1700147C RID: 5244
				// (get) Token: 0x06004F01 RID: 20225 RVA: 0x00136AD8 File Offset: 0x00134CD8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TASKBAR", 7, 0);
					}
				}
			}
		}

		/// <summary>Contains a class that provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited. </summary>
		// Token: 0x020005B4 RID: 1460
		public static class TaskbarClock
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited.  </summary>
			// Token: 0x020005B5 RID: 1461
			public static class Time
			{
				/// <summary>Gets a visual style element that represents the background of the taskbar clock. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the taskbar clock.</returns>
				// Token: 0x1700147D RID: 5245
				// (get) Token: 0x06004F02 RID: 20226 RVA: 0x00136AE8 File Offset: 0x00134CE8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("CLOCK", 1, 1);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a text box. This class cannot be inherited.</summary>
		// Token: 0x020005B6 RID: 1462
		public static class TextBox
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the caret of a text box. This class cannot be inherited. </summary>
			// Token: 0x020005B7 RID: 1463
			public static class Caret
			{
				/// <summary>Gets a visual style element that represents a text box caret.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the insertion point of a text box. </returns>
				// Token: 0x1700147E RID: 5246
				// (get) Token: 0x06004F03 RID: 20227 RVA: 0x00136AF8 File Offset: 0x00134CF8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 2, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a text box. This class cannot be inherited. </summary>
			// Token: 0x020005B8 RID: 1464
			public static class TextEdit
			{
				/// <summary>Gets a visual style element that represents a text box in assist mode.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box in assist mode.</returns>
				// Token: 0x1700147F RID: 5247
				// (get) Token: 0x06004F04 RID: 20228 RVA: 0x00136B08 File Offset: 0x00134D08
				public static VisualStyleElement Assist
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled text box.</returns>
				// Token: 0x17001480 RID: 5248
				// (get) Token: 0x06004F05 RID: 20229 RVA: 0x00136B18 File Offset: 0x00134D18
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a text box that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box that has focus.</returns>
				// Token: 0x17001481 RID: 5249
				// (get) Token: 0x06004F06 RID: 20230 RVA: 0x00136B28 File Offset: 0x00134D28
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a hot text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot text box.</returns>
				// Token: 0x17001482 RID: 5250
				// (get) Token: 0x06004F07 RID: 20231 RVA: 0x00136B38 File Offset: 0x00134D38
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal text box.</returns>
				// Token: 0x17001483 RID: 5251
				// (get) Token: 0x06004F08 RID: 20232 RVA: 0x00136B48 File Offset: 0x00134D48
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a read-only text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a read-only text box.</returns>
				// Token: 0x17001484 RID: 5252
				// (get) Token: 0x06004F09 RID: 20233 RVA: 0x00136B58 File Offset: 0x00134D58
				public static VisualStyleElement ReadOnly
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a selected text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected text box.</returns>
				// Token: 0x17001485 RID: 5253
				// (get) Token: 0x06004F0A RID: 20234 RVA: 0x00136B68 File Offset: 0x00134D68
				public static VisualStyleElement Selected
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a toolbar. This class cannot be inherited.</summary>
		// Token: 0x020005B9 RID: 1465
		public static class ToolBar
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a toolbar button. This class cannot be inherited. </summary>
			// Token: 0x020005BA RID: 1466
			public static class Button
			{
				/// <summary>Gets a visual style element that represents a toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the checked state.</returns>
				// Token: 0x17001486 RID: 5254
				// (get) Token: 0x06004F0B RID: 20235 RVA: 0x00136B78 File Offset: 0x00134D78
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the disabled state.</returns>
				// Token: 0x17001487 RID: 5255
				// (get) Token: 0x06004F0C RID: 20236 RVA: 0x00136B88 File Offset: 0x00134D88
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot state.</returns>
				// Token: 0x17001488 RID: 5256
				// (get) Token: 0x06004F0D RID: 20237 RVA: 0x00136B98 File Offset: 0x00134D98
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot and checked states.</returns>
				// Token: 0x17001489 RID: 5257
				// (get) Token: 0x06004F0E RID: 20238 RVA: 0x00136BA8 File Offset: 0x00134DA8
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the normal state.</returns>
				// Token: 0x1700148A RID: 5258
				// (get) Token: 0x06004F0F RID: 20239 RVA: 0x00136BB8 File Offset: 0x00134DB8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the pressed state.</returns>
				// Token: 0x1700148B RID: 5259
				// (get) Token: 0x06004F10 RID: 20240 RVA: 0x00136BC8 File Offset: 0x00134DC8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a drop-down toolbar button. This class cannot be inherited. </summary>
			// Token: 0x020005BB RID: 1467
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the checked state.</returns>
				// Token: 0x1700148C RID: 5260
				// (get) Token: 0x06004F11 RID: 20241 RVA: 0x00136BD8 File Offset: 0x00134DD8
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the disabled state.</returns>
				// Token: 0x1700148D RID: 5261
				// (get) Token: 0x06004F12 RID: 20242 RVA: 0x00136BE8 File Offset: 0x00134DE8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot state.</returns>
				// Token: 0x1700148E RID: 5262
				// (get) Token: 0x06004F13 RID: 20243 RVA: 0x00136BF8 File Offset: 0x00134DF8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot and checked states.</returns>
				// Token: 0x1700148F RID: 5263
				// (get) Token: 0x06004F14 RID: 20244 RVA: 0x00136C08 File Offset: 0x00134E08
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the normal state.</returns>
				// Token: 0x17001490 RID: 5264
				// (get) Token: 0x06004F15 RID: 20245 RVA: 0x00136C18 File Offset: 0x00134E18
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the pressed state.</returns>
				// Token: 0x17001491 RID: 5265
				// (get) Token: 0x06004F16 RID: 20246 RVA: 0x00136C28 File Offset: 0x00134E28
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a horizontal separator of the toolbar. This class cannot be inherited. </summary>
			// Token: 0x020005BC RID: 1468
			public static class SeparatorHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal separator of the toolbar.</returns>
				// Token: 0x17001492 RID: 5266
				// (get) Token: 0x06004F17 RID: 20247 RVA: 0x00136C38 File Offset: 0x00134E38
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a vertical separator of the toolbar. This class cannot be inherited. </summary>
			// Token: 0x020005BD RID: 1469
			public static class SeparatorVertical
			{
				/// <summary>Gets a visual style element that represents a vertical separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical separator of the toolbar.</returns>
				// Token: 0x17001493 RID: 5267
				// (get) Token: 0x06004F18 RID: 20248 RVA: 0x00136C48 File Offset: 0x00134E48
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 6, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the regular button portion of a combined regular button and drop-down button. This class cannot be inherited.</summary>
			// Token: 0x020005BE RID: 1470
			public static class SplitButton
			{
				/// <summary>Gets a visual style element that represents a split button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the checked state.</returns>
				// Token: 0x17001494 RID: 5268
				// (get) Token: 0x06004F19 RID: 20249 RVA: 0x00136C58 File Offset: 0x00134E58
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the disabled state.</returns>
				// Token: 0x17001495 RID: 5269
				// (get) Token: 0x06004F1A RID: 20250 RVA: 0x00136C68 File Offset: 0x00134E68
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot state.</returns>
				// Token: 0x17001496 RID: 5270
				// (get) Token: 0x06004F1B RID: 20251 RVA: 0x00136C78 File Offset: 0x00134E78
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot and checked states.</returns>
				// Token: 0x17001497 RID: 5271
				// (get) Token: 0x06004F1C RID: 20252 RVA: 0x00136C88 File Offset: 0x00134E88
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the normal state.</returns>
				// Token: 0x17001498 RID: 5272
				// (get) Token: 0x06004F1D RID: 20253 RVA: 0x00136C98 File Offset: 0x00134E98
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the pressed state. </returns>
				// Token: 0x17001499 RID: 5273
				// (get) Token: 0x06004F1E RID: 20254 RVA: 0x00136CA8 File Offset: 0x00134EA8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the drop-down portion of a combined regular button and drop-down button. This class cannot be inherited. </summary>
			// Token: 0x020005BF RID: 1471
			public static class SplitButtonDropDown
			{
				/// <summary>Gets a visual style element that represents a split drop-down button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the checked state.</returns>
				// Token: 0x1700149A RID: 5274
				// (get) Token: 0x06004F1F RID: 20255 RVA: 0x00136CB8 File Offset: 0x00134EB8
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the disabled state.</returns>
				// Token: 0x1700149B RID: 5275
				// (get) Token: 0x06004F20 RID: 20256 RVA: 0x00136CC8 File Offset: 0x00134EC8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot state.</returns>
				// Token: 0x1700149C RID: 5276
				// (get) Token: 0x06004F21 RID: 20257 RVA: 0x00136CD8 File Offset: 0x00134ED8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot and checked states.</returns>
				// Token: 0x1700149D RID: 5277
				// (get) Token: 0x06004F22 RID: 20258 RVA: 0x00136CE8 File Offset: 0x00134EE8
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the normal state.</returns>
				// Token: 0x1700149E RID: 5278
				// (get) Token: 0x06004F23 RID: 20259 RVA: 0x00136CF8 File Offset: 0x00134EF8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the pressed state.</returns>
				// Token: 0x1700149F RID: 5279
				// (get) Token: 0x06004F24 RID: 20260 RVA: 0x00136D08 File Offset: 0x00134F08
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a ToolTip. This class cannot be inherited.</summary>
		// Token: 0x020005C0 RID: 1472
		public static class ToolTip
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a balloon ToolTip. This class cannot be inherited. </summary>
			// Token: 0x020005C1 RID: 1473
			public static class Balloon
			{
				/// <summary>Gets a visual style element that represents a balloon ToolTip that contains a link.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains a link.</returns>
				// Token: 0x170014A0 RID: 5280
				// (get) Token: 0x06004F25 RID: 20261 RVA: 0x00136D18 File Offset: 0x00134F18
				public static VisualStyleElement Link
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a balloon ToolTip that contains text.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains text.</returns>
				// Token: 0x170014A1 RID: 5281
				// (get) Token: 0x06004F26 RID: 20262 RVA: 0x00136D28 File Offset: 0x00134F28
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 3, 1);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a balloon ToolTip. This class cannot be inherited. </summary>
			// Token: 0x020005C2 RID: 1474
			public static class BalloonTitle
			{
				/// <summary>Gets a visual style element that represents the title area of a balloon ToolTip. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a balloon ToolTip. </returns>
				// Token: 0x170014A2 RID: 5282
				// (get) Token: 0x06004F27 RID: 20263 RVA: 0x00136D38 File Offset: 0x00134F38
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 4, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a ToolTip. This class cannot be inherited. </summary>
			// Token: 0x020005C3 RID: 1475
			public static class Close
			{
				/// <summary>Gets a visual style element that represents the ToolTip Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the hot state.</returns>
				// Token: 0x170014A3 RID: 5283
				// (get) Token: 0x06004F28 RID: 20264 RVA: 0x00136D48 File Offset: 0x00134F48
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 5, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the ToolTip Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the normal state.</returns>
				// Token: 0x170014A4 RID: 5284
				// (get) Token: 0x06004F29 RID: 20265 RVA: 0x00136D58 File Offset: 0x00134F58
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the ToolTip Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the pressed state. </returns>
				// Token: 0x170014A5 RID: 5285
				// (get) Token: 0x06004F2A RID: 20266 RVA: 0x00136D68 File Offset: 0x00134F68
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 5, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a standard ToolTip. This class cannot be inherited. </summary>
			// Token: 0x020005C4 RID: 1476
			public static class Standard
			{
				/// <summary>Gets a visual style element that represents a standard ToolTip that contains a link.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains a link.</returns>
				// Token: 0x170014A6 RID: 5286
				// (get) Token: 0x06004F2B RID: 20267 RVA: 0x00136D78 File Offset: 0x00134F78
				public static VisualStyleElement Link
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a standard ToolTip that contains text.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains text.</returns>
				// Token: 0x170014A7 RID: 5287
				// (get) Token: 0x06004F2C RID: 20268 RVA: 0x00136D88 File Offset: 0x00134F88
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 1, 1);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a standard ToolTip. This class cannot be inherited. </summary>
			// Token: 0x020005C5 RID: 1477
			public static class StandardTitle
			{
				/// <summary>Gets a visual style element that represents the title area of a standard ToolTip. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a standard ToolTip. </returns>
				// Token: 0x170014A8 RID: 5288
				// (get) Token: 0x06004F2D RID: 20269 RVA: 0x00136D98 File Offset: 0x00134F98
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 2, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the track bar control. This class cannot be inherited.</summary>
		// Token: 0x020005C6 RID: 1478
		public static class TrackBar
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a horizontal track bar. This class cannot be inherited. </summary>
			// Token: 0x020005C7 RID: 1479
			public static class Thumb
			{
				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the disabled state.</returns>
				// Token: 0x170014A9 RID: 5289
				// (get) Token: 0x06004F2E RID: 20270 RVA: 0x00136DA8 File Offset: 0x00134FA8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar that has focus.</returns>
				// Token: 0x170014AA RID: 5290
				// (get) Token: 0x06004F2F RID: 20271 RVA: 0x00136DB8 File Offset: 0x00134FB8
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the hot state.</returns>
				// Token: 0x170014AB RID: 5291
				// (get) Token: 0x06004F30 RID: 20272 RVA: 0x00136DC8 File Offset: 0x00134FC8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the normal state.</returns>
				// Token: 0x170014AC RID: 5292
				// (get) Token: 0x06004F31 RID: 20273 RVA: 0x00136DD8 File Offset: 0x00134FD8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the pressed state.</returns>
				// Token: 0x170014AD RID: 5293
				// (get) Token: 0x06004F32 RID: 20274 RVA: 0x00136DE8 File Offset: 0x00134FE8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x020005C8 RID: 1480
			public static class ThumbBottom
			{
				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the disabled state.</returns>
				// Token: 0x170014AE RID: 5294
				// (get) Token: 0x06004F33 RID: 20275 RVA: 0x00136DF8 File Offset: 0x00134FF8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 4, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider that has focus.</returns>
				// Token: 0x170014AF RID: 5295
				// (get) Token: 0x06004F34 RID: 20276 RVA: 0x00136E08 File Offset: 0x00135008
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the hot state.</returns>
				// Token: 0x170014B0 RID: 5296
				// (get) Token: 0x06004F35 RID: 20277 RVA: 0x00136E18 File Offset: 0x00135018
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the normal state.</returns>
				// Token: 0x170014B1 RID: 5297
				// (get) Token: 0x06004F36 RID: 20278 RVA: 0x00136E28 File Offset: 0x00135028
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the pressed state.</returns>
				// Token: 0x170014B2 RID: 5298
				// (get) Token: 0x06004F37 RID: 20279 RVA: 0x00136E38 File Offset: 0x00135038
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x020005C9 RID: 1481
			public static class ThumbLeft
			{
				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the disabled state.</returns>
				// Token: 0x170014B3 RID: 5299
				// (get) Token: 0x06004F38 RID: 20280 RVA: 0x00136E48 File Offset: 0x00135048
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 7, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider that has focus.</returns>
				// Token: 0x170014B4 RID: 5300
				// (get) Token: 0x06004F39 RID: 20281 RVA: 0x00136E58 File Offset: 0x00135058
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 7, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the hot state.</returns>
				// Token: 0x170014B5 RID: 5301
				// (get) Token: 0x06004F3A RID: 20282 RVA: 0x00136E68 File Offset: 0x00135068
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 7, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the normal state.</returns>
				// Token: 0x170014B6 RID: 5302
				// (get) Token: 0x06004F3B RID: 20283 RVA: 0x00136E78 File Offset: 0x00135078
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the pressed state. </returns>
				// Token: 0x170014B7 RID: 5303
				// (get) Token: 0x06004F3C RID: 20284 RVA: 0x00136E88 File Offset: 0x00135088
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 7, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x020005CA RID: 1482
			public static class ThumbRight
			{
				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the disabled state.</returns>
				// Token: 0x170014B8 RID: 5304
				// (get) Token: 0x06004F3D RID: 20285 RVA: 0x00136E98 File Offset: 0x00135098
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 8, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider that has focus.</returns>
				// Token: 0x170014B9 RID: 5305
				// (get) Token: 0x06004F3E RID: 20286 RVA: 0x00136EA8 File Offset: 0x001350A8
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 8, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the hot state.</returns>
				// Token: 0x170014BA RID: 5306
				// (get) Token: 0x06004F3F RID: 20287 RVA: 0x00136EB8 File Offset: 0x001350B8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 8, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the normal state.</returns>
				// Token: 0x170014BB RID: 5307
				// (get) Token: 0x06004F40 RID: 20288 RVA: 0x00136EC8 File Offset: 0x001350C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 8, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the pressed state.</returns>
				// Token: 0x170014BC RID: 5308
				// (get) Token: 0x06004F41 RID: 20289 RVA: 0x00136ED8 File Offset: 0x001350D8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 8, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x020005CB RID: 1483
			public static class ThumbTop
			{
				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the disabled state.</returns>
				// Token: 0x170014BD RID: 5309
				// (get) Token: 0x06004F42 RID: 20290 RVA: 0x00136EE8 File Offset: 0x001350E8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 5, 5);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider that has focus.</returns>
				// Token: 0x170014BE RID: 5310
				// (get) Token: 0x06004F43 RID: 20291 RVA: 0x00136EF8 File Offset: 0x001350F8
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 5, 4);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the hot state.</returns>
				// Token: 0x170014BF RID: 5311
				// (get) Token: 0x06004F44 RID: 20292 RVA: 0x00136F08 File Offset: 0x00135108
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 5, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the normal state.</returns>
				// Token: 0x170014C0 RID: 5312
				// (get) Token: 0x06004F45 RID: 20293 RVA: 0x00136F18 File Offset: 0x00135118
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the pressed state.</returns>
				// Token: 0x170014C1 RID: 5313
				// (get) Token: 0x06004F46 RID: 20294 RVA: 0x00136F28 File Offset: 0x00135128
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 5, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a vertical track bar. This class cannot be inherited. </summary>
			// Token: 0x020005CC RID: 1484
			public static class ThumbVertical
			{
				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the disabled state.</returns>
				// Token: 0x170014C2 RID: 5314
				// (get) Token: 0x06004F47 RID: 20295 RVA: 0x00136F38 File Offset: 0x00135138
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 6, 5);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar that has focus. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar that has focus.</returns>
				// Token: 0x170014C3 RID: 5315
				// (get) Token: 0x06004F48 RID: 20296 RVA: 0x00136F48 File Offset: 0x00135148
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 6, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the hot state.</returns>
				// Token: 0x170014C4 RID: 5316
				// (get) Token: 0x06004F49 RID: 20297 RVA: 0x00136F58 File Offset: 0x00135158
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 6, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the normal state.</returns>
				// Token: 0x170014C5 RID: 5317
				// (get) Token: 0x06004F4A RID: 20298 RVA: 0x00136F68 File Offset: 0x00135168
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the pressed state. </returns>
				// Token: 0x170014C6 RID: 5318
				// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00136F78 File Offset: 0x00135178
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 6, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a horizontal track bar. This class cannot be inherited. </summary>
			// Token: 0x020005CD RID: 1485
			public static class Ticks
			{
				/// <summary>Gets a visual style element that represents a single tick of a horizontal track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a horizontal track bar.</returns>
				// Token: 0x170014C7 RID: 5319
				// (get) Token: 0x06004F4C RID: 20300 RVA: 0x00136F88 File Offset: 0x00135188
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 9, 1);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a vertical track bar. This class cannot be inherited. </summary>
			// Token: 0x020005CE RID: 1486
			public static class TicksVertical
			{
				/// <summary>Gets a visual style element that represents a single tick of a vertical track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a vertical track bar.</returns>
				// Token: 0x170014C8 RID: 5320
				// (get) Token: 0x06004F4D RID: 20301 RVA: 0x00136F98 File Offset: 0x00135198
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 10, 1);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a horizontal track bar. This class cannot be inherited. </summary>
			// Token: 0x020005CF RID: 1487
			public static class Track
			{
				/// <summary>Gets a visual style element that represents the track for a horizontal track bar. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a horizontal track bar. </returns>
				// Token: 0x170014C9 RID: 5321
				// (get) Token: 0x06004F4E RID: 20302 RVA: 0x00136FA8 File Offset: 0x001351A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 1, 1);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a vertical track bar. This class cannot be inherited. </summary>
			// Token: 0x020005D0 RID: 1488
			public static class TrackVertical
			{
				/// <summary>Gets a visual style element that represents the track for a vertical track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a vertical track bar.</returns>
				// Token: 0x170014CA RID: 5322
				// (get) Token: 0x06004F4F RID: 20303 RVA: 0x00136FB8 File Offset: 0x001351B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRACKBAR", 2, 1);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the background of the notification area, which is located at the far right of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x020005D1 RID: 1489
		public static class TrayNotify
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for an animated background of the notification area. This class cannot be inherited. </summary>
			// Token: 0x020005D2 RID: 1490
			public static class AnimateBackground
			{
				/// <summary>Gets a visual style element that represents an animated background of the notification area.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an animated background of the notification area. </returns>
				// Token: 0x170014CB RID: 5323
				// (get) Token: 0x06004F50 RID: 20304 RVA: 0x00136FC8 File Offset: 0x001351C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRAYNOTIFY", 2, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the notification area. This class cannot be inherited. </summary>
			// Token: 0x020005D3 RID: 1491
			public static class Background
			{
				/// <summary>Gets a visual style element that represents the background of the notification area.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the notification area. </returns>
				// Token: 0x170014CC RID: 5324
				// (get) Token: 0x06004F51 RID: 20305 RVA: 0x00136FD8 File Offset: 0x001351D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TRAYNOTIFY", 1, 0);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the tree view control. This class cannot be inherited.  </summary>
		// Token: 0x020005D4 RID: 1492
		public static class TreeView
		{
			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tree view branch. This class cannot be inherited. </summary>
			// Token: 0x020005D5 RID: 1493
			public static class Branch
			{
				/// <summary>Gets a visual style element that represents a tree view branch. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view branch.</returns>
				// Token: 0x170014CD RID: 5325
				// (get) Token: 0x06004F52 RID: 20306 RVA: 0x00136FE8 File Offset: 0x001351E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 3, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the plus sign (+) and minus sign (-) buttons of a tree view control. This class cannot be inherited. </summary>
			// Token: 0x020005D6 RID: 1494
			public static class Glyph
			{
				/// <summary>Gets a visual style element that represents a minus sign (-) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a minus sign button of a tree view node.</returns>
				// Token: 0x170014CE RID: 5326
				// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00136FF8 File Offset: 0x001351F8
				public static VisualStyleElement Closed
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a plus sign (+) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a plus sign button of a tree view node.</returns>
				// Token: 0x170014CF RID: 5327
				// (get) Token: 0x06004F54 RID: 20308 RVA: 0x00137008 File Offset: 0x00135208
				public static VisualStyleElement Opened
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 2, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tree view item. This class cannot be inherited. </summary>
			// Token: 0x020005D7 RID: 1495
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a tree view item in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the disabled state.</returns>
				// Token: 0x170014D0 RID: 5328
				// (get) Token: 0x06004F55 RID: 20309 RVA: 0x00137018 File Offset: 0x00135218
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the hot state.</returns>
				// Token: 0x170014D1 RID: 5329
				// (get) Token: 0x06004F56 RID: 20310 RVA: 0x00137028 File Offset: 0x00135228
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the normal state.</returns>
				// Token: 0x170014D2 RID: 5330
				// (get) Token: 0x06004F57 RID: 20311 RVA: 0x00137038 File Offset: 0x00135238
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item that is in the selected state and has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state and has focus.</returns>
				// Token: 0x170014D3 RID: 5331
				// (get) Token: 0x06004F58 RID: 20312 RVA: 0x00137048 File Offset: 0x00135248
				public static VisualStyleElement Selected
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 1, 3);
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item that is in the selected state but does not have focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state but does not have focus.</returns>
				// Token: 0x170014D4 RID: 5332
				// (get) Token: 0x06004F59 RID: 20313 RVA: 0x00137058 File Offset: 0x00135258
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 1, 5);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a window. This class cannot be inherited.</summary>
		// Token: 0x020005D8 RID: 1496
		public static class Window
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a window. This class cannot be inherited. </summary>
			// Token: 0x020005D9 RID: 1497
			public static class Caption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active window.</returns>
				// Token: 0x170014D5 RID: 5333
				// (get) Token: 0x06004F5A RID: 20314 RVA: 0x00137068 File Offset: 0x00135268
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled window.</returns>
				// Token: 0x170014D6 RID: 5334
				// (get) Token: 0x06004F5B RID: 20315 RVA: 0x00137078 File Offset: 0x00135278
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive window.</returns>
				// Token: 0x170014D7 RID: 5335
				// (get) Token: 0x06004F5C RID: 20316 RVA: 0x00137088 File Offset: 0x00135288
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window. This class cannot be inherited. </summary>
			// Token: 0x020005DA RID: 1498
			public static class CaptionSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the title bar of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window. </returns>
				// Token: 0x170014D8 RID: 5336
				// (get) Token: 0x06004F5D RID: 20317 RVA: 0x00137098 File Offset: 0x00135298
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 30, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a window. This class cannot be inherited. </summary>
			// Token: 0x020005DB RID: 1499
			public static class CloseButton
			{
				/// <summary>Gets a visual style element that represents a Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the disabled state.</returns>
				// Token: 0x170014D9 RID: 5337
				// (get) Token: 0x06004F5E RID: 20318 RVA: 0x001370A8 File Offset: 0x001352A8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
				// Token: 0x170014DA RID: 5338
				// (get) Token: 0x06004F5F RID: 20319 RVA: 0x001370B8 File Offset: 0x001352B8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
				// Token: 0x170014DB RID: 5339
				// (get) Token: 0x06004F60 RID: 20320 RVA: 0x001370C8 File Offset: 0x001352C8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state.</returns>
				// Token: 0x170014DC RID: 5340
				// (get) Token: 0x06004F61 RID: 20321 RVA: 0x001370D8 File Offset: 0x001352D8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a dialog box. This class cannot be inherited. </summary>
			// Token: 0x020005DC RID: 1500
			public static class Dialog
			{
				/// <summary>Gets a visual style element that represents the background of a dialog box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a dialog box.</returns>
				// Token: 0x170014DD RID: 5341
				// (get) Token: 0x06004F62 RID: 20322 RVA: 0x001370E8 File Offset: 0x001352E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 29, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005DD RID: 1501
			public static class FrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active window.</returns>
				// Token: 0x170014DE RID: 5342
				// (get) Token: 0x06004F63 RID: 20323 RVA: 0x001370F8 File Offset: 0x001352F8
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 9, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive window.</returns>
				// Token: 0x170014DF RID: 5343
				// (get) Token: 0x06004F64 RID: 20324 RVA: 0x00137108 File Offset: 0x00135308
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 9, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005DE RID: 1502
			public static class FrameBottomSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window.</returns>
				// Token: 0x170014E0 RID: 5344
				// (get) Token: 0x06004F65 RID: 20325 RVA: 0x00137118 File Offset: 0x00135318
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 36, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005DF RID: 1503
			public static class FrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active window.</returns>
				// Token: 0x170014E1 RID: 5345
				// (get) Token: 0x06004F66 RID: 20326 RVA: 0x00137128 File Offset: 0x00135328
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive window.</returns>
				// Token: 0x170014E2 RID: 5346
				// (get) Token: 0x06004F67 RID: 20327 RVA: 0x00137138 File Offset: 0x00135338
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 7, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E0 RID: 1504
			public static class FrameLeftSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the left border of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window.</returns>
				// Token: 0x170014E3 RID: 5347
				// (get) Token: 0x06004F68 RID: 20328 RVA: 0x00137148 File Offset: 0x00135348
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 32, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E1 RID: 1505
			public static class FrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active window.</returns>
				// Token: 0x170014E4 RID: 5348
				// (get) Token: 0x06004F69 RID: 20329 RVA: 0x00137158 File Offset: 0x00135358
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 8, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive window.</returns>
				// Token: 0x170014E5 RID: 5349
				// (get) Token: 0x06004F6A RID: 20330 RVA: 0x00137168 File Offset: 0x00135368
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 8, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E2 RID: 1506
			public static class FrameRightSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the right border of a window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window. </returns>
				// Token: 0x170014E6 RID: 5350
				// (get) Token: 0x06004F6B RID: 20331 RVA: 0x00137178 File Offset: 0x00135378
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 34, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a window or dialog box. This class cannot be inherited. </summary>
			// Token: 0x020005E3 RID: 1507
			public static class HelpButton
			{
				/// <summary>Gets a visual style element that represents a Help button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the disabled state.</returns>
				// Token: 0x170014E7 RID: 5351
				// (get) Token: 0x06004F6C RID: 20332 RVA: 0x00137188 File Offset: 0x00135388
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the hot state.</returns>
				// Token: 0x170014E8 RID: 5352
				// (get) Token: 0x06004F6D RID: 20333 RVA: 0x00137198 File Offset: 0x00135398
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the normal state.</returns>
				// Token: 0x170014E9 RID: 5353
				// (get) Token: 0x06004F6E RID: 20334 RVA: 0x001371A8 File Offset: 0x001353A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the pressed state.</returns>
				// Token: 0x170014EA RID: 5354
				// (get) Token: 0x06004F6F RID: 20335 RVA: 0x001371B8 File Offset: 0x001353B8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll bar of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E4 RID: 1508
			public static class HorizontalScroll
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the disabled state.</returns>
				// Token: 0x170014EB RID: 5355
				// (get) Token: 0x06004F70 RID: 20336 RVA: 0x001371C8 File Offset: 0x001353C8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 25, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the hot state.</returns>
				// Token: 0x170014EC RID: 5356
				// (get) Token: 0x06004F71 RID: 20337 RVA: 0x001371D8 File Offset: 0x001353D8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 25, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the normal state.</returns>
				// Token: 0x170014ED RID: 5357
				// (get) Token: 0x06004F72 RID: 20338 RVA: 0x001371E8 File Offset: 0x001353E8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 25, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the pressed state.</returns>
				// Token: 0x170014EE RID: 5358
				// (get) Token: 0x06004F73 RID: 20339 RVA: 0x001371F8 File Offset: 0x001353F8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 25, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll box (also known as the thumb) of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E5 RID: 1509
			public static class HorizontalThumb
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
				// Token: 0x170014EF RID: 5359
				// (get) Token: 0x06004F74 RID: 20340 RVA: 0x00137208 File Offset: 0x00135408
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 26, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
				// Token: 0x170014F0 RID: 5360
				// (get) Token: 0x06004F75 RID: 20341 RVA: 0x00137218 File Offset: 0x00135418
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 26, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
				// Token: 0x170014F1 RID: 5361
				// (get) Token: 0x06004F76 RID: 20342 RVA: 0x00137228 File Offset: 0x00135428
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 26, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
				// Token: 0x170014F2 RID: 5362
				// (get) Token: 0x06004F77 RID: 20343 RVA: 0x00137238 File Offset: 0x00135438
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 26, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Maximize button of a window. This class cannot be inherited. </summary>
			// Token: 0x020005E6 RID: 1510
			public static class MaxButton
			{
				/// <summary>Gets a visual style element that represents a Maximize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the disabled state.</returns>
				// Token: 0x170014F3 RID: 5363
				// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00137248 File Offset: 0x00135448
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the hot state.</returns>
				// Token: 0x170014F4 RID: 5364
				// (get) Token: 0x06004F79 RID: 20345 RVA: 0x00137258 File Offset: 0x00135458
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the normal state.</returns>
				// Token: 0x170014F5 RID: 5365
				// (get) Token: 0x06004F7A RID: 20346 RVA: 0x00137268 File Offset: 0x00135468
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the pressed state.</returns>
				// Token: 0x170014F6 RID: 5366
				// (get) Token: 0x06004F7B RID: 20347 RVA: 0x00137278 File Offset: 0x00135478
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized window. This class cannot be inherited. </summary>
			// Token: 0x020005E7 RID: 1511
			public static class MaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a maximized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized active window.</returns>
				// Token: 0x170014F7 RID: 5367
				// (get) Token: 0x06004F7C RID: 20348 RVA: 0x00137288 File Offset: 0x00135488
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized disabled window.</returns>
				// Token: 0x170014F8 RID: 5368
				// (get) Token: 0x06004F7D RID: 20349 RVA: 0x00137298 File Offset: 0x00135498
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized inactive window. </returns>
				// Token: 0x170014F9 RID: 5369
				// (get) Token: 0x06004F7E RID: 20350 RVA: 0x001372A8 File Offset: 0x001354A8
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x020005E8 RID: 1512
			public static class MdiCloseButton
			{
				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the disabled state.</returns>
				// Token: 0x170014FA RID: 5370
				// (get) Token: 0x06004F7F RID: 20351 RVA: 0x001372B8 File Offset: 0x001354B8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the hot state.</returns>
				// Token: 0x170014FB RID: 5371
				// (get) Token: 0x06004F80 RID: 20352 RVA: 0x001372C8 File Offset: 0x001354C8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the normal state.</returns>
				// Token: 0x170014FC RID: 5372
				// (get) Token: 0x06004F81 RID: 20353 RVA: 0x001372D8 File Offset: 0x001354D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the pressed state.</returns>
				// Token: 0x170014FD RID: 5373
				// (get) Token: 0x06004F82 RID: 20354 RVA: 0x001372E8 File Offset: 0x001354E8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x020005E9 RID: 1513
			public static class MdiHelpButton
			{
				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the disabled state.</returns>
				// Token: 0x170014FE RID: 5374
				// (get) Token: 0x06004F83 RID: 20355 RVA: 0x001372F8 File Offset: 0x001354F8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the hot state.</returns>
				// Token: 0x170014FF RID: 5375
				// (get) Token: 0x06004F84 RID: 20356 RVA: 0x00137308 File Offset: 0x00135508
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the normal state.</returns>
				// Token: 0x17001500 RID: 5376
				// (get) Token: 0x06004F85 RID: 20357 RVA: 0x00137318 File Offset: 0x00135518
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001501 RID: 5377
				// (get) Token: 0x06004F86 RID: 20358 RVA: 0x00137328 File Offset: 0x00135528
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x020005EA RID: 1514
			public static class MdiMinButton
			{
				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001502 RID: 5378
				// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00137338 File Offset: 0x00135538
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the hot state.</returns>
				// Token: 0x17001503 RID: 5379
				// (get) Token: 0x06004F88 RID: 20360 RVA: 0x00137348 File Offset: 0x00135548
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the normal state.</returns>
				// Token: 0x17001504 RID: 5380
				// (get) Token: 0x06004F89 RID: 20361 RVA: 0x00137358 File Offset: 0x00135558
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001505 RID: 5381
				// (get) Token: 0x06004F8A RID: 20362 RVA: 0x00137368 File Offset: 0x00135568
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x020005EB RID: 1515
			public static class MdiRestoreButton
			{
				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001506 RID: 5382
				// (get) Token: 0x06004F8B RID: 20363 RVA: 0x00137378 File Offset: 0x00135578
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the hot state.</returns>
				// Token: 0x17001507 RID: 5383
				// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00137388 File Offset: 0x00135588
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the normal state.</returns>
				// Token: 0x17001508 RID: 5384
				// (get) Token: 0x06004F8D RID: 20365 RVA: 0x00137398 File Offset: 0x00135598
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001509 RID: 5385
				// (get) Token: 0x06004F8E RID: 20366 RVA: 0x001373A8 File Offset: 0x001355A8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a multiple-document interface (MDI) child window with visual styles. This class cannot be inherited. </summary>
			// Token: 0x020005EC RID: 1516
			public static class MdiSysButton
			{
				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the disabled state.</returns>
				// Token: 0x1700150A RID: 5386
				// (get) Token: 0x06004F8F RID: 20367 RVA: 0x001373B8 File Offset: 0x001355B8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 14, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the hot state.</returns>
				// Token: 0x1700150B RID: 5387
				// (get) Token: 0x06004F90 RID: 20368 RVA: 0x001373C8 File Offset: 0x001355C8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 14, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the normal state.</returns>
				// Token: 0x1700150C RID: 5388
				// (get) Token: 0x06004F91 RID: 20369 RVA: 0x001373D8 File Offset: 0x001355D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 14, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the pressed state.</returns>
				// Token: 0x1700150D RID: 5389
				// (get) Token: 0x06004F92 RID: 20370 RVA: 0x001373E8 File Offset: 0x001355E8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 14, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a window. This class cannot be inherited. </summary>
			// Token: 0x020005ED RID: 1517
			public static class MinButton
			{
				/// <summary>Gets a visual style element that represents a Minimize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the disabled state.</returns>
				// Token: 0x1700150E RID: 5390
				// (get) Token: 0x06004F93 RID: 20371 RVA: 0x001373F8 File Offset: 0x001355F8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the hot state.</returns>
				// Token: 0x1700150F RID: 5391
				// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00137408 File Offset: 0x00135608
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the normal state.</returns>
				// Token: 0x17001510 RID: 5392
				// (get) Token: 0x06004F95 RID: 20373 RVA: 0x00137418 File Offset: 0x00135618
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the pressed state.</returns>
				// Token: 0x17001511 RID: 5393
				// (get) Token: 0x06004F96 RID: 20374 RVA: 0x00137428 File Offset: 0x00135628
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized window. This class cannot be inherited. </summary>
			// Token: 0x020005EE RID: 1518
			public static class MinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a minimized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized active window.</returns>
				// Token: 0x17001512 RID: 5394
				// (get) Token: 0x06004F97 RID: 20375 RVA: 0x00137438 File Offset: 0x00135638
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized disabled window.</returns>
				// Token: 0x17001513 RID: 5395
				// (get) Token: 0x06004F98 RID: 20376 RVA: 0x00137448 File Offset: 0x00135648
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized inactive window.</returns>
				// Token: 0x17001514 RID: 5396
				// (get) Token: 0x06004F99 RID: 20377 RVA: 0x00137458 File Offset: 0x00135658
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a window. This class cannot be inherited. </summary>
			// Token: 0x020005EF RID: 1519
			public static class RestoreButton
			{
				/// <summary>Gets a visual style element that represents a Restore button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the disabled state.</returns>
				// Token: 0x17001515 RID: 5397
				// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00137468 File Offset: 0x00135668
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the hot state.</returns>
				// Token: 0x17001516 RID: 5398
				// (get) Token: 0x06004F9B RID: 20379 RVA: 0x00137478 File Offset: 0x00135678
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the normal state. </returns>
				// Token: 0x17001517 RID: 5399
				// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00137488 File Offset: 0x00135688
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the pressed state. </returns>
				// Token: 0x17001518 RID: 5400
				// (get) Token: 0x06004F9D RID: 20381 RVA: 0x00137498 File Offset: 0x00135698
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F0 RID: 1520
			public static class SmallCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window.</returns>
				// Token: 0x17001519 RID: 5401
				// (get) Token: 0x06004F9E RID: 20382 RVA: 0x001374A8 File Offset: 0x001356A8
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window.</returns>
				// Token: 0x1700151A RID: 5402
				// (get) Token: 0x06004F9F RID: 20383 RVA: 0x001374B8 File Offset: 0x001356B8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window.</returns>
				// Token: 0x1700151B RID: 5403
				// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x001374C8 File Offset: 0x001356C8
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F1 RID: 1521
			public static class SmallCaptionSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the title bar of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window.</returns>
				// Token: 0x1700151C RID: 5404
				// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x001374D8 File Offset: 0x001356D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 31, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F2 RID: 1522
			public static class SmallCloseButton
			{
				/// <summary>Gets a visual style element that represents the small Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the disabled state.</returns>
				// Token: 0x1700151D RID: 5405
				// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x001374E8 File Offset: 0x001356E8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the hot state.</returns>
				// Token: 0x1700151E RID: 5406
				// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x001374F8 File Offset: 0x001356F8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the normal state.</returns>
				// Token: 0x1700151F RID: 5407
				// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x00137508 File Offset: 0x00135708
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the pressed state.</returns>
				// Token: 0x17001520 RID: 5408
				// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x00137518 File Offset: 0x00135718
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F3 RID: 1523
			public static class SmallFrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active small window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active small window.</returns>
				// Token: 0x17001521 RID: 5409
				// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x00137528 File Offset: 0x00135728
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 12, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive small window. </returns>
				// Token: 0x17001522 RID: 5410
				// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x00137538 File Offset: 0x00135738
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 12, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F4 RID: 1524
			public static class SmallFrameBottomSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window.</returns>
				// Token: 0x17001523 RID: 5411
				// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x00137548 File Offset: 0x00135748
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 37, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F5 RID: 1525
			public static class SmallFrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active small window.</returns>
				// Token: 0x17001524 RID: 5412
				// (get) Token: 0x06004FA9 RID: 20393 RVA: 0x00137558 File Offset: 0x00135758
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 10, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive small window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive small window. </returns>
				// Token: 0x17001525 RID: 5413
				// (get) Token: 0x06004FAA RID: 20394 RVA: 0x00137568 File Offset: 0x00135768
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 10, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F6 RID: 1526
			public static class SmallFrameLeftSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the left border of a small window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window. </returns>
				// Token: 0x17001526 RID: 5414
				// (get) Token: 0x06004FAB RID: 20395 RVA: 0x00137578 File Offset: 0x00135778
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 33, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F7 RID: 1527
			public static class SmallFrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active small window.</returns>
				// Token: 0x17001527 RID: 5415
				// (get) Token: 0x06004FAC RID: 20396 RVA: 0x00137588 File Offset: 0x00135788
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 11, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive small window.</returns>
				// Token: 0x17001528 RID: 5416
				// (get) Token: 0x06004FAD RID: 20397 RVA: 0x00137598 File Offset: 0x00135798
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 11, 2);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing template of the right border of a small window. This class cannot be inherited. </summary>
			// Token: 0x020005F8 RID: 1528
			public static class SmallFrameRightSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the right border of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a small window.</returns>
				// Token: 0x17001529 RID: 5417
				// (get) Token: 0x06004FAE RID: 20398 RVA: 0x001375A8 File Offset: 0x001357A8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 35, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized small window. This class cannot be inherited. </summary>
			// Token: 0x020005F9 RID: 1529
			public static class SmallMaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is maximized.</returns>
				// Token: 0x1700152A RID: 5418
				// (get) Token: 0x06004FAF RID: 20399 RVA: 0x001375B8 File Offset: 0x001357B8
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is maximized.</returns>
				// Token: 0x1700152B RID: 5419
				// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x001375C8 File Offset: 0x001357C8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is maximized.</returns>
				// Token: 0x1700152C RID: 5420
				// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x001375D8 File Offset: 0x001357D8
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized small window. This class cannot be inherited. </summary>
			// Token: 0x020005FA RID: 1530
			public static class SmallMinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is minimized.</returns>
				// Token: 0x1700152D RID: 5421
				// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x001375E8 File Offset: 0x001357E8
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is minimized.</returns>
				// Token: 0x1700152E RID: 5422
				// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x001375F8 File Offset: 0x001357F8
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is minimized.</returns>
				// Token: 0x1700152F RID: 5423
				// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x00137608 File Offset: 0x00135808
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a window. This class cannot be inherited. </summary>
			// Token: 0x020005FB RID: 1531
			public static class SysButton
			{
				/// <summary>Gets a visual style element that represents a System button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the disabled state.</returns>
				// Token: 0x17001530 RID: 5424
				// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x00137618 File Offset: 0x00135818
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 13, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the hot state.</returns>
				// Token: 0x17001531 RID: 5425
				// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x00137628 File Offset: 0x00135828
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 13, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the normal state.</returns>
				// Token: 0x17001532 RID: 5426
				// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x00137638 File Offset: 0x00135838
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 13, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the pressed state.</returns>
				// Token: 0x17001533 RID: 5427
				// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x00137648 File Offset: 0x00135848
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 13, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll bar of a window. This class cannot be inherited. </summary>
			// Token: 0x020005FC RID: 1532
			public static class VerticalScroll
			{
				/// <summary>Gets a visual style element that represents a vertical scroll bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the disabled state.</returns>
				// Token: 0x17001534 RID: 5428
				// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x00137658 File Offset: 0x00135858
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 27, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the hot state.</returns>
				// Token: 0x17001535 RID: 5429
				// (get) Token: 0x06004FBA RID: 20410 RVA: 0x00137668 File Offset: 0x00135868
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 27, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the normal state.</returns>
				// Token: 0x17001536 RID: 5430
				// (get) Token: 0x06004FBB RID: 20411 RVA: 0x00137678 File Offset: 0x00135878
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 27, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the pressed state.</returns>
				// Token: 0x17001537 RID: 5431
				// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00137688 File Offset: 0x00135888
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 27, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll box (also known as the thumb) of a window. This class cannot be inherited. </summary>
			// Token: 0x020005FD RID: 1533
			public static class VerticalThumb
			{
				/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
				// Token: 0x17001538 RID: 5432
				// (get) Token: 0x06004FBD RID: 20413 RVA: 0x00137698 File Offset: 0x00135898
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 28, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
				// Token: 0x17001539 RID: 5433
				// (get) Token: 0x06004FBE RID: 20414 RVA: 0x001376A8 File Offset: 0x001358A8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 28, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
				// Token: 0x1700153A RID: 5434
				// (get) Token: 0x06004FBF RID: 20415 RVA: 0x001376B8 File Offset: 0x001358B8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 28, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state.</returns>
				// Token: 0x1700153B RID: 5435
				// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x001376C8 File Offset: 0x001358C8
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 28, 3);
					}
				}
			}
		}
	}
}
