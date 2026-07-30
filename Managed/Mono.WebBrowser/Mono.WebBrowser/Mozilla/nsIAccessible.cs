using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200006D RID: 109
	[Guid("004b6882-2df1-49df-bb5f-0fb81a5b1edf")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessible
	{
		// Token: 0x06000329 RID: 809
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParent([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600032A RID: 810
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600032B RID: 811
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600032C RID: 812
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600032D RID: 813
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600032E RID: 814
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildren([MarshalAs(UnmanagedType.Interface)] out nsIArray ret);

		// Token: 0x0600032F RID: 815
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildCount(out int ret);

		// Token: 0x06000330 RID: 816
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIndexInParent(out int ret);

		// Token: 0x06000331 RID: 817
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x06000332 RID: 818
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setName(HandleRef value);

		// Token: 0x06000333 RID: 819
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getValue(HandleRef ret);

		// Token: 0x06000334 RID: 820
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDescription(HandleRef ret);

		// Token: 0x06000335 RID: 821
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getKeyboardShortcut(HandleRef ret);

		// Token: 0x06000336 RID: 822
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDefaultKeyBinding(HandleRef ret);

		// Token: 0x06000337 RID: 823
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getKeyBindings(char aActionIndex, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDOMStringList ret);

		// Token: 0x06000338 RID: 824
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRole(out uint ret);

		// Token: 0x06000339 RID: 825
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFinalRole(out uint ret);

		// Token: 0x0600033A RID: 826
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFinalState(out uint aState, out uint aExtraState);

		// Token: 0x0600033B RID: 827
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getHelp(HandleRef ret);

		// Token: 0x0600033C RID: 828
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFocusedChild([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600033D RID: 829
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIPersistentProperties ret);

		// Token: 0x0600033E RID: 830
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int groupPosition(out int aGroupLevel, out int aSimilarItemsInGroup, out int aPositionInGroup);

		// Token: 0x0600033F RID: 831
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildAtPoint(int x, int y, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000340 RID: 832
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildAt(int aChildIndex, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000341 RID: 833
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleToRight([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000342 RID: 834
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleToLeft([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000343 RID: 835
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleAbove([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000344 RID: 836
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleBelow([MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000345 RID: 837
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleRelated(uint aRelationType, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000346 RID: 838
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelationsCount(out uint ret);

		// Token: 0x06000347 RID: 839
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelation(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIAccessibleRelation ret);

		// Token: 0x06000348 RID: 840
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelations([MarshalAs(UnmanagedType.Interface)] out nsIArray ret);

		// Token: 0x06000349 RID: 841
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBounds(out int x, out int y, out int width, out int height);

		// Token: 0x0600034A RID: 842
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setSelected(bool isSelected);

		// Token: 0x0600034B RID: 843
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int extendSelection();

		// Token: 0x0600034C RID: 844
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int takeSelection();

		// Token: 0x0600034D RID: 845
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int takeFocus();

		// Token: 0x0600034E RID: 846
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNumActions(out char ret);

		// Token: 0x0600034F RID: 847
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getActionName(char index, HandleRef ret);

		// Token: 0x06000350 RID: 848
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getActionDescription(char aIndex, HandleRef ret);

		// Token: 0x06000351 RID: 849
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int doAction(char index);

		// Token: 0x06000352 RID: 850
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNativeInterface(out IntPtr aOutAccessible);
	}
}
