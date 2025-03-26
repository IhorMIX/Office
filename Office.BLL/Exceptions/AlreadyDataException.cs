namespace Office.BLL.Exceptions;

public class AlreadyDataException(string message) : CustomException(message);