using System.Net.Mail;
namespace Entities;
public class User
{
    public int IdUser;
    public UserInfo UserInfo;
    public string? Password;
    public List<Ticket> Reservations = new List<Ticket>();
    
    public User (int id, UserInfo userInfo, string password)
    {
        IdUser = id;
        UserInfo = userInfo;
        Password = password;
    }
    public User(int id, UserInfo userInfo)
    {
        IdUser = id;
        UserInfo = userInfo;
    }

    public override string ToString()
    {
        return $"ID: {IdUser}; Name: {UserInfo.FirstName}{(UserInfo.Preposition != "" ? $" {UserInfo.Preposition}" : "")} {UserInfo.LastName}; Email: {UserInfo.Email}; Number: {UserInfo.PhoneNumber}";
    }

    public string ToNewLineString() => $"ID: {IdUser}\nName: {UserInfo.FirstName}{(UserInfo.Preposition != "" ? $" {UserInfo.Preposition}" : "")} {UserInfo.LastName}\nEmail: {UserInfo.Email}\nNumber: {UserInfo.PhoneNumber}";
}
public class UserInfo
{
    public string FirstName;
    public string Preposition;
    public string LastName;
    public MailAddress Email;
    public string PhoneNumber;
    public DateTime DateOfBirth;
    public string Nationality;
    public string? DocumentNumber;
    public string? DocumentType;
    public DateTime? ExpirationDate;
   public UserInfo(string firstName, string preposition, 
                    string lastname, MailAddress email, string phonenumber, 
                    DateTime dateofbirth, string  nationality, string documentNumber, string documenttype, DateTime expirationDate)
    {
        FirstName = firstName;
        Preposition = preposition;
        LastName = lastname;
        Email = email;
        PhoneNumber = phonenumber;
        DateOfBirth = dateofbirth;
        Nationality = nationality;
        DocumentNumber = documentNumber;
        DocumentType = documenttype;
        ExpirationDate = expirationDate;
    }
    public UserInfo(string firstName, string preposition, 
                    string lastname, MailAddress email, string phonenumber, 
                    DateTime dateofbirth, string  nationality)
    {

        FirstName = firstName;
        Preposition = preposition;
        LastName = lastname;
        Email = email;
        PhoneNumber = phonenumber;
        DateOfBirth = dateofbirth;
        Nationality = nationality;
    }
}