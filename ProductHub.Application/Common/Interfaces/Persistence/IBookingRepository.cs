using ProductHub.Domain.Booking;

namespace ProductHub.Application.Common.Interfaces.Persistence;

public interface IBookingRepository
{
    Task AddBookingAsync(Booking booking);
    Task<Booking?> GetBookingByIdAsync(Guid id);
    Task<List<Booking>> GetBookingsAsync();
    Task UpdateBookingAsync(Booking booking);
    Task DeleteBookingAsync(Booking booking);
}