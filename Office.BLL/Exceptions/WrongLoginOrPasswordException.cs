namespace Office.BLL.Exceptions;

public class WrongLoginOrPasswordException(string message) : CustomException(message);