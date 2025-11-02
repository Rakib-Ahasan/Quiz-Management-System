using System.Linq;
using Api.Config;
using Domain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidFireLib.Lib.Authintication;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using RapidFireLib.View.Models.Identity;

namespace API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        RapidFire rf = new RapidFire(new AppConfig());

        [Route("/api/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(Login loginUser)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            //apiResponse.ApiPacket.Packet = null;
            var loginResult = (AccessControl.User)rf.AccessControl.LoginApi(loginUser, true, LoginType.LoginDB);
            if (loginResult == null)
                return BadRequest("invalid credential");
            else if (loginResult.Message.Contains("is not valid"))
                return BadRequest(loginResult.Message);
            apiResponse.Tag = "User";
            apiResponse.ApiPacket.Packet = GetLoginResult(loginResult, loginUser);
            apiResponse.Success = true;
            return Ok(apiResponse);

        }
        [Route("api/login/SendPacket")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(ApiPacketRequest apr)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            Login loginUser = JsonConvert.DeserializeObject<Login>(apr.ApiPacket.Packet.ToString());

            var loginResult = (AccessControl.User)rf.AccessControl.LoginApi(loginUser, true, LoginType.LoginDB);
            apiResponse.Message = loginResult.Message;
            if (apiResponse.Message != "Login Successful")
            {
                apiResponse.Success = false;
                return Ok(apiResponse);
            }

            apiResponse.Tag = "User";
            apiResponse.ApiPacket.Packet = GetLoginResult(loginResult, loginUser);
            apiResponse.Success = true;
            return Ok(apiResponse);

        }
        private AccessControl.User GetLoginResult(AccessControl.User loginResult, Login loginUser)
        {
            //your custom logic....
            return loginResult;
        }

    }

}