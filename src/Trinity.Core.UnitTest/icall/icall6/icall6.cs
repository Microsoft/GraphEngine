using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Trinity;
using Trinity.Storage;

namespace icall6
{
    public class icall6
    {
        [Fact]
        public void icall_loadcell()
        {
            var result = Global.LocalStorage.LoadCellAsync(0).Result;
            var val = result.ErrorCode;
            byte[] content = result.CellBuff;
            ushort type = result.CellType;
        }
    }
}
