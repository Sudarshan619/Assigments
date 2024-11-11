using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace QuizzApplicationBackend.Repositories
{
    public class UserRepository : IRepository<string,User>
    {
        private readonly QuizContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly EmailService _mailService;

        public UserRepository(QuizContext context, ILogger<UserRepository> logger,EmailService mailService)
        {
            _context = context;
            _logger = logger;
            _mailService = mailService;
        }
        public async Task<User> Add(User entity)   
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();

                await _mailService.SendEmailAsync(entity.Email, "Account Updated", "Your account details have been updated.");
                return entity;
            }
            catch (CouldNotAddException ex) {

                throw new CouldNotAddException("Error while adding user");
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
                throw new NotFoundException("Delete failed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error while deleting user: {ex.Message}");
                throw new Exception("Error while deleting user.");
            }
        }


        public async Task<User> Get(string name)
        {
            try
            {
                var result = _context.Users.FirstOrDefault(e => e.Name == name);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new NotFoundException("User not found");
                }
            }
            catch (NotFoundException ex) { 
                throw new NotFoundException(ex.Message);         
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var result = await _context.Users.ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new NotFoundException("User not found");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<User> Update(string name, User entity)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
                if (existingUser == null)
                {
                    throw new NotFoundException($"User with name {name} not found.");
                }

                
                existingUser.Email = entity.Email;
                existingUser.Name = entity.Name;
                existingUser.Password = entity.Password;
                existingUser.Role = entity.Role;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return existingUser;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Update failed: {ex.Message}");
                throw new NotFoundException("Update failed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error while updating user: {ex.Message}");
                throw new Exception("Error while updating user.");
            }
        }
    }
}
