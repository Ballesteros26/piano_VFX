using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000048 RID: 72
	internal sealed class ComIStreamMarshaler : ICustomMarshaler
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00002050 File Offset: 0x00000250
		private ComIStreamMarshaler()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007051 File Offset: 0x00005251
		private static ICustomMarshaler GetInstance(string cookie)
		{
			return ComIStreamMarshaler.defaultInstance;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007058 File Offset: 0x00005258
		public IntPtr MarshalManagedToNative(object managedObj)
		{
			return ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface((IStream)managedObj);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007065 File Offset: 0x00005265
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseInterface(pNativeData);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000706D File Offset: 0x0000526D
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(pNativeData, false);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007076 File Offset: 0x00005276
		public void CleanUpManagedData(object managedObj)
		{
			ComIStreamMarshaler.NativeToManagedWrapper.ReleaseInterface((IStream)managedObj);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007083 File Offset: 0x00005283
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0400035E RID: 862
		private const int S_OK = 0;

		// Token: 0x0400035F RID: 863
		private const int E_NOINTERFACE = -2147467262;

		// Token: 0x04000360 RID: 864
		private static readonly ComIStreamMarshaler defaultInstance = new ComIStreamMarshaler();

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x060002C5 RID: 709
		private delegate int QueryInterfaceDelegate(IntPtr @this, [In] ref Guid riid, IntPtr ppvObject);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x060002C9 RID: 713
		private delegate int AddRefDelegate(IntPtr @this);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060002CD RID: 717
		private delegate int ReleaseDelegate(IntPtr @this);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x060002D1 RID: 721
		private delegate int ReadDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x060002D5 RID: 725
		private delegate int WriteDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060002D9 RID: 729
		private delegate int SeekDelegate(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060002DD RID: 733
		private delegate int SetSizeDelegate(IntPtr @this, long libNewSize);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060002E1 RID: 737
		private delegate int CopyToDelegate(IntPtr @this, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x060002E5 RID: 741
		private delegate int CommitDelegate(IntPtr @this, int grfCommitFlags);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060002E9 RID: 745
		private delegate int RevertDelegate(IntPtr @this);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x060002ED RID: 749
		private delegate int LockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x060002F1 RID: 753
		private delegate int UnlockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x060002F5 RID: 757
		private delegate int StatDelegate(IntPtr @this, out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x060002F9 RID: 761
		private delegate int CloneDelegate(IntPtr @this, out IntPtr ppstm);

		// Token: 0x02000057 RID: 87
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamInterface
		{
			// Token: 0x04000361 RID: 865
			internal IntPtr lpVtbl;

			// Token: 0x04000362 RID: 866
			internal IntPtr gcHandle;
		}

		// Token: 0x02000058 RID: 88
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamVtbl
		{
			// Token: 0x04000363 RID: 867
			internal ComIStreamMarshaler.QueryInterfaceDelegate QueryInterface;

			// Token: 0x04000364 RID: 868
			internal ComIStreamMarshaler.AddRefDelegate AddRef;

			// Token: 0x04000365 RID: 869
			internal ComIStreamMarshaler.ReleaseDelegate Release;

			// Token: 0x04000366 RID: 870
			internal ComIStreamMarshaler.ReadDelegate Read;

			// Token: 0x04000367 RID: 871
			internal ComIStreamMarshaler.WriteDelegate Write;

			// Token: 0x04000368 RID: 872
			internal ComIStreamMarshaler.SeekDelegate Seek;

			// Token: 0x04000369 RID: 873
			internal ComIStreamMarshaler.SetSizeDelegate SetSize;

			// Token: 0x0400036A RID: 874
			internal ComIStreamMarshaler.CopyToDelegate CopyTo;

			// Token: 0x0400036B RID: 875
			internal ComIStreamMarshaler.CommitDelegate Commit;

			// Token: 0x0400036C RID: 876
			internal ComIStreamMarshaler.RevertDelegate Revert;

			// Token: 0x0400036D RID: 877
			internal ComIStreamMarshaler.LockRegionDelegate LockRegion;

			// Token: 0x0400036E RID: 878
			internal ComIStreamMarshaler.UnlockRegionDelegate UnlockRegion;

			// Token: 0x0400036F RID: 879
			internal ComIStreamMarshaler.StatDelegate Stat;

			// Token: 0x04000370 RID: 880
			internal ComIStreamMarshaler.CloneDelegate Clone;
		}

		// Token: 0x02000059 RID: 89
		private sealed class ManagedToNativeWrapper
		{
			// Token: 0x060002FE RID: 766 RVA: 0x00007094 File Offset: 0x00005294
			static ManagedToNativeWrapper()
			{
				EventHandler eventHandler = new EventHandler(ComIStreamMarshaler.ManagedToNativeWrapper.OnShutdown);
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.DomainUnload += eventHandler;
				currentDomain.ProcessExit += eventHandler;
				ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable = new ComIStreamMarshaler.IStreamVtbl
				{
					QueryInterface = new ComIStreamMarshaler.QueryInterfaceDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.QueryInterface),
					AddRef = new ComIStreamMarshaler.AddRefDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.AddRef),
					Release = new ComIStreamMarshaler.ReleaseDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Release),
					Read = new ComIStreamMarshaler.ReadDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Read),
					Write = new ComIStreamMarshaler.WriteDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Write),
					Seek = new ComIStreamMarshaler.SeekDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Seek),
					SetSize = new ComIStreamMarshaler.SetSizeDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.SetSize),
					CopyTo = new ComIStreamMarshaler.CopyToDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.CopyTo),
					Commit = new ComIStreamMarshaler.CommitDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Commit),
					Revert = new ComIStreamMarshaler.RevertDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Revert),
					LockRegion = new ComIStreamMarshaler.LockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.LockRegion),
					UnlockRegion = new ComIStreamMarshaler.UnlockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.UnlockRegion),
					Stat = new ComIStreamMarshaler.StatDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Stat),
					Clone = new ComIStreamMarshaler.CloneDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Clone)
				};
				ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
			}

			// Token: 0x060002FF RID: 767 RVA: 0x00007224 File Offset: 0x00005424
			private ManagedToNativeWrapper(IStream managedInterface)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable == IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
					}
					ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount++;
				}
				try
				{
					this.managedInterface = managedInterface;
					this.gcHandle = GCHandle.Alloc(this);
					ComIStreamMarshaler.IStreamInterface streamInterface = new ComIStreamMarshaler.IStreamInterface();
					streamInterface.lpVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.comVtable;
					streamInterface.gcHandle = (IntPtr)this.gcHandle;
					this.comInterface = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamInterface)));
					Marshal.StructureToPtr<ComIStreamMarshaler.IStreamInterface>(streamInterface, this.comInterface, false);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x00007308 File Offset: 0x00005508
			private void Dispose()
			{
				if (this.gcHandle.IsAllocated)
				{
					this.gcHandle.Free();
				}
				if (this.comInterface != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.comInterface);
					this.comInterface = IntPtr.Zero;
				}
				this.managedInterface = null;
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (--ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && Environment.HasShutdownStarted)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x06000301 RID: 769 RVA: 0x000073A4 File Offset: 0x000055A4
			private static void OnShutdown(object sender, EventArgs e)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable != IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x06000302 RID: 770 RVA: 0x000073FC File Offset: 0x000055FC
			private static void CreateVtable()
			{
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamVtbl)));
				Marshal.StructureToPtr<ComIStreamMarshaler.IStreamVtbl>(ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable, ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, false);
			}

			// Token: 0x06000303 RID: 771 RVA: 0x00007427 File Offset: 0x00005627
			private static void DisposeVtable()
			{
				Marshal.DestroyStructure(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, typeof(ComIStreamMarshaler.IStreamVtbl));
				Marshal.FreeHGlobal(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable);
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = IntPtr.Zero;
			}

			// Token: 0x06000304 RID: 772 RVA: 0x00007451 File Offset: 0x00005651
			internal static IStream GetUnderlyingInterface(IntPtr comInterface, bool outParam)
			{
				if (Marshal.ReadIntPtr(comInterface) == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
				{
					IStream stream = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(comInterface).managedInterface;
					if (outParam)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
					}
					return stream;
				}
				return null;
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000747C File Offset: 0x0000567C
			internal static IntPtr GetInterface(IStream managedInterface)
			{
				if (managedInterface == null)
				{
					return IntPtr.Zero;
				}
				IntPtr underlyingInterface;
				if ((underlyingInterface = ComIStreamMarshaler.NativeToManagedWrapper.GetUnderlyingInterface(managedInterface)) == IntPtr.Zero)
				{
					underlyingInterface = new ComIStreamMarshaler.ManagedToNativeWrapper(managedInterface).comInterface;
				}
				return underlyingInterface;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x000074B4 File Offset: 0x000056B4
			internal static void ReleaseInterface(IntPtr comInterface)
			{
				if (comInterface != IntPtr.Zero)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(comInterface);
					if (intPtr == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
						return;
					}
					((ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot)Marshal.PtrToStructure((IntPtr)((long)intPtr + (long)(IntPtr.Size * 2)), typeof(ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot))).Release(comInterface);
				}
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000751E File Offset: 0x0000571E
			private static int GetHRForException(Exception e)
			{
				return (int)ComIStreamMarshaler.ManagedToNativeWrapper.exceptionGetHResult.Invoke(e, null);
			}

			// Token: 0x06000308 RID: 776 RVA: 0x00007534 File Offset: 0x00005734
			private static ComIStreamMarshaler.ManagedToNativeWrapper GetObject(IntPtr @this)
			{
				return (ComIStreamMarshaler.ManagedToNativeWrapper)((GCHandle)Marshal.ReadIntPtr(@this, IntPtr.Size)).Target;
			}

			// Token: 0x06000309 RID: 777 RVA: 0x00007560 File Offset: 0x00005760
			private static int QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				int num;
				try
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.IID_IUnknown.Equals(riid) || ComIStreamMarshaler.ManagedToNativeWrapper.IID_IStream.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						ComIStreamMarshaler.ManagedToNativeWrapper.AddRef(@this);
						num = 0;
					}
					else
					{
						Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
						num = -2147467262;
					}
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x0600030A RID: 778 RVA: 0x000075D8 File Offset: 0x000057D8
			private static int AddRef(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper2 = @object;
						num = managedToNativeWrapper2.refCount + 1;
						managedToNativeWrapper2.refCount = num;
						num = num;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x0600030B RID: 779 RVA: 0x00007638 File Offset: 0x00005838
			private static int Release(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						if (@object.refCount != 0)
						{
							ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper2 = @object;
							num = managedToNativeWrapper2.refCount - 1;
							managedToNativeWrapper2.refCount = num;
							if (num == 0)
							{
								@object.Dispose();
							}
						}
						num = @object.refCount;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x0600030C RID: 780 RVA: 0x000076B0 File Offset: 0x000058B0
			private static int Read(IntPtr @this, byte[] pv, int cb, IntPtr pcbRead)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Read(pv, cb, pcbRead);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x0600030D RID: 781 RVA: 0x000076F0 File Offset: 0x000058F0
			private static int Write(IntPtr @this, byte[] pv, int cb, IntPtr pcbWritten)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Write(pv, cb, pcbWritten);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00007730 File Offset: 0x00005930
			private static int Seek(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Seek(dlibMove, dwOrigin, plibNewPosition);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x0600030F RID: 783 RVA: 0x00007770 File Offset: 0x00005970
			private static int SetSize(IntPtr @this, long libNewSize)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.SetSize(libNewSize);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000310 RID: 784 RVA: 0x000077AC File Offset: 0x000059AC
			private static int CopyTo(IntPtr @this, IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.CopyTo(pstm, cb, pcbRead, pcbWritten);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000311 RID: 785 RVA: 0x000077EC File Offset: 0x000059EC
			private static int Commit(IntPtr @this, int grfCommitFlags)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Commit(grfCommitFlags);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000312 RID: 786 RVA: 0x00007828 File Offset: 0x00005A28
			private static int Revert(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Revert();
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000313 RID: 787 RVA: 0x00007864 File Offset: 0x00005A64
			private static int LockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.LockRegion(libOffset, cb, dwLockType);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000314 RID: 788 RVA: 0x000078A4 File Offset: 0x00005AA4
			private static int UnlockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.UnlockRegion(libOffset, cb, dwLockType);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000315 RID: 789 RVA: 0x000078E4 File Offset: 0x00005AE4
			private static int Stat(IntPtr @this, out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Stat(out pstatstg, grfStatFlag);
					num = 0;
				}
				catch (Exception ex)
				{
					pstatstg = default(global::System.Runtime.InteropServices.ComTypes.STATSTG);
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x06000316 RID: 790 RVA: 0x00007928 File Offset: 0x00005B28
			private static int Clone(IntPtr @this, out IntPtr ppstm)
			{
				ppstm = IntPtr.Zero;
				int num;
				try
				{
					IStream stream;
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Clone(out stream);
					ppstm = ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface(stream);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x04000371 RID: 881
			private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

			// Token: 0x04000372 RID: 882
			private static readonly Guid IID_IStream = new Guid("0000000C-0000-0000-C000-000000000046");

			// Token: 0x04000373 RID: 883
			private static readonly MethodInfo exceptionGetHResult = typeof(Exception).GetTypeInfo().GetProperty("HResult", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.ExactBinding, null, typeof(int), new Type[0], null).GetGetMethod(true);

			// Token: 0x04000374 RID: 884
			private static readonly ComIStreamMarshaler.IStreamVtbl managedVtable;

			// Token: 0x04000375 RID: 885
			private static IntPtr comVtable;

			// Token: 0x04000376 RID: 886
			private static int vtableRefCount;

			// Token: 0x04000377 RID: 887
			private IStream managedInterface;

			// Token: 0x04000378 RID: 888
			private IntPtr comInterface;

			// Token: 0x04000379 RID: 889
			private GCHandle gcHandle;

			// Token: 0x0400037A RID: 890
			private int refCount = 1;

			// Token: 0x0200005A RID: 90
			[StructLayout(LayoutKind.Sequential)]
			private sealed class ReleaseSlot
			{
				// Token: 0x0400037B RID: 891
				internal ComIStreamMarshaler.ReleaseDelegate Release;
			}
		}

		// Token: 0x0200005B RID: 91
		private sealed class NativeToManagedWrapper : IStream
		{
			// Token: 0x06000318 RID: 792 RVA: 0x00007974 File Offset: 0x00005B74
			private NativeToManagedWrapper(IntPtr comInterface, bool outParam)
			{
				this.comInterface = comInterface;
				this.managedVtable = (ComIStreamMarshaler.IStreamVtbl)Marshal.PtrToStructure(Marshal.ReadIntPtr(comInterface), typeof(ComIStreamMarshaler.IStreamVtbl));
				if (!outParam)
				{
					this.managedVtable.AddRef(comInterface);
				}
			}

			// Token: 0x06000319 RID: 793 RVA: 0x000079C4 File Offset: 0x00005BC4
			~NativeToManagedWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x0600031A RID: 794 RVA: 0x000079F4 File Offset: 0x00005BF4
			private void Dispose(bool disposing)
			{
				this.managedVtable.Release(this.comInterface);
				if (disposing)
				{
					this.comInterface = IntPtr.Zero;
					this.managedVtable = null;
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x0600031B RID: 795 RVA: 0x00007A28 File Offset: 0x00005C28
			internal static IntPtr GetUnderlyingInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					ComIStreamMarshaler.NativeToManagedWrapper nativeToManagedWrapper = (ComIStreamMarshaler.NativeToManagedWrapper)managedInterface;
					nativeToManagedWrapper.managedVtable.AddRef(nativeToManagedWrapper.comInterface);
					return nativeToManagedWrapper.comInterface;
				}
				return IntPtr.Zero;
			}

			// Token: 0x0600031C RID: 796 RVA: 0x00007A68 File Offset: 0x00005C68
			internal static IStream GetInterface(IntPtr comInterface, bool outParam)
			{
				if (comInterface == IntPtr.Zero)
				{
					return null;
				}
				return ComIStreamMarshaler.ManagedToNativeWrapper.GetUnderlyingInterface(comInterface, outParam) ?? new ComIStreamMarshaler.NativeToManagedWrapper(comInterface, outParam);
			}

			// Token: 0x0600031D RID: 797 RVA: 0x00007A98 File Offset: 0x00005C98
			internal static void ReleaseInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					((ComIStreamMarshaler.NativeToManagedWrapper)managedInterface).Dispose(true);
				}
			}

			// Token: 0x0600031E RID: 798 RVA: 0x00007AAE File Offset: 0x00005CAE
			private static void ThrowExceptionForHR(int result)
			{
				if (result < 0)
				{
					throw new COMException(null, result);
				}
			}

			// Token: 0x0600031F RID: 799 RVA: 0x00007ABC File Offset: 0x00005CBC
			public void Read(byte[] pv, int cb, IntPtr pcbRead)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Read(this.comInterface, pv, cb, pcbRead));
			}

			// Token: 0x06000320 RID: 800 RVA: 0x00007ADC File Offset: 0x00005CDC
			public void Write(byte[] pv, int cb, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Write(this.comInterface, pv, cb, pcbWritten));
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00007AFC File Offset: 0x00005CFC
			public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Seek(this.comInterface, dlibMove, dwOrigin, plibNewPosition));
			}

			// Token: 0x06000322 RID: 802 RVA: 0x00007B1C File Offset: 0x00005D1C
			public void SetSize(long libNewSize)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.SetSize(this.comInterface, libNewSize));
			}

			// Token: 0x06000323 RID: 803 RVA: 0x00007B3A File Offset: 0x00005D3A
			public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.CopyTo(this.comInterface, pstm, cb, pcbRead, pcbWritten));
			}

			// Token: 0x06000324 RID: 804 RVA: 0x00007B5C File Offset: 0x00005D5C
			public void Commit(int grfCommitFlags)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Commit(this.comInterface, grfCommitFlags));
			}

			// Token: 0x06000325 RID: 805 RVA: 0x00007B7A File Offset: 0x00005D7A
			public void Revert()
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Revert(this.comInterface));
			}

			// Token: 0x06000326 RID: 806 RVA: 0x00007B97 File Offset: 0x00005D97
			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.LockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x06000327 RID: 807 RVA: 0x00007BB7 File Offset: 0x00005DB7
			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.UnlockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x06000328 RID: 808 RVA: 0x00007BD7 File Offset: 0x00005DD7
			public void Stat(out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Stat(this.comInterface, out pstatstg, grfStatFlag));
			}

			// Token: 0x06000329 RID: 809 RVA: 0x00007BF8 File Offset: 0x00005DF8
			public void Clone(out IStream ppstm)
			{
				IntPtr intPtr;
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Clone(this.comInterface, out intPtr));
				ppstm = ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(intPtr, true);
			}

			// Token: 0x0400037C RID: 892
			private IntPtr comInterface;

			// Token: 0x0400037D RID: 893
			private ComIStreamMarshaler.IStreamVtbl managedVtable;
		}
	}
}
