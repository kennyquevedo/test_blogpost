using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.BLogic.Interfaces
{
    public interface IBLRoles
    {
        void AddRole(Dto.Role role);
        void UpdateRole(Dto.Role role);
        void DeleteRole(int id);
        IEnumerable<Dto.Role> GetAll();
        Dto.Role GetById(int id);
        Dto.Role GetByName(string name);
    }
}
