using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using SehirRehberiEnginD.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SehirRehberiEnginD.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            //kullanıcı adını normal al sonrasında sıfresını hashleme ve tuzlama ıle sıfrele sonrasında verı tabanında boylle kontrol et

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user== null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

      

        public async Task<User> Register(User user, string password)
        {
           // kullanıcı kayıt işlemi

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x=>x.UserName == userName))
            {
                return true;
            }
            return false;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // c# oluşturdu

                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=userPasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
      
    }
}
