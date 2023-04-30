using System.Reflection.Metadata.Ecma335;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Enum
{
    public enum EventType
    {
        UserAuthenticationStarted = 0,
        UserAuthenticationSuccessful = 1,
        UserAuthenticationFailed = 2,

        UserAuthorizationGranted = 1000,

        UserPasswordChangeStarted = 2000,
        UserPasswordChangeSuccessfull = 2001,
        UserPasswordChangeFailed = 2002,

        UserTwoFactorAuthenticationStarted = 3000,
        UserTwoFactorAuthenticationActivated = 3001,
        UserTwoFactorAuthenticationFailed = 3002,

        ApplicationValidation = 4000,

        
    }

    public class EventypesSets
    {
        List<EventType> UserAuthenticationEvents => new List<EventType> { EventType.UserAuthenticationStarted, EventType.UserAuthenticationSuccessful, EventType.UserAuthenticationFailed };
    }
}
