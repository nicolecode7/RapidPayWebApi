﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RapidPayWebApi.Models;
using RapidPayWebApi.Models.Dto;
using RapidPayWebApi.Repository.IRepository;
using System.Net;

namespace RapidPayWebApi.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepo)
        {                      
            _userRepo = userRepo;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
           
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse == null)
            {                
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Internal server error");
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response); 
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

    }
}
