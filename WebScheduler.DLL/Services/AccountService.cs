using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserDbContext _userContext;
        private readonly IMapper _mapper;

        public AccountService(IUserDbContext userContext, IMapper mapper) =>
            (_userContext, _mapper) = (userContext, mapper);

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ip, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> expression = u =>
                u.Email == model.Username
/*                && model.Password.AreEqual(u.Salt, u.Password)*/;

            var user = await _userContext.Users
                .Include(u => u.Roles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(expression);

            if(user == null) return null;

            var jwtToken = GenerateJwtToken(user);
            var refreshTokenDto = GenerateRefreshToken(ip);
            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);

            user.RefreshTokens.Add(refreshToken);
            await _userContext.SaveChangesAsync(cancellationToken);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token , string ip, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users
                .Include(u => u.Roles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens
                .Any(t => t.Token == token));


            if (user == null) 
                return null;

            var refreshToken = user.RefreshTokens
                .FirstOrDefault(t => t.Token == token);
            var newRefreshTokenDto = GenerateRefreshToken(ip);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ip;
            refreshToken.ReplaceByToken = newRefreshTokenDto.Token;

            var newRefreshToken = _mapper.Map<RefreshToken>(newRefreshTokenDto);

            user.RefreshTokens.Add(newRefreshToken);
            var jwtToken = GenerateJwtToken(user);
            
            await _userContext.SaveChangesAsync(cancellationToken);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);

        }
        public async Task<bool> RevokeToken(string token, string ip, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens
                .Any(t => t.Token == token));

            if(user == null) 
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive) 
                return false;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ip;
            await _userContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        private string GenerateJwtToken(User user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;

            var jwtToken = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMinutes(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private RefreshTokenDto GenerateRefreshToken(string ip)
        {
            using(var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return new RefreshTokenDto
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ip
                };
            }
        }


    }
}
