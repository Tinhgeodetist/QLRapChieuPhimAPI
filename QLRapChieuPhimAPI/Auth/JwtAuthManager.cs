using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Auth
{
    public interface IJwtAuthManager
    {
        void RemoveExpiredTokens(DateTime now);
        void RemoveTokenByUserName(string userName);
        string CreateToken(UserInfo thanhvien);
    }
    public class Jwt
    {
        public static string SecretKey;
        
    }
    public class JwtAuthToken
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }    // can be used for usage tracking
        // can optionally include other metadata, such as user agent, ip address, device name, and so on

        [JsonPropertyName("useragent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string UserAgent { get; set; }
    }
    public class JwtAuthManager : IJwtAuthManager
    {
        //private readonly Jwt _jwt;
        public static ConcurrentDictionary<string, JwtAuthToken> _usersTokens;  // can store in a database or a distributed cache
        public JwtAuthManager()
        {
            //_jwt = jwt;
            _usersTokens = new();
        }
        public string CreateToken(UserInfo info)
        {
            byte[] key = Encoding.ASCII.GetBytes(Jwt.SecretKey);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var expire = DateTime.Now.AddMinutes(20);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, info.Username),
                    new Claim("THANHVIENID", info.Id.ToString()),
                    new Claim("USERNAME", info.Username),
                    new Claim("QUYENHAN", "ThanhVien"),
                    new Claim("HOTEN", info.HoTen),
                    new Claim("EMAIL", info.Email)
                }),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var secutiryToken = handler.CreateToken(descriptor);
            var token = handler.WriteToken(secutiryToken);
            var userToken = new JwtAuthToken { AccessToken = token, ExpireAt = expire, UserName = info.Email, UserAgent = info.UserAgent };
            _usersTokens.AddOrUpdate(userToken.AccessToken, userToken, (_, _) => userToken);
            return token;
        }

        public void RemoveExpiredTokens(DateTime now)
        {
            var expiredTokens = _usersTokens.Where(x => x.Value.ExpireAt < now).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                _usersTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        public void RemoveTokenByUserName(string userName)
        {
            var accessTokens = _usersTokens.Where(x => x.Value.UserName == userName).ToList();
            foreach (var accessToken in accessTokens)
            {
                _usersTokens.TryRemove(accessToken.Key, out _);
            }
        }
    }
}
