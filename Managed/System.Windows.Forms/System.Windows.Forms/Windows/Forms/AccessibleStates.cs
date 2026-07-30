using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies values representing possible states for an accessible object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003B RID: 59
	[Flags]
	public enum AccessibleStates
	{
		/// <summary>No state.</summary>
		// Token: 0x0400057D RID: 1405
		None = 0,
		/// <summary>An unavailable object.</summary>
		// Token: 0x0400057E RID: 1406
		Unavailable = 1,
		/// <summary>A selected object.</summary>
		// Token: 0x0400057F RID: 1407
		Selected = 2,
		/// <summary>An object with the keyboard focus.</summary>
		// Token: 0x04000580 RID: 1408
		Focused = 4,
		/// <summary>A pressed object.</summary>
		// Token: 0x04000581 RID: 1409
		Pressed = 8,
		/// <summary>An object with a selected check box.</summary>
		// Token: 0x04000582 RID: 1410
		Checked = 16,
		/// <summary>A three-state check box or toolbar button whose state is indeterminate. The check box is neither checked nor unchecked, and it is in the third or mixed state.</summary>
		// Token: 0x04000583 RID: 1411
		Mixed = 32,
		/// <summary>A three-state check box or toolbar button whose state is indeterminate. The check box is neither checked nor unchecked, and it is in the third or mixed state.</summary>
		// Token: 0x04000584 RID: 1412
		Indeterminate = 32,
		/// <summary>A read-only object.</summary>
		// Token: 0x04000585 RID: 1413
		ReadOnly = 64,
		/// <summary>The object hot-tracked by the mouse, meaning its appearance is highlighted to indicate the mouse pointer is located over it.</summary>
		// Token: 0x04000586 RID: 1414
		HotTracked = 128,
		/// <summary>The default button or menu item.</summary>
		// Token: 0x04000587 RID: 1415
		Default = 256,
		/// <summary>The displayed children of the object that are items in an outline or tree structure.</summary>
		// Token: 0x04000588 RID: 1416
		Expanded = 512,
		/// <summary>The hidden children of the object that are items in an outline or tree structure.</summary>
		// Token: 0x04000589 RID: 1417
		Collapsed = 1024,
		/// <summary>A control that cannot accept input in its current condition.</summary>
		// Token: 0x0400058A RID: 1418
		Busy = 2048,
		/// <summary>The object that is not fixed to the boundary of its parent object and that does not move automatically along with the parent.</summary>
		// Token: 0x0400058B RID: 1419
		Floating = 4096,
		/// <summary>An object with scrolling or moving text or graphics.</summary>
		// Token: 0x0400058C RID: 1420
		Marqueed = 8192,
		/// <summary>The object that rapidly or constantly changes appearance. Graphics that are occasionally animated, but not always, should be defined as <see cref="F:System.Windows.Forms.AccessibleRole.Graphic" />ORAnimated. This state should not be used to indicate that the object's location is changing.</summary>
		// Token: 0x0400058D RID: 1421
		Animated = 16384,
		/// <summary>An object without a visible user interface.</summary>
		// Token: 0x0400058E RID: 1422
		Invisible = 32768,
		/// <summary>No on-screen representation. A sound or alert object would have this state, or a hidden window that is never made visible.</summary>
		// Token: 0x0400058F RID: 1423
		Offscreen = 65536,
		/// <summary>A sizable object.</summary>
		// Token: 0x04000590 RID: 1424
		Sizeable = 131072,
		/// <summary>A movable object.</summary>
		// Token: 0x04000591 RID: 1425
		Moveable = 262144,
		/// <summary>The object or child can use text-to-speech (TTS) to describe itself. A speech-based accessibility aid should not announce information when an object with this state has the focus, because the object automatically announces information about itself.</summary>
		// Token: 0x04000592 RID: 1426
		SelfVoicing = 524288,
		/// <summary>The object on the active window that can receive keyboard focus.</summary>
		// Token: 0x04000593 RID: 1427
		Focusable = 1048576,
		/// <summary>An object that can accept selection.</summary>
		// Token: 0x04000594 RID: 1428
		Selectable = 2097152,
		/// <summary>A linked object that has not been previously selected.</summary>
		// Token: 0x04000595 RID: 1429
		Linked = 4194304,
		/// <summary>A linked object that has previously been selected.</summary>
		// Token: 0x04000596 RID: 1430
		Traversed = 8388608,
		/// <summary>An object that accepts multiple selected items.</summary>
		// Token: 0x04000597 RID: 1431
		MultiSelectable = 16777216,
		/// <summary>The altered selection such that all objects between the selection anchor, which is the object with the keyboard focus, and this object take on the anchor object's selection state. If the anchor object is not selected, the objects are removed from the selection. If the anchor object is selected, the selection is extended to include this object and all objects in between. You can set the selection state by combining this with <see cref="F:System.Windows.Forms.AccessibleSelection.AddSelection" /> or <see cref="F:System.Windows.Forms.AccessibleSelection.RemoveSelection" />. This state does not change the focus or the selection anchor unless it is combined with <see cref="F:System.Windows.Forms.AccessibleSelection.TakeFocus" />.</summary>
		// Token: 0x04000598 RID: 1432
		ExtSelectable = 33554432,
		/// <summary>The low-priority information that might not be important to the user.</summary>
		// Token: 0x04000599 RID: 1433
		AlertLow = 67108864,
		/// <summary>The important information that does not need to be conveyed to the user immediately. For example, when a battery-level indicator is starting to reach a low level, it could generate a medium-level alert. Blind access utilities could then generate a sound to let the user know that important information is available, without actually interrupting the user's work. Users can then query the alert information any time they choose.</summary>
		// Token: 0x0400059A RID: 1434
		AlertMedium = 134217728,
		/// <summary>The important information that should be conveyed to the user immediately. For example, a battery-level indicator reaching a critical low level would transition to this state, in which case, a blind access utility would announce this information immediately to the user, and a screen magnification program would scroll the screen so that the battery indicator is in view. This state is also appropriate for any prompt or operation that must be completed before the user can continue.</summary>
		// Token: 0x0400059B RID: 1435
		AlertHigh = 268435456,
		/// <summary>A password-protected edit control.</summary>
		// Token: 0x0400059C RID: 1436
		Protected = 536870912,
		/// <summary>A valid object. This property is deprecated in .NET Framework 2.0. </summary>
		// Token: 0x0400059D RID: 1437
		[Obsolete]
		Valid = 1073741823,
		/// <summary>The object displays a pop-up menu or window when invoked.</summary>
		// Token: 0x0400059E RID: 1438
		HasPopup = 1073741824
	}
}
