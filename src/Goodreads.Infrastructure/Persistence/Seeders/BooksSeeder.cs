using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Persistence.Seeders;

internal class BooksSeeder(ApplicationDbContext dbContext) : ISeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync() &&
            !await dbContext.Books.AnyAsync())
        {
            var books = GetBooks();
            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<Book> GetBooks()
    {
        return new List<Book>
        {
            // نجيب محفوظ
            new Book { Title = "بين القصرين", Description = "الجزء الأول من ثلاثية القاهرة.", AuthorId = "1", Language = "Arabic", ISBN = "9789770932534", PublicationDate = new DateOnly(1956, 1, 1), PageCount = 495, Publisher = "دار الشروق" },
            new Book { Title = "قصر الشوق", Description = "الجزء الثاني من ثلاثية القاهرة.", AuthorId = "1", Language = "Arabic", ISBN = "9789770932541", PublicationDate = new DateOnly(1957, 1, 1), PageCount = 422, Publisher = "دار الشروق" },
            new Book { Title = "السكرية", Description = "الجزء الثالث من ثلاثية القاهرة.", AuthorId = "1", Language = "Arabic", ISBN = "9789770932558", PublicationDate = new DateOnly(1957, 1, 1), PageCount = 400, Publisher = "دار الشروق" },
            new Book { Title = "اللص والكلاب", Description = "رواية اجتماعية.", AuthorId = "1", Language = "Arabic", ISBN = "9789770932565", PublicationDate = new DateOnly(1961, 1, 1), PageCount = 180, Publisher = "دار الشروق" },
            new Book { Title = "زقاق المدق", Description = "رواية عن الحياة الشعبية.", AuthorId = "1", Language = "Arabic", ISBN = "9789770932572", PublicationDate = new DateOnly(1947, 1, 1), PageCount = 240, Publisher = "دار الشروق" },

            // طه حسين
            new Book { Title = "الأيام", Description = "سيرة ذاتية.", AuthorId = "2", Language = "Arabic", ISBN = "9789770906078", PublicationDate = new DateOnly(1929, 1, 1), PageCount = 300, Publisher = "دار المعارف" },
            new Book { Title = "دعاء الكروان", Description = "رواية اجتماعية.", AuthorId = "2", Language = "Arabic", ISBN = "9789770906085", PublicationDate = new DateOnly(1934, 1, 1), PageCount = 200, Publisher = "دار المعارف" },

            // أليفه رفعت
            new Book { Title = "حياتي: قصص قصيرة", Description = "مجموعة قصصية.", AuthorId = "3", Language = "Arabic", ISBN = "9789770906092", PublicationDate = new DateOnly(1974, 1, 1), PageCount = 120, Publisher = "دار الهلال" },

            // غسان كنفاني
            new Book { Title = "رجال في الشمس", Description = "رواية فلسطينية.", AuthorId = "4", Language = "Arabic", ISBN = "9789953890812", PublicationDate = new DateOnly(1963, 1, 1), PageCount = 104, Publisher = "مؤسسة الأبحاث العربية" },
            new Book { Title = "عائد إلى حيفا", Description = "رواية فلسطينية.", AuthorId = "4", Language = "Arabic", ISBN = "9789953890829", PublicationDate = new DateOnly(1970, 1, 1), PageCount = 80, Publisher = "مؤسسة الأبحاث العربية" },

            // جبر إبراهيم جبر
            new Book { Title = "البحث عن وليد مسعود", Description = "رواية فلسطينية.", AuthorId = "5", Language = "Arabic", ISBN = "9789953890836", PublicationDate = new DateOnly(1978, 1, 1), PageCount = 400, Publisher = "مؤسسة الأبحاث العربية" },

            // إلياس خوري
            new Book { Title = "باب الشمس", Description = "رواية فلسطينية.", AuthorId = "6", Language = "Arabic", ISBN = "9789953890843", PublicationDate = new DateOnly(1998, 1, 1), PageCount = 540, Publisher = "دار الآداب" },

            // نزار قباني
            new Book { Title = "قصائد حب عربية", Description = "ديوان شعر.", AuthorId = "7", Language = "Arabic", ISBN = "9789953890850", PublicationDate = new DateOnly(1973, 1, 1), PageCount = 180, Publisher = "منشورات نزار قباني" },

            // محمود درويش
            new Book { Title = "أثر الفراشة", Description = "ديوان شعر.", AuthorId = "8", Language = "Arabic", ISBN = "9789953890867", PublicationDate = new DateOnly(2008, 1, 1), PageCount = 120, Publisher = "دار العودة" },
            new Book { Title = "جدارية", Description = "ديوان شعر.", AuthorId = "8", Language = "Arabic", ISBN = "9789953890874", PublicationDate = new DateOnly(2000, 1, 1), PageCount = 100, Publisher = "دار العودة" },

            // صنع الله إبراهيم
            new Book { Title = "اللجنة", Description = "رواية سياسية.", AuthorId = "9", Language = "Arabic", ISBN = "9789953890881", PublicationDate = new DateOnly(1981, 1, 1), PageCount = 200, Publisher = "دار المستقبل العربي" },

            // رضوى عاشور
            new Book { Title = "ثلاثية غرناطة", Description = "رواية تاريخية.", AuthorId = "10", Language = "Arabic", ISBN = "9789953890898", PublicationDate = new DateOnly(1994, 1, 1), PageCount = 500, Publisher = "دار الشروق" },

            // توفيق الحكيم
            new Book { Title = "عودة الروح", Description = "رواية فلسفية.", AuthorId = "11", Language = "Arabic", ISBN = "9789953890904", PublicationDate = new DateOnly(1933, 1, 1), PageCount = 320, Publisher = "دار الشروق" },

            // يوسف إدريس
            new Book { Title = "الحرام", Description = "رواية اجتماعية.", AuthorId = "12", Language = "Arabic", ISBN = "9789953890911", PublicationDate = new DateOnly(1959, 1, 1), PageCount = 220, Publisher = "دار الشروق" },

            // صلاح عبد الصبور
            new Book { Title = "مأساة الحلاج", Description = "مسرحية شعرية.", AuthorId = "13", Language = "Arabic", ISBN = "9789953890928", PublicationDate = new DateOnly(1964, 1, 1), PageCount = 150, Publisher = "دار الشروق" },

            // إبراهيم نصر الله
            new Book { Title = "زمن الخيول البيضاء", Description = "رواية فلسطينية.", AuthorId = "14", Language = "Arabic", ISBN = "9789953890935", PublicationDate = new DateOnly(2007, 1, 1), PageCount = 900, Publisher = "دار الشروق" },

            // جمال الغيطاني
            new Book { Title = "الزيني بركات", Description = "رواية تاريخية.", AuthorId = "15", Language = "Arabic", ISBN = "9789953890942", PublicationDate = new DateOnly(1974, 1, 1), PageCount = 320, Publisher = "دار الشروق" },

            // عبد الرحمن منيف
            new Book { Title = "مدن الملح", Description = "خماسية روائية.", AuthorId = "16", Language = "Arabic", ISBN = "9789953890959", PublicationDate = new DateOnly(1984, 1, 1), PageCount = 600, Publisher = "المؤسسة العربية للدراسات" },

            // أحلام مستغانمي
            new Book { Title = "ذاكرة الجسد", Description = "رواية جزائرية.", AuthorId = "17", Language = "Arabic", ISBN = "9789953890966", PublicationDate = new DateOnly(1993, 1, 1), PageCount = 400, Publisher = "دار الآداب" },

            // William Shakespeare
            new Book { Title = "Romeo and Juliet", Description = "A tragedy.", AuthorId = "18", Language = "English", ISBN = "9780141396474", PublicationDate = new DateOnly(1597, 1, 1), PageCount = 320, Publisher = "Penguin Classics" },
            new Book { Title = "Hamlet", Description = "A tragedy.", AuthorId = "18", Language = "English", ISBN = "9780141396504", PublicationDate = new DateOnly(1603, 1, 1), PageCount = 400, Publisher = "Penguin Classics" },
            new Book { Title = "Macbeth", Description = "A tragedy.", AuthorId = "18", Language = "English", ISBN = "9780141396313", PublicationDate = new DateOnly(1606, 1, 1), PageCount = 272, Publisher = "Penguin Classics" },

            // Jane Austen
            new Book { Title = "Pride and Prejudice", Description = "A romantic novel.", AuthorId = "19", Language = "English", ISBN = "9780141439518", PublicationDate = new DateOnly(1813, 1, 1), PageCount = 432, Publisher = "Penguin Classics" },
            new Book { Title = "Sense and Sensibility", Description = "A romantic novel.", AuthorId = "19", Language = "English", ISBN = "9780141439662", PublicationDate = new DateOnly(1811, 1, 1), PageCount = 400, Publisher = "Penguin Classics" },

            // Mark Twain
            new Book { Title = "Adventures of Huckleberry Finn", Description = "A classic novel.", AuthorId = "20", Language = "English", ISBN = "9780486280615", PublicationDate = new DateOnly(1884, 1, 1), PageCount = 366, Publisher = "Dover Publications" },
            new Book { Title = "The Adventures of Tom Sawyer", Description = "A classic novel.", AuthorId = "20", Language = "English", ISBN = "9780143039563", PublicationDate = new DateOnly(1876, 1, 1), PageCount = 274, Publisher = "Penguin Classics" },

            // Gabriel García Márquez
            new Book { Title = "One Hundred Years of Solitude", Description = "A magical realism novel.", AuthorId = "21", Language = "Spanish", ISBN = "9780060883287", PublicationDate = new DateOnly(1967, 1, 1), PageCount = 417, Publisher = "Harper Perennial" },
            new Book { Title = "Love in the Time of Cholera", Description = "A romantic novel.", AuthorId = "21", Language = "Spanish", ISBN = "9780307389732", PublicationDate = new DateOnly(1985, 1, 1), PageCount = 348, Publisher = "Vintage" },

            // Leo Tolstoy
            new Book { Title = "War and Peace", Description = "A historical novel.", AuthorId = "22", Language = "Russian", ISBN = "9780199232765", PublicationDate = new DateOnly(1869, 1, 1), PageCount = 1296, Publisher = "Oxford University Press" },
            new Book { Title = "Anna Karenina", Description = "A tragic novel.", AuthorId = "22", Language = "Russian", ISBN = "9780143035008", PublicationDate = new DateOnly(1877, 1, 1), PageCount = 864, Publisher = "Penguin Classics" },

            // Ernest Hemingway
            new Book { Title = "The Old Man and the Sea", Description = "A short novel.", AuthorId = "23", Language = "English", ISBN = "9780684801223", PublicationDate = new DateOnly(1952, 1, 1), PageCount = 127, Publisher = "Scribner" },
            new Book { Title = "A Farewell to Arms", Description = "A war novel.", AuthorId = "23", Language = "English", ISBN = "9780684801469", PublicationDate = new DateOnly(1929, 1, 1), PageCount = 355, Publisher = "Scribner" },

            // Virginia Woolf
            new Book { Title = "To the Lighthouse", Description = "A modernist novel.", AuthorId = "24", Language = "English", ISBN = "9780156907392", PublicationDate = new DateOnly(1927, 1, 1), PageCount = 209, Publisher = "Harcourt" },
            new Book { Title = "Mrs Dalloway", Description = "A modernist novel.", AuthorId = "24", Language = "English", ISBN = "9780156628709", PublicationDate = new DateOnly(1925, 1, 1), PageCount = 194, Publisher = "Harcourt" },

            // Franz Kafka
            new Book { Title = "The Metamorphosis", Description = "A novella.", AuthorId = "25", Language = "German", ISBN = "9780553213690", PublicationDate = new DateOnly(1915, 1, 1), PageCount = 201, Publisher = "Bantam Classics" },
            new Book { Title = "The Trial", Description = "A novel.", AuthorId = "25", Language = "German", ISBN = "9780805210408", PublicationDate = new DateOnly(1925, 1, 1), PageCount = 255, Publisher = "Schocken" },

            // Haruki Murakami
            new Book { Title = "Norwegian Wood", Description = "A coming-of-age novel.", AuthorId = "26", Language = "Japanese", ISBN = "9780375704024", PublicationDate = new DateOnly(1987, 1, 1), PageCount = 296, Publisher = "Vintage" },
            new Book { Title = "Kafka on the Shore", Description = "A surreal novel.", AuthorId = "26", Language = "Japanese", ISBN = "9781400079278", PublicationDate = new DateOnly(2002, 1, 1), PageCount = 505, Publisher = "Vintage" },

            // Chinua Achebe
            new Book { Title = "Things Fall Apart", Description = "A classic African novel.", AuthorId = "27", Language = "English", ISBN = "9780385474542", PublicationDate = new DateOnly(1958, 1, 1), PageCount = 209, Publisher = "Anchor Books" } };


    }
}