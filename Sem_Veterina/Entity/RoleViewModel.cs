using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class RoleViewModel
    {
        public List<ROLE> Role { get; set; } = new List<ROLE>();
        public ROLE SelectedRole { get; set; }
    }
}