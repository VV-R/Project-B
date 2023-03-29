using System.Net.Mail;
public class User
{
    public int IdUser;
    public string FirstName;
    public string Preposition;
    public string LastName;
    public string Password;
    public MailAddress Email;
    public string PhoneNumber;
    public DateTime DateOfBirth;
    public string Nationality;
    public string? DocumentNumber;
    public  string? DocumentType;
    public DateTime? ExpirationDate;
    public List<Ticket> Reservations = new List<Ticket>();
    

    public User (int id, string firtstname, string preposition, string lastname, 
                 string password, MailAddress email, string phonenumber, 
                 DateTime dateofbirth, string  nationality)
    {
        IdUser = id;
        FirstName = firtstname;
        Preposition = preposition;
        LastName = lastname;
        Password = password;
        Email = email;
        PhoneNumber = phonenumber;
        DateOfBirth = dateofbirth;
        Nationality = nationality;
    }
    public User (int id, string firtstname, string preposition, string lastname, 
                 string password, MailAddress email, string phonenumber, 
                 DateTime dateofbirth, string  nationality, string documentnumber,
                 string documenttype, DateTime expirationdate)
    {
        IdUser = id;
        FirstName = firtstname;
        Preposition = preposition;
        LastName = lastname;
        Password = password;
        Email = email;
        PhoneNumber = phonenumber;
        DateOfBirth = dateofbirth;
        Nationality = nationality;
        DocumentNumber = documentnumber;
        DocumentType = documenttype;
        ExpirationDate = expirationdate;
    }

    public override string ToString()
    {
        return $"ID: {IdUser}\nName: {FirstName} {LastName}\nEmail: {Email}\nNumber: {PhoneNumber}";
    }

}