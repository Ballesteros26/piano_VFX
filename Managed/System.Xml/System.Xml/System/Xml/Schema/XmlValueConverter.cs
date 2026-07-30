using System;

namespace System.Xml.Schema
{
	// Token: 0x02000492 RID: 1170
	internal abstract class XmlValueConverter
	{
		// Token: 0x06002E17 RID: 11799
		public abstract bool ToBoolean(bool value);

		// Token: 0x06002E18 RID: 11800
		public abstract bool ToBoolean(long value);

		// Token: 0x06002E19 RID: 11801
		public abstract bool ToBoolean(int value);

		// Token: 0x06002E1A RID: 11802
		public abstract bool ToBoolean(decimal value);

		// Token: 0x06002E1B RID: 11803
		public abstract bool ToBoolean(float value);

		// Token: 0x06002E1C RID: 11804
		public abstract bool ToBoolean(double value);

		// Token: 0x06002E1D RID: 11805
		public abstract bool ToBoolean(DateTime value);

		// Token: 0x06002E1E RID: 11806
		public abstract bool ToBoolean(DateTimeOffset value);

		// Token: 0x06002E1F RID: 11807
		public abstract bool ToBoolean(string value);

		// Token: 0x06002E20 RID: 11808
		public abstract bool ToBoolean(object value);

		// Token: 0x06002E21 RID: 11809
		public abstract int ToInt32(bool value);

		// Token: 0x06002E22 RID: 11810
		public abstract int ToInt32(int value);

		// Token: 0x06002E23 RID: 11811
		public abstract int ToInt32(long value);

		// Token: 0x06002E24 RID: 11812
		public abstract int ToInt32(decimal value);

		// Token: 0x06002E25 RID: 11813
		public abstract int ToInt32(float value);

		// Token: 0x06002E26 RID: 11814
		public abstract int ToInt32(double value);

		// Token: 0x06002E27 RID: 11815
		public abstract int ToInt32(DateTime value);

		// Token: 0x06002E28 RID: 11816
		public abstract int ToInt32(DateTimeOffset value);

		// Token: 0x06002E29 RID: 11817
		public abstract int ToInt32(string value);

		// Token: 0x06002E2A RID: 11818
		public abstract int ToInt32(object value);

		// Token: 0x06002E2B RID: 11819
		public abstract long ToInt64(bool value);

		// Token: 0x06002E2C RID: 11820
		public abstract long ToInt64(int value);

		// Token: 0x06002E2D RID: 11821
		public abstract long ToInt64(long value);

		// Token: 0x06002E2E RID: 11822
		public abstract long ToInt64(decimal value);

		// Token: 0x06002E2F RID: 11823
		public abstract long ToInt64(float value);

		// Token: 0x06002E30 RID: 11824
		public abstract long ToInt64(double value);

		// Token: 0x06002E31 RID: 11825
		public abstract long ToInt64(DateTime value);

		// Token: 0x06002E32 RID: 11826
		public abstract long ToInt64(DateTimeOffset value);

		// Token: 0x06002E33 RID: 11827
		public abstract long ToInt64(string value);

		// Token: 0x06002E34 RID: 11828
		public abstract long ToInt64(object value);

		// Token: 0x06002E35 RID: 11829
		public abstract decimal ToDecimal(bool value);

		// Token: 0x06002E36 RID: 11830
		public abstract decimal ToDecimal(int value);

		// Token: 0x06002E37 RID: 11831
		public abstract decimal ToDecimal(long value);

		// Token: 0x06002E38 RID: 11832
		public abstract decimal ToDecimal(decimal value);

		// Token: 0x06002E39 RID: 11833
		public abstract decimal ToDecimal(float value);

		// Token: 0x06002E3A RID: 11834
		public abstract decimal ToDecimal(double value);

		// Token: 0x06002E3B RID: 11835
		public abstract decimal ToDecimal(DateTime value);

		// Token: 0x06002E3C RID: 11836
		public abstract decimal ToDecimal(DateTimeOffset value);

		// Token: 0x06002E3D RID: 11837
		public abstract decimal ToDecimal(string value);

		// Token: 0x06002E3E RID: 11838
		public abstract decimal ToDecimal(object value);

		// Token: 0x06002E3F RID: 11839
		public abstract double ToDouble(bool value);

		// Token: 0x06002E40 RID: 11840
		public abstract double ToDouble(int value);

		// Token: 0x06002E41 RID: 11841
		public abstract double ToDouble(long value);

		// Token: 0x06002E42 RID: 11842
		public abstract double ToDouble(decimal value);

		// Token: 0x06002E43 RID: 11843
		public abstract double ToDouble(float value);

		// Token: 0x06002E44 RID: 11844
		public abstract double ToDouble(double value);

		// Token: 0x06002E45 RID: 11845
		public abstract double ToDouble(DateTime value);

		// Token: 0x06002E46 RID: 11846
		public abstract double ToDouble(DateTimeOffset value);

		// Token: 0x06002E47 RID: 11847
		public abstract double ToDouble(string value);

		// Token: 0x06002E48 RID: 11848
		public abstract double ToDouble(object value);

		// Token: 0x06002E49 RID: 11849
		public abstract float ToSingle(bool value);

		// Token: 0x06002E4A RID: 11850
		public abstract float ToSingle(int value);

		// Token: 0x06002E4B RID: 11851
		public abstract float ToSingle(long value);

		// Token: 0x06002E4C RID: 11852
		public abstract float ToSingle(decimal value);

		// Token: 0x06002E4D RID: 11853
		public abstract float ToSingle(float value);

		// Token: 0x06002E4E RID: 11854
		public abstract float ToSingle(double value);

		// Token: 0x06002E4F RID: 11855
		public abstract float ToSingle(DateTime value);

		// Token: 0x06002E50 RID: 11856
		public abstract float ToSingle(DateTimeOffset value);

		// Token: 0x06002E51 RID: 11857
		public abstract float ToSingle(string value);

		// Token: 0x06002E52 RID: 11858
		public abstract float ToSingle(object value);

		// Token: 0x06002E53 RID: 11859
		public abstract DateTime ToDateTime(bool value);

		// Token: 0x06002E54 RID: 11860
		public abstract DateTime ToDateTime(int value);

		// Token: 0x06002E55 RID: 11861
		public abstract DateTime ToDateTime(long value);

		// Token: 0x06002E56 RID: 11862
		public abstract DateTime ToDateTime(decimal value);

		// Token: 0x06002E57 RID: 11863
		public abstract DateTime ToDateTime(float value);

		// Token: 0x06002E58 RID: 11864
		public abstract DateTime ToDateTime(double value);

		// Token: 0x06002E59 RID: 11865
		public abstract DateTime ToDateTime(DateTime value);

		// Token: 0x06002E5A RID: 11866
		public abstract DateTime ToDateTime(DateTimeOffset value);

		// Token: 0x06002E5B RID: 11867
		public abstract DateTime ToDateTime(string value);

		// Token: 0x06002E5C RID: 11868
		public abstract DateTime ToDateTime(object value);

		// Token: 0x06002E5D RID: 11869
		public abstract DateTimeOffset ToDateTimeOffset(bool value);

		// Token: 0x06002E5E RID: 11870
		public abstract DateTimeOffset ToDateTimeOffset(int value);

		// Token: 0x06002E5F RID: 11871
		public abstract DateTimeOffset ToDateTimeOffset(long value);

		// Token: 0x06002E60 RID: 11872
		public abstract DateTimeOffset ToDateTimeOffset(decimal value);

		// Token: 0x06002E61 RID: 11873
		public abstract DateTimeOffset ToDateTimeOffset(float value);

		// Token: 0x06002E62 RID: 11874
		public abstract DateTimeOffset ToDateTimeOffset(double value);

		// Token: 0x06002E63 RID: 11875
		public abstract DateTimeOffset ToDateTimeOffset(DateTime value);

		// Token: 0x06002E64 RID: 11876
		public abstract DateTimeOffset ToDateTimeOffset(DateTimeOffset value);

		// Token: 0x06002E65 RID: 11877
		public abstract DateTimeOffset ToDateTimeOffset(string value);

		// Token: 0x06002E66 RID: 11878
		public abstract DateTimeOffset ToDateTimeOffset(object value);

		// Token: 0x06002E67 RID: 11879
		public abstract string ToString(bool value);

		// Token: 0x06002E68 RID: 11880
		public abstract string ToString(int value);

		// Token: 0x06002E69 RID: 11881
		public abstract string ToString(long value);

		// Token: 0x06002E6A RID: 11882
		public abstract string ToString(decimal value);

		// Token: 0x06002E6B RID: 11883
		public abstract string ToString(float value);

		// Token: 0x06002E6C RID: 11884
		public abstract string ToString(double value);

		// Token: 0x06002E6D RID: 11885
		public abstract string ToString(DateTime value);

		// Token: 0x06002E6E RID: 11886
		public abstract string ToString(DateTimeOffset value);

		// Token: 0x06002E6F RID: 11887
		public abstract string ToString(string value);

		// Token: 0x06002E70 RID: 11888
		public abstract string ToString(string value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002E71 RID: 11889
		public abstract string ToString(object value);

		// Token: 0x06002E72 RID: 11890
		public abstract string ToString(object value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002E73 RID: 11891
		public abstract object ChangeType(bool value, Type destinationType);

		// Token: 0x06002E74 RID: 11892
		public abstract object ChangeType(int value, Type destinationType);

		// Token: 0x06002E75 RID: 11893
		public abstract object ChangeType(long value, Type destinationType);

		// Token: 0x06002E76 RID: 11894
		public abstract object ChangeType(decimal value, Type destinationType);

		// Token: 0x06002E77 RID: 11895
		public abstract object ChangeType(float value, Type destinationType);

		// Token: 0x06002E78 RID: 11896
		public abstract object ChangeType(double value, Type destinationType);

		// Token: 0x06002E79 RID: 11897
		public abstract object ChangeType(DateTime value, Type destinationType);

		// Token: 0x06002E7A RID: 11898
		public abstract object ChangeType(DateTimeOffset value, Type destinationType);

		// Token: 0x06002E7B RID: 11899
		public abstract object ChangeType(string value, Type destinationType);

		// Token: 0x06002E7C RID: 11900
		public abstract object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002E7D RID: 11901
		public abstract object ChangeType(object value, Type destinationType);

		// Token: 0x06002E7E RID: 11902
		public abstract object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver);
	}
}
