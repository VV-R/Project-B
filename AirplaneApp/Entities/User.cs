using System.Net.Mail;

namespace Entities;
public class User
{
    public int IdUser { get; set; }
    public string FirstName { get; set; }
    public string Preposition { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public MailAddress Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string? DocumentNumber { get; set; }
    public  string? DocumentType { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public List<Ticket> Reservations = new List<Ticket>();


    public User (int idUser, string firstName, string preposition, string lastName,
                 string password, MailAddress email, string phoneNumber,
                 DateTime dateOfBirth, string nationality)
    {
        IdUser = idUser;
        FirstName = firstName;
        Preposition = preposition;
        LastName = lastName;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
    }
    public User (int idUser, string firstName, string preposition, string lastName, 
                 string password, MailAddress email, string phoneNumber, 
                 DateTime dateOfBirth, string  nationality, string documentNumber,
                 string documentType, DateTime expirationDate)
    {
        IdUser = idUser;
        FirstName = firstName;
        Preposition = preposition;
        LastName = lastName;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        DocumentNumber = documentNumber;
        DocumentType = documentType;
        ExpirationDate = expirationDate;
    }

    public override string ToString()
    {
        return $"ID: {IdUser}; Name: {FirstName}{(Preposition != "" ? $" {Preposition}" : "")} {LastName}; Email: {Email}; Number: {PhoneNumber}";
    }

    public string ToNewLineString() => $"ID: {IdUser}\nName: {FirstName}{(Preposition != "" ? $" {Preposition}" : "")} {LastName}\nEmail: {Email}\nNumber: {PhoneNumber}";
}
