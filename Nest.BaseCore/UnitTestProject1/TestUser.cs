using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.Entity;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUser
    {
        private readonly IUserService _userService;

        public TestUser()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            AutofacConfig.Register(serviceProvider);

            _userService = AutofacConfig.Resolve<IUserService>();
        }


        [TestMethod]
        public void Add()
        {
            var user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy",
                UserName = "Karroy"
            };
            var i = _userService.Add(user);


            List<UserInfo> users = new List<UserInfo>();
            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy",
                UserName = "Karroy"
            };
            users.Add(user);

            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "jay",
                UserName = "jay"
            };
            users.Add(user);
            i = _userService.Add(users);

        }
    }
}
