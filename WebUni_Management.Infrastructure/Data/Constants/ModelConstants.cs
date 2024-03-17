using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Infrastructure.Data.Constants
{
    public static class ModelConstants
    {
        public class Student 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 15;
            public const int AgeMinValue = 18;
            public const int AgeMaxValue = 100;
            public const int FacultyNumberLength = 8;
        }
        public class Library
        {
            public const int BookTitleMinLength = 2;
            public const int BookTitleMaxLength = 150;
            public const int BookAuthorsMinNameLength = 2;
            public const int BookAuthorMaxNameLength = 20;
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 30;
            public const int BookPublishYearMaxLength = 4;
            public const int BookDescriptionMinLength = 10;
            public const int BookDescriptionMaxLength = 400;
            public const int BookImageUrlMaxLength = 255;
        }
       
            
        
    }
}
