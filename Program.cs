using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGeetYou
{
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public long ContactPhoneNumber { get; set; }
    }

    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }
    }
    class TheAlienJordansContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=TheAlienJordansUnremarkableRecordLabelCompanyDB");
        }
    }
    class Program
    {
        // Generic Menu Options Method From First Bank Of Suncoast. Not Customized for this App but works nonetheless.

        static int CheckUserChoice(int options, int tryinput)
        {
            int choice = 1000;
            bool trueInt = false;
            int tries = -1;
            while (trueInt == false)
            {
                tries++;
                if (tries == tryinput && tries != 1)
                {
                    Console.WriteLine("Amount of input tries has been exceeded.");
                    trueInt = true;
                    choice = 2;
                }
                if (trueInt == false)
                {
                    trueInt = int.TryParse(Console.ReadLine(), out choice);
                    if (trueInt == false || choice > options || choice <= 0)
                    {
                        Console.WriteLine("Input was not recognized. Try again.");
                        trueInt = false;
                    }
                }
            }
            return choice;
        }
        static void Main(string[] args)
        {

            // Initializing connections to database created

            bool isRunning = true;

            while (isRunning == true)
            {
                var context = new TheAlienJordansContext();
                var bands = context.Bands.OrderBy(band => band.Id);
                var albums = context.Albums;//.Include(band => band.Id);
                Console.Clear();
                Console.WriteLine("\r\n__________________________________________________________");
                Console.WriteLine("Welcome to AlienJordans Record Label DB Management System!");
                Console.WriteLine("----------------------------------------------------------\r\n");
                Console.WriteLine("{ 1 }" + "ADD a new BAND");
                Console.WriteLine("{ 2 }" + "VIEW all BANDS");
                Console.WriteLine("{ 3 }" + "ADD an ALBUM for a BAND");
                Console.WriteLine("{ 4 }" + "LET a BAND GO (unsign from label)");
                Console.WriteLine("{ 5 }" + "SIGN ON a BAND to label");
                Console.WriteLine("{ 6 }" + "FIND ALL ALBUMS from a BAND");
                Console.WriteLine("{ 7 }" + "VIEW ALL ALBUMS by RELEASE DATE");
                Console.WriteLine("{ 8 }" + "VIEW ALL BANDS SIGNED on to label");
                Console.WriteLine("{ 9 }" + "VIEW ALL BANDS NOT SIGNED on to label");
                Console.WriteLine("{ 10 }" + "QUIT the system");
                int choice = CheckUserChoice(10, 1);

                switch (choice)
                {

                    // Adding a Band

                    case 1:
                        Console.Clear();
                        Console.WriteLine("Please enter the bands name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please enter the Country of Origin of the band:");
                        string countryOfOrigin = Console.ReadLine();
                        Console.WriteLine("Please enter the number of members in the band:");
                        int numberOfMembers = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the bands official website:");
                        string website = Console.ReadLine();
                        Console.WriteLine("Please enter the bands genre style:");
                        string style = Console.ReadLine();
                        Console.WriteLine("Please enter the primary person to contact for the band:");
                        string contactName = Console.ReadLine();
                        Console.WriteLine("Please enter international phone number of the person to contact:");
                        long contactPhoneNumber = long.Parse(Console.ReadLine());

                        var newBand = new Band
                        {
                            Name = name,
                            CountryOfOrigin = countryOfOrigin,
                            NumberOfMembers = numberOfMembers,
                            Website = website,
                            Style = style,
                            IsSigned = false,
                            ContactName = contactName,
                            ContactPhoneNumber = contactPhoneNumber
                        };
                        context.Bands.Add(newBand);
                        context.SaveChanges();
                        Console.Clear();
                        Console.WriteLine($"{name} has been added to the system. Currently, {name} is not signed onto the label. If you want to sign them on, go to option 5 on the menu.");
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Viewing all Bands

                    case 2:
                        Console.Clear();
                        Console.WriteLine($"Viewing all the bands currently in the System. There are {bands.Count()} bands.");
                        Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
                        foreach (Band band in bands)
                        {
                            Console.WriteLine($"_____________________________________________________________________________________________________________");
                            Console.WriteLine($"{band.Name} is a {band.NumberOfMembers} member group from {band.CountryOfOrigin}. They perform {band.Style}.\r\nContact {band.ContactName} at {band.ContactPhoneNumber}\r\n");
                        }
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Adding an Album. Easier than I expected

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Please enter the Title of the Album:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Does the album contain explicit language?\r\n { 1 } Yes\r\n { 2 } No");
                        choice = CheckUserChoice(2, 1);
                        bool isExplicit = (choice == 1) ? true : false;
                        Console.WriteLine("What was the release date of the album:\r\n Format: yyyy/mm/dd");
                        string releaseDate = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Which band did the album originate from?");
                        int iiii = 1;
                        foreach (Band band in bands)
                        {
                            Console.WriteLine("{ " + $"{iiii}" + " }" + $"{band.Name}");
                            iiii++;
                        }
                        int bandId = CheckUserChoice(iiii, 1);
                        var newAlbum = new Album
                        {
                            Title = title,
                            IsExplicit = isExplicit,
                            ReleaseDate = Convert.ToDateTime(releaseDate),
                            BandId = bandId,
                        };
                        context.Albums.Add(newAlbum);
                        context.SaveChanges();
                        Console.Clear();
                        Console.WriteLine($"{title} has been added to the system.");
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();

                        break;

                    // Firing a band. Here I realized that dbset doesn't like index[] syntax, so I had to fish for names instead...

                    case 4:
                        Console.Clear();
                        var signedBandsToFire = context.Bands.Where(band => band.IsSigned == true);
                        var fireList = new List<string>();
                        foreach (Band band in signedBandsToFire)
                        {
                            fireList.Add(band.Name);
                        }
                        Console.WriteLine("Please choose a band to unsign.");
                        int ii = 1;
                        foreach (Band band in signedBandsToFire)
                        {
                            Console.WriteLine("{ " + $"{ii}" + " }" + $"{band.Name}");
                            ii++;
                        }
                        choice = CheckUserChoice(ii, 1);

                        var chosenBandToFire = signedBandsToFire.First(band => band.Name == fireList[choice - 1]);
                        chosenBandToFire.IsSigned = false;
                        context.Entry(chosenBandToFire).State = EntityState.Modified;
                        context.SaveChanges();
                        Console.Clear();
                        Console.WriteLine($"{chosenBandToFire.Name} is no longer signed with The Alien Jordans Unremarkable Record Label Company");
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Hire a band

                    case 5:
                        Console.Clear();
                        var signedBandsToHire = context.Bands.Where(band => band.IsSigned == false);
                        var hireList = new List<string>();
                        foreach (Band band in signedBandsToHire)
                        {
                            hireList.Add(band.Name);
                        }
                        Console.WriteLine("Please choose a band to sign on.");
                        int iii = 1;
                        foreach (Band band in signedBandsToHire)
                        {
                            Console.WriteLine("{ " + $"{iii}" + " }" + $"{band.Name}");
                            iii++;
                        }
                        choice = CheckUserChoice(iii, 1);
                        var chosenBandToHire = context.Bands.First(band => band.Name == hireList[choice - 1]);
                        chosenBandToHire.IsSigned = true;
                        context.Entry(chosenBandToHire).State = EntityState.Modified;
                        context.SaveChanges();
                        Console.Clear();
                        Console.WriteLine($"{chosenBandToHire.Name} is now signed with The Alien Jordans Unremarkable Record Label Company");
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    /* Viewing albums by band.
                    I did this before case 4. After I did case 4, I realized I missed an important edge case. If the database table's Serial Primary Key
                    has gaps in its entry, the users choice would not line up with the correct Band.Id :-/ Will need to fix in the future*/

                    case 6:
                        Console.Clear();
                        Console.WriteLine("Please choose a band to view their albums");
                        int i = 1;
                        foreach (Band band in bands)
                        {
                            Console.WriteLine("{ " + $"{i}" + " }" + $"{band.Name}");
                            i++;
                        }
                        choice = CheckUserChoice(i, 1);
                        var chosenBand = context.Bands.First(band => band.Id == choice);
                        var chosenAlbums = context.Albums.Where(album => album.BandId == choice);
                        Console.Clear();
                        if (chosenAlbums.Count() >= 1)
                        {
                            Console.WriteLine($"Here are all the albums created by {chosenBand.Name}.");
                            foreach (Album album in chosenAlbums)
                            {
                                Console.WriteLine($"{album.Title}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"It looks like {chosenBand.Name} doesn't have any albums on record.");
                        }
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;
                    // All Albums by Release Date
                    case 7:
                        Console.Clear();
                        var albumsByReleaseDate = context.Albums.OrderBy(album => album.ReleaseDate);
                        Console.WriteLine($"Here are all the albums in the system by release order.\r\nThere are {albums.Count()} albums.");
                        Console.WriteLine("-------------------------------------------------------");
                        foreach (Album album in albumsByReleaseDate)
                        {
                            Console.WriteLine($"{album.Title} was released on {album.ReleaseDate}");
                        }
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Viewing all bands signed on

                    case 8:
                        Console.Clear();
                        var signedBands = context.Bands.Where(band => band.IsSigned == true);
                        Console.WriteLine($"Here are all the bands in the system that are signed with us.\r\nThere are {signedBands.Count()} bands.");
                        Console.WriteLine("--------------------------------------------------------------");
                        foreach (Band band in signedBands)
                        {
                            Console.WriteLine($"{band.Name}");
                        }
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Viewing All bands not signed

                    case 9:
                        Console.Clear();
                        var unsignedBands = context.Bands.Where(band => band.IsSigned == false);
                        Console.WriteLine($"Here are all the bands in the system that are not signed with us.\r\nThere are {unsignedBands.Count()} bands.");
                        Console.WriteLine("------------------------------------------------------------------");
                        foreach (Band band in unsignedBands)
                        {
                            Console.WriteLine($"{band.Name}");
                        }
                        Console.WriteLine("\r\n_____________________________");
                        Console.WriteLine("Press Enter to return to Menu");
                        Console.ReadLine();
                        break;

                    // Quit

                    case 10:
                        isRunning = false;
                        break;

                }

            }

        }
    }
}
