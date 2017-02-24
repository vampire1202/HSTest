using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR_Test
{
    class SelTestSample
    {
        private string _selTestSample;
        public string _SelTestSample
        {
            get { return _selTestSample; }
            set { _selTestSample = value; }
        }

        private int _rowIndex;
        public int _RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }
    }
}
