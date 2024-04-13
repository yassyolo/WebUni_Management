using System.Security.Claims;

namespace WebUni_Management.Extenstions
{
	public static class ClaimsPrincipalsExtension
	{
		public static string GetId(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
