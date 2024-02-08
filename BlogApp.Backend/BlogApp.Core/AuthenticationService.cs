﻿using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AuthenticationService(ILogger<AuthenticationService> logger, IUsersRepository usersRepository, IPasswordService passwordService, ITokenService tokenService) 
	{
        _logger = logger;
        _usersRepository = usersRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public AuthenticationResult Authenticate(string username, string password)
    {
		try
		{
            var user = _usersRepository.GetUserCredentials(username);

            if (user is null)
                throw new InvalidUserCredentialsException($"User with username [{username}] was not found.");
                       
            var validPassword = _passwordService.VerifyPasswordMatch(password, user.Password);

            if (validPassword)
            {
                var token = _tokenService.GetToken(user);

                return new AuthenticationResult
                {
                    Token = token
                };
            }

            throw new InvalidUserCredentialsException($"Password provided for the User [{username}] is invalid.");
        }
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}   
    }    
}