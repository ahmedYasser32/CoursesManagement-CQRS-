using CoursesCQRS.Application.Features.Books.Models;


using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Books.Commands
{


  public class CreateBookCommand : CreateBookDTO, IRequest<Book>
  {

  }
  public class Handler : IRequestHandler<CreateBookCommand, Book>
  {
    public static string EncryptPdf(string path,string filename)
    {

      DirectoryInfo folder = new DirectoryInfo(@"C:\Users\mfahmed\Desktop\CoursesCQRS\CoursesCQRS\Files\Books\");

      if (folder.Exists == false)
      {

        folder.Create();
        Console.WriteLine("Created Folder success");
      }


       string filenameDest = @$"C:\Users\mfahmed\Desktop\CoursesCQRS\CoursesCQRS\Files\Books\";

      File.Copy(Path.Combine("../../../../../PDFs/", path),
        Path.Combine(Directory.GetCurrentDirectory(), filenameDest), true);
      // Open an existing document. Providing an unrequired password is ignored.

      PdfDocument document = PdfReader.Open(filenameDest, "some text");

      PdfSecuritySettings securitySettings = document.SecuritySettings;


      // Setting one of the passwords automatically sets the security level to

      // PdfDocumentSecurityLevel.Encrypted128Bit.


      securitySettings.UserPassword = "!1i4 % S4p5sf % SA % SVe1599c %99c2 S2l6";
      securitySettings.OwnerPassword = "%SVe1599c2l64A! % 99c2S 99c2% S1i44p5sfA % S!1i44p5sfA %99c2 SVe1599c2l6";


      // Don't use 40 bit encryption unless needed for compatibility
      //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;
      securitySettings.PermitAccessibilityExtractContent = false;

      securitySettings.PermitAnnotations = false;

      securitySettings.PermitAssembleDocument = false;

      securitySettings.PermitExtractContent = false;

      securitySettings.PermitFormsFill = false;

      securitySettings.PermitFullQualityPrint = false;

      securitySettings.PermitModifyDocument = false;

      securitySettings.PermitPrint = false;

      //to prevent encoding errors
      System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

      // Save the document...
      document.Save(filenameDest);

      Console.WriteLine("new doc saved!");
      return filenameDest;

    }

    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }

    public async Task<Book> Handle(CreateBookCommand obj, CancellationToken cancellationToken)
    {
      try
      {
        // 1 ) Get Directory


        string BookFolderPath = Directory.GetCurrentDirectory() + @"/Files/Books/";


        //2) Get File Name

        string FileName = Guid.NewGuid() + Path.GetFileName(obj.File.FileName);


        // 3) Merge Path with File Name

        string FinalBookPath = Path.Combine(BookFolderPath, FileName);

    



        //4) Save File As Streams "Data Overtime"


        using (var Stream = new FileStream(FinalBookPath, FileMode.Create))

        {

          obj.File.CopyTo(Stream);

        }



        FinalBookPath = EncryptPdf(BookFolderPath, FileName);
        var entity = new Book()
        {
          CreatedAt = DateTime.UtcNow,
          Description = obj.Description,
          Title = obj.Title,
          File = obj.Filepath,
        
        };
              FinalBookPath = EncryptPdf(FinalBookPath, FileName);

        await db.Book.AddAsync(entity);


        await db.SaveChangesAsync();



        return entity;
      }

      catch (Exception)
      {

        throw;
      }
    }
  }
}

