using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models
{
	public class UserRole
	{
		[Key] public int RoleId { get; set; }
		public string Role { get; set; }
}
}
