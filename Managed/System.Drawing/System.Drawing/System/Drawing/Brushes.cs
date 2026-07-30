using System;

namespace System.Drawing
{
	/// <summary>Brushes for all the standard colors. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003E RID: 62
	public sealed class Brushes
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00002050 File Offset: 0x00000250
		private Brushes()
		{
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000047FF File Offset: 0x000029FF
		public static Brush AliceBlue
		{
			get
			{
				if (Brushes.aliceBlue == null)
				{
					Brushes.aliceBlue = new SolidBrush(Color.AliceBlue);
				}
				return Brushes.aliceBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000481C File Offset: 0x00002A1C
		public static Brush AntiqueWhite
		{
			get
			{
				if (Brushes.antiqueWhite == null)
				{
					Brushes.antiqueWhite = new SolidBrush(Color.AntiqueWhite);
				}
				return Brushes.antiqueWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004839 File Offset: 0x00002A39
		public static Brush Aqua
		{
			get
			{
				if (Brushes.aqua == null)
				{
					Brushes.aqua = new SolidBrush(Color.Aqua);
				}
				return Brushes.aqua;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004856 File Offset: 0x00002A56
		public static Brush Aquamarine
		{
			get
			{
				if (Brushes.aquamarine == null)
				{
					Brushes.aquamarine = new SolidBrush(Color.Aquamarine);
				}
				return Brushes.aquamarine;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004873 File Offset: 0x00002A73
		public static Brush Azure
		{
			get
			{
				if (Brushes.azure == null)
				{
					Brushes.azure = new SolidBrush(Color.Azure);
				}
				return Brushes.azure;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004890 File Offset: 0x00002A90
		public static Brush Beige
		{
			get
			{
				if (Brushes.beige == null)
				{
					Brushes.beige = new SolidBrush(Color.Beige);
				}
				return Brushes.beige;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000048AD File Offset: 0x00002AAD
		public static Brush Bisque
		{
			get
			{
				if (Brushes.bisque == null)
				{
					Brushes.bisque = new SolidBrush(Color.Bisque);
				}
				return Brushes.bisque;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000048CA File Offset: 0x00002ACA
		public static Brush Black
		{
			get
			{
				if (Brushes.black == null)
				{
					Brushes.black = new SolidBrush(Color.Black);
				}
				return Brushes.black;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000048E7 File Offset: 0x00002AE7
		public static Brush BlanchedAlmond
		{
			get
			{
				if (Brushes.blanchedAlmond == null)
				{
					Brushes.blanchedAlmond = new SolidBrush(Color.BlanchedAlmond);
				}
				return Brushes.blanchedAlmond;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004904 File Offset: 0x00002B04
		public static Brush Blue
		{
			get
			{
				if (Brushes.blue == null)
				{
					Brushes.blue = new SolidBrush(Color.Blue);
				}
				return Brushes.blue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004921 File Offset: 0x00002B21
		public static Brush BlueViolet
		{
			get
			{
				if (Brushes.blueViolet == null)
				{
					Brushes.blueViolet = new SolidBrush(Color.BlueViolet);
				}
				return Brushes.blueViolet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000493E File Offset: 0x00002B3E
		public static Brush Brown
		{
			get
			{
				if (Brushes.brown == null)
				{
					Brushes.brown = new SolidBrush(Color.Brown);
				}
				return Brushes.brown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000495B File Offset: 0x00002B5B
		public static Brush BurlyWood
		{
			get
			{
				if (Brushes.burlyWood == null)
				{
					Brushes.burlyWood = new SolidBrush(Color.BurlyWood);
				}
				return Brushes.burlyWood;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004978 File Offset: 0x00002B78
		public static Brush CadetBlue
		{
			get
			{
				if (Brushes.cadetBlue == null)
				{
					Brushes.cadetBlue = new SolidBrush(Color.CadetBlue);
				}
				return Brushes.cadetBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004995 File Offset: 0x00002B95
		public static Brush Chartreuse
		{
			get
			{
				if (Brushes.chartreuse == null)
				{
					Brushes.chartreuse = new SolidBrush(Color.Chartreuse);
				}
				return Brushes.chartreuse;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000049B2 File Offset: 0x00002BB2
		public static Brush Chocolate
		{
			get
			{
				if (Brushes.chocolate == null)
				{
					Brushes.chocolate = new SolidBrush(Color.Chocolate);
				}
				return Brushes.chocolate;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000049CF File Offset: 0x00002BCF
		public static Brush Coral
		{
			get
			{
				if (Brushes.coral == null)
				{
					Brushes.coral = new SolidBrush(Color.Coral);
				}
				return Brushes.coral;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000049EC File Offset: 0x00002BEC
		public static Brush CornflowerBlue
		{
			get
			{
				if (Brushes.cornflowerBlue == null)
				{
					Brushes.cornflowerBlue = new SolidBrush(Color.CornflowerBlue);
				}
				return Brushes.cornflowerBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004A09 File Offset: 0x00002C09
		public static Brush Cornsilk
		{
			get
			{
				if (Brushes.cornsilk == null)
				{
					Brushes.cornsilk = new SolidBrush(Color.Cornsilk);
				}
				return Brushes.cornsilk;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004A26 File Offset: 0x00002C26
		public static Brush Crimson
		{
			get
			{
				if (Brushes.crimson == null)
				{
					Brushes.crimson = new SolidBrush(Color.Crimson);
				}
				return Brushes.crimson;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004A43 File Offset: 0x00002C43
		public static Brush Cyan
		{
			get
			{
				if (Brushes.cyan == null)
				{
					Brushes.cyan = new SolidBrush(Color.Cyan);
				}
				return Brushes.cyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004A60 File Offset: 0x00002C60
		public static Brush DarkBlue
		{
			get
			{
				if (Brushes.darkBlue == null)
				{
					Brushes.darkBlue = new SolidBrush(Color.DarkBlue);
				}
				return Brushes.darkBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004A7D File Offset: 0x00002C7D
		public static Brush DarkCyan
		{
			get
			{
				if (Brushes.darkCyan == null)
				{
					Brushes.darkCyan = new SolidBrush(Color.DarkCyan);
				}
				return Brushes.darkCyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00004A9A File Offset: 0x00002C9A
		public static Brush DarkGoldenrod
		{
			get
			{
				if (Brushes.darkGoldenrod == null)
				{
					Brushes.darkGoldenrod = new SolidBrush(Color.DarkGoldenrod);
				}
				return Brushes.darkGoldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00004AB7 File Offset: 0x00002CB7
		public static Brush DarkGray
		{
			get
			{
				if (Brushes.darkGray == null)
				{
					Brushes.darkGray = new SolidBrush(Color.DarkGray);
				}
				return Brushes.darkGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static Brush DarkGreen
		{
			get
			{
				if (Brushes.darkGreen == null)
				{
					Brushes.darkGreen = new SolidBrush(Color.DarkGreen);
				}
				return Brushes.darkGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00004AF1 File Offset: 0x00002CF1
		public static Brush DarkKhaki
		{
			get
			{
				if (Brushes.darkKhaki == null)
				{
					Brushes.darkKhaki = new SolidBrush(Color.DarkKhaki);
				}
				return Brushes.darkKhaki;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00004B0E File Offset: 0x00002D0E
		public static Brush DarkMagenta
		{
			get
			{
				if (Brushes.darkMagenta == null)
				{
					Brushes.darkMagenta = new SolidBrush(Color.DarkMagenta);
				}
				return Brushes.darkMagenta;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00004B2B File Offset: 0x00002D2B
		public static Brush DarkOliveGreen
		{
			get
			{
				if (Brushes.darkOliveGreen == null)
				{
					Brushes.darkOliveGreen = new SolidBrush(Color.DarkOliveGreen);
				}
				return Brushes.darkOliveGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00004B48 File Offset: 0x00002D48
		public static Brush DarkOrange
		{
			get
			{
				if (Brushes.darkOrange == null)
				{
					Brushes.darkOrange = new SolidBrush(Color.DarkOrange);
				}
				return Brushes.darkOrange;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00004B65 File Offset: 0x00002D65
		public static Brush DarkOrchid
		{
			get
			{
				if (Brushes.darkOrchid == null)
				{
					Brushes.darkOrchid = new SolidBrush(Color.DarkOrchid);
				}
				return Brushes.darkOrchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00004B82 File Offset: 0x00002D82
		public static Brush DarkRed
		{
			get
			{
				if (Brushes.darkRed == null)
				{
					Brushes.darkRed = new SolidBrush(Color.DarkRed);
				}
				return Brushes.darkRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00004B9F File Offset: 0x00002D9F
		public static Brush DarkSalmon
		{
			get
			{
				if (Brushes.darkSalmon == null)
				{
					Brushes.darkSalmon = new SolidBrush(Color.DarkSalmon);
				}
				return Brushes.darkSalmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004BBC File Offset: 0x00002DBC
		public static Brush DarkSeaGreen
		{
			get
			{
				if (Brushes.darkSeaGreen == null)
				{
					Brushes.darkSeaGreen = new SolidBrush(Color.DarkSeaGreen);
				}
				return Brushes.darkSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public static Brush DarkSlateBlue
		{
			get
			{
				if (Brushes.darkSlateBlue == null)
				{
					Brushes.darkSlateBlue = new SolidBrush(Color.DarkSlateBlue);
				}
				return Brushes.darkSlateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004BF6 File Offset: 0x00002DF6
		public static Brush DarkSlateGray
		{
			get
			{
				if (Brushes.darkSlateGray == null)
				{
					Brushes.darkSlateGray = new SolidBrush(Color.DarkSlateGray);
				}
				return Brushes.darkSlateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004C13 File Offset: 0x00002E13
		public static Brush DarkTurquoise
		{
			get
			{
				if (Brushes.darkTurquoise == null)
				{
					Brushes.darkTurquoise = new SolidBrush(Color.DarkTurquoise);
				}
				return Brushes.darkTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00004C30 File Offset: 0x00002E30
		public static Brush DarkViolet
		{
			get
			{
				if (Brushes.darkViolet == null)
				{
					Brushes.darkViolet = new SolidBrush(Color.DarkViolet);
				}
				return Brushes.darkViolet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004C4D File Offset: 0x00002E4D
		public static Brush DeepPink
		{
			get
			{
				if (Brushes.deepPink == null)
				{
					Brushes.deepPink = new SolidBrush(Color.DeepPink);
				}
				return Brushes.deepPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00004C6A File Offset: 0x00002E6A
		public static Brush DeepSkyBlue
		{
			get
			{
				if (Brushes.deepSkyBlue == null)
				{
					Brushes.deepSkyBlue = new SolidBrush(Color.DeepSkyBlue);
				}
				return Brushes.deepSkyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004C87 File Offset: 0x00002E87
		public static Brush DimGray
		{
			get
			{
				if (Brushes.dimGray == null)
				{
					Brushes.dimGray = new SolidBrush(Color.DimGray);
				}
				return Brushes.dimGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public static Brush DodgerBlue
		{
			get
			{
				if (Brushes.dodgerBlue == null)
				{
					Brushes.dodgerBlue = new SolidBrush(Color.DodgerBlue);
				}
				return Brushes.dodgerBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004CC1 File Offset: 0x00002EC1
		public static Brush Firebrick
		{
			get
			{
				if (Brushes.firebrick == null)
				{
					Brushes.firebrick = new SolidBrush(Color.Firebrick);
				}
				return Brushes.firebrick;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004CDE File Offset: 0x00002EDE
		public static Brush FloralWhite
		{
			get
			{
				if (Brushes.floralWhite == null)
				{
					Brushes.floralWhite = new SolidBrush(Color.FloralWhite);
				}
				return Brushes.floralWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00004CFB File Offset: 0x00002EFB
		public static Brush ForestGreen
		{
			get
			{
				if (Brushes.forestGreen == null)
				{
					Brushes.forestGreen = new SolidBrush(Color.ForestGreen);
				}
				return Brushes.forestGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004D18 File Offset: 0x00002F18
		public static Brush Fuchsia
		{
			get
			{
				if (Brushes.fuchsia == null)
				{
					Brushes.fuchsia = new SolidBrush(Color.Fuchsia);
				}
				return Brushes.fuchsia;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004D35 File Offset: 0x00002F35
		public static Brush Gainsboro
		{
			get
			{
				if (Brushes.gainsboro == null)
				{
					Brushes.gainsboro = new SolidBrush(Color.Gainsboro);
				}
				return Brushes.gainsboro;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004D52 File Offset: 0x00002F52
		public static Brush GhostWhite
		{
			get
			{
				if (Brushes.ghostWhite == null)
				{
					Brushes.ghostWhite = new SolidBrush(Color.GhostWhite);
				}
				return Brushes.ghostWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00004D6F File Offset: 0x00002F6F
		public static Brush Gold
		{
			get
			{
				if (Brushes.gold == null)
				{
					Brushes.gold = new SolidBrush(Color.Gold);
				}
				return Brushes.gold;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004D8C File Offset: 0x00002F8C
		public static Brush Goldenrod
		{
			get
			{
				if (Brushes.goldenrod == null)
				{
					Brushes.goldenrod = new SolidBrush(Color.Goldenrod);
				}
				return Brushes.goldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00004DA9 File Offset: 0x00002FA9
		public static Brush Gray
		{
			get
			{
				if (Brushes.gray == null)
				{
					Brushes.gray = new SolidBrush(Color.Gray);
				}
				return Brushes.gray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public static Brush Green
		{
			get
			{
				if (Brushes.green == null)
				{
					Brushes.green = new SolidBrush(Color.Green);
				}
				return Brushes.green;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public static Brush GreenYellow
		{
			get
			{
				if (Brushes.greenYellow == null)
				{
					Brushes.greenYellow = new SolidBrush(Color.GreenYellow);
				}
				return Brushes.greenYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004E00 File Offset: 0x00003000
		public static Brush Honeydew
		{
			get
			{
				if (Brushes.honeydew == null)
				{
					Brushes.honeydew = new SolidBrush(Color.Honeydew);
				}
				return Brushes.honeydew;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004E1D File Offset: 0x0000301D
		public static Brush HotPink
		{
			get
			{
				if (Brushes.hotPink == null)
				{
					Brushes.hotPink = new SolidBrush(Color.HotPink);
				}
				return Brushes.hotPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004E3A File Offset: 0x0000303A
		public static Brush IndianRed
		{
			get
			{
				if (Brushes.indianRed == null)
				{
					Brushes.indianRed = new SolidBrush(Color.IndianRed);
				}
				return Brushes.indianRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004E57 File Offset: 0x00003057
		public static Brush Indigo
		{
			get
			{
				if (Brushes.indigo == null)
				{
					Brushes.indigo = new SolidBrush(Color.Indigo);
				}
				return Brushes.indigo;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004E74 File Offset: 0x00003074
		public static Brush Ivory
		{
			get
			{
				if (Brushes.ivory == null)
				{
					Brushes.ivory = new SolidBrush(Color.Ivory);
				}
				return Brushes.ivory;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004E91 File Offset: 0x00003091
		public static Brush Khaki
		{
			get
			{
				if (Brushes.khaki == null)
				{
					Brushes.khaki = new SolidBrush(Color.Khaki);
				}
				return Brushes.khaki;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004EAE File Offset: 0x000030AE
		public static Brush Lavender
		{
			get
			{
				if (Brushes.lavender == null)
				{
					Brushes.lavender = new SolidBrush(Color.Lavender);
				}
				return Brushes.lavender;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004ECB File Offset: 0x000030CB
		public static Brush LavenderBlush
		{
			get
			{
				if (Brushes.lavenderBlush == null)
				{
					Brushes.lavenderBlush = new SolidBrush(Color.LavenderBlush);
				}
				return Brushes.lavenderBlush;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004EE8 File Offset: 0x000030E8
		public static Brush LawnGreen
		{
			get
			{
				if (Brushes.lawnGreen == null)
				{
					Brushes.lawnGreen = new SolidBrush(Color.LawnGreen);
				}
				return Brushes.lawnGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004F05 File Offset: 0x00003105
		public static Brush LemonChiffon
		{
			get
			{
				if (Brushes.lemonChiffon == null)
				{
					Brushes.lemonChiffon = new SolidBrush(Color.LemonChiffon);
				}
				return Brushes.lemonChiffon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004F22 File Offset: 0x00003122
		public static Brush LightBlue
		{
			get
			{
				if (Brushes.lightBlue == null)
				{
					Brushes.lightBlue = new SolidBrush(Color.LightBlue);
				}
				return Brushes.lightBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00004F3F File Offset: 0x0000313F
		public static Brush LightCoral
		{
			get
			{
				if (Brushes.lightCoral == null)
				{
					Brushes.lightCoral = new SolidBrush(Color.LightCoral);
				}
				return Brushes.lightCoral;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004F5C File Offset: 0x0000315C
		public static Brush LightCyan
		{
			get
			{
				if (Brushes.lightCyan == null)
				{
					Brushes.lightCyan = new SolidBrush(Color.LightCyan);
				}
				return Brushes.lightCyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00004F79 File Offset: 0x00003179
		public static Brush LightGoldenrodYellow
		{
			get
			{
				if (Brushes.lightGoldenrodYellow == null)
				{
					Brushes.lightGoldenrodYellow = new SolidBrush(Color.LightGoldenrodYellow);
				}
				return Brushes.lightGoldenrodYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004F96 File Offset: 0x00003196
		public static Brush LightGray
		{
			get
			{
				if (Brushes.lightGray == null)
				{
					Brushes.lightGray = new SolidBrush(Color.LightGray);
				}
				return Brushes.lightGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004FB3 File Offset: 0x000031B3
		public static Brush LightGreen
		{
			get
			{
				if (Brushes.lightGreen == null)
				{
					Brushes.lightGreen = new SolidBrush(Color.LightGreen);
				}
				return Brushes.lightGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004FD0 File Offset: 0x000031D0
		public static Brush LightPink
		{
			get
			{
				if (Brushes.lightPink == null)
				{
					Brushes.lightPink = new SolidBrush(Color.LightPink);
				}
				return Brushes.lightPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004FED File Offset: 0x000031ED
		public static Brush LightSalmon
		{
			get
			{
				if (Brushes.lightSalmon == null)
				{
					Brushes.lightSalmon = new SolidBrush(Color.LightSalmon);
				}
				return Brushes.lightSalmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000500A File Offset: 0x0000320A
		public static Brush LightSeaGreen
		{
			get
			{
				if (Brushes.lightSeaGreen == null)
				{
					Brushes.lightSeaGreen = new SolidBrush(Color.LightSeaGreen);
				}
				return Brushes.lightSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005027 File Offset: 0x00003227
		public static Brush LightSkyBlue
		{
			get
			{
				if (Brushes.lightSkyBlue == null)
				{
					Brushes.lightSkyBlue = new SolidBrush(Color.LightSkyBlue);
				}
				return Brushes.lightSkyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005044 File Offset: 0x00003244
		public static Brush LightSlateGray
		{
			get
			{
				if (Brushes.lightSlateGray == null)
				{
					Brushes.lightSlateGray = new SolidBrush(Color.LightSlateGray);
				}
				return Brushes.lightSlateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005061 File Offset: 0x00003261
		public static Brush LightSteelBlue
		{
			get
			{
				if (Brushes.lightSteelBlue == null)
				{
					Brushes.lightSteelBlue = new SolidBrush(Color.LightSteelBlue);
				}
				return Brushes.lightSteelBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000507E File Offset: 0x0000327E
		public static Brush LightYellow
		{
			get
			{
				if (Brushes.lightYellow == null)
				{
					Brushes.lightYellow = new SolidBrush(Color.LightYellow);
				}
				return Brushes.lightYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000509B File Offset: 0x0000329B
		public static Brush Lime
		{
			get
			{
				if (Brushes.lime == null)
				{
					Brushes.lime = new SolidBrush(Color.Lime);
				}
				return Brushes.lime;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000050B8 File Offset: 0x000032B8
		public static Brush LimeGreen
		{
			get
			{
				if (Brushes.limeGreen == null)
				{
					Brushes.limeGreen = new SolidBrush(Color.LimeGreen);
				}
				return Brushes.limeGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000050D5 File Offset: 0x000032D5
		public static Brush Linen
		{
			get
			{
				if (Brushes.linen == null)
				{
					Brushes.linen = new SolidBrush(Color.Linen);
				}
				return Brushes.linen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000050F2 File Offset: 0x000032F2
		public static Brush Magenta
		{
			get
			{
				if (Brushes.magenta == null)
				{
					Brushes.magenta = new SolidBrush(Color.Magenta);
				}
				return Brushes.magenta;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000510F File Offset: 0x0000330F
		public static Brush Maroon
		{
			get
			{
				if (Brushes.maroon == null)
				{
					Brushes.maroon = new SolidBrush(Color.Maroon);
				}
				return Brushes.maroon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000512C File Offset: 0x0000332C
		public static Brush MediumAquamarine
		{
			get
			{
				if (Brushes.mediumAquamarine == null)
				{
					Brushes.mediumAquamarine = new SolidBrush(Color.MediumAquamarine);
				}
				return Brushes.mediumAquamarine;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005149 File Offset: 0x00003349
		public static Brush MediumBlue
		{
			get
			{
				if (Brushes.mediumBlue == null)
				{
					Brushes.mediumBlue = new SolidBrush(Color.MediumBlue);
				}
				return Brushes.mediumBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005166 File Offset: 0x00003366
		public static Brush MediumOrchid
		{
			get
			{
				if (Brushes.mediumOrchid == null)
				{
					Brushes.mediumOrchid = new SolidBrush(Color.MediumOrchid);
				}
				return Brushes.mediumOrchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005183 File Offset: 0x00003383
		public static Brush MediumPurple
		{
			get
			{
				if (Brushes.mediumPurple == null)
				{
					Brushes.mediumPurple = new SolidBrush(Color.MediumPurple);
				}
				return Brushes.mediumPurple;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000051A0 File Offset: 0x000033A0
		public static Brush MediumSeaGreen
		{
			get
			{
				if (Brushes.mediumSeaGreen == null)
				{
					Brushes.mediumSeaGreen = new SolidBrush(Color.MediumSeaGreen);
				}
				return Brushes.mediumSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000051BD File Offset: 0x000033BD
		public static Brush MediumSlateBlue
		{
			get
			{
				if (Brushes.mediumSlateBlue == null)
				{
					Brushes.mediumSlateBlue = new SolidBrush(Color.MediumSlateBlue);
				}
				return Brushes.mediumSlateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000051DA File Offset: 0x000033DA
		public static Brush MediumSpringGreen
		{
			get
			{
				if (Brushes.mediumSpringGreen == null)
				{
					Brushes.mediumSpringGreen = new SolidBrush(Color.MediumSpringGreen);
				}
				return Brushes.mediumSpringGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000051F7 File Offset: 0x000033F7
		public static Brush MediumTurquoise
		{
			get
			{
				if (Brushes.mediumTurquoise == null)
				{
					Brushes.mediumTurquoise = new SolidBrush(Color.MediumTurquoise);
				}
				return Brushes.mediumTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005214 File Offset: 0x00003414
		public static Brush MediumVioletRed
		{
			get
			{
				if (Brushes.mediumVioletRed == null)
				{
					Brushes.mediumVioletRed = new SolidBrush(Color.MediumVioletRed);
				}
				return Brushes.mediumVioletRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005231 File Offset: 0x00003431
		public static Brush MidnightBlue
		{
			get
			{
				if (Brushes.midnightBlue == null)
				{
					Brushes.midnightBlue = new SolidBrush(Color.MidnightBlue);
				}
				return Brushes.midnightBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000524E File Offset: 0x0000344E
		public static Brush MintCream
		{
			get
			{
				if (Brushes.mintCream == null)
				{
					Brushes.mintCream = new SolidBrush(Color.MintCream);
				}
				return Brushes.mintCream;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000526B File Offset: 0x0000346B
		public static Brush MistyRose
		{
			get
			{
				if (Brushes.mistyRose == null)
				{
					Brushes.mistyRose = new SolidBrush(Color.MistyRose);
				}
				return Brushes.mistyRose;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005288 File Offset: 0x00003488
		public static Brush Moccasin
		{
			get
			{
				if (Brushes.moccasin == null)
				{
					Brushes.moccasin = new SolidBrush(Color.Moccasin);
				}
				return Brushes.moccasin;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000052A5 File Offset: 0x000034A5
		public static Brush NavajoWhite
		{
			get
			{
				if (Brushes.navajoWhite == null)
				{
					Brushes.navajoWhite = new SolidBrush(Color.NavajoWhite);
				}
				return Brushes.navajoWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000052C2 File Offset: 0x000034C2
		public static Brush Navy
		{
			get
			{
				if (Brushes.navy == null)
				{
					Brushes.navy = new SolidBrush(Color.Navy);
				}
				return Brushes.navy;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000052DF File Offset: 0x000034DF
		public static Brush OldLace
		{
			get
			{
				if (Brushes.oldLace == null)
				{
					Brushes.oldLace = new SolidBrush(Color.OldLace);
				}
				return Brushes.oldLace;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000052FC File Offset: 0x000034FC
		public static Brush Olive
		{
			get
			{
				if (Brushes.olive == null)
				{
					Brushes.olive = new SolidBrush(Color.Olive);
				}
				return Brushes.olive;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005319 File Offset: 0x00003519
		public static Brush OliveDrab
		{
			get
			{
				if (Brushes.oliveDrab == null)
				{
					Brushes.oliveDrab = new SolidBrush(Color.OliveDrab);
				}
				return Brushes.oliveDrab;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005336 File Offset: 0x00003536
		public static Brush Orange
		{
			get
			{
				if (Brushes.orange == null)
				{
					Brushes.orange = new SolidBrush(Color.Orange);
				}
				return Brushes.orange;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005353 File Offset: 0x00003553
		public static Brush OrangeRed
		{
			get
			{
				if (Brushes.orangeRed == null)
				{
					Brushes.orangeRed = new SolidBrush(Color.OrangeRed);
				}
				return Brushes.orangeRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005370 File Offset: 0x00003570
		public static Brush Orchid
		{
			get
			{
				if (Brushes.orchid == null)
				{
					Brushes.orchid = new SolidBrush(Color.Orchid);
				}
				return Brushes.orchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000538D File Offset: 0x0000358D
		public static Brush PaleGoldenrod
		{
			get
			{
				if (Brushes.paleGoldenrod == null)
				{
					Brushes.paleGoldenrod = new SolidBrush(Color.PaleGoldenrod);
				}
				return Brushes.paleGoldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000053AA File Offset: 0x000035AA
		public static Brush PaleGreen
		{
			get
			{
				if (Brushes.paleGreen == null)
				{
					Brushes.paleGreen = new SolidBrush(Color.PaleGreen);
				}
				return Brushes.paleGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000053C7 File Offset: 0x000035C7
		public static Brush PaleTurquoise
		{
			get
			{
				if (Brushes.paleTurquoise == null)
				{
					Brushes.paleTurquoise = new SolidBrush(Color.PaleTurquoise);
				}
				return Brushes.paleTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000053E4 File Offset: 0x000035E4
		public static Brush PaleVioletRed
		{
			get
			{
				if (Brushes.paleVioletRed == null)
				{
					Brushes.paleVioletRed = new SolidBrush(Color.PaleVioletRed);
				}
				return Brushes.paleVioletRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005401 File Offset: 0x00003601
		public static Brush PapayaWhip
		{
			get
			{
				if (Brushes.papayaWhip == null)
				{
					Brushes.papayaWhip = new SolidBrush(Color.PapayaWhip);
				}
				return Brushes.papayaWhip;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000541E File Offset: 0x0000361E
		public static Brush PeachPuff
		{
			get
			{
				if (Brushes.peachPuff == null)
				{
					Brushes.peachPuff = new SolidBrush(Color.PeachPuff);
				}
				return Brushes.peachPuff;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000543B File Offset: 0x0000363B
		public static Brush Peru
		{
			get
			{
				if (Brushes.peru == null)
				{
					Brushes.peru = new SolidBrush(Color.Peru);
				}
				return Brushes.peru;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005458 File Offset: 0x00003658
		public static Brush Pink
		{
			get
			{
				if (Brushes.pink == null)
				{
					Brushes.pink = new SolidBrush(Color.Pink);
				}
				return Brushes.pink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005475 File Offset: 0x00003675
		public static Brush Plum
		{
			get
			{
				if (Brushes.plum == null)
				{
					Brushes.plum = new SolidBrush(Color.Plum);
				}
				return Brushes.plum;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005492 File Offset: 0x00003692
		public static Brush PowderBlue
		{
			get
			{
				if (Brushes.powderBlue == null)
				{
					Brushes.powderBlue = new SolidBrush(Color.PowderBlue);
				}
				return Brushes.powderBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000054AF File Offset: 0x000036AF
		public static Brush Purple
		{
			get
			{
				if (Brushes.purple == null)
				{
					Brushes.purple = new SolidBrush(Color.Purple);
				}
				return Brushes.purple;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000054CC File Offset: 0x000036CC
		public static Brush Red
		{
			get
			{
				if (Brushes.red == null)
				{
					Brushes.red = new SolidBrush(Color.Red);
				}
				return Brushes.red;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000054E9 File Offset: 0x000036E9
		public static Brush RosyBrown
		{
			get
			{
				if (Brushes.rosyBrown == null)
				{
					Brushes.rosyBrown = new SolidBrush(Color.RosyBrown);
				}
				return Brushes.rosyBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005506 File Offset: 0x00003706
		public static Brush RoyalBlue
		{
			get
			{
				if (Brushes.royalBlue == null)
				{
					Brushes.royalBlue = new SolidBrush(Color.RoyalBlue);
				}
				return Brushes.royalBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005523 File Offset: 0x00003723
		public static Brush SaddleBrown
		{
			get
			{
				if (Brushes.saddleBrown == null)
				{
					Brushes.saddleBrown = new SolidBrush(Color.SaddleBrown);
				}
				return Brushes.saddleBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005540 File Offset: 0x00003740
		public static Brush Salmon
		{
			get
			{
				if (Brushes.salmon == null)
				{
					Brushes.salmon = new SolidBrush(Color.Salmon);
				}
				return Brushes.salmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000555D File Offset: 0x0000375D
		public static Brush SandyBrown
		{
			get
			{
				if (Brushes.sandyBrown == null)
				{
					Brushes.sandyBrown = new SolidBrush(Color.SandyBrown);
				}
				return Brushes.sandyBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000557A File Offset: 0x0000377A
		public static Brush SeaGreen
		{
			get
			{
				if (Brushes.seaGreen == null)
				{
					Brushes.seaGreen = new SolidBrush(Color.SeaGreen);
				}
				return Brushes.seaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005597 File Offset: 0x00003797
		public static Brush SeaShell
		{
			get
			{
				if (Brushes.seaShell == null)
				{
					Brushes.seaShell = new SolidBrush(Color.SeaShell);
				}
				return Brushes.seaShell;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000055B4 File Offset: 0x000037B4
		public static Brush Sienna
		{
			get
			{
				if (Brushes.sienna == null)
				{
					Brushes.sienna = new SolidBrush(Color.Sienna);
				}
				return Brushes.sienna;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000055D1 File Offset: 0x000037D1
		public static Brush Silver
		{
			get
			{
				if (Brushes.silver == null)
				{
					Brushes.silver = new SolidBrush(Color.Silver);
				}
				return Brushes.silver;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000055EE File Offset: 0x000037EE
		public static Brush SkyBlue
		{
			get
			{
				if (Brushes.skyBlue == null)
				{
					Brushes.skyBlue = new SolidBrush(Color.SkyBlue);
				}
				return Brushes.skyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000560B File Offset: 0x0000380B
		public static Brush SlateBlue
		{
			get
			{
				if (Brushes.slateBlue == null)
				{
					Brushes.slateBlue = new SolidBrush(Color.SlateBlue);
				}
				return Brushes.slateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005628 File Offset: 0x00003828
		public static Brush SlateGray
		{
			get
			{
				if (Brushes.slateGray == null)
				{
					Brushes.slateGray = new SolidBrush(Color.SlateGray);
				}
				return Brushes.slateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00005645 File Offset: 0x00003845
		public static Brush Snow
		{
			get
			{
				if (Brushes.snow == null)
				{
					Brushes.snow = new SolidBrush(Color.Snow);
				}
				return Brushes.snow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005662 File Offset: 0x00003862
		public static Brush SpringGreen
		{
			get
			{
				if (Brushes.springGreen == null)
				{
					Brushes.springGreen = new SolidBrush(Color.SpringGreen);
				}
				return Brushes.springGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000567F File Offset: 0x0000387F
		public static Brush SteelBlue
		{
			get
			{
				if (Brushes.steelBlue == null)
				{
					Brushes.steelBlue = new SolidBrush(Color.SteelBlue);
				}
				return Brushes.steelBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000569C File Offset: 0x0000389C
		public static Brush Tan
		{
			get
			{
				if (Brushes.tan == null)
				{
					Brushes.tan = new SolidBrush(Color.Tan);
				}
				return Brushes.tan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000056B9 File Offset: 0x000038B9
		public static Brush Teal
		{
			get
			{
				if (Brushes.teal == null)
				{
					Brushes.teal = new SolidBrush(Color.Teal);
				}
				return Brushes.teal;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000056D6 File Offset: 0x000038D6
		public static Brush Thistle
		{
			get
			{
				if (Brushes.thistle == null)
				{
					Brushes.thistle = new SolidBrush(Color.Thistle);
				}
				return Brushes.thistle;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000056F3 File Offset: 0x000038F3
		public static Brush Tomato
		{
			get
			{
				if (Brushes.tomato == null)
				{
					Brushes.tomato = new SolidBrush(Color.Tomato);
				}
				return Brushes.tomato;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00005710 File Offset: 0x00003910
		public static Brush Transparent
		{
			get
			{
				if (Brushes.transparent == null)
				{
					Brushes.transparent = new SolidBrush(Color.Transparent);
				}
				return Brushes.transparent;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000572D File Offset: 0x0000392D
		public static Brush Turquoise
		{
			get
			{
				if (Brushes.turquoise == null)
				{
					Brushes.turquoise = new SolidBrush(Color.Turquoise);
				}
				return Brushes.turquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000574A File Offset: 0x0000394A
		public static Brush Violet
		{
			get
			{
				if (Brushes.violet == null)
				{
					Brushes.violet = new SolidBrush(Color.Violet);
				}
				return Brushes.violet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005767 File Offset: 0x00003967
		public static Brush Wheat
		{
			get
			{
				if (Brushes.wheat == null)
				{
					Brushes.wheat = new SolidBrush(Color.Wheat);
				}
				return Brushes.wheat;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005784 File Offset: 0x00003984
		public static Brush White
		{
			get
			{
				if (Brushes.white == null)
				{
					Brushes.white = new SolidBrush(Color.White);
				}
				return Brushes.white;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000057A1 File Offset: 0x000039A1
		public static Brush WhiteSmoke
		{
			get
			{
				if (Brushes.whiteSmoke == null)
				{
					Brushes.whiteSmoke = new SolidBrush(Color.WhiteSmoke);
				}
				return Brushes.whiteSmoke;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000057BE File Offset: 0x000039BE
		public static Brush Yellow
		{
			get
			{
				if (Brushes.yellow == null)
				{
					Brushes.yellow = new SolidBrush(Color.Yellow);
				}
				return Brushes.yellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000057DB File Offset: 0x000039DB
		public static Brush YellowGreen
		{
			get
			{
				if (Brushes.yellowGreen == null)
				{
					Brushes.yellowGreen = new SolidBrush(Color.YellowGreen);
				}
				return Brushes.yellowGreen;
			}
		}

		// Token: 0x040002BC RID: 700
		private static SolidBrush aliceBlue;

		// Token: 0x040002BD RID: 701
		private static SolidBrush antiqueWhite;

		// Token: 0x040002BE RID: 702
		private static SolidBrush aqua;

		// Token: 0x040002BF RID: 703
		private static SolidBrush aquamarine;

		// Token: 0x040002C0 RID: 704
		private static SolidBrush azure;

		// Token: 0x040002C1 RID: 705
		private static SolidBrush beige;

		// Token: 0x040002C2 RID: 706
		private static SolidBrush bisque;

		// Token: 0x040002C3 RID: 707
		private static SolidBrush black;

		// Token: 0x040002C4 RID: 708
		private static SolidBrush blanchedAlmond;

		// Token: 0x040002C5 RID: 709
		private static SolidBrush blue;

		// Token: 0x040002C6 RID: 710
		private static SolidBrush blueViolet;

		// Token: 0x040002C7 RID: 711
		private static SolidBrush brown;

		// Token: 0x040002C8 RID: 712
		private static SolidBrush burlyWood;

		// Token: 0x040002C9 RID: 713
		private static SolidBrush cadetBlue;

		// Token: 0x040002CA RID: 714
		private static SolidBrush chartreuse;

		// Token: 0x040002CB RID: 715
		private static SolidBrush chocolate;

		// Token: 0x040002CC RID: 716
		private static SolidBrush coral;

		// Token: 0x040002CD RID: 717
		private static SolidBrush cornflowerBlue;

		// Token: 0x040002CE RID: 718
		private static SolidBrush cornsilk;

		// Token: 0x040002CF RID: 719
		private static SolidBrush crimson;

		// Token: 0x040002D0 RID: 720
		private static SolidBrush cyan;

		// Token: 0x040002D1 RID: 721
		private static SolidBrush darkBlue;

		// Token: 0x040002D2 RID: 722
		private static SolidBrush darkCyan;

		// Token: 0x040002D3 RID: 723
		private static SolidBrush darkGoldenrod;

		// Token: 0x040002D4 RID: 724
		private static SolidBrush darkGray;

		// Token: 0x040002D5 RID: 725
		private static SolidBrush darkGreen;

		// Token: 0x040002D6 RID: 726
		private static SolidBrush darkKhaki;

		// Token: 0x040002D7 RID: 727
		private static SolidBrush darkMagenta;

		// Token: 0x040002D8 RID: 728
		private static SolidBrush darkOliveGreen;

		// Token: 0x040002D9 RID: 729
		private static SolidBrush darkOrange;

		// Token: 0x040002DA RID: 730
		private static SolidBrush darkOrchid;

		// Token: 0x040002DB RID: 731
		private static SolidBrush darkRed;

		// Token: 0x040002DC RID: 732
		private static SolidBrush darkSalmon;

		// Token: 0x040002DD RID: 733
		private static SolidBrush darkSeaGreen;

		// Token: 0x040002DE RID: 734
		private static SolidBrush darkSlateBlue;

		// Token: 0x040002DF RID: 735
		private static SolidBrush darkSlateGray;

		// Token: 0x040002E0 RID: 736
		private static SolidBrush darkTurquoise;

		// Token: 0x040002E1 RID: 737
		private static SolidBrush darkViolet;

		// Token: 0x040002E2 RID: 738
		private static SolidBrush deepPink;

		// Token: 0x040002E3 RID: 739
		private static SolidBrush deepSkyBlue;

		// Token: 0x040002E4 RID: 740
		private static SolidBrush dimGray;

		// Token: 0x040002E5 RID: 741
		private static SolidBrush dodgerBlue;

		// Token: 0x040002E6 RID: 742
		private static SolidBrush firebrick;

		// Token: 0x040002E7 RID: 743
		private static SolidBrush floralWhite;

		// Token: 0x040002E8 RID: 744
		private static SolidBrush forestGreen;

		// Token: 0x040002E9 RID: 745
		private static SolidBrush fuchsia;

		// Token: 0x040002EA RID: 746
		private static SolidBrush gainsboro;

		// Token: 0x040002EB RID: 747
		private static SolidBrush ghostWhite;

		// Token: 0x040002EC RID: 748
		private static SolidBrush gold;

		// Token: 0x040002ED RID: 749
		private static SolidBrush goldenrod;

		// Token: 0x040002EE RID: 750
		private static SolidBrush gray;

		// Token: 0x040002EF RID: 751
		private static SolidBrush green;

		// Token: 0x040002F0 RID: 752
		private static SolidBrush greenYellow;

		// Token: 0x040002F1 RID: 753
		private static SolidBrush honeydew;

		// Token: 0x040002F2 RID: 754
		private static SolidBrush hotPink;

		// Token: 0x040002F3 RID: 755
		private static SolidBrush indianRed;

		// Token: 0x040002F4 RID: 756
		private static SolidBrush indigo;

		// Token: 0x040002F5 RID: 757
		private static SolidBrush ivory;

		// Token: 0x040002F6 RID: 758
		private static SolidBrush khaki;

		// Token: 0x040002F7 RID: 759
		private static SolidBrush lavender;

		// Token: 0x040002F8 RID: 760
		private static SolidBrush lavenderBlush;

		// Token: 0x040002F9 RID: 761
		private static SolidBrush lawnGreen;

		// Token: 0x040002FA RID: 762
		private static SolidBrush lemonChiffon;

		// Token: 0x040002FB RID: 763
		private static SolidBrush lightBlue;

		// Token: 0x040002FC RID: 764
		private static SolidBrush lightCoral;

		// Token: 0x040002FD RID: 765
		private static SolidBrush lightCyan;

		// Token: 0x040002FE RID: 766
		private static SolidBrush lightGoldenrodYellow;

		// Token: 0x040002FF RID: 767
		private static SolidBrush lightGray;

		// Token: 0x04000300 RID: 768
		private static SolidBrush lightGreen;

		// Token: 0x04000301 RID: 769
		private static SolidBrush lightPink;

		// Token: 0x04000302 RID: 770
		private static SolidBrush lightSalmon;

		// Token: 0x04000303 RID: 771
		private static SolidBrush lightSeaGreen;

		// Token: 0x04000304 RID: 772
		private static SolidBrush lightSkyBlue;

		// Token: 0x04000305 RID: 773
		private static SolidBrush lightSlateGray;

		// Token: 0x04000306 RID: 774
		private static SolidBrush lightSteelBlue;

		// Token: 0x04000307 RID: 775
		private static SolidBrush lightYellow;

		// Token: 0x04000308 RID: 776
		private static SolidBrush lime;

		// Token: 0x04000309 RID: 777
		private static SolidBrush limeGreen;

		// Token: 0x0400030A RID: 778
		private static SolidBrush linen;

		// Token: 0x0400030B RID: 779
		private static SolidBrush magenta;

		// Token: 0x0400030C RID: 780
		private static SolidBrush maroon;

		// Token: 0x0400030D RID: 781
		private static SolidBrush mediumAquamarine;

		// Token: 0x0400030E RID: 782
		private static SolidBrush mediumBlue;

		// Token: 0x0400030F RID: 783
		private static SolidBrush mediumOrchid;

		// Token: 0x04000310 RID: 784
		private static SolidBrush mediumPurple;

		// Token: 0x04000311 RID: 785
		private static SolidBrush mediumSeaGreen;

		// Token: 0x04000312 RID: 786
		private static SolidBrush mediumSlateBlue;

		// Token: 0x04000313 RID: 787
		private static SolidBrush mediumSpringGreen;

		// Token: 0x04000314 RID: 788
		private static SolidBrush mediumTurquoise;

		// Token: 0x04000315 RID: 789
		private static SolidBrush mediumVioletRed;

		// Token: 0x04000316 RID: 790
		private static SolidBrush midnightBlue;

		// Token: 0x04000317 RID: 791
		private static SolidBrush mintCream;

		// Token: 0x04000318 RID: 792
		private static SolidBrush mistyRose;

		// Token: 0x04000319 RID: 793
		private static SolidBrush moccasin;

		// Token: 0x0400031A RID: 794
		private static SolidBrush navajoWhite;

		// Token: 0x0400031B RID: 795
		private static SolidBrush navy;

		// Token: 0x0400031C RID: 796
		private static SolidBrush oldLace;

		// Token: 0x0400031D RID: 797
		private static SolidBrush olive;

		// Token: 0x0400031E RID: 798
		private static SolidBrush oliveDrab;

		// Token: 0x0400031F RID: 799
		private static SolidBrush orange;

		// Token: 0x04000320 RID: 800
		private static SolidBrush orangeRed;

		// Token: 0x04000321 RID: 801
		private static SolidBrush orchid;

		// Token: 0x04000322 RID: 802
		private static SolidBrush paleGoldenrod;

		// Token: 0x04000323 RID: 803
		private static SolidBrush paleGreen;

		// Token: 0x04000324 RID: 804
		private static SolidBrush paleTurquoise;

		// Token: 0x04000325 RID: 805
		private static SolidBrush paleVioletRed;

		// Token: 0x04000326 RID: 806
		private static SolidBrush papayaWhip;

		// Token: 0x04000327 RID: 807
		private static SolidBrush peachPuff;

		// Token: 0x04000328 RID: 808
		private static SolidBrush peru;

		// Token: 0x04000329 RID: 809
		private static SolidBrush pink;

		// Token: 0x0400032A RID: 810
		private static SolidBrush plum;

		// Token: 0x0400032B RID: 811
		private static SolidBrush powderBlue;

		// Token: 0x0400032C RID: 812
		private static SolidBrush purple;

		// Token: 0x0400032D RID: 813
		private static SolidBrush red;

		// Token: 0x0400032E RID: 814
		private static SolidBrush rosyBrown;

		// Token: 0x0400032F RID: 815
		private static SolidBrush royalBlue;

		// Token: 0x04000330 RID: 816
		private static SolidBrush saddleBrown;

		// Token: 0x04000331 RID: 817
		private static SolidBrush salmon;

		// Token: 0x04000332 RID: 818
		private static SolidBrush sandyBrown;

		// Token: 0x04000333 RID: 819
		private static SolidBrush seaGreen;

		// Token: 0x04000334 RID: 820
		private static SolidBrush seaShell;

		// Token: 0x04000335 RID: 821
		private static SolidBrush sienna;

		// Token: 0x04000336 RID: 822
		private static SolidBrush silver;

		// Token: 0x04000337 RID: 823
		private static SolidBrush skyBlue;

		// Token: 0x04000338 RID: 824
		private static SolidBrush slateBlue;

		// Token: 0x04000339 RID: 825
		private static SolidBrush slateGray;

		// Token: 0x0400033A RID: 826
		private static SolidBrush snow;

		// Token: 0x0400033B RID: 827
		private static SolidBrush springGreen;

		// Token: 0x0400033C RID: 828
		private static SolidBrush steelBlue;

		// Token: 0x0400033D RID: 829
		private static SolidBrush tan;

		// Token: 0x0400033E RID: 830
		private static SolidBrush teal;

		// Token: 0x0400033F RID: 831
		private static SolidBrush thistle;

		// Token: 0x04000340 RID: 832
		private static SolidBrush tomato;

		// Token: 0x04000341 RID: 833
		private static SolidBrush transparent;

		// Token: 0x04000342 RID: 834
		private static SolidBrush turquoise;

		// Token: 0x04000343 RID: 835
		private static SolidBrush violet;

		// Token: 0x04000344 RID: 836
		private static SolidBrush wheat;

		// Token: 0x04000345 RID: 837
		private static SolidBrush white;

		// Token: 0x04000346 RID: 838
		private static SolidBrush whiteSmoke;

		// Token: 0x04000347 RID: 839
		private static SolidBrush yellow;

		// Token: 0x04000348 RID: 840
		private static SolidBrush yellowGreen;
	}
}
