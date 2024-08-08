# Notification System

This project is a minimal web API for a notification system, designed using the Vertical Slice architecture. The system supports sending notifications via two channels: SMS and Email. It uses MongoDB as the database, Fluent Email for email notifications, and integrates with MassTransit and RabbitMQ for event-driven communication.

## Features

- **Vertical Slice Architecture**: The project is structured in slices, each representing a feature with all related files.
- **Email Notifications**: Implemented using the [FluentEmail](https://github.com/lukencode/FluentEmail) package.
- **SMS Notifications**: Supports two SMS providers for flexibility and redundancy.
- **Event-Driven**: Uses MassTransit and RabbitMQ for subscribing to and handling events.
- **MongoDB**: Data persistence is handled using MongoDB.

## Technologies Used

- **.NET 8**: The core framework for building the API.
- **FluentEmail**: For sending email notifications.
- **MassTransit**: For message-based communication.
- **RabbitMQ**: As the message broker.
- **MongoDB**: For data storage.
- **Vertical Slice Architecture**: For organizing the codebase into features/slices.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

### Installation

1. **Clone the repository**
2. **Set the database connection configs**
3. **Set the api-key for using the sms provider and set the configuration for mail server**
3. **Ready to GO!**


## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
