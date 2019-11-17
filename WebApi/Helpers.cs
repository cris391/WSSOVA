using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace DatabaseService
{
  class Helpers
  {
    public static int GetUserIdFromJWTToken(string accessToken)
    {
      accessToken = accessToken.ToString().Replace("Bearer ", "");
      var handler = new JwtSecurityTokenHandler();
      var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
      var userIdStr = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
      if (int.TryParse(userIdStr, out int userId))
      {
        return userId;
      }
      return 0;
    }
  }
}