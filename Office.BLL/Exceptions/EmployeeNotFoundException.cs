namespace Office.BLL.Exceptions;

public class EmployeeNotFoundException(string message) : CustomException(message);