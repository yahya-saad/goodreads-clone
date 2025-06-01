using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Persistence;

namespace Goodreads.Infrastructure.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IRepository<Author>? _authorsRepository;
    private IRepository<Genre>? _genresRepository;
    private IRepository<Book>? _bookRepository;
    private IRepository<Shelf>? _shelfRepository;
    private IRepository<BookShelf>? _bookShelfRepository;
    private IRepository<AuthorClaimRequest>? _authorClaimRequestRepository;
    private IRepository<Quote>? _quoteRepository;
    private IRepository<QuoteLike>? _quoteLikeRepository;
    private IRepository<ReadingProgress>? _readingProgressRepository;
    private IRepository<UserYearChallenge>? _userYearChallengeRepository;
    private IRepository<BookReview>? _bookReviewRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<Author> Authors => _authorsRepository ??= new GenericRepository<Author>(_context);
    public IRepository<Genre> Genres => _genresRepository ??= new GenericRepository<Genre>(_context);
    public IRepository<Book> Books => _bookRepository ??= new GenericRepository<Book>(_context);
    public IRepository<Shelf> Shelves => _shelfRepository ??= new GenericRepository<Shelf>(_context);
    public IRepository<BookShelf> BookShelves => _bookShelfRepository ??= new GenericRepository<BookShelf>(_context);
    public IRepository<AuthorClaimRequest> AuthorClaimRequests => _authorClaimRequestRepository ??=
                                            new GenericRepository<AuthorClaimRequest>(_context);
    public IRepository<Quote> Quotes => _quoteRepository ??= new GenericRepository<Quote>(_context);
    public IRepository<QuoteLike> QuoteLikes => _quoteLikeRepository ??= new GenericRepository<QuoteLike>(_context);
    public IRepository<ReadingProgress> ReadingProgresses => _readingProgressRepository ??= new GenericRepository<ReadingProgress>(_context);
    public IRepository<UserYearChallenge> UserYearChallenges => _userYearChallengeRepository ??= new GenericRepository<UserYearChallenge>(_context);
    public IRepository<BookReview> BookReviews => _bookReviewRepository ??= new GenericRepository<BookReview>(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();

}
