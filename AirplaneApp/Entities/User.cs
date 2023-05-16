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
        return $"ID: {IdUser}; Name: {FirstName}{(Preposition != "" ? $" {Preposition}" : "")} {LastName}; Email: {Email}; Number: {PhoneNumber}";
    }

    public string ToNewLineString() => $"ID: {IdUser}\nName: {FirstName}{(Preposition != "" ? $" {Preposition}" : "")} {LastName}\nEmail: {Email}\nNumber: {PhoneNumber}";
}
