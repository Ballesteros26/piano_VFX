using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies values representing possible roles for an accessible object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000039 RID: 57
	public enum AccessibleRole
	{
		/// <summary>No role.</summary>
		// Token: 0x04000533 RID: 1331
		None,
		/// <summary>A title or caption bar for a window.</summary>
		// Token: 0x04000534 RID: 1332
		TitleBar,
		/// <summary>A menu bar, usually beneath the title bar of a window, from which users can select menus.</summary>
		// Token: 0x04000535 RID: 1333
		MenuBar,
		/// <summary>A vertical or horizontal scroll bar, which can be either part of the client area or used in a control.</summary>
		// Token: 0x04000536 RID: 1334
		ScrollBar,
		/// <summary>A special mouse pointer, which allows a user to manipulate user interface elements such as a window. For example, a user can click and drag a sizing grip in the lower-right corner of a window to resize it.</summary>
		// Token: 0x04000537 RID: 1335
		Grip,
		/// <summary>A system sound, which is associated with various system events.</summary>
		// Token: 0x04000538 RID: 1336
		Sound,
		/// <summary>A mouse pointer.</summary>
		// Token: 0x04000539 RID: 1337
		Cursor,
		/// <summary>A caret, which is a flashing line, block, or bitmap that marks the location of the insertion point in a window's client area.</summary>
		// Token: 0x0400053A RID: 1338
		Caret,
		/// <summary>An alert or condition that you can notify a user about. Use this role only for objects that embody an alert but are not associated with another user interface element, such as a message box, graphic, text, or sound.</summary>
		// Token: 0x0400053B RID: 1339
		Alert,
		/// <summary>A window frame, which usually contains child objects such as a title bar, client, and other objects typically contained in a window.</summary>
		// Token: 0x0400053C RID: 1340
		Window,
		/// <summary>A window's user area.</summary>
		// Token: 0x0400053D RID: 1341
		Client,
		/// <summary>A menu, which presents a list of options from which the user can make a selection to perform an action. All menu types must have this role, including drop-down menus that are displayed by selection from a menu bar, and shortcut menus that are displayed when the right mouse button is clicked.</summary>
		// Token: 0x0400053E RID: 1342
		MenuPopup,
		/// <summary>A menu item, which is an entry in a menu that a user can choose to carry out a command, select an option, or display another menu. Functionally, a menu item can be equivalent to a push button, radio button, check box, or menu.</summary>
		// Token: 0x0400053F RID: 1343
		MenuItem,
		/// <summary>A tool tip, which is a small rectangular pop-up window that displays a brief description of the purpose of a button.</summary>
		// Token: 0x04000540 RID: 1344
		ToolTip,
		/// <summary>The main window for an application.</summary>
		// Token: 0x04000541 RID: 1345
		Application,
		/// <summary>A document window, which is always contained within an application window. This role applies only to multiple-document interface (MDI) windows and refers to an object that contains the MDI title bar.</summary>
		// Token: 0x04000542 RID: 1346
		Document,
		/// <summary>A separate area in a frame, a split document window, or a rectangular area of the status bar that can be used to display information. Users can navigate between panes and within the contents of the current pane, but cannot navigate between items in different panes. Thus, panes represent a level of grouping lower than frame windows or documents, but above individual controls. Typically, the user navigates between panes by pressing TAB, F6, or CTRL+TAB, depending on the context.</summary>
		// Token: 0x04000543 RID: 1347
		Pane,
		/// <summary>A graphical image used to represent data.</summary>
		// Token: 0x04000544 RID: 1348
		Chart,
		/// <summary>A dialog box or message box.</summary>
		// Token: 0x04000545 RID: 1349
		Dialog,
		/// <summary>A window border. The entire border is represented by a single object, rather than by separate objects for each side.</summary>
		// Token: 0x04000546 RID: 1350
		Border,
		/// <summary>The objects grouped in a logical manner. There can be a parent-child relationship between the grouping object and the objects it contains.</summary>
		// Token: 0x04000547 RID: 1351
		Grouping,
		/// <summary>A space divided visually into two regions, such as a separator menu item or a separator dividing split panes within a window.</summary>
		// Token: 0x04000548 RID: 1352
		Separator,
		/// <summary>A toolbar, which is a grouping of controls that provide easy access to frequently used features.</summary>
		// Token: 0x04000549 RID: 1353
		ToolBar,
		/// <summary>A status bar, which is an area typically at the bottom of an application window that displays information about the current operation, state of the application, or selected object. The status bar can have multiple fields that display different kinds of information, such as an explanation of the currently selected menu command in the status bar.</summary>
		// Token: 0x0400054A RID: 1354
		StatusBar,
		/// <summary>A table containing rows and columns of cells and, optionally, row headers and column headers.</summary>
		// Token: 0x0400054B RID: 1355
		Table,
		/// <summary>A column header, which provides a visual label for a column in a table.</summary>
		// Token: 0x0400054C RID: 1356
		ColumnHeader,
		/// <summary>A row header, which provides a visual label for a table row.</summary>
		// Token: 0x0400054D RID: 1357
		RowHeader,
		/// <summary>A column of cells within a table.</summary>
		// Token: 0x0400054E RID: 1358
		Column,
		/// <summary>A row of cells within a table.</summary>
		// Token: 0x0400054F RID: 1359
		Row,
		/// <summary>A cell within a table.</summary>
		// Token: 0x04000550 RID: 1360
		Cell,
		/// <summary>A link, which is a connection between a source document and a destination document. This object might look like text or a graphic, but it acts like a button.</summary>
		// Token: 0x04000551 RID: 1361
		Link,
		/// <summary>A Help display in the form of a ToolTip or Help balloon, which contains buttons and labels that users can click to open custom Help topics.</summary>
		// Token: 0x04000552 RID: 1362
		HelpBalloon,
		/// <summary>A cartoon-like graphic object, such as Microsoft Office Assistant, which is typically displayed to provide help to users of an application.</summary>
		// Token: 0x04000553 RID: 1363
		Character,
		/// <summary>A list box, which allows the user to select one or more items.</summary>
		// Token: 0x04000554 RID: 1364
		List,
		/// <summary>An item in a list box or the list portion of a combo box, drop-down list box, or drop-down combo box.</summary>
		// Token: 0x04000555 RID: 1365
		ListItem,
		/// <summary>An outline or tree structure, such as a tree view control, which displays a hierarchical list and usually allows the user to expand and collapse branches.</summary>
		// Token: 0x04000556 RID: 1366
		Outline,
		/// <summary>An item in an outline or tree structure.</summary>
		// Token: 0x04000557 RID: 1367
		OutlineItem,
		/// <summary>A property page that allows a user to view the attributes for a page, such as the page's title, whether it is a home page, or whether the page has been modified. Normally, the only child of this control is a grouped object that contains the contents of the associated page.</summary>
		// Token: 0x04000558 RID: 1368
		PageTab,
		/// <summary>A property page, which is a dialog box that controls the appearance and the behavior of an object, such as a file or resource. A property page's appearance differs according to its purpose.</summary>
		// Token: 0x04000559 RID: 1369
		PropertyPage,
		/// <summary>An indicator, such as a pointer graphic, that points to the current item.</summary>
		// Token: 0x0400055A RID: 1370
		Indicator,
		/// <summary>A picture.</summary>
		// Token: 0x0400055B RID: 1371
		Graphic,
		/// <summary>The read-only text, such as in a label, for other controls or instructions in a dialog box. Static text cannot be modified or selected.</summary>
		// Token: 0x0400055C RID: 1372
		StaticText,
		/// <summary>The selectable text that can be editable or read-only.</summary>
		// Token: 0x0400055D RID: 1373
		Text,
		/// <summary>A push button control, which is a small rectangular control that a user can turn on or off. A push button, also known as a command button, has a raised appearance in its default off state and a sunken appearance when it is turned on.</summary>
		// Token: 0x0400055E RID: 1374
		PushButton,
		/// <summary>A check box control, which is an option that can be turned on or off independent of other options.</summary>
		// Token: 0x0400055F RID: 1375
		CheckButton,
		/// <summary>An option button, also known as a radio button. All objects sharing a single parent that have this attribute are assumed to be part of a single mutually exclusive group. You can use grouped objects to divide option buttons into separate groups when necessary.</summary>
		// Token: 0x04000560 RID: 1376
		RadioButton,
		/// <summary>A combo box, which is an edit control with an associated list box that provides a set of predefined choices.</summary>
		// Token: 0x04000561 RID: 1377
		ComboBox,
		/// <summary>A drop-down list box. This control shows one item and allows the user to display and select another from a list of alternative choices.</summary>
		// Token: 0x04000562 RID: 1378
		DropList,
		/// <summary>A progress bar, which indicates the progress of a lengthy operation by displaying colored lines inside a horizontal rectangle. The length of the lines in relation to the length of the rectangle corresponds to the percentage of the operation that is complete. This control does not take user input.</summary>
		// Token: 0x04000563 RID: 1379
		ProgressBar,
		/// <summary>A dial or knob. This can also be a read-only object, like a speedometer.</summary>
		// Token: 0x04000564 RID: 1380
		Dial,
		/// <summary>A hot-key field that allows the user to enter a combination or sequence of keystrokes to be used as a hot key, which enables users to perform an action quickly. A hot-key control displays the keystrokes entered by the user and ensures that the user selects a valid key combination.</summary>
		// Token: 0x04000565 RID: 1381
		HotkeyField,
		/// <summary>A control, sometimes called a trackbar, that enables a user to adjust a setting in given increments between minimum and maximum values by moving a slider. The volume controls in the Windows operating system are slider controls.</summary>
		// Token: 0x04000566 RID: 1382
		Slider,
		/// <summary>A spin box, also known as an up-down control, which contains a pair of arrow buttons. A user clicks the arrow buttons with a mouse to increment or decrement a value. A spin button control is most often used with a companion control, called a buddy window, where the current value is displayed.</summary>
		// Token: 0x04000567 RID: 1383
		SpinButton,
		/// <summary>A graphical image used to diagram data.</summary>
		// Token: 0x04000568 RID: 1384
		Diagram,
		/// <summary>An animation control, which contains content that is changing over time, such as a control that displays a series of bitmap frames, like a filmstrip. Animation controls are usually displayed when files are being copied, or when some other time-consuming task is being performed.</summary>
		// Token: 0x04000569 RID: 1385
		Animation,
		/// <summary>A mathematical equation.</summary>
		// Token: 0x0400056A RID: 1386
		Equation,
		/// <summary>A button that drops down a list of items.</summary>
		// Token: 0x0400056B RID: 1387
		ButtonDropDown,
		/// <summary>A button that drops down a menu.</summary>
		// Token: 0x0400056C RID: 1388
		ButtonMenu,
		/// <summary>A button that drops down a grid.</summary>
		// Token: 0x0400056D RID: 1389
		ButtonDropDownGrid,
		/// <summary>A blank space between other objects.</summary>
		// Token: 0x0400056E RID: 1390
		WhiteSpace,
		/// <summary>A container of page tab controls.</summary>
		// Token: 0x0400056F RID: 1391
		PageTabList,
		/// <summary>A control that displays the time.</summary>
		// Token: 0x04000570 RID: 1392
		Clock,
		/// <summary>A system-provided role.</summary>
		// Token: 0x04000571 RID: 1393
		Default = -1,
		/// <summary>A toolbar button that has a drop-down list icon directly adjacent to the button.</summary>
		// Token: 0x04000572 RID: 1394
		SplitButton = 62,
		/// <summary>A control designed for entering Internet Protocol (IP) addresses.</summary>
		// Token: 0x04000573 RID: 1395
		IpAddress,
		/// <summary>A control that navigates like an outline item.</summary>
		// Token: 0x04000574 RID: 1396
		OutlineButton
	}
}
