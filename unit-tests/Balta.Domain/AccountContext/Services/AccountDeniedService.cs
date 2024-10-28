using System.Collections.Concurrent;
using Balta.Domain.AccountContext.Entities.Exceptions;
using Balta.Domain.AccountContext.Services.Abstractions;

namespace Balta.Domain.AccountContext.Services;

public class AccountDeniedService : IAccountDeniedService
{
    private const int MaxAccessAttempts = 3;
    private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(5);
    
    private readonly ConcurrentDictionary<string, (int Attempts, DateTime BlockedUntil)> _accessRecords = new();

    public Task<bool> IsAccountDenied(string email)
    {
        if (_accessRecords.TryGetValue(email, out var record))
        {
            if (record.BlockedUntil > DateTime.UtcNow)
            {
                throw new AccountDeniedException();
            }

            if (record.Attempts >= MaxAccessAttempts)
            {
                _accessRecords.TryUpdate(email, (0, DateTime.MinValue), record);
            }
        }

        var attempts = record.Attempts + 1;
        if (attempts > MaxAccessAttempts)
        {
            _accessRecords[email] = (attempts, DateTime.UtcNow.Add(BlockDuration));
            throw new AccountDeniedException();
        }

        _accessRecords[email] = (attempts, record.BlockedUntil);
        return Task.FromResult(false);
    }
}