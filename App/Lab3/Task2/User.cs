namespace App.Lab3.Task2;

public class User
{
    private readonly Guid id;
    private readonly DateTime registerDate;
    private string login, password, name, surname, inn, phone;

    public Guid Id { get; init; }
    public DateTime RegisterDate { get; init; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Inn { get; set; }
    public string Phone {
        get => phone;
        set
        {
            if (!IsPhoneValid(value))
            {
                throw new ArgumentException("Phone is not valid");
            }
            
            phone = value;
        }
    }

    public User()
    {
        id = Guid.NewGuid();
        registerDate = DateTime.Now;
    }

    public User(string login, string password, string name, string surname, string inn, string phone)
    {
        id = Guid.NewGuid();
        registerDate = DateTime.Now;
        
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        this.phone = phone;
    }

    string GetUserFullName()
    {
        return $"{name} {surname}";
    }

    public bool TryUpdatePhone(string phone)
    {
        var isValid = IsPhoneValid(phone);
        if (!isValid)
        {
            return false;
        }
        
        this.phone = phone;
        return true;
    }

    private static bool IsPhoneValid(string phone)
    {
        var cleanPhone = phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        
        if (!cleanPhone.StartsWith("+7") &&  !cleanPhone.StartsWith("8"))
        {
            return false;
        }
        return cleanPhone.Replace("+", " ").Length == 11;
    }
}