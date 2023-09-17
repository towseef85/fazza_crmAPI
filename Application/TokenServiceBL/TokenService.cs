using Application.AppUserBL;
using Application.DriverBL;
using AutoMapper;
using Domain.AppUsers;
using FluentValidation;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.TokenServicesDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.TokenServiceBL
{
    public class TokenService
    {

        public class Command : IRequest<string>
        {
            public UserDto Login { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).SetValidator(new UserValidation());
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly ApplicationDbContext _context;
            private IConfiguration _config;
            public Handler(ApplicationDbContext context, IMapper mapper, IConfiguration config)
            {
                _context = context;
                _config = config;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
              
                    var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == request.Login.EmailId && x.Password == request.Login.Password);
                    if (user != null)
                    {

                        var tokenhandler = new JwtSecurityTokenHandler();
                        var tokenkey = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);
                        var tokendesc = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                            }),
                            Expires = DateTime.Now.AddDays(7),
                            Issuer = _config["Jwt:ValidIssuer"],
                            Audience = _config["Jwt:ValidAudience"],
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                        };
                        var token = tokenhandler.CreateToken(tokendesc);
                        var finaltoken = tokenhandler.WriteToken(token);

                        return finaltoken.ToString();

                    }

                return default;
            }
        }

        //private readonly UserManager<UserDto> _userManager;
        //private readonly IConfiguration _config;

        //public TokenService(UserManager<UserDto> userManager, IConfiguration config)
        //{
        //    _userManager = userManager;
        //    _config = config;
        //}

        //public async Task<string> GenerateToken(AppUser user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Role, user.Role)
        //    };

        //    var roles = await _userManager.GetRolesAsync(user);

        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        //    var tokenOptions = new JwtSecurityToken
        //    (
        //        issuer: null,
        //        audience: null,
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(7),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //}

    }
}









