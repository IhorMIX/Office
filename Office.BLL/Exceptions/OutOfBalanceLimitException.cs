namespace Office.BLL.Exceptions;

public class OutOfBalanceLimitException(string message) : CustomException(message);