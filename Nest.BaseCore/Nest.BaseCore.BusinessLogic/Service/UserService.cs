using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.Entity;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Nest.BaseCore.BusinessLogic.Service
{
    public class UserService : IUserService
    {
        //private readonly IConfiguration _config;
        //private HttpClient _httpClient;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel">参数</param>
        public ApiResultModel<LoginResponseModel> Login(LoginRequestModel requestModel)
        {
            var result = new ApiResultModel<LoginResponseModel>();
            var user = (from a in _userRepository.Find()
                        where a.UserName == requestModel.UserName
                        select new LoginResponseModel()
                        {
                            UserId = a.Id,
                            UserName = a.UserName,
                            RealName = a.RealName,
                            //RoleId = a.roleId,
                            Password = a.Pwd,
                        }).FirstOrDefault();

            result.Data = user;
            result.Code = ApiResultCode.Success;
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        public List<LoginResponseModel> GetUserList()
        {
            var result = _userRepository.Find().Select(a => new LoginResponseModel()
            {
                UserId = a.Id,
                UserName = a.UserName,
                RealName = a.RealName,
                // RoleId = a.roleId,
                Password = a.Pwd,
            }).ToList();

            return result;
        }


        public int Add(UserInfo a)
        {
            UserInfo user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy",
                UserName = "Karroy",
                Pwd = "123",
                //roleId = "1"
            };
            _userRepository.Add(user);
            var i = _userRepository.SaveChanges();

            var users = new List<UserInfo>();
            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy",
                UserName = "Karroy",
                Pwd = "123",
                //roleId = "1"
            };
            users.Add(user);
            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "jay",
                UserName = "jay",
                Pwd = "123",
                // roleId = "1"
            };
            users.Add(user);
            _userRepository.Add(users);
            i = _userRepository.SaveChanges();

            return i;
        }
        public int Add(List<UserInfo> t)
        {
            UserInfo user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy1",
                UserName = "Karroy1",
                Pwd = "123",
                // roleId = "1"
            };
            var users = new List<UserInfo>();
            users.Add(user);
            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "jay",
                UserName = "jay",
                Pwd = "123",
                //roleId = "1"
            };
            users.Add(user);
            _userRepository.Add(users);
            return _userRepository.SaveChanges();
        }

        public int Update()
        {
            var user = _userRepository.FirstOrDefault(x => x.UserName == "Karroy1");
            user.Pwd = "";
            _userRepository.Update(user);
            var i = _userRepository.SaveChanges();

            user.Pwd = "123";
            user.RealName = "jay";
            _userRepository.Update(user, "password", "name");
            i = _userRepository.SaveChanges();

            var list = _userRepository.Find().ToList();
            list.ForEach(item =>
            {
                item.Pwd = "d33f1a6621f17e8090f8fb9c1b6b6f01";
            });
            _userRepository.Update(list, "password");

            i = _userRepository.SaveChanges();

            return i;
        }

        public void Query()
        {
            var u = _userRepository.FindById("2");
            u = _userRepository.FirstOrDefault();
            u = _userRepository.FirstOrDefault(x => x.Id == "1");
            u = _userRepository.FirstOrDefault(x => x.Id != "001cb07a8f4f49af9ccac6d5a96be07e", null);

            var list = _userRepository.Find().ToList();
            list = _userRepository.Find(x => x.UserName == "").ToList();
            list = _userRepository.Find(out int total, 1, 2).ToList();

            var b = _userRepository.Any(x => x.Pwd == "d33f1a6621f17e8090f8fb9c1b6b6f01");
            b = _userRepository.Any();

            var c = _userRepository.Count();
            c = _userRepository.Count(x => x.UserName == "admin");

        }

        public void Delete()
        {
            _userRepository.Delete("2");
            var i = _userRepository.SaveChanges();

            UserInfo user = new UserInfo()
            {
                Id = "2",
                RealName = "Karroy",
                UserName = "Karroy",
                Pwd = "123",
                //roleId = "1"
            };
            _userRepository.Add(user);
            i = _userRepository.SaveChanges();

            _userRepository.Delete(x => x.Id == "22");
            i = _userRepository.SaveChanges();
        }

        public ApiResultModel<int> Add(AddUserRequestModel requestModel)
        {
            throw new System.NotImplementedException();
        }

        public ApiResultModel<int> Delete(BaseIdModel idModel)
        {
            throw new System.NotImplementedException();
        }

        public ApiResultModel<int> Edit(EditUserRequestModel requestModel)
        {
            throw new System.NotImplementedException();
        }

        public ApiResultModel<QueryUserResponseModel> GetOne(BaseIdModel idModel)
        {
            throw new System.NotImplementedException();
        }

        public ApiResultModel<List<QueryUserResponseModel>> GetList(QueryUserRequestModel requestModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
