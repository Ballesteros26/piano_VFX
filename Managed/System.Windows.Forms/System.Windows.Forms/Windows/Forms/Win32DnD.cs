using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020003B5 RID: 949
	internal class Win32DnD
	{
		// Token: 0x0600452F RID: 17711 RVA: 0x0010D93C File Offset: 0x0010BB3C
		static Win32DnD()
		{
			Win32DnD.Win32OleInitialize(IntPtr.Zero);
			Win32DnD.DragDropEventArgs = new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[0]), 0, 0, 0, DragDropEffects.None, DragDropEffects.None);
			Win32DnD.DragFeedbackEventArgs = new GiveFeedbackEventArgs(DragDropEffects.None, true);
			Win32DnD.DragContinueEventArgs = new QueryContinueDragEventArgs(0, false, DragAction.Continue);
			Win32DnD.DragFormats = new ArrayList();
			Win32DnD.DragFormatArray = new Win32DnD.FORMATETC[0];
			Win32DnD.DragMediums = new ArrayList();
			Win32DnD.DOQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDataObject.QueryInterface);
			Win32DnD.DOAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDataObject.AddRef);
			Win32DnD.DORelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDataObject.Release);
			Win32DnD.GetData = new Win32DnD.GetDataDelegate(Win32DnD.ComIDataObject.GetData);
			Win32DnD.GetDataHere = new Win32DnD.GetDataHereDelegate(Win32DnD.ComIDataObject.GetDataHere);
			Win32DnD.QueryGetData = new Win32DnD.QueryGetDataDelegate(Win32DnD.ComIDataObject.QueryGetData);
			Win32DnD.GetCanonicalFormatEtc = new Win32DnD.GetCanonicalFormatEtcDelegate(Win32DnD.ComIDataObject.GetCanonicalFormatEtc);
			Win32DnD.SetData = new Win32DnD.SetDataDelegate(Win32DnD.ComIDataObject.SetData);
			Win32DnD.EnumFormatEtc = new Win32DnD.EnumFormatEtcDelegate(Win32DnD.ComIDataObject.EnumFormatEtc);
			Win32DnD.DAdvise = new Win32DnD.DAdviseDelegate(Win32DnD.ComIDataObject.DAdvise);
			Win32DnD.DUnadvise = new Win32DnD.DUnadviseDelegate(Win32DnD.ComIDataObject.DUnadvise);
			Win32DnD.EnumDAdvise = new Win32DnD.EnumDAdviseDelegate(Win32DnD.ComIDataObject.EnumDAdvise);
			Win32DnD.DSQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDropSource.QueryInterface);
			Win32DnD.DSAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDropSource.AddRef);
			Win32DnD.DSRelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDropSource.Release);
			Win32DnD.QueryContinueDrag = new Win32DnD.QueryContinueDragDelegate(Win32DnD.ComIDropSource.QueryContinueDrag);
			Win32DnD.GiveFeedback = new Win32DnD.GiveFeedbackDelegate(Win32DnD.ComIDropSource.GiveFeedback);
			Win32DnD.DTQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDropTarget.QueryInterface);
			Win32DnD.DTAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDropTarget.AddRef);
			Win32DnD.DTRelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDropTarget.Release);
			Win32DnD.DragEnter = new Win32DnD.DragEnterDelegate(Win32DnD.ComIDropTarget.DragEnter);
			Win32DnD.DragOver = new Win32DnD.DragOverDelegate(Win32DnD.ComIDropTarget.DragOver);
			Win32DnD.DragLeave = new Win32DnD.DragLeaveDelegate(Win32DnD.ComIDropTarget.DragLeave);
			Win32DnD.Drop = new Win32DnD.DropDelegate(Win32DnD.ComIDropTarget.Drop);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x0010DB80 File Offset: 0x0010BD80
		internal static bool HandleWMDropFiles(ref MSG msg)
		{
			IntPtr wParam = msg.wParam;
			int num = Win32DnD.Win32DragQueryFile(wParam, -1, IntPtr.Zero, 0);
			string[] array = new string[num];
			StringBuilder stringBuilder = new StringBuilder(256);
			for (int i = 0; i < num; i++)
			{
				Win32DnD.Win32DragQueryFile(wParam, i, stringBuilder, stringBuilder.Capacity);
				array[i] = stringBuilder.ToString();
			}
			Win32DnD.DragDropEventArgs.Data.SetData(DataFormats.FileDrop, array);
			Control.FromHandle(msg.hwnd).DndDrop(Win32DnD.DragDropEventArgs);
			return true;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x0010DC10 File Offset: 0x0010BE10
		private static bool AddFormatAndMedium(ClipboardFormats cfFormat, object data)
		{
			IntPtr intPtr;
			switch (cfFormat)
			{
			case ClipboardFormats.CF_UNICODETEXT:
			{
				byte[] array = XplatUIWin32.StringToUnicode((string)data);
				intPtr = XplatUIWin32.CopyToMoveableMemory(array);
				break;
			}
			default:
				if (cfFormat != ClipboardFormats.CF_TEXT)
				{
					if (cfFormat != ClipboardFormats.CF_DIB)
					{
						intPtr = IntPtr.Zero;
					}
					else
					{
						byte[] array = XplatUIWin32.ImageToDIB((Image)data);
						intPtr = XplatUIWin32.CopyToMoveableMemory(array);
					}
				}
				else
				{
					byte[] array = XplatUIWin32.StringToAnsi((string)data);
					intPtr = XplatUIWin32.CopyToMoveableMemory(array);
				}
				break;
			case ClipboardFormats.CF_HDROP:
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (data is string || !(data is IEnumerable))
				{
					stringBuilder.Append(data.ToString());
					stringBuilder.Append('\0');
					stringBuilder.Append('\0');
				}
				else
				{
					foreach (object obj in ((IEnumerable)data))
					{
						stringBuilder.Append(obj.ToString());
						stringBuilder.Append('\0');
					}
					stringBuilder.Append('\0');
				}
				IntPtr intPtr2 = Marshal.StringToHGlobalUni(stringBuilder.ToString());
				int num = (int)XplatUIWin32.Win32GlobalSize(intPtr2);
				intPtr = XplatUIWin32.Win32GlobalAlloc(XplatUIWin32.GAllocFlags.GMEM_MOVEABLE | XplatUIWin32.GAllocFlags.GMEM_SHARE, 20 + num);
				IntPtr intPtr3 = XplatUIWin32.Win32GlobalLock(intPtr);
				Marshal.WriteInt32(intPtr3, 20);
				Marshal.WriteInt32(intPtr3, 1 * Marshal.SizeOf(typeof(uint)), 0);
				Marshal.WriteInt32(intPtr3, 2 * Marshal.SizeOf(typeof(uint)), 0);
				Marshal.WriteInt32(intPtr3, 3 * Marshal.SizeOf(typeof(uint)), 0);
				Marshal.WriteInt32(intPtr3, 4 * Marshal.SizeOf(typeof(uint)), 1);
				long num2 = (long)intPtr3;
				num2 += 20L;
				XplatUIWin32.Win32CopyMemory(new IntPtr(num2), intPtr2, num);
				Marshal.FreeHGlobal(intPtr2);
				XplatUIWin32.Win32GlobalUnlock(intPtr3);
				break;
			}
			}
			if (intPtr != IntPtr.Zero)
			{
				Win32DnD.STGMEDIUM stgmedium = default(Win32DnD.STGMEDIUM);
				stgmedium.tymed = Win32DnD.TYMED.TYMED_HGLOBAL;
				stgmedium.hHandle = intPtr;
				stgmedium.pUnkForRelease = IntPtr.Zero;
				Win32DnD.DragMediums.Add(stgmedium);
				Win32DnD.FORMATETC formatetc = default(Win32DnD.FORMATETC);
				formatetc.ptd = IntPtr.Zero;
				formatetc.dwAspect = Win32DnD.DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = Win32DnD.TYMED.TYMED_HGLOBAL;
				formatetc.cfFormat = cfFormat;
				Win32DnD.DragFormats.Add(formatetc);
				return true;
			}
			return false;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x0010DE80 File Offset: 0x0010C080
		private static int FindFormat(Win32DnD.FORMATETC pformatetc)
		{
			for (int i = 0; i < Win32DnD.DragFormats.Count; i++)
			{
				if (((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).cfFormat == pformatetc.cfFormat && ((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).dwAspect == pformatetc.dwAspect && (((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).tymed & pformatetc.tymed) != Win32DnD.TYMED.TYMED_NULL)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0010DF18 File Offset: 0x0010C118
		private static void BuildFormats(object data)
		{
			Win32DnD.DragFormats.Clear();
			Win32DnD.DragMediums.Clear();
			if (data is string)
			{
				Win32DnD.AddFormatAndMedium(ClipboardFormats.CF_TEXT, data);
				Win32DnD.AddFormatAndMedium(ClipboardFormats.CF_UNICODETEXT, data);
				Win32DnD.AddFormatAndMedium(ClipboardFormats.CF_HDROP, data);
			}
			else if (data is Bitmap)
			{
				Win32DnD.AddFormatAndMedium(ClipboardFormats.CF_DIB, data);
			}
			else if (data is ICollection)
			{
				Win32DnD.AddFormatAndMedium(ClipboardFormats.CF_HDROP, data);
			}
			else if (data is ISerializable)
			{
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0010DFA0 File Offset: 0x0010C1A0
		internal static DragDropEffects StartDrag(IntPtr Window, object data, DragDropEffects allowed)
		{
			Win32DnD.BuildFormats(data);
			IntPtr unmanaged = Win32DnD.ComIDataObject.GetUnmanaged();
			IntPtr unmanaged2 = Win32DnD.ComIDropSource.GetUnmanaged(Window);
			IntPtr intPtr = (IntPtr)0;
			Win32DnD.Win32DoDragDrop(unmanaged, unmanaged2, (IntPtr)((int)allowed), ref intPtr);
			Win32DnD.ComIDataObject.ReleaseUnmanaged(unmanaged);
			Win32DnD.ComIDropSource.ReleaseUnmanaged(unmanaged2);
			Win32DnD.DragFormats.Clear();
			Win32DnD.DragFormatArray = null;
			Win32DnD.DragMediums.Clear();
			return (DragDropEffects)intPtr.ToInt32();
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0010E004 File Offset: 0x0010C204
		internal static bool UnregisterDropTarget(IntPtr Window)
		{
			Win32DnD.Win32RevokeDragDrop(Window);
			return true;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0010E010 File Offset: 0x0010C210
		internal static bool RegisterDropTarget(IntPtr Window)
		{
			Hwnd hwnd = Hwnd.ObjectFromWindow(Window);
			if (hwnd == null)
			{
				return false;
			}
			IntPtr unmanaged = Win32DnD.ComIDropTarget.GetUnmanaged(Window);
			hwnd.marshal_free_list.Add(unmanaged);
			uint num = Win32DnD.Win32RegisterDragDrop(Window, unmanaged);
			return num == 0U;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x0010E058 File Offset: 0x0010C258
		private static MethodInfo CreateFuncPtrInterface(AssemblyBuilder assembly, string MethodName, Type ret_type, int param_count)
		{
			ModuleBuilder moduleBuilder = assembly.DefineDynamicModule("XplatUIWin32.FuncInterface" + MethodName);
			TypeBuilder typeBuilder = moduleBuilder.DefineType("XplatUIWin32.FuncInterface" + MethodName, 1);
			Type[] array = new Type[param_count];
			Type[] array2 = new Type[param_count + 1];
			array2[param_count] = typeof(IntPtr);
			for (int i = 0; i < param_count; i++)
			{
				array[i] = typeof(IntPtr);
				array2[i] = typeof(IntPtr);
			}
			MethodBuilder methodBuilder = typeBuilder.DefineMethod(MethodName, 22, ret_type, array2);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			if (param_count > 5)
			{
				ilgenerator.Emit(OpCodes.Ldarg_S, 6);
			}
			if (param_count > 4)
			{
				ilgenerator.Emit(OpCodes.Ldarg_S, 5);
			}
			if (param_count > 3)
			{
				ilgenerator.Emit(OpCodes.Ldarg_S, 4);
			}
			if (param_count > 2)
			{
				ilgenerator.Emit(OpCodes.Ldarg_3);
			}
			if (param_count > 1)
			{
				ilgenerator.Emit(OpCodes.Ldarg_2);
			}
			if (param_count > 0)
			{
				ilgenerator.Emit(OpCodes.Ldarg_1);
			}
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.EmitCalli(OpCodes.Calli, 3, ret_type, array);
			ilgenerator.Emit(OpCodes.Ret);
			Type type = typeBuilder.CreateType();
			return type.GetMethod(MethodName);
		}

		// Token: 0x06004538 RID: 17720
		[DllImport("ole32.dll", CallingConvention = 3, EntryPoint = "RegisterDragDrop")]
		private static extern uint Win32RegisterDragDrop(IntPtr Window, IntPtr pDropTarget);

		// Token: 0x06004539 RID: 17721
		[DllImport("ole32.dll", CallingConvention = 3, EntryPoint = "RevokeDragDrop")]
		private static extern int Win32RevokeDragDrop(IntPtr Window);

		// Token: 0x0600453A RID: 17722
		[DllImport("ole32.dll", CallingConvention = 3, EntryPoint = "DoDragDrop")]
		private static extern uint Win32DoDragDrop(IntPtr pDataObject, IntPtr pDropSource, IntPtr dwOKEffect, ref IntPtr pdwEffect);

		// Token: 0x0600453B RID: 17723
		[DllImport("ole32.dll", CallingConvention = 3, EntryPoint = "OleInitialize")]
		private static extern int Win32OleInitialize(IntPtr pvReserved);

		// Token: 0x0600453C RID: 17724
		[DllImport("shell32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "DragQueryFileW")]
		private static extern int Win32DragQueryFile(IntPtr hDrop, int iFile, IntPtr lpszFile, int cch);

		// Token: 0x0600453D RID: 17725
		[DllImport("shell32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "DragQueryFileW")]
		private static extern int Win32DragQueryFile(IntPtr hDrop, int iFile, StringBuilder lpszFile, int cch);

		// Token: 0x0600453E RID: 17726
		[DllImport("shell32.dll", CallingConvention = 3, EntryPoint = "SHCreateStdEnumFmtEtc")]
		private static extern uint Win32SHCreateStdEnumFmtEtc(uint cfmt, Win32DnD.FORMATETC[] afmt, ref IntPtr ppenumFormatEtc);

		// Token: 0x04001CCF RID: 7375
		private const uint DATADIR_GET = 1U;

		// Token: 0x04001CD0 RID: 7376
		private const uint S_OK = 0U;

		// Token: 0x04001CD1 RID: 7377
		private const uint S_FALSE = 1U;

		// Token: 0x04001CD2 RID: 7378
		private const uint DRAGDROP_S_DROP = 262400U;

		// Token: 0x04001CD3 RID: 7379
		private const uint DRAGDROP_S_CANCEL = 262401U;

		// Token: 0x04001CD4 RID: 7380
		private const uint DRAGDROP_S_USEDEFAULTCURSORS = 262402U;

		// Token: 0x04001CD5 RID: 7381
		private const uint E_NOTIMPL = 2147500033U;

		// Token: 0x04001CD6 RID: 7382
		private const uint E_NOINTERFACE = 2147500034U;

		// Token: 0x04001CD7 RID: 7383
		private const uint E_FAIL = 2147500037U;

		// Token: 0x04001CD8 RID: 7384
		private const uint OLE_E_ADVISENOTSUPPORTED = 2147745795U;

		// Token: 0x04001CD9 RID: 7385
		private const uint DV_E_FORMATETC = 2147745892U;

		// Token: 0x04001CDA RID: 7386
		private static Win32DnD.QueryInterfaceDelegate DOQueryInterface;

		// Token: 0x04001CDB RID: 7387
		private static Win32DnD.AddRefDelegate DOAddRef;

		// Token: 0x04001CDC RID: 7388
		private static Win32DnD.ReleaseDelegate DORelease;

		// Token: 0x04001CDD RID: 7389
		private static Win32DnD.GetDataDelegate GetData;

		// Token: 0x04001CDE RID: 7390
		private static Win32DnD.GetDataHereDelegate GetDataHere;

		// Token: 0x04001CDF RID: 7391
		private static Win32DnD.QueryGetDataDelegate QueryGetData;

		// Token: 0x04001CE0 RID: 7392
		private static Win32DnD.GetCanonicalFormatEtcDelegate GetCanonicalFormatEtc;

		// Token: 0x04001CE1 RID: 7393
		private static Win32DnD.SetDataDelegate SetData;

		// Token: 0x04001CE2 RID: 7394
		private static Win32DnD.EnumFormatEtcDelegate EnumFormatEtc;

		// Token: 0x04001CE3 RID: 7395
		private static Win32DnD.DAdviseDelegate DAdvise;

		// Token: 0x04001CE4 RID: 7396
		private static Win32DnD.DUnadviseDelegate DUnadvise;

		// Token: 0x04001CE5 RID: 7397
		private static Win32DnD.EnumDAdviseDelegate EnumDAdvise;

		// Token: 0x04001CE6 RID: 7398
		private static Win32DnD.QueryInterfaceDelegate DSQueryInterface;

		// Token: 0x04001CE7 RID: 7399
		private static Win32DnD.AddRefDelegate DSAddRef;

		// Token: 0x04001CE8 RID: 7400
		private static Win32DnD.ReleaseDelegate DSRelease;

		// Token: 0x04001CE9 RID: 7401
		private static Win32DnD.QueryContinueDragDelegate QueryContinueDrag;

		// Token: 0x04001CEA RID: 7402
		private static Win32DnD.GiveFeedbackDelegate GiveFeedback;

		// Token: 0x04001CEB RID: 7403
		private static Win32DnD.QueryInterfaceDelegate DTQueryInterface;

		// Token: 0x04001CEC RID: 7404
		private static Win32DnD.AddRefDelegate DTAddRef;

		// Token: 0x04001CED RID: 7405
		private static Win32DnD.ReleaseDelegate DTRelease;

		// Token: 0x04001CEE RID: 7406
		private static Win32DnD.DragEnterDelegate DragEnter;

		// Token: 0x04001CEF RID: 7407
		private static Win32DnD.DragOverDelegate DragOver;

		// Token: 0x04001CF0 RID: 7408
		private static Win32DnD.DragLeaveDelegate DragLeave;

		// Token: 0x04001CF1 RID: 7409
		private static Win32DnD.DropDelegate Drop;

		// Token: 0x04001CF2 RID: 7410
		private static DragEventArgs DragDropEventArgs;

		// Token: 0x04001CF3 RID: 7411
		private static GiveFeedbackEventArgs DragFeedbackEventArgs;

		// Token: 0x04001CF4 RID: 7412
		private static QueryContinueDragEventArgs DragContinueEventArgs;

		// Token: 0x04001CF5 RID: 7413
		private static ArrayList DragFormats;

		// Token: 0x04001CF6 RID: 7414
		private static Win32DnD.FORMATETC[] DragFormatArray;

		// Token: 0x04001CF7 RID: 7415
		private static ArrayList DragMediums;

		// Token: 0x04001CF8 RID: 7416
		private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

		// Token: 0x04001CF9 RID: 7417
		private static readonly Guid IID_IDataObject = new Guid("0000010e-0000-0000-C000-000000000046");

		// Token: 0x04001CFA RID: 7418
		private static readonly Guid IID_IDropSource = new Guid("00000121-0000-0000-C000-000000000046");

		// Token: 0x04001CFB RID: 7419
		private static readonly Guid IID_IDropTarget = new Guid("00000122-0000-0000-C000-000000000046");

		// Token: 0x020003B6 RID: 950
		internal struct FORMATETC
		{
			// Token: 0x04001CFC RID: 7420
			[MarshalAs(6)]
			internal ClipboardFormats cfFormat;

			// Token: 0x04001CFD RID: 7421
			internal IntPtr ptd;

			// Token: 0x04001CFE RID: 7422
			internal Win32DnD.DVASPECT dwAspect;

			// Token: 0x04001CFF RID: 7423
			internal int lindex;

			// Token: 0x04001D00 RID: 7424
			internal Win32DnD.TYMED tymed;
		}

		// Token: 0x020003B7 RID: 951
		internal struct STGMEDIUM
		{
			// Token: 0x04001D01 RID: 7425
			internal Win32DnD.TYMED tymed;

			// Token: 0x04001D02 RID: 7426
			internal IntPtr hHandle;

			// Token: 0x04001D03 RID: 7427
			internal IntPtr pUnkForRelease;
		}

		// Token: 0x020003B8 RID: 952
		[StructLayout(0, CharSet = 3)]
		internal struct DROPFILES
		{
			// Token: 0x04001D04 RID: 7428
			internal uint pFiles;

			// Token: 0x04001D05 RID: 7429
			internal uint pt_x;

			// Token: 0x04001D06 RID: 7430
			internal uint pt_y;

			// Token: 0x04001D07 RID: 7431
			internal bool fNC;

			// Token: 0x04001D08 RID: 7432
			internal bool fWide;

			// Token: 0x04001D09 RID: 7433
			internal string pText;
		}

		// Token: 0x020003B9 RID: 953
		internal enum DVASPECT
		{
			// Token: 0x04001D0B RID: 7435
			DVASPECT_CONTENT = 1,
			// Token: 0x04001D0C RID: 7436
			DVASPECT_THUMBNAIL,
			// Token: 0x04001D0D RID: 7437
			DVASPECT_ICON = 4,
			// Token: 0x04001D0E RID: 7438
			DVASPECT_DOCPRINT = 8
		}

		// Token: 0x020003BA RID: 954
		internal enum TYMED
		{
			// Token: 0x04001D10 RID: 7440
			TYMED_HGLOBAL = 1,
			// Token: 0x04001D11 RID: 7441
			TYMED_FILE,
			// Token: 0x04001D12 RID: 7442
			TYMED_ISTREAM = 4,
			// Token: 0x04001D13 RID: 7443
			TYMED_ISTORAGE = 8,
			// Token: 0x04001D14 RID: 7444
			TYMED_GDI = 16,
			// Token: 0x04001D15 RID: 7445
			TYMED_MFPICT = 32,
			// Token: 0x04001D16 RID: 7446
			TYMED_ENHMF = 64,
			// Token: 0x04001D17 RID: 7447
			TYMED_NULL = 0
		}

		// Token: 0x020003BB RID: 955
		internal class ComIDataObject
		{
			// Token: 0x06004541 RID: 17729 RVA: 0x0010E1C4 File Offset: 0x0010C3C4
			internal static IntPtr GetUnmanaged()
			{
				Win32DnD.ComIDataObject.DataObjectStruct dataObjectStruct = default(Win32DnD.ComIDataObject.DataObjectStruct);
				dataObjectStruct.QueryInterface = Win32DnD.DOQueryInterface;
				dataObjectStruct.AddRef = Win32DnD.DOAddRef;
				dataObjectStruct.Release = Win32DnD.DORelease;
				dataObjectStruct.GetData = Win32DnD.GetData;
				dataObjectStruct.GetDataHere = Win32DnD.GetDataHere;
				dataObjectStruct.QueryGetData = Win32DnD.QueryGetData;
				dataObjectStruct.GetCanonicalFormatEtc = Win32DnD.GetCanonicalFormatEtc;
				dataObjectStruct.SetData = Win32DnD.SetData;
				dataObjectStruct.EnumFormatEtc = Win32DnD.EnumFormatEtc;
				dataObjectStruct.DAdvise = Win32DnD.DAdvise;
				dataObjectStruct.DUnadvise = Win32DnD.DUnadvise;
				dataObjectStruct.EnumDAdvise = Win32DnD.EnumDAdvise;
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Win32DnD.ComIDataObject.DataObjectStruct)));
				Marshal.StructureToPtr(dataObjectStruct, intPtr, false);
				long num = intPtr.ToInt64();
				num += (long)Marshal.SizeOf(typeof(IntPtr));
				Marshal.WriteIntPtr(intPtr, new IntPtr(num));
				return intPtr;
			}

			// Token: 0x06004542 RID: 17730 RVA: 0x0010E2B4 File Offset: 0x0010C4B4
			internal static void ReleaseUnmanaged(IntPtr data_object_ptr)
			{
				Marshal.FreeHGlobal(data_object_ptr);
			}

			// Token: 0x06004543 RID: 17731 RVA: 0x0010E2BC File Offset: 0x0010C4BC
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDataObject.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x06004544 RID: 17732 RVA: 0x0010E350 File Offset: 0x0010C550
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x06004545 RID: 17733 RVA: 0x0010E354 File Offset: 0x0010C554
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x06004546 RID: 17734 RVA: 0x0010E358 File Offset: 0x0010C558
			internal static uint GetData(IntPtr this_, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pmedium)
			{
				int num = Win32DnD.FindFormat(pformatetcIn);
				if (num != -1)
				{
					Win32DnD.ComIDataObject.medium.tymed = Win32DnD.TYMED.TYMED_HGLOBAL;
					Win32DnD.ComIDataObject.medium.hHandle = XplatUIWin32.DupGlobalMem(((Win32DnD.STGMEDIUM)Win32DnD.DragMediums[num]).hHandle);
					Win32DnD.ComIDataObject.medium.pUnkForRelease = IntPtr.Zero;
					try
					{
						Marshal.StructureToPtr(Win32DnD.ComIDataObject.medium, pmedium, false);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: {0}", ex.Message);
					}
					return 0U;
				}
				return 2147745892U;
			}

			// Token: 0x06004547 RID: 17735 RVA: 0x0010E408 File Offset: 0x0010C608
			internal static uint GetDataHere(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium)
			{
				return 2147745892U;
			}

			// Token: 0x06004548 RID: 17736 RVA: 0x0010E410 File Offset: 0x0010C610
			internal static uint QueryGetData(IntPtr @this, ref Win32DnD.FORMATETC pformatetc)
			{
				if (Win32DnD.FindFormat(pformatetc) != -1)
				{
					return 0U;
				}
				return 2147745892U;
			}

			// Token: 0x06004549 RID: 17737 RVA: 0x0010E42C File Offset: 0x0010C62C
			internal static uint GetCanonicalFormatEtc(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pformatetcOut)
			{
				Marshal.WriteIntPtr(pformatetcOut, Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero);
				return 2147500033U;
			}

			// Token: 0x0600454A RID: 17738 RVA: 0x0010E450 File Offset: 0x0010C650
			internal static uint SetData(IntPtr this_, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium, bool release)
			{
				return 2147500033U;
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x0010E458 File Offset: 0x0010C658
			internal static uint EnumFormatEtc(IntPtr this_, uint direction, IntPtr ppenumFormatEtc)
			{
				if (direction == 1U)
				{
					IntPtr zero = IntPtr.Zero;
					Win32DnD.DragFormatArray = new Win32DnD.FORMATETC[Win32DnD.DragFormats.Count];
					for (int i = 0; i < Win32DnD.DragFormats.Count; i++)
					{
						Win32DnD.DragFormatArray[i] = (Win32DnD.FORMATETC)Win32DnD.DragFormats[i];
					}
					Win32DnD.Win32SHCreateStdEnumFmtEtc((uint)Win32DnD.DragFormatArray.Length, Win32DnD.DragFormatArray, ref zero);
					Marshal.WriteIntPtr(ppenumFormatEtc, zero);
					return 0U;
				}
				return 2147500033U;
			}

			// Token: 0x0600454C RID: 17740 RVA: 0x0010E4E4 File Offset: 0x0010C6E4
			internal static uint DAdvise(IntPtr this_, ref Win32DnD.FORMATETC pformatetc, uint advf, IntPtr pAdvSink, ref uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600454D RID: 17741 RVA: 0x0010E4EC File Offset: 0x0010C6EC
			internal static uint DUnadvise(IntPtr this_, uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600454E RID: 17742 RVA: 0x0010E4F4 File Offset: 0x0010C6F4
			internal static uint EnumDAdvise(IntPtr this_, IntPtr ppenumAdvise)
			{
				return 2147745795U;
			}

			// Token: 0x04001D18 RID: 7448
			internal static Win32DnD.STGMEDIUM medium = default(Win32DnD.STGMEDIUM);

			// Token: 0x020003BC RID: 956
			internal struct DataObjectStruct
			{
				// Token: 0x04001D19 RID: 7449
				internal IntPtr vtbl;

				// Token: 0x04001D1A RID: 7450
				internal Win32DnD.QueryInterfaceDelegate QueryInterface;

				// Token: 0x04001D1B RID: 7451
				internal Win32DnD.AddRefDelegate AddRef;

				// Token: 0x04001D1C RID: 7452
				internal Win32DnD.ReleaseDelegate Release;

				// Token: 0x04001D1D RID: 7453
				internal Win32DnD.GetDataDelegate GetData;

				// Token: 0x04001D1E RID: 7454
				internal Win32DnD.GetDataHereDelegate GetDataHere;

				// Token: 0x04001D1F RID: 7455
				internal Win32DnD.QueryGetDataDelegate QueryGetData;

				// Token: 0x04001D20 RID: 7456
				internal Win32DnD.GetCanonicalFormatEtcDelegate GetCanonicalFormatEtc;

				// Token: 0x04001D21 RID: 7457
				internal Win32DnD.SetDataDelegate SetData;

				// Token: 0x04001D22 RID: 7458
				internal Win32DnD.EnumFormatEtcDelegate EnumFormatEtc;

				// Token: 0x04001D23 RID: 7459
				internal Win32DnD.DAdviseDelegate DAdvise;

				// Token: 0x04001D24 RID: 7460
				internal Win32DnD.DUnadviseDelegate DUnadvise;

				// Token: 0x04001D25 RID: 7461
				internal Win32DnD.EnumDAdviseDelegate EnumDAdvise;
			}
		}

		// Token: 0x020003BD RID: 957
		internal class ComIDataObjectUnmanaged
		{
			// Token: 0x0600454F RID: 17743 RVA: 0x0010E4FC File Offset: 0x0010C6FC
			internal ComIDataObjectUnmanaged(IntPtr data_object_ptr)
			{
				if (!Win32DnD.ComIDataObjectUnmanaged.Initialized)
				{
					Win32DnD.ComIDataObjectUnmanaged.Initialize();
				}
				this.vtbl = default(Win32DnD.ComIDataObjectUnmanaged.IDataObjectUnmanaged);
				this.@this = data_object_ptr;
				try
				{
					this.vtbl = (Win32DnD.ComIDataObjectUnmanaged.IDataObjectUnmanaged)Marshal.PtrToStructure(Marshal.ReadIntPtr(data_object_ptr), typeof(Win32DnD.ComIDataObjectUnmanaged.IDataObjectUnmanaged));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception {0}", ex.Message);
				}
			}

			// Token: 0x06004550 RID: 17744 RVA: 0x0010E58C File Offset: 0x0010C78C
			private static void Initialize()
			{
				if (Win32DnD.ComIDataObjectUnmanaged.Initialized)
				{
					return;
				}
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "XplatUIWin32.FuncPtrInterface";
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, 1);
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments = new object[6];
				Win32DnD.ComIDataObjectUnmanaged.GetDataMethod = Win32DnD.CreateFuncPtrInterface(assemblyBuilder, "GetData", typeof(uint), 3);
				Win32DnD.ComIDataObjectUnmanaged.QueryGetDataMethod = Win32DnD.CreateFuncPtrInterface(assemblyBuilder, "QueryGetData", typeof(uint), 2);
				Win32DnD.ComIDataObjectUnmanaged.Initialized = true;
			}

			// Token: 0x06004551 RID: 17745 RVA: 0x0010E60C File Offset: 0x0010C80C
			internal uint QueryInterface(Guid riid, IntPtr ppvObject)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
				Marshal.StructureToPtr(riid, intPtr, false);
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[0] = this.vtbl.QueryInterface;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[1] = this.@this;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[2] = intPtr;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[3] = ppvObject;
				uint num;
				try
				{
					num = (uint)Win32DnD.ComIDataObjectUnmanaged.GetDataMethod.Invoke(null, Win32DnD.ComIDataObjectUnmanaged.MethodArguments);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Caught exception {0}", ex.Message);
					num = 2147500037U;
				}
				Marshal.FreeHGlobal(intPtr);
				return num;
			}

			// Token: 0x06004552 RID: 17746 RVA: 0x0010E6D8 File Offset: 0x0010C8D8
			internal uint AddRef()
			{
				return 1U;
			}

			// Token: 0x06004553 RID: 17747 RVA: 0x0010E6DC File Offset: 0x0010C8DC
			internal uint Release()
			{
				return 0U;
			}

			// Token: 0x06004554 RID: 17748 RVA: 0x0010E6E0 File Offset: 0x0010C8E0
			internal uint GetData(Win32DnD.FORMATETC pformatetcIn, ref Win32DnD.STGMEDIUM pmedium)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Win32DnD.FORMATETC)));
				Marshal.StructureToPtr(pformatetcIn, intPtr, false);
				IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Win32DnD.STGMEDIUM)));
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[0] = this.vtbl.GetData;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[1] = this.@this;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[2] = intPtr;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[3] = intPtr2;
				uint num;
				try
				{
					num = (uint)Win32DnD.ComIDataObjectUnmanaged.GetDataMethod.Invoke(null, Win32DnD.ComIDataObjectUnmanaged.MethodArguments);
					Marshal.PtrToStructure(intPtr2, pmedium);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Caught exception {0}", ex.Message);
					num = 2147500037U;
				}
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
				return num;
			}

			// Token: 0x06004555 RID: 17749 RVA: 0x0010E7D8 File Offset: 0x0010C9D8
			internal uint GetDataHere(Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium)
			{
				return 2147500033U;
			}

			// Token: 0x06004556 RID: 17750 RVA: 0x0010E7E0 File Offset: 0x0010C9E0
			internal uint QueryGetData(Win32DnD.FORMATETC pformatetc)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Win32DnD.FORMATETC)));
				Marshal.StructureToPtr(pformatetc, intPtr, false);
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[0] = this.vtbl.GetData;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[1] = this.@this;
				Win32DnD.ComIDataObjectUnmanaged.MethodArguments[2] = intPtr;
				uint num;
				try
				{
					num = (uint)Win32DnD.ComIDataObjectUnmanaged.QueryGetDataMethod.Invoke(null, Win32DnD.ComIDataObjectUnmanaged.MethodArguments);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Caught exception {0}", ex.Message);
					num = 2147500037U;
				}
				Marshal.FreeHGlobal(intPtr);
				return num;
			}

			// Token: 0x06004557 RID: 17751 RVA: 0x0010E8A0 File Offset: 0x0010CAA0
			internal uint GetCanonicalFormatEtc(Win32DnD.FORMATETC pformatetcIn, ref Win32DnD.FORMATETC pformatetcOut)
			{
				return 2147500033U;
			}

			// Token: 0x06004558 RID: 17752 RVA: 0x0010E8A8 File Offset: 0x0010CAA8
			internal uint SetData(Win32DnD.FORMATETC pformatetc, Win32DnD.STGMEDIUM pmedium, bool release)
			{
				return 2147500033U;
			}

			// Token: 0x06004559 RID: 17753 RVA: 0x0010E8B0 File Offset: 0x0010CAB0
			internal uint EnumFormatEtc(uint direction, IntPtr ppenumFormatEtc)
			{
				return 2147500033U;
			}

			// Token: 0x0600455A RID: 17754 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
			internal uint DAdvise(Win32DnD.FORMATETC pformatetc, uint advf, IntPtr pAdvSink, ref uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600455B RID: 17755 RVA: 0x0010E8C0 File Offset: 0x0010CAC0
			internal uint DUnadvise(uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600455C RID: 17756 RVA: 0x0010E8C8 File Offset: 0x0010CAC8
			internal uint EnumDAdvise(IntPtr ppenumAdvise)
			{
				return 2147745795U;
			}

			// Token: 0x04001D26 RID: 7462
			private static bool Initialized;

			// Token: 0x04001D27 RID: 7463
			private static MethodInfo GetDataMethod;

			// Token: 0x04001D28 RID: 7464
			private static MethodInfo QueryGetDataMethod;

			// Token: 0x04001D29 RID: 7465
			private static object[] MethodArguments;

			// Token: 0x04001D2A RID: 7466
			private Win32DnD.ComIDataObjectUnmanaged.IDataObjectUnmanaged vtbl;

			// Token: 0x04001D2B RID: 7467
			private IntPtr @this;

			// Token: 0x020003BE RID: 958
			internal struct IDataObjectUnmanaged
			{
				// Token: 0x04001D2C RID: 7468
				internal IntPtr QueryInterface;

				// Token: 0x04001D2D RID: 7469
				internal IntPtr AddRef;

				// Token: 0x04001D2E RID: 7470
				internal IntPtr Release;

				// Token: 0x04001D2F RID: 7471
				internal IntPtr GetData;

				// Token: 0x04001D30 RID: 7472
				internal IntPtr GetDataHere;

				// Token: 0x04001D31 RID: 7473
				internal IntPtr QueryGetData;

				// Token: 0x04001D32 RID: 7474
				internal IntPtr GetCanonicalFormatEtc;

				// Token: 0x04001D33 RID: 7475
				internal IntPtr SetData;

				// Token: 0x04001D34 RID: 7476
				internal IntPtr EnumFormatEtc;

				// Token: 0x04001D35 RID: 7477
				internal IntPtr DAdvise;

				// Token: 0x04001D36 RID: 7478
				internal IntPtr DUnadvise;

				// Token: 0x04001D37 RID: 7479
				internal IntPtr EnumDAdvise;
			}
		}

		// Token: 0x020003BF RID: 959
		internal class ComIDropSource
		{
			// Token: 0x0600455E RID: 17758 RVA: 0x0010E8D8 File Offset: 0x0010CAD8
			internal static IntPtr GetUnmanaged(IntPtr Window)
			{
				Win32DnD.ComIDropSource.IDropSource dropSource = new Win32DnD.ComIDropSource.IDropSource
				{
					QueryInterface = Win32DnD.DSQueryInterface,
					AddRef = Win32DnD.DSAddRef,
					Release = Win32DnD.DSRelease,
					QueryContinueDrag = Win32DnD.QueryContinueDrag,
					GiveFeedback = Win32DnD.GiveFeedback,
					Window = Window
				};
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dropSource));
				Marshal.StructureToPtr(dropSource, intPtr, false);
				long num = intPtr.ToInt64();
				num += (long)(2 * Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(intPtr, new IntPtr(num));
				return intPtr;
			}

			// Token: 0x0600455F RID: 17759 RVA: 0x0010E97C File Offset: 0x0010CB7C
			internal static void ReleaseUnmanaged(IntPtr drop_source_ptr)
			{
				Marshal.FreeHGlobal(drop_source_ptr);
			}

			// Token: 0x06004560 RID: 17760 RVA: 0x0010E984 File Offset: 0x0010CB84
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDropSource.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x06004561 RID: 17761 RVA: 0x0010EA18 File Offset: 0x0010CC18
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x06004562 RID: 17762 RVA: 0x0010EA1C File Offset: 0x0010CC1C
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x06004563 RID: 17763 RVA: 0x0010EA20 File Offset: 0x0010CC20
			internal static uint QueryContinueDrag(IntPtr @this, bool fEscapePressed, uint grfkeyState)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				if (fEscapePressed)
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Cancel;
				}
				else if ((grfkeyState & 19U) == 0U)
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Drop;
				}
				else
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Continue;
				}
				Win32DnD.DragContinueEventArgs.escape_pressed = fEscapePressed;
				Win32DnD.DragContinueEventArgs.key_state = (int)grfkeyState;
				Control.FromHandle(intPtr).DndContinueDrag(Win32DnD.DragContinueEventArgs);
				if (Win32DnD.DragContinueEventArgs.drag_action == DragAction.Cancel)
				{
					return 262401U;
				}
				if (Win32DnD.DragContinueEventArgs.drag_action == DragAction.Drop)
				{
					return 262400U;
				}
				return 0U;
			}

			// Token: 0x06004564 RID: 17764 RVA: 0x0010EAD0 File Offset: 0x0010CCD0
			internal static uint GiveFeedback(IntPtr @this, uint pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragFeedbackEventArgs.effect = (DragDropEffects)pdwEffect;
				Win32DnD.DragFeedbackEventArgs.use_default_cursors = true;
				Control.FromHandle(intPtr).DndFeedback(Win32DnD.DragFeedbackEventArgs);
				if (Win32DnD.DragFeedbackEventArgs.use_default_cursors)
				{
					return 262402U;
				}
				return 0U;
			}

			// Token: 0x020003C0 RID: 960
			internal struct IDropSource
			{
				// Token: 0x04001D38 RID: 7480
				internal IntPtr vtbl;

				// Token: 0x04001D39 RID: 7481
				internal IntPtr Window;

				// Token: 0x04001D3A RID: 7482
				internal Win32DnD.QueryInterfaceDelegate QueryInterface;

				// Token: 0x04001D3B RID: 7483
				internal Win32DnD.AddRefDelegate AddRef;

				// Token: 0x04001D3C RID: 7484
				internal Win32DnD.ReleaseDelegate Release;

				// Token: 0x04001D3D RID: 7485
				internal Win32DnD.QueryContinueDragDelegate QueryContinueDrag;

				// Token: 0x04001D3E RID: 7486
				internal Win32DnD.GiveFeedbackDelegate GiveFeedback;
			}
		}

		// Token: 0x020003C1 RID: 961
		internal class ComIDropTarget
		{
			// Token: 0x06004566 RID: 17766 RVA: 0x0010EB38 File Offset: 0x0010CD38
			internal static IntPtr GetUnmanaged(IntPtr Window)
			{
				Win32DnD.ComIDropTarget.IDropTarget dropTarget = new Win32DnD.ComIDropTarget.IDropTarget
				{
					QueryInterface = Win32DnD.DTQueryInterface,
					AddRef = Win32DnD.DTAddRef,
					Release = Win32DnD.DTRelease,
					DragEnter = Win32DnD.DragEnter,
					DragOver = Win32DnD.DragOver,
					DragLeave = Win32DnD.DragLeave,
					Drop = Win32DnD.Drop,
					Window = Window
				};
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dropTarget));
				Marshal.StructureToPtr(dropTarget, intPtr, false);
				long num = intPtr.ToInt64();
				num += (long)(2 * Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(intPtr, new IntPtr(num));
				return intPtr;
			}

			// Token: 0x06004567 RID: 17767 RVA: 0x0010EBF4 File Offset: 0x0010CDF4
			internal static void ReleaseUnmanaged(IntPtr drop_target_ptr)
			{
				Marshal.FreeHGlobal(drop_target_ptr);
			}

			// Token: 0x06004568 RID: 17768 RVA: 0x0010EBFC File Offset: 0x0010CDFC
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDropTarget.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x06004569 RID: 17769 RVA: 0x0010EC90 File Offset: 0x0010CE90
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x0600456A RID: 17770 RVA: 0x0010EC94 File Offset: 0x0010CE94
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x0600456B RID: 17771 RVA: 0x0010EC98 File Offset: 0x0010CE98
			internal static uint DragEnter(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control.FromHandle(intPtr).DndEnter(Win32DnD.DragDropEventArgs);
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x0600456C RID: 17772 RVA: 0x0010ED38 File Offset: 0x0010CF38
			internal static uint DragOver(IntPtr @this, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control.FromHandle(intPtr).DndOver(Win32DnD.DragDropEventArgs);
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x0600456D RID: 17773 RVA: 0x0010EDD8 File Offset: 0x0010CFD8
			internal static uint DragLeave(IntPtr @this)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Control.FromHandle(intPtr).DndLeave(EventArgs.Empty);
				return 0U;
			}

			// Token: 0x0600456E RID: 17774 RVA: 0x0010EE0C File Offset: 0x0010D00C
			internal static uint Drop(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control control = Control.FromHandle(intPtr);
				if (control != null)
				{
					control.DndDrop(Win32DnD.DragDropEventArgs);
					return 1U;
				}
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x020003C2 RID: 962
			internal struct IDropTarget
			{
				// Token: 0x04001D3F RID: 7487
				internal IntPtr vtbl;

				// Token: 0x04001D40 RID: 7488
				internal IntPtr Window;

				// Token: 0x04001D41 RID: 7489
				internal Win32DnD.QueryInterfaceDelegate QueryInterface;

				// Token: 0x04001D42 RID: 7490
				internal Win32DnD.AddRefDelegate AddRef;

				// Token: 0x04001D43 RID: 7491
				internal Win32DnD.ReleaseDelegate Release;

				// Token: 0x04001D44 RID: 7492
				internal Win32DnD.DragEnterDelegate DragEnter;

				// Token: 0x04001D45 RID: 7493
				internal Win32DnD.DragOverDelegate DragOver;

				// Token: 0x04001D46 RID: 7494
				internal Win32DnD.DragLeaveDelegate DragLeave;

				// Token: 0x04001D47 RID: 7495
				internal Win32DnD.DropDelegate Drop;
			}
		}

		// Token: 0x02000639 RID: 1593
		// (Invoke) Token: 0x06005096 RID: 20630
		internal delegate uint QueryInterfaceDelegate(IntPtr @this, ref Guid riid, IntPtr ppvObject);

		// Token: 0x0200063A RID: 1594
		// (Invoke) Token: 0x0600509A RID: 20634
		internal delegate uint AddRefDelegate(IntPtr @this);

		// Token: 0x0200063B RID: 1595
		// (Invoke) Token: 0x0600509E RID: 20638
		internal delegate uint ReleaseDelegate(IntPtr @this);

		// Token: 0x0200063C RID: 1596
		// (Invoke) Token: 0x060050A2 RID: 20642
		internal delegate uint GetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pmedium);

		// Token: 0x0200063D RID: 1597
		// (Invoke) Token: 0x060050A6 RID: 20646
		internal delegate uint GetDataHereDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium);

		// Token: 0x0200063E RID: 1598
		// (Invoke) Token: 0x060050AA RID: 20650
		internal delegate uint QueryGetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc);

		// Token: 0x0200063F RID: 1599
		// (Invoke) Token: 0x060050AE RID: 20654
		internal delegate uint GetCanonicalFormatEtcDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pformatetcOut);

		// Token: 0x02000640 RID: 1600
		// (Invoke) Token: 0x060050B2 RID: 20658
		internal delegate uint SetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium, bool release);

		// Token: 0x02000641 RID: 1601
		// (Invoke) Token: 0x060050B6 RID: 20662
		internal delegate uint EnumFormatEtcDelegate(IntPtr @this, uint direction, IntPtr ppenumFormatEtc);

		// Token: 0x02000642 RID: 1602
		// (Invoke) Token: 0x060050BA RID: 20666
		internal delegate uint DAdviseDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, uint advf, IntPtr pAdvSink, ref uint pdwConnection);

		// Token: 0x02000643 RID: 1603
		// (Invoke) Token: 0x060050BE RID: 20670
		internal delegate uint DUnadviseDelegate(IntPtr @this, uint pdwConnection);

		// Token: 0x02000644 RID: 1604
		// (Invoke) Token: 0x060050C2 RID: 20674
		internal delegate uint EnumDAdviseDelegate(IntPtr @this, IntPtr ppenumAdvise);

		// Token: 0x02000645 RID: 1605
		// (Invoke) Token: 0x060050C6 RID: 20678
		internal delegate uint QueryContinueDragDelegate(IntPtr @this, bool fEscapePressed, uint grfkeyState);

		// Token: 0x02000646 RID: 1606
		// (Invoke) Token: 0x060050CA RID: 20682
		internal delegate uint GiveFeedbackDelegate(IntPtr @this, uint pdwEffect);

		// Token: 0x02000647 RID: 1607
		// (Invoke) Token: 0x060050CE RID: 20686
		internal delegate uint DragEnterDelegate(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);

		// Token: 0x02000648 RID: 1608
		// (Invoke) Token: 0x060050D2 RID: 20690
		internal delegate uint DragOverDelegate(IntPtr @this, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);

		// Token: 0x02000649 RID: 1609
		// (Invoke) Token: 0x060050D6 RID: 20694
		internal delegate uint DragLeaveDelegate(IntPtr @this);

		// Token: 0x0200064A RID: 1610
		// (Invoke) Token: 0x060050DA RID: 20698
		internal delegate uint DropDelegate(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);
	}
}
