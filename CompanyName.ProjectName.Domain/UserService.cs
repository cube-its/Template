using CompanyName.ProjectName.Common.Exceptions;
using CompanyName.ProjectName.Common.Settings;
using CompanyName.ProjectName.Domain.Contracts;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Domain.Contracts.Services;
using CompanyName.ProjectName.Integration;
using CompanyName.ProjectName.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICubeResourceManager _cubeResourceManager;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICubeResourceManager cubeResourceManager, IOptions<AppSettings> appSettingsAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cubeResourceManager = cubeResourceManager;
            _appSettings = appSettingsAccessor.Value;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetByPhone(string phone)
        {
            return _userRepository.GetByPhone(phone);
        }

        public User Login(string phone, string email)
        {
            var existingUser = _userRepository.Login(phone, email);

            if (existingUser == null)
                throw new InvalidPhoneOrEmailException();

            sendSmsVerificationCode(phone, existingUser);

            return existingUser;
        }

        public User Register(User user)
        {
            // Validate if phone is unique.
            if (!IsPhoneUnique(user.Phone))
            {
                throw new NotUniquePhoneException();
            }

            // Validate if email is unique.
            if (!IsEmailUnique(user.Email))
            {
                throw new NotUniqueEmailException();
            }

            // Get the existing user record if already created before with the same phone number.
            User existingUser = GetByPhone(user.Phone);

            // Send SMS verification code to the given phone number.
            string code = _cubeResourceManager.SendSmsVerificationCode(user.Phone);

            // Check if the user record with the given phone number already exists.
            if (existingUser != null)
            {
                // Update the existing user record.
                existingUser.Email = user.Email;
                existingUser.SmsCode = code;
                existingUser.SmsCodeExpiredOn = DateTime.UtcNow.AddMinutes(_appSettings.SmsCodeExpiryInMinutes);

                _userRepository.Update(existingUser);

                // Commit the changes to the database.
                _unitOfWork.Commit();

                return existingUser;
            }
            else
            {
                // Create a new user record.
                user.CreatedOn = DateTime.UtcNow;
                user.SmsCode = code;
                user.SmsCodeExpiredOn = DateTime.UtcNow.AddMinutes(_appSettings.SmsCodeExpiryInMinutes);

                // Check if the user already exists in the Cube database.
                user.IsCubeUser = _cubeResourceManager.PhoneExists(user.Phone);

                _userRepository.Add(user);

                // Commit the changes to the database.
                _unitOfWork.Commit();

                return user;
            }
        }

        public bool IsPhoneUnique(string phone)
        {
            return _userRepository.IsPhoneUnique(phone);
        }

        public bool IsEmailUnique(string email)
        {
            return _userRepository.IsEmailUnique(email);
        }

        public bool VerifyPhoneCode(string phone, string code)
        {
            // Check if the phone belongs to a user record.
            User existingUser = GetByPhone(phone);

            if (existingUser == null)
                throw new NotFoundException();

            // Check if the verification code is valid.
            if (existingUser.SmsCode != code || existingUser.SmsCodeExpiredOn < DateTime.UtcNow)
                return false;

            // Valid verification code.
            // Activate the user record.
            existingUser.SmsCodePassedOn = DateTime.UtcNow;
            existingUser.Status = (int)EntityStatus.Active;

            _userRepository.Update(existingUser);

            // Commit the changes to the database.
            _unitOfWork.Commit();

            return true;
        }

        public void SendSmsVerificationCode(string phone)
        {
            var existingUser = GetByPhone(phone);

            if (existingUser == null)
                throw new NotFoundException();

            sendSmsVerificationCode(phone, existingUser);
        }

        private void sendSmsVerificationCode(string phone, User existingUser)
        {
            // Send SMS verification code to the given phone number.
            string code = _cubeResourceManager.SendSmsVerificationCode(phone);

            // Update the existing user record.
            existingUser.SmsCode = code;
            existingUser.SmsCodeExpiredOn = DateTime.UtcNow.AddMinutes(_appSettings.SmsCodeExpiryInMinutes);

            _userRepository.Update(existingUser);

            // Commit the changes to the database.
            _unitOfWork.Commit();
        }
    }
}
