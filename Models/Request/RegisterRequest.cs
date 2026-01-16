using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Request;

public class RegisterRequest
{
    [JsonPropertyName("namaLengkap")]
    public string NamaLengkap { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("noHP")]
    public string NoHP { get; set; } = string.Empty;

    [JsonPropertyName("kataSandi")]
    public string KataSandi { get; set; } = string.Empty;

    [JsonPropertyName("konfirmasiSandi")]
    public string KonfirmasiSandi { get; set; } = string.Empty;

    [JsonPropertyName("tanggalLahir")]
    public DateTime TanggalLahir { get; set; }

    [JsonPropertyName("idGender")]
    public string IdGender { get; set; } = string.Empty;

    [JsonPropertyName("alamatUtama")]
    public AlamatUtama? AlamatUtama { get; set; }
}

public class AlamatUtama
{
    [JsonPropertyName("provinsi")]
    public string Provinsi { get; set; } = string.Empty;

    [JsonPropertyName("kotaKabupaten")]
    public string KotaKabupaten { get; set; } = string.Empty;

    [JsonPropertyName("kecamatan")]
    public string Kecamatan { get; set; } = string.Empty;

    [JsonPropertyName("kodePos")]
    public string KodePos { get; set; } = string.Empty;

    [JsonPropertyName("alamatLengkap")]
    public string AlamatLengkap { get; set; } = string.Empty;
}
