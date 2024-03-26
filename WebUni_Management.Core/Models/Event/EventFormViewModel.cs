using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Event;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
namespace WebUni_Management.Core.Models.Event
{
	public class EventFormViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(GuestParticipantMaxLength, MinimumLength = GuestParticipantMinLength, ErrorMessage = MaxLengthErrorMessage)]
		public string GuestParticipant { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = MaxLengthErrorMessage)]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DataType(DataType.DateTime)]
		public DateTime StartTime { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DataType(DataType.DateTime)]
		public DateTime EndTime { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(EventImageUrlMaxLength, MinimumLength = EventImageUrlMinLength, ErrorMessage = MaxLengthErrorMessage)]
		public string ImageUrl { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(EventCapacityMinValue, EventCapacityMaxValue, ErrorMessage = MaxLengthErrorMessage)]
		public int Capacity { get; set; }
	}
}
