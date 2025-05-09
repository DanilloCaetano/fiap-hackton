using Common.MessagingService.QueuesConfig;
using Common.MessagingService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Domain.User.Repository;
using Domain.User.Model;
using Common.Helpers;

namespace UserConsumer
{
    public class UserWorker : BackgroundService
    {
        private readonly ILogger<UserWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UserWorker(ILogger<UserWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Insert Worker running at: {time}", DateTimeOffset.Now);
            try
            {
                using var scopeService = _serviceProvider.CreateScope();
                var _rabbitMqService = scopeService.ServiceProvider.GetRequiredService<IRabbitMqService>();

                using var conn = await _rabbitMqService.GetConnection("rabbitmq", "guest", "guest");
                using var channel = await conn.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: QueueNames.UserInsert,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += User_InsertReceivedAsync;

                await channel.BasicConsumeAsync(
                    queue: QueueNames.UserInsert,
                    autoAck: true,
                    consumer: consumer,
                    noLocal: false,
                    exclusive: false,
                    consumerTag: string.Empty,
                    arguments: null);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Insert Worker error: {err}", ex.Message);
            }

            await Task.Delay(2500, stoppingToken);
        }

        private async Task User_InsertReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                using var scopeService = _serviceProvider.CreateScope();
                var _usersService = scopeService.ServiceProvider.GetRequiredService<IUserRepository>();

                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entity = JsonSerializer.Deserialize<UserEntity>(message);
                _logger.LogInformation("Insert Worker get entity: " + JsonSerializer.Serialize(entity));

                if (entity == null)
                {
                    _logger.LogInformation("Insert Worker error entity = null");
                    return;
                }

                entity.Password = Encrypt.TEncrypt(entity.Password);

                if (entity != null)
                {
                    await _usersService.AddAsync(entity);
                    _logger.LogInformation("Insert Worker added entity: " + JsonSerializer.Serialize(entity));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Insert Worker error: {err}", ex.Message);
            }

        }
    }
}
