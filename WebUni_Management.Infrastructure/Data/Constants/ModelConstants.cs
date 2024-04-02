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
            public const int RoomRentalTimeDefaultValue = 1;

		public int RentalTime { get; set; }
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
       public class NewsArticle
        {
            public const int NewsTitleMinLength = 2;
            public const int NewsTitleMaxLength = 200;
            public const int NewsContentMinLength = 10;
            public const int NewsContentMaxLength = 1300;
            public const int NewsImageUrlMinLength = 0;
            public const int NewsImageUrlMaxLength = 255;
            public const int NewsAuthorMaxLength = 40;
            public const int NewsAuthorMinLength = 2;
        }
        public class Event 
        { 
            public const int EventNameMinLength = 2;
            public const int EventNameMaxLength = 110;
            public const int EventDescriptionMinLength = 10;
            public const int EventDescriptionMaxLength = 1100;
            public const int EventImageUrlMinLength = 0;
            public const int EventImageUrlMaxLength = 255;
            public const int EventCapacityMinValue = 1;
            public const int EventCapacityMaxValue = 100;
            public const int GuestParticipantMaxLength = 40;
            public const int GuestParticipantMinLength = 2;
        }
        public class Subject
        {
            public const int SubjectNameMaxLength = 50;
            public const int SubjectNameMinLength = 50;
            public const int SubjectDescriptionMaxLength = 500;
            public const int SubjectDescriptionMinLength = 10;
            public const int SubjectMinAttendanceTimes = 1;
            public const int SubjectMaxAttendanceTimes = 20;
            public const int SubjectProfessorNameMaxLength = 30;
            public const int SubjectProfessorNameMinLength = 2;
            public const int SubjectProfessorDescriptionMaxLength = 500;
            public const int SubjectProfessorDescriptionMinLength = 10;
            public const int SubjectProfessorEmailMaxLength = 50;
            public const int SubjectProfessorEmailMinLength = 5;
            public const int SubjectProfessorPhoneNumberMaxLength = 12;
            public const int SubjectProfessorPhoneNumberMinLength = 5;
            public const int SubjectProfessorTitleMaxLength = 30;
            public const int SubjectProfessorTitleMinLength = 2;
        }
                    
    }
}
