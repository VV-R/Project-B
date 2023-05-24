using System.Net.Mail;
namespace Entities;
public class User
{
    public int IdUser {get; set; }
    public UserInfo UserInfo {get; set; }
    public string Password {get; set; }
    public string Role {get; set; }
    public List<Ticket> Reservations = new List<Ticket>();
    
    public User (int id, UserInfo userInfo, string password)
    {
        IdUser = id;
        UserInfo = userInfo;
        Password = password;
        Role = "Customer";
    }

    public override string ToString()
    {
        return $"ID: {IdUser}; Name: {UserInfo.FirstName}{(UserInfo.Preposition != "" ? $" {UserInfo.Preposition}" : "")} {UserInfo.LastName}; Email: {UserInfo.Email}; Number: {UserInfo.PhoneNumber}";
    }

    public string ToNewLineString() => $"ID: {IdUser}\nName: {UserInfo.FirstName}{(UserInfo.Preposition != "" ? $" {UserInfo.Preposition}" : "")} {UserInfo.LastName}\nEmail: {UserInfo.Email}\nNumber: {UserInfo.PhoneNumber}";
}
public class UserInfo
{
    public string FirstName {get; set;}
    public string Preposition {get; set;}
    public string LastName {get; set;}
    public MailAddress? Email {get; set;}
    public string? PhoneNumber {get; set;}
    public DateTime DateOfBirth {get; set;}
    public string Nationality {get; set;}
    public string? DocumentNumber {get; set;}
    public string? DocumentType {get; set;}
    public DateTime? ExpirationDate {get; set;}
    public string? IBAN {get; set; }
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

    public UserInfo(string firstName, string preposition,
                    string lastname,DateTime dateofbirth, string nationality,
                    string documentNumber, DateTime expirationDate, string documentType)
    {
        FirstName = firstName;
        Preposition = preposition;
        LastName = lastname;
        DateOfBirth = dateofbirth;
        Nationality = nationality;
        DocumentNumber = documentNumber;
        DocumentType = documentType;
        ExpirationDate = expirationDate;
    }
}
