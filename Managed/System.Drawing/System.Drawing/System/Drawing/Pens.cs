using System;

namespace System.Drawing
{
	/// <summary>Pens for all the standard colors. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007B RID: 123
	public sealed class Pens
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00002050 File Offset: 0x00000250
		private Pens()
		{
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001072C File Offset: 0x0000E92C
		public static Pen AliceBlue
		{
			get
			{
				if (Pens.aliceblue == null)
				{
					Pens.aliceblue = new Pen(Color.AliceBlue);
					Pens.aliceblue.isModifiable = false;
				}
				return Pens.aliceblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00010754 File Offset: 0x0000E954
		public static Pen AntiqueWhite
		{
			get
			{
				if (Pens.antiquewhite == null)
				{
					Pens.antiquewhite = new Pen(Color.AntiqueWhite);
					Pens.antiquewhite.isModifiable = false;
				}
				return Pens.antiquewhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001077C File Offset: 0x0000E97C
		public static Pen Aqua
		{
			get
			{
				if (Pens.aqua == null)
				{
					Pens.aqua = new Pen(Color.Aqua);
					Pens.aqua.isModifiable = false;
				}
				return Pens.aqua;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public static Pen Aquamarine
		{
			get
			{
				if (Pens.aquamarine == null)
				{
					Pens.aquamarine = new Pen(Color.Aquamarine);
					Pens.aquamarine.isModifiable = false;
				}
				return Pens.aquamarine;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000107CC File Offset: 0x0000E9CC
		public static Pen Azure
		{
			get
			{
				if (Pens.azure == null)
				{
					Pens.azure = new Pen(Color.Azure);
					Pens.azure.isModifiable = false;
				}
				return Pens.azure;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public static Pen Beige
		{
			get
			{
				if (Pens.beige == null)
				{
					Pens.beige = new Pen(Color.Beige);
					Pens.beige.isModifiable = false;
				}
				return Pens.beige;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001081C File Offset: 0x0000EA1C
		public static Pen Bisque
		{
			get
			{
				if (Pens.bisque == null)
				{
					Pens.bisque = new Pen(Color.Bisque);
					Pens.bisque.isModifiable = false;
				}
				return Pens.bisque;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00010844 File Offset: 0x0000EA44
		public static Pen Black
		{
			get
			{
				if (Pens.black == null)
				{
					Pens.black = new Pen(Color.Black);
					Pens.black.isModifiable = false;
				}
				return Pens.black;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001086C File Offset: 0x0000EA6C
		public static Pen BlanchedAlmond
		{
			get
			{
				if (Pens.blanchedalmond == null)
				{
					Pens.blanchedalmond = new Pen(Color.BlanchedAlmond);
					Pens.blanchedalmond.isModifiable = false;
				}
				return Pens.blanchedalmond;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00010894 File Offset: 0x0000EA94
		public static Pen Blue
		{
			get
			{
				if (Pens.blue == null)
				{
					Pens.blue = new Pen(Color.Blue);
					Pens.blue.isModifiable = false;
				}
				return Pens.blue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000108BC File Offset: 0x0000EABC
		public static Pen BlueViolet
		{
			get
			{
				if (Pens.blueviolet == null)
				{
					Pens.blueviolet = new Pen(Color.BlueViolet);
					Pens.blueviolet.isModifiable = false;
				}
				return Pens.blueviolet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public static Pen Brown
		{
			get
			{
				if (Pens.brown == null)
				{
					Pens.brown = new Pen(Color.Brown);
					Pens.brown.isModifiable = false;
				}
				return Pens.brown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001090C File Offset: 0x0000EB0C
		public static Pen BurlyWood
		{
			get
			{
				if (Pens.burlywood == null)
				{
					Pens.burlywood = new Pen(Color.BurlyWood);
					Pens.burlywood.isModifiable = false;
				}
				return Pens.burlywood;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00010934 File Offset: 0x0000EB34
		public static Pen CadetBlue
		{
			get
			{
				if (Pens.cadetblue == null)
				{
					Pens.cadetblue = new Pen(Color.CadetBlue);
					Pens.cadetblue.isModifiable = false;
				}
				return Pens.cadetblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static Pen Chartreuse
		{
			get
			{
				if (Pens.chartreuse == null)
				{
					Pens.chartreuse = new Pen(Color.Chartreuse);
					Pens.chartreuse.isModifiable = false;
				}
				return Pens.chartreuse;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00010984 File Offset: 0x0000EB84
		public static Pen Chocolate
		{
			get
			{
				if (Pens.chocolate == null)
				{
					Pens.chocolate = new Pen(Color.Chocolate);
					Pens.chocolate.isModifiable = false;
				}
				return Pens.chocolate;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000109AC File Offset: 0x0000EBAC
		public static Pen Coral
		{
			get
			{
				if (Pens.coral == null)
				{
					Pens.coral = new Pen(Color.Coral);
					Pens.coral.isModifiable = false;
				}
				return Pens.coral;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000109D4 File Offset: 0x0000EBD4
		public static Pen CornflowerBlue
		{
			get
			{
				if (Pens.cornflowerblue == null)
				{
					Pens.cornflowerblue = new Pen(Color.CornflowerBlue);
					Pens.cornflowerblue.isModifiable = false;
				}
				return Pens.cornflowerblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000109FC File Offset: 0x0000EBFC
		public static Pen Cornsilk
		{
			get
			{
				if (Pens.cornsilk == null)
				{
					Pens.cornsilk = new Pen(Color.Cornsilk);
					Pens.cornsilk.isModifiable = false;
				}
				return Pens.cornsilk;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00010A24 File Offset: 0x0000EC24
		public static Pen Crimson
		{
			get
			{
				if (Pens.crimson == null)
				{
					Pens.crimson = new Pen(Color.Crimson);
					Pens.crimson.isModifiable = false;
				}
				return Pens.crimson;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public static Pen Cyan
		{
			get
			{
				if (Pens.cyan == null)
				{
					Pens.cyan = new Pen(Color.Cyan);
					Pens.cyan.isModifiable = false;
				}
				return Pens.cyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00010A74 File Offset: 0x0000EC74
		public static Pen DarkBlue
		{
			get
			{
				if (Pens.darkblue == null)
				{
					Pens.darkblue = new Pen(Color.DarkBlue);
					Pens.darkblue.isModifiable = false;
				}
				return Pens.darkblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public static Pen DarkCyan
		{
			get
			{
				if (Pens.darkcyan == null)
				{
					Pens.darkcyan = new Pen(Color.DarkCyan);
					Pens.darkcyan.isModifiable = false;
				}
				return Pens.darkcyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public static Pen DarkGoldenrod
		{
			get
			{
				if (Pens.darkgoldenrod == null)
				{
					Pens.darkgoldenrod = new Pen(Color.DarkGoldenrod);
					Pens.darkgoldenrod.isModifiable = false;
				}
				return Pens.darkgoldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public static Pen DarkGray
		{
			get
			{
				if (Pens.darkgray == null)
				{
					Pens.darkgray = new Pen(Color.DarkGray);
					Pens.darkgray.isModifiable = false;
				}
				return Pens.darkgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00010B14 File Offset: 0x0000ED14
		public static Pen DarkGreen
		{
			get
			{
				if (Pens.darkgreen == null)
				{
					Pens.darkgreen = new Pen(Color.DarkGreen);
					Pens.darkgreen.isModifiable = false;
				}
				return Pens.darkgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public static Pen DarkKhaki
		{
			get
			{
				if (Pens.darkkhaki == null)
				{
					Pens.darkkhaki = new Pen(Color.DarkKhaki);
					Pens.darkkhaki.isModifiable = false;
				}
				return Pens.darkkhaki;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00010B64 File Offset: 0x0000ED64
		public static Pen DarkMagenta
		{
			get
			{
				if (Pens.darkmagenta == null)
				{
					Pens.darkmagenta = new Pen(Color.DarkMagenta);
					Pens.darkmagenta.isModifiable = false;
				}
				return Pens.darkmagenta;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00010B8C File Offset: 0x0000ED8C
		public static Pen DarkOliveGreen
		{
			get
			{
				if (Pens.darkolivegreen == null)
				{
					Pens.darkolivegreen = new Pen(Color.DarkOliveGreen);
					Pens.darkolivegreen.isModifiable = false;
				}
				return Pens.darkolivegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public static Pen DarkOrange
		{
			get
			{
				if (Pens.darkorange == null)
				{
					Pens.darkorange = new Pen(Color.DarkOrange);
					Pens.darkorange.isModifiable = false;
				}
				return Pens.darkorange;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public static Pen DarkOrchid
		{
			get
			{
				if (Pens.darkorchid == null)
				{
					Pens.darkorchid = new Pen(Color.DarkOrchid);
					Pens.darkorchid.isModifiable = false;
				}
				return Pens.darkorchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00010C04 File Offset: 0x0000EE04
		public static Pen DarkRed
		{
			get
			{
				if (Pens.darkred == null)
				{
					Pens.darkred = new Pen(Color.DarkRed);
					Pens.darkred.isModifiable = false;
				}
				return Pens.darkred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public static Pen DarkSalmon
		{
			get
			{
				if (Pens.darksalmon == null)
				{
					Pens.darksalmon = new Pen(Color.DarkSalmon);
					Pens.darksalmon.isModifiable = false;
				}
				return Pens.darksalmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00010C54 File Offset: 0x0000EE54
		public static Pen DarkSeaGreen
		{
			get
			{
				if (Pens.darkseagreen == null)
				{
					Pens.darkseagreen = new Pen(Color.DarkSeaGreen);
					Pens.darkseagreen.isModifiable = false;
				}
				return Pens.darkseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public static Pen DarkSlateBlue
		{
			get
			{
				if (Pens.darkslateblue == null)
				{
					Pens.darkslateblue = new Pen(Color.DarkSlateBlue);
					Pens.darkslateblue.isModifiable = false;
				}
				return Pens.darkslateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		public static Pen DarkSlateGray
		{
			get
			{
				if (Pens.darkslategray == null)
				{
					Pens.darkslategray = new Pen(Color.DarkSlateGray);
					Pens.darkslategray.isModifiable = false;
				}
				return Pens.darkslategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00010CCC File Offset: 0x0000EECC
		public static Pen DarkTurquoise
		{
			get
			{
				if (Pens.darkturquoise == null)
				{
					Pens.darkturquoise = new Pen(Color.DarkTurquoise);
					Pens.darkturquoise.isModifiable = false;
				}
				return Pens.darkturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		public static Pen DarkViolet
		{
			get
			{
				if (Pens.darkviolet == null)
				{
					Pens.darkviolet = new Pen(Color.DarkViolet);
					Pens.darkviolet.isModifiable = false;
				}
				return Pens.darkviolet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00010D1C File Offset: 0x0000EF1C
		public static Pen DeepPink
		{
			get
			{
				if (Pens.deeppink == null)
				{
					Pens.deeppink = new Pen(Color.DeepPink);
					Pens.deeppink.isModifiable = false;
				}
				return Pens.deeppink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00010D44 File Offset: 0x0000EF44
		public static Pen DeepSkyBlue
		{
			get
			{
				if (Pens.deepskyblue == null)
				{
					Pens.deepskyblue = new Pen(Color.DeepSkyBlue);
					Pens.deepskyblue.isModifiable = false;
				}
				return Pens.deepskyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public static Pen DimGray
		{
			get
			{
				if (Pens.dimgray == null)
				{
					Pens.dimgray = new Pen(Color.DimGray);
					Pens.dimgray.isModifiable = false;
				}
				return Pens.dimgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00010D94 File Offset: 0x0000EF94
		public static Pen DodgerBlue
		{
			get
			{
				if (Pens.dodgerblue == null)
				{
					Pens.dodgerblue = new Pen(Color.DodgerBlue);
					Pens.dodgerblue.isModifiable = false;
				}
				return Pens.dodgerblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00010DBC File Offset: 0x0000EFBC
		public static Pen Firebrick
		{
			get
			{
				if (Pens.firebrick == null)
				{
					Pens.firebrick = new Pen(Color.Firebrick);
					Pens.firebrick.isModifiable = false;
				}
				return Pens.firebrick;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public static Pen FloralWhite
		{
			get
			{
				if (Pens.floralwhite == null)
				{
					Pens.floralwhite = new Pen(Color.FloralWhite);
					Pens.floralwhite.isModifiable = false;
				}
				return Pens.floralwhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00010E0C File Offset: 0x0000F00C
		public static Pen ForestGreen
		{
			get
			{
				if (Pens.forestgreen == null)
				{
					Pens.forestgreen = new Pen(Color.ForestGreen);
					Pens.forestgreen.isModifiable = false;
				}
				return Pens.forestgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00010E34 File Offset: 0x0000F034
		public static Pen Fuchsia
		{
			get
			{
				if (Pens.fuchsia == null)
				{
					Pens.fuchsia = new Pen(Color.Fuchsia);
					Pens.fuchsia.isModifiable = false;
				}
				return Pens.fuchsia;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00010E5C File Offset: 0x0000F05C
		public static Pen Gainsboro
		{
			get
			{
				if (Pens.gainsboro == null)
				{
					Pens.gainsboro = new Pen(Color.Gainsboro);
					Pens.gainsboro.isModifiable = false;
				}
				return Pens.gainsboro;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00010E84 File Offset: 0x0000F084
		public static Pen GhostWhite
		{
			get
			{
				if (Pens.ghostwhite == null)
				{
					Pens.ghostwhite = new Pen(Color.GhostWhite);
					Pens.ghostwhite.isModifiable = false;
				}
				return Pens.ghostwhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00010EAC File Offset: 0x0000F0AC
		public static Pen Gold
		{
			get
			{
				if (Pens.gold == null)
				{
					Pens.gold = new Pen(Color.Gold);
					Pens.gold.isModifiable = false;
				}
				return Pens.gold;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public static Pen Goldenrod
		{
			get
			{
				if (Pens.goldenrod == null)
				{
					Pens.goldenrod = new Pen(Color.Goldenrod);
					Pens.goldenrod.isModifiable = false;
				}
				return Pens.goldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00010EFC File Offset: 0x0000F0FC
		public static Pen Gray
		{
			get
			{
				if (Pens.gray == null)
				{
					Pens.gray = new Pen(Color.Gray);
					Pens.gray.isModifiable = false;
				}
				return Pens.gray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00010F24 File Offset: 0x0000F124
		public static Pen Green
		{
			get
			{
				if (Pens.green == null)
				{
					Pens.green = new Pen(Color.Green);
					Pens.green.isModifiable = false;
				}
				return Pens.green;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00010F4C File Offset: 0x0000F14C
		public static Pen GreenYellow
		{
			get
			{
				if (Pens.greenyellow == null)
				{
					Pens.greenyellow = new Pen(Color.GreenYellow);
					Pens.greenyellow.isModifiable = false;
				}
				return Pens.greenyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00010F74 File Offset: 0x0000F174
		public static Pen Honeydew
		{
			get
			{
				if (Pens.honeydew == null)
				{
					Pens.honeydew = new Pen(Color.Honeydew);
					Pens.honeydew.isModifiable = false;
				}
				return Pens.honeydew;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00010F9C File Offset: 0x0000F19C
		public static Pen HotPink
		{
			get
			{
				if (Pens.hotpink == null)
				{
					Pens.hotpink = new Pen(Color.HotPink);
					Pens.hotpink.isModifiable = false;
				}
				return Pens.hotpink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public static Pen IndianRed
		{
			get
			{
				if (Pens.indianred == null)
				{
					Pens.indianred = new Pen(Color.IndianRed);
					Pens.indianred.isModifiable = false;
				}
				return Pens.indianred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public static Pen Indigo
		{
			get
			{
				if (Pens.indigo == null)
				{
					Pens.indigo = new Pen(Color.Indigo);
					Pens.indigo.isModifiable = false;
				}
				return Pens.indigo;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00011014 File Offset: 0x0000F214
		public static Pen Ivory
		{
			get
			{
				if (Pens.ivory == null)
				{
					Pens.ivory = new Pen(Color.Ivory);
					Pens.ivory.isModifiable = false;
				}
				return Pens.ivory;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001103C File Offset: 0x0000F23C
		public static Pen Khaki
		{
			get
			{
				if (Pens.khaki == null)
				{
					Pens.khaki = new Pen(Color.Khaki);
					Pens.khaki.isModifiable = false;
				}
				return Pens.khaki;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00011064 File Offset: 0x0000F264
		public static Pen Lavender
		{
			get
			{
				if (Pens.lavender == null)
				{
					Pens.lavender = new Pen(Color.Lavender);
					Pens.lavender.isModifiable = false;
				}
				return Pens.lavender;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001108C File Offset: 0x0000F28C
		public static Pen LavenderBlush
		{
			get
			{
				if (Pens.lavenderblush == null)
				{
					Pens.lavenderblush = new Pen(Color.LavenderBlush);
					Pens.lavenderblush.isModifiable = false;
				}
				return Pens.lavenderblush;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000110B4 File Offset: 0x0000F2B4
		public static Pen LawnGreen
		{
			get
			{
				if (Pens.lawngreen == null)
				{
					Pens.lawngreen = new Pen(Color.LawnGreen);
					Pens.lawngreen.isModifiable = false;
				}
				return Pens.lawngreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000110DC File Offset: 0x0000F2DC
		public static Pen LemonChiffon
		{
			get
			{
				if (Pens.lemonchiffon == null)
				{
					Pens.lemonchiffon = new Pen(Color.LemonChiffon);
					Pens.lemonchiffon.isModifiable = false;
				}
				return Pens.lemonchiffon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00011104 File Offset: 0x0000F304
		public static Pen LightBlue
		{
			get
			{
				if (Pens.lightblue == null)
				{
					Pens.lightblue = new Pen(Color.LightBlue);
					Pens.lightblue.isModifiable = false;
				}
				return Pens.lightblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001112C File Offset: 0x0000F32C
		public static Pen LightCoral
		{
			get
			{
				if (Pens.lightcoral == null)
				{
					Pens.lightcoral = new Pen(Color.LightCoral);
					Pens.lightcoral.isModifiable = false;
				}
				return Pens.lightcoral;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00011154 File Offset: 0x0000F354
		public static Pen LightCyan
		{
			get
			{
				if (Pens.lightcyan == null)
				{
					Pens.lightcyan = new Pen(Color.LightCyan);
					Pens.lightcyan.isModifiable = false;
				}
				return Pens.lightcyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001117C File Offset: 0x0000F37C
		public static Pen LightGoldenrodYellow
		{
			get
			{
				if (Pens.lightgoldenrodyellow == null)
				{
					Pens.lightgoldenrodyellow = new Pen(Color.LightGoldenrodYellow);
					Pens.lightgoldenrodyellow.isModifiable = false;
				}
				return Pens.lightgoldenrodyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000111A4 File Offset: 0x0000F3A4
		public static Pen LightGray
		{
			get
			{
				if (Pens.lightgray == null)
				{
					Pens.lightgray = new Pen(Color.LightGray);
					Pens.lightgray.isModifiable = false;
				}
				return Pens.lightgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000111CC File Offset: 0x0000F3CC
		public static Pen LightGreen
		{
			get
			{
				if (Pens.lightgreen == null)
				{
					Pens.lightgreen = new Pen(Color.LightGreen);
					Pens.lightgreen.isModifiable = false;
				}
				return Pens.lightgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public static Pen LightPink
		{
			get
			{
				if (Pens.lightpink == null)
				{
					Pens.lightpink = new Pen(Color.LightPink);
					Pens.lightpink.isModifiable = false;
				}
				return Pens.lightpink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001121C File Offset: 0x0000F41C
		public static Pen LightSalmon
		{
			get
			{
				if (Pens.lightsalmon == null)
				{
					Pens.lightsalmon = new Pen(Color.LightSalmon);
					Pens.lightsalmon.isModifiable = false;
				}
				return Pens.lightsalmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00011244 File Offset: 0x0000F444
		public static Pen LightSeaGreen
		{
			get
			{
				if (Pens.lightseagreen == null)
				{
					Pens.lightseagreen = new Pen(Color.LightSeaGreen);
					Pens.lightseagreen.isModifiable = false;
				}
				return Pens.lightseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001126C File Offset: 0x0000F46C
		public static Pen LightSkyBlue
		{
			get
			{
				if (Pens.lightskyblue == null)
				{
					Pens.lightskyblue = new Pen(Color.LightSkyBlue);
					Pens.lightskyblue.isModifiable = false;
				}
				return Pens.lightskyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00011294 File Offset: 0x0000F494
		public static Pen LightSlateGray
		{
			get
			{
				if (Pens.lightslategray == null)
				{
					Pens.lightslategray = new Pen(Color.LightSlateGray);
					Pens.lightslategray.isModifiable = false;
				}
				return Pens.lightslategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000112BC File Offset: 0x0000F4BC
		public static Pen LightSteelBlue
		{
			get
			{
				if (Pens.lightsteelblue == null)
				{
					Pens.lightsteelblue = new Pen(Color.LightSteelBlue);
					Pens.lightsteelblue.isModifiable = false;
				}
				return Pens.lightsteelblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000112E4 File Offset: 0x0000F4E4
		public static Pen LightYellow
		{
			get
			{
				if (Pens.lightyellow == null)
				{
					Pens.lightyellow = new Pen(Color.LightYellow);
					Pens.lightyellow.isModifiable = false;
				}
				return Pens.lightyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001130C File Offset: 0x0000F50C
		public static Pen Lime
		{
			get
			{
				if (Pens.lime == null)
				{
					Pens.lime = new Pen(Color.Lime);
					Pens.lime.isModifiable = false;
				}
				return Pens.lime;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00011334 File Offset: 0x0000F534
		public static Pen LimeGreen
		{
			get
			{
				if (Pens.limegreen == null)
				{
					Pens.limegreen = new Pen(Color.LimeGreen);
					Pens.limegreen.isModifiable = false;
				}
				return Pens.limegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001135C File Offset: 0x0000F55C
		public static Pen Linen
		{
			get
			{
				if (Pens.linen == null)
				{
					Pens.linen = new Pen(Color.Linen);
					Pens.linen.isModifiable = false;
				}
				return Pens.linen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00011384 File Offset: 0x0000F584
		public static Pen Magenta
		{
			get
			{
				if (Pens.magenta == null)
				{
					Pens.magenta = new Pen(Color.Magenta);
					Pens.magenta.isModifiable = false;
				}
				return Pens.magenta;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000113AC File Offset: 0x0000F5AC
		public static Pen Maroon
		{
			get
			{
				if (Pens.maroon == null)
				{
					Pens.maroon = new Pen(Color.Maroon);
					Pens.maroon.isModifiable = false;
				}
				return Pens.maroon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public static Pen MediumAquamarine
		{
			get
			{
				if (Pens.mediumaquamarine == null)
				{
					Pens.mediumaquamarine = new Pen(Color.MediumAquamarine);
					Pens.mediumaquamarine.isModifiable = false;
				}
				return Pens.mediumaquamarine;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000113FC File Offset: 0x0000F5FC
		public static Pen MediumBlue
		{
			get
			{
				if (Pens.mediumblue == null)
				{
					Pens.mediumblue = new Pen(Color.MediumBlue);
					Pens.mediumblue.isModifiable = false;
				}
				return Pens.mediumblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00011424 File Offset: 0x0000F624
		public static Pen MediumOrchid
		{
			get
			{
				if (Pens.mediumorchid == null)
				{
					Pens.mediumorchid = new Pen(Color.MediumOrchid);
					Pens.mediumorchid.isModifiable = false;
				}
				return Pens.mediumorchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001144C File Offset: 0x0000F64C
		public static Pen MediumPurple
		{
			get
			{
				if (Pens.mediumpurple == null)
				{
					Pens.mediumpurple = new Pen(Color.MediumPurple);
					Pens.mediumpurple.isModifiable = false;
				}
				return Pens.mediumpurple;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00011474 File Offset: 0x0000F674
		public static Pen MediumSeaGreen
		{
			get
			{
				if (Pens.mediumseagreen == null)
				{
					Pens.mediumseagreen = new Pen(Color.MediumSeaGreen);
					Pens.mediumseagreen.isModifiable = false;
				}
				return Pens.mediumseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001149C File Offset: 0x0000F69C
		public static Pen MediumSlateBlue
		{
			get
			{
				if (Pens.mediumslateblue == null)
				{
					Pens.mediumslateblue = new Pen(Color.MediumSlateBlue);
					Pens.mediumslateblue.isModifiable = false;
				}
				return Pens.mediumslateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public static Pen MediumSpringGreen
		{
			get
			{
				if (Pens.mediumspringgreen == null)
				{
					Pens.mediumspringgreen = new Pen(Color.MediumSpringGreen);
					Pens.mediumspringgreen.isModifiable = false;
				}
				return Pens.mediumspringgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000114EC File Offset: 0x0000F6EC
		public static Pen MediumTurquoise
		{
			get
			{
				if (Pens.mediumturquoise == null)
				{
					Pens.mediumturquoise = new Pen(Color.MediumTurquoise);
					Pens.mediumturquoise.isModifiable = false;
				}
				return Pens.mediumturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00011514 File Offset: 0x0000F714
		public static Pen MediumVioletRed
		{
			get
			{
				if (Pens.mediumvioletred == null)
				{
					Pens.mediumvioletred = new Pen(Color.MediumVioletRed);
					Pens.mediumvioletred.isModifiable = false;
				}
				return Pens.mediumvioletred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001153C File Offset: 0x0000F73C
		public static Pen MidnightBlue
		{
			get
			{
				if (Pens.midnightblue == null)
				{
					Pens.midnightblue = new Pen(Color.MidnightBlue);
					Pens.midnightblue.isModifiable = false;
				}
				return Pens.midnightblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00011564 File Offset: 0x0000F764
		public static Pen MintCream
		{
			get
			{
				if (Pens.mintcream == null)
				{
					Pens.mintcream = new Pen(Color.MintCream);
					Pens.mintcream.isModifiable = false;
				}
				return Pens.mintcream;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001158C File Offset: 0x0000F78C
		public static Pen MistyRose
		{
			get
			{
				if (Pens.mistyrose == null)
				{
					Pens.mistyrose = new Pen(Color.MistyRose);
					Pens.mistyrose.isModifiable = false;
				}
				return Pens.mistyrose;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public static Pen Moccasin
		{
			get
			{
				if (Pens.moccasin == null)
				{
					Pens.moccasin = new Pen(Color.Moccasin);
					Pens.moccasin.isModifiable = false;
				}
				return Pens.moccasin;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000115DC File Offset: 0x0000F7DC
		public static Pen NavajoWhite
		{
			get
			{
				if (Pens.navajowhite == null)
				{
					Pens.navajowhite = new Pen(Color.NavajoWhite);
					Pens.navajowhite.isModifiable = false;
				}
				return Pens.navajowhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00011604 File Offset: 0x0000F804
		public static Pen Navy
		{
			get
			{
				if (Pens.navy == null)
				{
					Pens.navy = new Pen(Color.Navy);
					Pens.navy.isModifiable = false;
				}
				return Pens.navy;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001162C File Offset: 0x0000F82C
		public static Pen OldLace
		{
			get
			{
				if (Pens.oldlace == null)
				{
					Pens.oldlace = new Pen(Color.OldLace);
					Pens.oldlace.isModifiable = false;
				}
				return Pens.oldlace;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00011654 File Offset: 0x0000F854
		public static Pen Olive
		{
			get
			{
				if (Pens.olive == null)
				{
					Pens.olive = new Pen(Color.Olive);
					Pens.olive.isModifiable = false;
				}
				return Pens.olive;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001167C File Offset: 0x0000F87C
		public static Pen OliveDrab
		{
			get
			{
				if (Pens.olivedrab == null)
				{
					Pens.olivedrab = new Pen(Color.OliveDrab);
					Pens.olivedrab.isModifiable = false;
				}
				return Pens.olivedrab;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public static Pen Orange
		{
			get
			{
				if (Pens.orange == null)
				{
					Pens.orange = new Pen(Color.Orange);
					Pens.orange.isModifiable = false;
				}
				return Pens.orange;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000116CC File Offset: 0x0000F8CC
		public static Pen OrangeRed
		{
			get
			{
				if (Pens.orangered == null)
				{
					Pens.orangered = new Pen(Color.OrangeRed);
					Pens.orangered.isModifiable = false;
				}
				return Pens.orangered;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public static Pen Orchid
		{
			get
			{
				if (Pens.orchid == null)
				{
					Pens.orchid = new Pen(Color.Orchid);
					Pens.orchid.isModifiable = false;
				}
				return Pens.orchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001171C File Offset: 0x0000F91C
		public static Pen PaleGoldenrod
		{
			get
			{
				if (Pens.palegoldenrod == null)
				{
					Pens.palegoldenrod = new Pen(Color.PaleGoldenrod);
					Pens.palegoldenrod.isModifiable = false;
				}
				return Pens.palegoldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00011744 File Offset: 0x0000F944
		public static Pen PaleGreen
		{
			get
			{
				if (Pens.palegreen == null)
				{
					Pens.palegreen = new Pen(Color.PaleGreen);
					Pens.palegreen.isModifiable = false;
				}
				return Pens.palegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001176C File Offset: 0x0000F96C
		public static Pen PaleTurquoise
		{
			get
			{
				if (Pens.paleturquoise == null)
				{
					Pens.paleturquoise = new Pen(Color.PaleTurquoise);
					Pens.paleturquoise.isModifiable = false;
				}
				return Pens.paleturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00011794 File Offset: 0x0000F994
		public static Pen PaleVioletRed
		{
			get
			{
				if (Pens.palevioletred == null)
				{
					Pens.palevioletred = new Pen(Color.PaleVioletRed);
					Pens.palevioletred.isModifiable = false;
				}
				return Pens.palevioletred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000117BC File Offset: 0x0000F9BC
		public static Pen PapayaWhip
		{
			get
			{
				if (Pens.papayawhip == null)
				{
					Pens.papayawhip = new Pen(Color.PapayaWhip);
					Pens.papayawhip.isModifiable = false;
				}
				return Pens.papayawhip;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public static Pen PeachPuff
		{
			get
			{
				if (Pens.peachpuff == null)
				{
					Pens.peachpuff = new Pen(Color.PeachPuff);
					Pens.peachpuff.isModifiable = false;
				}
				return Pens.peachpuff;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001180C File Offset: 0x0000FA0C
		public static Pen Peru
		{
			get
			{
				if (Pens.peru == null)
				{
					Pens.peru = new Pen(Color.Peru);
					Pens.peru.isModifiable = false;
				}
				return Pens.peru;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00011834 File Offset: 0x0000FA34
		public static Pen Pink
		{
			get
			{
				if (Pens.pink == null)
				{
					Pens.pink = new Pen(Color.Pink);
					Pens.pink.isModifiable = false;
				}
				return Pens.pink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001185C File Offset: 0x0000FA5C
		public static Pen Plum
		{
			get
			{
				if (Pens.plum == null)
				{
					Pens.plum = new Pen(Color.Plum);
					Pens.plum.isModifiable = false;
				}
				return Pens.plum;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00011884 File Offset: 0x0000FA84
		public static Pen PowderBlue
		{
			get
			{
				if (Pens.powderblue == null)
				{
					Pens.powderblue = new Pen(Color.PowderBlue);
					Pens.powderblue.isModifiable = false;
				}
				return Pens.powderblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000118AC File Offset: 0x0000FAAC
		public static Pen Purple
		{
			get
			{
				if (Pens.purple == null)
				{
					Pens.purple = new Pen(Color.Purple);
					Pens.purple.isModifiable = false;
				}
				return Pens.purple;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public static Pen Red
		{
			get
			{
				if (Pens.red == null)
				{
					Pens.red = new Pen(Color.Red);
					Pens.red.isModifiable = false;
				}
				return Pens.red;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000118FC File Offset: 0x0000FAFC
		public static Pen RosyBrown
		{
			get
			{
				if (Pens.rosybrown == null)
				{
					Pens.rosybrown = new Pen(Color.RosyBrown);
					Pens.rosybrown.isModifiable = false;
				}
				return Pens.rosybrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00011924 File Offset: 0x0000FB24
		public static Pen RoyalBlue
		{
			get
			{
				if (Pens.royalblue == null)
				{
					Pens.royalblue = new Pen(Color.RoyalBlue);
					Pens.royalblue.isModifiable = false;
				}
				return Pens.royalblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001194C File Offset: 0x0000FB4C
		public static Pen SaddleBrown
		{
			get
			{
				if (Pens.saddlebrown == null)
				{
					Pens.saddlebrown = new Pen(Color.SaddleBrown);
					Pens.saddlebrown.isModifiable = false;
				}
				return Pens.saddlebrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00011974 File Offset: 0x0000FB74
		public static Pen Salmon
		{
			get
			{
				if (Pens.salmon == null)
				{
					Pens.salmon = new Pen(Color.Salmon);
					Pens.salmon.isModifiable = false;
				}
				return Pens.salmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001199C File Offset: 0x0000FB9C
		public static Pen SandyBrown
		{
			get
			{
				if (Pens.sandybrown == null)
				{
					Pens.sandybrown = new Pen(Color.SandyBrown);
					Pens.sandybrown.isModifiable = false;
				}
				return Pens.sandybrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public static Pen SeaGreen
		{
			get
			{
				if (Pens.seagreen == null)
				{
					Pens.seagreen = new Pen(Color.SeaGreen);
					Pens.seagreen.isModifiable = false;
				}
				return Pens.seagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000119EC File Offset: 0x0000FBEC
		public static Pen SeaShell
		{
			get
			{
				if (Pens.seashell == null)
				{
					Pens.seashell = new Pen(Color.SeaShell);
					Pens.seashell.isModifiable = false;
				}
				return Pens.seashell;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00011A14 File Offset: 0x0000FC14
		public static Pen Sienna
		{
			get
			{
				if (Pens.sienna == null)
				{
					Pens.sienna = new Pen(Color.Sienna);
					Pens.sienna.isModifiable = false;
				}
				return Pens.sienna;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00011A3C File Offset: 0x0000FC3C
		public static Pen Silver
		{
			get
			{
				if (Pens.silver == null)
				{
					Pens.silver = new Pen(Color.Silver);
					Pens.silver.isModifiable = false;
				}
				return Pens.silver;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00011A64 File Offset: 0x0000FC64
		public static Pen SkyBlue
		{
			get
			{
				if (Pens.skyblue == null)
				{
					Pens.skyblue = new Pen(Color.SkyBlue);
					Pens.skyblue.isModifiable = false;
				}
				return Pens.skyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00011A8C File Offset: 0x0000FC8C
		public static Pen SlateBlue
		{
			get
			{
				if (Pens.slateblue == null)
				{
					Pens.slateblue = new Pen(Color.SlateBlue);
					Pens.slateblue.isModifiable = false;
				}
				return Pens.slateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public static Pen SlateGray
		{
			get
			{
				if (Pens.slategray == null)
				{
					Pens.slategray = new Pen(Color.SlateGray);
					Pens.slategray.isModifiable = false;
				}
				return Pens.slategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public static Pen Snow
		{
			get
			{
				if (Pens.snow == null)
				{
					Pens.snow = new Pen(Color.Snow);
					Pens.snow.isModifiable = false;
				}
				return Pens.snow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00011B04 File Offset: 0x0000FD04
		public static Pen SpringGreen
		{
			get
			{
				if (Pens.springgreen == null)
				{
					Pens.springgreen = new Pen(Color.SpringGreen);
					Pens.springgreen.isModifiable = false;
				}
				return Pens.springgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00011B2C File Offset: 0x0000FD2C
		public static Pen SteelBlue
		{
			get
			{
				if (Pens.steelblue == null)
				{
					Pens.steelblue = new Pen(Color.SteelBlue);
					Pens.steelblue.isModifiable = false;
				}
				return Pens.steelblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00011B54 File Offset: 0x0000FD54
		public static Pen Tan
		{
			get
			{
				if (Pens.tan == null)
				{
					Pens.tan = new Pen(Color.Tan);
					Pens.tan.isModifiable = false;
				}
				return Pens.tan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public static Pen Teal
		{
			get
			{
				if (Pens.teal == null)
				{
					Pens.teal = new Pen(Color.Teal);
					Pens.teal.isModifiable = false;
				}
				return Pens.teal;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		public static Pen Thistle
		{
			get
			{
				if (Pens.thistle == null)
				{
					Pens.thistle = new Pen(Color.Thistle);
					Pens.thistle.isModifiable = false;
				}
				return Pens.thistle;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public static Pen Tomato
		{
			get
			{
				if (Pens.tomato == null)
				{
					Pens.tomato = new Pen(Color.Tomato);
					Pens.tomato.isModifiable = false;
				}
				return Pens.tomato;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public static Pen Transparent
		{
			get
			{
				if (Pens.transparent == null)
				{
					Pens.transparent = new Pen(Color.Transparent);
					Pens.transparent.isModifiable = false;
				}
				return Pens.transparent;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public static Pen Turquoise
		{
			get
			{
				if (Pens.turquoise == null)
				{
					Pens.turquoise = new Pen(Color.Turquoise);
					Pens.turquoise.isModifiable = false;
				}
				return Pens.turquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00011C44 File Offset: 0x0000FE44
		public static Pen Violet
		{
			get
			{
				if (Pens.violet == null)
				{
					Pens.violet = new Pen(Color.Violet);
					Pens.violet.isModifiable = false;
				}
				return Pens.violet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public static Pen Wheat
		{
			get
			{
				if (Pens.wheat == null)
				{
					Pens.wheat = new Pen(Color.Wheat);
					Pens.wheat.isModifiable = false;
				}
				return Pens.wheat;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00011C94 File Offset: 0x0000FE94
		public static Pen White
		{
			get
			{
				if (Pens.white == null)
				{
					Pens.white = new Pen(Color.White);
					Pens.white.isModifiable = false;
				}
				return Pens.white;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public static Pen WhiteSmoke
		{
			get
			{
				if (Pens.whitesmoke == null)
				{
					Pens.whitesmoke = new Pen(Color.WhiteSmoke);
					Pens.whitesmoke.isModifiable = false;
				}
				return Pens.whitesmoke;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public static Pen Yellow
		{
			get
			{
				if (Pens.yellow == null)
				{
					Pens.yellow = new Pen(Color.Yellow);
					Pens.yellow.isModifiable = false;
				}
				return Pens.yellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public static Pen YellowGreen
		{
			get
			{
				if (Pens.yellowgreen == null)
				{
					Pens.yellowgreen = new Pen(Color.YellowGreen);
					Pens.yellowgreen.isModifiable = false;
				}
				return Pens.yellowgreen;
			}
		}

		// Token: 0x040004AF RID: 1199
		private static Pen aliceblue;

		// Token: 0x040004B0 RID: 1200
		private static Pen antiquewhite;

		// Token: 0x040004B1 RID: 1201
		private static Pen aqua;

		// Token: 0x040004B2 RID: 1202
		private static Pen aquamarine;

		// Token: 0x040004B3 RID: 1203
		private static Pen azure;

		// Token: 0x040004B4 RID: 1204
		private static Pen beige;

		// Token: 0x040004B5 RID: 1205
		private static Pen bisque;

		// Token: 0x040004B6 RID: 1206
		private static Pen black;

		// Token: 0x040004B7 RID: 1207
		private static Pen blanchedalmond;

		// Token: 0x040004B8 RID: 1208
		private static Pen blue;

		// Token: 0x040004B9 RID: 1209
		private static Pen blueviolet;

		// Token: 0x040004BA RID: 1210
		private static Pen brown;

		// Token: 0x040004BB RID: 1211
		private static Pen burlywood;

		// Token: 0x040004BC RID: 1212
		private static Pen cadetblue;

		// Token: 0x040004BD RID: 1213
		private static Pen chartreuse;

		// Token: 0x040004BE RID: 1214
		private static Pen chocolate;

		// Token: 0x040004BF RID: 1215
		private static Pen coral;

		// Token: 0x040004C0 RID: 1216
		private static Pen cornflowerblue;

		// Token: 0x040004C1 RID: 1217
		private static Pen cornsilk;

		// Token: 0x040004C2 RID: 1218
		private static Pen crimson;

		// Token: 0x040004C3 RID: 1219
		private static Pen cyan;

		// Token: 0x040004C4 RID: 1220
		private static Pen darkblue;

		// Token: 0x040004C5 RID: 1221
		private static Pen darkcyan;

		// Token: 0x040004C6 RID: 1222
		private static Pen darkgoldenrod;

		// Token: 0x040004C7 RID: 1223
		private static Pen darkgray;

		// Token: 0x040004C8 RID: 1224
		private static Pen darkgreen;

		// Token: 0x040004C9 RID: 1225
		private static Pen darkkhaki;

		// Token: 0x040004CA RID: 1226
		private static Pen darkmagenta;

		// Token: 0x040004CB RID: 1227
		private static Pen darkolivegreen;

		// Token: 0x040004CC RID: 1228
		private static Pen darkorange;

		// Token: 0x040004CD RID: 1229
		private static Pen darkorchid;

		// Token: 0x040004CE RID: 1230
		private static Pen darkred;

		// Token: 0x040004CF RID: 1231
		private static Pen darksalmon;

		// Token: 0x040004D0 RID: 1232
		private static Pen darkseagreen;

		// Token: 0x040004D1 RID: 1233
		private static Pen darkslateblue;

		// Token: 0x040004D2 RID: 1234
		private static Pen darkslategray;

		// Token: 0x040004D3 RID: 1235
		private static Pen darkturquoise;

		// Token: 0x040004D4 RID: 1236
		private static Pen darkviolet;

		// Token: 0x040004D5 RID: 1237
		private static Pen deeppink;

		// Token: 0x040004D6 RID: 1238
		private static Pen deepskyblue;

		// Token: 0x040004D7 RID: 1239
		private static Pen dimgray;

		// Token: 0x040004D8 RID: 1240
		private static Pen dodgerblue;

		// Token: 0x040004D9 RID: 1241
		private static Pen firebrick;

		// Token: 0x040004DA RID: 1242
		private static Pen floralwhite;

		// Token: 0x040004DB RID: 1243
		private static Pen forestgreen;

		// Token: 0x040004DC RID: 1244
		private static Pen fuchsia;

		// Token: 0x040004DD RID: 1245
		private static Pen gainsboro;

		// Token: 0x040004DE RID: 1246
		private static Pen ghostwhite;

		// Token: 0x040004DF RID: 1247
		private static Pen gold;

		// Token: 0x040004E0 RID: 1248
		private static Pen goldenrod;

		// Token: 0x040004E1 RID: 1249
		private static Pen gray;

		// Token: 0x040004E2 RID: 1250
		private static Pen green;

		// Token: 0x040004E3 RID: 1251
		private static Pen greenyellow;

		// Token: 0x040004E4 RID: 1252
		private static Pen honeydew;

		// Token: 0x040004E5 RID: 1253
		private static Pen hotpink;

		// Token: 0x040004E6 RID: 1254
		private static Pen indianred;

		// Token: 0x040004E7 RID: 1255
		private static Pen indigo;

		// Token: 0x040004E8 RID: 1256
		private static Pen ivory;

		// Token: 0x040004E9 RID: 1257
		private static Pen khaki;

		// Token: 0x040004EA RID: 1258
		private static Pen lavender;

		// Token: 0x040004EB RID: 1259
		private static Pen lavenderblush;

		// Token: 0x040004EC RID: 1260
		private static Pen lawngreen;

		// Token: 0x040004ED RID: 1261
		private static Pen lemonchiffon;

		// Token: 0x040004EE RID: 1262
		private static Pen lightblue;

		// Token: 0x040004EF RID: 1263
		private static Pen lightcoral;

		// Token: 0x040004F0 RID: 1264
		private static Pen lightcyan;

		// Token: 0x040004F1 RID: 1265
		private static Pen lightgoldenrodyellow;

		// Token: 0x040004F2 RID: 1266
		private static Pen lightgray;

		// Token: 0x040004F3 RID: 1267
		private static Pen lightgreen;

		// Token: 0x040004F4 RID: 1268
		private static Pen lightpink;

		// Token: 0x040004F5 RID: 1269
		private static Pen lightsalmon;

		// Token: 0x040004F6 RID: 1270
		private static Pen lightseagreen;

		// Token: 0x040004F7 RID: 1271
		private static Pen lightskyblue;

		// Token: 0x040004F8 RID: 1272
		private static Pen lightslategray;

		// Token: 0x040004F9 RID: 1273
		private static Pen lightsteelblue;

		// Token: 0x040004FA RID: 1274
		private static Pen lightyellow;

		// Token: 0x040004FB RID: 1275
		private static Pen lime;

		// Token: 0x040004FC RID: 1276
		private static Pen limegreen;

		// Token: 0x040004FD RID: 1277
		private static Pen linen;

		// Token: 0x040004FE RID: 1278
		private static Pen magenta;

		// Token: 0x040004FF RID: 1279
		private static Pen maroon;

		// Token: 0x04000500 RID: 1280
		private static Pen mediumaquamarine;

		// Token: 0x04000501 RID: 1281
		private static Pen mediumblue;

		// Token: 0x04000502 RID: 1282
		private static Pen mediumorchid;

		// Token: 0x04000503 RID: 1283
		private static Pen mediumpurple;

		// Token: 0x04000504 RID: 1284
		private static Pen mediumseagreen;

		// Token: 0x04000505 RID: 1285
		private static Pen mediumslateblue;

		// Token: 0x04000506 RID: 1286
		private static Pen mediumspringgreen;

		// Token: 0x04000507 RID: 1287
		private static Pen mediumturquoise;

		// Token: 0x04000508 RID: 1288
		private static Pen mediumvioletred;

		// Token: 0x04000509 RID: 1289
		private static Pen midnightblue;

		// Token: 0x0400050A RID: 1290
		private static Pen mintcream;

		// Token: 0x0400050B RID: 1291
		private static Pen mistyrose;

		// Token: 0x0400050C RID: 1292
		private static Pen moccasin;

		// Token: 0x0400050D RID: 1293
		private static Pen navajowhite;

		// Token: 0x0400050E RID: 1294
		private static Pen navy;

		// Token: 0x0400050F RID: 1295
		private static Pen oldlace;

		// Token: 0x04000510 RID: 1296
		private static Pen olive;

		// Token: 0x04000511 RID: 1297
		private static Pen olivedrab;

		// Token: 0x04000512 RID: 1298
		private static Pen orange;

		// Token: 0x04000513 RID: 1299
		private static Pen orangered;

		// Token: 0x04000514 RID: 1300
		private static Pen orchid;

		// Token: 0x04000515 RID: 1301
		private static Pen palegoldenrod;

		// Token: 0x04000516 RID: 1302
		private static Pen palegreen;

		// Token: 0x04000517 RID: 1303
		private static Pen paleturquoise;

		// Token: 0x04000518 RID: 1304
		private static Pen palevioletred;

		// Token: 0x04000519 RID: 1305
		private static Pen papayawhip;

		// Token: 0x0400051A RID: 1306
		private static Pen peachpuff;

		// Token: 0x0400051B RID: 1307
		private static Pen peru;

		// Token: 0x0400051C RID: 1308
		private static Pen pink;

		// Token: 0x0400051D RID: 1309
		private static Pen plum;

		// Token: 0x0400051E RID: 1310
		private static Pen powderblue;

		// Token: 0x0400051F RID: 1311
		private static Pen purple;

		// Token: 0x04000520 RID: 1312
		private static Pen red;

		// Token: 0x04000521 RID: 1313
		private static Pen rosybrown;

		// Token: 0x04000522 RID: 1314
		private static Pen royalblue;

		// Token: 0x04000523 RID: 1315
		private static Pen saddlebrown;

		// Token: 0x04000524 RID: 1316
		private static Pen salmon;

		// Token: 0x04000525 RID: 1317
		private static Pen sandybrown;

		// Token: 0x04000526 RID: 1318
		private static Pen seagreen;

		// Token: 0x04000527 RID: 1319
		private static Pen seashell;

		// Token: 0x04000528 RID: 1320
		private static Pen sienna;

		// Token: 0x04000529 RID: 1321
		private static Pen silver;

		// Token: 0x0400052A RID: 1322
		private static Pen skyblue;

		// Token: 0x0400052B RID: 1323
		private static Pen slateblue;

		// Token: 0x0400052C RID: 1324
		private static Pen slategray;

		// Token: 0x0400052D RID: 1325
		private static Pen snow;

		// Token: 0x0400052E RID: 1326
		private static Pen springgreen;

		// Token: 0x0400052F RID: 1327
		private static Pen steelblue;

		// Token: 0x04000530 RID: 1328
		private static Pen tan;

		// Token: 0x04000531 RID: 1329
		private static Pen teal;

		// Token: 0x04000532 RID: 1330
		private static Pen thistle;

		// Token: 0x04000533 RID: 1331
		private static Pen tomato;

		// Token: 0x04000534 RID: 1332
		private static Pen transparent;

		// Token: 0x04000535 RID: 1333
		private static Pen turquoise;

		// Token: 0x04000536 RID: 1334
		private static Pen violet;

		// Token: 0x04000537 RID: 1335
		private static Pen wheat;

		// Token: 0x04000538 RID: 1336
		private static Pen white;

		// Token: 0x04000539 RID: 1337
		private static Pen whitesmoke;

		// Token: 0x0400053A RID: 1338
		private static Pen yellow;

		// Token: 0x0400053B RID: 1339
		private static Pen yellowgreen;
	}
}
