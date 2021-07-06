using System;
using Xunit;

namespace Examples
{
    // based on https://twitter.com/badamczewski01/status/1411723215479529472
    // needs AllowUnsafeBlocks
    public class Examples
    {
        [Fact]
        public void LengthIsStoredAtIndexNegative2()
        {
            var array = new int[8];
            int length;
            unsafe
            {
                fixed (int* p = &array[0])
                {
                    Assert.Equal(*(p - 2), 8);
                    length = *(p - 2);
                }
            }
            Assert.Equal(array.Length, length);
        }

        [Fact]
        public void LengthCanBeChanged()
        {
            var array = new int[8];
            int length;
            unsafe
            {
                fixed (int* p = &array[0])
                {
                    p[-2] = 42;
                    Assert.Equal(*(p - 2), 42);
                    length = *(p - 2);
                }
            }
            Assert.Equal(array.Length, length);

            array[41] = 8;
            Assert.Equal(8, array[41]);
        }
    }
}
