using System.Security.Claims;
using Chess.Domain.Identity.DTO;
using Chess.Domain.Identity.Entities;
using Chess.Domain.Identity.Exceptions;
using Chess.Domain.Identity.Services;
using Chess.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Identity.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly EFContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public IdentityService(UserManager<User> userManager, EFContext context, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    public async Task SignUp(string email, string password, CancellationToken cancellationToken)
    {
        var userEmailIsNotUnique = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        
        if (userEmailIsNotUnique)
            throw new UserWithEmailExistsException();
        
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        var user = new User()
        {
            Email = email,
            UserName = email
        };
        
        var createUser = await _userManager.CreateAsync(user, password);
        
        if(!createUser.Succeeded)
            throw new CreateUserException(createUser.Errors);
        
        // var addRoleResult = await _userManager.AddToRoleAsync(user, "User");
        // if(!addRoleResult.Succeeded)
        //     throw new AddToRoleException();
        
        var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if(!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if(!addNameClaimResult.Succeeded)
            throw new AddClaimException();
        
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<JsonWebToken> SignIn(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email !=null && x.Email == email,
                        cancellationToken)
                    ?? throw new InvalidCredentialsException();
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
        
        if(!result.Succeeded)
            throw new SignInException(result);
        
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        
        var jwt = await _tokenService.GenerateJwtToken(user.Id, user.Email!, roles, claims);

        return jwt;
    }
}