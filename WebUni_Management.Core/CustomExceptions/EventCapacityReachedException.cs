using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class EventCapacityReachedException : Exception
    {
        public EventCapacityReachedException(string message) : base(message)
        {
        }
    }

