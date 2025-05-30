using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Persistence.Seeders;

internal class AuthorsSeeder(ApplicationDbContext dbContext) : ISeeder
{

    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync() &&
            !await dbContext.Authors.AnyAsync())
        {
            var authors = GetAuthors();
            await dbContext.Authors.AddRangeAsync(authors);
            await dbContext.SaveChangesAsync();
        }
    }


    private IEnumerable<Author> GetAuthors()
    {
        var authors = new List<Author>
    {
        new Author { Id = "1", Name = "نجيب محفوظ", Bio = "كاتب مصري حائز على جائزة نوبل في الأدب." },
        new Author { Id = "2", Name = "طه حسين", Bio = "أديب ومفكر مصري بارز." },
        new Author { Id = "3", Name = "أليفه رفعت", Bio = "كاتبة مصرية متخصصة في القصص القصيرة." },
        new Author { Id = "4", Name = "غسان كنفاني", Bio = "كاتب وصحفي فلسطيني." },
        new Author { Id = "5", Name = "جبر إبراهيم جبر", Bio = "روائي وشاعر فلسطيني عراقي." },
        new Author { Id = "6", Name = "إلياس خوري", Bio = "روائي وناقد لبناني." },
        new Author { Id = "7", Name = "نزار قباني", Bio = "شاعر وسفير سوري." },
        new Author { Id = "8", Name = "محمود درويش", Bio = "شاعر وكاتب فلسطيني." },
        new Author { Id = "9", Name = "صنع الله إبراهيم", Bio = "روائي وكاتب مصري." },
        new Author { Id = "10", Name = "رضوى عاشور", Bio = "روائية وأكاديمية فلسطينية." },
        new Author { Id = "11", Name = "توفيق الحكيم", Bio = "كاتب مسرحي وروائي مصري." },
        new Author { Id = "12", Name = "يوسف إدريس", Bio = "كاتب مسرحي وروائي مصري." },
        new Author { Id = "13", Name = "صلاح عبد الصبور", Bio = "شاعر ومسرحي مصري." },
        new Author { Id = "14", Name = "إبراهيم نصر الله", Bio = "شاعر وروائي فلسطيني أردني." },
        new Author { Id = "15", Name = "جمال الغيطاني", Bio = "روائي وصحفي مصري." },
        new Author { Id = "16", Name = "عبد الرحمن منيف", Bio = "روائي سعودي معروف بأعماله عن العالم العربي." },
        new Author { Id = "17", Name = "أحلام مستغانمي", Bio = "كاتبة وشاعرة جزائرية." },

        new Author { Id = "18", Name = "William Shakespeare", Bio = "English playwright, poet, and actor." },
        new Author { Id = "19", Name = "Jane Austen", Bio = "English novelist known for romantic fiction." },
        new Author { Id = "20", Name = "Mark Twain", Bio = "American writer, humorist, and lecturer." },
        new Author { Id = "21", Name = "Gabriel García Márquez", Bio = "Colombian novelist and Nobel laureate." },
        new Author { Id = "22", Name = "Leo Tolstoy", Bio = "Russian author known for 'War and Peace'." },
        new Author { Id = "23", Name = "Ernest Hemingway", Bio = "American novelist and short story writer." },
        new Author { Id = "24", Name = "Virginia Woolf", Bio = "English writer and modernist pioneer." },
        new Author { Id = "25", Name = "Franz Kafka", Bio = "German-speaking Bohemian writer." },
        new Author { Id = "26", Name = "Haruki Murakami", Bio = "Contemporary Japanese novelist." },
        new Author { Id = "27", Name = "Chinua Achebe", Bio = "Nigerian novelist and critic." }
    };

        return authors;
    }


}