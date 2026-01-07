using AutoMapper;
using AutoMapper.QueryableExtensions;
using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Dtos.Users;
using KundenUmfrageTool.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserListDto>> GetUsersAsync(bool? isActive)
        {
            var query = _db.Users.Include(u => u.Role).AsQueryable();


            if (isActive.HasValue)
                query = query.Where(u => u.IsActive == isActive.Value);

            return await query
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDetailDto?> GetUserAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDetailDto>(user);
        }

        public async Task<UserDetailDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("E-Mail bereits vergeben.");

            var user = _mapper.Map<User>(dto);

            // Passwort hash
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return _mapper.Map<UserDetailDto>(user);
        }

        public async Task<UserDetailDto?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return null;

            _mapper.Map(dto, user);

            await _db.SaveChangesAsync();

            return _mapper.Map<UserDetailDto>(user);
        }

        public async Task<bool> SetActiveAsync(int id, bool active)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return false;

            user.IsActive = active;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}



/*
 var query = _db.Users.Include(u => u.Role).AsQueryable();
Holt die Users
Lädt Role mit (JOIN)
AsQueryable() => Filter später möglich

if(isActive.HasValue)
query = query.Where(u => u.IsActive == isActive.Value)

Wenn ?isActive= true => nur Aktiv
wenn =isAc


 
 
 */