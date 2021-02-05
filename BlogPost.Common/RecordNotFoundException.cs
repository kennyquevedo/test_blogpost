using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Common
{
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {
        }

        public RecordNotFoundException(string message)
            : base(message)
        {
        }

        public RecordNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
