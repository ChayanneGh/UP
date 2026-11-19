namespace UP.Domain
{
    public interface IUserRepository
    {
        Task <IUserRepository?> GetuserByIdAsync(int id);
        Task <IEnumerable<IUserRepository>> GetUsersAsync();
        Task <int> CreateUserAsync(IUserRepository user);
        Task UpdateUserAsync(IUserRepository user);
        Task DeleteUserAsync(int id);

    }
}
