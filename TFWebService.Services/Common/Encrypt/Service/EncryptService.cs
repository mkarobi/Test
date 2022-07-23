using System;
using System.Collections.Generic;
using System.Text;
using TFWebService.Common.Helper;
using TFWebService.Data.Dtos.Api.User;
using TFWebService.Data.Models;
using TFWebService.Services.Common.Encrypt.Interface;

namespace TFWebService.Services.Common.Encrypt.Service
{
    public class EncryptService : IEncryptService
    {
        public UserForUpdateDto UpdateUserDecrypt(UserForUpdateDto userForUpdateDto)
        {
            return new UserForUpdateDto()
            {
                Name = EncryptionUtils.Decrypt(userForUpdateDto.Name,EncryptionUtils.Key),
                Email = EncryptionUtils.Decrypt(userForUpdateDto.Email,EncryptionUtils.Key),
                PhoneNumber = EncryptionUtils.Decrypt(userForUpdateDto.PhoneNumber,EncryptionUtils.Key),
                City = EncryptionUtils.Decrypt(userForUpdateDto.City,EncryptionUtils.Key),
                Address = EncryptionUtils.Decrypt(userForUpdateDto.Address,EncryptionUtils.Key),
                DateOfBirth = EncryptionUtils.Decrypt(userForUpdateDto.DateOfBirth,EncryptionUtils.Key),
                Gender = userForUpdateDto.Gender

            };
        }

        public User UserEncrypt(User user)
        {
            return new User()
            {
                Name = EncryptionUtils.Encrypt(user.Name, EncryptionUtils.Key),
                Email = EncryptionUtils.Encrypt(user.Email, EncryptionUtils.Key),
                PhoneNumber = EncryptionUtils.Encrypt(user.PhoneNumber, EncryptionUtils.Key),
                City = EncryptionUtils.Encrypt(user.City, EncryptionUtils.Key),
                Address = EncryptionUtils.Encrypt(user.Address, EncryptionUtils.Key),
                DateOfBirth = EncryptionUtils.Encrypt(user.DateOfBirth, EncryptionUtils.Key),
                Gender = user.Gender,
                IsAdmin = user.IsAdmin,
                Status = user.Status,
                CreateTime = user.CreateTime,
                UpdateTime = user.UpdateTime,
                Id = user.Id,
                IsAcive = user.IsAcive,
                MainDetails = user.MainDetails,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                TrackDetails = user.TrackDetails,
                UserName = EncryptionUtils.Encrypt(user.UserName,EncryptionUtils.Key)
            };
        }

        public MainDetails MainDetailsEncrypt(MainDetails mainDetails)
        {
            return new MainDetails()
            {
                User = mainDetails.User,
                UpdateTime = mainDetails.UpdateTime,
                UserId = mainDetails.UserId,
                Id = mainDetails.Id,
                CreateTime = mainDetails.CreateTime,
                FoodCalories = EncryptionUtils.Encrypt(mainDetails.FoodCalories,EncryptionUtils.Key),
                SelfWeight = EncryptionUtils.Encrypt(mainDetails.SelfWeight, EncryptionUtils.Key),
                PersianDate = EncryptionUtils.Encrypt(mainDetails.PersianDate, EncryptionUtils.Key),
                ActivityCalories = EncryptionUtils.Encrypt(mainDetails.ActivityCalories, EncryptionUtils.Key),
                TotalCalories = EncryptionUtils.Encrypt(mainDetails.TotalCalories, EncryptionUtils.Key),
                WaterGlasses = EncryptionUtils.Encrypt(mainDetails.WaterGlasses, EncryptionUtils.Key)
            };
        }

        public MainDetails MainDetailsDecrypt(MainDetails mainDetails)
        {
            
        }
    }
}
