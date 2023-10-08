namespace Lab4;
/// <summary>
/// Consolidated error reporting to a custom class that works better with DisplayAlert().
/// </summary>
public class ErrorHandling
{
    private readonly Constants.ErrorType errorType; //keep Enum to check if ErrorType.NoError; any other type requires message display
    private readonly string message = "";           //message to display in even of error type not of ErrorType.NoError

    public Constants.ErrorType ErrorType { get { return errorType; } }
    public string Message { get { return message; } }

    /// <summary>
    /// ErrorHandling instance w/ no values to insert into message param OR ErrorType.NoError w/ empty string.
    /// </summary>
    /// <param name="errorType"> an enum field from Constants.ErrorType </param>
    /// <param name="message"> a string represnting the error message from Constants.cs  </param>
    public ErrorHandling(Constants.ErrorType errorType, string message)
    {
        this.errorType = errorType;
        this.message = message;
    }

    /// <summary>
    /// ErrorHandling instance w/ a single value to replace in message string.
    /// Didn't expand to multiple values for lack of time; may update in future iterations.
    /// </summary>
    /// <param name="errorType"> an enum field from Constants.ErrorType </param>
    /// <param name="message"> a string represnting the error message from Constants.cs </param>
    /// <param name="errorValue1"> the value being injected into the error message using standard string formatting </param>
    public ErrorHandling(Constants.ErrorType errorType, string message, string errorValue1)
    {
        this.errorType = errorType;
        this.message = string.Format(message, errorValue1);
    }
}