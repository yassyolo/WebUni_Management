﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class MajorFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MajorNameMaxLength, MinimumLength = MajorNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(FacultyNameMaxLength, MinimumLength = FacultyNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string FacultyName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MajorDescriptionMaxLength, MinimumLength = MajorDescriptionMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

    }
}
