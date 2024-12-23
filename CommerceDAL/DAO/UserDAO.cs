using CommerceDAL.Entities;
using CommerceDAL.Repository.Implementation;
using CommerceDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.DAO
{
    public class UserDAO
    {
        readonly IRepository<Users> _repo;
        public UserDAO(IRepository<Users> repo)
        {
            _repo = repo;
        }
        //need to do repository implementations
    }
}
