using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using LoginAPI.Repo;
using LoginAPI.Models;

namespace LoginAPI.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IUserRepo repo;

        public TokenGeneratorService(IUserRepo repo)
        {
            this.repo = repo;
        }        

        // GENERATE TOKEN METHOD
        public string GenerateToken(string userName, string password)
        {
            var role = repo.GetUserByUserName(userName).Role;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Role, role)
            };

            //Step 2: Define your secret key that will be used to generate the JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_secret_key_for_user"));
            //encoding.getbytes is a predefined method, passing into the symsecurity key

            //Step 3: Create the signature and define the algorithm you want to use
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);            

            //Step 4: Combine it all to generate the JWT token
            var token = new JwtSecurityToken(
                //JWT is a predefined class
                issuer: "authapi",
                audience: "userapi",
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(20),
                //the token will expire in 20 mins
                signingCredentials: creds
                 );

            //Step 5: Send the token in the response
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            //Step 6: Convert the response into JSON
            return JsonConvert.SerializeObject(response);

        }
    }
}
