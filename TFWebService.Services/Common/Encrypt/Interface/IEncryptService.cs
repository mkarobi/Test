using System;
using System.Collections.Generic;
using System.Text;
using TFWebService.Data.Dtos.Api.User;
using TFWebService.Data.Models;

namespace TFWebService.Services.Common.Encrypt.Interface
{
    public interface IEncryptService
    {
        UserForUpdateDto UpdateUserDecrypt(UserForUpdateDto userForUpdateDto);
        User UserEncrypt(User user);
        MainDetails MainDetailsEncrypt (MainDetails mainDetails);
        MainDetails MainDetailsDecrypt (MainDetails mainDetails);
        TrackDetails TrackDetailsEncrypt (TrackDetails trackDetails);
        TrackDetails TrackDetailsDecrypt(TrackDetails trackDetails);
    }
}
