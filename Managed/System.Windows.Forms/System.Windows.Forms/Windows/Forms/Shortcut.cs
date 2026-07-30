using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies shortcut keys that can be used by menu items.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002DC RID: 732
	[ComVisible(true)]
	public enum Shortcut
	{
		/// <summary>The shortcut keys ALT+0.</summary>
		// Token: 0x0400170A RID: 5898
		Alt0 = 262192,
		/// <summary>The shortcut keys ALT+1.</summary>
		// Token: 0x0400170B RID: 5899
		Alt1,
		/// <summary>The shortcut keys ALT+2.</summary>
		// Token: 0x0400170C RID: 5900
		Alt2,
		/// <summary>The shortcut keys ALT+3.</summary>
		// Token: 0x0400170D RID: 5901
		Alt3,
		/// <summary>The shortcut keys ALT+4.</summary>
		// Token: 0x0400170E RID: 5902
		Alt4,
		/// <summary>The shortcut keys ALT+5.</summary>
		// Token: 0x0400170F RID: 5903
		Alt5,
		/// <summary>The shortcut keys ALT+6.</summary>
		// Token: 0x04001710 RID: 5904
		Alt6,
		/// <summary>The shortcut keys ALT+7.</summary>
		// Token: 0x04001711 RID: 5905
		Alt7,
		/// <summary>The shortcut keys ALT+8.</summary>
		// Token: 0x04001712 RID: 5906
		Alt8,
		/// <summary>The shortcut keys ALT+9.</summary>
		// Token: 0x04001713 RID: 5907
		Alt9,
		/// <summary>The shortcut keys ALT+BACKSPACE.</summary>
		// Token: 0x04001714 RID: 5908
		AltBksp = 262152,
		/// <summary>The shortcut keys ALT+DOWNARROW.</summary>
		// Token: 0x04001715 RID: 5909
		AltDownArrow = 262184,
		/// <summary>The shortcut keys ALT+F1.</summary>
		// Token: 0x04001716 RID: 5910
		AltF1 = 262256,
		/// <summary>The shortcut keys ALT+F10.</summary>
		// Token: 0x04001717 RID: 5911
		AltF10 = 262265,
		/// <summary>The shortcut keys ALT+F11.</summary>
		// Token: 0x04001718 RID: 5912
		AltF11,
		/// <summary>The shortcut keys ALT+F12.</summary>
		// Token: 0x04001719 RID: 5913
		AltF12,
		/// <summary>The shortcut keys ALT+F2.</summary>
		// Token: 0x0400171A RID: 5914
		AltF2 = 262257,
		/// <summary>The shortcut keys ALT+F3.</summary>
		// Token: 0x0400171B RID: 5915
		AltF3,
		/// <summary>The shortcut keys ALT+F4.</summary>
		// Token: 0x0400171C RID: 5916
		AltF4,
		/// <summary>The shortcut keys ALT+F5.</summary>
		// Token: 0x0400171D RID: 5917
		AltF5,
		/// <summary>The shortcut keys ALT+F6.</summary>
		// Token: 0x0400171E RID: 5918
		AltF6,
		/// <summary>The shortcut keys ALT+F7.</summary>
		// Token: 0x0400171F RID: 5919
		AltF7,
		/// <summary>The shortcut keys ALT+F8.</summary>
		// Token: 0x04001720 RID: 5920
		AltF8,
		/// <summary>The shortcut keys ALT+F9.</summary>
		// Token: 0x04001721 RID: 5921
		AltF9,
		/// <summary>The shortcut keys ALT+LEFTARROW.</summary>
		// Token: 0x04001722 RID: 5922
		AltLeftArrow = 262181,
		/// <summary>The shortcut keys ALT+RIGHTARROW.</summary>
		// Token: 0x04001723 RID: 5923
		AltRightArrow = 262183,
		/// <summary>The shortcut keys ALT+UPARROW.</summary>
		// Token: 0x04001724 RID: 5924
		AltUpArrow = 262182,
		/// <summary>The shortcut keys CTRL+0.</summary>
		// Token: 0x04001725 RID: 5925
		Ctrl0 = 131120,
		/// <summary>The shortcut keys CTRL+1.</summary>
		// Token: 0x04001726 RID: 5926
		Ctrl1,
		/// <summary>The shortcut keys CTRL+2.</summary>
		// Token: 0x04001727 RID: 5927
		Ctrl2,
		/// <summary>The shortcut keys CTRL+3.</summary>
		// Token: 0x04001728 RID: 5928
		Ctrl3,
		/// <summary>The shortcut keys CTRL+4.</summary>
		// Token: 0x04001729 RID: 5929
		Ctrl4,
		/// <summary>The shortcut keys CTRL+5.</summary>
		// Token: 0x0400172A RID: 5930
		Ctrl5,
		/// <summary>The shortcut keys CTRL+6.</summary>
		// Token: 0x0400172B RID: 5931
		Ctrl6,
		/// <summary>The shortcut keys CTRL+7.</summary>
		// Token: 0x0400172C RID: 5932
		Ctrl7,
		/// <summary>The shortcut keys CTRL+8.</summary>
		// Token: 0x0400172D RID: 5933
		Ctrl8,
		/// <summary>The shortcut keys CTRL+9.</summary>
		// Token: 0x0400172E RID: 5934
		Ctrl9,
		/// <summary>The shortcut keys CTRL+A.</summary>
		// Token: 0x0400172F RID: 5935
		CtrlA = 131137,
		/// <summary>The shortcut keys CTRL+B.</summary>
		// Token: 0x04001730 RID: 5936
		CtrlB,
		/// <summary>The shortcut keys CTRL+C.</summary>
		// Token: 0x04001731 RID: 5937
		CtrlC,
		/// <summary>The shortcut keys CTRL+D.</summary>
		// Token: 0x04001732 RID: 5938
		CtrlD,
		/// <summary>The shortcut keys CTRL+DELETE.</summary>
		// Token: 0x04001733 RID: 5939
		CtrlDel = 131118,
		/// <summary>The shortcut keys CTRL+E.</summary>
		// Token: 0x04001734 RID: 5940
		CtrlE = 131141,
		/// <summary>The shortcut keys CTRL+F.</summary>
		// Token: 0x04001735 RID: 5941
		CtrlF,
		/// <summary>The shortcut keys CTRL+F1.</summary>
		// Token: 0x04001736 RID: 5942
		CtrlF1 = 131184,
		/// <summary>The shortcut keys CTRL+F10.</summary>
		// Token: 0x04001737 RID: 5943
		CtrlF10 = 131193,
		/// <summary>The shortcut keys CTRL+F11.</summary>
		// Token: 0x04001738 RID: 5944
		CtrlF11,
		/// <summary>The shortcut keys CTRL+F12.</summary>
		// Token: 0x04001739 RID: 5945
		CtrlF12,
		/// <summary>The shortcut keys CTRL+F2.</summary>
		// Token: 0x0400173A RID: 5946
		CtrlF2 = 131185,
		/// <summary>The shortcut keys CTRL+F3.</summary>
		// Token: 0x0400173B RID: 5947
		CtrlF3,
		/// <summary>The shortcut keys CTRL+F4.</summary>
		// Token: 0x0400173C RID: 5948
		CtrlF4,
		/// <summary>The shortcut keys CTRL+F5.</summary>
		// Token: 0x0400173D RID: 5949
		CtrlF5,
		/// <summary>The shortcut keys CTRL+F6.</summary>
		// Token: 0x0400173E RID: 5950
		CtrlF6,
		/// <summary>The shortcut keys CTRL+F7.</summary>
		// Token: 0x0400173F RID: 5951
		CtrlF7,
		/// <summary>The shortcut keys CTRL+F8.</summary>
		// Token: 0x04001740 RID: 5952
		CtrlF8,
		/// <summary>The shortcut keys CTRL+F9.</summary>
		// Token: 0x04001741 RID: 5953
		CtrlF9,
		/// <summary>The shortcut keys CTRL+G.</summary>
		// Token: 0x04001742 RID: 5954
		CtrlG = 131143,
		/// <summary>The shortcut keys CTRL+H.</summary>
		// Token: 0x04001743 RID: 5955
		CtrlH,
		/// <summary>The shortcut keys CTRL+I.</summary>
		// Token: 0x04001744 RID: 5956
		CtrlI,
		/// <summary>The shortcut keys CTRL+INSERT.</summary>
		// Token: 0x04001745 RID: 5957
		CtrlIns = 131117,
		/// <summary>The shortcut keys CTRL+J.</summary>
		// Token: 0x04001746 RID: 5958
		CtrlJ = 131146,
		/// <summary>The shortcut keys CTRL+K.</summary>
		// Token: 0x04001747 RID: 5959
		CtrlK,
		/// <summary>The shortcut keys CTRL+L.</summary>
		// Token: 0x04001748 RID: 5960
		CtrlL,
		/// <summary>The shortcut keys CTRL+M.</summary>
		// Token: 0x04001749 RID: 5961
		CtrlM,
		/// <summary>The shortcut keys CTRL+N.</summary>
		// Token: 0x0400174A RID: 5962
		CtrlN,
		/// <summary>The shortcut keys CTRL+O.</summary>
		// Token: 0x0400174B RID: 5963
		CtrlO,
		/// <summary>The shortcut keys CTRL+P.</summary>
		// Token: 0x0400174C RID: 5964
		CtrlP,
		/// <summary>The shortcut keys CTRL+Q.</summary>
		// Token: 0x0400174D RID: 5965
		CtrlQ,
		/// <summary>The shortcut keys CTRL+R.</summary>
		// Token: 0x0400174E RID: 5966
		CtrlR,
		/// <summary>The shortcut keys CTRL+S.</summary>
		// Token: 0x0400174F RID: 5967
		CtrlS,
		/// <summary>The shortcut keys CTRL+SHIFT+0.</summary>
		// Token: 0x04001750 RID: 5968
		CtrlShift0 = 196656,
		/// <summary>The shortcut keys CTRL+SHIFT+1.</summary>
		// Token: 0x04001751 RID: 5969
		CtrlShift1,
		/// <summary>The shortcut keys CTRL+SHIFT+2.</summary>
		// Token: 0x04001752 RID: 5970
		CtrlShift2,
		/// <summary>The shortcut keys CTRL+SHIFT+3.</summary>
		// Token: 0x04001753 RID: 5971
		CtrlShift3,
		/// <summary>The shortcut keys CTRL+SHIFT+4.</summary>
		// Token: 0x04001754 RID: 5972
		CtrlShift4,
		/// <summary>The shortcut keys CTRL+SHIFT+5.</summary>
		// Token: 0x04001755 RID: 5973
		CtrlShift5,
		/// <summary>The shortcut keys CTRL+SHIFT+6.</summary>
		// Token: 0x04001756 RID: 5974
		CtrlShift6,
		/// <summary>The shortcut keys CTRL+SHIFT+7.</summary>
		// Token: 0x04001757 RID: 5975
		CtrlShift7,
		/// <summary>The shortcut keys CTRL+SHIFT+8.</summary>
		// Token: 0x04001758 RID: 5976
		CtrlShift8,
		/// <summary>The shortcut keys CTRL+SHIFT+9.</summary>
		// Token: 0x04001759 RID: 5977
		CtrlShift9,
		/// <summary>The shortcut keys CTRL+SHIFT+A.</summary>
		// Token: 0x0400175A RID: 5978
		CtrlShiftA = 196673,
		/// <summary>The shortcut keys CTRL+SHIFT+B.</summary>
		// Token: 0x0400175B RID: 5979
		CtrlShiftB,
		/// <summary>The shortcut keys CTRL+SHIFT+C.</summary>
		// Token: 0x0400175C RID: 5980
		CtrlShiftC,
		/// <summary>The shortcut keys CTRL+SHIFT+D.</summary>
		// Token: 0x0400175D RID: 5981
		CtrlShiftD,
		/// <summary>The shortcut keys CTRL+SHIFT+E.</summary>
		// Token: 0x0400175E RID: 5982
		CtrlShiftE,
		/// <summary>The shortcut keys CTRL+SHIFT+F.</summary>
		// Token: 0x0400175F RID: 5983
		CtrlShiftF,
		/// <summary>The shortcut keys CTRL+SHIFT+F1.</summary>
		// Token: 0x04001760 RID: 5984
		CtrlShiftF1 = 196720,
		/// <summary>The shortcut keys CTRL+SHIFT+F10.</summary>
		// Token: 0x04001761 RID: 5985
		CtrlShiftF10 = 196729,
		/// <summary>The shortcut keys CTRL+SHIFT+F11.</summary>
		// Token: 0x04001762 RID: 5986
		CtrlShiftF11,
		/// <summary>The shortcut keys CTRL+SHIFT+F12.</summary>
		// Token: 0x04001763 RID: 5987
		CtrlShiftF12,
		/// <summary>The shortcut keys CTRL+SHIFT+F2.</summary>
		// Token: 0x04001764 RID: 5988
		CtrlShiftF2 = 196721,
		/// <summary>The shortcut keys CTRL+SHIFT+F3.</summary>
		// Token: 0x04001765 RID: 5989
		CtrlShiftF3,
		/// <summary>The shortcut keys CTRL+SHIFT+F4.</summary>
		// Token: 0x04001766 RID: 5990
		CtrlShiftF4,
		/// <summary>The shortcut keys CTRL+SHIFT+F5.</summary>
		// Token: 0x04001767 RID: 5991
		CtrlShiftF5,
		/// <summary>The shortcut keys CTRL+SHIFT+F6.</summary>
		// Token: 0x04001768 RID: 5992
		CtrlShiftF6,
		/// <summary>The shortcut keys CTRL+SHIFT+F7.</summary>
		// Token: 0x04001769 RID: 5993
		CtrlShiftF7,
		/// <summary>The shortcut keys CTRL+SHIFT+F8.</summary>
		// Token: 0x0400176A RID: 5994
		CtrlShiftF8,
		/// <summary>The shortcut keys CTRL+SHIFT+F9.</summary>
		// Token: 0x0400176B RID: 5995
		CtrlShiftF9,
		/// <summary>The shortcut keys CTRL+SHIFT+G.</summary>
		// Token: 0x0400176C RID: 5996
		CtrlShiftG = 196679,
		/// <summary>The shortcut keys CTRL+SHIFT+H.</summary>
		// Token: 0x0400176D RID: 5997
		CtrlShiftH,
		/// <summary>The shortcut keys CTRL+SHIFT+I.</summary>
		// Token: 0x0400176E RID: 5998
		CtrlShiftI,
		/// <summary>The shortcut keys CTRL+SHIFT+J.</summary>
		// Token: 0x0400176F RID: 5999
		CtrlShiftJ,
		/// <summary>The shortcut keys CTRL+SHIFT+K.</summary>
		// Token: 0x04001770 RID: 6000
		CtrlShiftK,
		/// <summary>The shortcut keys CTRL+SHIFT+L.</summary>
		// Token: 0x04001771 RID: 6001
		CtrlShiftL,
		/// <summary>The shortcut keys CTRL+SHIFT+M.</summary>
		// Token: 0x04001772 RID: 6002
		CtrlShiftM,
		/// <summary>The shortcut keys CTRL+SHIFT+N.</summary>
		// Token: 0x04001773 RID: 6003
		CtrlShiftN,
		/// <summary>The shortcut keys CTRL+SHIFT+O.</summary>
		// Token: 0x04001774 RID: 6004
		CtrlShiftO,
		/// <summary>The shortcut keys CTRL+SHIFT+P.</summary>
		// Token: 0x04001775 RID: 6005
		CtrlShiftP,
		/// <summary>The shortcut keys CTRL+SHIFT+Q.</summary>
		// Token: 0x04001776 RID: 6006
		CtrlShiftQ,
		/// <summary>The shortcut keys CTRL+SHIFT+R.</summary>
		// Token: 0x04001777 RID: 6007
		CtrlShiftR,
		/// <summary>The shortcut keys CTRL+SHIFT+S.</summary>
		// Token: 0x04001778 RID: 6008
		CtrlShiftS,
		/// <summary>The shortcut keys CTRL+SHIFT+T.</summary>
		// Token: 0x04001779 RID: 6009
		CtrlShiftT,
		/// <summary>The shortcut keys CTRL+SHIFT+U.</summary>
		// Token: 0x0400177A RID: 6010
		CtrlShiftU,
		/// <summary>The shortcut keys CTRL+SHIFT+V.</summary>
		// Token: 0x0400177B RID: 6011
		CtrlShiftV,
		/// <summary>The shortcut keys CTRL+SHIFT+W.</summary>
		// Token: 0x0400177C RID: 6012
		CtrlShiftW,
		/// <summary>The shortcut keys CTRL+SHIFT+X.</summary>
		// Token: 0x0400177D RID: 6013
		CtrlShiftX,
		/// <summary>The shortcut keys CTRL+SHIFT+Y.</summary>
		// Token: 0x0400177E RID: 6014
		CtrlShiftY,
		/// <summary>The shortcut keys CTRL+SHIFT+Z.</summary>
		// Token: 0x0400177F RID: 6015
		CtrlShiftZ,
		/// <summary>The shortcut keys CTRL+T.</summary>
		// Token: 0x04001780 RID: 6016
		CtrlT = 131156,
		/// <summary>The shortcut keys CTRL+U.</summary>
		// Token: 0x04001781 RID: 6017
		CtrlU,
		/// <summary>The shortcut keys CTRL+V.</summary>
		// Token: 0x04001782 RID: 6018
		CtrlV,
		/// <summary>The shortcut keys CTRL+W.</summary>
		// Token: 0x04001783 RID: 6019
		CtrlW,
		/// <summary>The shortcut keys CTRL+X.</summary>
		// Token: 0x04001784 RID: 6020
		CtrlX,
		/// <summary>The shortcut keys CTRL+Y.</summary>
		// Token: 0x04001785 RID: 6021
		CtrlY,
		/// <summary>The shortcut keys CTRL+Z.</summary>
		// Token: 0x04001786 RID: 6022
		CtrlZ,
		/// <summary>The shortcut key DELETE.</summary>
		// Token: 0x04001787 RID: 6023
		Del = 46,
		/// <summary>The shortcut key F1.</summary>
		// Token: 0x04001788 RID: 6024
		F1 = 112,
		/// <summary>The shortcut key F10.</summary>
		// Token: 0x04001789 RID: 6025
		F10 = 121,
		/// <summary>The shortcut key F11.</summary>
		// Token: 0x0400178A RID: 6026
		F11,
		/// <summary>The shortcut key F12.</summary>
		// Token: 0x0400178B RID: 6027
		F12,
		/// <summary>The shortcut key F2.</summary>
		// Token: 0x0400178C RID: 6028
		F2 = 113,
		/// <summary>The shortcut key F3.</summary>
		// Token: 0x0400178D RID: 6029
		F3,
		/// <summary>The shortcut key F4.</summary>
		// Token: 0x0400178E RID: 6030
		F4,
		/// <summary>The shortcut key F5.</summary>
		// Token: 0x0400178F RID: 6031
		F5,
		/// <summary>The shortcut key F6.</summary>
		// Token: 0x04001790 RID: 6032
		F6,
		/// <summary>The shortcut key F7.</summary>
		// Token: 0x04001791 RID: 6033
		F7,
		/// <summary>The shortcut key F8.</summary>
		// Token: 0x04001792 RID: 6034
		F8,
		/// <summary>The shortcut key F9.</summary>
		// Token: 0x04001793 RID: 6035
		F9,
		/// <summary>The shortcut key INSERT.</summary>
		// Token: 0x04001794 RID: 6036
		Ins = 45,
		/// <summary>No shortcut key is associated with the menu item.</summary>
		// Token: 0x04001795 RID: 6037
		None = 0,
		/// <summary>The shortcut keys SHIFT+DELETE.</summary>
		// Token: 0x04001796 RID: 6038
		ShiftDel = 65582,
		/// <summary>The shortcut keys SHIFT+F1.</summary>
		// Token: 0x04001797 RID: 6039
		ShiftF1 = 65648,
		/// <summary>The shortcut keys SHIFT+F10.</summary>
		// Token: 0x04001798 RID: 6040
		ShiftF10 = 65657,
		/// <summary>The shortcut keys SHIFT+F11.</summary>
		// Token: 0x04001799 RID: 6041
		ShiftF11,
		/// <summary>The shortcut keys SHIFT+F12.</summary>
		// Token: 0x0400179A RID: 6042
		ShiftF12,
		/// <summary>The shortcut keys SHIFT+F2.</summary>
		// Token: 0x0400179B RID: 6043
		ShiftF2 = 65649,
		/// <summary>The shortcut keys SHIFT+F3.</summary>
		// Token: 0x0400179C RID: 6044
		ShiftF3,
		/// <summary>The shortcut keys SHIFT+F4.</summary>
		// Token: 0x0400179D RID: 6045
		ShiftF4,
		/// <summary>The shortcut keys SHIFT+F5.</summary>
		// Token: 0x0400179E RID: 6046
		ShiftF5,
		/// <summary>The shortcut keys SHIFT+F6.</summary>
		// Token: 0x0400179F RID: 6047
		ShiftF6,
		/// <summary>The shortcut keys SHIFT+F7.</summary>
		// Token: 0x040017A0 RID: 6048
		ShiftF7,
		/// <summary>The shortcut keys SHIFT+F8.</summary>
		// Token: 0x040017A1 RID: 6049
		ShiftF8,
		/// <summary>The shortcut keys SHIFT+F9.</summary>
		// Token: 0x040017A2 RID: 6050
		ShiftF9,
		/// <summary>The shortcut keys SHIFT+INSERT.</summary>
		// Token: 0x040017A3 RID: 6051
		ShiftIns = 65581
	}
}
