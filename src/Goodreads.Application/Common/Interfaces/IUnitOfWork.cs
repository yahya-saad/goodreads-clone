namespace Goodreads.Application.Common.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IRepository<Author> Authors { get; }
    IRepository<Genre> Genres { get; }
    IRepository<Book> Books { get; }
    IRepository<Shelf> Shelves { get; }
    IRepository<BookShelf> BookShelves { get; }
    IRepository<AuthorClaimRequest> AuthorClaimRequests { get; }
    IRepository<Quote> Quotes { get; }
    IRepository<QuoteLike> QuoteLikes { get; }
    IRepository<ReadingProgress> ReadingProgresses { get; }
    IRepository<UserYearChallenge> UserYearChallenges { get; }

    Task<int> SaveChangesAsync();
}
