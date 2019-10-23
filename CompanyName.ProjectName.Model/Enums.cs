using System;

namespace CompanyName.ProjectName.Model
{
    public enum JoiningPlatform
    {
        Desktop = 1,
        Mobile = 2
    }

    public enum EntityStatus
    {
        InActive = 0,
        Active = 1,
    }

    public enum ErrorCode
    {
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        NotUniqueEmail = 10001,
        NotUniquePhone = 10002,
        InvalidPhoneOrEmail = 10003
    }
}
