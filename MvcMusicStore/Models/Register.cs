using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models

{
	public class Register
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]

		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone is Required")]

		public string Phone { get; set; }
		[Required(ErrorMessage = "Birthday is Required")]

		public DateOnly BirthDate	{ get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and ata max {1} character long")]
		[DataType(DataType.Password)]
		[Compare("ConfirmPassword", ErrorMessage = "Passwprd does not match")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]

		public string ConfirmPassword { get; set; }
		public UserRole RoleId { get; set; }
	}
}
