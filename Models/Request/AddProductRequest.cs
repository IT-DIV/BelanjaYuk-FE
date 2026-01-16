namespace BelanjaYuk.Client.Models.Request
{
    public class AddProductRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string IdKategori { get; set; } = string.Empty;
        public string NamaBarang { get; set; } = string.Empty;
        public string DeskripsiBarang { get; set; } = string.Empty;
        public decimal Harga { get; set; }
        public decimal Diskon { get; set; }
        public int Stok { get; set; }
    }
}
