using API.DTOs.Accounts;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
[Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    //[Route("register")]
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterDto register)
    {
        var createdRegister = _service.Register(register);
        if (createdRegister == null)
        {
            return BadRequest(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Register failed"
            });
        }

        return Ok(new ResponseHandler<RegisterDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully register",
            Data = createdRegister
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto loginDto)
    {
        var login = _service.Login(loginDto);
        if (login is "-1")
        {
            return NotFound(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found"
            });
        }
        if (login is "0")
        {
            return BadRequest(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is incorrect"
            });
        }
        if (login is "-2")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving when creating token"
            });
        }

        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success",
            Data = login
        });
    }

    // Forget Password
    [HttpPost("forget-password")]
    [AllowAnonymous]
    public IActionResult ForgetPassword(ForgetPasswordDto forgetPasswordDto)
    {
        var forgetPassword = _service.ForgetPassword(forgetPasswordDto);
        if (forgetPassword is -1)
        {
            return NotFound(new ResponseHandler<ForgetPasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });
        }

        if (forgetPassword is 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ForgetPasswordDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });
        }

        return Ok(new ResponseHandler<ForgetPasswordDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Otp has been sent to your email"
        });
    }

    // Change Password
    [HttpPost("change-password")]
    [Authorize(Roles = $"{nameof(RoleLevel.User)}")]
    public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var updatePassword = _service.ChangePassword(changePasswordDto);
        if (updatePassword is -1)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });
        }

        if (updatePassword is 0)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is incorect"
            });
        }

        if (updatePassword is 1)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is already used"
            });
        }

        if (updatePassword is 2)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is expired"
            });
        }

        return Ok(new ResponseHandler<ChangePasswordDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Password has been changed successfully"
        });
    }

    // Get All
    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetAccount();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<AccountDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    // Get By Guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _service.GetAccount(guid);
        if (account is null)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = account
        });
    }

    // Create Account
    [HttpPost]
    public IActionResult Create(NewAccountDto newAccountDto)
    {
        var createAccount = _service.CreateAccount(newAccountDto);
        if (createAccount is null)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createAccount
        });
    }

    // Update Account
    [HttpPut]
    public IActionResult Update(AccountDto updateAccountDto)
    {
        var update = _service.UpdateAccount(updateAccountDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    // Delete Account
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteAccount(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }
}
