// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("wptuHjJz4btowTcmWMCjvJLcxp6q1PoZ4Z2RWQFNK6oXl/0LzhOX5jkJLiQG3dOfClTv2sm9Jf83vCINL1X6lMu+9HM13HMTc/hK3uhd0ifdhN4++8X+oLBdGRexlHATfpgK/LOHNdBqcmNJBiUKjD1PwNm8JLnQ9+Ol4I1wAqamj4L4e/fY/xEogT/jBmlqcHrlHYJbi2ZEePXqwMgDqHaIsefa3i3x9LdYsK8WP3J/mfhLjvxTyPq+pv56AphAPQXouQZ2cPqICwUKOogLAAiICwsKqJw/QksByzqICyg6BwwDIIxCjP0HCwsLDwoJ/yo161/ipmS1/Qb9PJw0aUREhGwTocJFDWkBKUM4fP6gm2FLqHwgwx0CcNp6XXpVWQgJCwoL");
        private static int[] order = new int[] { 10,11,6,13,13,6,9,13,8,9,11,11,13,13,14 };
        private static int key = 10;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
