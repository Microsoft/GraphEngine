using System;
using Xunit;

using Python.Runtime;
using System.Collections.Generic;

namespace Trinity.FFI.Python.UnitTests
{
    public class PythonNetTests
    {
        public bool ApproxEqual(double a, double b)
        {
            return Math.Abs(a - b) < 0.00001;
        }

        [Fact]
        void pythonToNETCallback()
        {
            using (Py.GIL())
            {
                //new PyObject()
            }
        }

        [Fact]
        public void inlinePythonCode_persistent()
        {
            string pcode = @"
import numpy as np
a = np.array([1,2,3])
print(a.tolist())
";

            string pcode2 = @"
b = np.array([1,2,3])
print(b.tolist())";

            using (Py.GIL())
            {
                PythonEngine.Exec(pcode);
                PythonEngine.Exec(pcode2);
            }

            string pcode3 = @"
c = np.array([1,2,3])
print(c.tolist())";

            using (Py.GIL())
            {
                PythonEngine.Exec(pcode3);
            }
        }


        [Fact]
        public void inlinePythonCode()
        {
            string pcode = @"
import numpy as np
a = np.array([1,2,3])
print(a.tolist())
";

            using (Py.GIL())
            {
                PythonEngine.Exec(pcode);
            }
        }

        [Fact]
        public void PythonNetWorksHere()
        {
            using (Py.GIL())
            {
                dynamic np = Py.Import("numpy");
                Assert.True(ApproxEqual((double)np.cos(np.pi * 2), 1.0));

                dynamic sin = np.sin;
                Assert.True(ApproxEqual((double)sin(5), Math.Sin(5.0)));

                double c = np.cos(5) + sin(5);
                Assert.True(ApproxEqual(c, Math.Sin(5.0) + Math.Cos(5.0)));

                dynamic a = np.array(new List<float> { 1, 2, 3 });
                Assert.Equal(np.float64, a.dtype);

                dynamic b = np.array(new List<float> { 6, 5, 4 }, dtype: np.int32);
                Assert.Equal(np.float64, a.dtype);

                dynamic elementwise_product = (a * b).tolist();
                Assert.True(ApproxEqual(6, (double)elementwise_product[0]));
                Assert.True(ApproxEqual(10, (double)elementwise_product[1]));
                Assert.True(ApproxEqual(12, (double)elementwise_product[2]));
            }
        }
    }
}
