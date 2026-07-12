public class Il
{
    public int Id { get; set; }
    public string pr { get; set; } = string.Empty;
    public string cldeli { get; set; } = string.Empty;
    public string ftp { get; set; } = string.Empty;
    public int idcrac { get; set; }
    public int idte { get; set; }

    // vi es DATE en MySQL → usar DateTime
    public DateTime vi { get; set; }

    // mo es TIMESTAMP en MySQL → usar DateTime
    public DateTime mo { get; set; }

    public string bada { get; set; } = string.Empty;
    public string lo { get; set; } = string.Empty;
    public int nueq { get; set; } = 1;
    public string eq { get; set; } = string.Empty;
    public string idso { get; set; } = string.Empty;
    public int piso { get; set; } = 0;
    public string idsepe { get; set; } = string.Empty;
    public string idsono { get; set; } = string.Empty;
    public int pisono { get; set; } = 0;
}
