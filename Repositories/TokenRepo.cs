using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SpellViewer.Models;
using SpellViewer.Models.Entities;

namespace SpellViewer.Repositories
{
    public class TokenRepo: ITokenRepo
    {
        private readonly  IConfiguration configuration;
            public TokenRepo(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public string? CreateJWTToken(User user, List<string>? roles = null)
            {
                if(string.IsNullOrEmpty(user.Username) == false)
                {

                    var claims = new List<Claim>(){
                        new Claim(ClaimTypes.Name, user.Username)
                    };
                    if(roles == null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,"Guest"));
                    }
                    else 
                    {
                        foreach( var role in roles)
                        {
                            if(string.IsNullOrEmpty(role) == false)
                            {
                                claims.Add(new Claim(ClaimTypes.Role,role));
                            }
                        }
                        if (claims.Count() == 1)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,"Guest"));
                        }
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(15),
                        signingCredentials: credentials
                    );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                } else {
                    return null;
                }
            }
    }
}