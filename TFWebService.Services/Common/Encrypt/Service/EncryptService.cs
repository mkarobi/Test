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
                Name = EncryptionUtils.Decrypt(userForUpdateDto.Name, EncryptionUtils.Key),
                Email = EncryptionUtils.Decrypt(userForUpdateDto.Email, EncryptionUtils.Key),
                PhoneNumber = EncryptionUtils.Decrypt(userForUpdateDto.PhoneNumber, EncryptionUtils.Key),
                City = EncryptionUtils.Decrypt(userForUpdateDto.City, EncryptionUtils.Key),
                Address = EncryptionUtils.Decrypt(userForUpdateDto.Address, EncryptionUtils.Key),
                DateOfBirth = EncryptionUtils.Decrypt(userForUpdateDto.DateOfBirth, EncryptionUtils.Key),
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
                UserName = EncryptionUtils.Encrypt(user.UserName, EncryptionUtils.Key)
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
                FoodCalories = EncryptionUtils.Encrypt(mainDetails.FoodCalories, EncryptionUtils.Key),
                SelfWeight = EncryptionUtils.Encrypt(mainDetails.SelfWeight, EncryptionUtils.Key),
                PersianDate = EncryptionUtils.Encrypt(mainDetails.PersianDate, EncryptionUtils.Key),
                ActivityCalories = EncryptionUtils.Encrypt(mainDetails.ActivityCalories, EncryptionUtils.Key),
                TotalCalories = EncryptionUtils.Encrypt(mainDetails.TotalCalories, EncryptionUtils.Key),
                WaterGlasses = EncryptionUtils.Encrypt(mainDetails.WaterGlasses, EncryptionUtils.Key)
            };
        }

        public MainDetails MainDetailsDecrypt(MainDetails mainDetails)
        {
            return new MainDetails()
            {
                User = mainDetails.User,
                UserId = mainDetails.UserId,
                Id = mainDetails.Id,
                CreateTime = mainDetails.CreateTime,
                FoodCalories = EncryptionUtils.Decrypt(mainDetails.FoodCalories, EncryptionUtils.Key),
                SelfWeight = EncryptionUtils.Decrypt(mainDetails.SelfWeight, EncryptionUtils.Key),
                PersianDate = EncryptionUtils.Decrypt(mainDetails.PersianDate, EncryptionUtils.Key),
                ActivityCalories = EncryptionUtils.Decrypt(mainDetails.ActivityCalories, EncryptionUtils.Key),
                TotalCalories = EncryptionUtils.Decrypt(mainDetails.TotalCalories, EncryptionUtils.Key),
                WaterGlasses = EncryptionUtils.Decrypt(mainDetails.WaterGlasses, EncryptionUtils.Key)

            };
        }

        public TrackDetails TrackDetailsEncrypt(TrackDetails trackDetails)
        {
            return new TrackDetails()
            {
                User = trackDetails.User,
                UserId = trackDetails.UserId,
                CreateTime = trackDetails.CreateTime,
                Id = trackDetails.Id,
                PersianDate = EncryptionUtils.Encrypt(trackDetails.PersianDate, EncryptionUtils.Key),
                TrackActivity = EncryptionUtils.Encrypt(trackDetails.TrackActivity, EncryptionUtils.Key),
                TrackFood = EncryptionUtils.Encrypt(trackDetails.TrackFood, EncryptionUtils.Key),
                TrackWeight = EncryptionUtils.Encrypt(trackDetails.TrackWeight, EncryptionUtils.Key)
            };
        }

        public TrackDetails TrackDetailsDecrypt(TrackDetails trackDetails)
        {
            return new TrackDetails()
            {
                User = trackDetails.User,
                UserId = trackDetails.UserId,
                CreateTime = trackDetails.CreateTime,
                Id = trackDetails.Id,
                PersianDate = EncryptionUtils.Decrypt(trackDetails.PersianDate, EncryptionUtils.Key),
                TrackActivity = EncryptionUtils.Decrypt(trackDetails.TrackActivity, EncryptionUtils.Key),
                TrackFood = EncryptionUtils.Decrypt(trackDetails.TrackFood, EncryptionUtils.Key),
                TrackWeight = EncryptionUtils.Decrypt(trackDetails.TrackWeight, EncryptionUtils.Key)
            };
        }

        public Device DeviceDecrypt(Device device)
        {
            return new Device()
            {
                User = device.User,
                Id = device.Id,
                UserId = device.UserId,
                CreateTime = device.CreateTime,
                Type = EncryptionUtils.Decrypt(device.Type, EncryptionUtils.Key),
                BuildUser = EncryptionUtils.Decrypt(device.BuildUser, EncryptionUtils.Key),
                Base = EncryptionUtils.Decrypt(device.Base, EncryptionUtils.Key),
                Incremental = EncryptionUtils.Decrypt(device.Incremental, EncryptionUtils.Key),
                VersionCode = EncryptionUtils.Decrypt(device.VersionCode, EncryptionUtils.Key),
                Host = EncryptionUtils.Decrypt(device.Host, EncryptionUtils.Key),
                Board = EncryptionUtils.Decrypt(device.Board, EncryptionUtils.Key),
                Brand = EncryptionUtils.Decrypt(device.Brand, EncryptionUtils.Key),
                BuildID = EncryptionUtils.Decrypt(device.BuildID, EncryptionUtils.Key),
                DeviceID = EncryptionUtils.Decrypt(device.DeviceID, EncryptionUtils.Key),
                FingerPrint = EncryptionUtils.Decrypt(device.FingerPrint, EncryptionUtils.Key),
                Manufacture = EncryptionUtils.Decrypt(device.Manufacture, EncryptionUtils.Key),
                Model = EncryptionUtils.Decrypt(device.Model, EncryptionUtils.Key),
                SDK = EncryptionUtils.Decrypt(device.SDK, EncryptionUtils.Key)
            };
        }

        public Location LocationDecrypt(Location location)
        {
            return new Location()
            {
                User = location.User,
                UserId = location.UserId,
                Id = location.Id,
                CreateTime = location.CreateTime,
                Accuracy = EncryptionUtils.Decrypt(location.Accuracy, EncryptionUtils.Key),
                Altitude = EncryptionUtils.Decrypt(location.Altitude, EncryptionUtils.Key),
                Latitude = EncryptionUtils.Decrypt(location.Latitude, EncryptionUtils.Key),
                Longitude = EncryptionUtils.Decrypt(location.Longitude, EncryptionUtils.Key),
                Provider = EncryptionUtils.Decrypt(location.Provider, EncryptionUtils.Key),
                Speed = EncryptionUtils.Decrypt(location.Speed, EncryptionUtils.Key),
                Time = EncryptionUtils.Decrypt(location.Time, EncryptionUtils.Key),
            };
        }
    }
}
