//VOID
void creaCodice(string cognome, ref string codice,string nome, string data, string sesso, string comune)
{
    string vocali = "AEIOU", nomeControllo = "", lettere = "ABCDEHLMPRST", percorso = "../../../../comuni.txt";
    string[] mesi = { "gennaio", "febbraio", "marzo", "aprile",
        "maggio", "giugno", "luglio", "agosto", "settembre", "ottobre", "novembre", "dicembre"};
    int valoreFinale = 0;
    string caratteri = "A0B1C2D3E4F5G6H7I8J9K L M N O P Q R S T U V W X Y Z";
    int[] valori = { 1,1, 0,0, 5,5, 7,7, 9,9, 13,13, 15,15, 17,17, 19,19, 21,0, 2,0, 4,0, 18,0, 20,0, 11,0, 3,0, 6,0, 8,0, 12,0, 14,0, 16,0, 10,0, 22,0, 25,0, 24,0, 23 };
    //COGNOME
    for(int i = 0; i < cognome.Length && codice.Length<3; i++)
    {
        if (!vocali.Contains(cognome[i]))
        {
            codice += cognome[i];
        }
    }
    for(int i=0;i<cognome.Length && codice.Length < 3; i++)
    {
        if (vocali.Contains(cognome[i]))
        {
            codice += cognome[i];
        }
    }
    while(codice.Length < 3)
    {
        codice += 'X';
    }
    //NOME
    for(int i=0;i<nome.Length && nomeControllo.Length <=4; i++)
    {
        if (!vocali.Contains(nome[i]))
        {
            nomeControllo+= nome[i];
        }
    }
    if(nomeControllo.Length >= 4)
    {
        codice += nomeControllo[0] + nomeControllo.Substring(2);
    }
    else
    {
        codice += nomeControllo;
    }
    for(int i=0; i < nome.Length && codice.Length < 6; i++)
    {
        if (vocali.Contains(nome[i]))
        {
            codice += nome[i];
        }
    }
    while (codice.Length < 6)
    {
        codice += 'X';
    }
    string[] datiNascita = data.Split('/');
    //ANNO
    codice += datiNascita[0].Substring(datiNascita[0].Length - 2);
    //MESE
    codice += lettere[int.Parse(datiNascita[1])-1];
    //GIORNO
    if (sesso == "M")
    {
        codice += datiNascita[2];
    }
    else
    {
        codice += (int.Parse(datiNascita[2] + 40)).ToString();
    }
    //COMUNE
    string fileCompleto=File.ReadAllText(percorso);
    string[] codici = fileCompleto.Split(';');
    for(int i = 5; i < codici.Length; i += 25)
    {
        if (comune.ToUpper() == codici[i].ToUpper())
        {
            codice += codici[i + 12];
        }
    }
    //ULTIMA CIFRA
    for(int i=1;i< codice.Length; i += 2)
    {
            valoreFinale += caratteri.IndexOf(codice[i]) / 2;
    }
    for(int i=0;i< codice.Length; i += 2)
    {
            valoreFinale += valori[caratteri.IndexOf(codice[i])];
    }
    codice += caratteri[(valoreFinale %26)*2];
}
//MAIN
string codice="";
Console.WriteLine("Dammi il cognome");
string cogn = Console.ReadLine();
cogn=cogn.ToUpper().Replace(" ", "").Replace("'", "");

Console.WriteLine("Dammi il nome (se ne hai più di uno mettili tutti separati da uno spazio)");
string nome=Console.ReadLine();
nome = nome.Replace(" ", "").ToUpper();

Console.WriteLine("Dammi la tua data di nascita (AAAA/MM/GG)");
string data = Console.ReadLine();

Console.WriteLine("Dimmi il tuo sesso (M o F)");
string sesso=Console.ReadLine();
sesso = sesso.ToUpper();

Console.WriteLine("Dimmi il comune di nascita");
string comune=Console.ReadLine();

creaCodice(cogn, ref codice,nome, data, sesso, comune);
Console.WriteLine($"Il tuo codice fiscale è {codice}");