using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        // case Query ถ้าไม่ใช้บ่อยเขียน raw query จะเหมาะกว่านะ 
        User? GetUserByEmail(string email) ;
        void AddUser(User user);
    }
}