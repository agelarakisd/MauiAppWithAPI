﻿using Microsoft.EntityFrameworkCore;
using PassMaui.Application.Common.Interfaces;
using PassMaui.Domain;

namespace PassMaui.Infrastructure.Accounts.Persistence;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationContext _context;

    public AccountRepository(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<Account> Add(Account account, CancellationToken cancellationToken = default)
    {
        var res =  await _context.Accounts.AddAsync(account, cancellationToken);
        return res.Entity;
    }

    public void Delete(Account account)
    {
        _context.Accounts.Remove(account);
    }

    public async Task<Account> FindById(int id, CancellationToken cancellation = default)
    {
        var res = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null) 
        {
            throw new ArgumentNullException(nameof(res));
        }
        return res;
    }

    public async Task<List<Account>> GetAllAccounts(CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.ToListAsync(cancellationToken);
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}