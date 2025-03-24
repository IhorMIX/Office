namespace Office.BLL.Exceptions;

public class PositionNotFoundException(string message) : CustomException(message);