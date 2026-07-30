using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A5 RID: 1189
	internal class Dnd
	{
		// Token: 0x06004BB5 RID: 19381 RVA: 0x0012D0CC File Offset: 0x0012B2CC
		internal Dnd()
		{
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0012D0D4 File Offset: 0x0012B2D4
		static Dnd()
		{
			Dnd.InstallTrackingHandler(Dnd.DragTrackingHandler, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0012D110 File Offset: 0x0012B310
		internal static void TrackingCallback(short message, IntPtr window, IntPtr data, IntPtr dragref)
		{
			XplatUICarbon.GetInstance().FlushQueue();
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0012D11C File Offset: 0x0012B31C
		internal static DragDropEffects DragActionsToEffects(uint actions)
		{
			DragDropEffects dragDropEffects = DragDropEffects.None;
			if ((actions & 1U) != 0U)
			{
				dragDropEffects |= DragDropEffects.Copy;
			}
			if ((actions & 16U) != 0U)
			{
				dragDropEffects |= DragDropEffects.Move;
			}
			if ((actions & 4294967295U) != 0U)
			{
				dragDropEffects |= DragDropEffects.All;
			}
			return dragDropEffects;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0012D158 File Offset: 0x0012B358
		internal static DataObject DragToDataObject(IntPtr dragref)
		{
			uint num = 0U;
			ArrayList arrayList = new ArrayList();
			Dnd.CountDragItems(dragref, ref num);
			for (uint num2 = 1U; num2 <= num; num2 += 1U)
			{
				IntPtr zero = IntPtr.Zero;
				uint num3 = 0U;
				Dnd.GetDragItemReferenceNumber(dragref, num2, ref zero);
				Dnd.CountDragItemFlavors(dragref, zero, ref num3);
				for (uint num4 = 1U; num4 <= num3; num4 += 1U)
				{
					FlavorHandler flavorHandler = new FlavorHandler(dragref, zero, num4);
					if (flavorHandler.Supported)
					{
						arrayList.Add(flavorHandler);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				return ((FlavorHandler)arrayList[0]).Convert(arrayList);
			}
			return new DataObject();
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0012D200 File Offset: 0x0012B400
		internal static bool HandleEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			QDPoint qdpoint = default(QDPoint);
			uint num = 0U;
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null || hwnd.Handle != handle)
			{
				return false;
			}
			Dnd.GetEventParameter(eventref, 1685217639U, 1685217639U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref zero);
			XplatUICarbon.GetGlobalMouse(ref qdpoint);
			Dnd.GetDragAllowableActions(zero, ref num);
			Control control = Control.FromHandle(hwnd.Handle);
			DragDropEffects dragDropEffects = Dnd.DragActionsToEffects(num);
			DataObject dataObject = Dnd.DragToDataObject(zero);
			DragEventArgs dragEventArgs = new DragEventArgs(dataObject, 0, (int)qdpoint.x, (int)qdpoint.y, dragDropEffects, DragDropEffects.None);
			switch (kind)
			{
			case 18U:
			{
				bool allowDrop = control.AllowDrop;
				Dnd.SetEventParameter(eventref, 1668047975U, 1651470188U, (uint)Marshal.SizeOf(typeof(bool)), ref allowDrop);
				control.DndEnter(dragEventArgs);
				Dnd.effects = dragEventArgs.Effect;
				return false;
			}
			case 19U:
				control.DndOver(dragEventArgs);
				Dnd.effects = dragEventArgs.Effect;
				break;
			case 20U:
				control.DndLeave(dragEventArgs);
				break;
			case 21U:
				control.DndDrop(dragEventArgs);
				break;
			}
			return true;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0012D348 File Offset: 0x0012B548
		public void SetAllowDrop(Hwnd hwnd, bool allow)
		{
			if (hwnd.allow_drop == allow)
			{
				return;
			}
			hwnd.allow_drop = allow;
			Dnd.SetControlDragTrackingEnabled(hwnd.whole_window, true);
			Dnd.SetControlDragTrackingEnabled(hwnd.client_window, true);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0012D384 File Offset: 0x0012B584
		public void SendDrop(IntPtr handle, IntPtr from, IntPtr time)
		{
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0012D388 File Offset: 0x0012B588
		public DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowed_effects)
		{
			IntPtr zero = IntPtr.Zero;
			EventRecord eventRecord = default(EventRecord);
			Dnd.effects = DragDropEffects.None;
			Dnd.NewDrag(ref zero);
			XplatUICarbon.GetGlobalMouse(ref eventRecord.mouse);
			this.StoreObjectInDrag(handle, zero, data);
			Dnd.TrackDrag(zero, ref eventRecord, IntPtr.Zero);
			Dnd.DisposeDrag(zero);
			return Dnd.effects;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0012D3E4 File Offset: 0x0012B5E4
		public void StoreObjectInDrag(IntPtr handle, IntPtr dragref, object data)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (data is string)
			{
				throw new NotSupportedException("Implement me.");
			}
			int num2;
			if (data is ISerializable)
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, data);
				intPtr2 = Marshal.AllocHGlobal((int)memoryStream.Length);
				memoryStream.Seek(0L, 0);
				int num = 0;
				while ((long)num < memoryStream.Length)
				{
					Marshal.WriteByte(intPtr2, num, (byte)memoryStream.ReadByte());
					num++;
				}
				intPtr = (IntPtr)1836279154L;
				num2 = (int)memoryStream.Length;
			}
			else
			{
				intPtr2 = (IntPtr)GCHandle.Alloc(data);
				intPtr = (IntPtr)1836019311L;
				num2 = Marshal.SizeOf(typeof(IntPtr));
			}
			Dnd.AddDragItemFlavor(dragref, handle, intPtr, ref intPtr2, num2, 1U);
		}

		// Token: 0x06004BBF RID: 19391
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int InstallTrackingHandler(DragTrackingDelegate callback, IntPtr window, IntPtr data);

		// Token: 0x06004BC0 RID: 19392
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);

		// Token: 0x06004BC1 RID: 19393
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetEventParameter(IntPtr eventref, uint name, uint type, uint size, ref bool data);

		// Token: 0x06004BC2 RID: 19394
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetControlDragTrackingEnabled(IntPtr view, bool enabled);

		// Token: 0x06004BC3 RID: 19395
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int AddDragItemFlavor(IntPtr dragref, IntPtr itemref, IntPtr flavortype, ref IntPtr data, int size, uint flags);

		// Token: 0x06004BC4 RID: 19396
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CountDragItems(IntPtr dragref, ref uint count);

		// Token: 0x06004BC5 RID: 19397
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CountDragItemFlavors(IntPtr dragref, IntPtr itemref, ref uint count);

		// Token: 0x06004BC6 RID: 19398
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetDragItemReferenceNumber(IntPtr dragref, uint index, ref IntPtr itemref);

		// Token: 0x06004BC7 RID: 19399
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int NewDrag(ref IntPtr dragref);

		// Token: 0x06004BC8 RID: 19400
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int TrackDrag(IntPtr dragref, ref EventRecord eventrecord, IntPtr region);

		// Token: 0x06004BC9 RID: 19401
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int DisposeDrag(IntPtr dragref);

		// Token: 0x06004BCA RID: 19402
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetDragAllowableActions(IntPtr dragref, ref uint actions);

		// Token: 0x040028B7 RID: 10423
		internal const uint kEventParamDragRef = 1685217639U;

		// Token: 0x040028B8 RID: 10424
		internal const uint typeDragRef = 1685217639U;

		// Token: 0x040028B9 RID: 10425
		internal const uint typeMono = 1836019311U;

		// Token: 0x040028BA RID: 10426
		internal const uint typeMonoSerializedObject = 1836279154U;

		// Token: 0x040028BB RID: 10427
		private static DragDropEffects effects = DragDropEffects.None;

		// Token: 0x040028BC RID: 10428
		private static DragTrackingDelegate DragTrackingHandler = new DragTrackingDelegate(Dnd.TrackingCallback);
	}
}
