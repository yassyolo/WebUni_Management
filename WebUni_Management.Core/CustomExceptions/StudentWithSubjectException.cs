using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StudentWithSubjectException : Exception
{
    public StudentWithSubjectException(string message)
        : base(message)
    {
    }
}
