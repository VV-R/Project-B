using System.Globalization;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

class EmailJsonConverter : JsonConverter<MailAddress> {
    public override MailAddress Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) {
        return new MailAddress(reader.GetString()!);
    }

    public override void Write(
        Utf8JsonWriter writer,
        MailAddress email,
        JsonSerializerOptions options
    ) {
        writer.WriteStringValue(email.ToString());
    }
}
