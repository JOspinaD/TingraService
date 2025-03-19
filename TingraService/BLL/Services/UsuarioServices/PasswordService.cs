using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TingraService.BLL.Services.UsuarioServices
{
    public class PasswordService
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            //Genera un salt aleatorio
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000, // Número de iteraciones
                numBytesRequested: 256 / 8)); // Tamaño del hash

            return (hashed, Convert.ToBase64String(salt));
        }
        //Hashear la contraseña
        public bool verifyPassword(string password, string hash, string salt)
        {
            byte[] salbytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salbytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == hash;
        }
    }
}
