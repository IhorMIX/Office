namespace Office.BLL.Exceptions;

public class NotPermissionException(string message) : CustomException(message);