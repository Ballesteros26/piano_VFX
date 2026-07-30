using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200006B RID: 107
	[Guid("27386cf1-f27e-4d2d-9bf4-c4621d50d299")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessibilityService : nsIAccessibleRetrieval
	{
		// Token: 0x060002FD RID: 765
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x060002FE RID: 766
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttachedAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x060002FF RID: 767
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelevantContentNodeFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000300 RID: 768
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aDOMWin, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000301 RID: 769
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInWeakShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000302 RID: 770
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000303 RID: 771
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCachedAccessNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x06000304 RID: 772
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCachedAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000305 RID: 773
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringRole(uint aRole, HandleRef ret);

		// Token: 0x06000306 RID: 774
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringStates(uint aStates, uint aExtraStates, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDOMStringList ret);

		// Token: 0x06000307 RID: 775
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringEventType(uint aEventType, HandleRef ret);

		// Token: 0x06000308 RID: 776
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringRelationType(uint aRelationType, HandleRef ret);

		// Token: 0x06000309 RID: 777
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createOuterDocAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030A RID: 778
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createRootAccessible(IntPtr aShell, IntPtr aDocument, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTML4ButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030C RID: 780
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHyperTextAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLBRAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030E RID: 782
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLAccessibleByMarkup(IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aWeakShell, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode aDOMNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000310 RID: 784
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLLIAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] IntPtr aBulletFrame, HandleRef aBulletText, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000311 RID: 785
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLCheckboxAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000312 RID: 786
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLComboboxAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000313 RID: 787
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLGenericAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000314 RID: 788
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLGroupboxAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000315 RID: 789
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLHRAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000316 RID: 790
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLImageAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000317 RID: 791
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLLabelAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000318 RID: 792
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLListboxAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000319 RID: 793
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLObjectFrameAccessible(IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031A RID: 794
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLRadioButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031B RID: 795
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLSelectOptionAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIAccessible aAccParent, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031C RID: 796
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLTableAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031D RID: 797
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLTableCellAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031E RID: 798
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLTableHeadAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aDOMNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600031F RID: 799
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLTextAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000320 RID: 800
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLTextFieldAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000321 RID: 801
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createHTMLCaptionAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000322 RID: 802
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aWeakShell, out IntPtr frameHint, out bool aIsHidden, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000323 RID: 803
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addNativeRootAccessible(IntPtr aAtkAccessible, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000324 RID: 804
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeNativeRootAccessible([MarshalAs(UnmanagedType.Interface)] nsIAccessible aRootAccessible);

		// Token: 0x06000325 RID: 805
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int invalidateSubtreeFor(IntPtr aPresShell, IntPtr aChangedContent, uint aEvent);

		// Token: 0x06000326 RID: 806
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int processDocLoadEvent([MarshalAs(UnmanagedType.Interface)] nsITimer aTimer, IntPtr aClosure, uint aEventType);
	}
}
