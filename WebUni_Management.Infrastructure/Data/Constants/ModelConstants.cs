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
            public const int BookImageUrlMinLength = 0;
            public const int BookImageUrlMaxLength = 255;
            public const int RentalTimeDefaultValue = 2;
        }
        public class StudyRoom
        {
            public const int RoomNameMinLength = 2;
            public const int RoomNameMaxLength = 110;
            public const int RoomDescriptionMinLength = 10;
            public const int RoomDescriptionMaxLength = 500;
            public const int RoomImageUrlMinLength = 0;
            public const int RoomImageUrlMaxLength = 255;
            public const int RoomFloorMinValue = 0;
            public const int RoomFloorMaxValue = 3;
            public const int RoomCapacityMinValue = 1;
            public const int RoomCapacityMaxValue = 10;
        }
        public class Canteen
        {
            public const int DishNameMinLength = 2;
            public const int DishNameMaxLength = 70;
            public const int DishCategoryMinLength = 2;
            public const int DishCategoryMaxLength = 10;
            public const string DishPriceMinValue = "0.00";
            public const string DishPriceMaxValue = "10.00";
        }
       
            
        
    }
}
