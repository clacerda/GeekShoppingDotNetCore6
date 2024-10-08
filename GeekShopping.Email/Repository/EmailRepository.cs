﻿using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySqlContext> _context;
        public EmailRepository(DbContextOptions<MySqlContext> context)
        {
            _context = context;
        } 

        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            EmailLog email = new EmailLog()
            {
                Email = message.Email,
                Sent = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created successfully!"
            };

            await using var _db = new MySqlContext(_context);
            _db.Add(email);

            await _db.SaveChangesAsync();
            
        }
    }
}
