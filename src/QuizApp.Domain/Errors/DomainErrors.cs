using QuizApp.Domain.Shared;

namespace QuizApp.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "User NotFound",
            message: $"The user with the identifier {id} was not found.");
    }

    public static class OtpCode
    {
        public static readonly Func<Error> InValid = () => new Error(
            code: "Otp InValid",
            message: "Invalid otp code");

        public static readonly Func<Error> Expired = () => new Error(
            code: "Otp.Expired",
            message: "Expired otp code");
    }

    public static class Tag
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "Tag NotFound",
            message: $"The tag with the identifier {id} was not found.");
    }
    public static class Exam
    {
        public static readonly Func<string, Error> InvalidExamProperties = errors => new Error(
            code: "Exam properties is invalid",
            message: errors);

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "Exam NotFound",
            message: $"The exam with the identifier {id} was not found.");
    }
    public static class Question
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "Question NotFound",
            message: $"The Question with the identifier {id} was not found.");
    }
    public static class ExamAttendant
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
           code: "Exam Attendant NotFound",
           message: $"The Exam Attendant with the identifier {id} was not found.");

    }
}
