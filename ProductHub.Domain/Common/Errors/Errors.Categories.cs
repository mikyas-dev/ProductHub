using ErrorOr;

namespace ProductHub.Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NameIsAlreadyTaken => Error.Validation(
            code: "Category.NameIsAlreadyTaken",
            description: "Name is already taken.");
        
        public static Error DescriptionIsTooLong => Error.Validation(
            code: "Category.DescriptionIsTooLong",
            description: "Description is too long.");
        
        public static Error NameIsTooLong => Error.Validation(
            code: "Category.NameIsTooLong",
            description: "Name is too long.");

        public static Error NameIsTooShort => Error.Validation(
            code: "Category.NameIsTooShort",
            description: "Name is too short.");

        public static Error CategoryNotFound => Error.Validation(
            code: "Category.CategoryNotFound",
            description: "Category not found.");

    }
}