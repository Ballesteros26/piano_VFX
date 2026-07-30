using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x02000037 RID: 55
	public class DebugUI
	{
		// Token: 0x020000C1 RID: 193
		public class Container : DebugUI.Widget, DebugUI.IContainer
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001146C File Offset: 0x0000F66C
			// (set) Token: 0x060004AF RID: 1199 RVA: 0x00011474 File Offset: 0x0000F674
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001147D File Offset: 0x0000F67D
			// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00011488 File Offset: 0x0000F688
			public override DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
					foreach (DebugUI.Widget widget in this.children)
					{
						widget.panel = value;
					}
				}
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x000114DC File Offset: 0x0000F6DC
			public Container()
			{
				base.displayName = "";
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x00011535 File Offset: 0x0000F735
			public Container(string displayName, ObservableList<DebugUI.Widget> children)
			{
				base.displayName = displayName;
				this.children = children;
				children.ItemAdded += this.OnItemAdded;
				children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x00011574 File Offset: 0x0000F774
			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x000115C4 File Offset: 0x0000F7C4
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this.m_Panel;
					e.item.parent = this;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x000115FE File Offset: 0x0000F7FE
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x00011634 File Offset: 0x0000F834
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + base.queryPath.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}
		}

		// Token: 0x020000C2 RID: 194
		public class Foldout : DebugUI.Container, DebugUI.IValueField
		{
			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00005672 File Offset: 0x00003872
			public bool isReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001169C File Offset: 0x0000F89C
			// (set) Token: 0x060004BA RID: 1210 RVA: 0x000116A4 File Offset: 0x0000F8A4
			public string[] columnLabels { get; set; }

			// Token: 0x060004BB RID: 1211 RVA: 0x000116AD File Offset: 0x0000F8AD
			public Foldout()
			{
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x000116B5 File Offset: 0x0000F8B5
			public Foldout(string displayName, ObservableList<DebugUI.Widget> children, string[] columnLabels = null)
				: base(displayName, children)
			{
				this.columnLabels = columnLabels;
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x000116C6 File Offset: 0x0000F8C6
			public bool GetValue()
			{
				return this.opened;
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x000116CE File Offset: 0x0000F8CE
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x000116DB File Offset: 0x0000F8DB
			public void SetValue(object value)
			{
				this.SetValue((bool)value);
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x000116E9 File Offset: 0x0000F8E9
			public object ValidateValue(object value)
			{
				return value;
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x000116EC File Offset: 0x0000F8EC
			public void SetValue(bool value)
			{
				this.opened = value;
			}

			// Token: 0x0400027A RID: 634
			public bool opened;
		}

		// Token: 0x020000C3 RID: 195
		public class HBox : DebugUI.Container
		{
			// Token: 0x060004C2 RID: 1218 RVA: 0x000116F5 File Offset: 0x0000F8F5
			public HBox()
			{
				base.displayName = "HBox";
			}
		}

		// Token: 0x020000C4 RID: 196
		public class VBox : DebugUI.Container
		{
			// Token: 0x060004C3 RID: 1219 RVA: 0x00011708 File Offset: 0x0000F908
			public VBox()
			{
				base.displayName = "VBox";
			}
		}

		// Token: 0x020000C5 RID: 197
		public abstract class Field<T> : DebugUI.Widget, DebugUI.IValueField
		{
			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001171B File Offset: 0x0000F91B
			// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00011723 File Offset: 0x0000F923
			public Func<T> getter { get; set; }

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001172C File Offset: 0x0000F92C
			// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00011734 File Offset: 0x0000F934
			public Action<T> setter { get; set; }

			// Token: 0x060004C8 RID: 1224 RVA: 0x0001173D File Offset: 0x0000F93D
			object DebugUI.IValueField.ValidateValue(object value)
			{
				return this.ValidateValue((T)((object)value));
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x000116E9 File Offset: 0x0000F8E9
			public virtual T ValidateValue(T value)
			{
				return value;
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x00011750 File Offset: 0x0000F950
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0001175D File Offset: 0x0000F95D
			public T GetValue()
			{
				return this.getter();
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0001176A File Offset: 0x0000F96A
			public void SetValue(object value)
			{
				this.SetValue((T)((object)value));
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00011778 File Offset: 0x0000F978
			public void SetValue(T value)
			{
				T t = this.ValidateValue(value);
				if (!t.Equals(this.getter()))
				{
					this.setter(t);
					if (this.onValueChanged != null)
					{
						this.onValueChanged(this, t);
					}
				}
			}

			// Token: 0x0400027E RID: 638
			public Action<DebugUI.Field<T>, T> onValueChanged;
		}

		// Token: 0x020000C6 RID: 198
		public class BoolField : DebugUI.Field<bool>
		{
		}

		// Token: 0x020000C7 RID: 199
		public class HistoryBoolField : DebugUI.BoolField
		{
			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000117DD File Offset: 0x0000F9DD
			// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000117E5 File Offset: 0x0000F9E5
			public Func<bool>[] historyGetter { get; set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000117EE File Offset: 0x0000F9EE
			public int historyDepth
			{
				get
				{
					Func<bool>[] historyGetter = this.historyGetter;
					if (historyGetter == null)
					{
						return 0;
					}
					return historyGetter.Length;
				}
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x000117FE File Offset: 0x0000F9FE
			public bool GetHistoryValue(int historyIndex)
			{
				return this.historyGetter[historyIndex]();
			}
		}

		// Token: 0x020000C8 RID: 200
		public class IntField : DebugUI.Field<int>
		{
			// Token: 0x060004D5 RID: 1237 RVA: 0x00011815 File Offset: 0x0000FA15
			public override int ValidateValue(int value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x04000280 RID: 640
			public Func<int> min;

			// Token: 0x04000281 RID: 641
			public Func<int> max;

			// Token: 0x04000282 RID: 642
			public int incStep = 1;

			// Token: 0x04000283 RID: 643
			public int intStepMult = 10;
		}

		// Token: 0x020000C9 RID: 201
		public class UIntField : DebugUI.Field<uint>
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x00011865 File Offset: 0x0000FA65
			public override uint ValidateValue(uint value)
			{
				if (this.min != null)
				{
					value = (uint)Mathf.Max((int)value, (int)this.min());
				}
				if (this.max != null)
				{
					value = (uint)Mathf.Min((int)value, (int)this.max());
				}
				return value;
			}

			// Token: 0x04000284 RID: 644
			public Func<uint> min;

			// Token: 0x04000285 RID: 645
			public Func<uint> max;

			// Token: 0x04000286 RID: 646
			public uint incStep = 1U;

			// Token: 0x04000287 RID: 647
			public uint intStepMult = 10U;
		}

		// Token: 0x020000CA RID: 202
		public class FloatField : DebugUI.Field<float>
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x000118B5 File Offset: 0x0000FAB5
			public override float ValidateValue(float value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x04000288 RID: 648
			public Func<float> min;

			// Token: 0x04000289 RID: 649
			public Func<float> max;

			// Token: 0x0400028A RID: 650
			public float incStep = 0.1f;

			// Token: 0x0400028B RID: 651
			public float incStepMult = 10f;

			// Token: 0x0400028C RID: 652
			public int decimals = 3;
		}

		// Token: 0x020000CB RID: 203
		public class EnumField : DebugUI.Field<int>
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x00011913 File Offset: 0x0000FB13
			// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001191B File Offset: 0x0000FB1B
			public Func<int> getIndex { get; set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x00011924 File Offset: 0x0000FB24
			// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001192C File Offset: 0x0000FB2C
			public Action<int> setIndex { get; set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060004DF RID: 1247 RVA: 0x00011935 File Offset: 0x0000FB35
			// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00011942 File Offset: 0x0000FB42
			public int currentIndex
			{
				get
				{
					return this.getIndex();
				}
				set
				{
					this.setIndex(value);
				}
			}

			// Token: 0x170000A8 RID: 168
			// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00011950 File Offset: 0x0000FB50
			public Type autoEnum
			{
				set
				{
					this.enumNames = (from x in Enum.GetNames(value)
						select new GUIContent(x)).ToArray<GUIContent>();
					Array values = Enum.GetValues(value);
					this.enumValues = new int[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						this.enumValues[i] = (int)values.GetValue(i);
					}
					this.InitIndexes();
					this.InitQuickSeparators();
				}
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x000119DC File Offset: 0x0000FBDC
			internal void InitQuickSeparators()
			{
				IEnumerable<string> enumerable = this.enumNames.Select(delegate(GUIContent x)
				{
					string[] array = x.text.Split(new char[] { '/' });
					if (array.Length == 1)
					{
						return "";
					}
					return array[0];
				});
				this.quickSeparators = new int[enumerable.Distinct<string>().Count<string>()];
				string text = null;
				int i = 0;
				int num = 0;
				while (i < this.quickSeparators.Length)
				{
					string text2 = enumerable.ElementAt(num);
					while (text == text2)
					{
						text2 = enumerable.ElementAt(++num);
					}
					text = text2;
					this.quickSeparators[i] = num++;
					i++;
				}
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00011A74 File Offset: 0x0000FC74
			internal void InitIndexes()
			{
				this.indexes = new int[this.enumNames.Length];
				for (int i = 0; i < this.enumNames.Length; i++)
				{
					this.indexes[i] = i;
				}
			}

			// Token: 0x0400028D RID: 653
			public GUIContent[] enumNames;

			// Token: 0x0400028E RID: 654
			public int[] enumValues;

			// Token: 0x0400028F RID: 655
			internal int[] quickSeparators;

			// Token: 0x04000290 RID: 656
			internal int[] indexes;
		}

		// Token: 0x020000CC RID: 204
		public class HistoryEnumField : DebugUI.EnumField
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011AB8 File Offset: 0x0000FCB8
			// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00011AC0 File Offset: 0x0000FCC0
			public Func<int>[] historyIndexGetter { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00011AC9 File Offset: 0x0000FCC9
			public int historyDepth
			{
				get
				{
					Func<int>[] historyIndexGetter = this.historyIndexGetter;
					if (historyIndexGetter == null)
					{
						return 0;
					}
					return historyIndexGetter.Length;
				}
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00011AD9 File Offset: 0x0000FCD9
			public int GetHistoryValue(int historyIndex)
			{
				return this.historyIndexGetter[historyIndex]();
			}
		}

		// Token: 0x020000CD RID: 205
		public class BitField : DebugUI.Field<Enum>
		{
			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x00011AF0 File Offset: 0x0000FCF0
			// (set) Token: 0x060004EB RID: 1259 RVA: 0x00011AF8 File Offset: 0x0000FCF8
			public GUIContent[] enumNames { get; private set; }

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x00011B01 File Offset: 0x0000FD01
			// (set) Token: 0x060004ED RID: 1261 RVA: 0x00011B09 File Offset: 0x0000FD09
			public int[] enumValues { get; private set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00011B9A File Offset: 0x0000FD9A
			// (set) Token: 0x060004EE RID: 1262 RVA: 0x00011B14 File Offset: 0x0000FD14
			public Type enumType
			{
				get
				{
					return this.m_EnumType;
				}
				set
				{
					this.enumNames = (from x in Enum.GetNames(value)
						select new GUIContent(x)).ToArray<GUIContent>();
					Array values = Enum.GetValues(value);
					this.enumValues = new int[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						this.enumValues[i] = (int)values.GetValue(i);
					}
					this.m_EnumType = value;
				}
			}

			// Token: 0x04000296 RID: 662
			internal Type m_EnumType;
		}

		// Token: 0x020000CE RID: 206
		public class ColorField : DebugUI.Field<Color>
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x00011BAC File Offset: 0x0000FDAC
			public override Color ValidateValue(Color value)
			{
				if (!this.hdr)
				{
					value.r = Mathf.Clamp01(value.r);
					value.g = Mathf.Clamp01(value.g);
					value.b = Mathf.Clamp01(value.b);
					value.a = Mathf.Clamp01(value.a);
				}
				return value;
			}

			// Token: 0x04000297 RID: 663
			public bool hdr;

			// Token: 0x04000298 RID: 664
			public bool showAlpha = true;

			// Token: 0x04000299 RID: 665
			public bool showPicker = true;

			// Token: 0x0400029A RID: 666
			public float incStep = 0.025f;

			// Token: 0x0400029B RID: 667
			public float incStepMult = 5f;

			// Token: 0x0400029C RID: 668
			public int decimals = 3;
		}

		// Token: 0x020000CF RID: 207
		public class Vector2Field : DebugUI.Field<Vector2>
		{
			// Token: 0x0400029D RID: 669
			public float incStep = 0.025f;

			// Token: 0x0400029E RID: 670
			public float incStepMult = 10f;

			// Token: 0x0400029F RID: 671
			public int decimals = 3;
		}

		// Token: 0x020000D0 RID: 208
		public class Vector3Field : DebugUI.Field<Vector3>
		{
			// Token: 0x040002A0 RID: 672
			public float incStep = 0.025f;

			// Token: 0x040002A1 RID: 673
			public float incStepMult = 10f;

			// Token: 0x040002A2 RID: 674
			public int decimals = 3;
		}

		// Token: 0x020000D1 RID: 209
		public class Vector4Field : DebugUI.Field<Vector4>
		{
			// Token: 0x040002A3 RID: 675
			public float incStep = 0.025f;

			// Token: 0x040002A4 RID: 676
			public float incStepMult = 10f;

			// Token: 0x040002A5 RID: 677
			public int decimals = 3;
		}

		// Token: 0x020000D2 RID: 210
		public class Panel : DebugUI.IContainer, IComparable<DebugUI.Panel>
		{
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00011CAC File Offset: 0x0000FEAC
			// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00011CB4 File Offset: 0x0000FEB4
			public DebugUI.Flags flags { get; set; }

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00011CBD File Offset: 0x0000FEBD
			// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00011CC5 File Offset: 0x0000FEC5
			public string displayName { get; set; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x00011CCE File Offset: 0x0000FECE
			// (set) Token: 0x060004FB RID: 1275 RVA: 0x00011CD6 File Offset: 0x0000FED6
			public int groupIndex { get; set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x00011CDF File Offset: 0x0000FEDF
			public string queryPath
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x00011CE7 File Offset: 0x0000FEE7
			public bool isEditorOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x00011CF4 File Offset: 0x0000FEF4
			public bool isRuntimeOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.RuntimeOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x00011D01 File Offset: 0x0000FF01
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x00011D15 File Offset: 0x0000FF15
			public bool editorForceUpdate
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorForceUpdate) > DebugUI.Flags.None;
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x00011D22 File Offset: 0x0000FF22
			// (set) Token: 0x06000502 RID: 1282 RVA: 0x00011D2A File Offset: 0x0000FF2A
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x14000006 RID: 6
			// (add) Token: 0x06000503 RID: 1283 RVA: 0x00011D34 File Offset: 0x0000FF34
			// (remove) Token: 0x06000504 RID: 1284 RVA: 0x00011D6C File Offset: 0x0000FF6C
			public event Action<DebugUI.Panel> onSetDirty = delegate
			{
			};

			// Token: 0x06000505 RID: 1285 RVA: 0x00011DA4 File Offset: 0x0000FFA4
			public Panel()
			{
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x00011E17 File Offset: 0x00010017
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this;
					e.item.parent = this;
				}
				this.SetDirty();
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x00011E3F File Offset: 0x0001003F
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				this.SetDirty();
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x00011E68 File Offset: 0x00010068
			public void SetDirty()
			{
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
				this.onSetDirty(this);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00011EC0 File Offset: 0x000100C0
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + this.displayName.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x00011F28 File Offset: 0x00010128
			int IComparable<DebugUI.Panel>.CompareTo(DebugUI.Panel other)
			{
				if (other != null)
				{
					return this.groupIndex.CompareTo(other.groupIndex);
				}
				return 1;
			}
		}

		// Token: 0x020000D3 RID: 211
		[Flags]
		public enum Flags
		{
			// Token: 0x040002AC RID: 684
			None = 0,
			// Token: 0x040002AD RID: 685
			EditorOnly = 2,
			// Token: 0x040002AE RID: 686
			RuntimeOnly = 4,
			// Token: 0x040002AF RID: 687
			EditorForceUpdate = 8
		}

		// Token: 0x020000D4 RID: 212
		public abstract class Widget
		{
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001147D File Offset: 0x0000F67D
			// (set) Token: 0x0600050C RID: 1292 RVA: 0x00011F4E File Offset: 0x0001014E
			public virtual DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x00011F57 File Offset: 0x00010157
			// (set) Token: 0x0600050E RID: 1294 RVA: 0x00011F5F File Offset: 0x0001015F
			public virtual DebugUI.IContainer parent
			{
				get
				{
					return this.m_Parent;
				}
				internal set
				{
					this.m_Parent = value;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x00011F68 File Offset: 0x00010168
			// (set) Token: 0x06000510 RID: 1296 RVA: 0x00011F70 File Offset: 0x00010170
			public DebugUI.Flags flags { get; set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x00011F79 File Offset: 0x00010179
			// (set) Token: 0x06000512 RID: 1298 RVA: 0x00011F81 File Offset: 0x00010181
			public string displayName { get; set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x00011F8A File Offset: 0x0001018A
			// (set) Token: 0x06000514 RID: 1300 RVA: 0x00011F92 File Offset: 0x00010192
			public string queryPath { get; private set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011F9B File Offset: 0x0001019B
			public bool isEditorOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011FA8 File Offset: 0x000101A8
			public bool isRuntimeOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.RuntimeOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x00011FB5 File Offset: 0x000101B5
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x00011FC9 File Offset: 0x000101C9
			internal virtual void GenerateQueryPath()
			{
				this.queryPath = this.displayName.Trim();
				if (this.m_Parent != null)
				{
					this.queryPath = this.m_Parent.queryPath + " -> " + this.queryPath;
				}
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x00012005 File Offset: 0x00010205
			public override int GetHashCode()
			{
				return this.queryPath.GetHashCode();
			}

			// Token: 0x040002B0 RID: 688
			protected DebugUI.Panel m_Panel;

			// Token: 0x040002B1 RID: 689
			protected DebugUI.IContainer m_Parent;
		}

		// Token: 0x020000D5 RID: 213
		public interface IContainer
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600051B RID: 1307
			ObservableList<DebugUI.Widget> children { get; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x0600051C RID: 1308
			// (set) Token: 0x0600051D RID: 1309
			string displayName { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600051E RID: 1310
			string queryPath { get; }
		}

		// Token: 0x020000D6 RID: 214
		public interface IValueField
		{
			// Token: 0x0600051F RID: 1311
			object GetValue();

			// Token: 0x06000520 RID: 1312
			void SetValue(object value);

			// Token: 0x06000521 RID: 1313
			object ValidateValue(object value);
		}

		// Token: 0x020000D7 RID: 215
		public class Button : DebugUI.Widget
		{
			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000522 RID: 1314 RVA: 0x00012012 File Offset: 0x00010212
			// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001201A File Offset: 0x0001021A
			public Action action { get; set; }
		}

		// Token: 0x020000D8 RID: 216
		public class Value : DebugUI.Widget
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000525 RID: 1317 RVA: 0x00012023 File Offset: 0x00010223
			// (set) Token: 0x06000526 RID: 1318 RVA: 0x0001202B File Offset: 0x0001022B
			public Func<object> getter { get; set; }

			// Token: 0x06000527 RID: 1319 RVA: 0x00012034 File Offset: 0x00010234
			public object GetValue()
			{
				return this.getter();
			}

			// Token: 0x040002B7 RID: 695
			public float refreshRate = 0.1f;
		}
	}
}
