using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuizzApplicationBackend.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly QuizContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly EmailService _mailService;

        public UserRepository(QuizContext context, ILogger<UserRepository> logger, EmailService mailService)
        {
            _context = context;
            _logger = logger;
            _mailService = mailService;
        }

        public async Task<User> Add(User entity)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == entity.Name);
                if (existingUser != null)
                {
                    throw new Exception("User already exists.");
                }

                _context.Users.Add(entity);
                await _context.SaveChangesAsync();

                await _mailService.SendEmailAsync(entity.Email, "Account created", $"Your account details have been created for user {entity.Name}.");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding user: {ex.Message}");
                throw new CouldNotAddException("Error while adding user.");
            }
        }

        public async Task<User> Delete(string name)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
                if (user == null)
                {
                    throw new NotFoundException($"User with name {name} not found.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Delete failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error while deleting user: {ex.Message}");
                throw new Exception("Error while deleting user.");
            }
        }

        public async Task<User> Get(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Name == name);
            return user; // Return null if user is not found
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Update(string name, User updatedUser)
        {
            //// Fetch the existing user by name
            //var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

            //if (existingUser == null)
            //{
            //    throw new NotFoundException($"User with name {name} not found.");
            //}

            //// Update the properties of the existing user
            //existingUser.Email = updatedUser.Email;
            //existingUser.Password = updatedUser.Password;
            //existingUser.HashKey = updatedUser.HashKey;
            //existingUser.Role = updatedUser.Role;

            //// Save changes to the database
            //_context.Users.Update(existingUser);
            //await _context.SaveChangesAsync();

            //// Return the updated user
            //return existingUser;
            throw new NotImplementedException();
        }


    }
}
