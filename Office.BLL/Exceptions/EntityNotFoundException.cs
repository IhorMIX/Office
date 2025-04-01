namespace Office.BLL.Exceptions;

public class EntityNotFoundException(string message) : CustomException(message);