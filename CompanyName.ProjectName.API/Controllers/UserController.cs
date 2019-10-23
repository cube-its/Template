using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompanyName.ProjectName.API.DTOs;
using CompanyName.ProjectName.Domain.Contracts.Services;
using CompanyName.ProjectName.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyName.ProjectName.API.Controllers
{
    /// <summary>
    /// This API controller contains APIs supporting the membership of the users who will fill the survey.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtFactory _jwtFactory;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IJwtFactory jwtFactory, IMapper mapper)
        {
            _userService = userService;
            _jwtFactory = jwtFactory;
            _mapper = mapper;
        }

        // GET: api/user
        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var lst = _userService.GetUsers();
            return Ok(lst);
        }

        // POST: api/user/login
        /// <summary>
        /// Authenticates the user using phone number and email address.
        /// </summary>
        /// <param name="userLoginDTO">User login object containing phone and email</param>
        /// <returns>Feedback status</returns>
        [HttpPost("login")]
        public ActionResult<UserLoginResultDTO> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = _userService.Login(userLoginDTO.Phone, userLoginDTO.Email);
            var result = _mapper.Map<UserLoginResultDTO>(user);

            return Ok(result);
        }

        // POST: api/user/register
        /// <summary>
        /// Registers user to the system.
        /// </summary>
        /// <param name="userRegisterDTO">User register object containing phone and email</param>
        /// <returns>Feedback status</returns>
        [HttpPost("register")]
        public ActionResult<UserRegisterResultDTO> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);
            var createdUser = _userService.Register(user);
            var result = _mapper.Map<UserRegisterResultDTO>(createdUser);

            return Ok(result);
        }

        // GET: api/user/availability/phone/{phone}
        /// <summary>
        /// Checks if the phone number is available or not for registration.
        /// </summary>
        /// <param name="phone">Phone number to be checked</param>
        /// <returns>Boolean</returns>
        [HttpGet("availability/phone/{phone}")]
        public ActionResult<BooleanResultDTO> IsPhoneUnique([Phone] string phone)
        {
            var res = new BooleanResultDTO();
            res.Result = _userService.IsPhoneUnique(phone);

            return Ok(res);
        }

        // GET: api/user/availability/email/{email}
        /// <summary>
        /// Checks if the email address is available or not for registration.
        /// </summary>
        /// <param name="email">Email address to be checked</param>
        /// <returns>Boolean</returns>
        [HttpGet("availability/email/{email}")]
        public ActionResult<BooleanResultDTO> IsEmailUnique([EmailAddress]string email)
        {
            var res = new BooleanResultDTO();
            res.Result = _userService.IsEmailUnique(email);

            return Ok(res);
        }

        // GET: api/user/phone/{phone}/verification/{code}
        /// <summary>
        /// Checks if the verification code is valid for the given phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="code">Verification code to be checked</param>
        /// <returns>Access token</returns>
        [HttpGet("phone/{phone}/verification/{code}")]
        public async Task<ActionResult<PhoneValidationResultDTO>> VerifyPhoneCode([Phone] string phone, string code)
        {
            var res = new PhoneValidationResultDTO();

            // Check if the verification code is valid for the given phone number.
            res.Result = _userService.VerifyPhoneCode(phone, code);

            if (res.Result)
            {
                // Generate access token.
                res.AccessToken = (await _jwtFactory.GenerateEncodedToken(_userService.GetByPhone(phone))).AuthToken;
            }

            return Ok(res);
        }
    }
}
