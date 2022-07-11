using AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<UpdateUserDTO> GetById(string id)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(selector: x => new AppUserVM
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Email = x.Email,
                Password = x.PasswordHash,
                ConfirmPassword = x.PasswordHash,
                ImagePath = x.ImagePath
            },
            expression: x => x.Id == id && x.Status != Domain.Enums.Status.Deleted);

            var updateUser = _mapper.Map<UpdateUserDTO>(user);

            return updateUser;
        }

        public async Task<AppUserVM> GetByUser(string id)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(selector: x => new AppUserVM
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Email = x.Email,
                Address = x.Address,
                ImagePath = x.ImagePath
            },
            expression: x => x.Id == id && x.Status != Domain.Enums.Status.Deleted);

            return user;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            SignInResult result;
            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                return result;
            }

            result = SignInResult.Failed;

            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var newUser = _mapper.Map<AppUser>(model);

            var anyUser = await _unitOfWork.AppUserRepository.Any(x=>x.Email == model.Email);

            IdentityResult result;
            if (!anyUser)
            {
                result = await _userManager.CreateAsync(newUser, model.Password);

                return result;
            }

            result = IdentityResult.Failed();

            return result;
        }

        public Task UpdateUser(UpdateUserDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
