using AutoMapper;
using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BlogPost.Common;
using System.Linq;

namespace BlogPost.BLogic
{
    public class BLRoles : Interfaces.IBLRoles
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;


        public BLRoles(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public void AddRole(Dto.Role role_dto)
        {
            if (role_dto == null)
                new ArgumentException("Object is null.");

            try
            {
                var role = _mapper.Map<Role>(role_dto);
                if (role != null)
                    role.CreatedDate = DateTime.UtcNow;

                _unitWork.Roles.Add(role);
                _unitWork.Complete();
            }
            //TODO: Validate specific catchs
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRole(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero (0)");

            try
            {
                //get role to delete.
                var role = _unitWork.Roles.GetById(id);
                if (role != null)
                {
                    _unitWork.Roles.Remove(role);
                    _unitWork.Complete();
                }
                else
                {
                    throw new RecordNotFoundException($"Role with id {id} not found to delete it.");
                }
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateRole(Dto.Role role)
        {
            if (role == null)
                throw new ArgumentException("Object is null.");

            if (role.Id <= 0)
                throw new ArgumentException("Id must be greater than zero (0)");

            try
            {
                var role_ = _unitWork.Roles.GetById(role.Id);
                if (role_ != null)
                {
                    role_.Name = role.Name;
                    role_.ModifiedDate = DateTime.UtcNow;

                    //Update
                    _unitWork.Roles.Update(role_);
                    _unitWork.Complete();
                }
                else
                {
                    throw new RecordNotFoundException($"Role with id {role.Id} not found.");
                }
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Dto.Role> GetAll()
        {
            IList<Dto.Role> roles_dto = null;

            try
            {
                var roles = _unitWork.Roles.GetAll();
                if (roles.IsAny())
                    roles_dto = _mapper.Map<IEnumerable<Role>, IList<Dto.Role>>(roles);
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roles_dto;
        }

        public Dto.Role GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero (0)");

            try
            {
                Dto.Role role_dto = null;

                var role = _unitWork.Roles.GetById(id);
                if (role != null)
                {
                    role_dto = _mapper.Map<Dto.Role>(role);
                }
                else
                {
                    throw new RecordNotFoundException($"Role with id {id} not found.");
                }

                return role_dto;
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dto.Role GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name must be provided to search the role.");

            try
            {
                Dto.Role role_dto = null;

                var role = _unitWork.Roles.Find(r => r.Name == name);
                if (role.IsAny())
                    role_dto = _mapper.Map<Dto.Role>(role.FirstOrDefault());
                else
                    throw new RecordNotFoundException($"role with name \"{name}\" not found");

                return role_dto;
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
