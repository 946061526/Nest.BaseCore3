using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.BaseCore.BusinessLogic.Service
{
    public class RoleService : IRoleService
    {
        //private HttpClient _httpClient;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        public List<RoleResponseModel> GetRoleList()
        {
            var result = _roleRepository.Find().Select(a => new RoleResponseModel()
            {
                Id = a.id,
                Name = a.name
            }).ToList();

            return result;
        }

        public ApiResultModel<int> Add(AddRoleRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public ApiResultModel<int> Delete(BaseIdModel idModel)
        {
            throw new NotImplementedException();
        }

        public ApiResultModel<int> Edit(EditRoleRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public ApiResultModel<QueryRoleResponseModel> GetOne(BaseIdModel idModel)
        {
            throw new NotImplementedException();
        }

        public ApiResultModel<List<QueryRoleResponseModel>> GetList(QueryRoleRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        //public ApiResultModel<List<QueryRoleResponseModel>> GetListPage(QueryRolePageRequestModel requestModel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
