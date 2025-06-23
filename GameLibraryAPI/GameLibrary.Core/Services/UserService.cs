using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;

namespace GameLibrary.Core.Services
{
    public class UserService(AuthService authService, 
                             UserRepository userRepository,
                             GameRepository gameRepository)
    {
        public async Task RegisterAsync(RegisterRequest registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var salt = authService.GenerateSalt();
            var hashedPassword = authService.HashPassword(registerData.Password, salt);

            var registerEntity = registerData.ToEntity();
            registerEntity.Password = hashedPassword;
            registerEntity.PasswordSalt = Convert.ToBase64String(salt);

            if(registerEntity.Email.Contains("@admin.com"))
                registerEntity.IsAdmin = true;

            await userRepository.AddUserAsync(registerEntity);
        }

        public async Task<string> LoginAsync(LoginRequest payload)
        {
            var user = await userRepository.GetByEmailAsync(payload.Email);

            if (authService.HashPassword(payload.Password, Convert.FromBase64String(user.PasswordSalt)) == user.Password)
            {
                var role = GetRole(user);

                return authService.GetToken(user, role);
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
        }

        private string GetRole(User user)
        {
            if (user.IsAdmin)
            {
                return "Admin";
            }
            else
            {
                return "User";
            }
        }

        public async Task AddGameToUserLibraryAsync(int id, AddGameToUserRequest payload)
        {
            if(payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            User user = new User();
            user.Games = await GetAllGamesAsync(payload.GamesIds);
            await userRepository.AddGameToUserAsync(id, user);
        }

        public async Task<List<Game>> GetAllGamesAsync(List<int> gamesIds)
        {
            return await gameRepository.GetGamesByIdsAsync(gamesIds);
        }
    }
}
