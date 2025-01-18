using eVault.Domain.Models;

namespace eVault.Domain.Constants
{
    public static class ApplicationResources
    {
        #region Result Messages And Codes

        // Messages
        public const string SuccessMessage = "Success";

        public const string FailureMessage = "Internal Server Error";

        public const string NotFoundMessage = "Requested resource was not found.";

        public const string BadRequestMessage = "Some of the values are invalid.";

        public const string ConflictMessage = "There was a conflict in your request.";

        public const string UnauthorizedMessage = "Please log in to your account.";

        public const string ForbiddenMessage = "You don't have access to this resource.";

        // Codes
        public const string SuccessCode = "Success";

        public const string FailureCode = "InternalServerError";

        public const string NotFoundCode = "NotFound";

        public const string BadRequestCode = "BadRequest";

        public const string ConflictCode = "Conflict";

        public const string UnauthorizedCode = "Unauthorized";

        public const string ForbiddenCode = "Forbidden";

        public const string ResultSystemFailure = "ResultSystemFailure";

        #endregion

        #region Generic Response Methods

        public static string GetSuccessfullySavedString(string resourceName) => $"{resourceName} was saved successfully.";

        public static string GetSuccessfullyDeletedString(string resourceName) => $"{resourceName} was deleted successfully.";

        public static string GetSuccessfullyUpdatedString(string resourceName) => $"{resourceName} was updated successfully.";

        public static string GetResourceNotFoundString(string resourceName) => $"{resourceName} was not found.";

        public static string GetResourceExistsString(string resourceName) => $"{resourceName} already exists in the system.";

        #endregion

        public const string ErrorSavingChanges = "There was an error while saving changes.";
    }
}
