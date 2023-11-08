using ErrorOr;

namespace ProductHub.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");

        public static Error DuplicateUserName => Error.Conflict(
            code: "User.DuplicateUserName",
            description: "Username is already in use.");
        
        public static Error UserNotFound => Error.NotFound(
            code: "User.UserNotFound",
            description: "User not found.");
        
        public static Error UserCreationFailed => Error.Conflict(
            code: "User.UserCreationFailed",
            description: "User creation failed.");

        public static Error RoleAssignmentFailed => Error.Conflict(
            code: "User.RoleAssignmentFailed",
            description: "Role assignment failed.");
                        
    }
}