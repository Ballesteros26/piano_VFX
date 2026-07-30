using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F9 RID: 249
	public struct Volume
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001A1EE File Offset: 0x000183EE
		public Volume(ushort volume)
		{
			this = new Volume(volume, volume);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001A1F8 File Offset: 0x000183F8
		public Volume(ushort leftVolume, ushort rightVolume)
		{
			this.LeftVolume = leftVolume;
			this.RightVolume = rightVolume;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001A208 File Offset: 0x00018408
		public ushort LeftVolume { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001A210 File Offset: 0x00018410
		public ushort RightVolume { get; }

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A218 File Offset: 0x00018418
		public static Volume Right(ushort volume)
		{
			return new Volume(0, volume);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001A221 File Offset: 0x00018421
		public static Volume Left(ushort volume)
		{
			return new Volume(volume, 0);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001A22A File Offset: 0x0001842A
		public override string ToString()
		{
			return string.Format("L {0} R {1}", this.LeftVolume, this.RightVolume);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001A24C File Offset: 0x0001844C
		public override bool Equals(object obj)
		{
			if (!(obj is Volume))
			{
				return false;
			}
			Volume volume = (Volume)obj;
			return volume.LeftVolume == this.LeftVolume && volume.RightVolume == this.RightVolume;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001A28C File Offset: 0x0001848C
		public override int GetHashCode()
		{
			return (17 * 23 + this.LeftVolume.GetHashCode()) * 23 + this.RightVolume.GetHashCode();
		}

		// Token: 0x040007D3 RID: 2003
		public static readonly Volume Zero = default(Volume);

		// Token: 0x040007D4 RID: 2004
		public static readonly Volume FullLeft = Volume.Left(ushort.MaxValue);

		// Token: 0x040007D5 RID: 2005
		public static readonly Volume FullRight = Volume.Right(ushort.MaxValue);
	}
}
