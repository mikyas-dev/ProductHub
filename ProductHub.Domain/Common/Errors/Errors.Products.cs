using ErrorOr;

namespace ProductHub.Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NameIsAlreadyTaken => Error.Validation(
            code: "Product.NameIsAlreadyTaken",
            description: "Name is already taken.");
        
        public static Error DescriptionIsTooLong => Error.Validation(
            code: "Product.DescriptionIsTooLong",
            description: "Description is too long.");
        
        public static Error NameIsTooLong => Error.Validation(
            code: "Product.NameIsTooLong",
            description: "Name is too long.");

        public static Error NameIsTooShort => Error.Validation(
            code: "Product.NameIsTooShort",
            description: "Name is too short.");

        public static Error ProductNotFound => Error.Validation(
            code: "Product.ProductNotFound",
            description: "Product not found.");

        public static Error ProductNotCreated => Error.Validation(
            code: "Product.ProductNotCreated",
            description: "Product not created.");

    }
}