namespace Office.BLL.Exceptions;

public class AlreadyLoginException(string message) : CustomException(message);