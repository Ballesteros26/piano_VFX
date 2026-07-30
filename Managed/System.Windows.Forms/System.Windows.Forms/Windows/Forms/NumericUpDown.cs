using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows spin box (also known as an up-down control) that displays numeric values.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000278 RID: 632
	[ComVisible(true)]
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	[DefaultProperty("Value")]
	[ClassInterface(1)]
	public class NumericUpDown : UpDownBase, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDown" /> class.</summary>
		// Token: 0x0600292E RID: 10542 RVA: 0x0009F318 File Offset: 0x0009D518
		public NumericUpDown()
		{
			this.suppress_validation = false;
			this.decimal_places = 0;
			this.hexadecimal = false;
			this.increment = 1m;
			this.maximum = 100m;
			this.minimum = 0m;
			this.thousands_separator = false;
			this.Text = "0";
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0009F378 File Offset: 0x0009D578
		// Note: this type is marked as 'beforefieldinit'.
		static NumericUpDown()
		{
			NumericUpDown.UIAMinimumChangedEvent = new object();
			NumericUpDown.UIAMaximumChangedEvent = new object();
			NumericUpDown.UIASmallChangeChangedEvent = new object();
			NumericUpDown.ValueChangedEvent = new object();
		}

		// Token: 0x14000265 RID: 613
		// (add) Token: 0x06002930 RID: 10544 RVA: 0x0009F3B0 File Offset: 0x0009D5B0
		// (remove) Token: 0x06002931 RID: 10545 RVA: 0x0009F3C4 File Offset: 0x0009D5C4
		internal event EventHandler UIAMinimumChanged
		{
			add
			{
				base.Events.AddHandler(NumericUpDown.UIAMinimumChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NumericUpDown.UIAMinimumChangedEvent, value);
			}
		}

		// Token: 0x14000266 RID: 614
		// (add) Token: 0x06002932 RID: 10546 RVA: 0x0009F3D8 File Offset: 0x0009D5D8
		// (remove) Token: 0x06002933 RID: 10547 RVA: 0x0009F3EC File Offset: 0x0009D5EC
		internal event EventHandler UIAMaximumChanged
		{
			add
			{
				base.Events.AddHandler(NumericUpDown.UIAMaximumChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NumericUpDown.UIAMaximumChangedEvent, value);
			}
		}

		// Token: 0x14000267 RID: 615
		// (add) Token: 0x06002934 RID: 10548 RVA: 0x0009F400 File Offset: 0x0009D600
		// (remove) Token: 0x06002935 RID: 10549 RVA: 0x0009F414 File Offset: 0x0009D614
		internal event EventHandler UIASmallChangeChanged
		{
			add
			{
				base.Events.AddHandler(NumericUpDown.UIASmallChangeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NumericUpDown.UIASmallChangeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Padding" /> property changes.</summary>
		// Token: 0x14000268 RID: 616
		// (add) Token: 0x06002936 RID: 10550 RVA: 0x0009F428 File Offset: 0x0009D628
		// (remove) Token: 0x06002937 RID: 10551 RVA: 0x0009F434 File Offset: 0x0009D634
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property has been changed in some way.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000269 RID: 617
		// (add) Token: 0x06002938 RID: 10552 RVA: 0x0009F440 File Offset: 0x0009D640
		// (remove) Token: 0x06002939 RID: 10553 RVA: 0x0009F454 File Offset: 0x0009D654
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(NumericUpDown.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NumericUpDown.ValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400026A RID: 618
		// (add) Token: 0x0600293A RID: 10554 RVA: 0x0009F468 File Offset: 0x0009D668
		// (remove) Token: 0x0600293B RID: 10555 RVA: 0x0009F474 File Offset: 0x0009D674
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0009F480 File Offset: 0x0009D680
		internal void OnUIAMinimumChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NumericUpDown.UIAMinimumChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009F4B4 File Offset: 0x0009D6B4
		internal void OnUIAMaximumChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NumericUpDown.UIAMaximumChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0009F4E8 File Offset: 0x0009D6E8
		internal void OnUIASmallChangeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NumericUpDown.UIASmallChangeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x0009F51C File Offset: 0x0009D71C
		private void wide_number_multiply_by_10(int[] number)
		{
			long num = 0L;
			for (int i = 0; i < number.Length; i++)
			{
				long num2 = num + (long)(10UL * (ulong)number[i]);
				num = num2 >> 32;
				number[i] = (int)num2;
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0009F558 File Offset: 0x0009D758
		private void wide_number_multiply_by_16(int[] number)
		{
			int num = 0;
			for (int i = 0; i < number.Length; i++)
			{
				int num2 = num | (number[i] << 4);
				num = (number[i] >> 28) & 15;
				number[i] = num2;
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0009F594 File Offset: 0x0009D794
		private void wide_number_divide_by_16(int[] number)
		{
			int num = 0;
			for (int i = number.Length - 1; i >= 0; i--)
			{
				int num2 = num | ((number[i] >> 4) & 268435455);
				num = number[i] << 28;
				number[i] = num2;
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009F5D4 File Offset: 0x0009D7D4
		private bool wide_number_less_than(int[] left, int[] right)
		{
			for (int i = left.Length - 1; i >= 0; i--)
			{
				uint num = (uint)left[i];
				uint num2 = (uint)right[i];
				if (num > num2)
				{
					return false;
				}
				if (num < num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0009F614 File Offset: 0x0009D814
		private void wide_number_subtract(int[] subtrahend, int[] minuend)
		{
			long num = 0L;
			for (int i = 0; i < subtrahend.Length; i++)
			{
				long num2 = (long)((ulong)subtrahend[i]);
				long num3 = (long)((ulong)minuend[i]);
				long num4 = num2 - num3 + num;
				if (num4 < 0L)
				{
					num = -1L;
					num4 -= -2147483648L;
					num4 -= -2147483648L;
				}
				else
				{
					num = 0L;
				}
				subtrahend[i] = (int)num4;
			}
		}

		/// <summary>Gets a collection of sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> containing the sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x0009F67C File Offset: 0x0009D87C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public NumericUpDownAccelerationCollection Accelerations
		{
			get
			{
				if (this.accelerations == null)
				{
					this.accelerations = new NumericUpDownAccelerationCollection();
				}
				return this.accelerations;
			}
		}

		/// <summary>Gets or sets the number of decimal places to display in the spin box (also known as an up-down control).</summary>
		/// <returns>The number of decimal places to display in the spin box. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than 0.-or- The value assigned is greater than 99. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x0009F69C File Offset: 0x0009D89C
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x0009F6A4 File Offset: 0x0009D8A4
		[DefaultValue(0)]
		public int DecimalPlaces
		{
			get
			{
				return this.decimal_places;
			}
			set
			{
				this.decimal_places = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets a value indicating whether the spin box (also known as an up-down control) should display the value it contains in hexadecimal format.</summary>
		/// <returns>true if the spin box should display its value in hexadecimal format; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x0009F6BC File Offset: 0x0009D8BC
		[DefaultValue(false)]
		public bool Hexadecimal
		{
			get
			{
				return this.hexadecimal;
			}
			set
			{
				this.hexadecimal = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when the up or down buttons are clicked.</summary>
		/// <returns>The value to increment or decrement the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property when the up or down buttons are clicked on the spin box. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is not greater than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x0009F6CC File Offset: 0x0009D8CC
		// (set) Token: 0x0600294A RID: 10570 RVA: 0x0009F6D4 File Offset: 0x0009D8D4
		public decimal Increment
		{
			get
			{
				return this.increment;
			}
			set
			{
				if (value < 0m)
				{
					throw new ArgumentOutOfRangeException("value", value, "NumericUpDown increment cannot be negative");
				}
				this.increment = value;
				this.OnUIASmallChangeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the maximum value for the spin box (also known as an up-down control).</summary>
		/// <returns>The maximum value for the spin box. The default value is 100.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x0009F710 File Offset: 0x0009D910
		// (set) Token: 0x0600294C RID: 10572 RVA: 0x0009F718 File Offset: 0x0009D918
		[RefreshProperties(1)]
		public decimal Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				this.maximum = value;
				if (this.minimum > this.maximum)
				{
					this.minimum = this.maximum;
				}
				if (this.dvalue > this.maximum)
				{
					this.Value = this.maximum;
				}
				this.OnUIAMaximumChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the minimum allowed value for the spin box (also known as an up-down control).</summary>
		/// <returns>The minimum allowed value for the spin box. The default value is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0009F77C File Offset: 0x0009D97C
		// (set) Token: 0x0600294E RID: 10574 RVA: 0x0009F784 File Offset: 0x0009D984
		[RefreshProperties(1)]
		public decimal Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				this.minimum = value;
				if (this.maximum < this.minimum)
				{
					this.maximum = this.minimum;
				}
				if (this.dvalue < this.minimum)
				{
					this.Value = this.minimum;
				}
				this.OnUIAMinimumChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x0009F7E8 File Offset: 0x0009D9E8
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x0009F7F0 File Offset: 0x0009D9F0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Padding Padding
		{
			get
			{
				return Padding.Empty;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the text to be displayed in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x0009F7F4 File Offset: 0x0009D9F4
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x0009F7FC File Offset: 0x0009D9FC
		[Bindable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a thousands separator is displayed in the spin box (also known as an up-down control) when appropriate.</summary>
		/// <returns>true if a thousands separator is displayed in the spin box when appropriate; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x0009F808 File Offset: 0x0009DA08
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0009F810 File Offset: 0x0009DA10
		[Localizable(true)]
		[DefaultValue(false)]
		public bool ThousandsSeparator
		{
			get
			{
				return this.thousands_separator;
			}
			set
			{
				this.thousands_separator = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets the value assigned to the spin box (also known as an up-down control).</summary>
		/// <returns>The numeric value of the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.NumericUpDown.Minimum" /> property value.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.NumericUpDown.Maximum" /> property value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x0009F820 File Offset: 0x0009DA20
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x0009F83C File Offset: 0x0009DA3C
		[Bindable(true)]
		public decimal Value
		{
			get
			{
				if (base.UserEdit)
				{
					this.ValidateEditText();
				}
				return this.dvalue;
			}
			set
			{
				if (value != this.dvalue)
				{
					if (!this.suppress_validation && (value < this.minimum || value > this.maximum))
					{
						throw new ArgumentOutOfRangeException("value", "NumericUpDown.Value must be within the specified Minimum and Maximum values");
					}
					this.dvalue = value;
					this.OnValueChanged(EventArgs.Empty);
					this.UpdateEditText();
				}
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002957 RID: 10583 RVA: 0x0009F8B0 File Offset: 0x0009DAB0
		public void BeginInit()
		{
			this.suppress_validation = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002958 RID: 10584 RVA: 0x0009F8BC File Offset: 0x0009DABC
		public void EndInit()
		{
			this.suppress_validation = false;
			this.Value = this.Check(this.dvalue);
			this.UpdateEditText();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.NumericUpDown" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002959 RID: 10585 RVA: 0x0009F8E0 File Offset: 0x0009DAE0
		public override string ToString()
		{
			return string.Format("{0}, Minimum = {1}, Maximum = {2}", base.ToString(), this.minimum, this.maximum);
		}

		/// <summary>Decrements the value of the spin box (also known as an up-down control).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600295A RID: 10586 RVA: 0x0009F914 File Offset: 0x0009DB14
		public override void DownButton()
		{
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			this.Value = Math.Max(this.minimum, this.dvalue - this.increment);
			base.OnUIADownButtonClick(EventArgs.Empty);
		}

		/// <summary>Increments the value of the spin box (also known as an up-down control).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600295B RID: 10587 RVA: 0x0009F960 File Offset: 0x0009DB60
		public override void UpButton()
		{
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			this.Value = Math.Min(this.maximum, this.dvalue + this.increment);
			base.OnUIAUpButtonClick(EventArgs.Empty);
		}

		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x0600295C RID: 10588 RVA: 0x0009F9AC File Offset: 0x0009DBAC
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new AccessibleObject(this)
			{
				role = AccessibleRole.SpinButton
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x0600295D RID: 10589 RVA: 0x0009F9CC File Offset: 0x0009DBCC
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			if ((Control.ModifierKeys & ~Keys.Shift) != Keys.None)
			{
				return;
			}
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			string text = e.KeyChar.ToString();
			if (text != numberFormat.NegativeSign && text != numberFormat.NumberDecimalSeparator && text != numberFormat.NumberGroupSeparator)
			{
				string text2 = ((!this.hexadecimal) ? "\b0123456789" : "\b0123456789abcdefABCDEF");
				if (text2.IndexOf(e.KeyChar) == -1)
				{
					e.Handled = true;
				}
			}
			base.OnTextBoxKeyPress(source, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.NumericUpDown.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600295E RID: 10590 RVA: 0x0009FA74 File Offset: 0x0009DC74
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NumericUpDown.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Converts the text displayed in the spin box (also known as an up-down control) to a numeric value and evaluates it.</summary>
		// Token: 0x0600295F RID: 10591 RVA: 0x0009FAA8 File Offset: 0x0009DCA8
		protected void ParseEditText()
		{
			try
			{
				if (!this.hexadecimal)
				{
					this.Value = this.Check(decimal.Parse(this.Text, CultureInfo.CurrentCulture));
				}
				else
				{
					this.Value = this.Check(Convert.ToDecimal(Convert.ToInt32(this.Text, 10)));
				}
			}
			catch
			{
			}
			finally
			{
				base.UserEdit = false;
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0009FB48 File Offset: 0x0009DD48
		private decimal Check(decimal val)
		{
			decimal num = val;
			if (num < this.minimum)
			{
				num = this.minimum;
			}
			if (num > this.maximum)
			{
				num = this.maximum;
			}
			return num;
		}

		/// <summary>Displays the current value of the spin box (also known as an up-down control) in the appropriate format.</summary>
		// Token: 0x06002961 RID: 10593 RVA: 0x0009FB88 File Offset: 0x0009DD88
		protected override void UpdateEditText()
		{
			if (this.suppress_validation)
			{
				return;
			}
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			base.ChangingText = true;
			if (!this.hexadecimal)
			{
				string text;
				if (this.thousands_separator)
				{
					text = "N";
				}
				else
				{
					text = "F";
				}
				text += this.decimal_places;
				this.Text = this.dvalue.ToString(text, CultureInfo.CurrentCulture);
			}
			else
			{
				int[] bits = decimal.GetBits(this.dvalue);
				bool flag = bits[3] < 0;
				int num = (bits[3] >> 16) & 31;
				bits[3] = 0;
				int[] array = new int[4];
				array[0] = 1;
				for (int i = 0; i < num; i++)
				{
					this.wide_number_multiply_by_10(array);
				}
				int num2 = 0;
				while (!this.wide_number_less_than(bits, array))
				{
					num2++;
					this.wide_number_multiply_by_16(array);
				}
				if (num2 == 0)
				{
					this.Text = "0";
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (flag)
				{
					stringBuilder.Append('-');
				}
				for (int j = 0; j < num2; j++)
				{
					int num3 = 0;
					this.wide_number_divide_by_16(array);
					while (!this.wide_number_less_than(bits, array))
					{
						num3++;
						this.wide_number_subtract(bits, array);
					}
					if (num3 < 10)
					{
						stringBuilder.Append((char)(48 + num3));
					}
					else
					{
						stringBuilder.Append((char)(65 + num3 - 10));
					}
				}
				this.Text = stringBuilder.ToString();
			}
		}

		/// <summary>Validates and updates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x06002962 RID: 10594 RVA: 0x0009FD28 File Offset: 0x0009DF28
		protected override void ValidateEditText()
		{
			this.ParseEditText();
			this.UpdateEditText();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002963 RID: 10595 RVA: 0x0009FD38 File Offset: 0x0009DF38
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (base.UserEdit)
			{
				this.UpdateEditText();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06002964 RID: 10596 RVA: 0x0009FD54 File Offset: 0x0009DF54
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06002965 RID: 10597 RVA: 0x0009FD60 File Offset: 0x0009DF60
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		// Token: 0x0400148B RID: 5259
		private bool suppress_validation;

		// Token: 0x0400148C RID: 5260
		private int decimal_places;

		// Token: 0x0400148D RID: 5261
		private bool hexadecimal;

		// Token: 0x0400148E RID: 5262
		private decimal increment;

		// Token: 0x0400148F RID: 5263
		private decimal maximum;

		// Token: 0x04001490 RID: 5264
		private decimal minimum;

		// Token: 0x04001491 RID: 5265
		private bool thousands_separator;

		// Token: 0x04001492 RID: 5266
		private decimal dvalue;

		// Token: 0x04001493 RID: 5267
		private NumericUpDownAccelerationCollection accelerations;
	}
}
