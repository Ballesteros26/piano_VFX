using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200065D RID: 1629
	internal struct StorageDescriptor
	{
		// Token: 0x06004195 RID: 16789 RVA: 0x0015F054 File Offset: 0x0015D254
		public static StorageDescriptor None()
		{
			return default(StorageDescriptor);
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x0015F06C File Offset: 0x0015D26C
		public static StorageDescriptor Stack(Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Stack,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x0015F09C File Offset: 0x0015D29C
		public static StorageDescriptor Parameter(int paramIndex, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Parameter,
				locationObject = paramIndex,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0015F0D8 File Offset: 0x0015D2D8
		public static StorageDescriptor Local(LocalBuilder loc, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Local,
				locationObject = loc,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0015F110 File Offset: 0x0015D310
		public static StorageDescriptor Current(LocalBuilder locIter, Type itemStorageType)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Current,
				locationObject = locIter,
				itemStorageType = itemStorageType
			};
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x0015F140 File Offset: 0x0015D340
		public static StorageDescriptor Global(MethodInfo methGlobal, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Global,
				locationObject = methGlobal,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x0015F176 File Offset: 0x0015D376
		public StorageDescriptor ToStack()
		{
			return StorageDescriptor.Stack(this.itemStorageType, this.isCached);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x0015F189 File Offset: 0x0015D389
		public StorageDescriptor ToLocal(LocalBuilder loc)
		{
			return StorageDescriptor.Local(loc, this.itemStorageType, this.isCached);
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x0015F1A0 File Offset: 0x0015D3A0
		public StorageDescriptor ToStorageType(Type itemStorageType)
		{
			StorageDescriptor storageDescriptor = this;
			storageDescriptor.itemStorageType = itemStorageType;
			return storageDescriptor;
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x0015F1BD File Offset: 0x0015D3BD
		public ItemLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x0015F1C5 File Offset: 0x0015D3C5
		public int ParameterLocation
		{
			get
			{
				return (int)this.locationObject;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x0015F1D2 File Offset: 0x0015D3D2
		public LocalBuilder LocalLocation
		{
			get
			{
				return this.locationObject as LocalBuilder;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x0015F1D2 File Offset: 0x0015D3D2
		public LocalBuilder CurrentLocation
		{
			get
			{
				return this.locationObject as LocalBuilder;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x0015F1DF File Offset: 0x0015D3DF
		public MethodInfo GlobalLocation
		{
			get
			{
				return this.locationObject as MethodInfo;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x0015F1EC File Offset: 0x0015D3EC
		public bool IsCached
		{
			get
			{
				return this.isCached;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x0015F1F4 File Offset: 0x0015D3F4
		public Type ItemStorageType
		{
			get
			{
				return this.itemStorageType;
			}
		}

		// Token: 0x04002A04 RID: 10756
		private ItemLocation location;

		// Token: 0x04002A05 RID: 10757
		private object locationObject;

		// Token: 0x04002A06 RID: 10758
		private Type itemStorageType;

		// Token: 0x04002A07 RID: 10759
		private bool isCached;
	}
}
